using UnityEngine;

public class AIWeapons : ShipWeapons {

  public Transform Target;
  public float shootChance;
  public float missilesDistance;
  public float missilesChanceDecrease;

  public void Update() {

    // TODO - dynamically select weapon types.

    if (Target == null) {
      return;
    }
    
    // HACKED
    float dist = Vector3.Distance(Target.transform.position, transform.position);
    if (CurrentWeapon != 0 && dist < missilesDistance && Weapons.Length > 0) {
      CurrentWeapon = 0; // turret
      shootChance += missilesChanceDecrease;
    } else if (CurrentWeapon != 1 && dist > missilesDistance && Weapons.Length > 1) {
      CurrentWeapon = 1; // missiles
      shootChance -= missilesChanceDecrease;
    }

    if (Target != null && shouldFire()) {
      Fire();
    }

  }

  private bool shouldFire() {
    // TODO - flesh this out a bit more.
    if (Random.Range(0, 1) < shootChance) {
      return true;
    } else {
      return false;
    }
  }

}
