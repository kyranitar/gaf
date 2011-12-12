using UnityEngine;
using System.Collections;

public class PlayerCreation : MonoBehaviour {

  // Player prefab.
  public GameObject ShipPrefab;

  public GameObject StartingWeapon;
  public GameObject StartingSkill;
  public GameObject StartingControls;
  
  private bool needsBuild = true;
  /*
   * TODO for version 3
   *
   * add database feature.
   */

	public void Start() {
    ShipPrefab = Instantiate(ShipPrefab) as GameObject;
    DontDestroyOnLoad(ShipPrefab);
    PlayerActivation activater = ShipPrefab.GetComponent<PlayerActivation>();
    activater.Hide();
    activater.SetBehavioursEnabled(false);
    activater.SetFactoriesEnabled(true);

  }

  public void Update() {

    if(needsBuild) {
      PlayerActivation activater = ShipPrefab.GetComponent<PlayerActivation>();
      needsBuild = false;
      AddBasicBuild();
      activater.Recreate();
      activater.SetBehavioursEnabled(false);
      activater.SetFactoriesEnabled(false);
      activater.Hide();
    }
  }

  public void AddBasicBuild() {
    // TODO get this from database, or if no databae entry start with basic.
    foreach (ModuleFactory mf in ShipPrefab.GetComponents<ModuleFactory>()) {
      if (mf.FactoryType == "Weapon") {
        mf.AddModuleByObject(StartingWeapon);
      } else if (mf.FactoryType == "Skill") {
        mf.AddModuleByObject(StartingSkill);
      } else if (mf.FactoryType == "Control") {
        mf.AddModuleByObject(StartingControls);
      }
    }
  }
}
