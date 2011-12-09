using UnityEngine;
using System.Collections;

/// Code taken from the Unity Community forums.
/// Used temporarily to show framerate on screen. (can also be scene by selecting the owner in the unity inspector.
public class FPS : MonoBehaviour {
  float timeA; 
  public int fps;
  public int lastFPS;
  public GUIStyle textStyle;

  void Start () {
    timeA = Time.timeSinceLevelLoad;
  }

  void Update () {

    if(Time.timeSinceLevelLoad  - timeA <= 1) {
      fps++;
    } else {
      lastFPS = fps + 1;
      timeA = Time.timeSinceLevelLoad;
      fps = 0;
    }
  }

  void OnGUI() {
    GUI.Label(new Rect(450, 5, 30, 30), "" + lastFPS, textStyle);
  }
}