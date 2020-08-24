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
    /// Interaction logic for mangeComnyUsers.xaml
    /// </summary>
    public partial class mangeComnyUsers : Window
    {
        function f = new function();
        company com;
        user use;
        string testfortest;
        public mangeComnyUsers(company c, user u)
        {
            InitializeComponent();
            f.define_project_base("https://ana10project.firebaseio.com");
            this.com = c;
            this.use = u;
            testfortest = f.get("");
            define_combobox();

        }

        public void define_combobox()
        {
            if(f.CheckForInternetConnection())
            {
                //int num = f.num_of_mone("users", "usersmone");
                int num = f.faster_getmone(testfortest, "usersmone");
                for (int i=1;i<num+1;i++)
                {
                    user newoneuse = f.fast_getuserbyid(testfortest, i);
                    if(newoneuse != null)
                    {
                        if(f.removequat(newoneuse.copmanyID)==f.removequat(this.com.companyID))
                        {
                            user newone = newoneuse;
                            ComboBoxItem item = new ComboBoxItem();
                            item.Content = newone.username;
                            item.Tag = newone.userid;
                            generalcb.Items.Add(item);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("אין חיבור לרשת");
            }
        }
        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            if (f.CheckForInternetConnection()) {
                if (generalcb.SelectedItem == null) { MessageBox.Show("אנא בחר מתוך הרשימה"); } else { 
               
                    string selected = ((ComboBoxItem)generalcb.SelectedItem).Tag.ToString();
                    user u = f.retrunuserbyid(f.removequat(selected));
                    userbox ub = new userbox(this.com, u);
                    ub.Show();
                    this.Close();

                }
               
               
                
            }
            else { MessageBox.Show("אין חיבור לרשת"); }
            
        }

        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            compnaymanage cm = new compnaymanage(this.com, this.use);
            cm.Show();
            this.Close();
        }


        private void adduser_Click(object sender, RoutedEventArgs e)
        {
            userbox ub = new userbox(this.com, this.use, "add");
            ub.Show();
            this.Close();
        }
    }
}
