<?php

  // Necessary parameters
  $score = @$_GET['score'];
  $fbid = @$_GET['fbid'];

  // Unnecessary paramteres
  $table = @$_GET['table'];

  // Ensure necessary parameters are passed
  if (!$score || !$fbid) {
    die(
      "No facebook id (fbid) or score (score) are given.<br>
      Try using - ?fbid=4&score=1<br><br>"
      );
  }

  // Open the database
  try {
    $db = new PDO('sqlite:players.sqlite');
  } catch(PDOException $e) {
    print 'Exception : '.$e->getMessage();
  }

  // Create the database
  $db->exec("CREATE TABLE Scores (Id INTEGER PRIMARY  KEY, fbid TEXT, score INTEGER, creation_date TIMESTAMP)");

  // Insert parameters
  $db->exec("INSERT INTO Scores (fbid, score, creation_date) VALUES ('$fbid', $score, date('now'))");

?>