using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillSet : MonoBehaviour {

  public GameObject[] Skills;
  public int defensiveCount;
  public GameObject SkillBarPrefab;
  public float iconSize = 40;
  public float iconSpacing = 80;

  public bool isPlayer;
  private List<GameObject> skillBars;

  private int offensiveIndex = 0;
  private int defensiveIndex = 0;

  public void Start() {

    int length = Skills.Length;

    for (int i = 0; i < length; i++) {

      GameObject skill = Instantiate(Skills[i]) as GameObject;
      Ability abilityBase = skill.GetComponent<Ability>();
      abilityBase.Ship = gameObject;
      Skills[i] = skill;

      if (isPlayer) {
        GameObject skillBar = Instantiate(SkillBarPrefab) as GameObject;
        SkillBar script = skillBar.GetComponent<SkillBar>();

        script.DefensiveSkillCount = defensiveCount;
        script.OffensiveSkillCount = Skills.Length - defensiveCount;

        if(abilityBase.isOffensive) {
          script.SkillIndex = offensiveIndex++;
        } else {
          script.SkillIndex = defensiveIndex++;
        }

        script.Controller = abilityBase;
        script.Init();
      }
    }
  }

  public void OnDestroy() {
    foreach (GameObject skill in Skills) {
      Destroy(skill);
    }
  }
  
}
