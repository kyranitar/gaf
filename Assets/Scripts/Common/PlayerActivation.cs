using UnityEngine;
using System.Collections;

public class PlayerActivation : MonoBehaviour {

  /* Keeps a reference to all the factories we have */
  private ModuleFactory[] factories;

  private bool isActive;
  public bool IsActive {
    get {
      return isActive;
    }
  }

  public void Start() {
    factories = GetComponents<ModuleFactory>();
  }

  public void TurnOn() {
    gameObject.renderer.enabled = true;
    //gameObject.active = true;
    Debug.Log("factories: " + factories.Length);
    foreach(ModuleFactory module in factories) {
      foreach (MonoBehaviour script in module.GetComponents<MonoBehaviour>()) {
        Debug.Log("found a " + script.name + " in player");
        script.enabled = true;

      }
    }
    isActive = true;
  }

  public void TurnOff() {
    gameObject.renderer.enabled = false;
    //gameObject.active = false;
    foreach(ModuleFactory module in factories) {
      foreach (MonoBehaviour script in module.GetComponents<MonoBehaviour>()) {
        script.enabled = false;
      }
    }
    isActive = false;
  }
}
