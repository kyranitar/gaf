using UnityEngine;
using System.Collections;

public class Star : UtilBehaviour {

  public GameObject Marker;

  public void OnMouseUpAsButton() {
    Marker.GetComponent<StarMarker>().OnMouseUpAsButton();
  }
  
}
