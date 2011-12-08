using UnityEngine;
using System.Collections;

public class SpeedBoost : Ability {

  public float SpeedIncrease;
  public float ActiveLength;
  public float Cooldown;

  public void Start() {
    speed = SpeedIncrease;
    duration = ActiveLength;
    cooldownTime = Cooldown;
    
    Castable = true;
  }

  public void Update() {
    updateSkill();
  }

  protected override void addEffects() {
    movementHandle.Acceleration += speed;
    movementHandle.MaxSpeed += speed;
  }

  protected override void removeEffects() {
    movementHandle.Acceleration -= speed;
    movementHandle.MaxSpeed -= speed;
  }
  
}
