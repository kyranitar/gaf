using UnityEngine;
using System.Collections.Generic;

public class EnemyDamage : ShipDamage {

  private static HashSet<EnemyDamage> damages = new HashSet<EnemyDamage>();

  public void Start() {
    damages.Add(this);
  }

  protected override void OnDeath() {
    damages.Remove(this);
    if (damages.Count == 0) {
	  PlayerActivation playerActivation = GameObject.FindGameObjectWithTag("ShipBlueprint").GetComponent<PlayerActivation>();
	  playerActivation.Hide();
	  playerActivation.SetBehavioursEnabled(false);
      ExperienceManager.AddExperience(20);
      Application.LoadLevel("Star Map");
    }
    
    Destroy(gameObject);
  }
}
