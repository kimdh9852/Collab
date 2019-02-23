 <?php

	$servername = "localhost";
	$username = "root";
	$password = "";
	$dbName = "heroparable";
	
	$conn = new mysqli($servername,$username, $password, $dbName) ;

	if(!$conn)
		die("Connection Failed. ". mysqli_connect_error());	
	
	$sql = "SELECT Id, PassWord, Name, Gold, Cash, MaxClearStage FROM userlist";
	$result = mysqli_query($conn, $sql);

	if(mysqli_num_rows($result) > 0)
	{
		while($row = mysqli_fetch_assoc($result))
		{
			echo"Id:".$row['Id']."|PassWord:".$row['PassWord']. "|Name:".$row['Name']."|Gold:".$row['Gold']."|Cash:".$row['Cash']."|MaxClearStage:".$row['MaxClearStage']. "<br>";
		}
	}






?>