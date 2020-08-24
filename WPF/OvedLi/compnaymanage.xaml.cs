using Newtonsoft.Json;
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
    /// Interaction logic for compnaymanage.xaml
    /// </summary>
    public partial class compnaymanage : Window
    {
        
        company com;
        user use;
        public compnaymanage(company c , user u)
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            this.com = c;
            this.use = u;
            this.Title = "ברוך הבא :" + use.getusername() + " בחברת " + com.companyName;
            loadtimeclock.Visibility = Visibility.Hidden;
            var image = new BitmapImage();
            int BytesToRead = 100;
            define_stats();
            WebRequest request = WebRequest.Create(new Uri(this.com.companyLogo, UriKind.Absolute));
            request.Timeout = -1;
            try
            {
                WebResponse response = request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                BinaryReader reader = new BinaryReader(responseStream);
                MemoryStream memoryStream = new MemoryStream();

                byte[] bytebuffer = new byte[BytesToRead];
                int bytesRead = reader.Read(bytebuffer, 0, BytesToRead);

                while (bytesRead > 0)
                {
                    memoryStream.Write(bytebuffer, 0, bytesRead);
                    bytesRead = reader.Read(bytebuffer, 0, BytesToRead);
                }

                image.BeginInit();
                memoryStream.Seek(0, SeekOrigin.Begin);

                image.StreamSource = memoryStream;
                image.EndInit();
            }
            catch(Exception ex)
            {
                 request = WebRequest.Create(new Uri("https://wingslax.com/wp-content/uploads/2017/12/no-image-available.png", UriKind.Absolute));
                request.Timeout = -1;
                WebResponse response = request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                BinaryReader reader = new BinaryReader(responseStream);
                MemoryStream memoryStream = new MemoryStream();

                byte[] bytebuffer = new byte[BytesToRead];
                int bytesRead = reader.Read(bytebuffer, 0, BytesToRead);

                while (bytesRead > 0)
                {
                    memoryStream.Write(bytebuffer, 0, bytesRead);
                    bytesRead = reader.Read(bytebuffer, 0, BytesToRead);
                }

                image.BeginInit();
                memoryStream.Seek(0, SeekOrigin.Begin);

                image.StreamSource = memoryStream;
                image.EndInit();
            }

            comlogo.Source = image;








        }

        public void define_stats()
        {
            int services_counter = 0;
            int open_call = 0;
            int closed_call = 0;
            int money = 0;
            int mone_users = 0;
            int mone_employeee = 0;
            function f = new function();
            f.define_project_base("https://ana10project.firebaseio.com");
            string testfortest = f.get("");
            var json = JsonConvert.DeserializeObject<dynamic>(testfortest);
            //var jsonemployees = JsonConvert.DeserializeObject<dynamic>(json.calls.ChildrenTokens[0]);
            int mone = f.faster_getmone(testfortest, "moneemployees");
             //mone = f.num_of_mone("employees", "moneemployees");
            for(int i=1;i<mone+1;i++)
            {
                
                //if (f.return_employee_by_id(i)!=null)
                if (f.faster_getemployeebyid(testfortest, i) != null)
                {
                    if(f.removequat(f.faster_getemployeebyid(testfortest, i).company.companyID)==f.removequat(this.com.companyID))
                    { mone_employeee++; }
                }
            }
            this.employstslbl.Content += mone_employeee+"";

            //mone = f.num_of_mone("users", "usersmone");
            mone = f.faster_getmone(testfortest, "usersmone");
            for (int i=1;i<mone+1;i++)
            {
                
                //if(f.retrunuserbyid(i)!=null)
                if (f.fast_getuserbyid(testfortest, i) != null)
                {
                    if(f.removequat(f.fast_getuserbyid(testfortest, i).copmanyID)==f.removequat(this.com.companyID))
                    {
                        mone_users++;
                    }
                }
            }
            this.usersstslbl.Content += mone_users + "";
            
           // mone = f.num_of_mone("calls", "callsmone");
            mone = f.faster_getmone(testfortest, "callsmone");
            for (int i=1;i<mone+1;i++)
            {
               
                //if (f.set_call_by_id(i)!=null)
                if(f.fast_callbyid(testfortest,i) != null)
                {
                    //if(f.removequat(f.set_call_by_id(i).serviceid.company.companyID)==f.removequat(this.com.companyID))
                      if (f.removequat(f.fast_callbyid(testfortest, i).serviceid.company.companyID) == f.removequat(this.com.companyID))
                        {
                        //if(f.removequat(f.set_call_by_id(i).issolved)==0)
                        if (f.removequat(f.fast_callbyid(testfortest, i).issolved) == 0)
                            {
                            open_call++;
                        }
                        else
                        {
                            closed_call++;
                            money += f.removequat(f.set_call_by_id(i).serviceid.price);
                        }
                    }
                }
            }

            
            this.totalcallstslbl.Content += (closed_call+open_call)+" ";
            this.opencallstslbl.Content += open_call + "";
            this.closedcallstslbl.Content += closed_call + "";
            this.erencallstslbl.Content += money + " ₪";
            //mone = f.num_of_mone("services", "moneServices");
            mone = f.faster_getmone(testfortest, "moneServices");
            for (int i=1;i<mone+1;i++)
            {
                
                //if(f.return_service_by_id(i)!=null)
                if (f.fast_servicebyid(testfortest, i) != null)
                {
                    //if(f.removequat(f.return_service_by_id(i).company.companyID)==f.removequat(this.com.companyID))
                    if (f.removequat(f.fast_servicebyid(testfortest, i).company.companyID) == f.removequat(this.com.companyID))
                    {
                        services_counter++;
                    }
                    
                }
            }

            
            this.servicestslbl.Content += services_counter + "";


        }
        private void picturereplace_Click(object sender, RoutedEventArgs e)
        {
            loadtimeclock.Visibility = Visibility.Visible;
            img_company_replace icr = new img_company_replace(this.com, this.use);
            icr.Show();
            this.Close();

        }

        private void addservicebtn_Click(object sender, RoutedEventArgs e)
        {
            loadtimeclock.Visibility = Visibility.Visible;
            serviceBox sb = new serviceBox(this.com, this.use);
            sb.Show();
            this.Close();
        }

        private void editselectservice_Click(object sender, RoutedEventArgs e)
        {
            loadtimeclock.Visibility = Visibility.Visible;
            serviceselect servicesel = new serviceselect(this.com, this.use);
            servicesel.Show();
            this.Close();
        }

        private void editemployee_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addemployeebtn_Click(object sender, RoutedEventArgs e)
        {
            loadtimeclock.Visibility = Visibility.Visible;
            employeesBox eb = new employeesBox(this.com, this.use);
            eb.Show();
            this.Close();
        }

        private void employeesselectbtn_Click(object sender, RoutedEventArgs e)
        {
            loadtimeclock.Visibility = Visibility.Visible;
            selectEmployee se = new selectEmployee(this.com, this.use);
            se.Show();
            this.Close();
        }

        private void callsbtn_Click(object sender, RoutedEventArgs e)
        {
            loadtimeclock.Visibility = Visibility.Visible;
            callsWindow cw = new callsWindow(this.com, this.use);
            cw.Show();
            this.Close();
        }

        private void users_Click(object sender, RoutedEventArgs e)
        {
            loadtimeclock.Visibility = Visibility.Visible;
            mangeComnyUsers mcu = new mangeComnyUsers(this.com, this.use);
            mcu.Show();
            this.Close();
        }

        private void disconnect_Click(object sender, RoutedEventArgs e)
        {
            loadtimeclock.Visibility = Visibility.Visible;
            MessageBox.Show("נותק בהצלחה");
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
    }
}
