using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class FacebookManager : MonoBehaviour {

  public void Start() {
//    Application.ExternalCall("sendCurrentUser");
  }

  public void SetFacebookUserID(string fbid) {
    FacebookController.FacebookID = uint.Parse(fbid);
  }

}
