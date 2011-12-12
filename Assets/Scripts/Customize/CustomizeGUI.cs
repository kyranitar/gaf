using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// Handles the interface for the customize screen.
/// In version three this will tell the Module Factories (ModuleFactory.cs) on the 'Ship' (if not 'Ship' it will be 'PlayerShip')
/// what modules to create during the AddModule() method.
public class CustomizeGUI : MonoBehaviour {

  private int selectedUI;

  private const int boxSpacing = 50;
  private const float boxScreenWidth = 0.25f;

  private Rect customiseRect;
  private Rect[] weaponButtons;
  private Rect[] skillButtons;
  private Rect[] controlsButtons;

  private string[] toolbarStrings = new string[3] { "Weapons", "Skills", "Controls" };

  // Number of Available weapons -- get from Factory
  private int numWeapons;
  // Number of Available skills -- get from Factory
  private int numSkills;
  // Number of Available control schemes -- get from Factory
  private int numControls;

  // TODO get this data from hull type?
  // Number of weapons the ship can have
  private const int maxWeapons = 2;
  // Number of skills the ship can have
  private const int maxSkills = 2;
  // Number of control schemes the ship can have
  private const int maxControls = 1;

  // Size (diameter?) of buttons
  private const int buttonSize = 45;
  // Left offset to center the buttons
  private int buttonLeftOffset = 0;
  // Number of buttons per row in the button grid
  private int buttonsPerRow;

  private ModuleFactory weaponFactory;
  private ModuleFactory skillFactory;
  private ModuleFactory controlFactory;

  private string message = "Weclome to the Customize Menu.\nHere you can toggle different weapons, skills,\nand control schemes on and off.";

  public void Start() {
    // Show ship and enable factories
    GameObject shipPrefab = GameObject.FindGameObjectWithTag("ShipBlueprint");
    
    PlayerActivation activater = shipPrefab.GetComponent<PlayerActivation>();
    activater.SetFactoriesEnabled(true);
    activater.Show();
    
    
    foreach (ModuleFactory mf in shipPrefab.GetComponents<ModuleFactory>()) {
      if (mf.FactoryType == "Weapon") {
        weaponFactory = mf;
      } else if (mf.FactoryType == "Skill") {
        skillFactory = mf;
      } else if (mf.FactoryType == "Control") {
        controlFactory = mf;
      }
    }
    
    // Set Factory-dependent variables
    numWeapons = weaponFactory.Prefabs.Count;
    numSkills = skillFactory.Prefabs.Count;
    numControls = controlFactory.Prefabs.Count;
    
    // TODO update this stuff to version 3.0
    
    InitUI();
  }

  private void InitUI() {
    int boxLeft = boxSpacing;
    int boxTop = boxSpacing;
    
    int boxWidth = (int) (Camera.current.GetScreenWidth() * boxScreenWidth - boxSpacing);
    
    buttonsPerRow = Mathf.FloorToInt((boxWidth + boxSpacing) / (buttonSize + boxSpacing));
    
    int boxHeight = (int) (Camera.current.GetScreenHeight() - boxSpacing * 2);
    
    customiseRect = new Rect(boxLeft, boxTop, boxWidth, boxHeight);
    
    buttonLeftOffset = ((boxWidth - boxSpacing) - (int) ((buttonSize + boxSpacing) * Mathf.Floor((boxWidth - boxSpacing) / (buttonSize + boxSpacing)))) / 2;
    
    // Create Individual Button Bounding Boxes
    weaponButtons = new Rect[numWeapons];
    MakeButtons(weaponButtons);
    
    skillButtons = new Rect[numSkills];
    MakeButtons(skillButtons);
    
    controlsButtons = new Rect[numControls];
    MakeButtons(controlsButtons);
  }

  private void MakeButtons(Rect[] rectArray) {
    int yOffset = boxSpacing * 4;
    
    for (int i = 0; i < rectArray.Length; i++) {
      if (i % buttonsPerRow == 0 && i != 0)
        yOffset += boxSpacing + buttonSize;
      
      int bLeft = buttonLeftOffset + (int) customiseRect.x + boxSpacing * ((i % buttonsPerRow) + 1) + buttonSize * (i % buttonsPerRow);
      int bTop = yOffset;
      
      rectArray[i] = new Rect(bLeft, bTop, buttonSize * 2.0f, buttonSize);
    }
  }

