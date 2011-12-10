using UnityEngine;
using System.Collections;

public class AIControls : PlayerControls {

  public override void Start() {
    base.Start();

    GameObject ship = this.movement.gameObject;
    AIMovement move = ship.GetComponent<AIMovement>();
    if (move == null) {
      ship.AddComponent<AIMovement>();
    }
  }

}
