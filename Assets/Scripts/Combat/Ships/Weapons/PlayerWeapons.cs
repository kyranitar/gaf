using UnityEngine;

public class PlayerWeapons : ShipWeapons {

  public float FireRate = 1;

  private float reloadTime = 50;

  private float reloadTimeLeft;
  private bool reloaded = true;

  public void Update() {

    if (Input.GetKeyUp(KeyCode.Alpha1) && Weapons.Length > 0) {
      CurrentWeapon = 0;
    } else if (Input.GetKeyUp(KeyCode.Alpha2) && Weapons.Length > 1) {
      CurrentWeapon = 1;
    } else if (Input.GetKeyUp(KeyCode.Alpha3) && Weapons.Length > 2) {
      CurrentWeapon = 2;
    }

    if(!reloaded) {
      reloadTimeLeft -= FireRate;

      if (reloadTimeLeft <= 0) {
        reloadTimeLeft = 0;
        reloaded = true;
      }
    }

    if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0) && reloaded) {
      reloaded = false;
      reloadTimeLeft = reloadTime;
      Fire();
    }
  }

}
