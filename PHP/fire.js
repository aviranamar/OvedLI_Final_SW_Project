//check if post
var code="";
   $.post("func.php?act=getcode",
    {
      
    },
    function(data,status){
      code=parseInt(data);
    });
//end check if post

function swapContent(that) {
	$.get("func.php",
    {
		code: code,
		act: "rankthetech",
		therank: that.value
	},
	function(data,status)
	{
		//document.getElementById("urlinject").innerHTML="<iframe src='read.php' style='width:0;height:0;border:0; border:none;'></iframe>";
		alert("בוצע בהצלחה!");
	});
}

function openCallNow()
{ 
	var dateimport=$( "#datepicker" ).val();
	  dateimport = dateimport.replace("/", ".");
	  dateimport = dateimport.replace("/", ".");

	  $.get("func.php?act=sendopencall",
    {
      code: code,
	  id:$( "#id" ).val(),
	  fname:$( "#fname" ).val(),
	  lname:$( "#lname" ).val(),
	  adress:$( "#adress" ).val(),
	  phone:$( "#phone" ).val(),
	  datepicker:dateimport,
	  chooseround:$( "#chooseround" ).val(),
	  serviceid:$( "#chooseservice" ).val(),
	  comment:$( "#comment" ).val(),
	  beforeSend: function () {
        document.getElementById("secopencall").innerHTML="<center><img src='https://thumbs.gfycat.com/AcceptablePerfumedIraniangroundjay-max-1mb.gif' height='20%' width='20%'></center>"; 
    }

    },
	
    function(data,status){
		if(data==1)
		{
			alert('בוצע בהצלחה');
			 location.reload();
		}
		else
		{
			'<font size="3" color="red">יש לבחור חברה קודם!</font>';
			document.getElementById("error").innerHTML='<font size="3" color="red">'+data+'</font>';
			 document.getElementById("secopencall").innerHTML="<div id='secopencall'><button type='submit' value='Submit' onclick='openCallNow()'> פתח קריאה</button>"; 
		}
	  
    });
	 
	 

	}


//open the form of the new call
function openCallBtn()
{
	if($( "#selectCompany" ).val()!=-1){
	 document.getElementById("openCall").innerHTML="<br><div id='error'></div><input type='text' name='id' id='id' placeholder='ת.ז'><br><input type='text' name='fname' id='fname' placeholder='שם פרטי'><br><input type='text' name='lastname' id='lname' placeholder='שם משפחה'><br><input type='text' id='adress' name='adress' placeholder='כתובת'><br><input type='text' id='phone' name='phone' placeholder='טלפון'><input type='hidden'  name='companyid' value="+$( "#selectCompany" ).val()+">";
	 var compid=$( "#selectCompany" ).val();
	  $.get("func.php",
    {
      code: code,
	  addcall: "getServices",
	  companyid:compid,
	  beforeSend: function () {
        document.getElementById("openCall").innerHTML+="<div id='loader'><center><img src='https://thumbs.gfycat.com/AcceptablePerfumedIraniangroundjay-max-1mb.gif' height='20%' width='20%'></center></div>"; 
    }
	  
    },
	
    function(data,status){
		$("#loader").remove();
      document.getElementById("openCall").innerHTML+=data;
	  document.getElementById("openCall").innerHTML+='<label for="comment">פירוט התקלה:</label><textarea class="form-control" rows="3" name="comment" id="comment" placeholder="לא חובה"></textarea><br>';
	  document.getElementById("openCall").innerHTML+="<br><div id='secopencall'><button type='submit' value='Submit' onclick='openCallNow()'> פתח קריאה</button></div></form>";
	  
    });
	 
	 
	   $("#openCallBtn").css('visibility','hidden');
	   $("#selectCompany").css('visibility','hidden');
	}
	else
	{
		 document.getElementById("openCall").innerHTML='<font size="3" color="red">יש לבחור חברה קודם!</font>';
	}
	  
  
  
  
  

}

//CHECK IF THE CODE IS VAILD

