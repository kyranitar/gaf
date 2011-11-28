using UnityEngine;
using System.Collections;

public class CustomizeHUD : MonoBehaviour {

  public float iconSize;
  public Texture iconTex;
  public Texture profileTex;

  public Texture BackIcon;

  private Rect profileIcon;
  private Rect textIcon;
  private Rect abilityIconL;
  private Rect abilityIconR;
  private MaterialHandle materialHandle;

  private Rect backRect;

  void Start() {
    textIcon = new Rect(iconSize * 2.1f, iconSize * 0.1f, iconSize * 3.0f, iconSize);
    profileIcon = new Rect(iconSize, iconSize * 0.1f, iconSize, iconSize);
    abilityIconL = new Rect(Camera.main.GetScreenWidth() / 2 - iconSize * 2, Camera.main.GetScreenHeight() - iconSize * 3, iconSize, iconSize);
    abilityIconR = new Rect(Camera.main.GetScreenWidth() / 2 + iconSize * 2, Camera.main.GetScreenHeight() - iconSize * 3, iconSize, iconSize);

    backRect = new Rect(0, Camera.main.GetScreenHeight() - iconSize, iconSize, iconSize);
    
    GameObject player = GameObject.FindGameObjectWithTag("Player");
    materialHandle = player.GetComponent<MaterialHandle>();
  }

  void Update() {
    
  }

  void OnGUI() {
    
    GUI.Label(profileIcon, profileTex);
    GUI.TextField(textIcon, "Holminator\nScore: 45");
    
    // Left, decrease index
    if (GUI.Button(abilityIconL, iconTex)) {
      materialHandle.IncMaterialIndex(false);
    }
    
    // Right, increase index
    if (GUI.Button(abilityIconR, iconTex)) {
      materialHandle.IncMaterialIndex(true);
    }
    
    if (GUI.Button(backRect, BackIcon)) {
      Application.LoadLevel("Star Map");
    }
  }
}
