using UnityEngine;
using System.Collections;

public class PlayerMarker : MonoBehaviour {

  public float RotationSpeed = 50;

  public void Update() {
    this.transform.Rotate(0, 0, RotationSpeed * Time.deltaTime);
  }

}
