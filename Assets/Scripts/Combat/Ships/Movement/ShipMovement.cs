using UnityEngine;
using System.Collections;

/// The basic movement for a ship.
public abstract class ShipMovement : MonoBehaviour {

  public float MaxSpeed = 11;
  public float Acceleration = 4;
  public float Deceleration = 4;
  public float TurnSpeed = 280;

  private float currVelocity = 0;

  public virtual void Update() {
    if (currVelocity > MaxSpeed) {
      currVelocity = MaxSpeed;
    }
    
    this.transform.position += this.transform.forward * currVelocity * Time.deltaTime;
  }

  public void Accelerate() {
    currVelocity += Acceleration * Time.deltaTime;
    if (currVelocity > MaxSpeed) {
      currVelocity = MaxSpeed;
    }
  }

  public void Decelerate() {
    currVelocity -= Deceleration * currVelocity * Time.deltaTime;
    if (currVelocity < 0) {
      currVelocity = 0;
    }
  }

  public void TurnLeft() {
    this.transform.Rotate(0, -TurnSpeed * Time.deltaTime, 0);
  }

  public void TurnRight() {
    this.transform.Rotate(0, TurnSpeed * Time.deltaTime, 0);
  }

  /// Turn to face the given point at maximum turning speed.
  public void TurnTowards(float x, float y, float z) {
    TurnTowards(new Vector3(x, y, z));
  }

  /// Turn to face the given point at maximum turning speed.
  public void TurnTowards(Vector3 targetPosition) {
    Quaternion look = Quaternion.LookRotation(targetPosition - transform.position, transform.up);
    transform.rotation = Quaternion.RotateTowards(transform.rotation, look, TurnSpeed * Time.deltaTime);
  }
  
}
