using UnityEngine;
using System.Collections;

/// The basic movement for a ship.
public abstract class ShipMovement : MonoBehaviour {

  public float maxSpeed = 11f;
  public float acceleration = 4f;
  public float turnSpeed = 280;
  
  private float currVelocity = 0f;

  protected void Accelerate() {
    currVelocity += acceleration * Time.deltaTime;
    if(currVelocity > maxSpeed) {
      currVelocity = maxSpeed;
    }
  }
  
  protected void Decelerate() {
    currVelocity -= acceleration * 0.5f * currVelocity * Time.deltaTime;
    if(currVelocity < 0) {
      currVelocity = 0;
    }
  }
  
  protected void Move() {
    transform.position += transform.forward * currVelocity * Time.deltaTime;
  }

  /* Turn to face the target at maximum turning speed. */
  protected void TurnTowards(Vector3 targetPosition) {
    float angle = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.Euler(new Vector3(0, -angle + 90, 0));
    
    /*transform.rotation = Quaternion.RotateTowards(
                                                  transform.rotation, 
                                                  Quaternion.LookRotation(targetPosition - transform.position, transform.up),
                                                  turnSpeed * Time.deltaTime
                                                  );*/
  }
  
  /* Turn to face the mouse at maximum turning speed. */ // <- TODO put in unity methods
  protected void TurnTowardsMouse() {
    Vector3 mousePosition = Input.mousePosition;
    Vector3 objPosition = Camera.main.WorldToScreenPoint(transform.position);
    mousePosition.x = mousePosition.x - objPosition.x;
    mousePosition.y = mousePosition.y - objPosition.y;
    float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.Euler(new Vector3(0, -angle + 90, 0));  
  }

  protected float DistanceFromPlayer() {
    GameObject player = GameObject.FindGameObjectWithTag("Player"); 
    return Vector3.Distance(transform.position, player.transform.position);
  }

}
