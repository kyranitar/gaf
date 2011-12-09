using UnityEngine;

public class PlayerWeapons : ShipWeapons {

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

    // Run over factory and get relevant modules, and add to the weapons.
    foreach(ModuleFactory module in GetComponents<ModuleFactory>()){
      if(module.FactoryType == "Weapon") {
        foreach(GameObject weapon in module.Modules) {
          weapons.Add(weapon);
          weapon.transform.parent = transform;
        }
      }
    }
  }

}
