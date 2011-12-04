using UnityEngine;
using System.Collections;

public class RapidFire : Ability {

  public bool isPlayer;
  public float FireRateMultiplier;
  public float ActiveLength;
  public float Cooldown;

  private GameObject shipWeaponHandle = null;

  public void Start() {
    duration = ActiveLength;
    cooldownTime = Cooldown;
    Castable = true;
  }

  public void Update() {
    if (shipWeaponHandle == null) {
      ShipWeapons weaponSys = Ship.GetComponent<ShipWeapons>();
      shipWeaponHandle = weaponSys.Weapons[weaponSys.CurrentWeapon];
    }
    updateSkill();
  }

  protected override void addEffects() {
    shipWeaponHandle.GetComponent<ProjectileWeapon>().CooldownLength /= FireRateMultiplier;
  }

  protected override void removeEffects() {
    shipWeaponHandle.GetComponent<ProjectileWeapon>().CooldownLength *= FireRateMultiplier;
    shipWeaponHandle = null;
  }
  
}
