using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cursor : MonoBehaviour {

  private Vector3 planeNormal = new Vector3(0, 1, 0);
  private float cameraDist;
  private float currentProjectedNormal;
  private float cameraProjectedNormal;
  private float pOnPlaneNormal;
  private float yPos;
  private Camera mainCamera;

  public void Start() {

    mainCamera = findCamera();
  }

  public void Update() {
    move();
  }

  private Camera findCamera() {
    if (camera)
      return camera;
    else
      return Camera.main;
  }

  private void move() {

    Vector3 p;

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
    yPos = transform.position.y;
    transform.position = new Vector3(transform.position.x, yPos, transform.position.z);

  }
}
