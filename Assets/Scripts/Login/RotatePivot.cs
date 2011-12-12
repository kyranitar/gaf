using UnityEngine;
using System.Collections;

public class RotatePivot : MonoBehaviour {

  public float RotationSpeed = 4;

	void Update () {
    transform.Rotate(Vector3.up, Time.deltaTime * RotationSpeed);
	}
}
