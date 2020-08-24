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
    /// Interaction logic for employeesBox.xaml
    /// </summary>
    public partial class employeesBox : Window
    {
        function f = new function();
        company c;
        user use;
        public employeesBox(company c, user u)
        {
            InitializeComponent();
            this.c = c;
            this.use = u;
            f.define_project_base("https://ana10project.firebaseio.com");

        }



        public string dateforjoin()
        {
            string ddd = DateTime.Now.ToString("MM/dd/yyyy");
            ddd = ddd.Replace('/', '.');
            string[] words = ddd.Split('.');
            ddd = words[1] + "." + words[0] + "." + words[2];
            return ddd;
        }

        public bool CheckIDNo(string strID)
        {
            int[] id_12_digits = { 1, 2, 1, 2, 1, 2, 1, 2, 1 };
            int count = 0;

            if (strID == null)
                return false;

            strID = strID.PadLeft(9, '0');

            for (int i = 0; i < 9; i++)
            {
                int num = Int32.Parse(strID.Substring(i, 1)) * id_12_digits[i];

                if (num > 9)
                    num = (num / 10) + (num % 10);

                count += num;
            }

            return (count % 10 == 0);
        }

        public int checkIfThereIsNOBLankTB()

        {
            int res = 0;
            if(String.IsNullOrEmpty(fnametb.Text) || String.IsNullOrEmpty(lnametb.Text)|| String.IsNullOrEmpty(idtb.Text) || String.IsNullOrEmpty(adresstb.Text) || String.IsNullOrEmpty(phonetb.Text) || String.IsNullOrEmpty(passwordBox.Password.ToString()))
            {
                
                res++;
            }
           if(res==0)
            {
                //check errors
                bool containsInt = fnametb.Text.Any(char.IsDigit);
                bool cont_spaical = f.CheckSpacialCherter(fnametb.Text);
                if (containsInt == true || cont_spaical == true) { res++; }
                if (f.CheckIDNo(idtb.Text) == false) { res++; }
                containsInt = lnametb.Text.Any(char.IsDigit);
                cont_spaical = f.CheckSpacialCherter(lnametb.Text);
                if (containsInt == true || cont_spaical == true) { res++; }
                bool conset_phone_digit_check = f.IsDigitsOnly(phonetb.Text);
                if (conset_phone_digit_check == false) { res++; }

            }


            return res;

        }

        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            compnaymanage cm = new compnaymanage(this.c, this.use);
            cm.Show();
            this.Close();
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            if(f.CheckForInternetConnection())
            {
                if(checkIfThereIsNOBLankTB()==0)
                {

                    if(CheckIDNo(idtb.Text))
                    {
                        employee ee = new employee();
                        int getid = f.num_of_mone("employees", "moneemployees");
                        getid++;
                        ee.employee_id = getid;
                        ee.adress = adresstb.Text;
                        ee.company = this.c;
                        ee.dateofjoin = dateforjoin();
                        ee.employee_id_number = idtb.Text;
                        ee.firstname = this.fnametb.Text;
                        ee.lastname = this.lnametb.Text;
                        ee.password = f.strEncryptred(this.passwordBox.Password.ToString());
                        ee.geolocation = "32.291347,34.8620103";
                        ee.phone = this.phonetb.Text;
                        f.commit_employee(ee);
                        f.commit("employees", "{\"moneemployees\":" + getid + "}");
                        MessageBox.Show("בוצע בהצלחה!");
                        compnaymanage cm = new compnaymanage(this.c, this.use);
                        cm.Show();
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("תעודת זהות אינה תקינה");
                    }
                }
                else
                {
                    MessageBox.Show("שים לב שיש שדות רקים");
                }

            }
            else
            {
                MessageBox.Show("אין אינטרנט");

            }
        }
    }
}
