using UnityEngine;
using System.Collections;

public abstract class Ability : MonoBehaviour {

  /// Name and GUI Image for skill.
  protected string skillName;
  public Texture Image;

  /// Handles for ship damage, movement, and weapons
  protected ShipDamage damageHandle;
  protected ShipMovement movementHandle;
  protected ShipWeapons weaponHandle;

  protected Vector3 position;
  
  /// The effect this skill has on speed.
  protected float speed = 0;

  /// The damage alteration this skill has (like a shield, but no immunity).
  protected float deflection = 0;

  /// Toggle for immunity.
  protected bool immunity = false;

  /// How long the skill's effects last for (optional).
  protected float duration = 0;

  /// How long the cooldown lasts.
  protected float cooldownTime;

  /// Active timers and handle
  public bool Castable = false;
  private float activeTimeLeft;

  /// Cooldown timers and handle
  private float cooldownTimeLeft;
  private bool skillActive;

  private GameObject ship;
  public GameObject Ship {
    get {
      return ship;
    }
    set {
      damageHandle = value.GetComponent<ShipDamage>();
      movementHandle = value.GetComponent<ShipMovement>();
      weaponHandle = value.GetComponent<ShipWeapons>();
      ship = value;
    }
  }

  protected void updateSkill() {
    if (skillActive) {
      // If skill is still running then run duration effects.
      durationEffects();
      durationTimer();
    } else if (!skillActive && !Castable) {
      // If skill has finished run the cooldown.
      cooldown();
    }
  }

  protected void skillActivate(float time) {
    skillActive = true;
    activeTimeLeft = time;
  }

  protected void skillDeactivate() {
    removeEffects();
    skillActive = false;
  }

  public void Activate(Vector3 position) {
    if (Castable) {
      this.position = position;
      instantEffects();
      addEffects();
      Castable = false;
      skillActivate(duration);
      cooldownTimeLeft = cooldownTime;
    }
  }

  protected void durationTimer() {
    activeTimeLeft--;
    if (activeTimeLeft <= 0) {
      skillDeactivate();
    }
  }

  protected void cooldown() {
    cooldownTimeLeft--;
    if (cooldownTimeLeft <= 0) {
      Castable = true;
    }
  }

  protected virtual void instantEffects() {}

  protected virtual void addEffects() {}

  protected virtual void removeEffects() {}

  protected virtual void durationEffects() {}

  // Add things like weapon drops, and flares here.
  protected void SpawnObject(GameObject prefab, Transform parent) {
    Instantiate(prefab, parent.position, parent.rotation);
    // TODO add combat team to game object, so it knows what to attack / hurt
  }
  
}
