<?php
echo '<head>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
</head>
<script>';
echo 'var data = {"rank": "4"};
jQuery.ajax({
    accept: "application/json",';
	echo " type: 'PATCH',";
	
	echo 'contentType: "application/json; charset=utf-8",
    dataType: "json",
    url: "https://ana10project.firebaseio.com/calls/1/.json",
    data: JSON.stringify(data),
});

</script>';




?>