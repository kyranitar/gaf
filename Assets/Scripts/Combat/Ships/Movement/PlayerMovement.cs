using UnityEngine;
using System.Collections;

/// The player's controlled movement of a ship.
/// 
/// Authors: Timothy Jones & Cam Owen & Richard Roberts
public class PlayerMovement : ShipMovement {

  public void Update() {
    if (Input.GetKey(KeyCode.W)) {
      Accelerate();
    } else if (Input.GetKey(KeyCode.S)) {
      Decelerate();
    }
    
    // Rotate to face mouse.
    TurnTowardsMouse();
    
    // Move forwards.
    Move();

    Transform cam = Camera.mainCamera.transform;
    Vector3 pos = this.transform.position;
    cam.position = new Vector3(pos.x, cam.position.y, pos.z);
  }
  
}
