using UnityEngine;
using System.Collections.Generic;

public class ExperienceManager {

  public static string domain = "zimothy.com:8001";
  public static uint id;

  private static GameObject held;
  private static MonoBehaviour sender;
  static ExperienceManager() {
    held = new GameObject();
    GameObject.DontDestroyOnLoad(held);
    sender = held.AddComponent<MonoBehaviour>();
  }

  public delegate void ExperienceCallback(int exp);

  public static void GetExperience(ExperienceCallback callback) {
    sender.StartCoroutine(SendGet(callback));
  }

  private static IEnumerator<WWW> SendGet(ExperienceCallback callback) {
    WWW req = new WWW("http://" + domain + "/get?id=" + id);
    yield return req;
    callback(int.Parse(req.text));
  }

  public delegate void HighScoreCallback(Score[] scores);

  public static void GetHighScores(HighScoreCallback callback) {
    sender.StartCoroutine(SendHigh(callback));
  }

  private static IEnumerator<WWW> SendHigh(HighScoreCallback callback) {
    WWW req = new WWW("http://" + domain + "/high");
    yield return req;
    string[] res = req.text.Split('\n');
    Score[] scores = new Score[res.Length];
    for (int i = 0; i < res.Length; ++i) {
      string[] each = res[i].Split(':');
      WWW fb = new WWW("http://graph.facebook.com/" + each[0]);
      yield return fb;
      string uname = new JSONObject(fb.text).GetField("username").str;
      WWW pr = new WWW("http://graph.facebook.com/" + each[0] + "/picture");
      yield return pr;
      scores[i] = new Score(uname, pr.texture, int.Parse(each[1]));
    }

    callback(scores);
  }

  public class Score {

    public string username;
    public Texture picture;
    public int score;

    public Score(string username, Texture picture, int score) {
      this.username = username;
      this.picture = picture;
      this.score = score;
    }

  }

  public static void ModifyExperience(int amount) {
    sender.StartCoroutine(SendModification(amount));
  }

  private static IEnumerator<WWW> SendModification(int amount) {
    WWWForm form = new WWWForm();
    form.AddField("id", id.ToString());
    form.AddField("score", amount);

    yield return new WWW("http://" + domain + "/add", form);
  }
  
}
