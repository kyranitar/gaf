using UnityEngine;

/// AI movement behaviour for ships.
/// 
/// Authors: Timothy Jones & Cam Owen
public class AIMovement : ShipMovement {

  /// 1/X chance the ship will change its target.
  public int ChangeTargetChance = 100;

  /// Range the ship can set its target to.
  public int MoveTargetRange = 10;

  // Turn angle at which the ship will accelerate/decelerate.
  public float AccelerationThreshold = 160;

  // Current destination.
  private Vector3 moveTarget;

  public void Start() {
    pickRandomTarget();
  }

  public void Update() {
    // Move towards the target.
    TurnTowards(moveTarget);

    Vector3 pos = moveTarget - transform.position;
    if (Vector3.Angle(pos, transform.forward) < AccelerationThreshold) {
      Accelerate();
    } else {
      Decelerate();
    }

    Move();

    maybePickTarget();
  }

  private void maybePickTarget() {
    // Generate random number for target behaviour.
    int r = Random.Range(0, ChangeTargetChance);

    if (r < 1) {
      pickRandomTarget();
    } else if (r < 2) {
      aggressiveMoveTarget();
    } else if (r < 3) {
      defensiveMoveTarget();
    }
  }

  void pickRandomTarget() {
    /* Choose a random point within range */
    int x = Random.Range(-MoveTargetRange, MoveTargetRange);
    int y = Random.Range(-MoveTargetRange, MoveTargetRange);

    SetMoveTarget(new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z));
  }

  /* Aggressive Movement tracks to the nearest opponent */
  private void aggressiveMoveTarget() {
    TeamTarget target = gameObject.GetComponent<TargetMarker>().Targeting;
    Transform nearestOpponent = target.FindNearestTarget(this.transform.position).transform;

    if (!nearestOpponent) {
      pickRandomTarget();
    } else {
      SetMoveTarget(nearestOpponent.position);
    }
  }

  /* Defensive Movement tracks away from the nearest opponent */
  private void defensiveMoveTarget() {
    TeamTarget target = gameObject.GetComponent<TargetMarker>().Targeting;
    Transform nearestOpponent = target.FindNearestTarget(this.transform.position).transform;

    if (!nearestOpponent) {
      pickRandomTarget();
    } else {
      Vector3 pos = nearestOpponent.position - transform.position;
      SetMoveTarget(pos.normalized * -MoveTargetRange + transform.position);
    }
  }

  private void SetMoveTarget(Vector3 vector) {
    vector.z = transform.position.z;
    this.moveTarget = vector;
  }

}
