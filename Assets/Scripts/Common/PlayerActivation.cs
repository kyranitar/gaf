using UnityEngine;

public class PlayerActivation : MonoBehaviour {

  public void Recreate() {
    GetComponent<PlayerSkills>().Recreate();
    GetComponent<PlayerWeapons>().Recreate();
  }

  public void Show() {
    gameObject.renderer.enabled = true;
  }

  public void Hide() {
    gameObject.renderer.enabled = false;
  }

  public void SetBehavioursEnabled(bool enabled) {
    foreach (MonoBehaviour com in GetComponents<MonoBehaviour>()) {
      if (com != this && !(com is ModuleFactory)) {
        com.enabled = enabled;
      }
    }
    GetComponent<PlayerSkills>().Enabled = enabled;
    GetComponent<PlayerWeapons>().Enabled = enabled;
  }

  public void SetFactoriesEnabled(bool enabled) {
    foreach (ModuleFactory com in GetComponents<ModuleFactory>()) {
      com.enabled = enabled;
    }
  }

}
