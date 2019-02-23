<?php

	$servername = "localhost";
	$server_username = "root";
	$server_password = "";
	$dbName = "heroparable";
	
	$user_id ="gameis";
	$user_password = "lkt0430";
	$user_name = "근태";
	$user_gold = 0;
	$user_cash = 0;
	$user_maxclearstage = 11; 
	
	$conn = new mysqli($servername,$server_username, $server_password, $dbName) ;

	if(!$conn)
		die("Connection Failed. ". mysqli_connect_error());	
	
	$sql = "INSERT INTO userlist (Id, PassWord, Name, Gold, Cash, MaxClearStage) 
			VALUES ('".$user_id."', '".$user_password."', '".$user_name."', '".$user_gold."', '".$user_cash."', '".$user_maxclearstage."')";

	$result = mysqli_query($conn, $sql);

	if(!result)
	{
		echo "there was an error";
	}else 
	{
		echo "Everything ok.";
	}













?>