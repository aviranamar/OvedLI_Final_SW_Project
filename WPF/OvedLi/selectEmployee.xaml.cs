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
    /// Interaction logic for selectEmployee.xaml
    /// </summary>
    public partial class selectEmployee : Window
    {
        function f = new function();
        company c;
        user u;
        public selectEmployee(company c,user u)
        {
            InitializeComponent();
            f.define_project_base("https://ana10project.firebaseio.com");
            this.c = c;
            this.u = u;
            defineComBoxBox();
        }



        public void defineComBoxBox()
        {
            int mone = f.num_of_mone("employees", "moneemployees");
            for(int i=1;i<mone+1;i++)
            {
                if(f.return_employee_by_id(i)!=null)
                {
                    if(f.removequat(this.c.companyID)== f.removequat(f.return_employee_by_id(i).company.companyID))
                    {
                        employee newone = f.return_employee_by_id(i);
                        ComboBoxItem item = new ComboBoxItem();
                        item.Content = newone.firstname+" " + newone.lastname+" - "+ newone.employee_id_number;
                        item.Tag = newone.employee_id;
                        emcb.Items.Add(item);
                    }
                }
            }
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            if (f.CheckForInternetConnection())
            {
                if (emcb.SelectedItem == null)
                { MessageBox.Show("אנא בחר שם מהרשימה"); }
                else { 
                string selected = ((ComboBoxItem)emcb.SelectedItem).Tag.ToString();
                employee em = f.return_employee_by_id(f.removequat(selected));
                employeeboxedit ebe = new employeeboxedit(this.c, this.u, em);
                ebe.Show();
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
            compnaymanage cm = new compnaymanage(this.c, this.u);
            cm.Show();
            this.Close();
        }
    }
}
