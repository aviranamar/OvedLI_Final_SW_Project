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
    /// Interaction logic for serviceselect.xaml
    /// </summary>
    public partial class serviceselect : Window
    {
        function f = new function();
        company com;
        user use;
        int mono = 0;
        public serviceselect(company c, user u)
        {
            InitializeComponent();
            this.Title = "בחר חברה";
            this.com = c;
            this.use = u;
            f.define_project_base("https://ana10project.firebaseio.com");
            defineComboBox();
        }

        public void defineComboBox()
        {
            int mone = f.num_of_mone("services", "moneServices");
            for (int i = 1; i < mone + 1; i++)
            {
                if (f.return_service_by_id(i) != null)
                {
                    if (f.removequat(f.return_service_by_id(i).company.companyID) == f.removequat(this.com.companyID))
                    {
                        this.mono++;
                        service newone = f.return_service_by_id(i);
                        ComboBoxItem item = new ComboBoxItem();
                        item.Content = newone.get_the_name();
                        item.Tag = newone.get_the_service_id();
                        this.selectservicecb.Items.Add(item);
                    }
                }

            }

        }
        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            compnaymanage cm = new compnaymanage(this.com, this.use);
            cm.Show();
            this.Close();
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.mono > 0)
            {
                if (f.CheckForInternetConnection())
                {
                    if (selectservicecb.SelectedItem == null)
                    {
                        MessageBox.Show("אנא בחר מהרישמה");
                    }
                    else
                    { 
                    string selected = ((ComboBoxItem)selectservicecb.SelectedItem).Tag.ToString();
                    int selecttonum = f.removequat(selected);
                    service se = f.return_service_by_id(selecttonum);
                    serviceBox sb = new serviceBox(this.com, this.use, se);
                    sb.Show();
                    this.Close();
                    }
                }
                else
                { MessageBox.Show("אין חיבור לאינטרנט"); }
            }
            else
            { MessageBox.Show("אין רשומות"); }

        }

    }
}
