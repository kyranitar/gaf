using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

  public static string FacebookId = "1";
  private const int max = 6;
  private string btnText = "Press to roll";

  public void OnGUI() {

    if (GUI.Button(new Rect(10, 10, 200, 50), btnText)) {
      int score = UnityEngine.Random.Range(1, max + 1);
      btnText = string.Format("You rolled {0}, \n click to try again.", score);

//      StartCoroutine(submitHighscore(score, FacebookId));
    }
  }

  private IEnumerator sumbitHighscore(int score, string fbid) {
    yield return null;
  }
  
}