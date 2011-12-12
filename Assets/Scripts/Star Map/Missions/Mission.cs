using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public class Mission {

  public const int Dist = 100;

  private List<Team> teams = new List<Team>();

  public ReadOnlyCollection<Team> Teams {
    get {
      return new ReadOnlyCollection<Team>(teams);
    }
  }

  private GameObject cursor;
  public Mission(GameObject c) {
    cursor = c;
  }

  public void AddTeam(GameObject prefab, int shipCount, Vector3 pos) {
    teams.Add(new Team(prefab, shipCount, pos));
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
      makeShip(team.Prefab, combatTeam, allies, enemies, new Vector3(team.Pos.x + Random.Range(-Dist, Dist), 0, team.Pos.z + Random.Range(-Dist, Dist)));
    }

    if (makePlayer) {
      // Make cursor
      MonoBehaviour.Instantiate(cursor, new Vector3(0, 0, 0), Quaternion.identity);

      // Reset player for start of combat.
      GameObject player = GameObject.FindGameObjectWithTag("ShipBlueprint");
      player.transform.position = new Vector3(team.Pos.x + + Random.Range(-Dist, Dist), 0, team.Pos.z + Random.Range(-Dist, Dist));
      player.transform.rotation = new Quaternion(0, 0, 0, 0);
      player.GetComponent<TeamMarker>().Team = combatTeam;
      player.GetComponent<TargetMarker>().AlliedTargets = allies;
      player.GetComponent<TargetMarker>().EnemyTargets = enemies;

      PlayerActivation activater = player.GetComponent<PlayerActivation>();
      activater.Recreate();
      activater.SetBehavioursEnabled(true);
      activater.Show();

    }
  }

  private void makeShip(GameObject prefab, CombatTeam team, TeamTarget allies, TeamTarget enemies, Vector3 pos) {
    Quaternion rot = new Quaternion(0, 0, 0, 0);
    GameObject ship = MonoBehaviour.Instantiate(prefab, pos, rot) as GameObject;

    ship.GetComponent<TeamMarker>().Team = team;
    ship.GetComponent<TargetMarker>().AlliedTargets = allies;
    ship.GetComponent<TargetMarker>().EnemyTargets = enemies;
  }

  /// Just a storage class for the data on a team.
  public class Team {

    public GameObject Prefab;
    public int ShipCount;
    public Vector3 Pos;

    public Team(GameObject prefab, int shipCount, Vector3 pos) {
      this.Prefab = prefab;
      this.ShipCount = shipCount;
      this.Pos = pos;
    }

  }

}
