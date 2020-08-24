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
    /// Interaction logic for change_tech_manul_select.xaml
    /// </summary>
    public partial class change_tech_manul_select : Window
    {
        function f = new function();
        company com;
        user use;
        Call cll;
        public change_tech_manul_select(company c, user us,Call ca)
        {
            InitializeComponent();
            f.define_project_base("https://ana10project.firebaseio.com");
            this.com = c;
            this.use = us;
            this.cll = ca;
            define_comboBox();

        }

        public void define_comboBox()
        {
            if(f.CheckForInternetConnection())
            {
                int mone = f.num_of_mone("employees", "moneemployees");
                for(int i=1;i<mone+1;i++)
                {
                    if(f.return_employee_by_id(i)!=null)
                    {
                        if(f.removequat(f.return_employee_by_id(i).company.companyID)==f.removequat(this.com.companyID))
                        {
                            employee newone = f.return_employee_by_id(i);
                            ComboBoxItem item = new ComboBoxItem();
                            item.Content = newone.firstname+" "+newone.lastname;
                            item.Tag = i;
                            generalcb.Items.Add(item);
                        }
                    }
                }

            }
            else
            { MessageBox.Show("אין חיבור לרשת"); }
        }
        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            if (f.CheckForInternetConnection()) { 
            string selected = ((ComboBoxItem)generalcb.SelectedItem).Tag.ToString();
            if(selected.ToString() != "")
            {
                    this.cll.techid = f.return_employee_by_id(f.removequat(selected));
                    string mystringCorrect = f.getapitodelmanuel(this.cll.call_id + "", f.removequat(selected) + "");
                    string[] strArr = null;
                    char[] splitchar = { ':' };
                    strArr = mystringCorrect.Split(splitchar);
                    strArr[2] = strArr[2].Replace("}", "");
                    string thedate = strArr[1] + ":" + strArr[2];
                    this.cll.date_of_arrive = thedate;
                    MessageBox.Show("בוצע בהצלחה");
                    editCallWindow ecw = new editCallWindow(this.com, this.use, this.cll);
                    ecw.Show();
                    this.Close();

                }
            }
            else
            {
                MessageBox.Show("אין גישה לרשת");
            }
        }

        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            editCallWindow ecw = new editCallWindow(this.com, this.use, this.cll);
            ecw.Show();
            this.Close();
        }

        private void calcbtn_Click(object sender, RoutedEventArgs e)
        {
            if (f.CheckForInternetConnection()) {
                string selected = ((ComboBoxItem)generalcb.SelectedItem).Tag.ToString();
                if (selected.ToString() != "") {
                    string mystringCorrect = f.getapitodelmanuel(this.cll.call_id + "", f.removequat(selected) + "");
                    string[] strArr = null;
                    char[] splitchar = { ':' };
                    strArr = mystringCorrect.Split(splitchar);
                    strArr[2] = strArr[2].Replace("}", "");
                    string thedate = strArr[1]+":"+ strArr[2];
                    generallbl.Content += thedate;
                }
            }
            else
            {
                MessageBox.Show("אין גישה לרשת");
            }


        }
    }
}
