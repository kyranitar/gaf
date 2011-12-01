using UnityEngine;
using System.Collections;

public class Targeter : MonoBehaviour {
 
  private Transform owner;
	// Use this for initialization
	void Start () {
	
  }
	
	// Update is called once per frame
	void Update () {
    
      
	}
  
  public void GiveOwner(Transform ownerRef) {
    Vector3 pos = new Vector3(ownerRef.position.x, ownerRef.position.y, ownerRef.position.z);
    transform.position = pos;
  }
}
