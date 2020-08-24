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
    /// Interaction logic for userbox.xaml
    /// </summary>
    public partial class userbox : Window
    {
        function f = new function();
        
        company com = null;
        user us = null;
        int editorregisetr = 0;
        int editForCompany = 0;


        public userbox(company c)
        {
            InitializeComponent();
            f.define_project_base("https://ana10project.firebaseio.com");
            this.com = c;
            this.Title = "הוסף משתמש חדש";
            this.deletebtn.Visibility =Visibility.Hidden;
            companyNameTB.Text = c.companyName;
            companyNameTB.Tag = f.removequat(c.companyID);
            this.com = c;
            usernameTB.Text = "";
            passwordTB.Password = "";
        }
        public userbox(company c,user u,string s)
        {
            InitializeComponent();
            f.define_project_base("https://ana10project.firebaseio.com");
            this.com = c;
            this.Title = "הוסף משתמש חדש";
            this.deletebtn.Visibility = Visibility.Hidden;
            this.isAdminCB.Visibility = Visibility.Hidden;
            companyNameTB.Text = c.companyName;
            companyNameTB.Tag = f.removequat(c.companyID);
            this.com = c;
            usernameTB.Text = "";
            passwordTB.Password = "";

            editForCompany++;
        }
        public userbox(user u)
        {
            InitializeComponent();
            this.editorregisetr = 1;
            this.us = u;
            f.define_project_base("https://ana10project.firebaseio.com");
            if(f.removequat(u.copmanyID)!=0)
            { 
                this.com = f.set_Company(f.removequat(u.copmanyID));
                companyNameTB.Text = com.companyName;
            }
            else
            {
                companyNameTB.Text = "לא משוייך לחברה";

            }
            usernameTB.Text = u.username;
            if(u.superuser==1)
            {
                this.isAdminCB.IsChecked = true;
                this.isAdminCB.IsEnabled = false;
            }
        }
        public userbox(company c,user u)
        {
            InitializeComponent();
            this.editorregisetr = 1;
            this.isAdminCB.Visibility = Visibility.Hidden;
            this.us = u;
            f.define_project_base("https://ana10project.firebaseio.com");
            if (f.removequat(u.copmanyID) != 0)
            {
                this.com = f.set_Company(f.removequat(u.copmanyID));
                companyNameTB.Text = com.companyName;
            }
            else
            {
                companyNameTB.Text = "לא משוייך לחברה";

            }
            usernameTB.Text = u.username;
            if (u.superuser == 1)
            {
                this.isAdminCB.IsChecked = true;
                this.isAdminCB.IsEnabled = false;
            }
            editForCompany++;
        }
        public void setCompany(company c)
        {
            this.com = c;

        }

        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {
            if (f.CheckForInternetConnection()) {
                if (MessageBox.Show("לחיצה על אישור תמחק את משתמש זה האם אתה בטוח?", "מחיקת משתמש", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                { }
                else { 
                    f.del("users/" + us.getuserid(), "usersmone");
                    MessageBox.Show("נמחק בהצלחה!");
                    if(this.editForCompany>0)
                    { compnaymanage win = new compnaymanage(this.com, this.us);
                        win.Show();
                        this.Close();
                    }
                    else
                    { superuser win = new superuser();
                        win.Show();
                        this.Close();
                    }
                    
                   
                }
            }
            else
            {
                MessageBox.Show("אין חיבור לאינטרנט");
            }
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            if (f.CheckForInternetConnection())
            {
                
                int mone_for_id = 0;
                if (editorregisetr == 1)
                {
                    string protectlowername = this.usernameTB.Text;
                    protectlowername = protectlowername.ToLower();
                    this.us.setusername(protectlowername);
                    if (this.passwordTB.Password.ToString() != "")
                    {
                        this.us.setpassword(f.strEncryptred(this.passwordTB.Password.ToString()));
                    }
                    if ((bool)isAdminCB.IsChecked == true)
                    {
                        this.us.superuser = 1;
                        this.us.copmanyID = 0;
                    }
                    if ((bool)isAdminCB.IsChecked == false)
                    {
                        this.us.superuser = 0;
                        this.us.copmanyID = f.removequat(this.us.copmanyID);
                    }


                }
                else
                {
                    this.us = new user();
                    string protectlowername = this.usernameTB.Text;
                    protectlowername = protectlowername.ToLower();
                    this.us.setusername(protectlowername);
                    if (this.passwordTB.Password.ToString() != "")
                    {
                        this.us.setpassword(f.strEncryptred(this.passwordTB.Password.ToString()));
                    }
                    if ((bool)isAdminCB.IsChecked == true)
                    {
                        this.us.superuser = 1;
                        this.us.copmanyID = 0;
                    }
                    if ((bool)isAdminCB.IsChecked == false)
                    {
                        this.us.superuser = 0;
                        this.us.copmanyID = f.removequat(this.companyNameTB.Tag.ToString());
                    }
                    mone_for_id = f.num_of_mone("users", "usersmone");
                    this.us.setuserid(mone_for_id + 1);
                }
                if (this.passwordTB.Password.ToString() == "" || this.usernameTB.Text == "")
                {
                    MessageBox.Show("השדות לא יכולים להיות ריקים");
                }
                else {
                    f.commitUser(this.us);
                    MessageBox.Show("בוצע בהצלחה");
                }
                
                if (this.editForCompany > 0)
                {
                    compnaymanage win = new compnaymanage(this.com, this.us);
                    win.Show();
                    this.Close();
                }
                else
                {
                    superuser win = new superuser();
                    win.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("אין אינטרנט");
            }
        }

        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.editForCompany > 0)
            {
                compnaymanage win = new compnaymanage(this.com, this.us);
                win.Show();
                this.Close();
            }
            else
            {
                superuser win = new superuser();
                win.Show();
                this.Close();
            }
        }
    }
}
