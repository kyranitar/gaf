using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public class ModuleFactory : MonoBehaviour {

  public GameObject[] Prefabs;

  public Vector3[] Positions;

  private IList<GameObject> modules;
  public ReadOnlyCollection<GameObject> Modules {
    get {
      return new ReadOnlyCollection<GameObject>(modules);
    }
  }

  public void Start() {
    modules = new List<GameObject>();
  }

  public void AddModule() {
    // TODO instatiate correct prefab.
    GameObject module = Instantiate(Prefabs[0], transform.position + Positions[modules.Count], transform.rotation) as GameObject;
    module.transform.parent = this.transform;
    modules.Add(module);
    foreach (MonoBehaviour script in module.GetComponents<MonoBehaviour>()) {
      Debug.Log("added a " + script.name + " to the player");
      script.enabled = false;
    }
  }

  public void RemoveModule() {
    GameObject module = modules[modules.Count - 1];
    modules.Remove(module);
    Destroy(module);
  }

}
