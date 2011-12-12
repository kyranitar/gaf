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

  private static string facebookUser;
  public static string FacebookUser {
    get {
      return facebookUser;
    }
    set {
      facebookUser = value;
      profilePicture = profilePictureRequest(value);
      facebookData = facebookDataRequest(value);
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

  private static WWW profilePictureRequest(string username) {
    return new WWW("http://graph.facebook.com/" + username + "/picture");
  }

  private static WWW facebookDataRequest(string username) {
    return new WWW("http://graph.facebook.com/" + username);
  }
}
