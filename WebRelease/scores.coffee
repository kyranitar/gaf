{Database} = require 'sqlite3'
db = new Database 'scores.db'

# CREATE TABLE Scores (facebookID TEXT, score INTEGER);

get = "SELECT score FROM Scores WHERE facebookID = ?"
add = "INSERT INTO Scores (score, facebookID) VALUES (?, ?)"
set = "UPDATE Scores SET score = ? WHERE facebookID = ?"

high = "SELECT facebookID, score FROM Scores LIMIT 10 ORDER BY score DESC"

express = require 'express'
app = express.createServer()

app.get '/get', (req, res) ->
  id = req.params 'id'
  db.all get, id, error res, (rows) ->
    if rows.length is 0
      res.end error "ERROR: No such user."
    else
      res.end rows[0].score

app.get '/high', (req, res) ->
  db.all high, error res, (rows) ->
    for row, i in rows
      rows[i] = row.score
    res.end rows.join '\n'

app.post '/set', (req, res) ->
  id = req.params 'id'
  score = req.params 'score'
  db.all get, id, error res, (rows) ->
    which = if rows.length is 0 then add else set
    db.all which, score, id, error res, (err) ->
      res.end ""

error = (res, f) -> (err, rows) ->
  if err then res.end "ERROR: #{err}" else f rows
