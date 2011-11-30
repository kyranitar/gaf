using UnityEngine;
using System.Collections.Generic;

public class Marker : UtilBehaviour {

  private static Marker currentDestination = null;

  public bool IsDestination {
    set {
      if (value == false) {
        this.renderer.material.color = new Color(0.5f, 0, 0);
        return;
      }

      if (currentDestination != null && currentDestination != this) {
        currentDestination.IsDestination = false;
      }

      this.renderer.material.color = new Color(0, 1, 0);
      currentDestination = this;
      
      GameObject player = GameObject.FindGameObjectWithTag("Player");
      Player script = player.GetComponent<Player>();
      script.TravelTo(gameObject);
    }
  }

  public void OnMouseUpAsButton(){
    this.IsDestination = true;
    
    GameObject player = GameObject.FindGameObjectWithTag("Player");
    
    if (ThisY(player.transform.position) == transform.position) {
      Application.LoadLevel("Combat");

    } else if (this != currentDestination) {
      Player pScript = player.GetComponent<Player>();
      pScript.Stop();
    }
  }

}
