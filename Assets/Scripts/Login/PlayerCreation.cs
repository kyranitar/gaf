using UnityEngine;
using System.Collections;

public class PlayerCreation : MonoBehaviour {

  public GameObject ShipPrefab;

  // Create the player TODO add database feature.
	public void Start() {
    if (ShipPrefab != null) {
      ShipPrefab = Instantiate(ShipPrefab) as GameObject;
      ShipPrefab.renderer.enabled = false;
      DontDestroyOnLoad(ShipPrefab);

    } else {
      Debug.LogError("No ship prefab found");
    }
  }

  public void Update() {
    PlayerActivation pAct = ShipPrefab.GetComponent<PlayerActivation>();
    if (pAct.IsActive) {
      pAct.TurnOff();
    }
  }
}
