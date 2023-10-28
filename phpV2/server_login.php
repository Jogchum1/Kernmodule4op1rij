<?php
include "Connect.php";
session_start();

$id = $_GET["id"];
$pw = $_GET["pw"];
 
$query = "SELECT id, password FROM Servers WHERE id='$id' AND password ='$pw' LIMIT 1 ";
    
if (!($result = $mysqli->query($query)))
{
  showerror($mysqli->errno,$mysqli->error); // als toepassen mislukt error laten zien    
} 
    
$row = $result->fetch_assoc(); 
$my_json = json_encode($row); 




if($id == $row["id"]){
    
    echo session_id();
    echo " is session id $message";
    
    $_SESSION["server_id"] = 7;
    //$_SESSION["sessionId"] = 7;
    $_SESSION["loggedin"] = true;
}else{
    echo "0 $message";
}





?>