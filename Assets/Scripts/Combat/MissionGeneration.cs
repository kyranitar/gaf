using UnityEngine;
using System.Collections;

/// Only for testing purposes.
public class MissionGeneration : MonoBehaviour {

  public int TeamShipCount;

  public GameObject[] TeamPrefabs;

  public GameObject CursorPrefab;

  public void Start() {

    Mission mission = new Mission(CursorPrefab);

    foreach (GameObject prefab in TeamPrefabs) {
      mission.AddTeam(prefab, TeamShipCount);
    }

    mission.BuildMission();
  }
  
}
