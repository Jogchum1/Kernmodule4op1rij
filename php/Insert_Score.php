<?php
include "Connect.php";


echo "HELLO WORLD $message";

session_start();
echo session_id();

$server = $_SESSION["server_id"];
$user = $_SESSION["user_id"];
$score = $_GET["score"]; 
$game_id = 3;
#$user_id = 100;
$check_var = 0;



/*
if (!(filter_var($score, FILTER_VALIDATE_INT))) {
  $check_var = 1;
}
if (!(filter_var($game_id, FILTER_VALIDATE_INT))) {
  $check_var = 1;
}
if (!(filter_var($user_id, FILTER_VALIDATE_INT))) {
  $check_var = 1;
}


*/
if (isset($_SESSION["server_id"])) {

    if($check_var == 0){
        $query = "INSERT INTO Score(id, score, game_id, user_id, server_id, datetime) 
        VALUES (NULL, $score, $game_id, $_SESSION[user_id], $_SESSION[server_id], CURRENT_TIMESTAMP)"; //query    
    } else {
        echo"ERROR IN CHECKV_VAR $message";
    }
} else {
    
    echo"Error in server id $message";
  //echo 0 (foutmelding aan unity server dat ie opnieuw moet inloggen)

}



if (!($result = $mysqli->query($query)))
{
  showerror($mysqli->errno,$mysqli->error); // als toepassen mislukt error laten zien    
} else {
    echo "TIS GELUKT $message";
}


?>