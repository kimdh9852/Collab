<?php

//unity import
$user = $_POST['Input_user'];
$pass = $_POST['Input_pass'];

// mysql의 아이디와 비밀번호를 입력해준다. 
$con = mysql_connect("jupitermhl.cafe24.com","jupitermhl","whswkfaudgkr813!") or ("Cannot connect!" .mysql_error());

if(!$con){
	die('Cound not Connect:' . mysql_error());
}
mysql_select_db("jupitermhl",$con) or die ("could not load the database" .mysql_error());

$check = mysql_query("SELECT `Evade` FROM `123_ChaList` WHERE 1");  

$numrows = mysql_num_rows($check);    
if ($numrows == 0)
{
	die("Does not exist. \n");
}
else
{
	while($row = mysql_fetch_assoc($check))
	{
		echo ("'".$row['Evade`']."' \n");
	}
}

?>