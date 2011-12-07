using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cursor : MonoBehaviour {

  public GameObject[] cooldownBars;
  private List<Ability> abilities;

  private Vector3 planeNormal = new Vector3(0, 1, 0);
  private float cameraDist;
  private float currentProjectedNormal;
  private float cameraProjectedNormal;
  private float pOnPlaneNormal;
  private float yPos;
  private Camera mainCamera;

  public void Start() {

    // Maximum of four skills for now.
    abilities = new List<Ability>();
    mainCamera = findCamera();

    float angle = 360 / cooldownBars.Length;
    for (int i=0; i < cooldownBars.Length; i++) {
      cooldownBars[i] = Instantiate(cooldownBars[i], transform.position, transform.rotation) as GameObject;
      cooldownBars[i].transform.Rotate(Vector3.up, i * angle);
      cooldownBars[i].transform.parent = transform;
    }
  }

  public void Update() {
    resizeBars();
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

  private void resizeBars() {

    for (int i = 0; i < abilities.Count; i++) {

      if (i > cooldownBars.Length) {
        Debug.LogError("Not enough cooldown bars");
        return;
      }

      float maxCooldown = abilities[i].CooldownTime + abilities[i].Duration;
      float timeLeft =  abilities[i].CooldownTimeLeft + abilities[i].ActiveTimeLeft;
      float scaleAmount = (timeLeft == maxCooldown) ? 0.0f : (maxCooldown - timeLeft) / maxCooldown;
      cooldownBars[i].transform.localScale = new Vector3(1.0f, 1.0f, scaleAmount);
    }
  }

  public void AddAbilityReference(Ability ability) {
    abilities.Add(ability);
  }
}
