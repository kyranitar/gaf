using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// Handles the interface for the customize screen.
/// In version three this will tell the Module Factories (ModuleFactory.cs) on the 'Ship' (if not 'Ship' it will be 'PlayerShip')
/// what modules to create during the AddModule() method.
public class CustomizeGUI : MonoBehaviour
{

  private int selectedUI;

  private const int boxSpacing = 10;
  private const float boxScreenWidth = 0.25f;

  private Rect customiseRect;
  private Rect[] weaponButtons;
  private Rect[] skillButtons;
  private Rect[] controlsButtons;

  private string[] toolbarStrings = new string[3] { "Weapons", "Skills", "Controls" };
  private string[] weaponNames;
  private string[] skillNames;
  private string[] controlsNames;

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

  private List<int> equippedWeapons = new List<int> ();
  private List<int> equippedSkills = new List<int> ();
  private List<int> equippedControls = new List<int> ();

  private ModuleFactory weaponFactory;
  private ModuleFactory skillFactory;
  private ModuleFactory controlsFactory;

  public void Start ()
  {
    // Show ship and enable factories
    GameObject shipPrefab = GameObject.FindGameObjectWithTag ("ShipBlueprint");
    
    PlayerActivation activater = shipPrefab.GetComponent<PlayerActivation> ();
    activater.SetFactoriesEnabled (true);
    activater.Show ();
    
    
    foreach (ModuleFactory mf in shipPrefab.GetComponents<ModuleFactory> ()) {
      if (mf.FactoryType == "Weapon") {
        weaponFactory = mf;
      } else if (mf.FactoryType == "Skill") {
        skillFactory = mf;
      } else if (mf.FactoryType == "Control") {
        controlsFactory = mf;
      }
    }
    
    // Set Factory-dependent variables
    numWeapons = weaponFactory.Prefabs.Length;
    numSkills = skillFactory.Prefabs.Length;
    numControls = controlsFactory.Prefabs.Length;
    
    weaponNames = new string[numWeapons];
    skillNames = new string[numSkills];
    controlsNames = new string[numControls];
    
    for (int i = 0; i < weaponNames.Length; i++)
      weaponNames[i] = weaponFactory.Prefabs[i].name;
    for (int i = 0; i < skillNames.Length; i++)
      skillNames[i] = skillFactory.Prefabs[i].name;
    for (int i = 0; i < controlsNames.Length; i++)
      controlsNames[i] = controlsFactory.Prefabs[i].name;
    
    // TODO update this stuff to version 3.0
    
    InitUI ();
  }

  private void InitUI ()
  {
    int boxLeft = boxSpacing;
    int boxTop = boxSpacing;
    
    int boxWidth = (int)(Camera.current.GetScreenWidth () * boxScreenWidth - boxSpacing);
    
    buttonsPerRow = Mathf.FloorToInt ((boxWidth + boxSpacing) / (buttonSize + boxSpacing));
    
    int boxHeight = (int)(Camera.current.GetScreenHeight () - boxSpacing * 2);
    
    customiseRect = new Rect (boxLeft, boxTop, boxWidth, boxHeight);
    
    buttonLeftOffset = ((boxWidth - boxSpacing) - (int)((buttonSize + boxSpacing) * Mathf.Floor ((boxWidth - boxSpacing) / (buttonSize + boxSpacing)))) / 2;
    
    // Create Individual Button Bounding Boxes
    weaponButtons = new Rect[numWeapons];
    MakeButtons (weaponButtons);
    
    skillButtons = new Rect[numSkills];
    MakeButtons (skillButtons);
    
    controlsButtons = new Rect[numControls];
    MakeButtons (controlsButtons);
  }

  private void MakeButtons (Rect[] rectArray)
  {
    int yOffset = boxSpacing * 4;
    
    for (int i = 0; i < rectArray.Length; i++) {
      if (i % buttonsPerRow == 0 && i != 0)
        yOffset += boxSpacing + buttonSize;
      
      int bLeft = buttonLeftOffset + (int)customiseRect.x + boxSpacing * ((i % buttonsPerRow) + 1) + buttonSize * (i % buttonsPerRow);
      int bTop = yOffset;
      
      rectArray[i] = new Rect (bLeft, bTop, buttonSize, buttonSize);
    }
  }

