using UnityEngine;
using System.Text;

public class ExperienceManager {

  public static string domain = "zimothy.com:8001";
  public static uint id = 643387554;

  public static void ModifyExperience(int amount) {
    WWWForm form = new WWWForm();
    form.AddField("id", id.ToString());
    form.AddField("score", amount);
    new WWW("http://" + domain + "/add", form);
  }

}
