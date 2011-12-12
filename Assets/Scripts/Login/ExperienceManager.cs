using UnityEngine;
using System.Text;

public class ExperienceManager {

  public static string domain = "localhost";
  public static uint ID = 643387554;

  public static void AddExperience(uint amount) {
    Encoding utf8 = new UTF8Encoding();
    byte[] data = utf8.GetBytes("id=" + ID + "&amount=" + amount);
    new WWW("http://" + domain + "/add", data);
  }

}
