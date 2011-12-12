using UnityEngine;
using System.Text;

public class ExperienceManager {

  public static string domain = "localhost";
  public static uint id = 643387554;

  public static void ModifyExperience(int amount) {
    Encoding utf8 = new UTF8Encoding();
    byte[] data = utf8.GetBytes("id=" + id + "&amount=" + amount);
    new WWW("http://" + domain + "/add", data);
  }

}
