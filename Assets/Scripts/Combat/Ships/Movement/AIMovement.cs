using UnityEngine;
using System.Collections;

public class AIMovement : ShipMovement {
  
  #region Fields Set in Unity GUI
  public bool debugActions;
  public bool doISeekAllies;
  public bool doISeekObjectives;
  public bool doIRetreat;
  
  public int decisionTime = 120;
  public float inFrontAngle = 20;
  public float attackRate = 0.7f;
  public float accel = 160f;
  public int range = 10;
  
  public Vector2 chanceFront = new Vector2(0.85f, 0.1f);
  public Vector2 chanceNear = new Vector2(0.9f, 0.05f);
  public float chanceAlly = 0.9f;
  public float chanceObjective = 0.9f;
  public float chanceRetreat = 0.95f;
  
  public float distFront = 1.2f;
  public float distNear = 2.0f;
  public float distMin = 3.0f;
  #endregion
  
  #region Other fields
  private Transform moveTarget;
  private Transform attackTarget;
  private GameObject searchTarget;
  private Transform objective;
  private Transform home;
  private int aiTimer;
  private bool aiLocked;
  private bool aiAttack;
  #endregion
  
  public void Start () {
    objective = gameObject.GetComponent<Locations>().objectiveLocation;
    home = gameObject.GetComponent<Locations>().baseLocation;
    
    /* Update distances */
    distFront *= range;
    distNear *= range;
    distMin *= range;
    
    /* Instatiate search target, and unlock AI behaviour. */
    if(searchTarget == null) searchTarget = new GameObject("searchTarget");
    UnlockAi();
  }
  
  public void Update () {
    /* Decrease lock timer, or if unlocked then make new decision. */
    if (aiLocked) {
      aiTimer --;
      if (aiTimer <= 0) UnlockAi(); 
    } else {
      Decide();
    }
    
    /* Guns a'blazing, and also stop attack if target has been deleted (killed like a mofo). */
    if (!attackTarget) {
      StopAttack();
    }
    
    /* Turning and Movement */
    TurnTowards(moveTarget.position);
    if(Vector3.Angle(moveTarget.position - transform.position, transform.forward) < accel) {
      Accelerate();
    } else {
      Decelerate();
    }
    Move();
    
    if (aiAttack && RandomRoll() < attackRate) {
      //Shoot();
    }
    
  }
  
  #region Following
  bool FollowClosestFriend(int length) {
    
    /* Find closest ally (removed if closest ally is self). */
    // old -- Transform nearestAllyPos = Targeting.FindNearestTag(bFriendly? "Friendly" : "Enemy", transform);
    TeamTarget target = gameObject.GetComponent<TargetMarker>().AlliedTargets;
    Transform nearestAllyPos = target.FindNearestTarget(this.transform.position).transform;
    if (nearestAllyPos == transform) nearestAllyPos = null;
    
    /* Turn back on if player is tagged player
    // If no allies found follow player (must be on players team)
    if (bFriendly && !nearestAllyPos) {
      nearestAllyPos = playerRef.transform;
    }
    
    // If ally found, but player is closer then follow player (must be on players team)
    if (bFriendly && DistanceFromPlayer() < Vector3.Distance(nearestAllyPos.position, transform.position)) {
      nearestAllyPos = playerRef.transform;
    }
    */
    
    /* Let the decide method know if we found friend or not */
    if(nearestAllyPos) {
      SetMoveTarget(nearestAllyPos);
      LockAi(length);
      return true;
    } else {
      return false;
    }
    
  }
    
  bool FollowClosestEnemy(int length, float maxDist, bool attack, bool mustBeInFront) {
    
    /* Find closest enemy (removed if closest enemy is self). */
    // old -- Transform nearestEnemyPos = Targeting.FindNearestTag(bFriendly? "Enemy" : "Friendly", transform);
    TeamTarget target = gameObject.GetComponent<TargetMarker>().EnemyTargets;
    Transform nearestEnemyPos = target.FindNearestTarget(this.transform.position).transform;
    
    if (nearestEnemyPos == transform) nearestEnemyPos = null;
    
    /* Turn back on if player is tagged player
    // If no enemies found follow player (must not be on players team)
    if (!bFriendly && !nearestEnemyPos) {
      nearestEnemyPos = playerRef.transform;
    }
    
    //If enemy found, but player is close then follow player (must not be on players team)
    if (!bFriendly && DistanceFromPlayer() < Vector3.Distance(nearestEnemyPos.position, transform.position)) {
      nearestEnemyPos = playerRef.transform;  
    }
    */
    
    /* Drop enemy if further away than the maximum distance. */
    if (maxDist > 0 && nearestEnemyPos) {
      if (Vector3.Distance(nearestEnemyPos.position, transform.position) > maxDist) {
        return false;
      }
    }
    
    if (mustBeInFront) {
      Vector3 targetDir = moveTarget.position - transform.position;
      float angleBetween = Vector3.Angle(transform.forward, targetDir); 
      if (angleBetween > 0 - inFrontAngle * 0.5 && angleBetween < inFrontAngle * 0.5) {
        return false; 
      }
    }
    
    /* Let the decide method know if we found enemy or not */
    if (nearestEnemyPos) {
      if(attack) SetAttackTarget(nearestEnemyPos.transform);
      SetMoveTarget(nearestEnemyPos.transform);
      LockAi(length);
      return true;
    } else {
      return false;
    }
  }
  #endregion
  
