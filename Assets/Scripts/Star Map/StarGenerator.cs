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
        
        //Check top bounds
        float m = (TopRight.x - TopLeft.x) / (TopRight.y - TopLeft.y);
        float b = TopLeft.x + (z - TopLeft.y) * m;
        if (x > b) {
          continue;
        }

		//Check bottom bounds
		m = (BottomRight.x - BottomLeft.x) / (BottomRight.y - BottomLeft.y);
		b = BottomLeft.x + (z - BottomLeft.y) * m;
		if (x < b) {
          continue;
        }
				
		//check left bounds
        m = (TopLeft.x - BottomLeft.x) / (TopLeft.y - BottomLeft.y);
        b = (x - BottomLeft.x) / m + BottomLeft.y;
        if (z > b) {
          continue;
        }
		
        //check right bounds
		m = (TopRight.x - BottomRight.x) / (TopRight.y - BottomRight.y);
        b = (x - BottomRight.x) / m + BottomRight.y;
        if (z < b) {
          continue;
        }
        
        //check bounds with other stars
        Vector3 testpos = new Vector3(x, 0.01f, z);
        //This boolean is just here until I can figure out how to get it to break
        //to a label, since continue just affects the inner loop.
        bool ok = true;
        foreach(GameObject star in stars){
	      if(star != null && Vector3.Distance(testpos,star.transform.position) < 50){
	        ok = false;	
	      }
	    }
        if(!ok){
          continue;
        }
        
        //If we get here, everything is ok!
	    break;
      }
      Vector3 pos = new Vector3(x, 0.01f, z);
      Quaternion rot = new Quaternion(0, 0, 0, 0);
      stars[i] = Instantiate(StarPrefab, pos, rot) as GameObject;
    }
    
    Instantiate(PlayerPrefab);
  }
  
}
