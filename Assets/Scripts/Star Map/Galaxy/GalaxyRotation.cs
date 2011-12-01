using UnityEngine;
using System.Collections;

public class GalaxyRotation : MonoBehaviour {

	public float RotationSpeed = 1;

  public void Update() {
    this.transform.Rotate(0, RotationSpeed * Time.deltaTime, 0);
  }

}
