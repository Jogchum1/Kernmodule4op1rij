<?php
include "Connect.php";
echo "HELLO WORLD $message";

session_start();

$id = $_GET["id"];
$pw = $_GET["pw"];
 
#$_SESSION["server_id"] = 2;

$query = "SELECT id, password FROM Servers WHERE id='$id' AND password ='$pw' LIMIT 1 ";
    
if (!($result = $mysqli->query($query)))
{
  showerror($mysqli->errno,$mysqli->error); // als toepassen mislukt error laten zien    
} 
    
$row = $result->fetch_assoc(); 
$my_json .= json_encode($row); 
echo $my_json;


if($id == $row["id"]){
    
    echo session_id();
    echo "is session id $message";
    
    $_SESSION["server_id"] = 1;
    echo $_SESSION["server_id"];
    echo "test $message";
    

}else{
    echo "0 $message";
}





#session_destroy();
?>