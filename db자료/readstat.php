<?php

	$servername = "localhost";
	$server_username = "root";
	$server_password = "";
	$dbname = "heroparable";

	$con = new mysqli($servername,$server_username,$server_password,$dbname);

	if(!$con){
		die("Connection Failed. ".mysqli_connect_error);
	}
	
	$sql = "SELECT Hp, AttackPower, AttackRange, MoveSpeed FROM Stat"; 
	$result = mysqli_query($con,$sql);
	
	if(mysqli_num_rows($result) > 0)
	{
		while($row = mysqli_fetch_assoc($result))
		{
			echo "".$row['Hp']. "|" .$row['AttackPower']. "|" .$row['AttackRange']. "|" .$row['MoveSpeed']. ";";
		}	
	}
	
?>