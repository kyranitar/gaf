using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;

/// Makes a collection of modules for the player.
/// By modules, we mean weapons, skills, or controls - each module is a unique prefab found in unity.
/// Each factory is limited to produce only a set of weapons, skills, or controls.
public class ModuleFactory : MonoBehaviour
{

  /// A list of possible skills avialable through the customize screen.
  public GameObject[] Prefabs;

  /// Where each skill's mesh sits in realtion to coordinate (0, 0, 0) on the player mesh.
  public Vector3[] Positions;

  /// A string for quering what this factory produces.
  public string FactoryType;

  /// A list of all the currently installed modules on the players (e.g. weapons A, B, and C, or skills D, and E).
  private IList<GameObject> modules;
  public ReadOnlyCollection<GameObject> Modules {
    get { return new ReadOnlyCollection<GameObject> (modules); }
  }

  public void Start ()
  {
    modules = new List<GameObject> ();
  }

  /// Adds a given module to the player (modules are specified in the unity interface, on the 'Ship' (if not ship, it will be 'PlayerShip') prefab).
  public void AddModule (int i)
  {
    // TODO Remove this dependency on currentIndex and the if statement checking the bounds.
    if (i >= Prefabs.Length) {
      return;
    }
    GameObject module = Instantiate (Prefabs[i], transform.position + Positions[modules.Count], transform.rotation) as GameObject;
    //module.transform.parent = this.transform;
    modules.Add (module);
    
    // Turn all of this new modules (prefabs) components off, to avoid things like shooting or skills activating in the menu scenes.
    foreach (MonoBehaviour script in module.GetComponents<MonoBehaviour> ()) {
      script.enabled = false;
    }
  }

  /// Removes a module based on the currentIndex (currentIndex will eventually reflect what the player has selected in the menu system).
  public void RemoveModule (int i)
  {
    // TODO Remove this dependency on currentIndex and the if statement checking the bounds.
    if (i <= 0) {
      return;
    }
    
    GameObject module = modules[i];
    modules.Remove (module);
    Destroy (module);
  }

  public void RemoveModuleByName (string name)
  {
    for (int i = 0; i < modules.Count; i++) {
      if (modules[i].name.Contains (name)) {
        Destroy (modules[i]);
        modules.RemoveAt (i);
        return;
      }
    }
  }
  
}
