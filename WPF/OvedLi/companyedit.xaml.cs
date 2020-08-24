using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
    /// Interaction logic for companyedit.xaml
    /// </summary>
    public partial class companyedit : Window
    {
        function f=new function();
        company com = null;
        int editorregisetr = 0;
        public companyedit()
        {
            InitializeComponent();
            checlifeditorregisetr();
        }

        public companyedit(company c)
        {
            this.com = c;
            InitializeComponent();
            checlifeditorregisetr();
        }

        public void setcompany(company c)
        {
            this.com = c;
        }
        public void checlifeditorregisetr()
        {
            if(f.CheckForInternetConnection())
            {
                
            if (com == null)
            {
                editorregisetr = 1;
                this.Title = "רשום חברה חדשה";
                companynametb.Text = "שם החברה";
                companylogotbt.Text = "כתובת הלוגו";
                imagelogo.Visibility = Visibility.Hidden;
                deletebtn.Visibility = Visibility.Hidden;
                imagelogo.IsEnabled = false;
                deletebtn.IsEnabled = false;

            }
            else
            {
                editorregisetr = 0;
                this.Title = "ערוך חברה";
                companynametb.Text = com.companyName;
                companylogotbt.Text = com.companyLogo;
                WebClient wc = new WebClient();
                try
                {

                    byte[] bytes = wc.DownloadData(com.companyLogo);
                    MemoryStream ms = new MemoryStream(bytes);
                    System.Drawing.Image imagelogo = System.Drawing.Image.FromStream(ms);

                }
                catch (Exception e) {  }
                deletebtn.Visibility = Visibility.Visible;
                deletebtn.IsEnabled = true;

            }
            }
            else
            {
                MessageBox.Show("אין חיבור לאינטרנט");
            }
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            if (f.CheckForInternetConnection()) {
                if(companynametb.Text!="" && companylogotbt.Text != "") { 
                disableallbtns();
                f.define_project_base("https://ana10project.firebaseio.com");
            int mone = f.num_of_mone("company", "mone");
            int moneForNew= mone+1;
            if(editorregisetr==1)
            {
                com = new company(moneForNew, companynametb.Text, companylogotbt.Text);

            }
            else
            {
                
                com.setCompnayLogo(companylogotbt.Text);
                com.setCompnayName(companynametb.Text);
            }
            
            f.comit_company(this.com);
            if(editorregisetr == 1)
            {
                
                string value = "{\"mone\":"+moneForNew+"}";
                f.commit("company/", value);
            }
            MessageBox.Show("בוצע בהצלחה!");
                superuser win = new superuser();
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
            superuser win = new superuser();
            win.Show();
            this.Close();
        }

        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {
            if(f.CheckForInternetConnection())
            { 
            if (MessageBox.Show("לחיצה על אישור תמחק את כל הקריאות והטכנאים השייכים לחברה זו האם אתה בטוח?", "מחיקת החברה", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                
            }
            else
            {
                    disableallbtns();
                    f.define_project_base("https://ana10project.firebaseio.com");
                    f.delcompnay(this.com);
                    MessageBox.Show("בוצע בהצלחה");
                    superuser win = new superuser();
                    win.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("אין חיבור לאינטרנט");
            }
        }

        public void disableallbtns()
        {
            this.okbtn.IsEnabled = false;
            this.cancelbtn.IsEnabled = false;
            this.deletebtn.IsEnabled = false;
        }
    }
}
