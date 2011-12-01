using UnityEngine;
using System.Collections;

public class Player : UtilBehaviour {

  /// The maximum amount to move per frame.
  public float Speed = 10;
  private float maxSpeed {
    get {
      return Speed * Time.deltaTime;
    }
  }

  /// The maximum amount to rotate by per frame.
  public float Rotation = 100;
  private float maxRotation {
    get {
      return Rotation * Time.deltaTime;
    }
  }

  /// The marker which shows where the player is on the map.
  public GameObject Marker;

  // The destination of the player. Stationary if the current position.
  private Vector3 destination;

  // Sets which of the standard directions the player faces.
  private Vector3 forward {
    get {
      return transform.up;
    }
  }

  public void Start() {
    // Pick a star to start at.
    GameObject start = GameObject.FindGameObjectWithTag("Star");
    this.transform.position = ThisY(start.transform.position);
    start.GetComponent<Star>().Marker.GetComponent<StarMarker>().IsDestination = true;
    
    // No initial destination.
    this.destination = transform.position;
  }

  public void Update() {
    Vector3 position = transform.position;

    // If a destination is available, move towards it.
    if (destination != position) {
      Vector3 turn = IgnoreY(destination);

      // Calculate the current and destination rotations.
      // Note that we ignore the z axis to avoid 3D rotation.
      Quaternion cRot = transform.rotation;
      Quaternion dRot = Quaternion.FromToRotation(Vector3.up, turn);

      // Calculate the desired rotation.
      Quaternion rot = Quaternion.RotateTowards(cRot, dRot, maxRotation);

      // If the rotation is complete, move towards the target.
      if (transform.rotation != rot) {
        this.transform.rotation = rot;
      } else {
        position = Vector3.MoveTowards(position, destination, maxSpeed);
        this.transform.position = position;
      }
    }
  }

  /// Sets the player's destination. If the player already has a destination,
  /// it will stop where it is and begin moving towards this new destination.
  public void TravelTo(GameObject destination) {
    this.destination = ThisY(destination.transform.position);
  }

  /// Stops the player's movement.
  public void Stop() {
    this.destination = transform.position;
  }

}
