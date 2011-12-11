using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarMapUI : MonoBehaviour {

  public float IconSize = 50;
  public float Offset = 20.0f;

  public Texture StarmapTex;
  public Texture CustomizeTex;
  public Texture StatsTexture;

  public float GUIscale = 0.8f;

  public Rect customizePos = new Rect (0, 100 + 50, 50, 50);
  public Rect starmapPos = new Rect (0, 100 + 50 * 2, 50, 50);

  private Rect statCoords;

  private Vector3 activeStarPos;
  public Vector3 ActiveStarPos {
    set {
      activeStarPos = value;
    }
  }

  private Vector3 selectedStarPos;
  public Vector3 SelectedStarPos {
    set {
      selectedStarPos = value;
    }
  }

  private Mission selectedMission;
  public Mission SelectedMission {
    set {
      selectedMission = value;
    }
  }

  private Mission hoveredMission;
  public Mission HoveredMission {
    set {
      hoveredMission = value;
    }
  }

  public void Start() {
    statCoords = new Rect(0, 0, 400 * GUIscale, 215 * GUIscale);
  }

  public void Update() {
    GameObject player = GameObject.FindGameObjectWithTag("Player");
    if (player.transform.position != selectedStarPos) {
      selectedMission = null;
    }
  }

  public void OnGUI() {

    if (GUI.Button(customizePos, CustomizeTex)) {
      Application.LoadLevel("Customize");
    }

    if (GUI.Button(starmapPos, StarmapTex)) {
      Debug.Log("nothing");
    }

    int friendCount;
    int enemyCount;
    string difficulty;

    if (hoveredMission != null) {
      statCoords = chooseDirection(activeStarPos);
      friendCount = hoveredMission.Teams[0].ShipCount;
      enemyCount = hoveredMission.Teams[1].ShipCount;
      difficulty = (1.0f / hoveredMission.Teams[0].ShipCount * hoveredMission.Teams[1].ShipCount).ToString("N1");
    } else if (selectedMission != null) {
      GameObject player = GameObject.FindGameObjectWithTag("Player");
      statCoords = chooseDirection(player.transform.position);
      friendCount = selectedMission.Teams[0].ShipCount;
      enemyCount = selectedMission.Teams[1].ShipCount;
      difficulty = (1.0f / selectedMission.Teams[0].ShipCount * selectedMission.Teams[1].ShipCount).ToString("N1");
    } else {
      return;
    }

    GUI.DrawTexture(statCoords, StatsTexture, ScaleMode.StretchToFill, true, 0);
    GUI.Label(new Rect(statCoords.x + statCoords.width * 0.55f, statCoords.y + statCoords.height * 0.25f, 100, 30), "" + friendCount);
    GUI.Label(new Rect(statCoords.x + statCoords.width * 0.55f, statCoords.y + statCoords.height * 0.45f, 100, 30), "" + enemyCount);
    GUI.Label(new Rect(statCoords.x + statCoords.width * 0.55f, statCoords.y + statCoords.height * 0.65f, 100, 30), difficulty);
  }

  private Rect chooseDirection(Vector3 objPosition) {

    Vector3 screenPos = Camera.main.WorldToScreenPoint(objPosition);
    Rect coords = new Rect(screenPos.x, Screen.height - screenPos.y, statCoords.width, statCoords.height);

    if (coords.x + coords.width + Offset > Screen.width) {
      coords.x -= coords.width + Offset;
    } else {
      coords.x += Offset;
    }

    if (coords.y + coords.height + Offset > Screen.height) {
      coords.y -= coords.height + Offset;
    } else {
      coords.y += Offset;
    }

    return coords;
  }
}
