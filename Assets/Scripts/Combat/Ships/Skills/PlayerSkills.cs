using UnityEngine;
using System.Collections;

public class PlayerSkills : SkillSet {

  public void Update() {
    if (Input.GetKeyDown(KeyCode.Z) && Skills.Length > 0) {
      Skills[0].GetComponent<Ability>().Activate(transform.position);
    } else if (Input.GetKeyDown(KeyCode.X) && Skills.Length > 1) {
      Skills[1].GetComponent<Ability>().Activate(transform.position);
    } else if (Input.GetKeyDown(KeyCode.C) && Skills.Length > 2) {
      Skills[2].GetComponent<Ability>().Activate(transform.position);
    }
  }
}