  public void OnGUI ()
  {
    
    // Update the toolbar.
    selectedUI = GUI.Toolbar (new Rect (boxSpacing, boxSpacing, customiseRect.width, boxSpacing * 2), selectedUI, toolbarStrings);
    GUI.Box (customiseRect, "");
    
    if (selectedUI == 0) {
      for (int i = 0; i < weaponButtons.Length; i++)
        if (GUI.Button (weaponButtons[i], weaponNames[i]))
          ToggleWeapon (i);
    } else if (selectedUI == 1) {
      for (int i = 0; i < skillButtons.Length; i++)
        if (GUI.Button (skillButtons[i], skillNames[i]))
          ToggleSkill (i);
    } else if (selectedUI == 2) {
      for (int i = 0; i < controlsButtons.Length; i++)
        if (GUI.Button (controlsButtons[i], controlsNames[i]))
          ToggleControls (i);
    }
    
    Rect backButtonRect = new Rect (boxSpacing, customiseRect.height - boxSpacing, customiseRect.width, boxSpacing * 2);
    
    if (GUI.Button (backButtonRect, "Back")) {
      GameObject shipPrefab = GameObject.FindGameObjectWithTag ("ShipBlueprint");
      PlayerActivation activater = shipPrefab.GetComponent<PlayerActivation> ();
      activater.Hide ();
      activater.SetFactoriesEnabled (false);
      Application.LoadLevel ("Star Map");
    }
  }

  private void ToggleWeapon (int i)
  {
    if (equippedWeapons.Contains (i)) {
      //TODO change icon rather than string
      weaponNames[i] = ToggleString (weaponNames[i]);
      equippedWeapons.Remove (i);
      
      weaponFactory.RemoveModuleByName (weaponNames[i]);
      return;
    }
    if (equippedWeapons.Count >= maxWeapons)
      ToggleWeapon (equippedWeapons[0]);
    
    equippedWeapons.Add (i);
    weaponNames[i] = ToggleString (weaponNames[i]);
    
    weaponFactory.AddModule (i);
  }

  private void ToggleSkill (int i)
  {
    if (equippedSkills.Contains (i)) {
      //TODO change icon rather than string
      skillNames[i] = ToggleString (skillNames[i]);
      equippedSkills.Remove (i);
      
      skillFactory.RemoveModuleByName (skillNames[i]);
      return;
    }
    if (equippedSkills.Count >= maxSkills)
      ToggleSkill (equippedSkills[0]);
    
    equippedSkills.Add (i);
    skillNames[i] = ToggleString (skillNames[i]);
    
    skillFactory.AddModule (i);
  }

  private void ToggleControls (int i)
  {
    if (equippedControls.Contains (i)) {
      //TODO change icon rather than string
      controlsNames[i] = ToggleString (controlsNames[i]);
      equippedControls.Remove (i);
      
      controlsFactory.RemoveModuleByName (controlsNames[i]);
      return;
    }
    if (equippedControls.Count >= maxControls)
      ToggleControls (equippedControls[0]);
    
    equippedControls.Add (i);
    controlsNames[i] = ToggleString (controlsNames[i]);
    
    controlsFactory.AddModule (i);
  }

  private void RemoveByName (List<GameObject> objects, string name)
  {
    for (int i = 0; i < objects.Count; i++) {
      if (objects[i].name.Contains (name)) {
        objects.RemoveAt (i);
        return;
      }
    }
  }

  string ToggleString (string s)
  {
    return s;
  }
  
  /* TODO for version 3.0
   *
   * Select our factory with the toolbar index (e.g. do we want to edit the weapons, skills, or movement).
   * There should then be a second arguement the specifies what to add or remove (e.g. add the rapid fire skill, provided we are in skill edit mode.
   */  
}
