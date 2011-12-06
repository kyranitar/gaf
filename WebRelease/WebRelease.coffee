window.fbAsyncInit = ->
  FB.init
    appId:  '240667755997026'
    status: true
    cookie: true
    oauth:  true
    xfbml:  false

addEventListener 'load', ->

  window.sendCurrentUser = ->
    FB.getLoginStatus (res) ->
      if res.authResponse
        doSend()
      else
        FB.login (res) ->
          if res.authResponse
            doSend()
  
  doSend = ->
    FB.api '/me', (res) ->
      if unity = getUnity()
        unity.SendMessage "FacebookManager", "SetFacebookUserID", res.id

  getUnity = ->
    if typeof unityObject isnt 'undefined'
      unityObject.getObjectById "unityPlayer"
    else
      null

  if typeof unityObject isnt 'undefined'
    unityObject.embedUnity 'unityPlayer', '/WebRelease.unity3d', 960, 640

, false
