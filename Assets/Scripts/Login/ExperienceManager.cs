using UnityEngine;

public class ExperienceManager {

  public static string domain = "localhost";
  public static uint ID = 643387554;

  public static void AddExperience(uint amount) {
    new WWW("http://" + domain + "/add?id=" + ID + "&amount=" + amount);
  }

}
