using UnityEngine;

/// A collection of weapons for a ship.
/// Needs to be updated to deal with weapon slots.
/// 
/// Author: Timothy Jones
public abstract class ShipWeapons : MonoBehaviour {

  /// The weapons available to this weapon system.
  /// Note that every WeaponSystem MUST have at least one weapon.
  public GameObject[] Weapons;

  private int currentWeapon = 0;
  /// The index of the current weapon in the weapon list.
  public int CurrentWeapon {
    get {
      return currentWeapon;
    }
    set {
      if (value >= 0 && value < Weapons.Length) {
        currentWeapon = value;
      }
    }
  }

  /// Cause the current weapon to fire.
  public void Fire() {
    Weapons[currentWeapon].GetComponent<Weapon>().Fire();
  }

}
