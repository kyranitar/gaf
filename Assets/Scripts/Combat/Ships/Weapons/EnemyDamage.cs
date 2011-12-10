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
      ExperienceManager.AddExperience(20);
      Application.LoadLevel("Star Map");
    }
  }

}
