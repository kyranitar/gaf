using UnityEngine;
using System.Collections;

public abstract class KeyboardControls : PlayerControls {

  public KeyCode[] Move = new KeyCode[2] { KeyCode.UpArrow, KeyCode.W };

  public virtual void Update() {
    if (isDown(Move)) {
      movement.Accelerate();
    } else {
      movement.Decelerate();
    }
  }

}
