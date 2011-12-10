using UnityEngine;

public class PlayerWeapons : ShipWeapons {

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

  public void Update() {
    if (Input.GetKeyUp(KeyCode.Alpha1) && weapons.Count > 0) {
      CurrentWeapon = 0;
    } else if (Input.GetKeyUp(KeyCode.Alpha2) && weapons.Count > 1) {
      CurrentWeapon = 1;
    } else if (Input.GetKeyUp(KeyCode.Alpha3) && weapons.Count > 2) {
      CurrentWeapon = 2;
    }

    if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) {
      Fire();
    }
  }

  public void Recreate() {
    weapons.Clear();

    TeamTarget enemies = GetComponent<TargetMarker>().EnemyTargets;
    // Run over factory and get relevant modules, and add to the weapons.
    foreach(ModuleFactory module in GetComponents<ModuleFactory>()){
      if(module.FactoryType == "Weapon") {
        foreach(GameObject weapon in module.Modules) {
          weapon.GetComponent<Weapon>().targeting = enemies;
          weapons.Add(weapon);
          weapon.transform.parent = transform;
        }
      }
    }
  }

}
