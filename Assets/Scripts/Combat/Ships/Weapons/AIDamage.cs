using UnityEngine;

public class AIDamage : ShipDamage {

  protected override void OnDeath() {
    Destroy(gameObject);
  }

}
