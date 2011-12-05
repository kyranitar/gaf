using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectCreation : MonoBehaviour {

  /// The object (turret, engine, shield, etc)
  public GameObject Prefab;

  /// Where to add each object on the ship (in sequencial order)
  /// The length of this determines the max amount
  public Vector3[] Positions;

  /// Points to the last added object.
  private GameObject lastAdded;

  /// A pool of all the turrets on the ship.
  private List<GameObject> pool;

	public void Start () {
    pool = new List<GameObject>();
	}

	public void Update () {

	}

  /// Makes a new object from the prefab.
  private GameObject getObject() {
    return Instantiate(Prefab, transform.position + Positions[pool.Count], transform.rotation) as GameObject;

  }

  /// Parents the given object to the ship.
  public void MakeChild(GameObject obj) {
    obj.transform.parent = transform;
  }

  /// Adds a new object onto the ship.
  public void AddOne() {

    // Return if max objects already used.
    if(pool.Count == Positions.Length) {
      return;
    }

    lastAdded = getObject();
    MakeChild(lastAdded);
    pool.Add(lastAdded);
  }

  /// Removes an object from the ship
  public void RemoveOne() {

    // Destroy the last object
    if (pool.Count > 0) {
      Destroy(pool[pool.Count - 1]);
      pool.Remove(lastAdded);

      // The last object is now the predecessor of the object destroyed above.
      if (pool.Count > 0) {
        lastAdded = pool[pool.Count - 1];
      }
    }
  }
}
