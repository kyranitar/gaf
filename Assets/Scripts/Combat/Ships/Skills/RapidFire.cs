using UnityEngine;
using System.Collections;

public class RapidFire : Ability {

  public bool isPlayer;
  public float FireRateIncrease;
  public float ActiveLength;
  public float Cooldown;

  private PlayerWeapons playerWeaponHandle;
  private AIWeapons aiWeaponHandle;

  public void Start() {

    skillName = "Rapid Fire";
    duration = ActiveLength;
    cooldownTime = Cooldown;
    Castable = true;
  }

  public void Update() {

    if(isPlayer && playerWeaponHandle == null) {
      GameObject player = GameObject.FindGameObjectWithTag("Player");
      playerWeaponHandle = player.GetComponent<PlayerWeapons>();
    }
    updateSkill();
  }

  protected override void addEffects() {
    if (isPlayer) {
      playerWeaponHandle.FireRate += FireRateIncrease;
    }
  }

  protected override void removeEffects() {
    if (isPlayer) {
      playerWeaponHandle.FireRate -= FireRateIncrease;
    }
  }
  
}
