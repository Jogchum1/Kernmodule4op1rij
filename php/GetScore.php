<?php

include "Connect.php";

$name = $_GET["name"];

$start = strtotime("-1 month");
$date =  date('Y-m-d h:i:s', $start);
//$query = "SELECT s.user_id, COUNT(user_id) AS aantal_keer_gespeeld, AVG(score) AS gemiddelde_score FROM Score s LEFT JOIN Users u ON (s.user_id = u.id) GROUP BY user_id ORDER BY gemiddelde_score DESC";
    
$query = "SELECT u.name, u.id ,s.datetime AS untill_date, SUM(s.score) AS wins FROM Users u INNER JOIN Score s ON s.user_id=u.id Where s.datetime >= '$date' AND name='$name'";
if (!($result = $mysqli->query($query))) // query toepassen
  showerror($mysqli->errno,$mysqli->error); // als toepassen mislukt error laten zien


$row = $result->fetch_assoc();

do {
  $my_json .= json_encode($row); 
} while ($row = $result->fetch_assoc()); 


 
echo $my_json; 

    
?>