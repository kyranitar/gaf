using UnityEngine;

public class DrawHUD : MonoBehaviour {

  public Texture PeppyTex;

  private const float cHealthBarWidth = 70f;
  private const float cHealthBarHeight = 10f;

  private const int cAbilityIconSize = 32;
  private const int cAbilityIconSpacing = 10;

  private GUIStyle cHealthBarStyle = new GUIStyle();

  public void Start() {
    cHealthBarStyle.alignment = TextAnchor.UpperLeft;
    cHealthBarStyle.normal.textColor = Color.white;
    cHealthBarStyle.fontStyle = FontStyle.Bold;
  }

  public void OnGUI() {
    DrawAbilityBar();
  }

  public void DrawAbilityBar() {
    Rect AbilityIcon = new Rect(Camera.main.GetScreenWidth() / 2 - 2 * cAbilityIconSize - 1.5f * cAbilityIconSpacing, Camera.main.GetScreenHeight() - cAbilityIconSize - cAbilityIconSpacing,
                                cAbilityIconSize, cAbilityIconSize);

    for (int i = 0; i < 4; i++) {
      GUI.Label(AbilityIcon, PeppyTex);
      AbilityIcon.x += cAbilityIconSize + cAbilityIconSpacing;
    }
  }
}
