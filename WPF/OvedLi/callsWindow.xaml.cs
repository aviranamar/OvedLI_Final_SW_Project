using System;
using System.Collections;
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
    /// Interaction logic for callsWindow.xaml
    /// </summary>
    public partial class callsWindow : Window
    {
        function f = new function();
        company com;
        user use;
        int emp_count = 0;
        string testfortest;

        public callsWindow(company c, user e)
        {
            InitializeComponent();
            loadtimeclock.Visibility = Visibility.Hidden;
            WindowState = WindowState.Maximized;
            f.define_project_base("https://ana10project.firebaseio.com");
            this.com = c;
            this.use = e;
            if (!f.CheckForInternetConnection())
            {
                MessageBox.Show("אין חיבור לאינטרנט");
                compnaymanage cm = new compnaymanage(this.com, this.use);
                cm.Show();
                this.Close();
            }
            this.testfortest = f.get("");
            defineAllControllers();

        }

        public void defineAllControllers()
        {
            //strat to define employyee combo box
            
            //int employee_mone = f.num_of_mone("employees", "moneemployees");
            int employee_mone = f.faster_getmone(testfortest, "moneemployees");
            if (employee_mone!=0)
            {
                for(int i=1;i<employee_mone+1;i++)
                {
                    //if(f.return_employee_by_id(i)!=null)
                      if (f.faster_getemployeebyid(testfortest,i) != null)
                        {
                        if(f.removequat(f.faster_getemployeebyid(testfortest, i).company.companyID)==f.removequat(com.companyID))
                        {
                            emp_count++;
                            employee newone = f.faster_getemployeebyid(testfortest, i);
                            ComboBoxItem item = new ComboBoxItem();
                            item.Content = newone.firstname+" "+newone.lastname+" - "+newone.employee_id_number;
                            item.Tag = newone.employee_id;
                            this.callbyemployeecb.Items.Add(item);
                            
                            item = new ComboBoxItem();
                            item.Content = newone.firstname + " " + newone.lastname + " - " + newone.employee_id_number;
                            item.Tag = newone.employee_id;
                            this.callbyemployeecb_closecb.Items.Add(item);
                        }
                    }
                }

            }
            //end the define employyee combo box 

        }

        private void callsbyemployeebtn_Click(object sender, RoutedEventArgs e)
        {
            if (f.CheckForInternetConnection())
            {
                loadtimeclock.Visibility = Visibility.Visible;
                if (((ComboBoxItem)callbyemployeecb.SelectedItem) != null) { 
                sp.Children.Clear();
                if (emp_count > 0)
                {
                    int moneofemplyee = 0;
                    string selected = ((ComboBoxItem)callbyemployeecb.SelectedItem).Tag.ToString();
                    int selecttonum = f.removequat(selected);
                        //int callmone = f.num_of_mone("calls", "callsmone");
                        int callmone = f.faster_getmone(testfortest, "callsmone");
                        if (callmone>0)
                    {
                        for(int i=1;i<callmone+1;i++)
                        {
                                Call callnew = f.fast_callbyid(testfortest, i);
                            if(callnew != null)
                            {
                                    if (callnew.techid != null) { 
                                if (f.removequat(callnew.techid.employee_id) == selecttonum)
                                {
                                    if (f.removequat(callnew.issolved) == 0) {
                                        moneofemplyee++;
                                        Button button = new Button();
                                        button.Content = "תאריך פתיחה: " + callnew.date_of_open + "כתובת: " + callnew.geolocation;
                                        button.Tag = i;
                                        button.Click += new RoutedEventHandler(EditCall);
                                        this.sp.Children.Add(button);
                                    }


                                }
                                    }
                                }
                        }
                            loadtimeclock.Visibility = Visibility.Hidden;
                            if (moneofemplyee==0)
                        {
                                loadtimeclock.Visibility = Visibility.Hidden;
                                Label lbl = new Label();
                            lbl.Content = "אין קריאות פתוחות לעובד זה ";
                            this.sp.Children.Add(lbl);

                        }
                        
                        
                    }
                   

                    else
                    {
                            loadtimeclock.Visibility = Visibility.Hidden;
                            MessageBox.Show("אין קריאות למשתמש זה");
                    }
                }
                else
                {
                        loadtimeclock.Visibility = Visibility.Hidden;
                        MessageBox.Show("אין עובדים כרגע");
                }
                }
                else
                {
                    loadtimeclock.Visibility = Visibility.Hidden;
                    MessageBox.Show("אנא בחר עובד קודם"); }
            }
            else
            {
                loadtimeclock.Visibility = Visibility.Hidden;
                MessageBox.Show("אין חיבור לאינטרנט");
            }
        }

        private void EditCall(object sender, RoutedEventArgs e)
        {
            loadtimeclock.Visibility = Visibility.Visible;
            Button clicked = (Button)sender;
            int num = f.removequat(clicked.Tag.ToString());
            editCallWindow ecw = new editCallWindow(this.com, this.use, f.set_call_by_id(num));
            ecw.Show();
            this.Close();

        }

        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            loadtimeclock.Visibility = Visibility.Visible;
            compnaymanage cm = new compnaymanage(this.com, this.use);
            cm.Show();
            this.Close();

        }

        private void callsbyemployeebtn_closetb_Click(object sender, RoutedEventArgs e)
        {
            if (f.CheckForInternetConnection())
            {
                loadtimeclock.Visibility = Visibility.Visible;
                if (((ComboBoxItem)callbyemployeecb_closecb.SelectedItem) != null)
                {
                    sp.Children.Clear();

                    if (emp_count > 0)
                    {
                        int moneofemplyee = 0;
                        string selected = ((ComboBoxItem)callbyemployeecb_closecb.SelectedItem).Tag.ToString();
                        int selecttonum = f.removequat(selected);
                        //int callmone = f.num_of_mone("calls", "callsmone");
                        int callmone = f.faster_getmone(testfortest, "callsmone");
                        if (callmone > 0)
                        {
                            for (int i = 1; i < callmone + 1; i++)
                            {
                                Call callnew = f.fast_callbyid(testfortest, i);
                                if (callnew != null)
                                {
                                    if (callnew.techid != null)
                                    {
                                        if (f.removequat(callnew.techid.employee_id) == selecttonum)
                                        {
                                            if (f.removequat(callnew.issolved) == 1)
                                            {
                                                moneofemplyee++;
                                                Button button = new Button();
                                                button.Content = "תאריך פתיחה: " + callnew.date_of_open + "כתובת: " + callnew.geolocation;
                                                button.Tag = i;
                                                button.Click += new RoutedEventHandler(EditCall);
                                                this.sp.Children.Add(button);
                                            }


                                        }
                                    }
                                }
                            }
                            loadtimeclock.Visibility = Visibility.Hidden;
                            if (moneofemplyee == 0)
                            {
                                loadtimeclock.Visibility = Visibility.Hidden;
                                Label lbl = new Label();
                                lbl.Content = "אין קריאות פתוחות לעובד זה ";
                                this.sp.Children.Add(lbl);

                            }


                        }


                        else
                        {
                            loadtimeclock.Visibility = Visibility.Hidden;
                            MessageBox.Show("אין קריאות למשתמש זה");
                        }
                    }
                    else
                    {
                        loadtimeclock.Visibility = Visibility.Hidden;
                        MessageBox.Show("אין עובדים כרגע");
                    }
                }
                else
                {
                    loadtimeclock.Visibility = Visibility.Hidden;
                    MessageBox.Show("אנא בחר עובד קודם"); }
            }
            else
            {
                loadtimeclock.Visibility = Visibility.Hidden;
                MessageBox.Show("אין חיבור לאינטרנט");
            }
        }

        private void notechbtn_Click(object sender, RoutedEventArgs e)
        {
            if (f.CheckForInternetConnection())
            {
                loadtimeclock.Visibility = Visibility.Visible;
                sp.Children.Clear();

                if (emp_count > 0)
                {
                    int moneofemplyee = 0;
                    //int callmone = f.num_of_mone("calls", "callsmone");
                    int callmone = f.faster_getmone(testfortest, "callsmone");
                    if (callmone > 0)
                    {
                        for (int i = 1; i < callmone + 1; i++)
                        {
                            Call newone = f.fast_callbyid(testfortest, i);
                            if (newone != null)
                            {
                                if (f.removequat(newone.serviceid.company.companyID) == f.removequat(this.com.companyID)) {
                                if (newone.techid == null)
                                {
                                   
                                        moneofemplyee++;
                                        Button button = new Button();
                                        button.Content = "תאריך פתיחה: " + newone.date_of_open + "כתובת: " + newone.geolocation;
                                        button.Tag = i;
                                        button.Click += new RoutedEventHandler(EditCall);
                                        this.sp.Children.Add(button);
                                    


                                }
                                }
                            }
                        }
                        loadtimeclock.Visibility = Visibility.Hidden;
                        if (moneofemplyee == 0)
                        {
                            loadtimeclock.Visibility = Visibility.Hidden;
                            Label lbl = new Label();
                            lbl.Content = "אין קריאות ללא עובדים ";
                            this.sp.Children.Add(lbl);

                        }


                    }


                    else
                    {
                        loadtimeclock.Visibility = Visibility.Hidden;
                        MessageBox.Show("אין קריאות למשתמש זה");
                    }
                }
                else
                {
                    loadtimeclock.Visibility = Visibility.Hidden;
                    MessageBox.Show("אין עובדים כרגע");
                }
            }
            else
            {
                loadtimeclock.Visibility = Visibility.Hidden;
                MessageBox.Show("אין חיבור לאינטרנט");
            }
        }

        private void searchbtn_Click(object sender, RoutedEventArgs e)
        {
            if(f.CheckForInternetConnection())
            {
                loadtimeclock.Visibility = Visibility.Visible;
                sp.Children.Clear();
                if(this.serchtb.Text!="")
                {
                    if(f.IsDigitsOnly(this.serchtb.Text))
                    {
                        Call newone = f.fast_callbyid(testfortest, f.removequat(this.serchtb.Text));
                        if(f.removequat(newone.call_id)!=0)
                        {
                            Button button = new Button();
                            button.Content = "תאריך פתיחה: " + newone.date_of_open + "כתובת: " + newone.geolocation;
                            button.Tag = f.removequat(this.serchtb.Text) + "";
                            button.Click += new RoutedEventHandler(EditCall);
                            this.sp.Children.Add(button);
                            loadtimeclock.Visibility = Visibility.Hidden;
                        }
                        else
                        {
                            loadtimeclock.Visibility = Visibility.Hidden;
                            MessageBox.Show("לא נמצאה קריאה"); }
                    }
                    else
                    {
                        loadtimeclock.Visibility = Visibility.Hidden;
                        MessageBox.Show("אנא הכנס ספרות בלבד"); }
                }
                else
                {
                    loadtimeclock.Visibility = Visibility.Hidden;
                    MessageBox.Show("אנא הכנס ערך"); }
            }
            else
            {
                loadtimeclock.Visibility = Visibility.Hidden;
                MessageBox.Show("אין חיבור לאינטרנט"); }
        }
    }
}
