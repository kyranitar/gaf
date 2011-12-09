using UnityEngine;
using System.Collections;

/// Randomly generates stars for the star map.
/// 
/// Authors: Timothy Jones and Daniel Atkins

public class StarGeneration : MonoBehaviour {

  public int StarCount = 10;
  public GameObject StarPrefab;
  public GameObject PlayerPrefab;
  public GameObject CursorPrefab;
  public Vector2 TopLeft;
  public Vector2 TopRight;
  public Vector2 BottomLeft;
  public Vector2 BottomRight;

  public GameObject CombatPlayerPrefab;
  public GameObject[] CombatTeamPrefabs;

  private static Vector2[] positions = null;

  public void Start() {
    if (positions == null) {
      positions = new Vector2[StarCount];
      
      float minX = Mathf.Min(TopLeft.x, TopRight.x, BottomLeft.x, BottomRight.x);
      float maxX = Mathf.Max(TopLeft.x, TopRight.x, BottomLeft.x, BottomRight.x);
      float minZ = Mathf.Min(TopLeft.y, TopRight.y, BottomLeft.y, BottomRight.y);
      float maxZ = Mathf.Max(TopLeft.y, TopRight.y, BottomLeft.y, BottomRight.y);
      
      
      for (int i = 0; i < StarCount; i++) {
        // Randomly generate coordinates.
        float x = 0, y = 0;
  
        CheckBounds:
        
        x = minX + Random.value * (maxX - minX);
        y = minZ + Random.value * (maxZ - minZ);
        
        // Check top bounds.
        float m = (TopRight.x - TopLeft.x) / (TopRight.y - TopLeft.y);
        float b = TopLeft.x + (y - TopLeft.y) * m;
        if (x > b) {
          goto CheckBounds;
        }
        
        // Check bottom bounds.
        m = (BottomRight.x - BottomLeft.x) / (BottomRight.y - BottomLeft.y);
        b = BottomLeft.x + (y - BottomLeft.y) * m;
        if (x < b) {
          goto CheckBounds;
        }
        
        // Check left bounds.
        m = (TopLeft.x - BottomLeft.x) / (TopLeft.y - BottomLeft.y);
        b = (x - BottomLeft.x) / m + BottomLeft.y;
        if (y > b) {
          goto CheckBounds;
        }
        
        // Check right bounds.
        m = (TopRight.x - BottomRight.x) / (TopRight.y - BottomRight.y);
        b = (x - BottomRight.x) / m + BottomRight.y;
        if (y < b) {
          goto CheckBounds;
        }
        
        // Check bounds with other stars.
        Vector2 test = new Vector2(x, y);
        for (int j = 0; j < i; j++) {
          Vector2 position = positions[j];
          if (Vector2.Distance(test, position) < 50) {
            goto CheckBounds;
          }
        }

        positions[i] = new Vector2(x, y);
      }
    }

    int which = Random.Range(1, positions.Length - 1);
    for (int i = 0; i < positions.Length; i++) {
      Vector3 pos = new Vector3(positions[i].x, 0.01f, positions[i].y);
      Quaternion rot = new Quaternion(0, 0, 0, 0);
      GameObject star = Instantiate(StarPrefab, pos, rot) as GameObject;

      Star script = star.GetComponent<Star>();
      GameObject marker = Instantiate(script.Marker, pos, rot) as GameObject;
      marker.transform.parent = star.transform;

      if (i == which) {
        Mission mission = new Mission(CursorPrefab);
        foreach (GameObject prefab in CombatTeamPrefabs) {
          mission.AddTeam(prefab, Random.Range(15, 20));
        }

        marker.GetComponent<StarMarker>().Mission = mission;
      }

      script.Marker = marker;
    }
    
    Instantiate(PlayerPrefab);
  }
  
}
