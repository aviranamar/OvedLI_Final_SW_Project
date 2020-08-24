<?php

$act=$_GET["action"];
$id_num=$_GET["idnum"];
$id_of_call=$_GET["callid"];
$lastone="";
error_reporting(0);
if($act=="checkifidvaild" && $id_num!="" )
{
		echo ValidateID($id_num);
}
if($act=="changetech" && $id_of_call!="" )
{
		echo changeTech($id_of_call);
}


function ValidateID($str)
		{
			$R_ELEGAL_INPUT=-1;
			$R_NOT_VALID=-2;
			$R_VALID=1;
		   //Convert to string, in case numeric input
		   $IDnum = strval($str);

		   //validate correct input 
		   if(! ctype_digit($IDnum)) // is it all digits
			  return $R_ELEGAL_INPUT;
		   if((strlen($IDnum)>9) || (strlen($IDnum)<5))
			  return $R_ELEGAL_INPUT;

		   //If the input length less then 9 and bigger then 5 add leading 0 
		   while(strlen($IDnum<9))
		   {
			  $IDnum = '0'.$IDnum;
		   }

		   $mone = 0;
		   //Validate the ID number
		   for($i=0; $i<9; $i++)
		   {
			  $char = mb_substr($IDnum, $i, 1);
			  $incNum = intval($char); 
			  $incNum*=($i%2)+1;
			  if($incNum > 9)
				 $incNum-=9;
			  $mone+= $incNum; 
		   } 

		   if($mone%10==0)
			  return $R_VALID;
		   else
			  return $R_NOT_VALID; 
		}	
		
		
		
// 
function getTheGpsLocationByAdress($str)
{
	     $adress=urlencode($str);
		 $url='https://api.tomtom.com/search/2/geocode/'.$adress.'.xml?key=VjsKK4fEUVclIS5slDjs3okAKwrJcOSf&language=he-IL';
		 $thereso = file_get_contents($url);
		 $thereso = strstr( $thereso, '<position>' );
		 $thereso = strstr( $thereso, '</position>',true );
		 $thereso =str_replace("<position>","",$thereso);
		 //$thereso =intval($thereso);
		 $thereso=explode("</lat>",$thereso);
		 $thereso[0]=str_replace("<lat>","",$thereso[0]);
		 $thereso[0]=str_replace(" ","",$thereso[0]);
		 $thereso[1]=str_replace("<lon>","",$thereso[1]);
		 $thereso[1]=str_replace("</lon>","",$thereso[1]);
		 $thereso[1]=str_replace(" ","",$thereso[1]);
		 return trim($thereso[0]).",".trim($thereso[1]);
}

function retrunTheTimeBetweenTwoPoints($str1,$str2)
{

	$adress=urldecode($str1).":".urldecode($str2);
		 $url='https://api.tomtom.com/routing/1/calculateRoute/'.$adress.'?key=VjsKK4fEUVclIS5slDjs3okAKwrJcOSf';
		 $thereso = file_get_contents($url);
		 $thereso = strstr( $thereso, '<travelTimeInSeconds>' );
		 $thereso = strstr( $thereso, '</travelTimeInSeconds>',true );
		 $thereso =str_replace("<travelTimeInSeconds>","",$thereso);
		 $thereso =intval($thereso);
		 
		 
		 
		 return intval($thereso/60);
}
function getthenextcallofthetech($current,$techid,$date)
{
	$res=null;
	$flag=0;
	$thecalls = file_get_contents('https://ana10project.firebaseio.com/calls/callsmone/.json?format=export');
	$thecalls = json_decode($thecalls);

	if(intval($thecalls)>intval($current))
	{
		for($i=$current+1;$i<intval($thecalls)+1;$i++)
		{
			if($flag==0){
			$thecallsof = file_get_contents('https://ana10project.firebaseio.com/calls/'.$i.'/.json?format=export');
			$thecallsof = json_decode($thecallsof);
			
			$thedate=explode(" ",$thecallsof->{'arrivedate'});
			if($thedate[0]==$date && $techid==$thecallsof->{'techid'})
				{
					return $thecallsof;
					$flag++;
				}
				
			}
		}
	}
	return $res;
}

function addTheTime($currentTime,$addTime)
{
	
	if($currentTime==0)
	{
		$currentTime="08:00";
		
	}
	$currentTime=explode(":",$currentTime);
	
	if((intval($addTime)/60)>=1){
	$currentTime[0]=intval($currentTime[0])+(intval($addTime)/60);
	$currentTime[1]=intval($currentTime[1])+(intval($addTime)%60);
	if($currentTime[1]>=60)
	{
		$currentTime[0]=intval($currentTime[0])+1;
		$currentTime[1]=$currentTime[1]%60;
		
	}
	}
	if($currentTime[0]>=24)
	{
		$currentTime[0]=intval($currentTime[0])/24;
	}
	else
	{
		
		$currentTime[1]=intval($currentTime[1])+$addTime;
		if($currentTime[1]>=60)
	{
		$currentTime[0]=intval($currentTime[0])+1;
		$currentTime[1]=$currentTime[1]%60;
	}
	if($currentTime[0]>=24)
	{
		$currentTime[0]=intval($currentTime[0])/24;
	}
		
		
	}
	
	if(intval($currentTime[0])<=9)
	{
		if($currentTime[0][0]!=0){
		$currentTime[0]="0".$currentTime[0];
		}
	}
	if(intval($currentTime[1])<=9)
	{
		//if($currentTime[1][0]!=0){
		$currentTime[1]="0".$currentTime[1];
		//}
	}
	
	
	return $currentTime[0].":".$currentTime[1];
}
 
