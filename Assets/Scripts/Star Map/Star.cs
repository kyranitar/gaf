using UnityEngine;
using System.Collections;

public class Star : UtilBehaviour {

  public GameObject Marker;

  public void Start() {
    Vector3 pos = this.transform.position;
    Quaternion rot = this.transform.rotation;
    Marker = Instantiate(Marker, pos, rot) as GameObject;
    Marker.transform.parent = this.transform;
  }

  public void OnMouseUpAsButton() {
    Marker.GetComponent<Marker>().OnMouseUpAsButton();
  }
  
}
