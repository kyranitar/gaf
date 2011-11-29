using UnityEngine;
using System.Collections;

/// The player's controlled movement of a ship.
/// 
/// Author: Timothy Jones & Cam Owen & Richard Roberts

public class PlayerMovement : ShipMovement {
 
  public float cameraZoom = -30;
  
  protected override void Start() {
  
  }
  
  protected override void Update() {
    
    // Camera zoom.
    if (Input.GetAxis("Mouse ScrollWheel") < 0) {
      cameraZoom --;
    } else if (Input.GetAxis("Mouse ScrollWheel") > 0) {
      cameraZoom ++;
    }
    Camera.mainCamera.transform.position = new Vector3(transform.position.x, cameraZoom, transform.position.z);
  
    if (Input.GetKey(KeyCode.W)) {
      Accelerate();
    } else if (Input.GetKey(KeyCode.S)) {
      Decelerate();
    }

    // Rotate to face mouse.
    /* Vector3 pos = transform.position;
    Vector3 mouse = Input.mousePosition;
    mouse = Camera.mainCamera.ScreenToWorldPoint(mouse);
    mouse.z = pos.z;
    TurnTowards(mouse); */
    TurnTowardsMouse();

    // Move forwards.
    Move();
  }

}
