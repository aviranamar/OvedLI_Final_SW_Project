using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OvedLi
{
    /// <summary>
    /// Interaction logic for employeeboxedit.xaml
    /// </summary>
    public partial class employeeboxedit : Window
    {
        function f=new function();
        company c;
        user u;
        employee e;
        int zoom = 10;
        string gpsglobal;
        public employeeboxedit(company com,user us,employee em)
        {
           
            InitializeComponent();
            f.define_project_base("https://ana10project.firebaseio.com");
            this.c = com;
            this.u = us;
            this.e = em;
            definition();

        }


 
        public void definition()
        {
            if (f.CheckForInternetConnection()) {
                this.Title = "עריכת עובד " + e.firstname + " " + e.lastname;
                this.idnumbertb.Text = e.employee_id_number;
                this.fnametb.Text = e.firstname;
                this.lnametb.Text = e.lastname;
                this.adresstb.Text = e.adress;
                this.phonetb.Text = e.phone;
                string geostr = e.geolocation;
                string[] words = geostr.Split(',');
                geostr = words[1] + "," + words[0];
                this.gpsglobal = geostr;
                this.locationonmapweb.Visibility = Visibility.Visible;
                this.locationonmapweb.Navigate("https://api.tomtom.com/map/1/staticimage?key=VjsKK4fEUVclIS5slDjs3okAKwrJcOSf&zoom=10&center=" + this.gpsglobal + "& format=jpg&layer=basic&style=main&width=500&height=150&view=Unified");
                this.dtataforjoinkbk.Content += " " + e.dateofjoin;
                this.companylbl.Content += " \n" + e.company.companyName;
                string testfortest = f.get("");

                int mone = f.faster_getmone(testfortest, "callsmone");
               // f.num_of_mone("calls", "callsmone");
                int calls = 0; 
                int closedCall = 0;
                float avg = 0;
                int themoney = 0;
                for (int i=1;i<mone+1;i++)
                {

                    //Call temp = f.set_call_by_id(i);
                    Call temp = f.fast_callbyid(testfortest, i);
                    if (temp != null)
                    {
                        if (temp.techid != null) { 
                        if (f.removequat(temp.techid.employee_id) == f.removequat(e.employee_id))
                        {
                            calls++;
                            if (f.removequat(temp.issolved) == 1)
                            {
                                avg += f.removequat(temp.rank);
                                closedCall++;
                                themoney += f.removequat(temp.serviceid.price);
                            }
                        }
                    }
                    }
                }
                this.opencalltotal.Content += " " + calls;
                this.closedcall.Content += " " + closedCall;
                if (avg != 0) { 
                this.rankavg.Content += " \n" + ((avg) / closedCall) + "/5";
                }
                else
                {
                    this.rankavg.Content += " \n 5/5";
                }
                this.money.Content += " " + themoney;


            }
            else
            {
                MessageBox.Show("אין חיבור לאינטרנט");
            }
        }

        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {
            if (f.CheckForInternetConnection())
            {
                if (MessageBox.Show("לחיצה על אישור תמחק את עובד זה האם אתה בטוח?", "מחיקת עובד", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                { }
                else
                {
                    f.del_employee(this.e);
                    MessageBox.Show("נמחק בהצלחה!");
                    compnaymanage win = new compnaymanage(this.c,this.u);
                    win.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("אין חיבור לאינטרנט");
            }
        }

        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            if (f.CheckForInternetConnection()) { 
            compnaymanage cm = new compnaymanage(this.c, this.u);
            cm.Show();
            this.Close();
            }
            else
            {
                MessageBox.Show("אין חיבור לאינטרנט");
            }
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            string errors = "";
            int errors_count = 0;
            if(f.CheckForInternetConnection())
            {
                if(fnametb.Text!="")
                {
                    bool containsInt = fnametb.Text.Any(char.IsDigit);
                    bool cont_spaical = f.CheckSpacialCherter(fnametb.Text);
                    if(containsInt==true || cont_spaical == true) { errors += "\n שם לא יכול להכי תווים מיוחדים או מספרים"; errors_count++; }
                    else { 
                    this.e.firstname = fnametb.Text;
                    }
                }
                else
                { errors += "\n שם לא יכול להיות ריק"; errors_count++; }
                if(idnumbertb.Text!="")
                {
                    if (f.CheckIDNo(idnumbertb.Text)) { 
                    this.e.employee_id_number = idnumbertb.Text;
                    }
                    else { errors += "\n תעודת זהות אינה תקינה"; errors_count++; }


                }
                if (lnametb.Text != "")
                {
                    bool containsInt = lnametb.Text.Any(char.IsDigit);
                    bool cont_spaical = f.CheckSpacialCherter(lnametb.Text);
                    if (containsInt == true || cont_spaical == true) { errors += "\n שם לא יכול להכי תווים מיוחדים או מספרים"; errors_count++; }
                    else
                    {
                        this.e.lastname = lnametb.Text;
                    }

                }
                else
                {
                    errors += "\n שם משפחה לא יכול להיות ריק";
                    errors_count++;
                }
                if(adresstb.Text!="")
                {
                    this.e.adress = adresstb.Text;
                }
                else
                {
                    errors += "\n כתובת אינה יכולה להיות ריקה";
                    errors_count++;
                }
                if(phonetb.Text!="")
                {
                    bool conset_phone_digit_check = f.IsDigitsOnly(phonetb.Text);
                    if (conset_phone_digit_check == false) { errors += "\n טלפון לא יכול להכיל תווים חוץ ממספרים"; errors_count++; }
                    else { 
                    this.e.phone = phonetb.Text;
                    }
                }
                else
                {
                    errors += "\n טלפון אינו יכול להיות ריק";
                    errors_count++;
                }
                if(passwordBox.Password!="")
                {
                    this.e.password = f.strEncryptred(passwordBox.Password);
                }
                if (errors_count == 0) { 
                f.commit_employee(this.e);
                MessageBox.Show("בוצע בהצלחה");
                compnaymanage cm = new compnaymanage(this.c, this.u);
                cm.Show();
                this.Close();
                }
                else
                {
                    MessageBox.Show(errors);
                }


            }
            else
            {
                MessageBox.Show("אין חיבור לאינטרנט");
            }
        }

        private void plus_Click(object sender, RoutedEventArgs e)
        {
            if (f.CheckForInternetConnection()) { 
            if (this.zoom < 22)
            {
                this.zoom++;
                this.locationonmapweb.Navigate("https://api.tomtom.com/map/1/staticimage?key=VjsKK4fEUVclIS5slDjs3okAKwrJcOSf&zoom=" + this.zoom + "&center=" + this.gpsglobal + "& format=jpg&layer=basic&style=main&width=500&height=150&view=Unified");
            }
            else
            {
                this.zoom=10;
                this.locationonmapweb.Navigate("https://api.tomtom.com/map/1/staticimage?key=VjsKK4fEUVclIS5slDjs3okAKwrJcOSf&zoom=" + this.zoom + "&center=" + this.gpsglobal + "& format=jpg&layer=basic&style=main&width=500&height=150&view=Unified");
            }
            }
            else
            { MessageBox.Show("אין חיבור לאינטרנט"); }
        }

        private void minus_Click(object sender, RoutedEventArgs e)
        {
            if (f.CheckForInternetConnection()) { 
            if (this.zoom > 1)
            {
                this.zoom--;
                this.locationonmapweb.Navigate("https://api.tomtom.com/map/1/staticimage?key=VjsKK4fEUVclIS5slDjs3okAKwrJcOSf&zoom=" + this.zoom + "&center=" + this.gpsglobal + "& format=jpg&layer=basic&style=main&width=500&height=150&view=Unified");
            }
            else
            {
                this.zoom = 10;
                this.locationonmapweb.Navigate("https://api.tomtom.com/map/1/staticimage?key=VjsKK4fEUVclIS5slDjs3okAKwrJcOSf&zoom=" + this.zoom + "&center=" + this.gpsglobal + "& format=jpg&layer=basic&style=main&width=500&height=150&view=Unified");

            }
            }
            else
            {
                MessageBox.Show("אין חיבור לאינטרנט");
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            compnaymanage cm = new compnaymanage(this.c, this.u);
            cm.Show();
            this.Close();
        }
    }
}
