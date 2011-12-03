using UnityEngine;
using System.Collections;

/// Only for testing purposes.
public class MissionGeneration : MonoBehaviour {

  public int TeamShipCount;

  public GameObject[] TeamPrefabs;

  public GameObject PlayerPrefab;

  public static bool isActive = true;

  public void Start() {
    if (!isActive) {
      return;
    }

    Mission mission = new Mission(PlayerPrefab);
    foreach (GameObject prefab in TeamPrefabs) {
      mission.AddTeam(prefab, TeamShipCount);
    }
    
    mission.BuildMission();
  }
  
}
