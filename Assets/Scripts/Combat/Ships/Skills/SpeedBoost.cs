using UnityEngine;
using System.Collections;

public class SpeedBoost : Ability {

  public float SpeedIncrease;
  public float ActiveLength;
  public float Cooldown;

	public void Start () {
    skillName = "Speed Boost";

    speed = SpeedIncrease;
    duration = ActiveLength;
    cooldownTime = Cooldown;

    Castable = true;
	}

	public void Update () {
    updateSkill();
	}
}
