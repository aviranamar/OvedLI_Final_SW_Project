using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvedLi
{
    public class Call
    {
        public int call_id = 0;
        public string date_of_open;
        public string date_of_arrive;
        public string description;
        public string emailofuser;
        public string fullname;
        public string geolocation;
        public string id;
        public string issolved;
        public string phone;
        public string rank;
        public service serviceid;
        public employee techid;
        public Call()
        {
            this.call_id = 0;
            this.date_of_arrive = "";
            this.fullname = "";
            this.geolocation = "";
            this.date_of_open = "";
            this.description = "";
            this.emailofuser = "";
            this.id = "";
            this.issolved = "";
            this.phone = "";
            this.rank = "";
            this.serviceid = new service();
            this.techid = new employee();

        }
        public Call(int callid, string arrive, string open, string des, string email,string fullnam,string geo, string id, string slov, string ph, string ra, service servic, employee tech)
        {
            this.call_id = callid;
            this.date_of_arrive = arrive;
            this.date_of_open = open;
            this.description = des;
            this.emailofuser = email;
            this.fullname = fullnam;
            this.geolocation = geo;
            this.id = id;
            this.issolved = slov;
            this.phone = ph;
            this.rank = ra;
            this.serviceid = servic;
            this.techid = tech;

        }
    }
}
