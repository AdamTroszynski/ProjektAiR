package com.example.myapplication;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.os.Bundle;
import android.os.Handler;
import android.util.Log;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.TextView;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.Iterator;



public class ListActivity extends AppCompatActivity {
    LinearLayout linearLayout;
    ArrayList<String> arrNames = new ArrayList<>();
private Handler handler = new Handler();
Runnable r = new Runnable() {
        public void run() {
            sendRequest();
            handler.postDelayed(this, Long.parseLong(sample));
        }
    };
String sample = GlobalVar.CONFIG_SAMPLE_TIME;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_list);
        handler.postDelayed(r, Long.parseLong(sample));

    }
    @Override
    public void onPause(){
        super.onPause();
        handler.removeCallbacks(r);
    };

    public void click_get(View view) {
        sendRequest();
    }
    private void sendRequest()
    {
        //final TextView text_ret = (TextView) findViewById(R.id.text_return);

        RequestQueue queue = Volley.newRequestQueue(this);
        String url = "http://"+ GlobalVar.CONFIG_IP_ADDRESS +"/ProjektAiR/get_chart_mes.php";;

        JsonArrayRequest jsonArrayRequest = new JsonArrayRequest(Request.Method.GET, url,null,
                new Response.Listener<JSONArray>() {
                    @Override
                    public void onResponse(JSONArray response) {

                        LinearLayout linearLayout = (LinearLayout) findViewById(R.id.vertical_data1);
                        LinearLayout linearLayout2 = (LinearLayout) findViewById(R.id.vertical_data2);
                        LinearLayout linearLayout3 = (LinearLayout) findViewById(R.id.vertical_data3);
                        linearLayout.removeAllViews();
                        linearLayout2.removeAllViews();
                        linearLayout3.removeAllViews();

                        Log.d("RES", response.toString());
                        try {
                            for (int i=0; i<response.length(); i++){
                                JSONObject objRes = response.getJSONObject(i);
                                String name = objRes.getString("name");
                                String value = objRes.getString("value");
                                String unit = objRes.getString("unit");

                                Context context = linearLayout.getContext();
                                TextView textView = new TextView(context);
                                textView.setText(name);
                                linearLayout.addView(textView);

                                TextView textView2 = new TextView(context);
                                textView2.setText(value);
                                linearLayout2.addView(textView2);

                                TextView textView3 = new TextView(context);
                                textView3.setText(unit);
                                linearLayout3.addView(textView3);

                                arrNames.add(name);

                            }
                        } catch (JSONException e) {
                            e.printStackTrace();
                        }





                        /*
                        for (Iterator<String> it = obj.keys(); it.hasNext(); ) {
                            String name = it.next();

                            String dataStr = null;
                            try {
                                dataStr = obj.getString(name);
                            } catch (JSONException e) {
                                e.printStackTrace();
                            }

                            Context context = linearLayout.getContext();
                            TextView textView = new TextView(context);
                            textView.setText(name + ":            " + dataStr);
                            linearLayout.addView(textView);

                        }

                         */
                    }
                },
                new Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError error) {

                    }
                });
        queue.add(jsonArrayRequest);
    }
}