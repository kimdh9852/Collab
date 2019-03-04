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
	
	$id_sql = mysqli_query($con,"SELECT * FROM userlist WHERE Id = '".$id."'"); 
	$name_sql = mysqli_query($con,"SELECT * FROM userlist WHERE Name = '".$name."'"); 
	$id_overlap_decision = mysqli_fetch_array($id_sql); 
	$name_overlap_decision = mysqli_fetch_array($name_sql); 

	if($id_overlap_decision != 0) 
	{
		echo ("Id");
		die();
	}
	else if($name_overlap_decision != 0) 
	{	
		echo ("Name");
		die();
	}
	
	$sql = "INSERT INTO userlist(Id,PassWord,Name) VALUES ('".$id."','".$password."','".$name."')"; 
	$result = mysqli_query($con,$sql);
	
	if(!$result) echo "Failed";
		else echo "OK";
	
?>