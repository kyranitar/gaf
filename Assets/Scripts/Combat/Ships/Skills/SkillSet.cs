using UnityEngine;
using System.Collections;

public class SkillSet : MonoBehaviour {

  public GameObject[] Skills;
  public float iconSize = 40;
  public float iconSpacing = 80;
  public bool isPlayer;

  //private skillIcon[] icons;

  protected struct skillIcon {
    public Texture tex;
    public Rect pos;
  }

  public void Start() {
    
    int length = Skills.Length;
    //icons = new skillIcon[Skills.Length];
    
    for (int i = 0; i < length; i++) {
      GameObject skill = Instantiate(Skills[i]) as GameObject;
      skill.GetComponent<Ability>().Ship = gameObject;
      Ability abilityBase = skill.GetComponent<Ability>();
      
      abilityBase.Ship = gameObject;
      Skills[i] = skill;
      
      if (isPlayer) {
        Cursor cursorRef = GameObject.Find("Cursor").GetComponent<Cursor>();
        cursorRef.AddAbilityReference(abilityBase);
        //icons[i] = createIcon(i, script.Image);
      }
    }
  }

  private skillIcon createIcon(int index, Texture image) {
    skillIcon icon = new skillIcon();
    icon.tex = image;
    icon.pos = new Rect(Camera.main.GetScreenWidth() * 0.5f - iconSize * index + (iconSize + iconSpacing) * index, Camera.main.GetScreenHeight() - iconSize * 2.0f, iconSize, iconSize);
    return icon;
  }

  void OnGUI() {
  }
  /*foreach (skillIcon icon in icons) {
      GUI.Button(icon.pos, icon.tex);
    }*/

  public void OnDestroy() {
    foreach (GameObject skill in Skills) {
      Destroy(skill);
    }
  }
  
}
