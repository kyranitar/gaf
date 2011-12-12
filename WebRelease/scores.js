(function() {
  var Database, add, app, db, error, express, get, high, readFile, set;

  Database = require('sqlite3').Database;

  readFile = require('fs').readFile;

  db = new Database('scores.db');

  get = "SELECT score FROM Scores WHERE facebookID = ?";

  add = "INSERT INTO Scores (score, facebookID) VALUES (?, ?)";

  set = "UPDATE Scores SET score = ? WHERE facebookID = ?";

  high = "SELECT facebookID, score FROM Scores ORDER BY score DESC LIMIT 10";

  express = require('express');

  app = express.createServer();

  app.configure(function() {
    return app.use(express.bodyParser());
  });

  app.get('/get', function(req, res) {
    var id;
    id = req.query.id;
    return db.all(get, id, error(res, function(rows) {
      if (rows.length === 0) {
        return res.end('0');
      } else {
        return res.end(rows[0].score.toString());
      }
    }));
  });

  app.get('/high', function(req, res) {
    return db.all(high, error(res, function(rows) {
      var i, row, _len;
      for (i = 0, _len = rows.length; i < _len; i++) {
        row = rows[i];
        rows[i] = "" + row.facebookID + ":" + row.score;
      }
      return res.end(rows.join('\n'));
    }));
  });

  app.post('/add', function(req, res) {
    var id, score;
    console.log("! here i am");
    id = parseInt(req.body.id, 10);
    score = parseInt(req.body.score, 10);
    if (isNaN(id || isNaN(score))) {
      return res.end("ERROR: Invalid score");
    } else {
      return db.all(get, id, error(res, function(rows) {
        var which;
        which = rows.length !== 0 ? (score += rows[0].score, set) : add;
        return db.all(which, score, id, error(res, function(err) {
          return res.end("");
        }));
      }));
    }
  });

  app.get('/crossdomain.xml', function(req, res) {
    return readFile('crossdomain.xml', error(res, function(data) {
      return res.end(data);
    }));
  });

  app.all('*', function(req, res) {
    console.log('invalid path: ' + req.path);
    return res.end('');
  });

  error = function(res, f) {
    return function(err, rows) {
      if (err) {
        return res.end("ERROR: " + err);
      } else {
        return f(rows);
      }
    };
  };

  app.listen(8001);

}).call(this);
