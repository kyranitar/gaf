using UnityEngine;
using System.Collections.Generic;

public class Mission {

  public const int Dist = 200;

  private List<Team> teams = new List<Team>();

  private GameObject playerPrefab;

  public Mission(GameObject playerPrefab) {
    this.playerPrefab = playerPrefab;
  }

  public void AddTeam(GameObject prefab, int shipCount) {
    teams.Add(new Team(prefab, shipCount));
  }

  public void BuildMission() {
    bool makePlayer = true;
    foreach (Team team in teams) {
      makeTeam(team, makePlayer);
      makePlayer = false;
    }
  }

  private void makeTeam(Team team, bool makePlayer) {
    CombatTeam combatTeam = new CombatTeam();

    TeamTarget allies = TeamTarget.TargetJust(combatTeam);
    TeamTarget enemies = TeamTarget.TargetAllExcept(combatTeam);

    for (int i = 0; i < team.ShipCount; i++) {
      makeShip(team.Prefab, combatTeam, allies, enemies);
    }

    if (makePlayer) {
      makeShip(playerPrefab, combatTeam, allies, enemies);
    }
  }

  private void makeShip(GameObject prefab, CombatTeam team, TeamTarget allies, TeamTarget enemies) {
    Vector3 pos = new Vector3(Random.Range(-Dist, Dist), 0, Random.Range(-Dist, Dist));
    Quaternion rot = new Quaternion(0, 0, 0, 0);
    GameObject ship = MonoBehaviour.Instantiate(prefab, pos, rot) as GameObject;

    ship.GetComponent<TeamMarker>().Team = team;
    ship.GetComponent<TargetMarker>().AlliedTargets = allies;
    ship.GetComponent<TargetMarker>().EnemyTargets = enemies;
  }

  /// Just a storage class for the data on a team.
  private class Team {

    public GameObject Prefab;
    public int ShipCount;

    public Team(GameObject prefab, int shipCount) {
      this.Prefab = prefab;
      this.ShipCount = shipCount;
    }

  }

}
