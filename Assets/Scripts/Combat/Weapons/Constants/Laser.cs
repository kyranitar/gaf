using System;
using UnityEngine;

/// The behaviour for the laser weapon.
/// 
/// Authors: Timothy Jones & Cam Owen
public class Laser : ConstantWeapon {

  /// The laser material.
  public Material LaserMaterial;

  /// The width of the beam.
  public float Width = 0.2f;

  /// The maximum distance in Unity units the laser beam can reach.
  public float MaximumDistance = 50;

  /// The time taken in seconds to fade out a completed beam.
  public float FadeTime = 1;

  /// The line that will be used to draw the beam.
  private LineRenderer line;

  public void Start() {
    // Set up the line renderer.
    line = gameObject.AddComponent<LineRenderer>();
    line.SetWidth(Width, Width);
    line.SetVertexCount(2);
    line.material = LaserMaterial;
    line.material.shader = Shader.Find("Transparent/Diffuse");
    line.renderer.enabled = true;
  }

  public void Update() {
    if (!isFiring) {
      return;
    }

    Vector3 pos = transform.position;

    // Get the world coordinates of the mouse, for targeting.
    Vector3 mouse = Camera.mainCamera.ScreenToWorldPoint(Input.mousePosition);
    mouse.z = pos.z;

    // Raycast towards the target position to see if we hit anything.
    RaycastHit[] collisions = Physics.RaycastAll(pos, (mouse - pos), MaximumDistance);

    // Sort by closest collider.
    Array.Sort(collisions, delegate(RaycastHit one, RaycastHit two) {
      return one.distance < two.distance ? -1 : one.distance > two.distance ? 1 : 0;
    });

    // Line stems from origin.
    line.SetPosition(0, pos);

    bool collided = false;

    foreach (RaycastHit collision in collisions) {
      GameObject collider = collision.collider.gameObject;
      if (targeting.IsTargeting(collider)) {
        // A target has been hit. Don't let the line pass through the target.
        collided = true;
        line.SetPosition(1, collision.point);

        // Damage the target.
        CauseDamage(collider);

        break;
      }
    }

    if (!collided) {
      // Extrapolate the position of the beam.
      line.SetPosition(1, (mouse - pos).normalized * MaximumDistance + pos);
    }
  }

}
