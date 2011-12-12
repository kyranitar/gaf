using UnityEngine;
using System.Collections;

public class PlayerCreation : MonoBehaviour {

  // Player prefab.
  public GameObject ShipPrefab;

  public GameObject StartingWeapon;
  public Vector3 WeaponPos;

  public GameObject StartingSkill;
  public Vector3 SkillPos;

  public GameObject StartingControls;
  public Vector3 ControlPos;

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
        mf.AddModuleByObject(StartingWeapon, WeaponPos);
      } else if (mf.FactoryType == "Skill") {
        mf.AddModuleByObject(StartingSkill, SkillPos);
      } else if (mf.FactoryType == "Control") {
        mf.AddModuleByObject(StartingControls, ControlPos);
      }
    }
  }
}
