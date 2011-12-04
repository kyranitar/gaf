using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {

    if (Input.GetKeyDown(KeyCode.A)) {
      if (tag == "a") {
        transform.Translate(-1f, 0, 0);
      } else {
        transform.Translate(1f, 0, 0);
      }
    }

    if (Input.GetKeyDown(KeyCode.S)) {
      if (tag == "a") {
        transform.Translate(1f, 0, 0);
      } else {
        transform.Translate(-1f, 0, 0);
      }
    }
	
	}

  void OnTriggerEnter(Collider other) {
    Debug.Log("xx");
  }
}
