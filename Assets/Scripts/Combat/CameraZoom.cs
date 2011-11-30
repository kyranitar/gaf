using UnityEngine;
using System.Collections;

/// Controls the camera, allowing scrolling to change the zoom.
///
/// Authors: Timothy Jones & Daniel Atkins
public class CameraZoom : MonoBehaviour {

	public float InitialCameraZoom = 30;

  public void Start() {
    Transform cam = Camera.mainCamera.transform;
    Vector3 pos = cam.position;
    cam.position = new Vector3(pos.x, InitialCameraZoom, pos.z);
  }

  public void Update() {
    if (Input.GetAxis("Mouse ScrollWheel") < 0) {
      Camera.mainCamera.transform.Translate(0, -1, 0);
    } else if (Input.GetAxis("Mouse ScrollWheel") > 0) {
      Camera.mainCamera.transform.Translate(0, 1, 0);
    }
  }

}
