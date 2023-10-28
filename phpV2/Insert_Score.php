<?php
include "Connect.php";


echo "HELLO WORLD $message";

session_start();

//Check of server is ingelogd
if(isset($_SESSION["loggedin"]) || $_SESSION["loggedin"]) {
    $sessionID = htmlspecialchars($_GET["sessionId"]);
    session_id($sessionID);
    echo $sessionID;
    echo "Server logged in";
}else{
    echo "server not logged in";
}

echo session_id();

$server = $_SESSION["server_id"];
$user = $_GET["user_id"];
$score = $_GET["score"]; 
$game_id = 3;
#$user_id = 100;
$check_var = 0;




if (filter_var($score, FILTER_VALIDATE_INT) === 0 || !filter_var($score, FILTER_VALIDATE_INT) ===  false) {
    echo "Score is an integer.";
  } else {
    echo "Score is DEFINiTELY not an integer";
    $check_var = 1;
  }
if (!(filter_var($game_id, FILTER_VALIDATE_INT))) {
  $check_var = 1;
    echo $game_id;
}
if (!(filter_var($user, FILTER_VALIDATE_INT))) {
  $check_var = 1;
    echo $user;
}



if (isset($_SESSION["server_id"]) && $_SESSION["server_id"] != 0) {

    if($check_var == 0){
        $query = "INSERT INTO Score(id, score, game_id, user_id, server_id, datetime) 
        VALUES (NULL, $score, $game_id, $user, $_SESSION[server_id], CURRENT_TIMESTAMP)"; //query    
    } else {
        echo"ERROR IN CHECKV_VAR $message";
        echo"User id";
        echo $user;
        echo"Game id";
        echo $game_id;
        echo"Score";
        echo $score;
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