using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/*
 * TODO
 * 
 * Dont destory the skills when player is destroyed.
 * Instead the player should not be destoryed, but deactivated.
 * However the NPC skills SHOULD be destroyed
 * (We still need it for later, e.g the customize screen).
 */

/// Handles the skills contained on a ship (player or AI).
public class SkillSet : MonoBehaviour {

  /// Holds the various skills.
  public List<GameObject> Skills = new List<GameObject>();

  /// If this skill set is owned by the player we will add the GUI (SkillBarPrefab) for each skill.
  public bool isPlayer;

  /// READ ME.
  /// All the following fields are only used if this skill set is owned by the player (if isPlayer).
  /// Good.

  /// The prefab for the skillbar GUI objects.
  public GameObject SkillBarPrefab;

  /// Holds a record of the activated SkillBars.
  private List<GameObject> skillBars;

  /// How many of our skills are offensive.
  private int offensiveCount = 0;

  /// Recreates the skill set based on the relevant factory.
  public virtual void RecreateSkills() {

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
      Skills[i] = skill;
      
      // Adds GUI content for the palyer.
      if (isPlayer) {
        
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
  }

  /// Adds our skill, and handles the defensiveCount.
  public void AddSkill(GameObject skill) {
    
    if (skill.GetComponent<Ability>().isOffensive) {
      offensiveCount++;
    }
    Skills.Add(skill);
  }

  /// Activates all of the current skills (run once before battle).
  public void ActivateSkills() {
    foreach(GameObject skill in Skills) {
      foreach(MonoBehaviour component in skill.GetComponents<MonoBehaviour>()) {
        component.enabled = true;
      }
    }
  }

  /// Deactivates all of the current skills (run once at the end battle).
  public void DeactivateSkills() {
    foreach(GameObject skill in Skills) {
      foreach(MonoBehaviour component in skill.GetComponents<MonoBehaviour>()) {
        component.enabled = false;
      }
    }
  }

  /// Makes sure that all the skills are destroyed if the player is killed.
  public void OnDestroy() {
    foreach (GameObject skill in Skills) {
      Destroy(skill);
    }
  }
}
