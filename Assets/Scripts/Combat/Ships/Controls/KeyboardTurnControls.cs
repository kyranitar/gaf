using UnityEngine;
using System.Collections;

public class KeyboardTurnControls : KeyboardControls {

  public KeyCode[] TurnLeft = new KeyCode[2] { KeyCode.LeftArrow, KeyCode.A };
  public KeyCode[] TurnRight = new KeyCode[2] { KeyCode.RightArrow, KeyCode.D };

	public override void Update () {
    base.Update();

	  bool right = isDown(TurnRight);
    if (isDown(TurnLeft) && !right) {
      movement.TurnLeft();
    } else if (right) {
      movement.TurnRight();
    }
	}
}
