using UnityEngine;

public class ExperienceManager {

  public static const string domain = "localhost";
  public static const string ID = 643387554;

  public static void AddExperience(uint amount) {
    new WWW("http://" + domain + "/add?id=" + ID + "&amount=" + amount);
  }

}
