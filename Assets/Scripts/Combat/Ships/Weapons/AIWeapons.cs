﻿using UnityEngine;

public class AIWeapons : ShipWeapons {

  public float shootChance;
  public float missilesDistance;
  public float missilesChanceDecrease;
  public float shootAngle = 20;

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

  public void Update() {
    // TODO - dynamically select weapon types.
    if (Target == null) {
      return;
    }

    // HACKED
    float dist = Vector3.Distance(Target.transform.position, transform.position);
    if (CurrentWeapon != 0 && dist < missilesDistance && weapons.Count > 0) {
      CurrentWeapon = 0; // turret
      shootChance += missilesChanceDecrease;
    } else if (CurrentWeapon != 1 && dist > missilesDistance && weapons.Count > 1) {
      CurrentWeapon = 1; // missiles
      shootChance -= missilesChanceDecrease;
    }

    if (Target != null && shouldFire()) {

      Vector3 targetDir = Target.position - transform.position;
      float angleBetween = Vector3.Angle(transform.forward, targetDir);
      float offset = 0;
      if (angleBetween > 0 - shootAngle * 0.5 + offset && angleBetween < shootAngle * 0.5 + offset) {
        Fire();
      }
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
