using UnityEngine;
using System.Collections;

/// Class to control the Space Flare Skill. Scatters flares randomly around ship.
/// Author: Daniel

public class SpaceFlare : Ability {

  public GameObject FlarePrefab;
  public int NumFlares;
  public float Cooldown;

  public void Start() {
    cooldownTime = Cooldown;
    Castable = true;
  }

  public void Update() {
    updateSkill();
  }

  protected override void instantEffects() {
    Vector3 pos = this.position;
    Quaternion rot = new Quaternion(0, 0, 0, 0);
    GameObject[] flares = new GameObject[NumFlares];
    GameObject ship = this.GetComponent<Ability>().Ship;
    TeamTarget allies = ship.GetComponent<TargetMarker>().AlliedTargets;
    TeamTarget enemies = ship.GetComponent<TargetMarker>().EnemyTargets;
    CombatTeam team = ship.GetComponent<TeamMarker>().Team;
    for (int i = 0; i < NumFlares; i++) {
      flares[i] = Instantiate(FlarePrefab, pos, rot) as GameObject;
      flares[i].transform.Rotate(0, Random.Range(0.0f, 360.0f), 0);
      flares[i].transform.Translate(Random.Range(1.0f, 20.0f), -5, 0);
      flares[i].GetComponent<TeamMarker>().Team = team;
      flares[i].GetComponent<TargetMarker>().AlliedTargets = allies;
      flares[i].GetComponent<TargetMarker>().EnemyTargets = enemies;
      Destroy(flares[i], 10);
    }
  }
  
}
