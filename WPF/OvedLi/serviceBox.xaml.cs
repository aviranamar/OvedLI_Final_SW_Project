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
    /// Interaction logic for serviceBox.xaml
    /// </summary>
    public partial class serviceBox : Window
    {
        company com;
        user use;
        service se;
        function f=new function();
        int editforuse = 0;
        public serviceBox(company c, user u)
        {
            InitializeComponent();
            f.define_project_base("https://ana10project.firebaseio.com");
            this.com = c;
            this.use = u;
            this.Title = "הוספת שירות";
            this.nametb.Text= "";
            this.pricetb.Text = ""+0;
            this.timetb.Text = 0 + "";
            this.deletebtn.IsEnabled = false;
            this.deletebtn.Visibility = Visibility.Hidden;

        }
        public serviceBox(company c, user u,service s)
        {
            InitializeComponent();
            f.define_project_base("https://ana10project.firebaseio.com");
            this.com = c;
            this.use = u;
            this.se = s;
            this.editforuse = 1;
            this.Title = "עריכת שירות : "+ se.name;
            this.nametb.Text = s.name;
            this.pricetb.Text = f.removequat(s.price)+"";
            this.timetb.Text = f.removequat(s.time) + "";



        }

        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            compnaymanage cm = new compnaymanage(this.com, this.use);
            cm.Show();
            this.Close();
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            //checks setp
            string name = this.nametb.Text;
            int flagForExeption = 0;
            int parsedValue;
            int price = 0;
            int time = 0;
            if (!int.TryParse(this.pricetb.Text, out parsedValue))
            {
                MessageBox.Show("מחיר יכול להיות מספר או מספר שלם בלבד");
                flagForExeption++;
            }
            else
            {  price = f.removequat(this.pricetb.Text); }
            if (!int.TryParse(this.timetb.Text, out parsedValue))
            {
                MessageBox.Show("זמן יכול להיות מספר או מספר שלם בלבד");
                flagForExeption++;
            }
            else
            {  time = f.removequat(this.timetb.Text); }
            if(name=="" || this.pricetb.Text=="" || this.timetb.Text=="")
            {
                MessageBox.Show("השדות לא יכולים להיות ריקים");
                flagForExeption++;
            }
            if(flagForExeption==0)
            {
                if(price<=0)
                {
                    MessageBox.Show("המחיר חייב להיות גדול מ0");
                    flagForExeption++;
                }
                if (time <= 0)
                {
                    MessageBox.Show("הזמן חייב להיות גדול מ0");
                    flagForExeption++;
                }
            }
            if (flagForExeption == 0) { 
            if (f.CheckForInternetConnection())
            {
                if(this.editforuse==0)
                    {
                        int mone = 0;
                        mone = f.removequat(f.num_of_mone("services", "moneServices"));
                        this.se = new service(mone + 1, this.com, name, price, time);
                        f.commit_service(this.se);
                        //add to mone +1
                        mone++;
                        f.commit("services", "{\"moneServices\":"+mone+"}");
                    }
                    if (this.editforuse == 1)
                    {
                        this.se.name = name;
                        this.se.price =f.removequat(price);
                        this.se.time =f.removequat(time);

                        f.commit_service(this.se);
                        //add to mone +1
                       
                    }
                    MessageBox.Show("בוצע בהצלחה");
                    compnaymanage cm = new compnaymanage(this.com, this.use);
                    cm.Show();
                    this.Close();
                }
            else
            { MessageBox.Show("אין חיבור לאינטרנט"); }
            }
        }

        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {
            if(f.CheckForInternetConnection())
            {
                f.del_service(this.se);
                MessageBox.Show("בוצע בהצלחה");
                compnaymanage cm = new compnaymanage(this.com, this.use);
                cm.Show();
                this.Close();
            }
            else
            { MessageBox.Show("אין חיבור לאינטרנט"); }
        }
    }
}