  #region Return to area
  void ReturnToHome() {
    SetMoveTarget(home);
  }
  
  void ReturnToObjective() {
    SetMoveTarget(objective);
  }
  #endregion
  
  #region Search
  void Search() {
    
    /* Move randomly, to appear as though searching. */
    int x = Random.Range(-range * 4, range * 4);
    int z = Random.Range(-range * 4, range * 4);
    searchTarget.transform.position = new Vector3(transform.position.x + x, 0.0f, transform.position.z + z);
    SetMoveTarget(searchTarget.transform);
  }
  #endregion
  
  #region Lock and Unlock AI Behaviour
  void LockAi(int length) {
    
    aiLocked = true;
    aiTimer = length;
  }
  
  void UnlockAi() {
    StopAttack();
    aiLocked = false;
    aiTimer = 0;
  }
  #endregion
  
  #region Attack Behaviour
  void StartAttack() {
    
    aiAttack = true;
  }
  
  void StopAttack() {
    
    ReturnToHome();
    aiAttack = false;
  }
  #endregion
  
  #region Helpers
  float RandomRoll() {
    return Random.Range(0.0f, 1.0f);
  }
  #endregion
  
  #region Setters (targets)
  void SetAttackTarget(Transform target) {
    StartAttack();
    attackTarget = target;
  }
  
  void SetMoveTarget(Transform target) {
    moveTarget = target;
  }
  #endregion
  
  #region Decision Tree
  /* Make a decision based on the current situation (only runs if AI behaviour is unlocked) */
  void Decide() {
    
    /* Lets us know when to quit */
    bool done;
    
    /* If enemy within cone infront of this ship */
    if (RandomRoll() < chanceFront.x) {
      
      if (RandomRoll() < chanceFront.y) {
        done = FollowClosestEnemy(decisionTime * 10, distFront, true, true);
      } else {
        done = FollowClosestEnemy(decisionTime * 2, distFront, true, true);
      }
      if (done) {
        if(debugActions) Debug.Log("following infront enemy");
        return;
      }
    }
    
    /* If enemy is near */
    if (RandomRoll() < chanceNear.x) {
      
      if (RandomRoll() < chanceNear.y) {
        done = FollowClosestEnemy(decisionTime * 10, distNear, true, false);
      } else {
        done = FollowClosestEnemy(decisionTime, distNear, true, false);
      }
      if (done) { 
        if(debugActions) Debug.Log("following near enemy");
        return;
      }
    }
    
    /* If ally is near */
    if (RandomRoll() < chanceAlly && doISeekAllies) {
      
      done = FollowClosestFriend(decisionTime * 2);
      if (done) {
        if(debugActions) Debug.Log("following ally");
        return;
      }
    }
    
    /* If far from objective */
    if (RandomRoll() < chanceObjective && doISeekObjectives && Vector3.Distance(objective.position, transform.position) > distMin) {
      if(debugActions) Debug.Log("going to objective");
      
      ReturnToObjective();
      LockAi(decisionTime * 2);
      return;
    }
    
    /* If can do special objective, or retreat */
    if (RandomRoll() < chanceRetreat && doIRetreat) {
      if(debugActions) Debug.Log("going home");
      
      ReturnToHome();
      LockAi(decisionTime * 2);
      return;
    }
    
    /* If there are any enemies chase them down */
    done = FollowClosestEnemy(decisionTime * 3, 0, false, false);
    if (done) {
      if(debugActions) Debug.Log("EODT - following enemy");
      return;
    }
    
    /* If nothing else then search */
    if(debugActions) Debug.Log("EODT - searching");
    Search();
    LockAi(decisionTime * 4);
  }
  #endregion
}
