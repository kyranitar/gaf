using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization;

public class FacebookController : MonoBehaviour {

//  public string Domain = "localhost";

//  private const int max = 6;
//  private string btnText = "Press to roll";
//  private Dictionary<string, int> scores;

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

  private static WWW facebookData = null;
  private static WWW profilePicture = null;

  private Rect usernameRect = new Rect(10, 80, 200, 50);
  private Rect profilePictureRect = new Rect(0, 0, 50, 50);

  // The key is the facebook id, value is his score.
  void Start() {
//    scores = new Dictionary<string, int>();
    FacebookID = 643387554;
  }

  void OnGUI() {
    if (facebookData != null && facebookData.isDone) {
      JSONObject userData = new JSONObject(facebookData.text);
      GUI.Label(usernameRect, "User name: " + userData.GetField("username"));
    } else {
      GUI.Label(usernameRect, "Loading...");
    }

    if (GUI.Button(new Rect(10, 160, 200, 50), "start game")) {
      Application.LoadLevel("Star Map");
    }

//    if (GUI.Button(new Rect(10, 10, 200, 50), btnText)) {
//      int score = UnityEngine.Random.Range(1, max + 1);
//      btnText = string.Format("You rolled {0}, \nclick to try again.", score);
//
//      StartCoroutine(setScore(FacebookId, score));
//    }

//    float h = 10;
//    foreach (var score in scores) {
//      GUI.Label(new Rect(300, h, 200, 50), score.Value.ToString());
//
//      h += 60;
//    }

    if (profilePicture != null && profilePicture.isDone) {
      GUI.DrawTexture(profilePictureRect, profilePicture.texture);
    }
  }

//  IEnumerator<WWW> getScore(string facebookID) {
//    WWW request = new WWW(getURL(facebookID));
//    yield return request;
//
//    int.Parse(request.text);
//  }
//
//  IEnumerator<WWW> setScore(string facebookID, int score) {
//    WWW request = new WWW(setURL(facebookID, score));
//    yield return request;
//  }
//
//  IEnumerator<WWW> getHighScores() {
//    WWW request = new WWW(highURL());
//    yield return request;
//    
//    string[] lines = request.text.Split(new string[] { "\n" }, System.StringSplitOptions.RemoveEmptyEntries);
//    
//    scores = new Dictionary<string, int>();
//    //Always reset our scores, as we just got new ones.
//    foreach (string line in lines) {
//      string[] parts = line.Split(',');
//      
//      string id = parts[0];
//      int score = int.Parse(parts[1]);
//
//      scores.Add(id, score);
//    }
//  }

  private static WWW profilePictureRequest(uint id) {
    return new WWW("http://graph.facebook.com/" + id + "/picture");
  }

  private static WWW facebookDataRequest(uint id) {
    return new WWW("http://graph.facebook.com/" + id);
  }

//  private string getURL(string facebookID) {
//    return "http://" + Domain + "/get?id=" + facebookID;
//  }
//
//  private string setURL(string facebookID, int score) {
//    return "http://" + Domain + "/set?id=" + facebookID + "&score=" + score;
//  }
//
//  private string highURL() {
//    return "http://" + Domain + "/all";
//  }

}
