using UnityEngine;

public class PlayerDamage : ShipDamage {

  protected override void OnDeath() {
    Application.LoadLevel("Star Map");
  }

}

