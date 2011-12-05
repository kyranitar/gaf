using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

  public GameObject shipPrefab;

  private Rect AddRemoveCoords = new Rect(25, 25, 100, 90);
  private Rect addCoords;
  private Rect removeCoords;

  private Rect toolbarCoords = new Rect(25, Screen.height - 55, 250, 30);
  private int toolbarIndex = 0;
  private string[] toolbarStrings;

  private MovementCreation movement;
  private SkillCreation skills;
  private WeaponCreation weapons;

  void Start() {

    addCoords = new Rect(AddRemoveCoords.x + 10, AddRemoveCoords.y + 30, AddRemoveCoords.width * 0.8f, AddRemoveCoords.height * 0.2f);
    removeCoords = new Rect(AddRemoveCoords.x + 10, AddRemoveCoords.y + 60, AddRemoveCoords.width * 0.8f, AddRemoveCoords.height * 0.2f);

    if (shipPrefab != null) {
      shipPrefab = Instantiate(shipPrefab) as GameObject;
      movement = shipPrefab.GetComponent<MovementCreation>();
      skills = shipPrefab.GetComponent<SkillCreation>();
      weapons = shipPrefab.GetComponent<WeaponCreation>();
    } else {
      Debug.LogError("No ship prefab found");
    }
    toolbarStrings = new string[3] { "Weapons", "Movement", "Skills" };
  }

  void Update() {
    
    
  }

  void OnGUI() {

    toolbarIndex = GUI.Toolbar(toolbarCoords, toolbarIndex, toolbarStrings);

    GUI.Box(AddRemoveCoords, toolbarStrings[toolbarIndex]);

    if (GUI.Button(addCoords, "Add")) {
      AddObject();
    }

    if (GUI.Button(removeCoords, "Remove")) {
      RemoveObject();
    }

    if (Input.GetKeyDown(KeyCode.L)) {
      Debug.Log(toolbarIndex);
    }
  }

  void AddObject() {
    
    switch (toolbarIndex) {
    
    // Weapons
    case 0:
      weapons.AddOne();
      break;

    // Movement
    case 1:
      movement.AddOne();
      break;

    // Skills
    case 2:
      skills.AddOne();
      break;
    }
  }

  void RemoveObject() {

    switch (toolbarIndex) {

    // Weapons
    case 0:
      weapons.RemoveOne();
      break;

    // Movement
    case 1:
      movement.RemoveOne();
      break;

    // Skills
    case 2:
      skills.RemoveOne();
      break;
    }
  }
}
