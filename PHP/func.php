<?php
session_start();
if(isset($_GET["act"]))
{
  $act=$_GET["act"];
}

error_reporting(0);
$codeIsVaild=false;
/* creat new file 
$myfile = fopen("newfile.html", "w") or die("Unable to open file!");
$txt = "John Doe\n";
fwrite($myfile, $txt);
$txt = "Jane Doe\n";
fwrite($myfile, $txt);
fclose($myfile);
*/

//check if the user exits
function getAllTheCompnies()
{
	$getCompany = file_get_contents('https://ana10project.firebaseio.com/company/mone/.json?format=export');
	$theCounter=$getCompany;
	for($i=1;$i<=$theCounter;$i++)
	{
		$getCompany = file_get_contents('https://ana10project.firebaseio.com/company/'.$i.'/.json?format=export');
		$getCompany = json_decode($getCompany);
		$companyArr[$i][0]=$getCompany->{'companyName'};
		$companyArr[$i][1]=$getCompany->{'companyID'};
		
	}
	return $companyArr;
	
}


function checkIfuserexit()
{
	$homepage = file_get_contents('https://ana10project.firebaseio.com/.json?format=export');
    echo $homepage;
}
// check if the HTTP_REFERER is allow
if($_SERVER["HTTP_REFERER"]=="http://ovedli.orgfree.com/")
 {

	//creat the code
	if($_GET["act"]=="getcode")
	{
		//creat code by using time formula
		$code=date("Y")*1;
		$code+=date("m")*1;
		$code+=date("d")*1;
		$code+=date("h")*1;
		$code+=date("i")*1;
		echo intval($code); 

	}

	//check if the code make sence
	if(isset($_GET["code"]))
	{
		$_GET["code"]=trim($_GET["code"]);
		$checkVaildCode=date("Y")*1;
		$checkVaildCode+=date("m")*1;
		$checkVaildCode+=date("d")*1;
		$checkVaildCode+=date("h")*1;
		$checkVaildCode+=intval(date("i"))*1;
		if(($checkVaildCode-($_GET["code"]*1))<=10)
		{
			$codeIsVaild=true;
		}
	}

	
	
	//stats for user
		function statsforuser()
	{
		$money=0;
		$callsuntilnow=0;
		$callclosed=0;
		$callopen=0;
		$getthecallforranking = file_get_contents('https://ana10project.firebaseio.com/calls/callsmone/.json?format=export');
		$getthecallforrankingjson = json_decode($getthecallforranking);
		$mone=intval($getthecallforrankingjson);
		for($i=1;$i<$mone+1;$i++)
		{
			$getthecall = file_get_contents('https://ana10project.firebaseio.com/calls/'.$i.'.json?format=export');
			$getthecall = json_decode($getthecall);
			if($getthecall!=null&& $_SESSION["username"]==$getthecall->{'emailofuser'})
				{
					$callsuntilnow++;
					if($getthecall->{'issolved'}==0)
						{
							$callopen++;
						}
						else
						{
							$callclosed++;
							$moneyoftheservice = file_get_contents('https://ana10project.firebaseio.com/services/'.$getthecall->{'serviceid'}.'.json?format=export');
						$moneyoftheservice = json_decode($moneyoftheservice);
						$money=$money+intval($moneyoftheservice->{'price'});
						}
						
						
				}
			
		}
			echo "<br> <b>סטטיסטיקת משתמש</b><br><b>כמות קריאות: </b>".$callsuntilnow."<br><b>כמות קריאות פתוחות: </b>".$callopen."<br><b>כמות קריאות סגורות: </b>".$callclosed."<br><b>תשלום ששולם עד כה: </b>".$money.' ₪';
	}

	
	//check if the user is verifed
	if(isset($_GET["userauth"])&& $codeIsVaild==true &&!isset($_GET["addcall"]) )
	{
		$splitinforUserAuth=$_GET["userauth"];		
		$splitinforUserAuth=urldecode($splitinforUserAuth);
		$splitinforUserAuth=explode(",", $splitinforUserAuth);
		//$splitinforUserAuth[0] // email
		//$splitinforUserAuth[1] // valdition
		//creat ne session with php 
		$_SESSION["username"]=$splitinforUserAuth[0];
		$_SESSION["verified"]=$splitinforUserAuth[1];
		echo '  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css">';		
	echo '<style>table {
  border-collapse: collapse;
  border-spacing: 0;
  width: 100%;
  border: 1px solid #ddd;
}</style>
';
		echo '<table class="table" style="overflow-x:auto;">';
		echo ' <tr>';
		if (($splitinforUserAuth[1])== "false")
		{
			echo '<td>';
			echo '<div class="panel panel-success"><div class="panel-heading"><center>';
			echo 'ברוך הבא : ' . $splitinforUserAuth[0] .'</center></div>
      <div class="panel-body"><center>מיל מאומת :<br/>לא <br/><br/><button id="send_verification" style="width:20%;" onclick="send_verification()">אמת חשבון</button><br/><br/><button type="submit" class="cancelbtn" style="width:20%;" id="signOutBtn" onclick="signOutBtn()">יציאה</button><br/></center></div>
    </div></td></tr>';
	echo 'בס"ד';
			
			
		}
		else
		{
			echo '<td>';
			echo '<div class="panel panel-success"><div class="panel-heading">';
			echo '<center> ברוך הבא : ' .$splitinforUserAuth[0]. '</center></div><class="panel-body"><center><br/>מיל מאומת :<br/>כן <br/><br/> <button type="submit" class="cancelbtn" id="signOutBtn" onclick="signOutBtn()">יציאה</button><br/>';
			statsforuser();
			echo '<br/></center></div></div>';
			
			/*after code here */
			
			echo '<tr><td>';
			echo '<div class="panel panel-success"><div class="panel-heading"> פתח קריאה';
			echo '</div> <div class="panel-body">';
			echo "<br/><select name='selectCompany' id='selectCompany'>";
			$companyarray=getAllTheCompnies();
			echo '<option value="-1">בחר חברה    </option>';
			for($i=0;$i<=count($companyarray);$i++)
			{
				if($companyarray[$i][0]!="")
				{
					echo '<option value="'.$companyarray[$i][1].'">'.$companyarray[$i][0].'</option>';
				}
			}
							 
			echo "</select>";
			echo "<div id='openCall'></div>";
			echo "<br/><button type='submit' id='openCallBtn' onclick='openCallBtn()'>פתח קריאה</button>";
			
			echo '</div></div></td></tr>';
			
			echo "<br/>בס''ד";
			
		}
		
			
			
			
			/*after code here */
			
			
			
			
			echo '</td>';
			
			//echo '</tr>';
			echo '<td><div class="panel panel-success"><div class="panel-heading">הקריאות שלי</div><div class="panel-body">';
			echo '<div id="mycallslist">';
			
			echo '<table class="table">
    <thead>
      <tr>
        <th style="text-align: right;">קריאה</th>

      </tr>
    </thead>
    <tbody>
      <tr>';
	  
			$getmycallscounter = file_get_contents('https://ana10project.firebaseio.com/calls/callsmone/.json?format=export');
			$getmycallscounter=json_decode($getmycallscounter);
			for($i=1;$i<=$getmycallscounter+1;$i++)
				{
					$getmycalls = file_get_contents('https://ana10project.firebaseio.com/calls/'.$i.'/.json?format=export');
					$getmycalls = json_decode($getmycalls);
					
					if($getmycalls->{'emailofuser'}==$_SESSION["username"])
						{
							echo "<td> <b>תאריך פתיחה : </b>".$getmycalls->{'date'}."";
							echo " <b> הגעה צפויה : </b> ".$getmycalls->{'arrivedate'}." ";
							echo "<b> פירוט : </b> ".$getmycalls->{'description'}." ";
							if($getmycalls->{'issolved'}==0)
								{
									echo "<b> האם נפתר :</b> ";
									echo " לא ";
								}
								else
								{
									
									echo "<b> האם נפתר : </b>  כן ";
									echo '<b> דירוג :  </b> <select name="rankthetech" ONCHANGE="swapContent(this)">';

									for($j=1;$j<6;$j++)
									{
										if($getmycalls->{'rank'}==$j)
											{
												echo '<option value="'.$i.'-'.$j.'" selected>'.$j.'</option>';
											}else
											{
												echo '<option value="'.$i.'-'.$j.'">'.$j.'</option>';
											}
									}
									echo "</select>";
									
									
									
								}
								echo "<br><b> שם החברה : </b>";
								//the name iof the company
								
								
								$grabTheCompanyId = file_get_contents('https://ana10project.firebaseio.com/services/'.$getmycalls->{'serviceid'}.'/.json?format=export');
								$grabTheCompanyId = json_decode($grabTheCompanyId);
								if($grabTheCompanyId->{'companyID'}!=null)
									{
										$grabTheCompanyId12 = file_get_contents('https://ana10project.firebaseio.com/company/'.$grabTheCompanyId->{'companyID'}.'/.json?format=export');
										$grabTheCompanyId12 = json_decode($grabTheCompanyId12);
										echo $grabTheCompanyId12->{'companyName'};
									}
									else
									{
										echo "";
									}
								
								echo "<b> שם השירות : </b> ";
								$grabTheCompanyId = file_get_contents('https://ana10project.firebaseio.com/services/'.$getmycalls->{'serviceid'}.'/.json?format=export');
								$grabTheCompanyId = json_decode($grabTheCompanyId);
								echo $grabTheCompanyId->{'name'};
								echo "<b> מחיר : </b> ";
								echo $grabTheCompanyId->{'price'}.' ₪';
								
								
								//end the name of the company
									
									echo "</td>";
							echo "</tr>";
						}
					
				}
				
        echo'
		
		</tr>

    </tbody>
  </table>
';
			
			
			
			echo '</div>';
			echo '</tr><tr>';
			/* code  here */
		
		echo '</tr>';
		echo '</table>';
		

	}
	
	//return the services by companyid
	if($codeIsVaild==true &&$_GET["addcall"]=="getServices"&& isset($_GET["companyid"]) )
	{

		echo "<label for='chooseservice'>בחר שירות :</label><select style='width:100%;'  class='form-control' name='chooseservice' id='chooseservice'>";
		$getServices = file_get_contents('https://ana10project.firebaseio.com/services/moneServices/.json?format=export');
		$servicesCounter=$getServices;
		for($i=1;$i<=$servicesCounter;$i++)
	{
		$getCompanyForService = file_get_contents('https://ana10project.firebaseio.com/services/'.$i.'/.json?format=export');
		$getCompanyForService = json_decode($getCompanyForService);
		if($getCompanyForService->{'companyID'}==$_GET["companyid"])
			{
				echo '<option value="'.$i.'">'.$getCompanyForService->{'name'}.' - מחיר: '. $getCompanyForService->{'price'}.' ₪</option>';
			}
		
		
	}
	echo "</select><br>";
	//chose date
	
echo '<style>input[type=date] {
    width: 100%;
    padding: 12px 20px;
    margin: 8px 0;
    display: inline-block;
    border: 1px solid #ccc;
    box-sizing: border-box;
}</style>';
	echo'<p>Date: <input type="date" id="datepicker"  required pattern="[0-9]{2}.[0-9]{2}.[0-9]{2}" style="with:100%;"></p>';
	
	//choose round
	echo "<label for='chooseround'>בחר סבב :</label><select style='width:100%;'  class='form-control' name='chooseround' id='chooseround'>";
		$getServices = file_get_contents('https://ana10project.firebaseio.com/rounds/mone/.json?format=export');
		$servicesCounter=$getServices;
		for($i=1;$i<=$servicesCounter;$i++)
	{
		$getCompanyForService = file_get_contents('https://ana10project.firebaseio.com/rounds/'.$i.'/.json?format=export');
		$getCompanyForService = json_decode($getCompanyForService);

				echo '<option value="'.$i.'">'.$getCompanyForService->{'name'}.'</option>';
			
		
		
	}
	echo "</select><br>";
	
	
	
	}
	
	//return the adress
	function checkIfAdressIsVaild($adress)
	{
		$adress=urlencode($adress);
		 $url='https://api.tomtom.com/search/2/geocode/'.$adress.'.xml?key=VjsKK4fEUVclIS5slDjs3okAKwrJcOSf&language=he-IL';
		 $thereso = file_get_contents($url);
		 $thereso = strstr( $thereso, 'totalResults' );
		 $thereso = strstr( $thereso, '</totalResults>',true );
		 $thereso =str_replace("totalResults>","",$thereso);
		 $thereso =intval($thereso);
		 
		 
		 
		 return $thereso;
        
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

function checkIfNameAndLastNameAreVaild($str1,$str2)
{
	$flag=0;
	if(preg_match('~[0-9]~', $str1))
	{
		$flag++;
	}
		if(preg_match('~[0-9]~', $str2))
	{
		$flag++;
	}
	if(preg_match('/[\'^£$%&*()}{@#~?><>,|=_+¬-]/', $str1))
		{
			$flag++;
		}
	if(preg_match('/[\'^£$%&*()}{@#~?><>,|=_+¬-]/', $str2))
		{
			$flag++;
		}
	if($flag==0)
	{
		return 1;
	}		
	else
	{
		return 0;
	}
	
}	

function CheckTelephone($str)
{
	 if (ctype_digit($str)) {
		 return 1;
	 }
	 else
	 {
		 return 0;
	 }
}

function checkIfServiceExite($str)
{
	$str=intval($str);
	if($str)
	{
		$getCompanyForServiceForOpenCall = file_get_contents('https://ana10project.firebaseio.com/services/'.$str.'/.json?format=export');
		$getCompanyForServiceForOpenCall = json_decode($getCompanyForServiceForOpenCall);
		if($getCompanyForServiceForOpenCall!=null)
		{
			return 1;
		}
		else{
			return 0;
		}
	}
}
//add time function
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
function retrunTheTimeBetweenTwoPoints($str1,$str2)
{
	$adress=urlencode($str1).":".urlencode($str2);
		 $url='https://api.tomtom.com/routing/1/calculateRoute/'.$adress.'?key=VjsKK4fEUVclIS5slDjs3okAKwrJcOSf';
		 $thereso = file_get_contents($url);
		 $thereso = strstr( $thereso, '<travelTimeInSeconds>' );
		 $thereso = strstr( $thereso, '</travelTimeInSeconds>',true );
		 $thereso =str_replace("<travelTimeInSeconds>","",$thereso);
		 $thereso =intval($thereso);
		 
		 
		 
		 return intval($thereso/60);
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

function returnAllTheTechByCompnayID($str)
{
	$employeeArray="";
	$indicatorForEmploee=0;
	$grabTheCompanyId = file_get_contents('https://ana10project.firebaseio.com/company/'.$str.'/.json?format=export');
	$grabTheCompanyId = json_decode($grabTheCompanyId);
	if($grabTheCompanyId!=null)
	{
		$getTheNumberOfTheAllEmplyes=file_get_contents('https://ana10project.firebaseio.com/employees/moneemployees/.json?format=export');
		$getTheNumberOfTheAllEmplyes=intval($getTheNumberOfTheAllEmplyes);
		for($i=1;$i<$getTheNumberOfTheAllEmplyes+1;$i++)
		{
			$tempEmploee=file_get_contents('https://ana10project.firebaseio.com/employees/'.$i.'/.json?format=export');
			$tempEmploee=json_decode($tempEmploee);
			if($tempEmploee->{'companyid'}==$str)
				{
					$employeeArray[$indicatorForEmploee]=$i;
					$indicatorForEmploee++;
					
				}
		}
		return  $employeeArray; 
	}
	else
	{
		return null;
	}
}

function BestTechFinder($teachArray,$date)
{
	if($teachArray!=null)
	{
		if (!is_array($teachArray))
		{
			$teachArray=array($teachArray);
		}
		//creat the date of tommorow
		$date=date("d.m.y");
		$date=date("d.m.y",strtotime($date . "+1 days"));
		
		//set up the time a float var to do math opretions
		$mintemp=returnTheTimeOfTheTech($teachArray[0],$date);
		if($mintemp==0)
		{
			$mintemp='08:00';
		}
		$mintemp=explode(":",$mintemp);
		$min=intval($mintemp[0]).".".$mintemp[1];
		$min=floatval($min);
		$themintech=$teachArray[0];
		foreach($teachArray as $value)
		{
			$mintemp=returnTheTimeOfTheTech($value,$date);
			$mintemp=explode(":",$mintemp);
			$mintemp=intval($mintemp[0]).".".$mintemp[1];
			$mintemp=floatval($min);
			if($mintemp<$min)
			{
				$themintech=$value;
				$min=$mintemp;
			}
			
		}
		
		return $themintech;
	}
	return null;
}
function checkwichroundsareaviable($roundid,$date,$serviceid,$gelocation)
{
	
	$teacharry="";
	$theround = file_get_contents('https://ana10project.firebaseio.com/rounds/'.$roundid.'/.json?format=export');
	$theround = json_decode($theround);
	if($theround==null)
	{
		return 0;
	}
	$theround=explode("-", $theround->{'name'});
	$theround[0]=intval($theround[0]);
	$theround[1]=intval($theround[1]);
	$teacharry=returnAllTheTechByCompnayID(GetTheCompanyByServiceId($serviceid));
	$teachArray=$teacharry;
	if($teacharry=="" || $teacharry==null)
	{
						
		return 0;
	}
		
	if (!is_array($teacharry))
	{
		$teacharry=array($teacharry);
	}
	
	$flag=0;

	for($i=0;$i<count($teacharry);$i++)
	{
		if($flag==0){
			$mintemp=returnTheTimeOfTheTech($teachArray[$i],$date);
			
			if($mintemp==0)
			{
				$mintemp='08:00';
			}	
			$mintemp=explode(":",$mintemp);
			if(intval($mintemp[0])>=intval($theround[0])&&intval($mintemp[0])<intval($theround[1]))
			{
				$min=intval($mintemp[0]).".".$mintemp[1];
				$min=floatval($min);
				$themintech=$teachArray[$i];
				$flag++;
	
			}
		}
	}
	
	
	for($j=$i;$j<count($teacharry);$j++)
	{
		$posttechid=$teacharry[$j];
		$postdatearrive=returnTheTimeOfTheTech($posttechid,$date);
		$postdatearrive=addTheTime($postdatearrive,retrunTheTimeBetweenTwoPoints(getTheGpsLocationByAdress(returnTheLastAdressOfTheTeach($posttechid,$date)),getTheGpsLocationByAdress($gelocation)));
		$postdatearrive=$date." ".$postdatearrive;
		$mintemp=$postdatearrive;
		$mintemp=explode(":",$mintemp);
		$mintemp=intval($mintemp[0]).".".$mintemp[1];
		$mintemp=floatval($min);
		if($mintemp<$min && intval($mintemp[0])>=intval($theround[0])&&intval($mintemp[0])<intval($theround[1]) )
			{
				$themintech=$teacharry[$j];
				$min=$mintemp;
			}
	}
	
					
					
					
	//strat best of the techid
	//set up the time a float var to do math opretions
		
		if($themintech=="" || $themintech==null)
		{
			$themintech=0;
		}
		return $themintech;
	//end the best of the tech id	
	
}
function returnTheLastAdressOfTheTeach($techid,$date)
{
	$theLastAdress="";
	$thecalls = file_get_contents('https://ana10project.firebaseio.com/calls/callsmone/.json?format=export');
	$thecalls = json_decode($thecalls);
	for($i=1;$i<intval($thecalls)+1;$i++)
	{
		$thecallsof = file_get_contents('https://ana10project.firebaseio.com/calls/'.$i.'/.json?format=export');
		$thecallsof = json_decode($thecallsof);
		$thedate=explode(" ",$thecallsof->{'arrivedate'});
		if($thedate[0]==$date && $techid==$thecallsof->{'techid'})
			{
				$theLastAdress=$thecallsof->{'geolocation'};
			}
	}
	return $theLastAdress;
}

//
	//insert new call
	
	//if($codeIsVaild==true&&$_GET["addcall"]=="opencall")
	//{
		if($codeIsVaild==true && $_GET["act"]=="sendopencall"){
			$errorListOfOpenCall="";
			$errorListOfOpenCallCounter=0;
			if(isset($_GET["adress"])&&checkIfAdressIsVaild($_GET["adress"])>0 && checkIfAdressIsVaild($_GET["adress"])<10)
				{
					//echo "yes";
			
				}
				else
				{
					$errorListOfOpenCall.=" לא קיימת כתובת כזאת";
					$errorListOfOpenCall.="<br> ";
					$errorListOfOpenCallCounter++;
				}
				
				if(ValidateID($_GET["id"])==1)
				{
					//echo " yes - id";
				}
				else
				{
					$errorListOfOpenCall.=" תעודת זהות אינה תקינה <br> ";
					$errorListOfOpenCall.="<br> ";
					$errorListOfOpenCallCounter++;
				}
				
				//check the name if vaild
				if(checkIfNameAndLastNameAreVaild($_GET["fname"],$_GET["lname"])==1)
				{
					//echo "yes";
				}
				else
				{
					$errorListOfOpenCall.=" שם אינו תקין <br> ";
					$errorListOfOpenCall.="<br> ";
					$errorListOfOpenCallCounter++;
				}
				if(CheckTelephone($_GET["phone"])==1)
				{
				}
				else
				{
					$errorListOfOpenCall.=" מספר הטלפון שגוי  ספרות בלבד<br> ";
					$errorListOfOpenCall.="<br> ";
					$errorListOfOpenCallCounter++;
				}
				if(checkIfServiceExite($_GET["serviceid"])==1)
				{
				}
				else
				{
					$errorListOfOpenCall.=" שירות לא קיים <br> ";
					$errorListOfOpenCall.="<br> ";
					$errorListOfOpenCallCounter++;
				}
				if($_GET["datepicker"]!="")
				{
					$datepickersplit=$_GET["datepicker"];
					$datepickersplit=explode("-", $datepickersplit);
					//($datepickersplit);
					/*check if the date is garther than today
					*/
					  $date_now = date("Y-m-d"); // this format is string comparable
						if ($date_now > ''.$datepickersplit[0].'-'.$datepickersplit[1].'-'.$datepickersplit[2].'') {
							$errorListOfOpenCall.=" אנא הכנס תאריך מאוחר מהיום <br> ";
						$errorListOfOpenCall.="<br> ";
						$errorListOfOpenCallCounter++;
						}else{
						
					}
						
						//correct bug for cheking for today
						$datetodaycheck=date("d.m.Y");
						$dateforcheckcheck=date(($datepickersplit[2].".".$datepickersplit[1].".".$datepickersplit[0].""));
					if($datetodaycheck==$dateforcheckcheck)
					{
						$errorListOfOpenCall.=" אנא הכנס תאריך מאוחר מהיום <br> ";
						$errorListOfOpenCall.="<br> ";
						$errorListOfOpenCallCounter++;
					}
					//end of the date checking about today
					//check if the input is vaild
					if(!is_numeric($datepickersplit[2]) || !is_numeric($datepickersplit[1]) || !is_numeric($datepickersplit[0]))
					{
						$errorListOfOpenCall.=" אנא הכנס תאריך תקין <br> ";
						$errorListOfOpenCall.="<br> ";
						$errorListOfOpenCallCounter++;
					}
					if(count($datepickersplit)>3 || count($datepickersplit)<3)
					{
						$errorListOfOpenCall.=" אנא הכנס תאריך תקין <br> ";
						$errorListOfOpenCall.="<br> ";
						$errorListOfOpenCallCounter++;
					}
					else{
							$datepickernosplit=$datepickersplit[2].".".$datepickersplit[1].".".((intval($datepickersplit[0]))-2000)."";
					}
				}
				else
				{
						$errorListOfOpenCall.=" אנא הכנס תאריך תקין <br> ";
						$errorListOfOpenCall.="<br> ";
						$errorListOfOpenCallCounter++;
				}
				if($errorListOfOpenCallCounter==0)
				{
					
					$roundid=$_GET["chooseround"];
					$postID=$_GET["id"];
					$Postfullname=$_GET["fname"]." ".$_GET["lname"];
					$postPhone=$_GET["phone"];
					$postgeolocation=$_GET["adress"];
					$postserviceid=$_GET["serviceid"];
					$postdateopencall=date("d.m.y");
					//$postdatetommorow=date("d.m.y",strtotime($date . "+1 days"));
					$postdatetommorow=$datepickernosplit;
					$postDescription=$_GET["comment"];
					$postemailofuser=$_SESSION["username"];
					$postissolved=0;
					$postRank=5;
					$posttechid=checkwichroundsareaviable($roundid,$postdatetommorow,$postserviceid,$postgeolocation);
					$posttechid1=returnAllTheTechByCompnayID(GetTheCompanyByServiceId($postserviceid));
					if($posttechid1==""|| $posttechid1==null)
					{
						echo "סליחה - חברה זו אינה מספקת שירותי טכנאים כרגע";
						return "סליחה - חברה זו אינה מספקת שירותי טכנאים כרגע";
					}
					
					if($posttechid==0 )
					{
						echo "סבב לא פנוי אנא בחר סבב אחר";
						return "סבב לא פנוי אנא בחר סבב אחר";
					}
					
					$postdatearrive=returnTheTimeOfTheTech($posttechid,$postdatetommorow);
					$postdatearrive=addTheTime($postdatearrive,retrunTheTimeBetweenTwoPoints(getTheGpsLocationByAdress(returnTheLastAdressOfTheTeach($posttechid,$postdatetommorow)),getTheGpsLocationByAdress($postgeolocation)));
					$postdatearrive=$postdatetommorow." ".$postdatearrive;
					$monecallsPost = file_get_contents('https://ana10project.firebaseio.com/calls/callsmone/.json?format=export');
					$monecallsPost = json_decode($monecallsPost);
					$monecallsPost=intval($monecallsPost);
					$monecallsPost++;
					
					$data = '{"id": "'.$postID.'","fullname": "'.$Postfullname.'","phone": "'.$postPhone.'","geolocation": "'.$postgeolocation.'","serviceid": "'.$postserviceid.'","date": "'.$postdateopencall.'","description": "'.$postDescription.'","emailofuser": "'.$postemailofuser.'","issolved": "'.$postissolved.'","rank": "'.$postRank.'","techid": "'.$posttechid.'","arrivedate": "'.$postdatearrive.'"}';

					$url = "https://ana10project.firebaseio.com/calls/".$monecallsPost."/.json";
					$ch = curl_init();
					curl_setopt($ch, CURLOPT_URL, $url);     
					curl_setopt($ch, CURLOPT_CUSTOMREQUEST, 'PATCH');	
					curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
					//curl_setopt($ch, CURLOPT_POST, 1);
					curl_setopt($ch, CURLOPT_POSTFIELDS, $data);
					curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-Type: text/plain'));
					$jsonResponse = curl_exec($ch);
					//($jsonResponse);
					if(curl_errno($ch))
					{
						echo 'Curl error: ' . curl_error($ch);
					}
					curl_close($ch);
					
					
					
					//update 
				$data = '{"callsmone": "'.$monecallsPost.'"}';
				$url = "https://ana10project.firebaseio.com/calls/.json";
					$ch = curl_init();
					curl_setopt($ch, CURLOPT_URL, $url);     
					curl_setopt($ch, CURLOPT_CUSTOMREQUEST, 'PATCH');	
					curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
					//curl_setopt($ch, CURLOPT_POST, 1);
					curl_setopt($ch, CURLOPT_POSTFIELDS, $data);
					curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-Type: text/plain'));
					$jsonResponse = curl_exec($ch);
					//($jsonResponse);
					if(curl_errno($ch))
					{
						echo 'Curl error: ' . curl_error($ch);
					}
					curl_close($ch);
					echo 1;
				}
				else{
					echo $errorListOfOpenCall;
				}
				
		}
		
		
		
	//}
	
	
	//rank the employ
	if($codeIsVaild==true && $_GET["act"]=="rankthetech")
	{
		$rankTheTech=$_GET["therank"];
		$rankTheTech = explode("-", $rankTheTech);
		//$rankTheTech[0];- the id of the calll
		//$rankTheTech[1];- the rank value
		$getthecallforranking = file_get_contents('https://ana10project.firebaseio.com/calls/'.$rankTheTech[0].'/.json?format=export');
		$getthecallforrankingjson = json_decode($getthecallforranking);
		//($getthecallforrankingjson->{'emailofuser'});
		if($getthecallforranking!=null&& intval($rankTheTech[1])&&$_SESSION["username"]==$getthecallforrankingjson->{'emailofuser'})
		{
				if($rankTheTech[1]<1)
				{
					$rankTheTech[1]=1;
				}
				if($rankTheTech[1]>5)
				{
					$rankTheTech[1]=5;
				}
				//echo $rankTheTech[1];
				/*
				$myfile = fopen("newfile.html", "w") or die("Unable to open file!");
				$txt = '<html><head><script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script></head><script>';
				$txt =$txt . 'var data = {"rank": "'.$rankTheTech[1].'"};';
				$txt =$txt . 'jQuery.ajax({ accept: "application/json",';
				$txt =$txt . "type: 'PATCH',";
				$txt =$txt . ' contentType: "application/json; charset=utf-8",
						dataType: "json",
						url: "https://ana10project.firebaseio.com/calls/'.$rankTheTech[0].'/.json",
						data: JSON.stringify(data),
					});

					</script></html>';
				fwrite($myfile, $txt);
				fclose($myfile);
*/
				//update in firebase
				$data = '{"rank": "'.$rankTheTech[1].'"}';

				$url = "https://ana10project.firebaseio.com/calls/".$rankTheTech[0]."/.json";
				$ch = curl_init();
				curl_setopt($ch, CURLOPT_URL, $url);     
				curl_setopt($ch, CURLOPT_CUSTOMREQUEST, 'PATCH');	
				curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
				//curl_setopt($ch, CURLOPT_POST, 1);
				curl_setopt($ch, CURLOPT_POSTFIELDS, $data);
				curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-Type: text/plain'));
				$jsonResponse = curl_exec($ch);
				//($jsonResponse);
				if(curl_errno($ch))
				{
					echo 'Curl error: ' . curl_error($ch);
				}
				curl_close($ch);
				//end update for firebase
			 
			
				//clear the html 
				/*
				$myfile = fopen("newfile.html", "w") or die("Unable to open file!");
				$txt = "";
				fwrite($myfile, $txt);
				fclose($myfile);
				*/
				//echo "העדכון בוצע!";
				
		}
		
	}



	
	


	
	


	//destroy all the session logout
	if($_GET["logoout"]=="yes"&& $codeIsVaild==true)
	{
		session_destroy(); 

	}
	
 }

?>		