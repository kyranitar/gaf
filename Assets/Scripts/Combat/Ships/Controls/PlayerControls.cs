using UnityEngine;
using System.Collections;

public abstract class PlayerControls : MonoBehaviour {

  protected ShipMovement movement;

  public virtual void Start() {
    this.movement = GetComponent<ShipMovement>();
  }

  protected bool isDown(KeyCode key) {
    return Input.GetKey(key);
  }

  protected bool isDown(KeyCode[] keys) {
    foreach (KeyCode key in keys) {
      if (Input.GetKey(key)) {
        return true;
      }
    }

    return false;
  }

}
