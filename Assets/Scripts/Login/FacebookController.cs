using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class FacebookController : MonoBehaviour {
  private const int max = 6;
  private string btnText = "Press to roll";
  public static string FacebookId = "1";
  private Dictionary<string, int> scores;
  // The key is the facebook id, value is his score.
  void Start() {
    scores = new Dictionary<string, int>();
    StartCoroutine(retrieveHighscores());
  }

  void OnGUI() {
    
    GUI.Label(new Rect(10, 80, 200, 50), "FacebookID:\n" + FacebookId);
    
    
    if (GUI.Button(new Rect(10, 160, 200, 50), "start game")) {
      Application.LoadLevel("Star Map");
    }
    
    if (GUI.Button(new Rect(10, 10, 200, 50), btnText)) {
      int score = UnityEngine.Random.Range(1, max + 1);
      btnText = string.Format("You rolled {0}, \nclick to try again.", score);
      
      StartCoroutine(submitHighscore(score, FacebookId));
    }
    
    float h = 10;
    foreach (var score in scores) {
      GUI.Label(new Rect(300, h, 200, 50), score.Value.ToString());
      
      h += 60;
    }
  }

  IEnumerator submitHighscore(int score, string fbid) {
    WWW webRequest = new WWW("http://localhost/insert_highscore.php?score=" + score + "&fbid=" + fbid);
    yield return webRequest;
    
    yield return retrieveHighscores();
    //After we submit we'd want an update, perhaps someone else played too!
  }

  IEnumerator retrieveHighscores() {
    WWW webRequest = new WWW("http://localhost/get_highscores.php");
    yield return webRequest;
    
    string[] lines = webRequest.text.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
    
    scores = new Dictionary<string, int>();
    //Always reset our scores, as we just got new ones.
    foreach (string line in lines) {
      string[] parts = line.Split(',');
      
      string id = parts[0];
      int score = int.Parse(parts[1]);
      
      scores.Add(id, score);
    }
  }
}
