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
using System.Windows.Navigation;
using System.Windows.Shapes;
//--יוזינג פירביס--
using FireSharp.Response;
using FireSharp.Config;
using FireSharp.Interfaces;
using RestSharp;

namespace OvedLi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        user u1;
        function f;


        

        public MainWindow()
        {
            
            WindowState = WindowState.Maximized;  
            f = new OvedLi.function();
            if (f.CheckForInternetConnection()) { 
            f.define_project_base("https://ana10project.firebaseio.com");
                this.u1 = f.set_local_user_after_login(f.check_if_username_and_password_are_ok("Admin", "123456"));
            InitializeComponent();
                loadtimeclock.Visibility = Visibility.Hidden;

            }
            else
            {
                MessageBox.Show("אין חיבור לאינטרנט");
            }

            /*
            //test
            var client = new RestClient("https://ana10project.firebaseio.com/test/.json");
            var request = new RestRequest();
            var strJSONContent = "{\"hit\":\"hit\"}";
            request.Method = Method.PATCH;
            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", strJSONContent, ParameterType.RequestBody);

            var response = client.Execute(request);

            //end test
            */
            int v = 0;

        }

        private void loginbtnonclick(object sender, RoutedEventArgs e)
        {

            if (f.CheckForInternetConnection())
            {
                loadtimeclock.Visibility = Visibility.Visible;
                f.define_project_base("https://ana10project.firebaseio.com");
                this.u1 = f.set_local_user_after_login(f.check_if_username_and_password_are_ok(usernameTB.Text, passwordTB.Password.ToString()));
                if (this.u1 != null)
                {
                    if (f.removequat(u1.superuser) == 1) {
                        
                        MessageBox.Show("ההתחברות בוצעה בהצלחה!");
                    this.passwordTB.IsEnabled = false;
                    this.usernameTB.IsEnabled = false;
                    this.loginbtn.IsEnabled = false;
                    var superuserwindow = new superuser();
                    superuserwindow.setupTheUser(u1);
                    superuserwindow.Show();
                    this.Close();
                        // MainWindow.Content = new superuser();
                    }
                    else
                    {
                        MessageBox.Show("ההתחברות בוצעה בהצלחה!");
                        this.passwordTB.IsEnabled = false;
                        this.usernameTB.IsEnabled = false;
                        this.loginbtn.IsEnabled = false;
                        int comid = f.removequat(u1.copmanyID);
                        compnaymanage cm = new compnaymanage(f.set_Company(comid), u1);
                        cm.Show();
                        this.Close();
                    }
                }
                else
                {
                    loadtimeclock.Visibility = Visibility.Hidden;
                    MessageBox.Show("שם משתמש או סיסמה שגויים");

                }
            }
            else
            {
                MessageBox.Show("אין חיבור לאינטרנט");
            }
        }

    }
}
