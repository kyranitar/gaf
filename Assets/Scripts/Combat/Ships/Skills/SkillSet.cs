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

  /// A refence to this NPC is targeting.
  public Transform Target;

  /// Holds the various skills.
  public List<GameObject> Skills = new List<GameObject>();

}
