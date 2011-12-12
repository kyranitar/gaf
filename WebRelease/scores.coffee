{Database} = require 'sqlite3'
{readFile} = require 'fs'
db = new Database 'scores.db'


# CREATE TABLE Scores (facebookID TEXT, score INTEGER);

get = "SELECT score FROM Scores WHERE facebookID = ?"
add = "INSERT INTO Scores (score, facebookID) VALUES (?, ?)"
set = "UPDATE Scores SET score = ? WHERE facebookID = ?"

high = "SELECT facebookID, score FROM Scores ORDER BY score DESC LIMIT 10"

express = require 'express'
app = express.createServer()

app.configure ->
  app.use express.bodyParser()

app.get '/get', (req, res) ->
  id = req.query.id
  db.all get, id, error res, (rows) ->
    if rows.length is 0
      res.end "ERROR: No such user."
    else
      res.end String rows[0].score

app.get '/high', (req, res) ->
  db.all high, error res, (rows) ->
    for row, i in rows
      rows[i] = "#{row.facebookID}:#{row.score}"
    res.end rows.join '\n'

app.post '/add', (req, res) ->
  console.log "! here i am"
  id = req.body.id
  score = parseInt req.body.score, 10
  if isNaN score
    res.end "ERROR: Invalid score"
  else
    db.all get, id, error res, (rows) ->
      which = if rows.length isnt 0
        score += rows[0].score
        set
      else add
      db.all which, score, id, error res, (err) ->
        res.end ""

app.get '/crossdomain.xml', (req, res) ->
  readFile 'crossdomain.xml', error res, (data) ->
    res.end data

app.all '*', (req, res) ->
  console.log 'invalid path: ' + req.path
  res.end ''

error = (res, f) -> (err, rows) ->
  if err then res.end "ERROR: #{err}" else f rows

app.listen 8001
