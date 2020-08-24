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
    /// Interaction logic for edituser.xaml
    /// </summary>
    public partial class edituser : Window
    {
        function f = new function();
        company c;
        user u;
        int mooo = 0;
        int editforuser = 0;
        public edituser()
        {
            
            InitializeComponent();
            f.define_project_base("https://ana10project.firebaseio.com");
            defineComBoxBox();

        }

        public edituser(company cc)
        {
            this.editforuser = 1;
            this.c = cc;
            InitializeComponent();
            f.define_project_base("https://ana10project.firebaseio.com");
            generallbl.Content = "בחר משתמש";
            
            defineComBoxBox();
        }
        public edituser(int a)
        {
            this.editforuser = 2;
            InitializeComponent();
            f.define_project_base("https://ana10project.firebaseio.com");
            generallbl.Content = "בחר משתמש";
            defineComBoxBox();
        }
       /* public edituser(user uu)
        {
            this.editforuser = 3;
            this.u = uu;
            InitializeComponent();
            f.define_project_base("https://ana10project.firebaseio.com");
            if(u.copmanyID!=0)
            { 
                this.c =f.set_Company(this.u.copmanyID);
            }

        }
        */
        public void defineComBoxBox()
        {
            int mone = f.num_of_mone("company", "mone");
            
            //in the first tsep
            if(this.editforuser==0)
            {
                generallbl.Content = "בחר חברה";
                ComboBoxItem item = new ComboBoxItem();
                item.Content = "משתמשי על";
                item.Tag = 0;
                generalcb.SelectedIndex = 0;
                generalcb.Items.Add(item);
                for(int i=1;i<mone+1;i++)
                {
                    if(f.set_Company(i)!=null)
                    {
                        mooo = mooo+1;
                        company newone = f.set_Company(i);
                    item = new ComboBoxItem();
                    item.Content = newone.companyName;
                    item.Tag = newone.companyID;
                    generalcb.Items.Add(item);
                    }

                }
            }
            if(this.editforuser==1)
            {
                
                mone =f.num_of_mone("users", "usersmone");
               
                for (int i = 1; i < mone + 1; i++)
                {
                    if (f.retrunuserbyid(i) != null)
                    {
                        if(f.removequat(f.retrunuserbyid(i).getcopmanyID())==f.removequat(c.companyID))
                        {
                            mooo = mone+1;
                            user newone = f.retrunuserbyid(i);
                            ComboBoxItem item = new ComboBoxItem();
                            item.Content = newone.getusername();
                            item.Tag = newone.getuserid();
                            generalcb.Items.Add(item);
                        }
                    }

                }



            }
            if(this.editforuser==2)
            {
                mone = f.num_of_mone("users", "usersmone");
                mooo = mone;
                for (int i = 1; i < mone + 1; i++)
                {
                    if (f.retrunuserbyid(i) != null)
                    {
                        if (f.removequat(f.retrunuserbyid(i).getcopmanyID()) == f.removequat(0))
                        {
                            mooo = mone + 1;
                            user newone = f.retrunuserbyid(i);
                            ComboBoxItem item = new ComboBoxItem();
                            item.Content = newone.getusername();
                            item.Tag = newone.getuserid();
                            generalcb.Items.Add(item);
                        }
                    }

                }
            }

            /* company newone = f.set_Company(i);
             ComboBoxItem item = new ComboBoxItem();
             item.Content = newone.companyName;
             item.Tag = newone.companyID;
             companyEditComboBox.SelectedIndex = 0;
             companyEditComboBox.Items.Add(item);
             */

        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            loader.Content = "טוען..";
            if(mooo > 0) { 
            if (f.CheckForInternetConnection()) {
                
            string selected = ((ComboBoxItem)generalcb.SelectedItem).Tag.ToString();
            if (editforuser==0)
            {
                if(f.removequat(selected)==0)
                {
                    edituser ed = new edituser(0);
                    ed.Show();
                    this.Close();

                }
                else
                {
                    edituser ed = new edituser(f.set_Company(f.removequat(selected)));
                    ed.Show();
                    this.Close();
                }


            }
            if(editforuser==1|| editforuser==2)
            {
                userbox ed = new userbox(f.retrunuserbyid(f.removequat(selected)));
                ed.Show();
                this.Close();
            }
            }
            else {
                MessageBox.Show("אין אינטרנט");
                    loader.Content = "";

                }
            }
            else
            {
                MessageBox.Show("אין רשומות");
                loader.Content = "";
            }
        }

        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            superuser su = new superuser();
            su.Show();
            this.Close();
        }
    }
}
