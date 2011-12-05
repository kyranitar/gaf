using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class FacebookManager : MonoBehaviour
{
    void Start()
    {
        Application.ExternalCall("GetCurrentUser");
    }
    public void GetCurrentUserComplete(string fbid)
    {
        FacebookController.FacebookId = fbid;
    }
}
