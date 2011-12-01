using UnityEngine;
using System.Collections.Generic;

public class StarMarker : UtilBehaviour {

  public float RotationSpeed = 50;

  private static StarMarker currentDestination = null;

  public bool IsDestination {
    set {
      if (value == false) {
        this.renderer.material.color = new Color(0.5f, 0, 0);
        return;
      }

      if (currentDestination != null && currentDestination != this) {
        currentDestination.IsDestination = false;
      }

      this.renderer.material.color = new Color(0, 0.5f, 0);
      currentDestination = this;
      
      GameObject player = GameObject.FindGameObjectWithTag("Player");
      Player script = player.GetComponent<Player>();
      script.TravelTo(gameObject);
    }
  }

  public void Start() {
    this.transform.Rotate(0, Random.Range(0, 360), 0);
  }

  public void Update() {
    this.transform.Rotate(0, RotationSpeed * Time.deltaTime, 0);
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
