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
	
	$id_sql = mysqli_query($con, "SELECT Id FROM userlist WHERE Id = '".$id."'"); 
	$name_sql = mysqli_query($con, "SELECT Name FROM userlist WHERE Name = '".$name."'"); 
	
	// $id_overlap_decision = $id_sql -> mysqli_fetch_array(); 
	// $name_overlap_decision = $name_sql -> mysqli_fetch_array(); 

	// if($id_overlap_decision != 0) 
		// die "ID"; 
	// else if($name_overlap_decision != 0) 
		// die "Name";
	
	if($id_sql != false)//ID검색결과가 있으면
		die("ID");
	else if($name != false)//Name검색결과가 있으면
		die("Name");
	
	
	
	$sql = "INSERT INTO userlist(Id,PassWord,Name) VALUES ('".$id."','".$password."','".$name."')"; 
	$result = mysqli_query($con,$sql);
	
	if(!$result) echo "Failed";
	else echo "OK";
	

	
	
	
	
	
?>