function getTheServiceTime($str)
{
	$getServiceTime = file_get_contents('https://ana10project.firebaseio.com/services/'.$str.'/.json?format=export');
	$getServiceTime = json_decode($getServiceTime);
	return intval($getServiceTime->{'time'});
	
}

function returnTheTimeOfTheTech($techid,$date)
{
	$time=0;
	$thecalls = file_get_contents('https://ana10project.firebaseio.com/calls/callsmone/.json?format=export');
	$thecalls = json_decode($thecalls);
	for($i=1;$i<intval($thecalls)+1;$i++)
	{
		$thecallsof = file_get_contents('https://ana10project.firebaseio.com/calls/'.$i.'/.json?format=export');
		$thecallsof = json_decode($thecallsof);
		$thedate=explode(" ",$thecallsof->{'arrivedate'});
		if($thedate[0]==$date && $techid==$thecallsof->{'techid'})
			{
				$num=getTheServiceTime($thecallsof->{'serviceid'});
				$time=addTheTime($time,$num);
				
				if(getthenextcallofthetech($i,$techid,$date)!=null)
				{
					
					$time=addTheTime($time,retrunTheTimeBetweenTwoPoints(getTheGpsLocationByAdress($thecallsof->{'geolocation'}),getTheGpsLocationByAdress(getthenextcallofthetech($i,$techid,$date)->{'geolocation'})));
					
					
				}
				else
				{
					$GLOBALS['lastone']=$i;
				}
					
			}
		
	}
	return $time;
}

function GetTheCompanyByServiceId($str)
{
	$grabTheCompanyId = file_get_contents('https://ana10project.firebaseio.com/services/'.$str.'/.json?format=export');
	$grabTheCompanyId = json_decode($grabTheCompanyId);
	if($grabTheCompanyId->{'companyID'}!=null)
		{
			return $grabTheCompanyId->{'companyID'};
		}
		else
		{
			return null;
		}

}

function changeTech($call_id)
{
	$the_res=0;
	$thecall = file_get_contents('https://ana10project.firebaseio.com/calls/'.$call_id.'/.json?format=export');
	$thecall  = json_decode($thecall);
	$the_current_teach=$thecall->{'techid'};
	$the_date=$thecall->{'arrivedate'};
	$the_date=explode(" ",$the_date);
	$the_mone_of_tech = file_get_contents('https://ana10project.firebaseio.com/employees/moneemployees/.json?format=export');
	$the_mone_of_tech  = json_decode($the_mone_of_tech);
	$the_mone_of_tech =intval($the_mone_of_tech);
	for($i=1;$i<$the_mone_of_tech+1;$i++)
	{
		$the_employee= file_get_contents('https://ana10project.firebaseio.com/employees/'.$i.'/.json?format=export');
		$the_employee  = json_decode($the_employee);
		$thecall_continu = file_get_contents('https://ana10project.firebaseio.com/calls/'.$_GET["callid"].'/.json?format=export');
		$thecall_continu= json_decode($thecall_continu);
		$Another_employee=intval($thecall_continu->{'techid'});
			if($Another_employee!=$i){
		if(intval($the_employee->{'companyid'})==intval(GetTheCompanyByServiceId($thecall_continu->{'serviceid'})))
			{
		
		
				 $if_the_tech_will_arrived_at_time=returnTheTimeOfTheTech($i,$the_date[0]);
				$to_Get_The_Last_Location= file_get_contents('https://ana10project.firebaseio.com/employees/'.$GLOBALS['lastone'].'/.json?format=export');
				$to_Get_The_Last_Location  = json_decode($to_Get_The_Last_Location);
				$timebetwenn2points=intval(retrunTheTimeBetweenTwoPoints($to_Get_The_Last_Location->{'geolocation'},$thecall_continu->{'geolocation'}));
				 $time4time=addTheTime($if_the_tech_will_arrived_at_time,$timebetwenn2points);
				 $temp_first_time=$thecall_continu->{'arrivedate'};
				 $temp_first_time=explode(" ",$temp_first_time);
				 $temp_first_time=explode(":",$temp_first_time[1]);
				 $time4time=explode(":",$temp_first_time[1]);
				 if(intval($temp_first_time[0])>=intval($time4time[0]))
				 {
					 if(intval($temp_first_time[1])>=intval($time4time[1]))
					 {
						 $the_res=$i;
					 }
				 }
			   
			}
			}
		//echo returnTheTimeOfTheTech($i,$the_date[0]);
	}
	
	return '{"thetech":'. $the_res.'}';
}

		
?>