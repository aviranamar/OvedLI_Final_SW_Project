using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvedLi
{
    public class user
    {

        public string username;
        public string password;
        public int copmanyID;
        public int superuser;
        public int userid;

        public user()
        {
            this.username = "";
            this.password = "";
            this.copmanyID = 0;
            this.superuser=0;
            this.userid = 0;

         }

        public user(string u,string p,int c,int s,int i)
        {
            this.username = u;
            this.password = p;
            this.copmanyID = c;
            this.superuser = s;
            this.userid = i;

        }

        //gets 

        public string getusername()
        {
            return this.username;
        }
        public string getpassword()
        {
            return this.password;
        }
        public int getcopmanyID()
        {
            return this.copmanyID;
        }
        public int getsuperuser()
        {
            return this.superuser;
        }
        public int getuserid()
        {
            return this.userid;
        }
        //sets

        public void setusername(string u)
        {
             this.username=u;
        }
        public void setpassword(string p)
        {
             this.password=p;
        }
        public void setcopmanyID(int c)
        {
            this.copmanyID=c;
        }
        public void setsuperuser(int s)
        {
            this.superuser=s;
        }
        public void setuserid(int i)
        {
            this.userid = i;
        }

    }
}
