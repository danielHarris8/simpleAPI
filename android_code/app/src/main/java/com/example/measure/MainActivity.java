package com.example.measure;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.widget.Toast;


import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.shinelw.library.ColorArcProgressBar;


import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;


import java.util.HashMap;
import java.util.Map;



public class MainActivity extends AppCompatActivity {

    private RequestQueue mQueue;

    private ColorArcProgressBar bar1;
    private ColorArcProgressBar bar2;
    String token_value;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        mQueue = Volley.newRequestQueue(this);
        bar1 = findViewById(R.id.bar1);
        bar2 = findViewById(R.id.bar2);

    }

    @Override
    protected void onResume() {
        super.onResume();

        t.start();
    }

    @Override
    protected void onPause() {
        super.onPause();
        t.interrupt();
    }

    // first, request token
    public  void getToken(){
        String url = "https://yourdomain/api/token?type=android_user&name=test";

        StringRequest request = new StringRequest(Request.Method.POST, url,  new Response.Listener<String>() {
            @Override
            public void onResponse(String response) {
                try {
                    System.out.println(response);
                    token_value=response;
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        },new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                error.printStackTrace();
            }
        });

        mQueue.add(request);
    }

    // and then, get date
    public void getData(final String token){
        String url = "https://yourdomain/api/data";
        JsonObjectRequest request = new JsonObjectRequest(Request.Method.GET, url, null,
                new Response.Listener<JSONObject>() {
                    @Override
                    public void onResponse(JSONObject response) {
                        try {

                            float temperature = Float.parseFloat(response.getString("temperature"));
                            System.out.println(temperature);
                            float humidity =Float.parseFloat(response.getString("humidity"));
                            System.out.println(humidity);
                            bar1.setCurrentValues(temperature);
                            bar2.setCurrentValues(humidity);

                        } catch (JSONException e) {
                            e.printStackTrace();
                        }
                    }
                }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                error.printStackTrace();
            }
        }){
            @Override
            public Map<String, String> getHeaders() throws AuthFailureError {
                Map<String, String>  params = new HashMap<String, String>();
                params.put("Authorization","Bearer "+token);


                return params;
            }
        };
        mQueue.add(request);
    }

    // Keep doing
    Thread t =  new Thread(new Runnable() {
        @Override
        public void run() {
            try {
                while (true) {
                    getToken();
                    getData(token_value);
                    Thread.sleep(5000);
                }
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
    });
}
