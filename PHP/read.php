<?php
session_start();
?>
<html>

<body>
<iframe src="newfile.html"></iframe>
</body>
</html>
<?php

	if($_SESSION["favcolor"]==""){
		$_SESSION["favcolor"] ="yes";
		echo '<meta http-equiv="refresh" content="2">';
		
	}
	else{
				$myfile = fopen("newfile.html", "w") or die("Unable to open file!");
				$txt = "";
				fwrite($myfile, $txt);
				fclose($myfile);
				$_SESSION["favcolor"]="";
    }

				?>