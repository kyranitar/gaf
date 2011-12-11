using UnityEngine;
using System.Collections.Generic;

public class StarMarker : UtilBehaviour {

  public float RotationSpeed = 50;

  public float PulseRate = 1;

  private static StarMarker currentDestination = null;

  public Mission Mission;
  private bool ascending = false;

  private string label;
  public string Label {
    get {
      return label;
    }
  }

  private StarMapUI interfaceRef;

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
    label = "";
    interfaceRef = Camera.main.GetComponent<StarMapUI>();
  }

  public void Update() {
    this.transform.Rotate(0, RotationSpeed * Time.deltaTime, 0);

    if (Mission != null) {
      Material mat = this.renderer.material;
      Color c = mat.color;
      if (c.b <= 0) {
        ascending = true;
      } else if (c.b >= 1) {
        ascending = false;
      }

      mat.color = new Color(c.r, c.g, c.b + PulseRate * Time.deltaTime * (ascending ? 1 : -1));

      GameObject player = GameObject.FindGameObjectWithTag("Player");
      if (ThisY(player.transform.position) == this.transform.position) {
        Debug.Log("wacked");
        interfaceRef.SelectedStarPos = transform.position;
        interfaceRef.SelectedMission = Mission;
      }
    }
  }

  public void OnMouseUpAsButton(){
    this.IsDestination = true;
    
    GameObject player = GameObject.FindGameObjectWithTag("Player");
    
    if (Mission != null && ThisY(player.transform.position) == this.transform.position) {
      // Prevent the tester from firing.
      //MissionGeneration.isActive = false;
      Application.LoadLevel("Combat");
      //Mission.BuildMission();
    } else if (this != currentDestination) {
      Player pScript = player.GetComponent<Player>();
      pScript.Stop();
    }
  }

  public void OnMouseEnter() {
    interfaceRef.HoveredMission = Mission;
    interfaceRef.ActiveStarPos = transform.position;
  }

  public void OnMouseExit() {
    interfaceRef.HoveredMission = null;
  }
}