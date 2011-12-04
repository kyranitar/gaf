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

  /// Offset position
  public Vector3 offset = new Vector3(-2.05f, 0.7f, -0.75f);

  // Weapon Cooldown

  void Start() {
    transform.Translate(offset);
  }

  void Update() {

  }

  /// Creates a new projectile object. Override for more detailed behaviour.
  public override void Fire() {

    // If cooldown is not finished then return, otherwise set the cooldown;
    if (cooldown > 0) {
      return;
    }
    cooldown = CooldownLength;

    // Retrieve these values to shorten up the following code.
    Vector3 pos = transform.position;
    Quaternion rot = transform.rotation;

    // Create the projectile and retrieve its behaviour.
    // The correct behaviour is to throw an exception if no such behaviour exists.
    GameObject projectile = Instantiate(ProjectilePrefab, pos, rot) as GameObject;
    Projectile behaviour = projectile.GetComponent<Projectile>();

    // Set the stats from this weapon.
    behaviour.Damage = DamagePerProjectile;
    behaviour.Speed = ProjectileSpeed;
    behaviour.LifeTime = ProjectileLifeTime;
    behaviour.Targeting = Targeting;
  }

}
