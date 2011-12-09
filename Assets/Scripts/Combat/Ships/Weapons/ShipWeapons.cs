using UnityEngine;
using System.Collections.Generic;

/// A collection of weapons for a ship.
/// Needs to be updated to deal with weapon slots.
/// 
/// Author: Timothy Jones, Richard Roberts, and Cameron Owen.
public abstract class ShipWeapons : MonoBehaviour {

  /// The weapons available to this weapon system.
  /// Note that every WeaponSystem MUST have at least one weapon.
  public List<GameObject> weapons;

  // TODO make this private to hide it from unity
  /// The index of the currently selected weapon.
  private int currentWeapon = 0;

  /// The index of the current weapon in the weapon list.
  public int CurrentWeapon {
    get {
      return currentWeapon;
    }
    set {
      if (value >= 0 && value < weapons.Count) {
        currentWeapon = value;
      }
    }
  }

  /// Cause the current weapon to fire.
  public void Fire() {
    weapons[currentWeapon].GetComponent<Weapon>().Fire();
  }

}
