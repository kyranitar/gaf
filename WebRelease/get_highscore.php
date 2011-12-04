<?php

  // Open the database
  try {
    $db = new PDO('sqlite:players.sqlite');
  } catch(PDOException $e) {
    print 'Exception : '.$e->getMessage();
  }

  // Create the database
  //$db->exec("CREATE TABLE Scores (Id INTEGER PRIMARY  KEY, fbid TEXT, score INTEGER, creation_date TIMESTAMP)");

  // Insert parameters
  //$db->exec("INSERT INTO Scores (fbid, score, creation_date) VALUES ('$fbid', $score, date('now'))");
  $result = $db->query('SELECT fbid, MAX(score) AS score FROM Scores GROUP BY fbid ORDER BY score DESC');

  foreach($result as $row) {
    echo $row['fbid'] . ',' . $row['score'] . "\n";
  }

?>

