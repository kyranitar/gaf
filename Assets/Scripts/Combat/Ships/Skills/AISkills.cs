using UnityEngine;
using System.Collections;

public class AISkills : SkillSet {

  public Transform Target;

  public float Likelyness;
  public float ChaseDist;

  private int indexRF;
  private int indexSB;

  new public void Start() {
    base.Start();

    indexRF = -1;
    indexSB = -1;

    for (int i = 0; i < Skills.Length; i++) {

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

  private void activateSB() {
    if (indexSB != -1) {
      Skills[indexSB].GetComponent<Ability>().Activate(transform.position);
    }
  }

  private void activateRF() {
    if (indexRF != -1) {
      Skills[indexRF].GetComponent<Ability>().Activate(transform.position);
    }
  }

  private bool randomChance() {
    if (Random.Range(0, 1) < Likelyness) {
      return true;
    } else {
      return false;
    }
  }
}
