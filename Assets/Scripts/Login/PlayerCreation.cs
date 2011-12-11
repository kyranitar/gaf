using UnityEngine;
using System.Collections;

public class PlayerCreation : MonoBehaviour {

  // Player prefab.
  public GameObject ShipPrefab;

  /*
   * TODO for version 3
   *
   * add database feature.
   */

	public void Start() {
    ShipPrefab = Instantiate(ShipPrefab) as GameObject;
    DontDestroyOnLoad(ShipPrefab);

    PlayerActivation activater = ShipPrefab.GetComponent<PlayerActivation>();
    activater.Hide();
    activater.SetFactoriesEnabled(false);
    activater.SetBehavioursEnabled(false);
  }
}
