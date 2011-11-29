using UnityEngine;
using System.Collections;

public class MissionGeneration : MonoBehaviour {
 
  public GameObject[] TeamPrefabs;
  public GameObject PlayerPrefab;
  public int TeamShipCount;
  
  public GameObject enemyBase;
  public GameObject enemyObjective;
  public GameObject friendBase;
  public GameObject friendObjective;
  
  void Start() {
    GenerateMission(); 
   }
  
	// Use this for initialization
	public void GenerateMission() {
    
    Instantiate(enemyBase);
    Instantiate(enemyObjective);
    Instantiate(friendBase);
    Instantiate(friendObjective);
    
    int dist = 200;
    enemyBase.transform.position = new Vector3(Random.Range(-dist, dist), 0, Random.Range(-dist, dist));
    enemyObjective.transform.position = new Vector3(Random.Range(-dist, dist), 0, Random.Range(-dist, dist));
    friendBase.transform.position = new Vector3(Random.Range(-dist, dist), 0, Random.Range(-dist, dist));
    friendObjective.transform.position = new Vector3(Random.Range(-dist, dist), 0, Random.Range(-dist, dist));
    
    GameObject[][] teams = new GameObject[TeamPrefabs.Length][];
    
    bool player = false;
    int i = 0;
    foreach (GameObject prefab in TeamPrefabs) {
      teams[i++] = makeTeam(prefab, TeamShipCount, !player ? player = true : false);
    }
	}
  
  private GameObject[] makeTeam(GameObject prefab, int amount, bool hasPlayer) {
    GameObject[] result = new GameObject[amount + (hasPlayer ? 1 : 0)];
    
    GameObject tempTeam = new GameObject("tempTeam");
    tempTeam.AddComponent<CombatTeam>();
    CombatTeam team = tempTeam.GetComponent<CombatTeam>();
    
    TeamTarget allyTarget = TeamTarget.TargetJust(team);
    TeamTarget enemyTarget = TeamTarget.TargetAllExcept(team);
    
    for (int i = 0; i < amount; i++) {
      if (hasPlayer) {
        result[i] = makeShip(prefab, team, allyTarget, enemyTarget, friendBase, friendObjective);
      } else {
        result[i] = makeShip(prefab, team, allyTarget, enemyTarget, enemyBase, enemyObjective);
      }
    }
    
    if (hasPlayer) {
      result[amount] = makeShip(PlayerPrefab, team, allyTarget, enemyTarget, friendBase, friendObjective);
    }
    
    return result;
  }
  
  private GameObject makeShip(GameObject prefab, CombatTeam team, TeamTarget allyTarget, TeamTarget enemyTarget, GameObject homeBase, GameObject objective) {
    GameObject ship = Instantiate(prefab) as GameObject;
    
    ship.GetComponent<TeamMarker>().Team = team;
    ship.GetComponent<AllyMarker>().Targeting = allyTarget;
    ship.GetComponent<EnemyMarker>().Targeting = enemyTarget;
    ship.GetComponent<Locations>().baseLocation = homeBase.transform;
    ship.GetComponent<Locations>().objectiveLocation = objective.transform;
    
    ShipWeapons shipWeapons = ship.GetComponent<ShipWeapons>();
    if (shipWeapons != null) {
      GameObject[] weapons = shipWeapons.Weapons;
      for (int i = 0; i < weapons.Length; i++) {
        GameObject weapon = Instantiate(weapons[i], ship.transform.position, ship.transform.rotation) as GameObject;
        weapon.transform.parent = ship.transform;
        weapon.GetComponent<Weapon>().Targeting = enemyTarget;
        weapons[i] = weapon;
      }
    }
    return ship;
  }
  
}
