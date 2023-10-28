<?php
include "Connect.php";

session_start();

//Check of server is ingelogd
if(isset($_SESSION["loggedin"]) || $_SESSION["loggedin"]) 
{
    $sessionID = htmlspecialchars($_GET["sessionId"]);
    session_id($sessionID);
    echo $sessionID;
    echo " Server logged in ";
}
else
{
    echo " server not logged in ";
}

$name = $_GET["name"];
$pw = $_GET["pw"];



if(isset($_SESSION["server_id"]) && $_SESSION["server_id"] != 0){
    $query = "SELECT id, name, password FROM Users WHERE name='$name' AND password = '$pw' LIMIT 1";
}else{
    echo "Server id not found $message";
    echo $_GET["server_id"];
}


if (!($result = $mysqli->query($query)))
{
  showerror($mysqli->errno,$mysqli->error); // als toepassen mislukt error laten zien   
  echo "error $message";
}

$row = $result->fetch_assoc(); 


if($name == $row["name"]){
    $_SESSION["user_id"] = $row["id"];
    echo $_SESSION["user_id"];
    echo " User logged in! ";
    
}else{
    echo "0 $message";
}


?>