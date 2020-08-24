package avishayandaviran.ovedli;

public class Employee {
    public int id_employee;
    public String adress;
    public int compnay_id;
    public String dateofjoin;
    public String fname;
    public String geolocation;
    public String id_num;
    public int isadmin;
    public int istech;
    public String lname;
    public String password;
    public String phone_num;

    public Employee()
    {
       this.id_employee=0;
       this.adress="";
       this.compnay_id=0;
       this.dateofjoin="";
       this.fname="";
       this.geolocation="";
       this.id_num="";
       this.isadmin=0;
       this.istech=0;
       this.lname="";
       this.password="";
       this.phone_num="";
    }

    public Employee(int id,String adrr,int compid,String doj,String firstname,String Geo,String idnumber,int admin,int tech ,String lastname, String pass, String numphone)
    {
        this.id_employee=id;
        this.adress=adrr;
        this.compnay_id=compid;
        this.dateofjoin=doj;
        this.fname=firstname;
        this.geolocation=Geo;
        this.id_num=idnumber;
        this.isadmin=admin;
        this.istech=tech;
        this.lname=lastname;
        this.password=pass;
        this.phone_num=numphone;
    }


}
