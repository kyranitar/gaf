using UnityEngine;
using System.Collections;

/// Controls the camera, allowing scrolling to change the zoom.
///
/// Authors: Timothy Jones & Daniel Atkins
public class CameraZoom : MonoBehaviour {

  public float InitialCameraZoom = 30;
  public float ZoomSpeed = 5;

  private Transform cam;

  public void Start() {
    
    cam = Camera.mainCamera.transform;
    Vector3 pos = cam.position;
    cam.position = new Vector3(pos.x, InitialCameraZoom, pos.z);
  }

  public void Update() {
    
    transform.Translate(0, 0, Time.deltaTime * Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed, Camera.main.transform);
    
    
    if (Input.GetKeyDown(KeyCode.J)) {
      transform.position = new Vector3(transform.position.x, 60, transform.position.z);
    } else if (Input.GetKeyDown(KeyCode.K)) {
      transform.position = new Vector3(transform.position.x, 130, transform.position.z);
    } else if (Input.GetKeyDown(KeyCode.L)) {
      transform.position = new Vector3(transform.position.x, 250, transform.position.z);
    }
    
  }
  
  
}
