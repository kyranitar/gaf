express = require 'express'
app = express.createServer()

app.configure ->
  app.use express.static __dirname
  app.use express.errorHandler dumpExceptions: true, showStack: true

app.redirect 'play', '/WebRelease.html'

app.get '/', (req, res) ->
  res.redirect 'play'

app.listen 8080
