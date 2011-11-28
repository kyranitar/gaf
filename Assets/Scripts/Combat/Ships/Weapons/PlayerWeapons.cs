using UnityEngine;

public class PlayerWeapons : WeaponSystem {

  public void Update() {
    if (Input.GetKeyUp(KeyCode.Alpha1)) {
      CurrentWeapon = 0;
    } else if (Input.GetKeyUp(KeyCode.Alpha2)) {
      CurrentWeapon = 1;
    } else if (Input.GetKeyUp(KeyCode.Alpha3)) {
      CurrentWeapon = 2;
    }

    if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) {
      Fire();
    }
  }

}
