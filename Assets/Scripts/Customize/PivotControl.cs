using UnityEngine;
using System.Collections;

public class PivotControl : MonoBehaviour {

  public float RotationSpeed = 100;

	void Update () {
    float val = Time.deltaTime * RotationSpeed * Input.GetAxis("Horizontal");
	  transform.Rotate(Vector3.up, val);
	}
}
