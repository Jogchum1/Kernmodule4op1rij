<?php
include "Connect.php";
#echo "HELLO WORLD $message";

session_start();

$name = $_GET["name"];
$pw = $_GET["pw"];
$check_var = 0;

if(isset($_SESSION["server_id"])){
    $query = "SELECT id, name, password FROM Users WHERE name='$name' LIMIT 1";
    $query2 = "SELECT password FROM Users WHERE name='$name' LIMIT 1";
}else{
    echo "Server id not found $message";
}

#if($check_var == 1){
#    $query = "SELECT id, name, password FROM Users WHERE name='$name' ";
#}


if (!($result = $mysqli->query($query)))
{
  showerror($mysqli->errno,$mysqli->error); // als toepassen mislukt error laten zien   
  echo "error $message";
}
else //Test of wachtwoord klopt met user
{
    $result2 = $mysqli->query($query2);
    $row = mysqli_fetch_assoc($result2);
    $hash = $row['password'];
    
    $ver_pw = password_verify($pw, $hash);
    if($ver_pw == true)
    {
        $check_var = 1;
    }
    else
    {
        echo 'Invalid password.';
        $check_var = 0;
    }
} 

$row = $result->fetch_assoc(); 


if($name == $row["name"] && $check_var == 1){
    $_SESSION["user_id"] = $row["id"];
    
}else{
    echo "0 $message";
}


#session_destroy();
?>