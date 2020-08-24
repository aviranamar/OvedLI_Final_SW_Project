using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvedLi
{
   public class service
    {
        //the atributes
       public int service_id;
       public company company;
       public string name;
        public int price;
        public int time;

     
        public service()
        {
             this.service_id=0;
            this.company=new company();
            this.name="";
            this.price=0;
            this.time = 0;
        }

        public service(int  id, company c,string n,int price,int time)
        {
            this.service_id = id;
            this.company = c;
            this.name =n;
            this.price=price;
            this.time =time;
        }

        //gets
        public int get_the_service_id()
        {
            return this.service_id;
        }
        public company get_the_company()
        {
            return this.company;
        }
        public string get_the_name()
        {
            return this.name;
        }
        public int get_the_price()
        {
            return this.price;
        }
        public int get_the_time()
        {
            return this.time;
        }


        //sets
        public void get_the_service_id(int theid)
        {
            this.service_id=theid;
        }
        public void get_the_company(company c)
        {
             this.company=c;
        }
        public void get_the_name(string nn)
        {
             this.name=nn;
        }
        public void get_the_price(int pr)
        {
             this.price=pr;
        }
        public void get_the_time(int ti)
        {
             this.time=ti;
        }
    }
}
