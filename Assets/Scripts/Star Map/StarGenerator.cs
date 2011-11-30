using UnityEngine;
using System.Collections;

public class StarGenerator : MonoBehaviour {

  public int StarCount = 10;
  public GameObject StarPrefab;
  public GameObject PlayerPrefab;
  public Vector2 TopLeft;
  public Vector2 TopRight;
  public Vector2 BottomLeft;
  public Vector2 BottomRight;

  public void Start() {
    GameObject[] stars = new GameObject[StarCount];
    
    float minX = Mathf.Min(TopLeft.x, TopRight.x, BottomLeft.x, BottomRight.x);
    float maxX = Mathf.Max(TopLeft.x, TopRight.x, BottomLeft.x, BottomRight.x);
    float minZ = Mathf.Min(TopLeft.y, TopRight.y, BottomLeft.y, BottomRight.y);
    float maxZ = Mathf.Max(TopLeft.y, TopRight.y, BottomLeft.y, BottomRight.y);
    
    
    for (int i = 0; i < StarCount; i++) {
      //Randomly generate coordinates
      float x = 0, z = 0;

      while (true) {
        x = minX + Random.value * (maxX - minX);
        z = minZ + Random.value * (maxZ - minZ);
        
        //Check bounds
        float m1 = (TopRight.x - TopLeft.x) / (TopRight.y - TopLeft.y);
        float m2 = (BottomRight.x - BottomLeft.x) / (BottomRight.y - BottomLeft.y);
        
        float x1 = TopLeft.x + (z - TopLeft.y) * m1;
        float x2 = BottomLeft.x + (z - BottomLeft.y) * m2;
        if (x > x1 || x < x2) {
          continue;
        }

        break;
        
        m1 = (TopLeft.x - BottomLeft.x) / (TopLeft.y - BottomLeft.y);
        m2 = (TopRight.x - BottomRight.x) / (TopRight.y - BottomRight.y);
        
        float z1 = (x - BottomLeft.x) / m1 + BottomLeft.y;
        float z2 = (x - BottomRight.x) / m2 + BottomRight.y;
        if (z < z1 || z > z2) {
          continue;
        }
        
        break;
      }
      
      Vector3 pos = new Vector3(x, 0.01f, z);
      Quaternion rot = new Quaternion(0, 0, 0, 0);
      stars[i] = Instantiate(StarPrefab, pos, rot) as GameObject;
    }
    
    Instantiate(PlayerPrefab);
  }
  
}
