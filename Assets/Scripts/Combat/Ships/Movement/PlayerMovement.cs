using UnityEngine;
using System.Collections;

/// The player's controlled movement of a ship.
/// 
/// Authors: Timothy Jones & Cam Owen & Richard Roberts
public class PlayerMovement : ShipMovement {
 
  private new Transform camera;

  public void Start() {
    camera = Camera.mainCamera.gameObject.transform;
    this.transform.Rotate(Vector3.up, -90);
  }
  
  public override void Update() {
    base.Update();

    // The remaining changes are handled by the PlayerControls component.

    // Make the camera follow player.
    camera.position = new Vector3(transform.position.x, camera.position.y, transform.position.z);
  }
}
