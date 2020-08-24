using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvedLi
{
    public class employee
    {
        public int employee_id;
        public string adress;
        public company company;
        public string dateofjoin;
        public string firstname;
        public string lastname;
        public string geolocation;
        public string employee_id_number;
        public int isadmin;
        public int istech;
        public string password;
        public string phone;

        public employee()
        {
             this.employee_id=0;
            this.adress = "";
            this.company=new company();
            this.dateofjoin="";
            this.firstname="";
            this.lastname="";
            this.geolocation="";
            this.employee_id_number="";
            this.isadmin=0;
            this.istech=1;
            this.password="";
            this.phone="";
    }
        public employee(int id,string adr,company c,string date,string firstn,string lastn,string geo,string idnum,string pass,string pho)
        {
            this.employee_id = id;
            this.adress = adr;
            this.company = c;
            this.dateofjoin = date;
            this.firstname = firstn;
            this.lastname = lastn;
            this.geolocation = geo;
            this.employee_id_number = idnum;
            this.isadmin = 0;
            this.istech = 1;
            this.password = pass;
            this.phone = pho;
        }

        //gets
        public int get_employee_id() { return this.employee_id; }
        public string get_adress() { return this.adress; }
        public company get_company() { return this.company; }
        public string get_date() { return this.dateofjoin; }
        public string get_firstname() { return this.firstname; }
        public string get_lastname() { return this.lastname; }
        public string get_geoLocation() { return this.geolocation; }
        public string get_employee_id_number() { return this.employee_id_number; }
        public string get_password() { return this.password; }
        public string get_phone() { return this.phone; }
            

    }

}
