package avishayandaviran.ovedli;

import android.os.StrictMode;

import com.google.firebase.database.DataSnapshot;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.URL;



public class function {
    public DataSnapshot dataSnapshot;

    public function() {

        StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
        StrictMode.setThreadPolicy(policy);

    }

    public boolean isNetworkAvailable() {
        try {

            URL url = new URL("http://clients3.google.com/generate_204");
            BufferedReader reader = new BufferedReader(new InputStreamReader(url.openStream()));
            String line;

            reader.close();
            return true;
        } catch (Exception e) {
            return false;

        }


    }

    public String encriptString(String str) {
        String res = "";
        if (isNetworkAvailable() == true) {


            try {
                URL url = new URL("http://ovedli.orgfree.com/dec.php?string=" + str);
                BufferedReader reader = new BufferedReader(new InputStreamReader(url.openStream()));
                String line;
                while ((line = reader.readLine()) != null) {
                    res += line;
                }
                reader.close();
            } catch (Exception e) {

            }
        } else {
            res = "";
        }


        return res;
    }

    public Employee Return_employee_by_ID(String userid) {
        Employee newone = null;

        if (isNetworkAvailable() == true) {
            String res = "";
            try {
                URL url = new URL("http://ovedli.orgfree.com/api.php?action=returnemployee&empid=" + userid);
                BufferedReader reader = new BufferedReader(new InputStreamReader(url.openStream()));
                String line;
                while ((line = reader.readLine()) != null) {
                    res += line;
                }
                reader.close();
            } catch (Exception e) {

            }
            if( !res .equals( "null"))
            {
                if (!res.equals("")) {
                    String[] separated = res.split("~");
                    newone = new Employee(Integer.parseInt(separated[0]), separated[1], Integer.parseInt(separated[2]), separated[3], separated[4], separated[5], separated[6], Integer.parseInt(separated[7]), Integer.parseInt(separated[8]), separated[9], separated[10], separated[11]);

                }
            }

        }

        return newone;
    }



    public int Check_If_User_and_Pass_are_correct(String user, String Pass)
    {
        int res=0;
        //FirebaseDatabase database = FirebaseDatabase.getInstance();
        //DatabaseReference myRef = database.getReference("employees");
        if(Return_employee_by_ID(user)!=null)
        {
            Employee  temp =Return_employee_by_ID(user);
            String Temppass=temp.password.toString();
            if(Temppass.equals(Pass))
            {
                res=temp.id_employee;

            }
        }




        return res;
    }


    public void updategps(int userid,String locat)
    {
        if (isNetworkAvailable() == true) {

            String res="";
            try {
                URL url = new URL("http://ovedli.orgfree.com/api.php?action=updaegps&empid=" + userid+"&location="+locat);
                BufferedReader reader = new BufferedReader(new InputStreamReader(url.openStream()));
                String line;
                while ((line = reader.readLine()) != null) {
                    res += line;
                }
                reader.close();
            } catch (Exception e) {

            }


        }

    }


    public Call [] returnAllTheCalls(String empid,int tommorow)

    {
        Call [] calllist=null;
        if(this.isNetworkAvailable())
        {

            String res="";
            try {
                URL url = new URL("http://ovedli.orgfree.com/api.php?action=showcalls&empid="+empid+"&tommorow="+tommorow);
                BufferedReader reader = new BufferedReader(new InputStreamReader(url.openStream()));
                String line;
                while ((line = reader.readLine()) != null) {
                    res += line;
                }
                reader.close();
            } catch (Exception e) {

            }

            if(!res.equals("")){
            String[] Num_of_recors=res.split(";");
            calllist=new Call[Num_of_recors.length];
            for(int i=0;i<Num_of_recors.length;i++)
            {
                String[] StringtoArry=Num_of_recors[i].split("~");
                calllist[i]=new Call(Integer.parseInt(StringtoArry[0]),StringtoArry[1],StringtoArry[2],StringtoArry[3],StringtoArry[4],StringtoArry[5],StringtoArry[6],StringtoArry[7],StringtoArry[8],StringtoArry[9],StringtoArry[10],StringtoArry[11],StringtoArry[12]);

            }

            }
        }
        return calllist;
    }
    public String returnTheNameOfTheSrvice(String theid)
    {
        String res="";
        try {
            URL url = new URL("http://ovedli.orgfree.com/api.php?action=servtxtbyid&servid="+theid);
            BufferedReader reader = new BufferedReader(new InputStreamReader(url.openStream()));
            String line;
            while ((line = reader.readLine()) != null) {
                res += line;
            }
            reader.close();
        } catch (Exception e) {

        }


        return res;
    }

    public void finsihthejob(String theid)
    {
        String res="";
        try {
            URL url = new URL("http://ovedli.orgfree.com/api.php?action=callfinish&callid="+theid);
            BufferedReader reader = new BufferedReader(new InputStreamReader(url.openStream()));
            String line;
            while ((line = reader.readLine()) != null) {
                res += line;
            }
            reader.close();
        } catch (Exception e) {

        }


    }
}
