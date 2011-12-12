using UnityEngine;
using System.Collections.Generic;

public class FriendDamage : ShipDamage {

  public static IList<FriendDamage> damages = new List<FriendDamage>();

  private bool following = false;

  public override void Start() {
    base.Start();

    damages.Add(this);
  }

  public void Update() {
    if (combatComplete != null) {
      Vector3 pos = this.transform.position;
      Vector3 cam = Camera.main.transform.position;
      Camera.main.transform.position = new Vector3(pos.x, cam.y, pos.z);
    }
  }

  protected override void OnDeath() {
    Destroy(gameObject);
  }

  public void OnDestroy() {
    damages.Remove(this);

    if (following) {
      if (!FollowFriend()) {
        combatComplete.Defeat();
      }
    }
  }

  /// Causes the camera to follow a friend.
  /// This is intended to be used for when the player dies.
  ///
  /// Returns whether a friend exists to be followed.
  public static bool FollowFriend() {
    if (damages.Count == 0) {
      return false;
    }

    damages[0].following = true;

    return true;
  }

}
