using UnityEngine;
using System.Collections;

public class SkillBar : RadialBar {
  
  private int skillIndex;
  public int SkillIndex {
    get {
      return skillIndex;
    }
    set {
      skillIndex = value;
    }
  }

  private int offensiveSkillCount;
  public int OffensiveSkillCount {
    get {
      return offensiveSkillCount;
    }
    set {
      offensiveSkillCount = value;
    }
  }

  private int defensiveSkillCount;
  public int DefensiveSkillCount {
    get {
      return defensiveSkillCount;
    }
    set {
      defensiveSkillCount = value;
    }
  }

  private Ability controller;
  public Ability Controller {
    set {
      controller = value;
    }
  }

  public override void Start() {
    // Do Nothing
  }

  public void Init() {

    base.Start();

    // Set different colors, TODO change this to materials
    if (skillIndex == 0) {
      if (controller.isOffensive) {
        SetColor(Color.red);
      } else {
        SetColor(Color.cyan);
      }
    } else if (skillIndex == 1) {
      SetColor(Color.magenta);
    }


  }

	new public void Update() {

    // Hack - doesn't seem to pull variables properly in start, possibly because skill is intantiated after start? Don't know.
    if(TotalValue == 0) {
      TotalValue = controller.CooldownTime + controller.ActiveTime;
    }

    CurrentValue = TotalValue - (controller.CooldownTimeLeft + controller.ActiveTimeLeft);
    if(controller.name == "RapidFire(Clone)") {
      //Debug.Log(controller.TotalValue + " " controller);
    }
    //Debug.Log(controller.name + " " + controller.CooldownTimeLeft + " " + controller.ActiveTimeLeft);
    base.Update();
  }

  public void Attach() {

    if(controller.isOffensive) {
      // Anchor to cursor
      transform.position = GameObject.Find("Cursor").transform.position;
      transform.parent = GameObject.Find("Cursor").transform;
      MinAngle = (360.0f / offensiveSkillCount) * skillIndex;
      MaxAngle = MinAngle + 360.0f / offensiveSkillCount;
    } else {
      // Anchor to player
      transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
      transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
      MinAngle = (360.0f / defensiveSkillCount) * SkillIndex;
      MaxAngle = MinAngle + 360.0f / defensiveSkillCount;
    }
  }
}