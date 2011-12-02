using UnityEngine;
using System.Collections;

public class Targeter : MonoBehaviour {
 
  private Transform owner;

  void Start () {
	
  }
	
	void Update () {
    
      
	}
  
  public void GiveOwner(Transform ownerRef) {
    Vector3 pos = new Vector3(ownerRef.position.x, ownerRef.position.y, ownerRef.position.z);
    transform.position = pos;
  }
}
