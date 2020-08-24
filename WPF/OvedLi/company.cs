using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvedLi
{
    public class company
    {
        public int companyID;
        public string companyLogo;
        public string companyName;
        public company()
        {
            this.companyID = 0;
            this.companyName = "";
            this.companyLogo = "";
        }
        public company(int cNum, string name,string logo)
        {
            this.companyID = cNum;
            this.companyName = name;
            this.companyLogo = logo;
        }

        //gets

        public int getCompnayId()
        {
            return this.companyID;
        }
        public string getCompnayLogo()
        {
            return this.companyLogo;
        }
        public string getCompnayName()
        {
            return this.companyName;
        }

        //sets
        public void setCompnayId(int id)
        {
            this.companyID=id;
        }
        public void setCompnayLogo(string logo)
        {
            this.companyLogo=logo;
        }
        public void setCompnayName(string name)
        {
            this.companyName=name;
        }
    }
}