  public void OnGUI() {

    GUI.Box(new Rect(Screen.width * 0.5f, Screen.height * 0.5f, 400, 200), "");
    GUI.Label(new Rect(Screen.width * 0.5f + 10, Screen.height * 0.5f + 10, 380, 180), message);

    // Update the toolbar.
    selectedUI = GUI.Toolbar(new Rect(boxSpacing, boxSpacing, customiseRect.width, boxSpacing * 2), selectedUI, toolbarStrings);
    GUI.Box(customiseRect, "");
    int i = 0;
    
    if (selectedUI == 0) {
      foreach (GameObject prefab in weaponFactory.Prefabs) {
        Texture buttonGrahpic = (IsMade(weaponFactory, prefab) ? weaponFactory.OnImages[i] : weaponFactory.OffImages[i]);
        if (GUI.Button(weaponButtons[i++], buttonGrahpic)) {
          ToggleWeapon(prefab);
        }
      }
    } else if (selectedUI == 1) {
      foreach (GameObject prefab in skillFactory.Prefabs) {
        Texture buttonGrahpic = (IsMade(skillFactory, prefab) ? skillFactory.OnImages[i] : skillFactory.OffImages[i]);
        if (GUI.Button(skillButtons[i++], buttonGrahpic)) {
          ToggleSkill(prefab);
        }
      }
    } else if (selectedUI == 2) {
      foreach (GameObject prefab in controlFactory.Prefabs) {
        Texture buttonGrahpic = (IsMade(controlFactory, prefab) ? controlFactory.OnImages[i] : skillFactory.OffImages[i]);
        if (GUI.Button(controlsButtons[i++], buttonGrahpic)) {
          ToggleControls(prefab);
        }
      }
    }
    
    Rect backButtonRect = new Rect(boxSpacing, customiseRect.height - boxSpacing, customiseRect.width, boxSpacing * 2);
    
    if (GUI.Button(backButtonRect, "Back")) {

      if (weaponFactory.Modules.Count <= 0 || skillFactory.Modules.Count <= 0 || controlFactory.Modules.Count <= 0) {
        message = "You must always have atleast one weapon, one skill,\nand control scheme on your ship. Please choose these\nbefore leaving this menu.";
        return;
      }

      GameObject shipPrefab = GameObject.FindGameObjectWithTag("ShipBlueprint");
      PlayerActivation activater = shipPrefab.GetComponent<PlayerActivation>();
      activater.Hide();
      activater.SetFactoriesEnabled(false);
      Application.LoadLevel("Star Map");
    }
  }

  private void ToggleWeapon(GameObject prefab) {

    // Check if the factory has already instaited this prefab, if so remove it.
    for (int i = 0; i < weaponFactory.Modules.Count; i++) {
      if (weaponFactory.Modules[i].name == prefab.name) {
        weaponFactory.RemoveModule(i);
        return;
      }
    }

    // Check for max, and return if so.
    if(weaponFactory.Modules.Count >= maxWeapons) {
      message = "You have reached the maximum amount of weapons.\nPlease remove one first.";
      return;
    }

    // If not found, then add it.
    weaponFactory.AddModuleByObject(prefab);
  }

  private void ToggleSkill(GameObject prefab) {

    // Check if the factory has already instaited this prefab, if so remove it.
    for (int i = 0; i < skillFactory.Modules.Count; i++) {
      if (skillFactory.Modules[i].name == prefab.name) {
        skillFactory.RemoveModule(i);
        return;
      }
    }

    // Check for max, and return if so.
    if(skillFactory.Modules.Count >= maxSkills) {
      message = "You have reached the maximum amount of skills.\nPlease remove one first.";
      return;
    }

    // If not found, then add it.
    skillFactory.AddModuleByObject(prefab);
  }

  private void ToggleControls(GameObject prefab) {

    // Check if the factory has already instaited this prefab, if so remove it.
    for (int i = 0; i < controlFactory.Modules.Count; i++) {
      if (controlFactory.Modules[i].name == prefab.name) {
        controlFactory.RemoveModule(i);
        return;
      }
    }

    // Check for max, and return if so.
    if(controlFactory.Modules.Count >= maxControls) {
      message = "You have reached the maximum amount of control schemes.\nPlease remove one first.";
      return;
    }

    // If not found, then add it.
    controlFactory.AddModuleByObject(prefab);

  }

  private bool IsMade(ModuleFactory factory, GameObject prefab) {
    foreach(GameObject module in factory.Modules) {
      if(module.name == prefab.name) {
        return true;
      }
    }

   return false;
  }
}
