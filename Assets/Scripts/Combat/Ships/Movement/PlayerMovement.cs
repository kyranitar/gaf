using UnityEngine;
using System.Collections;

/// The player's controlled movement of a ship.
/// 
/// Author: Timothy Jones & Cam Owen
public class PlayerMovement : ShipMovement {

  public void Update() {
    if (Input.GetKey(KeyCode.W)) {
      Accelerate();
    } else if (Input.GetKey(KeyCode.S)) {
      Decelerate();
    }

    // Camera zoom.
    if (Input.GetAxis("Mouse ScrollWheel") < 0) {
      Camera.mainCamera.transform.Translate(0, 0, 1);
    } else if (Input.GetAxis("Mouse ScrollWheel") > 0) {
      Camera.mainCamera.transform.Translate(0, 0, -1);
    }

    Vector3 pos = transform.position;

    // Rotate to face mouse.
    Vector3 mouse = Input.mousePosition;
    mouse = Camera.mainCamera.ScreenToWorldPoint(mouse);
    mouse.z = pos.z;
    TurnTowards(mouse);

    // Move forwards.
    Move();
  }

}
