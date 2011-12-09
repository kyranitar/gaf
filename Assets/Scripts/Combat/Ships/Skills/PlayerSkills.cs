using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* TODO
 *
 * Remove the control scheme from here and add it to the control modules handled by the ModuleFactories.
 *
 * When doing this make sure to handle the player ship being able to see it's skill set component.
 * (As well as the other scripts that look for it also, unsure of how many do this (there could be none)).
 */

public class PlayerSkills : SkillSet {

  /// The prefab for the skillbar GUI objects.
  public GameObject SkillBarPrefab;

  /// Holds a record of the activated SkillBars.
  private List<GameObject> skillBars;

  /// How many of our skills are offensive.
  private int offensiveCount = 0;

  public bool Enabled {
    get {
      return this.enabled;
    }
    set {
      foreach(GameObject skill in Skills) {
        foreach(MonoBehaviour component in skill.GetComponents<MonoBehaviour>()) {
          component.enabled = value;
        }
      }
    }
  }

  public void Update() {
    if (Input.GetKeyDown(KeyCode.Z) && Skills.Count > 0) {
      Skills[0].GetComponent<Ability>().Activate(transform.position);
    } else if (Input.GetKeyDown(KeyCode.X) && Skills.Count > 1) {
      Skills[1].GetComponent<Ability>().Activate(transform.position);
    } else if (Input.GetKeyDown(KeyCode.C) && Skills.Count > 2) {
      Skills[2].GetComponent<Ability>().Activate(transform.position);
    }
  }

  /// Recreates the skill set based on the relevant factory.
  public virtual void Recreate() {

    // Clear all the skills.
    Skills.Clear();

    // Get skills from factories and place them into the skill set. Call once after creating the cursor object at the start of combat.
    foreach (ModuleFactory module in GetComponents<ModuleFactory>()) {
      if (module.FactoryType == "Skill") {
        foreach (GameObject skill in module.Modules) {
          AddSkill(skill);
        }
      }
    }
    
    int offensiveIndex = 0;
    int defensiveIndex = 0;
    
    for (int i = 0; i < Skills.Count; i++) {

      // Makes the skill and gives passes a refence to the ship that this skill set belongs to.
      GameObject skill = Skills[i];
      Ability abilityBase = skill.GetComponent<Ability>();
      abilityBase.Ship = gameObject;

      // Adds GUI content for the palyer
      GameObject skillBar = Instantiate(SkillBarPrefab) as GameObject;
      SkillBar script = skillBar.GetComponent<SkillBar>();

      script.OffensiveSkillCount = offensiveCount;
      script.DefensiveSkillCount = Skills.Count - offensiveCount;

      if (abilityBase.isOffensive) {
        script.SkillIndex = offensiveIndex++;
      } else {
        script.SkillIndex = defensiveIndex++;
      }

      // Contorller is a reference to the ability (the skills super class), which the GUI object reads from.
      script.Controller = abilityBase;
      script.Init();
    }
  }

  /// Adds our skill, and handles the defensiveCount.
  public void AddSkill(GameObject skill) {
    
    if (skill.GetComponent<Ability>().isOffensive) {
      offensiveCount++;
    }
    skill.transform.parent = transform;
    Skills.Add(skill);
  }

}
