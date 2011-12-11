using UnityEngine;

public class PlayerDamage : ShipDamage {

  protected override void OnDeath() {
	
	PlayerActivation playerActivation = GetComponent<PlayerActivation>();
	playerActivation.Hide();
	playerActivation.SetBehavioursEnabled(false);
    Application.LoadLevel("Star Map");
  }

}

