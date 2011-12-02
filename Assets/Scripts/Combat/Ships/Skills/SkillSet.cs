using UnityEngine;
using System.Collections;

public class SkillSet : MonoBehaviour {

  public GameObject[] Skills;
  public float iconSize = 40;
  public float iconSpacing = 20;
  public bool isPlayer;

  private skillIcon[] icons;

  protected struct skillIcon {
    public Texture tex;
    public Rect pos;
  }

  void Start() {

    int length = Skills.Length;
    icons = new skillIcon[Skills.Length];

    for (int i = 0; i < length; i++) {

      GameObject skill = Instantiate(Skills[i]) as GameObject;
      Ability script = skill.GetComponent<Ability>();
      script.Ship = gameObject;
      Skills[i] = skill;

      if (isPlayer) {
        icons[i] = createIcon(i, script.Image);
      }
    }
  }

  void Update() {
    
    if (Input.GetKeyDown(KeyCode.Z) && Skills.Length > 0) {
      Skills[0].GetComponent<Ability>().Activate(transform.position);
    }
  }

  private skillIcon createIcon(int index, Texture image) {
    skillIcon icon = new skillIcon();
    icon.tex = image;
    icon.pos = new Rect(Camera.main.GetScreenWidth() * 0.5f - iconSize * index + iconSpacing * index, Camera.main.GetScreenHeight() - iconSize * 2.0f, iconSize, iconSize);
    return icon;
  }

  void OnGUI() {
    foreach (skillIcon icon in icons) {
      GUI.Button(icon.pos, icon.tex);
    }
  }
}
