using UnityEngine;
using System.Collections;

public class Flare : MonoBehaviour {

	// Use this for initialization
	public void Start () {
	  this.GetComponent<TeamMarker>().Start();
	}
	
	public void OnDestroy(){
    this.GetComponent<TeamMarker>().OnDestroy();
  }
}
