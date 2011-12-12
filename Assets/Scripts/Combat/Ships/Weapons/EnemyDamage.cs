using UnityEngine;
using System.Collections.Generic;

public class EnemyDamage : ShipDamage {

  private static HashSet<EnemyDamage> damages = new HashSet<EnemyDamage>();

  public override void Start() {
    base.Start();
    
    damages.Add(this);
  }

  protected override void OnDeath() {
    Destroy(gameObject);
  }

  public void OnDestroy() {
    damages.Remove(this);

    if (damages.Count == 0) {
      combatComplete.Victory();
    }
  }
  
}
