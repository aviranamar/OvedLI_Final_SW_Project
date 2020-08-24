using Newtonsoft.Json;
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
    /// Interaction logic for editCallWindow.xaml
    /// </summary>
    public partial class editCallWindow : Window
    {
        function f = new function();
        company com;
        user use;
        Call ca;
        
        public editCallWindow(company c, user u, Call cc)
        {
            InitializeComponent();
            f.define_project_base("https://ana10project.firebaseio.com");
            this.com = c;
            this.use = u;
            this.ca = cc;
            define_window();

        }

        public void define_window()
        {
            if (f.CheckForInternetConnection()) { 
            openDateTB.Text = ca.date_of_open;
            arrivedateTB.Text = ca.date_of_arrive;
            desLBLans.Content = ca.description;
            emailtb.Text = ca.emailofuser;
            fullnameTB.Text = ca.fullname;
            adressTB.Text = ca.geolocation;
                arrivedateTB.IsEnabled = false;
            if(f.removequat(this.ca.issolved)==1)
                {
                    this.arrivedateTB.IsEnabled = false;
                    this.emailtb.IsEnabled = false;
                    this.fullnameTB.IsEnabled = false;
                    this.adressTB.IsEnabled = false;
                    this.change_manul_employee.IsEnabled = false;
                    this.change_auto_employee.IsEnabled = false;
                    this.finishme.IsEnabled = false;
                }
            if (ca.techid != null) { 
            techlblcontent.Content=ca.techid.firstname+" "+ca.techid.lastname;
            }
            else
            {
                techlblcontent.Content = "אין טכנאי ";
            }
            }
            else
            {
                MessageBox.Show("אין חיבור לאינטרנט");
            }
        }

        private void call_edit_ok_Click(object sender, RoutedEventArgs e)
        {
            if (f.CheckForInternetConnection())
            {
                int num_of_error = 0;
                string error_list = "";
                if (openDateTB.Text != "")
                {
                    this.ca.date_of_open = openDateTB.Text;
                }
                else
                {
                    num_of_error++;
                    error_list += "תאריך פתיחה לא יכול להיות ריק";

                }
                if (arrivedateTB.Text != "")
                {
                    this.ca.date_of_arrive = arrivedateTB.Text;
                }
                else
                {
                    num_of_error++;
                    error_list += "תאריך הגעה לא יכול להיות ריק";
                }
                if (emailtb.Text != "")
                {
                    this.ca.emailofuser = emailtb.Text;
                }
                else
                {
                    num_of_error++;
                    error_list += "אימייל לא יכול להיות ריק";
                }
                if (fullnameTB.Text != "")
                {
                    this.ca.fullname = fullnameTB.Text;
                }
                else
                {
                    num_of_error++;
                    error_list += "שם לא יכול להיות ריק";
                }
                if (adressTB.Text != "")
                {
                    this.ca.geolocation = adressTB.Text;
                }
                else
                {
                    num_of_error++;
                    error_list += "כתובת לא יכול להיות ריקה";
                }
                if(num_of_error>0)
                {
                    MessageBox.Show(error_list);
                }
                else
                {
                    f.commit_Call(this.ca);
                    MessageBox.Show("בוצע בהצלחה!");
                    callsWindow cw = new callsWindow(this.com, this.use);
                    cw.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("אין חיבור לאינטרנט");
            }
        }
        private void call_edit_cancel_Click(object sender, RoutedEventArgs e)
        {
            callsWindow cw = new callsWindow(this.com, this.use);
            cw.Show();
            this.Close();
        }

        private void change_manul_employee_Click(object sender, RoutedEventArgs e)
        {
           if(f.CheckForInternetConnection())
            {
                change_tech_manul_select ctms = new change_tech_manul_select(this.com, this.use, this.ca);
                ctms.Show();
                this.Close();
            }
           else
            {
                MessageBox.Show("אין חיבור לרשת");
            }
        }

        private void change_auto_employee_Click(object sender, RoutedEventArgs e)
        {
            var json = JsonConvert.DeserializeObject<dynamic>(f.getapitodel(this.ca.call_id + ""));
            string techid = json.thetech;
            int techidint = f.removequat(techid);
            if (techidint == 0)
            { MessageBox.Show("אין טכנאים זמינים"); }
            else
            {
                this.ca.techid = f.return_employee_by_id(techidint);
                techlblcontent.Content = ca.techid.firstname + " " + ca.techid.lastname;
                MessageBox.Show("בוצע בהצלחה!");

            }
        }

        private void finishme_Click(object sender, RoutedEventArgs e)
        {
            f.finishthecall(this.ca.call_id + "");
            this.ca.issolved = 1+"";
            MessageBox.Show("בוצע בהצלחה");
            this.arrivedateTB.IsEnabled = false;
            this.emailtb.IsEnabled = false;
            this.fullnameTB.IsEnabled = false;
            this.adressTB.IsEnabled = false;
            this.change_manul_employee.IsEnabled = false;
            this.change_auto_employee.IsEnabled = false;
            this.finishme.IsEnabled = false;
        }
    }
}
