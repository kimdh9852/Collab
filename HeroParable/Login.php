<?php

//unity import
$user = $_POST['Input_user'];
$pass = $_POST['Input_pass'];

// mysql�� ���̵�� ��й�ȣ�� �Է����ش�. 
$con = mysql_connect("jupitermhl.cafe24.com","jupitermhl","whswkfaudgkr813!") or ("Cannot connect!" .mysql_error());

if(!$con)
	die('Cound not Connect:' . mysql_error());

mysql_select_db("jupitermhl",$con) or die ("could not load the database" .mysql_error());


$check = mysql_query("SELECT * FROM TotalUserList WHERE `Id`='".$user."'");  
//TotalUserList��� ���̺��� ���� �Է��� Id���� ã�ڴ�. 


// Mysql_num_row()�Լ��� �����ͺ��̽����� ������ ������ ���� ���ڵ��� ������ �˾Ƴ��� ����.
// �� 0�� ���Դٴ� ���� ���� ���� ã�� ID���� ���ٴ� ���̴�. 

$numrows = mysql_num_rows($check);    
if ($numrows == 0)
{
	die("ID does not exist. \n");
}
else
{
	while($row = mysql_fetch_assoc($check))
	{
		if($pass == $row['pass'])
		{	
			//������ �ҷ��´�. 
			echo ("'".$row ['Info']."' \n");
			die("Login-Success!!");
		}
		else
		{
			die("Pass does not Match. \n");
		}
	}
}

?>