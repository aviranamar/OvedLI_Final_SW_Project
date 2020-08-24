package avishayandaviran.ovedli;

import android.Manifest;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.pm.PackageManager;
import android.location.Location;
import android.location.LocationManager;
import android.net.Uri;
import android.os.Bundle;
import android.support.v4.app.ActivityCompat;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.TextView;
import android.widget.Toast;

public class callActivity extends AppCompatActivity {

    Call theCall;
    SharedPreferences pref;
    int tommorow;
    function f;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_call);
        setTitle("עובד לי - ניהול עובדים");
        ActivityCompat.requestPermissions(this, new String[]{android.Manifest.permission.ACCESS_FINE_LOCATION}, 1);
        f =new function();
        pref = getApplicationContext().getSharedPreferences("MyPref", MODE_PRIVATE);
        SharedPreferences.Editor editor = pref.edit();
        String restoredText = pref.getString("empid", null);
        if(f.isNetworkAvailable())
        {
            if(ifGPsIsEnable())
            {
                String loca=getGPSLocation()+"";
                f.updategps(Integer.parseInt(restoredText),loca);
                Bundle extras = getIntent().getExtras();
                int theid=extras.getInt("id");
                String arrivedate=extras.getString("arrivedate");
                String dateofopen=extras.getString("dateofopen");
                String description=extras.getString("description");
                String emailforuser=extras.getString("emailforuser");
                String fullname=extras.getString("fullname");
                String geolocation=extras.getString("geolocation");
                String cosid=extras.getString("cosid");
                String iss=extras.getString("iss");
                String cphone=extras.getString("cphone");
                String ran=extras.getString("ran");
                String servid=extras.getString("servid");
                tommorow=extras.getInt("tommorow");
                theCall=new Call(theid,arrivedate,dateofopen,description,emailforuser,fullname,geolocation,cosid,iss,cphone,ran,servid,restoredText);
                TextView temp ;
                temp=(TextView)findViewById(R.id.datearrive);
                temp.setText(temp.getText()+" "+arrivedate);
                temp=(TextView)findViewById(R.id.dateopning);
                temp.setText(temp.getText()+" "+dateofopen);
                temp=(TextView)findViewById(R.id.pirut);
                temp.setText(temp.getText()+" "+description);
                temp=(TextView)findViewById(R.id.emailshellakoch);
                temp.setText(temp.getText()+" "+emailforuser);
                temp=(TextView)findViewById(R.id.shemmale);
                temp.setText(temp.getText()+" "+fullname);
                temp=(TextView)findViewById(R.id.ktovet);
                temp.setText(temp.getText()+" "+geolocation);
                temp=(TextView)findViewById(R.id.teudatzeut);
                temp.setText(temp.getText()+" "+cosid);
                temp=(TextView)findViewById(R.id.hatelefon);
                temp.setText(temp.getText()+" "+cphone);
                temp=(TextView)findViewById(R.id.shiroot);
                temp.setText(temp.getText()+" "+f.returnTheNameOfTheSrvice(servid));

            }
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
    public String getGPSLocation()
    {
        final LocationManager manager;
        manager = (LocationManager) getSystemService(Context.LOCATION_SERVICE);
        if (ActivityCompat.checkSelfPermission(callActivity.this, Manifest.permission.ACCESS_FINE_LOCATION)
                != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission
                (callActivity.this, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
            ActivityCompat.requestPermissions(callActivity.this, new String[]{Manifest.permission.ACCESS_FINE_LOCATION}, 1);

        }
        Location location=manager.getLastKnownLocation(LocationManager.NETWORK_PROVIDER);
        String loca=location.getLatitude()+","+location.getLongitude()+"";
        return loca;
    }

    public void back(View view) {
        Intent in=new Intent(callActivity.this,CallListActivity.class);
        in.putExtra("tommorow",tommorow);
        startActivity(in);
    }

    public void finish(View view) {
        if(tommorow>0)
        {
            Toast.makeText(getApplicationContext(),"לא ניתן לסגור קריאה מוקדם מתאריך ההגעה",Toast.LENGTH_SHORT).show();
        }
        else {
            f.finsihthejob(theCall.id_call + "");
            Intent in = new Intent(callActivity.this, CallListActivity.class);
            in.putExtra("tommorow", tommorow);
            startActivity(in);
        }

    }

    public void callme(View view) {
        Intent intent = new Intent(Intent.ACTION_DIAL);
        intent.setData(Uri.parse("tel:"+theCall.call_phone));
        startActivity(intent);
    }

    public void wazeme(View view) {
        try
        {
            // Launch Waze to look for Hawaii:
            String url = "https://waze.com/ul?q="+theCall.adress+"&navigate=yes&zoom=17";
            Intent intent = new Intent( Intent.ACTION_VIEW, Uri.parse( url ) );
            startActivity( intent );
        }
        catch ( Exception ex  )
        {
            // If Waze is not installed, open it in Google Play:
            Intent intent = new Intent( Intent.ACTION_VIEW, Uri.parse( "market://details?id=com.waze" ) );
            startActivity(intent);
        }
    }
}
