using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using RestSharp;

namespace OvedLi
{
    public class function
    {
        //IFirebaseConfig config;
        //IFirebaseClient client1;
        string projectbase;
        int internet_is_connected = 0;
        //FirebaseResponse response;
        /*public void configfirebase()
        {
            this.config = new FirebaseConfig
            {
                AuthSecret = "",
                BasePath = "https://ana10project.firebaseio.com/"
            };
            //this.client= new FirebaseClient(this.config);

        }
        */



        //check if the internet is connected
        public bool CheckForInternetConnection()
        {
            bool res = false;
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    this.internet_is_connected = 1;
                    res = true;
                }
            }
            catch
            {
                this.internet_is_connected = 0;
                res = false;
                
            }
            return res;
        }



        //cut the string
        public string CutME(string start, string end, string thestring)
        {
            int indexstart = 0;
            int indexend = 0;
            string newone = "";
            string newonefor = "";
            //check if the word exits
            if (thestring.IndexOf(start) == -1)
            {
                return "-1";
            }
            if (thestring.IndexOf(end) == -1)
            {
                return "-1";
            }

            indexstart = thestring.IndexOf(start);
            for (int i = indexstart; i < thestring.Length; i++)
            {
                newonefor += thestring[i];
            }
            indexend = newonefor.IndexOf(end);
            indexstart = newonefor.IndexOf(start);
            
            for (int i = indexstart; i < indexend; i++)
            {
                newone += newonefor[i];
            }
            return newone;
            return "-1";
        }

        //define the base url of the firebase database
        public void define_project_base(string url)
        {
            if (CheckForInternetConnection())
            {
                this.projectbase = url;
            }
            
        }

        public bool CheckSpacialCherter(string str)
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
            foreach (var item in specialChar)
            {
                if (str.Contains(item)) return true;
            }

            return false;
        }

        public bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
        public bool CheckIDNo(String strID)
        {
            int[] id_12_digits = { 1, 2, 1, 2, 1, 2, 1, 2, 1 };
            int count = 0;

            if (strID == null)
                return false;

            strID = strID.PadLeft(9, '0');

            for (int i = 0; i < 9; i++)
            {
                int num = Int32.Parse(strID.Substring(i, 1)) * id_12_digits[i];

                if (num > 9)
                    num = (num / 10) + (num % 10);

                count += num;
            }

            return (count % 10 == 0);
        }

        //strEncryptred 
        public string strEncryptred(string str)
        {
            /*
            var password = "OvedLi";
            string strEncryptred = Cipher.Encrypt(str, password);
            return strEncryptred;
            */
            var responseString = "";
            
                var url = "http://ovedli.orgfree.com/dec.php?string=" + str;
                var request = (HttpWebRequest)WebRequest.Create(url);

                var response = (HttpWebResponse)request.GetResponse();

                responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            
            return responseString;
        }

        //
        /*
        public string strDecrypted(string str)
        {
            var password = "OvedLi";
            string strDecrypted = Cipher.Decrypt(str, password);
            return strDecrypted;
        }

*/
        //get the reuskt from path
        public string get(string path)
        {
            var responseString = "";
            if (CheckForInternetConnection()) { 
            var url = this.projectbase + "/" + path+"/.json";
            var request = (HttpWebRequest)WebRequest.Create(url);

            var response = (HttpWebResponse)request.GetResponse();

            responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            
            }
            return responseString;
        }



        //return the num of the mone
        public int num_of_mone(string path,string monename)
        {
            
            int resint = 0;
            if (CheckForInternetConnection())
            {
                string res = get(path + "/" + monename);
                res = removequat(res)+"";

                resint = int.Parse(res);
            }
            return resint;
        }

        public void commit(string path,string values)
        {
            var client = new RestClient(this.projectbase + "/"+path+"/.json");
            var request = new RestRequest();
            //var strJSONContent = "{\"hit\":\"hit\"}";
            var strJSONContent = values;
            request.Method = Method.PATCH;
            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", strJSONContent, ParameterType.RequestBody);

            var response = client.Execute(request);
        }

        public void del(string path,string mone)
        {
         
            var client = new RestClient(this.projectbase + "/" + path + "/.json");
            var request = new RestRequest();
            //var strJSONContent = "{\"hit\":\"hit\"}";
            var strJSONContent = "";
            request.Method = Method.DELETE;
            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", strJSONContent, ParameterType.RequestBody);

            var response = client.Execute(request);
            char s = '/';
            var depath = path.Split(s);
            path = depath[0] + "";
            int numOfMone = num_of_mone(path, mone);
            if(numOfMone<=1)
            {
                numOfMone = 0;
            }
            else
            {
                numOfMone = numOfMone-1;
            }
            strJSONContent = "{\""+mone+"\":" + numOfMone + "}";
            commit(path, strJSONContent);

        }
    public int removequat(object ob)
        {
            if(ob.ToString()=="null" ||ob==null)
            {
                return 0;
            }
            if (ob.ToString() == "")
            {
                return 0;
            }
            int res = 0;
            string s = ob + "";
            int n;
            bool isNumeric = int.TryParse(s, out n);
            if (isNumeric == false)
            {
                s = s.Replace("\"", "");
                isNumeric = int.TryParse(s, out n);
                res = int.Parse(s);
                return res;
            }
            else
            {
                res = int.Parse(s);
                return res;
            }
        }
        /* //////////////////////////////////////////////////////////////////////////
         * 
         *                                   LOGIN PAGE 
         *                      
         * /////////////////////////////////////////////////////////////////////////
        */

        //check if there is users
        public int check_if_username_and_password_are_ok(string username,string password)
        {
            int res = 0;
            username=username.ToLower();
            WebClient webClient = new WebClient();
            int mone = num_of_mone("users", "usersmone");
            password = strEncryptred(password);
            for(int i=1;i<mone+1;i++)
            {
                if(get("users/" + i) != "" && get("users/"+i)!=null)
                {
                   var json = JsonConvert.DeserializeObject<dynamic>(get("users/" + i));
                    if (json != null) { 
                   if(json.username== username && json.password== password)
                    {
                        res = i;
                    }
                    }
                }
            }



            return res;
        }
        public user retrunuserbyid(int id)
        {
            user user = null;
            int mone = num_of_mone("users", "usersmone");
           
                if (get("users/" + id) != null)
                {

                            user = new user();
                            var json = JsonConvert.DeserializeObject<dynamic>(get("users/" + id));
                if (json != null) { 
                            int compid = removequat(json.companyID);
                            string pass2string = json.password + "";
                            int supus = removequat(json.superuser);
                            int usid = removequat(id);
                            string us = json.username + "";
                            user.setcopmanyID(compid);
                            user.setpassword(pass2string);
                            user.setsuperuser(supus);
                            user.setuserid(usid);
                            user.setusername(us);
                }


            }
            return user;
        }
        //build fille userobject
        public user set_local_user_after_login(int theid)
        {
            user newone=null;
            var json= JsonConvert.DeserializeObject<dynamic>(get("users/" + theid));
            if (json != null) { 
            int cid = json.companyID;
            int sup = json.superuser;
            string uname = json.username;
            string pa = json.password;
            newone = new user(uname, pa, cid, sup, theid);
            }
            return newone;
        }

        //commit user

            public void commitUser(user u)
            {
                var json = JsonConvert.DeserializeObject<dynamic>(get("users/" + u.userid));
                string values = "{\"companyID\":" + u.getcopmanyID() + ",\"password\":\"" + u.password + "\",\"username\":\"" + u.username + "\", \"superuser\":"+u.getsuperuser()+"}";
                string path = "users/" + u.userid;
                commit(path, values);
                if(json==null)
                {
                    int mone = removequat(num_of_mone("users", "usersmone"));
                    mone = mone + 1;
                    values = "{\"usersmone\":" + mone + "}";
                    path = "users/";
                    commit(path, values);
                }
            }

        //commit new/edited user
        /*
        public void comit_User(user u)
        {
            string values = "{\"companyID\":" + removequat(u.copmanyID) + ",\"password\":\"" + u.password + "\",\"superuser\":\"" + removequat(u.getsuperuser()) + "\",\"usersmone\":\"" + removequat(u.getsuperuser()) + "\"}";
            string path = "users/" + u.;
        }
        */

        /*
         * ////////////////////////////////////////////////////////////////////////////////
         * 
         *                      define the copamy
         *                 
         * ////////////////////////////////////////////////////////////////////////////////
         */

        public company set_Company(int id)
        {
            company co = null;
            var json = JsonConvert.DeserializeObject<dynamic>(get("company/" + id));
            if (json != null)
            {
                int compid = json.companyID;
                string comlogo = json.companyLogo;
                string  comname= json.companyName;
                co = new company(compid, comname, comlogo);
            }
            return co;

         }
        public void comit_company(company c)
        {
            string values = "{\"companyID\":"+c.companyID+ ",\"companyLogo\":\"" + c.companyLogo + "\",\"companyName\":\"" + c.companyName + "\"}";
            string path = "company/" + c.companyID;
            commit(path, values);

        }

        public void delcompnay(company c)
        {
            int num_Of_The_employee = num_of_mone("employees", "moneemployees");
            int num_Of_The_calls = num_of_mone("calls", "callsmone");
            int num_of_the_users= num_of_mone("users", "usersmone");
            int num_of_the_services = num_of_mone("services", "moneServices");
            for (int i = 1; i < num_Of_The_employee + 1; i++)
            {

                var json = JsonConvert.DeserializeObject<dynamic>(get("employees/" + i));
                if (json != null) { 
                if (removequat(json.companyid) == removequat(c.companyID))
                {
                        for (int j = 1; j < num_Of_The_calls + 1; j++)
                        {
                            var json2 = JsonConvert.DeserializeObject<dynamic>(get("calls/" + j));
                            if (json2 != null) { 
                            if (removequat(json2.techid) == removequat(i))
                            {
                                del("calls/" + j, "callsmone");
                            }
                        }
                    }
                    del("employees/" + i, "moneemployees");
                }
            }
            }

            for (int i = 0; i < num_of_the_users + 1; i++)
            {
                var json3 = JsonConvert.DeserializeObject<dynamic>(get("users/" + i));
                if (json3 != null) { 
               if (removequat(json3.companyID) == removequat(c.companyID))
                {
                    del("users/" + i, "usersmone");
                }
            }
            }

            for (int i = 0; i < num_of_the_services + 1; i++)
            {
                var json4 = JsonConvert.DeserializeObject<dynamic>(get("services/" + i));
                if (json4 != null) { 
                if (removequat(json4.companyID) == removequat(c.companyID))
                {
                    del("services/" + i, "moneServices");
                }
            }
            }

            del("company/" + removequat(c.companyID), "mone");
            
        }



        /*
         * ///////////////////////////////////////////////////////////////////////////////////
         * 
         * 
         *                              the service part
         * 
         * 
         * /////////////////////////////////////////////////////////////////////////////////
         */ 

        public service return_service_by_id(int id)
        {
            service res = new service();
            var json = JsonConvert.DeserializeObject<dynamic>(get("services/" + id));
            if(json!=null)
            {
                int tempcompnayid = removequat(json.companyID);
                string tempname = json.name;
                int tempprice = removequat(json.price);
                int temptime= removequat(json.time);

                res.service_id = id;
                res.company = this.set_Company(tempcompnayid);
                res.name = tempname;
                res.price = tempprice;
                res.time = temptime;

            }


            return res;
        }
       
        public void commit_service(service s)
        {
            string values = "{\"companyID\":" + removequat(s.company.companyID) + ",\"name\":\"" + s.name + "\",\"price\":" + removequat(s.price) + ",\"time\":"+ removequat(s.time)+"}";
            string path = "services/" + s.service_id;
            commit(path, values);
        }
       
        public void del_service(service s)
        {
            del("services/" + s.service_id, "moneServices");
        }



        /*
         * //////////////////////////////////////////////////////////////////////
         * 
         *                        employee section! 
         * 
         * /////////////////////////////////////////////////////////////////////
         */

        public employee return_employee_by_id(int num)
        {
            employee newone = null;
            int mone = removequat(num_of_mone("employees", "moneemployees"));
            var json = JsonConvert.DeserializeObject<dynamic>(get("employees/" + num));
            if (json != null)
            {
                string adress = json.adress;
                company c = this.set_Company(removequat(json.companyid));
                string date = json.dateofjoin;
                string firstname = json.fname;
                string lastname = json.lname;
                string geolocation = json.geolocation;
                int id_num = this.removequat(json.id);
                string pass = json.password;
                string phone = json.phone;
                newone = new employee(num, adress, c, date, firstname, lastname, geolocation, id_num+"", pass, phone+"");
            }


                return newone;

        }
        public void commit_employee(employee e)
        {
            string values = "{\"adress\":\"" + e.adress + "\",\"companyid\":" + removequat(e.company.companyID) + ",\"dateofjoin\":\"" + e.dateofjoin + "\",\"fname\":\"" + e.firstname + "\",\"geolocation\":\""+e.geolocation+"\",\"id\":\""+e.employee_id_number+ "\",\"isadmin\":"+e.isadmin+ ",\"istech\":"+e.istech+ ",\"lname\":\""+e.lastname+ "\",\"password\":\""+e.password+"\",\"phone\":\""+e.phone+"\"}";
            string path = "employees/" + e.employee_id;
            commit(path, values);
        }
        public string getapitodel(string path)
        {
            var responseString = "";
            if (CheckForInternetConnection())
            {
                var url = "http://ovedli.orgfree.com/api.php?action=changetech&callid=" + path;
                var request = (HttpWebRequest)WebRequest.Create(url);

                var response = (HttpWebResponse)request.GetResponse();

                responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            }
            return responseString;
        }

        public string getapitodelmanuel(string path, string pathtwo)
        {
            var responseString = "";
            if (CheckForInternetConnection())
            {
                var url = "http://ovedli.orgfree.com/api.php?action=changetechmanul&callid=" + path+ "&empid="+pathtwo;
                var request = (HttpWebRequest)WebRequest.Create(url);

                var response = (HttpWebResponse)request.GetResponse();

                responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            }
            return responseString;
        }

        public void del_employee(employee e)
        {
            int flag = 0;
            int mone = this.num_of_mone("calls", "callsmone");
            for (int i = 1; i < mone + 1; i++)
            {
                Call temp = this.set_call_by_id(i);
                if (temp != null)
                {
                    if (temp.techid != null) { 
                    if (temp.techid.employee_id == e.employee_id)
                    {
                        var json = JsonConvert.DeserializeObject<dynamic>(getapitodel(i + ""));
                        {
                            string techid = json.thetech;
                            int techidint = removequat(techid);
                            if (techidint == 0) { temp.techid = new employee(); }
                            else
                            {
                                temp.techid = return_employee_by_id(techidint);
                            }
                            commit_Call(temp);
                        }
                    }
                    }
                }
            }
            del("employees/"+e.employee_id, "moneemployees");
        }
    

            /*
             * //////////////////////////////////////////////////////////////////
             *                          calls 
             * 
             * /////////////////////////////////////////////////////////////////
             */
         public void commit_Call(Call c)
        {
            string values = "{\"arrivedate\":\""+c.date_of_arrive+ "\",\"date\":\""+c.date_of_open+ "\",\"description\":\""+c.description+ "\",\"emailofuser\":\""+c.emailofuser+ "\",\"fullname\":\""+c.fullname+ "\",\"geolocation\":\""+c.geolocation+ "\",\"id\":\""+c.id+ "\",\"issolved\":\""+c.issolved+ "\",\"phone\":\""+c.phone+ "\",\"rank\":\""+c.rank+ "\",\"serviceid\":\""+ c.serviceid.service_id+ "\",\"techid\":\""+c.techid.employee_id+"\"}";
            string path = "calls/" + c.call_id;
            commit(path, values);

        }
         public Call set_call_by_id(int id_num)
        {
            Call res = new Call();
            var json = JsonConvert.DeserializeObject<dynamic>(get("calls/" + id_num));
            if(json!=null)
            {
                string arrivedate = json.arrivedate;
                string date = json.date;
                string description = json.description;
                string emailofuser = json.emailofuser;
                string fullname = json.fullname;
                string geolocation = json.geolocation;
                string id = json.id;
                string issolved = json.issolved;
                string phone = json.phone;
                string rank = json.rank;
                string serviceids = json.serviceid;
                string etechids = json.techid;
                service serviceid = this.return_service_by_id(removequat(serviceids));
                employee techid = this.return_employee_by_id(removequat(etechids));
                res = new Call(id_num, arrivedate, date, description, emailofuser,fullname,geolocation, id, issolved, phone, rank, serviceid, techid);
            }
            return res;
        }

        public void remove_call(int id)
        {
            del("calls/" + id, "callsmone");
        }

        public void finishthecall(string call)
        {
            var responseString = "";
            if (CheckForInternetConnection())
            {
                var url = "http://ovedli.orgfree.com/api.php?action=callfinish&callid=" + call;
                var request = (HttpWebRequest)WebRequest.Create(url);

                var response = (HttpWebResponse)request.GetResponse();

                responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            }
            
        }

        /***************************************************************************
         * 
         * function try to get faster 
         * 
         * ---------------------------------------------------------------------------
         */

        public int faster_getmone(string json, string mone)
        {
            string res = "";
            int theres = 0;
            res=CutME(mone, "}", json);
            res = res.Replace(mone, "");
            res = res.Replace("\"", "");
            res = res.Replace(":", "");
            theres = removequat(res);


            return theres;
        }
        public employee faster_getemployeebyid(string json,int id)
        {
            int flag = 0;
            employee newone = new employee();
            string res = "";
            res = CutME("employees", ",\"moneemployees\"", json);
            res = res.Replace("employees\":{", "");
            string[] strArr = null;
            char[] splitchar = { '}' };
            strArr = res.Split(splitchar);
            for (int i = 0; i < strArr.Length; i++)
            {
                if (flag == 0) { 
                string[] strArrsec = null;
                char[] splitcharsec = { '{' };
                if (strArr[i] != "")
                {
                    strArrsec = strArr[i].Split(splitcharsec);
                    strArrsec[0] = strArrsec[0].Replace(":", "");
                    strArrsec[0] = strArrsec[0].Replace(",", "");
                    if (removequat(strArrsec[0]) == id)
                    {
                        string[] strArrtre = null;
                        char[] splitchartre = { ',' };
                        newone.employee_id = removequat(strArrsec[0]);
                        strArrtre = strArrsec[1].Split(splitchartre);
                        newone.adress = strArrtre[0];
                        newone.adress = newone.adress.Replace("\"adress\":", "");
                        newone.adress = newone.adress.Replace("\"", "");
                        strArrtre[1] = strArrtre[1].Replace("\"companyid\":", "");
                        strArrtre[1] = strArrtre[1].Replace("\"", "");
                        newone.company = fast_getcompanybyid(json, removequat(strArrtre[1]));
                        newone.dateofjoin = strArrtre[2];
                        newone.dateofjoin = newone.dateofjoin.Replace("\"dateofjoin\":", "");
                        newone.dateofjoin = newone.dateofjoin.Replace("\"", "");
                        newone.firstname= strArrtre[3];
                        newone.firstname = newone.firstname.Replace("\"fname\":", "");
                        newone.firstname = newone.firstname.Replace("\"", "");
                        strArrtre[4]= strArrtre[4].Replace("\"geolocation\":", ""); 
                        strArrtre[4] = strArrtre[4].Replace("\"", "");
                        strArrtre[5]=strArrtre[5].Replace("\"", "");
                        newone.geolocation = strArrtre[4] + "," + strArrtre[5];
                        strArrtre[6] = strArrtre[6].Replace("\"id\":", "");
                        strArrtre[6] = strArrtre[6].Replace("\"", "");
                        newone.employee_id_number = strArrtre[6];
                        strArrtre[9] = strArrtre[9].Replace("\"lname\":", "");
                        strArrtre[9] = strArrtre[9].Replace("\"", "");
                        newone.lastname = strArrtre[9];
                        strArrtre[10] = strArrtre[10].Replace("\"password\":", "");
                        strArrtre[10] = strArrtre[10].Replace("\"", "");
                        newone.password = strArrtre[10];
                        strArrtre[11] = strArrtre[11].Replace("\"phone\":", "");
                        strArrtre[11] = strArrtre[11].Replace("\"", "");
                        newone.phone = strArrtre[11];
                            
                            flag++;
                        
                    }

                }
            }
            }
            //res = res.Replace("", "");
            return newone; 
        }

        public company fast_getcompanybyid(string json,int id)
        {
            int flag = 0;
            company newone = new company();
            string res = "";
            res = CutME("company", ",\"mone\"", json);
            res = res.Replace("company\":{", "");
            string[] strArr = null;
            char[] splitchar = { '}' };
            strArr = res.Split(splitchar);
            for (int i = 0; i < strArr.Length; i++)
            {
                if (flag == 0) { 
                string[] strArrsec = null;
                char[] splitcharsec = { '{' };
                if (strArr[i] != "")
                {
                    strArrsec = strArr[i].Split(splitcharsec);
                    strArrsec[0] = strArrsec[0].Replace(":", "");
                    strArrsec[0] = strArrsec[0].Replace(",", "");
                    if (removequat(strArrsec[0]) == id)
                    {
                        string[] strArrtre = null;
                        char[] splitchartre = { ',' };
                        newone.companyID = removequat(strArrsec[0]);
                        strArrtre = strArrsec[1].Split(splitchartre);
                        newone.companyLogo = strArrtre[1];
                        newone.companyLogo = newone.companyLogo.Replace("\"companyLogo\":", "");
                        newone.companyLogo = newone.companyLogo.Replace("\"", "");
                        newone.companyName = strArrtre[2];
                        newone.companyName = newone.companyName.Replace("\"companyName\":", "");
                        newone.companyName = newone.companyName.Replace("\"", "");
                            flag++;

                    }

                }
            }
            }
            //res = res.Replace("", "");
            return newone;
            
        }

        public user fast_getuserbyid(string json, int id)
        {
            int flag = 0;
            user newone = new user();
            string res = "";
            res = CutME("users", ",\"usersmone\"", json);
            res = res.Replace("users\":{", "");
            string[] strArr = null;
            char[] splitchar = { '}' };
            strArr = res.Split(splitchar);
            for (int i = 0; i < strArr.Length; i++)
            {
                if (flag == 0)
                {
                    string[] strArrsec = null;
                    char[] splitcharsec = { '{' };
                    if (strArr[i] != "")
                    {
                        strArrsec = strArr[i].Split(splitcharsec);
                        strArrsec[0] = strArrsec[0].Replace(":", "");
                        strArrsec[0] = strArrsec[0].Replace(",", "");
                        if (removequat(strArrsec[0]) == id)
                        {
                            string[] strArrtre = null;
                            char[] splitchartre = { ',' };
                            newone.userid = removequat(strArrsec[0]);
                            strArrtre = strArrsec[1].Split(splitchartre);
                            strArrtre[0] = strArrtre[0].Replace("\"companyID\":", "");
                            strArrtre[0] = strArrtre[0].Replace("\"", "");
                            newone.copmanyID= removequat(strArrtre[0]);
                            newone.password = strArrtre[1];
                            newone.password = newone.password.Replace("\"password\":", "");
                            newone.password = newone.password.Replace("\"", "");
                            strArrtre[2]= strArrtre[2].Replace("\"superuser\":", "");
                            strArrtre[2] = strArrtre[2].Replace("\"", "");
                            newone.superuser = removequat(strArrtre[2]);
                            newone.username = strArrtre[3];
                            newone.username = newone.username.Replace("\"username\":", "");
                            newone.username = newone.username.Replace("\"", "");
                           
                            flag++;

                        }

                    }
                }
            }
            //res = res.Replace("", "");
            return newone;

        }

        public service fast_servicebyid(string json, int id)
        {
            int flag = 0;
            service newone = new service();
            string res = "";
            res = CutME("services", ",\"moneServices\"", json);
            res = res.Replace("services\":{", "");
            string[] strArr = null;
            char[] splitchar = { '}' };
            strArr = res.Split(splitchar);
            for (int i = 0; i < strArr.Length; i++)
            {
                if (flag == 0)
                {
                    string[] strArrsec = null;
                    char[] splitcharsec = { '{' };
                    if (strArr[i] != "")
                    {
                        strArrsec = strArr[i].Split(splitcharsec);
                        strArrsec[0] = strArrsec[0].Replace(":", "");
                        strArrsec[0] = strArrsec[0].Replace(",", "");
                        if (removequat(strArrsec[0]) == id)
                        {
                            string[] strArrtre = null;
                            char[] splitchartre = { ',' };
                            newone.service_id = removequat(strArrsec[0]);
                            strArrtre = strArrsec[1].Split(splitchartre);
                            strArrtre[0] = strArrtre[0].Replace("\"companyID\":", "");
                            strArrtre[0] = strArrtre[0].Replace("\"", "");
                            newone.company = fast_getcompanybyid(json,removequat(strArrtre[0]));
                            newone.name = strArrtre[1];
                            newone.name = newone.name.Replace("\"name\":", "");
                            newone.name = newone.name.Replace("\"", "");
                            strArrtre[2] = strArrtre[2].Replace("\"price\":", "");
                            strArrtre[2] = strArrtre[2].Replace("\"", "");
                            newone.price = removequat(strArrtre[2]);
                            strArrtre[3] = strArrtre[3].Replace("\"time\":", "");
                            strArrtre[3] = strArrtre[3].Replace("\"", "");
                            newone.time = removequat(strArrtre[3]);
                            
                            flag++;

                        }

                    }
                }
            }
            //res = res.Replace("", "");
            return newone;

        }
        public Call fast_callbyid(string json, int id)
        {
            int flag = 0;
            Call newone = new Call();
            string res = "";
            res = CutME("calls", ",\"callsmone\"", json);
            res = res.Replace("calls\":{", "");
            string[] strArr = null;
            char[] splitchar = { '}' };
            strArr = res.Split(splitchar);
            for (int i = 0; i < strArr.Length; i++)
            {
                if (flag == 0)
                {
                    string[] strArrsec = null;
                    char[] splitcharsec = { '{' };
                    if (strArr[i] != "")
                    {
                        strArrsec = strArr[i].Split(splitcharsec);
                        strArrsec[0] = strArrsec[0].Replace(":", "");
                        strArrsec[0] = strArrsec[0].Replace(",", "");
                        if (removequat(strArrsec[0]) == id)
                        {
                            string[] strArrtre = null;
                            char[] splitchartre = { ',' };
                            newone.call_id = removequat(strArrsec[0]);
                            strArrtre = strArrsec[1].Split(splitchartre);
                            strArrtre[0] = strArrtre[0].Replace("\"arrivedate\":", "");
                            strArrtre[0] = strArrtre[0].Replace("\"", "");
                            newone.date_of_arrive = strArrtre[0];
                            strArrtre[1] = strArrtre[1].Replace("\"date\":", "");
                            strArrtre[1] = strArrtre[1].Replace("\"", "");
                            newone.date_of_open = strArrtre[1];
                            strArrtre[2] = strArrtre[2].Replace("\"description\":", "");
                            strArrtre[2] = strArrtre[2].Replace("\"", "");
                            newone.description = strArrtre[2];
                            strArrtre[3] = strArrtre[3].Replace("\"emailofuser\":", "");
                            strArrtre[3] = strArrtre[3].Replace("\"", "");
                            newone.emailofuser = strArrtre[3];
                            strArrtre[4] = strArrtre[4].Replace("\"fullname\":", "");
                            strArrtre[4] = strArrtre[4].Replace("\"", "");
                            newone.fullname = strArrtre[4];
                            strArrtre[5] = strArrtre[5].Replace("\"geolocation\":", "");
                            strArrtre[5] = strArrtre[5].Replace("\"", "");
                            newone.geolocation = strArrtre[5];
                            strArrtre[6] = strArrtre[6].Replace("\"id\":", "");
                            strArrtre[6] = strArrtre[6].Replace("\"", "");
                            newone.id = strArrtre[6];
                            strArrtre[7] = strArrtre[7].Replace("\"issolved\":", "");
                            strArrtre[7] = strArrtre[7].Replace("\"", "");
                            newone.issolved = strArrtre[7];
                            strArrtre[8] = strArrtre[8].Replace("\"phone\":", "");
                            strArrtre[8] = strArrtre[8].Replace("\"", "");
                            newone.phone = strArrtre[8];
                            strArrtre[9] = strArrtre[9].Replace("\"rank\":", "");
                            strArrtre[9] = strArrtre[9].Replace("\"", "");
                            newone.rank = strArrtre[9];
                            strArrtre[10] = strArrtre[10].Replace("\"serviceid\":", "");
                            strArrtre[10] = strArrtre[10].Replace("\"", "");
                            newone.serviceid = fast_servicebyid(json, removequat(strArrtre[10]));
                            strArrtre[11] = strArrtre[11].Replace("\"techid\":", "");
                            strArrtre[11] = strArrtre[11].Replace("\"", "");
                            newone.techid = faster_getemployeebyid(json, removequat(strArrtre[11]));
                            
                            flag++;

                        }

                    }
                }
            }
            //res = res.Replace("", "");
            return newone;

        }
    }
}
