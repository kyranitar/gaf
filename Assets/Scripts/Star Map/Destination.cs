using UnityEngine;
using System.Collections;

public class Destination : UtilBehaviour {

  private Vector3 destination;

  private float travelSpeed {
    get { return 100 * Time.deltaTime; }
  }

  public void Start() {
    this.destination = transform.position;
  }

  public void Update() {
    Vector3 position = transform.position;
    if (destination != position) {
      Vector3 move = Vector3.MoveTowards(position, destination, travelSpeed);
      transform.position = move;
      
      if (destination == transform.position) {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Player script = player.GetComponent<Player>();
        script.TravelTo(gameObject);
      }
    }
  }

  public void TravelTo(GameObject destination) {
    this.destination = ThisZ(destination.transform.position);
  }
  
}
