using UnityEngine;

public class PlayerDamage : ShipDamage {

  protected override void OnDeath() {
    ExitCombat();

    if (!FriendDamage.FollowFriend()) {
      combatComplete.Defeat();
    }
  }

  public void ExitCombat() {
    PlayerActivation playerActivation = this.GetComponent<PlayerActivation>();
    playerActivation.Hide();
    playerActivation.SetBehavioursEnabled(false);
  }
  
}

