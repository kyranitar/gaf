using UnityEngine;
using System.Collections;

/* GUI handler for signing in and retrieving experience.
 *
 */
public class LoginGUI : MonoBehaviour {

  public string stringToEdit = "username";

  /* Index of # is:
   0) customize off
   1) customize on
   2) profile off
   3) profile on
   4) starmap off
   5) starmap on
   */
  public Texture[] CommonUITextures;

  private Rect LoginFieldPos;
  private Rect ConfirmLoginPos;
  private Rect MenuPos;
  private Rect LeaderBoardsPos;
  private Rect labelPos;

  private Rect gamerTagRect;
  private Rect usernameRect;
  private Rect profilePictureRect;

  private Vector3 ShipLocation;

  private int gsState = 0;

  public Texture FBTexture;
  public string FBName;
  public int CurrentExp;

  void Start() {

    float spacing = 10;

    Vector2 halfRes = new Vector2(Screen.width * 0.5f, Screen.height * 0.25f);

    LoginFieldPos = new Rect(halfRes.x - 35, halfRes.y, 300, 50);
    LoginFieldPos.x -= LoginFieldPos.width * 0.5f;
    ConfirmLoginPos = new Rect(LoginFieldPos.x + LoginFieldPos.width + spacing, LoginFieldPos.y, 50, LoginFieldPos.height);
    labelPos = new Rect(LoginFieldPos.x, ConfirmLoginPos.y + LoginFieldPos.height + spacing, ConfirmLoginPos.width + LoginFieldPos.width + spacing * 0.5f, LoginFieldPos.height);

    MenuPos = new Rect(20, 20, 68, 68);
    LeaderBoardsPos = new Rect(halfRes.x + 200, halfRes.y, 300, 50);
    ShipLocation = new Vector3(0, 0, 0);

    gamerTagRect = new Rect(MenuPos);
    gamerTagRect.x += MenuPos.width + spacing;
    profilePictureRect = new Rect(gamerTagRect.x + spacing, gamerTagRect.y + spacing, 50, 50);
    usernameRect = new Rect(profilePictureRect.x + profilePictureRect.width + spacing, gamerTagRect.y + spacing, 200, 50);

    gamerTagRect.width = usernameRect.width + profilePictureRect.width + spacing * 3.0f;
    gamerTagRect.height = usernameRect.height + spacing * 2.0f;
  }

  void OnGUI() {

    switch (gsState) {

    case 0:
      GSInsertUsername();
      break;

    case 1:
      GSMain();
      break;

    case 2:
      break;
    }
  }

  void GSInsertUsername() {
    // TODO show splash screen.
    stringToEdit = GUI.TextField(LoginFieldPos, stringToEdit, 25);

    if (GUI.Button(ConfirmLoginPos, "Go")) {
      PlayerActivation activater = GameObject.FindGameObjectWithTag("ShipBlueprint").GetComponent<PlayerActivation>();
      activater.Show();
      FacebookController.FacebookUser = stringToEdit;
      gsState = 1;
    }

    GUI.Label(labelPos, "A facebook username should be entered.\nCreate one with Facebook, in your account settings.");
  }

  void GSMain() {

    DrawGamerTag();

    float spacing = 20;

    // TODO show leaderboars, current xp, and ship build.
    Rect tempRect = new Rect(MenuPos);

    // Customize
    if (GUI.Button(tempRect, CommonUITextures[0])) {
      Application.LoadLevel("Customize");
    }
    tempRect.y += tempRect.height + spacing;

    // Profile
    if (GUI.Button(tempRect, CommonUITextures[3])) {
      // Do nothing.
    }
    tempRect.y += tempRect.height + spacing;

    // StarMap
    if (GUI.Button(tempRect, CommonUITextures[4])) {
      Application.LoadLevel("Star Map");
    }


    GUI.Box(new Rect(LeaderBoardsPos.x, LeaderBoardsPos.y, 200, 500), "Leader Boards");
  }

  void GetLeaderBoards() {
    // TODO generate leaderboards
  }

  void GetExperience() {
    // TODO get the users exp from database
  }

  void GetShipSpec() {
    // TODO get the users last created ship.
  }

  void DrawGamerTag() {

    GUI.Box(gamerTagRect, "");

    if (FacebookController.facebookData != null && FacebookController.facebookData.isDone) {
      JSONObject userData = new JSONObject(FacebookController.facebookData.text);
      GUI.Label(usernameRect, "User name: " + userData.GetField("username").str + "\nExperience: " + 18);
    } else {
      GUI.Label(usernameRect, "Loading...");
    }

    if (FacebookController.profilePicture != null && FacebookController.profilePicture.isDone) {
      GUI.DrawTexture(profilePictureRect, FacebookController.profilePicture.texture);
    }

  }

  void ProcessPlayer() {

    /*
     * Get FB username
     * Derive FBID
     *
     * Async
     *   GetExperience()
     *   GetShipSpec
     *   GetLeaderBoards()
     * Sync
     */
  }
}