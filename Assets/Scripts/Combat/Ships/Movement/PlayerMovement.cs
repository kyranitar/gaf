using UnityEngine;
using System.Collections;

/// The player's controlled movement of a ship.
/// 
/// Authors: Timothy Jones & Cam Owen & Richard Roberts
public class PlayerMovement : ShipMovement {
 
  private Transform cam;
  
  public void Start() {
    
    cam = Camera.mainCamera.gameObject.transform;
  }
  
  public void Update() {
    
    float accel = Input.GetAxis("Vertical");
    if (accel > 0) {
      Accelerate();
    } else if (accel < 0) {
      Decelerate();
    }
    
    // Rotate to face mouse.
    TurnTowardsMouse();
    
    // Move forwards.
    Move();
    
    // Camera follow player.
    cam.position = new Vector3(transform.position.x, cam.position.y, transform.position.z);
  }
  
}
