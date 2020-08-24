package avishayandaviran.ovedli;

public class Call {
    public int id_call;
    public String arrivedate;
    public String date_of_open;
    public String call_description;
    public String emailofuser;
    public String fullname;
    public String adress;
    public String costtemer_id;
    public String issolved;
    public String call_phone;
    public String rank;
    public String serviceid;
    public String techid;

    public Call()
    {
        this.id_call=0;
        this.arrivedate="";
        this.date_of_open="";
        this.call_description="";
        this.emailofuser="";
        this.fullname="";
        this.adress="";
        this.costtemer_id="";
        this.issolved="";
        this.call_phone="";
        this.rank="";
        this.serviceid="";
        this.techid="";
    }
    public Call(int idc,String ad,String doo,String cd,String emailou,String thename, String adr,String cos_id,String iss,String cp,String ran,String servid,String TechID)
    {
        this.id_call=idc;
        this.arrivedate=ad;
        this.date_of_open=doo;
        this.call_description=cd;
        this.emailofuser=emailou;
        this.fullname=thename;
        this.adress=adr;
        this.costtemer_id=cos_id;
        this.issolved=iss;
        this.call_phone=cp;
        this.rank=ran;
        this.serviceid=servid;
        this.techid=TechID;
    }





}
