using UnityEngine;
using System.Collections;

/// Handles the interface for the customize screen.
/// In version three this will tell the Module Factories (ModuleFactory.cs) on the 'Ship' (if not 'Ship' it will be 'PlayerShip')
/// what modules to create during the AddModule() method.
public class CustomizeGUI : MonoBehaviour {

  /// Mock idea for layout.
  private Rect AddRemoveCoords = new Rect(25, 25, 100, 90);
  private Rect addCoords;
  private Rect removeCoords;

  private Rect toolbarCoords = new Rect(25, Screen.height - 55, 250, 30);
  private string[] toolbarStrings;

  private int toolbarIndex = 0;
  private ModuleFactory[] factories;

  public void Start() {
    // TODO update this stuff to version 3.0
    addCoords = new Rect(AddRemoveCoords.x + 10, AddRemoveCoords.y + 30, AddRemoveCoords.width * 0.8f, AddRemoveCoords.height * 0.2f);
    removeCoords = new Rect(AddRemoveCoords.x + 10, AddRemoveCoords.y + 60, AddRemoveCoords.width * 0.8f, AddRemoveCoords.height * 0.2f);
    GameObject shipPrefab = GameObject.FindGameObjectWithTag("ShipBlueprint");
    factories = shipPrefab.GetComponents<ModuleFactory>();
    toolbarStrings = new string[3] { "Weapons", "Skills", "Movement" };
  }

  public void OnGUI() {

    // Update the toolbar.
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

    if (GUI.Button(new Rect(200, 200, 50, 50), "Activate")) {
      GameObject shipPrefab = GameObject.FindGameObjectWithTag("ShipBlueprint");
      shipPrefab.GetComponent<PlayerSkills>().RecreateSkills();
    }

    if (GUI.Button(new Rect(300, 200, 50, 50), "Turn on")) {
      GameObject shipPrefab = GameObject.FindGameObjectWithTag("ShipBlueprint");
      PlayerActivation pAct = shipPrefab.GetComponent<PlayerActivation>();
      if (!pAct.IsActive) {
        pAct.TurnOn();
      }
    }
  }

  /* TODO for version 3.0
   *
   * Select our factory with the toolbar index (e.g. do we want to edit the weapons, skills, or movement).
   * There should then be a second arguement the specifies what to add or remove (e.g. add the rapid fire skill, provided we are in skill edit mode.
   */
  
  /// Tells our factory what to add. This will eventually pass in two arguements.
  private void addObject(int toolbarIndex) {
    factories[toolbarIndex].AddModule();
  }

  /// Tells our factory what to remove. This will eventually pass in two arguements.
  private void removeObject(int toolbarIndex) {
    factories[toolbarIndex].RemoveModule();
  }
}
