using UnityEngine;

public class AIWeapons : ShipWeapons {

  /// 1/X chance the Ship will shoot.
  public int ShootChance = 400;

  public void Update() {
    if (Random.Range(0, ShootChance) < 1) {
      Fire();
    }
  }

}
