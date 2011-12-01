using UnityEngine;

/// A weapon which fires single projectiles at a time.
///
/// Author: Timothy Jones
public class ProjectileWeapon : Weapon {

  public GameObject ProjectilePrefab;

  /// The amount of damage to cause to a target if a projectile comes into contact.
  public float DamagePerProjectile;

  /// The speed at which the projectiles should move at.
  public float ProjectileSpeed;

  /// The life time of a projectile in seconds.
  public float ProjectileLifeTime;

  public override void Fire() {
    // Creates a new projectile object. Override for more detailed behaviour.

    // Retrieve these values to shorten up the following code.
    Vector3 pos = transform.position;
    Quaternion rot = transform.rotation;

    // Create the projectile and retrieve its behaviour.
    // The correct behaviour is to throw an exception if no such behaviour exists.
    GameObject projectile = Instantiate(ProjectilePrefab, pos, rot) as GameObject;
    if (!projectile) Debug.Log("failed");
    
    Projectile behaviour = projectile.GetComponent<Projectile>();

    // Set the stats from this weapon.
    behaviour.Damage = DamagePerProjectile;
    behaviour.Speed = ProjectileSpeed;
    behaviour.LifeTime = ProjectileLifeTime;
    behaviour.Targeting = Targeting;
  }

}
