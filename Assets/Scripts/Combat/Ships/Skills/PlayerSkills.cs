using UnityEngine;
using System.Collections;

/* TODO
 *
 * Remove the control scheme from here and add it to the control modules handled by the ModuleFactories.
 *
 * When doing this make sure to handle the player ship being able to see it's skill set component.
 * (As well as the other scripts that look for it also, unsure of how many do this (there could be none)).
 */

public class PlayerSkills : SkillSet {

  public void Update() {
    if (Input.GetKeyDown(KeyCode.Z) && Skills.Count > 0) {
      Skills[0].GetComponent<Ability>().Activate(transform.position);
    } else if (Input.GetKeyDown(KeyCode.X) && Skills.Count > 1) {
      Skills[1].GetComponent<Ability>().Activate(transform.position);
    } else if (Input.GetKeyDown(KeyCode.C) && Skills.Count > 2) {
      Skills[2].GetComponent<Ability>().Activate(transform.position);
    }
  }
}
