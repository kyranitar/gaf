using UnityEngine;
using System.Collections;

public class AISkills : SkillSet {

  public void Update() {
    if (Skills.Length > 0) {
      Skills[0].GetComponent<Ability>().Activate(transform.position);
    }
  }
}
