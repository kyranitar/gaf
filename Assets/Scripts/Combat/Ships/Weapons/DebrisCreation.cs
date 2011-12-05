using UnityEngine;
using System.Collections;

/// The behaviour responsible for creating an object's debris.
/// 
/// Author: Timothy Jones
public class DebrisCreation : MonoBehaviour {

  /// The debris objects to create for this object.
  public Transform Prefab;

  /// The minimum number of debris objects to create.
  public int MinimumDebrisCount = 3;

  /// The maximum number of debris objects to create.
  public int MaximumDebrisCount = 9;

  public int DebrisLifeTime = 10;

  public void CreateDebris() {
    if (Prefab == null) {
      return;
    }
    
    Vector3 pos = transform.position;
    Quaternion rot = transform.rotation;
    // Vector3 scale = transform.lossyScale;
    // float size = (scale.x + scale.y + scale.z) / 3f;
    
    for (int i = 0; i < Random.Range(MinimumDebrisCount, MaximumDebrisCount); i++) {
      Transform debris = Instantiate(Prefab, pos, rot) as Transform;

      // float debrisScale = Random.Range(5, 30) * size / 1000f;
      float debrisScale = Random.Range(0.55f, 1.2f);
      debris.localScale = new Vector3(debrisScale, debrisScale, debrisScale);
      debris.Translate(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10));
      Destroy(debris.gameObject, DebrisLifeTime);
    }
  }
  
}
