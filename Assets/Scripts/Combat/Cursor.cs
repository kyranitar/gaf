using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {

  public float cameraDist;
  public Vector3 planeNormal = new Vector3(0, 1, 0);
  public float currentProjectedNormal;
  public float cameraProjectedNormal;
  public float pOnPlaneNormal;

  private float yPos;

  private GameObject[] cooldownsBars;

  void Start() {

    // Maximum of four skills for now.
    cooldownsBars = new GameObject[4];
  }


  void Update() {
    Drag();
    transform.Translate(10.0f, 0.0f, 0.0f);
    transform.localScale = new Vector3(0f, 1, 4.0f);
  }

  void OnMouseDown() {
    yPos = transform.position.y;
  }

  Camera FindCamera() {
    if (camera)
      return camera;
    else
      return Camera.main;
  }

  void Drag() {
    Vector3 p;
    Camera mainCamera = FindCamera();
    
    // Position on the near clipping plane of the camera in world space
    p = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.nearClipPlane));
    // Position relative to the eye-point of the camera
    p -= mainCamera.transform.position;
    
    currentProjectedNormal = Vector3.Dot(planeNormal, transform.position);
    cameraProjectedNormal = Vector3.Dot(planeNormal, mainCamera.transform.position);
    cameraDist = cameraProjectedNormal - currentProjectedNormal;
    pOnPlaneNormal = Vector3.Dot(planeNormal, p);
    
    p *= cameraDist / -pOnPlaneNormal;
    p += mainCamera.transform.position;
    transform.position = p;
    
    // If we know that the object should only move in its x,z coordinates, we can
    // make sure we don't drift slowly off the plane by doing this:
    transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
  }
}
