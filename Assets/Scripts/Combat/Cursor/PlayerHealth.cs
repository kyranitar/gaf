using UnityEngine;
using System.Collections;

public class PlayerHealth : RadialBar {

  public float startAngle = 0;
  public float finishAngle = 180;
  private ShipDamage damageScript;

  public new void Start() {

    damageScript = GameObject.FindGameObjectWithTag("Player").GetComponent<ShipDamage>();
    TotalValue = damageScript.TotalHealth;
    CurrentValue = TotalValue - damageScript.Damage;
    MinAngle = startAngle;
    MaxAngle = finishAngle;
    
    base.Start();

  }

  public new void Update() {
    CurrentValue = TotalValue - damageScript.Damage;
    base.Update();


  }
}
