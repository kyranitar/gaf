using UnityEngine;
using System.Collections;

/// Maintains the damage allotted to this game object.
/// Responsible for informing the object when it has been destroyed.
/// 
/// Authors: Timothy Jones & Cam Owen
public class ShipDamage : MonoBehaviour {

  /// The amount of damage this object can take before being destroyed.
  public float TotalHealth = 1;

  /// The debris to create if this object is destroyed.
  public Transform DebrisPrefab = null;

  /// The minimum amount of debris to create if this object is destroyed.
  public int MinimumDebrisCount = 3;

  /// The maximum amount of debris to create if this object is destroyed.
  public int MaximumDebrisCount = 9;

  /// Damage count.
  private float Damage;

  /// Modify the damage amount.
  /// Negative values remove damage, but will never fall below zero.
  /// If the modification causes the object to be destroyed, this will be carried out.
  public void ModifyDamage(float amount) {
    Damage += amount;
    
    if (Damage < 0) {
      Damage = 0;
    } else if (Damage >= TotalHealth) {
      DebrisCreation debris = gameObject.GetComponent<DebrisCreation>();
      
      // It's acceptable not to have a debris creator.
      if (debris != null) {
        debris.CreateDebris();
      }
      
      Destroy(gameObject);

      OnDeath();
    }
  }

  protected virtual void OnDeath() {

  }
  
}
