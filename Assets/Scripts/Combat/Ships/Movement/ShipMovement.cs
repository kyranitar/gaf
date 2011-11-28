using UnityEngine;
using System.Collections;

/// The basic movement for a ship.
public abstract class ShipMovement : MonoBehaviour {

  /// The speed that the ship will move at.
  public float Speed = 1;

  /// The maximum speed the ship can turn at.
  public float MaximumSpeed = 11;

  /// The acceleration of the ship.
  public float Acceleration = 4;

  /// The rotation speed of the ship.
  public float RotationSpeed = 60;

  /// Speed the ship up (Thrusters).
  protected void Accelerate() {
    Speed += Acceleration * Time.deltaTime;

    if (Speed > MaximumSpeed)
      Speed = MaximumSpeed;
  }

  /// Slow the ship down (Brakes).
  protected void Decelerate() {
    Speed -= Acceleration / 2 * Time.deltaTime;

    if (Speed < 0) {
      Speed = 0;
    }
  }

  /// Move the ship forwards.
  protected void Move() {
    transform.position += transform.forward * Speed * Time.deltaTime;
  }

  /// Turn left at maximum turning speed.
  protected void TurnLeft() {
    transform.RotateAround(transform.up, -RotationSpeed * Time.deltaTime);
  }

  /// Turn right at maximum turning speed.
  protected void TurnRight() {
    transform.RotateAround(transform.up, RotationSpeed * Time.deltaTime);
  }

  /// Turn to face the target at maximum turning speed.
  protected void TurnTowards(Vector3 targetPosition) {
    Quaternion look = Quaternion.LookRotation(targetPosition - transform.position, transform.up);
    float rot = RotationSpeed * Time.deltaTime;
    transform.rotation = Quaternion.RotateTowards(this.transform.rotation, look, rot);
  }

}
