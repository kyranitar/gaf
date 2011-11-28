using UnityEngine;
using System.Collections;

public class Star : UtilBehaviour {

  public void OnMouseUpAsButton() {
    GameObject destination = GameObject.FindGameObjectWithTag("Destination");
    Destination dScript = destination.GetComponent<Destination>();
    dScript.TravelTo(gameObject);
    
    GameObject player = GameObject.FindGameObjectWithTag("Player");
    
    if (ThisZ(player.transform.position) == transform.position) {
      Application.LoadLevel("Combat");
      
    } else if (ThisZ(destination.transform.position) != transform.position) {
      Player pScript = player.GetComponent<Player>();
      pScript.Stop();
    }
  }
  
}
