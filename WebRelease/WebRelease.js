
  window.fbAsyncInit = function() {
    return FB.init({
      appId: '240667755997026',
      status: true,
      cookie: true,
      oauth: true,
      xfbml: false
    });
  };

  addEventListener('load', function() {
    var doSend, getUnity;
    window.sendCurrentUser = function() {
      return FB.getLoginStatus(function(res) {
        if (res.authResponse) {
          return doSend();
        } else {
          return FB.login(function(res) {
            if (res.authResponse) return doSend();
          });
        }
      });
    };
    doSend = function() {
      return FB.api('/me', function(res) {
        var unity;
        if (unity = getUnity()) {
          return unity.SendMessage("FacebookManager", "SetFacebookUserID", res.id);
        }
      });
    };
    getUnity = function() {
      if (typeof unityObject !== 'undefined') {
        return unityObject.getObjectById("unityPlayer");
      } else {
        return null;
      }
    };
    if (typeof unityObject !== 'undefined') {
      return unityObject.embedUnity('unityPlayer', '/WebRelease.unity3d', 960, 640);
    }
  }, false);
