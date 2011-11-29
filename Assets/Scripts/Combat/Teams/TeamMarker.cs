using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

/// A basic marker for objects to determine if it is a target.
/// 
/// Author: Timothy Jones
public class TeamMarker : MonoBehaviour {

  /// The team that this target is on.
  public CombatTeam Team;
  
  private static List<GameObject> marked = new List<GameObject>();
  
  public void Start() {
    marked.Add(gameObject);
  }
  
  public void OnDestroy() {
    marked.Remove(gameObject);
  }
  
  public static ReadOnlyCollection<GameObject> GetAllMarkedGameObjects() {
    return new ReadOnlyCollection<GameObject>(marked);
  }

}
