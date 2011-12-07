using UnityEngine;
using System.Collections;

/// Controls the camera, allowing scrolling to change the zoom.
///
/// Authors: Timothy Jones & Daniel Atkins
public class CameraZoom : MonoBehaviour {

  public float InitialCameraZoom = 30;
  public float ZoomSpeed = 5;

  public void Start() {
    Vector3 pos = this.transform.position;
    this.transform.position = new Vector3(pos.x, InitialCameraZoom, pos.z);
  }

  public void Update() {
    transform.Translate(0, 0, Time.deltaTime * Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed, Camera.main.transform);

    Vector3 pos = this.transform.position;
    if (Input.GetKeyDown(KeyCode.J)) {
      this.transform.position = new Vector3(pos.x, 60, pos.z);
    } else if (Input.GetKeyDown(KeyCode.K)) {
      this.transform.position = new Vector3(pos.x, 130, pos.z);
    } else if (Input.GetKeyDown(KeyCode.L)) {
      this.transform.position = new Vector3(pos.x, 250, pos.z);
    }
  }
  
  
}
