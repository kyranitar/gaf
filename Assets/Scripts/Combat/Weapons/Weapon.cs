using UnityEngine;

/// A spaceship weapon for use in the Combat mode.
/// 
/// Author: Timothy Jones
public abstract class Weapon : MonoBehaviour {

  /// The type of ships to cause damage to.
  public TeamTarget targeting;

  /// Cause the weapon to fire.
  public abstract void Fire();

}
