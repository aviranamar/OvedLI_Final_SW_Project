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
    /// Interaction logic for superuser.xaml
    /// </summary>
    public partial class superuser : Window
    {
        function f;
         user u1;
       public  superuser()
        {
            f = new function();
            f.define_project_base("https://ana10project.firebaseio.com");
            WindowState = WindowState.Maximized;
            InitializeComponent();
            defineComboBoxOfTheCompanys();
            //insert

            // companyEditComboBox.Items.Add()
        }


        public void setupTheUser(user a)
        {
            this.u1 = a;
            

        }

        //define the company box
        public void defineComboBoxOfTheCompanys()
        {
            if(f.CheckForInternetConnection())
            { 
            int monecompany = f.num_of_mone("company", "mone");
            for (int i = 0; i < monecompany + 1; i++)
            {
               if(f.set_Company(i)!=null)
                {
                    company newone = f.set_Company(i);
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = newone.companyName;
                    item.Tag = newone.companyID;
                    companyEditComboBox.SelectedIndex = 0;
                    companyEditComboBox.Items.Add(item);
                }
            }
            }
            else
            {
                MessageBox.Show("אין חיבור לאינטרנט");
            }
        }

        private void companyaddbtn_Click(object sender, RoutedEventArgs e)
        {
            if (f.CheckForInternetConnection())
            {
                var companyedit = new companyedit();
                companyedit.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("אין חיבור לאינטרנט");
            }
        
        }

        private void companyedit_Click(object sender, RoutedEventArgs e)
        {
            //string selected = this.companyEditComboBox.SelectedItem.ToString();
            if (f.CheckForInternetConnection())
            {
                string selected = ((ComboBoxItem)companyEditComboBox.SelectedItem).Tag.ToString();
                company selectedone = f.set_Company(int.Parse(selected));
                var companyedit = new companyedit(selectedone);
                companyedit.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("אין חיבור לאינטרנט");
            }

        }

        private void refreshtb_Click(object sender, RoutedEventArgs e)
        {
            companyEditComboBox.Items.Clear();
            defineComboBoxOfTheCompanys();


        }

        private void adduserbtn_Click(object sender, RoutedEventArgs e)
        {
            if (f.CheckForInternetConnection())
            {
                string selected = ((ComboBoxItem)companyEditComboBox.SelectedItem).Tag.ToString();
                company selectedone = f.set_Company(int.Parse(selected));
                userbox userb = new userbox(selectedone);
                userb.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("אין חיבור לאינטרנט");
            }
        }

        private void editusertbn_Click(object sender, RoutedEventArgs e)
        {
            if(f.CheckForInternetConnection())
            { 
                edituser ed = new edituser();
                ed.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("אין אינטרנט");
            }
        }

        private void disconectbtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("התנתקת מהמערכת");
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
    }
}
