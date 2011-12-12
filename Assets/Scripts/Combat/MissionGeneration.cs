using UnityEngine;
using System.Collections;

/// Only for testing purposes.
public class MissionGeneration : MonoBehaviour {

  public int TeamShipCount;

  public GameObject[] TeamPrefabs;

  public GameObject CursorPrefab;

  public static int CountA = 5;
  public static int CountB = 1;

  public void Start() {
    
    Mission mission = new Mission(CursorPrefab);
    mission.AddTeam(TeamPrefabs[0], CountA, new Vector3(100, 0, 100));
    mission.AddTeam(TeamPrefabs[1], CountB, new Vector3(-100, 0, -100));
    
    mission.BuildMission();
  }
  
}
