<?php
include "Connect.php";

echo "HELLO WORLD $message";
session_start();

$name = $_GET["name"];
$email = $_GET["mail"];
$password = $_GET["pw"];
$check_var = 0;
$hashed_password = password_hash($password, PASSWORD_DEFAULT);

if (!(filter_var($email, FILTER_VALIDATE_EMAIL))) {
  echo("$email is not a valid email address ");
    $check_var = 1;
}


if ($password == trim($password) && strpos($password, ' ') !== false) {
    echo 'has spaces, but not at beginning or end';
    $check_var = 1;
}

if($check_var == 0){
    if(isset($_SESSION["server_id"])){
        $query = "INSERT INTO Users(id, name, email, password, datetime) 
        VALUES (NULL, '$name', '$email', '$hashed_password', CURRENT_TIMESTAMP)"; 
    }else{
        echo "Couldnt find server id $message";
    }
    
    
}
else{
    echo "0 $message";
}


if (!($result = $mysqli->query($query)))
{
  showerror($mysqli->errno,$mysqli->error); // als toepassen mislukt error laten zien    
} else {
    echo "TIS GELUKT $message";
    echo $hashed_password;
}

?>