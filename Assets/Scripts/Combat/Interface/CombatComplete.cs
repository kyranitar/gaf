using UnityEngine;

public class CombatComplete : MonoBehaviour {

  public int statusWidth = 200;
  public int statusHeight = 50;

  public int victoryExp = 20;
  public int defeatExp = 10;

  private CombatStatus status = CombatStatus.Pending;

  public void Victory() {
    status = CombatStatus.Victory;
  }

  public void Defeat() {
    status = CombatStatus.Defeat;
  }

  public void OnGUI() {
    if (status == CombatStatus.Pending) {
      return;
    }
    
    string label = status == CombatStatus.Victory ? "Victory!" : "Defeat.";
    
    int x = Screen.width / 2 - statusWidth / 2;
    int y = Screen.height / 2 - statusHeight / 2;
    
    GUI.Label(new Rect(x, y, statusWidth, statusHeight), label);

    if (GUI.Button(new Rect(Screen.width - 200, Screen.height - 50, 200, 50), "Continue")) {
      ExperienceManager.ModifyExperience(status == CombatStatus.Victory ? victoryExp : defeatExp);

      if (status == CombatStatus.Victory) {
        GameObject player = GameObject.FindGameObjectWithTag("ShipBlueprint");
        player.GetComponent<PlayerDamage>().ExitCombat();
      }

      status = CombatStatus.Pending;
      Application.LoadLevel("Star Map");
    }
  }

  private enum CombatStatus {
    Victory,
    Defeat,
    Pending
  }
  
}
