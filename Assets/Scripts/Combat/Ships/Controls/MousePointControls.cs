using UnityEngine;
using System.Collections;

public class MousePointControls : KeyboardControls {

	public override void Update() {
    base.Update();

    Camera camera = Camera.main;
    Vector3 mouse = Input.mousePosition;
    Vector3 input = new Vector3(mouse.x, mouse.y, camera.nearClipPlane);
    Vector3 world = camera.ScreenToWorldPoint(input);
    movement.TurnTowards(world.x, this.transform.position.y, world.z);
  }

}
