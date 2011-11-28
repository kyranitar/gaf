using UnityEngine;

/// A projectile fired from a projectile weapon.
/// 
/// Authors: Timothy Jones & Cam Owen
public class Projectile : MonoBehaviour {

  /// The amount of damage to cause to a target if this projectile comes into contact.
  public float Damage;

  /// The speed at which the projectile will move at.
  public float Speed;

  /// The life time of the projectile in seconds.
  public float LifeTime;

  /// The teams this projectile is targeting.
  public TeamTarget Targeting;

  public void Start() {
    // After its lifetime is passed, destroy this projectile.
    Destroy(gameObject, LifeTime);
  }

  public virtual void Update() {
    if (!checkCollision()) {
      transform.position += transform.forward * Speed * Time.deltaTime;
    }
  }

  /// Checks if the projectile is about to hit a target.
  /// If so, it causes damage to the target and is destroyed.
  private bool checkCollision() {
    Vector3 pos = transform.position;

    // Raycast one frame ahead of the missile to check if it's about to collide.
    RaycastHit ray;
    bool hit = Physics.Raycast(pos, transform.forward, out ray, Speed * Time.deltaTime);

    if (!hit) {
      return false;
    }

    GameObject collider = ray.collider.gameObject;
    if (Targeting.IsTargeting(collider)) {
      // This projectile has hit a target. Cause damage.
      collider.GetComponent<ShipDamage>().ModifyDamage(Damage);

      // This projectile is gone.
      Destroy(gameObject);

      return true;
    }

    return false;
  }

}