firebase.auth().onAuthStateChanged(function(user) {
  if (user) {
    // User is signed in.
    document.getElementById("loginDialog").style.display = "none";
    document.getElementById("inside").style.display = "block";
    var user = firebase.auth().currentUser;
    if(user != null){
            var email = user.email;
            var verified = user.emailVerified;
			var emailver=email+","+verified;
			$.get("func.php",
			{
				code: code,
				userauth: emailver,
				beforeSend: function () {
        document.getElementById("inside").innerHTML="<div id='loaderopen'><center><b> אנא המתן </b><br><img src='https://thumbs.gfycat.com/AcceptablePerfumedIraniangroundjay-max-1mb.gif' height='20%' width='20%'></center></div>"; 
    }
			},
			function(data,status){
				$("#loaderopen").remove();
			  document.getElementById("inside").innerHTML=data;
			});
			
            
            
    }
    }
    else {
    document.getElementById("loginDialog").style.display = "block";
    document.getElementById("inside").style.display = "none";
  }
});


/* SIGNUP PROCESS */
$("#signupBtn").click(
  function(){
    var email = $("#loginEmail").val();
    var password = $("#loginPassword").val();
    if(email != "" && password != ""){
firebase.auth().createUserWithEmailAndPassword(email, password).then(function(user){
        $("#loginError").show().text("הרישום עבר בהצלחה");
        //you can save the user data here.
    }).catch(function(error) {
        // Handle Errors here.
        var errorCode = error.code;
        var errorMessage = error.message;
        if(errorCode == "auth/weak-password"){$("#loginError").show().text("הסיסמה צריכה להיות לפחות 6 תווים");}
        if(errorCode == "auth/invalid-email"){$("#loginError").show().text("נא ציין כתובת מיל חוקית");}
        if(errorCode == "auth/email-already-in-use"){$("#loginError").show().text("כתובת המיל כבר בשימוש");}
        //$("#loginError").show().text(errorMessage);
      });
    }
  }
);

/* LOGIN PROCESS */
$("#loginBtn").click(
  function(){
    var email = $("#loginEmail").val();
    var password = $("#loginPassword").val();
    if(email != "" && password != ""){
firebase.auth().signInWithEmailAndPassword(email, password).then(function(user){
        $("#loginError").show().text("התחברת בהצלחה");
        //you can save the user data here.
    }).catch(function(error) {        
        // Handle Errors here.
        var errorCode = error.code;
        var errorMessage = error.message;
        if(errorCode == "auth/wrong-password"){$("#loginError").show().text("סיסמה לא נכונה");}
        if(errorCode == "auth/user-not-found"){$("#loginError").show().text("כתובת המיל לא בשימוש");}
        if(errorCode == "auth/user-disabled"){$("#loginError").show().text("המשתמש הוקפא , פנה למנהל האחראי");}
        //$("#loginError").show().text(errorMessage);
      });
    }
  }
);

/* LOGOUT PROCESS */

//$("#signOutBtn").click(
  function signOutBtn(){

	$.get("func.php",
			{
				code: code,
				logoout: "yes"
			},
			function(data,status){
			  document.getElementById("inside").innerHTML='<p id="text"></p>';
			});
    firebase.auth().signOut().then(function() {
     
      // Sign-out successful.
    }).catch(function(error) {
      // An error happened.
      alert(error.message);
    });

  }
//);

//end of the signoutgooglebutten
 


//$("#send_verification").click(
function send_verification(){
        var user = firebase.auth().currentUser;
        
        user.sendEmailVerification().then(function() {
        // Email sent.
        window.alert("מיל נשלח בהצלחה")
        }).catch(function(error) {
        // An error happened.
        window.alert(error.message)
        });
}
//)

$("#password_reset").click(
function(){
var auth = firebase.auth();
var emailAddress = $("#loginEmail").val();

auth.sendPasswordResetEmail(emailAddress).then(function() {
  // Email sent.
  window.alert("מיל נשלח בהצלחה")
}).catch(function(error) {
  // An error happened.
  window.alert(error.message)
});
}
)