using UnityEngine;
using System.Collections;

public class AIMovement : ShipMovement {


  public bool debugActions;
  public bool doISeekAllies;

  public float accel = 160f;
  public int accelMultiplier = 5;
  public int decelMultiplier = 1;

  public int decisionTime = 120;
  public float inFrontAngle = 20;
  public int range = 10;

  public Vector2 chanceFront = new Vector2(0.85f, 0.1f);
  public Vector2 chanceNear = new Vector2(0.9f, 0.05f);
  public float chanceAlly = 0.9f;
  public float chanceAnyEnemy = 0.5f;

  public float distFront = 1.2f;
  public float distNear = 2.0f;
  public float distMin = 3.0f;

  private Transform moveTarget;
  public GameObject targetery;
  private GameObject searchTarget;
  private Targeter targeter;

  private float aiTimer;
  private bool aiLocked;

  private AISkills skillsRef;
  private AIWeapons weaponsRef;

  public void Start() {
    
    // Update distances
    distFront *= range;
    distNear *= range;
    distMin *= range;
    
    //targetery = Instantiate(targetery, transform.position, transform.rotation) as GameObject;
    //targeter = targetery.GetComponent<Targeter>();
    
    searchTarget = new GameObject("searchTarget");

    skillsRef = gameObject.GetComponent<AISkills>();
    weaponsRef = gameObject.GetComponent<AIWeapons>();

    if(skillsRef == null) {
      Debug.Log("FAIL");
    }

    UnlockAi();
  }

  public void Update() {
    // Decrease lock timer, or if unlocked then make new decision.
    if (aiLocked) {
      aiTimer--;
      if (aiTimer <= 0)
        UnlockAi();
    } else {
      Decide();
    }

    if (moveTarget == null) {
      UnlockAi();
      Decide();
    } else {
      TurnTowards(moveTarget.position);
    }

    //targeter.GiveOwner(moveTarget);

    if (Vector3.Angle(moveTarget.position - transform.position, transform.forward) < accel) {
      // Hack
      for (int i = 0; i < accelMultiplier; i++) {
        Accelerate();
      }
    } else {
      // Hack
      for (int i = 0; i < decelMultiplier; i++) {
        Decelerate();
      }
    }
    Move();
  }

  #region Following
  bool FollowClosestFriend(int length) {
    // Find closest ally (removed if closest ally is self).
    Transform nearestAllyPos;
    TeamTarget target = gameObject.GetComponent<TargetMarker>().AlliedTargets;
    
    GameObject nearestAlly = target.FindNearestTarget(this.transform.position);
    if (nearestAlly != null) {
      nearestAllyPos = nearestAlly.transform;
    } else {
      return false;
    }
    
    // Make sure ally is not self.
    if (nearestAllyPos == transform) {
      nearestAllyPos = null;
    }
    
    // Let the decision tree know if we found friend or not.
    if (nearestAllyPos) {
      SetMoveTarget(nearestAllyPos, false);
      LockAi(length);
      return true;
    } else {
      return false;
    }
  }

  bool FollowClosestEnemy(int length, float maxDist, bool mustBeInFront, bool attack) {
    
    // Find closest enemy (removed if closest enemy is self).
    Transform nearestEnemyPos;
    TeamTarget target = gameObject.GetComponent<TargetMarker>().EnemyTargets;
    
    // Try and find any enemy in the game.
    GameObject nearestEnemy = target.FindNearestTarget(this.transform.position);
    if (nearestEnemy != null) {
      nearestEnemyPos = nearestEnemy.transform;
    } else {
      return false;
    }
    
    // Make sure ally is not self.
    if (nearestEnemyPos == transform) {
      nearestEnemyPos = null;
    }
    
    // Drop enemy if further away than the maximum distance.
    if (maxDist > 0 && nearestEnemyPos) {
      if (Vector3.Distance(nearestEnemyPos.position, transform.position) > maxDist) {
        return false;
      }
    }
    
    if (mustBeInFront && nearestEnemyPos) {
      Vector3 targetDir = nearestEnemyPos.position - transform.position;
      float angleBetween = Vector3.Angle(transform.forward, targetDir);
      float offset = 0;
      if (angleBetween > 0 - inFrontAngle * 0.5 + offset && angleBetween < inFrontAngle * 0.5 + offset) {

      } else {
        return false;
      }
    }
    
    // Let the decision tree know if we found enemy or not.
    if (nearestEnemyPos) {
      SetMoveTarget(nearestEnemyPos.transform, attack);
      LockAi(length);
      if (debugActions) {
        Debug.Log("I am following " + nearestEnemyPos.name + " at: " + nearestEnemyPos.position);
      }
      return true;
    } else {
      return false;
    }
  }
  #endregion

  #region Search
  void Search() {
    /* Move randomly, to appear as though searching. */    
    int x = Random.Range(-range * 4, range * 4);
    int z = Random.Range(-range * 4, range * 4);
    searchTarget.transform.position = new Vector3(transform.position.x + x, 0.0f, transform.position.z + z);
    SetMoveTarget(searchTarget.transform, false);
  }
  #endregion

  void LockAi(float length) {
    aiLocked = true;
    aiTimer = length;
  }

  void UnlockAi() {
    aiLocked = false;
    aiTimer = 0;
  }

  float RandomRoll() {
    return Random.Range(0.0f, 1.0f);
  }

  void SetMoveTarget(Transform target, bool attack) {

    moveTarget = target;

    if (attack) {
      skillsRef.Target = target;
      weaponsRef.Target = target;
    } else {
      skillsRef.Target = null;
      weaponsRef.Target = null;
    }
  }

  #region Decision Tree
  /// Make a decision based on the current situation (only runs if AI behaviour is unlocked)
  void Decide() {
    
    // Lets us know when to quit
    bool done;
    
    // If enemy within cone infront of this ship
    if (RandomRoll() < chanceFront.x) {
      
      if (RandomRoll() < chanceFront.y) {
        done = FollowClosestEnemy(decisionTime * 10, distFront, true, true);
      } else {
        done = FollowClosestEnemy(decisionTime * 2, distFront, true, true);
      }
      if (done) {
        if (debugActions) {
          Debug.Log("following infront enemy");
        }
        return;
      }
    }
    
    // If enemy is near
    if (RandomRoll() < chanceNear.x) {
      
      if (RandomRoll() < chanceNear.y) {
        done = FollowClosestEnemy(decisionTime * 10, distNear, false, false);
      } else {
        done = FollowClosestEnemy(decisionTime, distNear, false, false);
      }
      if (done) {
        if (debugActions)
          Debug.Log("successfully following near enemy");
        return;
      }
    }
    
    // If ally is near
    if (RandomRoll() < chanceAlly && doISeekAllies) {
      
      done = FollowClosestFriend(decisionTime * 2);
      if (done) {
        if (debugActions)
          Debug.Log("following ally");
        return;
      }
    }
    
    // If there are any enemies chase them down
    if (RandomRoll() < chanceAnyEnemy) {
      done = FollowClosestEnemy(decisionTime * 3, 0, false, false);
      if (done) {
        if (debugActions)
          Debug.Log("EODT - following enemy");
        return;
      }
    }
    
    // If nothing else then search
    if (debugActions)
      Debug.Log("EODT - searching");
    Search();
    LockAi(decisionTime * 0.3f);
  }
  #endregion
}
