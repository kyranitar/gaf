using UnityEngine;
using System.Collections;

public class StarMapUI : MonoBehaviour {

  public float IconSize = 50;
  public Texture IconTexture;

  private Rect customize;

  void Start() {
    float height = Camera.main.GetScreenHeight();
    customize = new Rect(0, height - IconSize, IconSize, IconSize);
  }

  void OnGUI() {
    if (GUI.Button(customize, IconTexture)) {
      Application.LoadLevel("Customize");
    }
  }
}
