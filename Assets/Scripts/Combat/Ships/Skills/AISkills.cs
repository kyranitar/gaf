using UnityEngine;
using System.Collections;

/* TODO for version 3
 *
 * make an AI that can handle any skill set, not just speedboost and rapid fire.
 * this ai should be able to seem aggresive, passive, conservative etc.
 */

/// AI Prototype for NPC's using skills.
public class AISkills : SkillSet {

  /// A refence to this NPC is targeting.
  public Transform Target;

  /// How likely this AI is to use the ability (lower values make the AI appear to be more timid,
  /// while higher values appear aggresive).
  public float Likelyness;

  /// How far the AI has to be away from target to use the speedboost, if close this AI build will opt for rapidfire.
  public float ChaseDist;

  /// Where the skills sit on the skillset (set in unity -- temporary set up only).
  private int indexRF;
  private int indexSB;

  public void Start() {

    for (int i = 0; i < Skills.Count; i++) {
      GameObject skill = Instantiate(Skills[i], transform.position, transform.rotation) as GameObject;
      Ability abilityBase = skill.GetComponent<Ability>();
      abilityBase.Ship = gameObject;
      Skills[i] = skill;
    }

    indexRF = -1;
    indexSB = -1;

    for (int i = 0; i < Skills.Count; i++) {

      string skillName = Skills[i].GetComponent<Ability>().SkillName;
      if (skillName == "Rapid Fire") {
        indexRF = i;
      } else if (skillName == "Speed Boost") {
        indexSB = i;
      }
    }
  }

  public void Update() {

    if(Target != null) {
      float dist = Vector3.Distance(Target.transform.position, transform.position);
      if (dist > ChaseDist && randomChance()) {
        activateSB();
      } else if (dist < ChaseDist && randomChance()) {
        activateRF();
      }
    }
  }

  /// Activate speed boost - hardcoded for version two.
  private void activateSB() {
    if (indexSB != -1) {
      Skills[indexSB].GetComponent<Ability>().Activate(transform.position);
    }
  }

  /// Activate rapid fire - hardcoded for version two.
  private void activateRF() {
    if (indexRF != -1) {
      Skills[indexRF].GetComponent<Ability>().Activate(transform.position);
    }
  }

  /// Returns a bool that takes into account this AI's likelyness field (a biased dice roll).
  private bool randomChance() {
    if (Random.Range(0, 1) < Likelyness) {
      return true;
    } else {
      return false;
    }
  }

  /// Makes sure that all the skills are destroyed if the AI is killed.
  public void OnDestroy() {
    foreach (GameObject skill in Skills) {
      Destroy(skill);
    }
  }
}
