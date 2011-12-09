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

  public bool Enabled {
    get {
      return this.enabled;
    }
    set {
      this.enabled = value;
      foreach(GameObject weapon in weapons) {
        foreach(MonoBehaviour component in weapon.GetComponents<MonoBehaviour>()) {
          component.enabled = value;
        }
      }
    }
  }

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

  public void Start() {
    // Replace the prefabs in Weapons with real instances.
    int length = weapons.Count;
    TeamTarget enemies = GetComponent<TargetMarker>().EnemyTargets;
    for (int i = 0; i < length; i++) {
      GameObject weapon = Instantiate(weapons[i], transform.position, transform.rotation) as GameObject;
      weapon.transform.parent = this.transform;
      weapon.GetComponent<Weapon>().targeting = enemies;
      weapons[i] = weapon;
    }
  }

  /// Cause the current weapon to fire.
  public void Fire() {
    weapons[currentWeapon].GetComponent<Weapon>().Fire();
  }

}
