using UnityEngine;

/// A weapon which fires constantly until turned off.
/// 
/// Author: Timothy Jones
public abstract class ConstantWeapon : Weapon {

  /// Whether or not the weapon is firing.
  protected bool isFiring;

  /// The amount of damage to cause to a target per second of contact.
  /// If negative, this weapon will heal its target.
  public float DamagePerSecond;

  public override void Fire() {
    isFiring = true;
  }

  /// Cause the weapon to stop firing.
  public void CeaseFire() {
    isFiring = false;
  }

  /// Helper for causing damage to a given object.
  public void CauseDamage(GameObject to) {
    // Correct behaviour is to throw an exception if no damage monitor exists.
    to.GetComponent<ShipDamage>().ModifyDamage(DamagePerSecond * Time.deltaTime);
  }

}
