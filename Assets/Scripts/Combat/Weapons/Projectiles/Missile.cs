using UnityEngine;
using System.Collections;

/// The specialised tracking behaviour for a missile.
/// 
/// Authors: Timothy Jones & Cam Owen
public class Missile : Projectile {

  /// The acceleration rate of the missile.
  public float Acceleration;

  /// The rotation speed on the 2D plane of the missile.
  public float RotationSpeed;

  /// The spin speed of the missile (aesthetic).
  public float SpinSpeed;

  /// The greatest speed the missile can reach through acceleration.
  public float MaximumSpeed;

  /// The missile's current target. If null then no target.
  private Transform target = null;

  public override void Update() {
    // If our target no longer exists, pick out a new one.
    if (target == null) {
      recalculateTarget();
    }

    // Turn towards the target.
    if (target != null) {
      Vector3 pos = target.position - this.transform.position;
      float rot = RotationSpeed * Time.deltaTime;

      Quaternion look = Quaternion.LookRotation(pos, transform.up);
      transform.rotation = Quaternion.RotateTowards(this.transform.rotation, look, rot);
    }

    // Accelerate the missile.
    if (Speed < MaximumSpeed) {
      Speed += Acceleration * Time.deltaTime;

      // Cap speed at MaximumSpeed.
      if (Speed > MaximumSpeed) {
        Speed = MaximumSpeed;
      }
    }

    // Spin missile on its axis.
    float spin = SpinSpeed * Time.deltaTime;
    transform.RotateAround(transform.forward, spin);

    // Perform the normal movement.
    base.Update();
  }

  private void recalculateTarget() {
    target = Targeting.FindNearestTarget(this.transform.position).transform;
  }

}
