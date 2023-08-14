<?php
   $db_user = 'jogchumhofma';
   $db_pass = 'duaxa6Eec9';
   $db_host = 'localhost';
   $db_name = 'jogchumhofma';


$mysqli = new mysqli("$db_host","$db_user","$db_pass","$db_name");


/* check connection */
if ($mysqli->connect_errno) {
   echo "Failed to connect to MySQL: (" . $mysqli->connect_errno() . ") " . $mysqli->connect_error();
    
   exit();
}
    


function showerror($error,$errornr) {
die("Error (" . $errornr . ") " . $error);
}
?>