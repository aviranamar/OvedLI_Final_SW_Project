package avishayandaviran.ovedli;

import android.Manifest;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.pm.PackageManager;
import android.location.Location;
import android.location.LocationManager;
import android.os.Bundle;
import android.support.v4.app.ActivityCompat;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

public class CallListActivity extends AppCompatActivity {

    SharedPreferences pref;
    int tommorow;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.calllistactivity);
        setTitle("עובד לי - ניהול עובדים");
        ActivityCompat.requestPermissions(this, new String[]{android.Manifest.permission.ACCESS_FINE_LOCATION}, 1);
        function f=new function();
        pref = getApplicationContext().getSharedPreferences("MyPref", MODE_PRIVATE);
        SharedPreferences.Editor editor = pref.edit();
        String restoredText = pref.getString("empid", null);
        checkNetwork();
        CheckGps();
        String loca=getGPSLocation();
        f.updategps(Integer.parseInt(restoredText),loca);
        Bundle extras = getIntent().getExtras();
        if(extras!=null) {
           String tom=extras.getInt("tommorow")+"";
           tommorow=Integer.parseInt(tom);
        }
        else{
            tommorow=0;
        }

        if(tommorow==0)
        {
            Button nextbtn=(Button)findViewById(R.id.tomtombtn);
            nextbtn.setText("לוז למחר");
            nextbtn.setOnClickListener(new View.OnClickListener() {
                public void onClick(View view) {
                    Intent ii=new Intent(CallListActivity.this, CallListActivity.class);
                    ii.putExtra("tommorow",1);
                    startActivity(ii);
                }
            });
        }
        else {

            Button nextbtn=(Button)findViewById(R.id.tomtombtn);
            nextbtn.setText("אתמול");
            nextbtn.setOnClickListener(new View.OnClickListener() {
                public void onClick(View view) {
                    Button backbutton=findViewById(R.id.back);
                    backbutton.setVisibility(View.GONE);
                    Intent ii=new Intent(CallListActivity.this, CallListActivity.class);
                    ii.putExtra("tommorow",0);
                    startActivity(ii);
                }
            });
        }
        final Call callsarray[]=f.returnAllTheCalls(restoredText,tommorow);
        if(callsarray!=null){
        for(int i=0;i<callsarray.length;i++) {
            LinearLayout ll = (LinearLayout)findViewById(R.id.locall);

            Button btn = new Button(this);
            btn.setId(callsarray[i].id_call);
            final int id_ = btn.getId();
            final int myi = i;
            btn.setText(callsarray[i].arrivedate+" \r"+callsarray[i].adress+"\r");

            ll.addView(btn);
            Button btn1 = ((Button) findViewById(callsarray[i].id_call));
            btn1.setOnClickListener(new View.OnClickListener() {
                public void onClick(View view) {
                    /*
                    Toast.makeText(view.getContext(),
                            "Button clicked index = " + id_, Toast.LENGTH_SHORT)
                            .show();
                            */
                  Intent ig=new Intent(CallListActivity.this,callActivity.class);
                    pref = getApplicationContext().getSharedPreferences("MyPref", MODE_PRIVATE);
                    SharedPreferences.Editor editor = pref.edit();
                    String restoredText = pref.getString("empid", null);
                    ig.putExtra("id",id_);
                    ig.putExtra("arrivedate",callsarray[myi].arrivedate.toString());
                    ig.putExtra("dateofopen",callsarray[myi].date_of_open);
                    ig.putExtra("description",callsarray[myi].call_description);
                    ig.putExtra("emailforuser",callsarray[myi].emailofuser);
                    ig.putExtra("fullname",callsarray[myi].fullname);
                    ig.putExtra("geolocation",callsarray[myi].adress);
                    ig.putExtra("cosid",callsarray[myi].costtemer_id);
                    ig.putExtra("iss",callsarray[myi].issolved);
                    ig.putExtra("cphone",callsarray[myi].call_phone);
                    ig.putExtra("ran",callsarray[myi].rank);
                    ig.putExtra("servid",callsarray[myi].serviceid);
                    ig.putExtra("tommorow",tommorow);
                    startActivity(ig);
                }
            });
        }
        }
        else
        {

            TextView spc=(TextView)findViewById(R.id.spacebar);
            spc.setText("אין קריאות להיום");

        }







    }
    public void checkNetwork()
    {
        function f=new function();
        if(!f.isNetworkAvailable()){
        AlertDialog alertDialog = new AlertDialog.Builder(this)
                //set icon
                .setIcon(android.R.drawable.ic_dialog_alert)
                //set title
                .setTitle("שגיאת רשת")
                //set message
                .setMessage("אינך מחובר לרשת")
                //set positive button
                .setPositiveButton("אישור", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialogInterface, int i) {
                        //set what would happen when positive button is clicked
                        finish();
                    }
                })

                .show();
        }
    }


    public boolean ifGPsIsEnable() {
        boolean res=true;
        final LocationManager manager;
        manager = (LocationManager) getSystemService(Context.LOCATION_SERVICE);

        if (!manager.isProviderEnabled(LocationManager.GPS_PROVIDER)) {
            res=false;
        }
        return res;
    }

    public  void CheckGps()
    {
        if(!ifGPsIsEnable())
        {
            /*
            AlertDialog alertDialog = new AlertDialog.Builder(this)
                    //set icon
                    .setIcon(android.R.drawable.ic_dialog_alert)
                    //set title
                    .setTitle("שגיאת מיקום")
                    //set message
                    .setMessage("שירותי מיקום אינם מופעלים")
                    //set positive button
                    .setPositiveButton("אישור", new DialogInterface.OnClickListener() {
                        @Override
                        public void onClick(DialogInterface dialogInterface, int i) {
                            //set what would happen when positive button is clicked
                            finish();
                        }
                    })

                    .show();
                    */
            Toast.makeText(getApplicationContext(),"שירותי מיקום כבויים",Toast.LENGTH_LONG).show();
            Intent ii=new Intent( CallListActivity.this,MainActivity.class);
            startActivity(ii);

        }
    }

    public String getGPSLocation()
    {
        final LocationManager manager;
        manager = (LocationManager) getSystemService(Context.LOCATION_SERVICE);
        if (ActivityCompat.checkSelfPermission(CallListActivity.this, Manifest.permission.ACCESS_FINE_LOCATION)
                != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission
                (CallListActivity.this, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
            ActivityCompat.requestPermissions(CallListActivity.this, new String[]{Manifest.permission.ACCESS_FINE_LOCATION}, 1);

        }
        Location location=manager.getLastKnownLocation(LocationManager.NETWORK_PROVIDER);
        String loca=location.getLatitude()+","+location.getLongitude()+"";
        return loca;
    }

    public void logout(View view) {
        pref.edit().remove("empid").commit();
        Intent ii=new Intent(CallListActivity.this,MainActivity.class);
        startActivity(ii);

    }
}
