using UnityEngine;
using System.Collections;

public class CustomizeGUI : MonoBehaviour {

  private Rect AddRemoveCoords = new Rect(25, 25, 100, 90);
  private Rect addCoords;
  private Rect removeCoords;

  private Rect toolbarCoords = new Rect(25, Screen.height - 55, 250, 30);
  private string[] toolbarStrings;

  private int toolbarIndex = 0;
  private ModuleFactory[] factories;

  public void Start() {
    addCoords = new Rect(AddRemoveCoords.x + 10, AddRemoveCoords.y + 30, AddRemoveCoords.width * 0.8f, AddRemoveCoords.height * 0.2f);
    removeCoords = new Rect(AddRemoveCoords.x + 10, AddRemoveCoords.y + 60, AddRemoveCoords.width * 0.8f, AddRemoveCoords.height * 0.2f);

    GameObject shipPrefab = GameObject.FindGameObjectWithTag("ShipBlueprint");
    factories = shipPrefab.GetComponents<ModuleFactory>();

    toolbarStrings = new string[3] { "Weapons", "Skills", "Movement" };
  }

  public void OnGUI() {
    toolbarIndex = GUI.Toolbar(toolbarCoords, toolbarIndex, toolbarStrings);

    GUI.Box(AddRemoveCoords, toolbarStrings[toolbarIndex]);

    if (GUI.Button(addCoords, "Add")) {
      addObject(toolbarIndex);
    }

    if (GUI.Button(removeCoords, "Remove")) {
      removeObject(toolbarIndex);
    }

    if (Input.GetKeyDown(KeyCode.L)) {
      Debug.Log(toolbarIndex);
    }

    if (GUI.Button(new Rect(100, 200, 50, 50), "Back")) {
      Application.LoadLevel("Star Map");
    }

    if (GUI.Button(new Rect(200, 200, 50, 50), "Turn on")) {
      GameObject shipPrefab = GameObject.FindGameObjectWithTag("ShipBlueprint");
      PlayerActivation pAct = shipPrefab.GetComponent<PlayerActivation>();
      if (!pAct.IsActive) {
        pAct.TurnOn();
      }
    }

    if (GUI.Button(new Rect(300, 200, 50, 50), "Turn off")) {
      GameObject shipPrefab = GameObject.FindGameObjectWithTag("ShipBlueprint");
      PlayerActivation pAct = shipPrefab.GetComponent<PlayerActivation>();
      if (pAct.IsActive) {
        pAct.TurnOff();
      }
    }
  }

  private void addObject(int toolbarIndex) {
    factories[toolbarIndex].AddModule();
  }

  private void removeObject(int toolbarIndex) {
    factories[toolbarIndex].RemoveModule();
  }
}
