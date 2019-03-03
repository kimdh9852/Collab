<?php

	$servername = "localhost";
	$server_username = "root";
	$server_password = "";
	$dbname = "heroparable";

	$hp = $_POST['Hp'];
	$ap = $_POST['AttackPower'];
	$ar = $_POST['AttackRange'];
	$ms = $_POST['MoveSpeed'];

	$con = new mysqli($servername,$server_username,$server_password,$dbname);
	
	if(!$con){
		die("Connection Failed. ".mysqli_connect_error);
	}
	
	$sql = "UPDATE stat SET Hp = '".$hp."',AttackPower= '".$ap."',AttackRange = '".$ar."',MoveSpeed = '".$ms."'";
	
	$result = mysqli_query($con, $sql);
	
	if(!$result) 
	{
		echo "Error";
	}
	else
	{
		echo "OK";
	}
	
?>