using UnityEngine;
using System.Collections;

/// The player's controlled movement of a ship.
/// 
/// Authors: Timothy Jones & Cam Owen & Richard Roberts
public class PlayerMovement : ShipMovement {
 
  private Transform cam;
  
  public void Start() {
    cam = Camera.mainCamera.gameObject.transform;
    this.transform.Rotate(Vector3.up, -90);
  }
  
  public override void Update() {
    base.Update();

    // The remaining changes are handled by the PlayerControls component.

    // Make the camera follow player.
    cam.position = new Vector3(transform.position.x, cam.position.y, transform.position.z);
  }
  
}
