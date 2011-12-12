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
    mission.AddTeam(TeamPrefabs[0], CountA);
    mission.AddTeam(TeamPrefabs[1], CountB);
    
    mission.BuildMission();
  }
  
}
