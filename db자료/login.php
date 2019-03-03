<?php

	$servername = "localhost";
	$server_username = "root";
	$server_password = "";
	$dbname = "heroparable";

	$id = $_POST['Id'];
	$password = $_POST['PassWord'];
	
	$con = new mysqli($servername,$server_username,$server_password,$dbname);

	if(!$con){
		die("Connection Failed. ".mysqli_connect_error);
	}
	
	$sql = "SELECT * FROM userlist WHERE Id='".$id."'";
	$result = mysqli_query($con,$sql);
	
	$numrows = mysqli_num_rows($result);    
	
	if ($numrows == 0)
	{
		echo "ID";
	}	
	else
	{
		while($row = mysqli_fetch_assoc($result))
		{
			if($password == $row['PassWord'])
			{	
				echo "OK";	
			}
			else
			{
				echo "PassWord";
			}
		}
	}
?>