package avishayandaviran.ovedli;

import android.Manifest;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.pm.PackageManager;
import android.location.Location;
import android.location.LocationManager;
import android.os.Bundle;
import android.support.v4.app.ActivityCompat;
import android.support.v7.app.AppCompatActivity;
import android.text.TextUtils;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

public class MainActivity extends AppCompatActivity {

    SharedPreferences pref;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        setTitle("עובד לי - ניהול עובדים");
        ActivityCompat.requestPermissions(this, new String[]{android.Manifest.permission.ACCESS_FINE_LOCATION}, 1);
        function f=new function();
         pref = getApplicationContext().getSharedPreferences("MyPref", MODE_PRIVATE);
        SharedPreferences.Editor editor = pref.edit();
        String restoredText = pref.getString("empid", null);
        if (restoredText != null) {
            Intent ii=new Intent(MainActivity.this, CallListActivity.class);
            startActivity(ii);

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
            if (ActivityCompat.checkSelfPermission(MainActivity.this, Manifest.permission.ACCESS_FINE_LOCATION)
                    != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission
                    (MainActivity.this, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
                ActivityCompat.requestPermissions(MainActivity.this, new String[]{Manifest.permission.ACCESS_FINE_LOCATION}, 1);

            }
            Location location=manager.getLastKnownLocation(LocationManager.NETWORK_PROVIDER);
            String loca=location.getLatitude()+","+location.getLongitude()+"";
            return loca;
        }


    public void login(View view) {
        function f=new function();
        EditText theid=(EditText)findViewById(R.id.idofuser);
        EditText thepass=(EditText)findViewById(R.id.passforuser);
        if(TextUtils.isEmpty(theid.getText().toString())|| TextUtils.isEmpty(thepass.getText().toString()))
        {
            Toast.makeText(getApplicationContext(),"השדות לא יכולים להיות ריקים",Toast.LENGTH_LONG).show();
        }
        else{
            if(f.isNetworkAvailable())
            {
                if(ifGPsIsEnable()==true)
                {
                    String loca=getGPSLocation()+"";
                    String the_id=theid.getText().toString();
                    String the_pass=thepass.getText().toString();
                    the_pass=f.encriptString(the_pass);
                    //the part of thec chek
                    if(f.Check_If_User_and_Pass_are_correct(the_id,the_pass)!=0)
                    {
                        f.updategps(f.Check_If_User_and_Pass_are_correct(the_id,the_pass),loca);
                        SharedPreferences.Editor editor = pref.edit();
                        editor.putString("empid", f.Check_If_User_and_Pass_are_correct(the_id,the_pass)+"");
                        editor.commit();
                        Intent ii=new Intent(MainActivity.this, CallListActivity.class);
                        ii.putExtra("tommorow",0);
                        startActivity(ii);

                    }

                    else
                    {
                        Toast.makeText(getApplicationContext(),"פרטים שגויים",Toast.LENGTH_LONG).show();
                    }
                }else
                {
                    Toast.makeText(getApplicationContext(),"אנא הפעל קודם את שירותי המיקום",Toast.LENGTH_LONG).show();
                }
            }
            else
            {
                Toast.makeText(getApplicationContext(),"אין חיבור לרשת",Toast.LENGTH_LONG).show();
            }
            }

    }
}


