using UnityEngine;
using System.Collections.Generic;

public class FacebookController : MonoBehaviour {

  private static uint facebookID = 0;
  public static uint FacebookID {
    get {
      return facebookID;
    }
    set {
      facebookID = value;
      ExperienceManager.id = value;
      profilePicture = profilePictureRequest(value);
      facebookData = facebookDataRequest(value);
    }
  }

  public static string FacebookUsername {
    set {
      GameObject obj = new GameObject();
      MonoBehaviour beh = obj.AddComponent<MonoBehaviour>();
      beh.StartCoroutine(facebookDataRequest(value));
    }
  }

  public static WWW facebookData = null;
  public static WWW profilePicture = null;

  // The key is the facebook id, value is their score.
  private static WWW profilePictureRequest(uint id) {
    return new WWW("http://graph.facebook.com/" + id + "/picture");
  }

  private static WWW facebookDataRequest(uint id) {
    return new WWW("http://graph.facebook.com/" + id);
  }

  private static IEnumerator<WWW> facebookDataRequest(string username) {
    WWW req = new WWW("http://graph.facebook.com/" + username);
    yield return req;
    FacebookID = uint.Parse(new JSONObject(req.text).GetField("id").str);
  }

  private static WWW experienceRequest(uint id) {
    return new WWW("http://zimothy.com/get?id=" + id);
  }

}
