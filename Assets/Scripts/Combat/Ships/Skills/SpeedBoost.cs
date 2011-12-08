using UnityEngine;
using System.Collections;

public class SpeedBoost : Ability {

  public float SpeedIncrease;
  public float ActiveLength;
  public float Cooldown;

  public void Start() {
    speed = SpeedIncrease;
    activeTime = ActiveLength;
    cooldownTime = Cooldown;
    
    Castable = true;
  }

  public void Update() {
    updateSkill();
  }

  protected override void addEffects() {
    movementHandle.acceleration += speed;
    movementHandle.maxSpeed += speed;
  }

  protected override void removeEffects() {
    movementHandle.acceleration -= speed;
    movementHandle.maxSpeed -= speed;
  }
  
}
