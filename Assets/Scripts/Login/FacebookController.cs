using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization;

public class FacebookController : MonoBehaviour {

  private static uint facebookID;
  public static uint FacebookID {
    get {
      return facebookID;
    }
    set {
      facebookID = value;
      profilePicture = profilePictureRequest(value);
      facebookData = facebookDataRequest(value);
    }
  }

  public static WWW facebookData = null;
  public static WWW profilePicture = null;

  // The key is the facebook id, value is their score.
  void Start() {
    FacebookID = 643387554;
  }

  private static WWW profilePictureRequest(uint id) {
    return new WWW("http://graph.facebook.com/" + id + "/picture");
  }

  private static WWW facebookDataRequest(uint id) {
    return new WWW("http://graph.facebook.com/" + id);
  }
}
