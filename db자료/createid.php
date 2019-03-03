<?php

	$servername = "localhost";
	$server_username = "root";
	$server_password = "";
	$dbname = "heroparable";

	$id = $_POST['Id'];
	$password = $_POST['PassWord'];
	$name = $_POST['Name'];
	
	$con = new mysqli($servername,$server_username,$server_password,$dbname);

	if(!$con){
		die("Connection Failed. ".mysqli_connect_error);
	}
	
	$sql = "INSERT INTO userlist(Id,PassWord,Name) VALUES ('".$id."','".$password."','".$name."')"; 
	$result = mysqli_query($con,$sql);
	
	if(!$result) echo "ID";
	else echo "OK";
?>