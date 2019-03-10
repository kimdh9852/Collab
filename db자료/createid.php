<?php

	$servername = "http://jupitermhl.cafe24.com/410";
	$server_username = "jupitermhl";
	$server_password = "whswkfaudgkr813!";
	$dbname = "jupitermhl";

	$id = $_POST['Id'];
	$password = $_POST['PassWord'];
	
	$con = new mysqli($servername,$server_username,$server_password,$dbname);

	if(!$con){
		die("Connection Failed. ".mysqli_connect_error);
	}
	
	$sql = "INSERT INTO userlist(Id,PassWord) VALUES ('".$id."','".$password."')"; 
	$result = mysqli_query($con,$sql);
	
	if(!$result) echo "Id";
		else echo "OK";
	
?>