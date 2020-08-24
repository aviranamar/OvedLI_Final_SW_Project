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
    /// Interaction logic for img_company_replace.xaml
    /// </summary>
    public partial class img_company_replace : Window
    {
        company com;
        user use;
        function f = new function();

        public img_company_replace(company c,user u)
        {
            InitializeComponent();
            f.define_project_base("https://ana10project.firebaseio.com");
            this.com = c;
            this.use = u;
            logolink.Text = com.companyLogo;

        }

        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            compnaymanage win = new compnaymanage(com,use);
            win.Show();
            this.Close();
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            if (f.CheckForInternetConnection())
            {
                if (logolink.Text != "")
                {
                    this.com.setCompnayLogo(logolink.Text);
                    f.comit_company(this.com);
                    MessageBox.Show("בוצע בהצלחה");
                    compnaymanage win = new compnaymanage(com, use);
                    win.Show();
                    this.Close();

                }
                else
                {
                    MessageBox.Show("לא ניתן להשאיר שדה ריק");
                }
            }
            else
            {
                MessageBox.Show("אין חיבור לאינטרנט");
            }
        }
    }
}
