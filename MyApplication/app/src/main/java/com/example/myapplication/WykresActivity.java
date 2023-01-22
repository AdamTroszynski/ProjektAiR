package com.example.myapplication;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.graphics.Color;
import android.os.Bundle;
import android.os.Handler;
import android.view.View;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.android.volley.DefaultRetryPolicy;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;

import com.jjoe64.graphview.GraphView;
import com.jjoe64.graphview.LegendRenderer;
import com.jjoe64.graphview.series.DataPoint;
import com.jjoe64.graphview.series.LineGraphSeries;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.Iterator;



public class WykresActivity extends AppCompatActivity {
    private RequestQueue queue;
    private EditText textURL;
    private LineGraphSeries<DataPoint> series = new LineGraphSeries<DataPoint>();
    private LineGraphSeries<DataPoint> series2 = new LineGraphSeries<DataPoint>();
    private LineGraphSeries<DataPoint> series3 = new LineGraphSeries<DataPoint>();
    private int currentDataPoint = 0;

    Handler handler = new Handler();
    final Runnable r = new Runnable() {
        public void run() {
            sendRequest();
            handler.postDelayed(this, Integer.parseInt(GlobalVar.CONFIG_SAMPLE_TIME));
        }
    };
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_wykres);

        String url = "http://"+ GlobalVar.CONFIG_IP_ADDRESS +"/ProjektAiR/get_chart_mes.php";

        handler.postDelayed(r, Integer.parseInt(GlobalVar.CONFIG_SAMPLE_TIME));

        GraphView graph = (GraphView) findViewById(R.id.graph);

        graph.setTitle("Wykres orientacji urządzenia");
        series.setTitle("Roll");
        series2.setTitle("Pitch");
        series3.setTitle("Yaw");

        series.setColor(Color.RED);
        series2.setColor(Color.GREEN);
        series3.setColor(Color.BLUE);

        graph.getLegendRenderer().setVisible(true);
        graph.getLegendRenderer().setAlign(LegendRenderer.LegendAlign.TOP);
        graph.getLegendRenderer().setMargin(10);
        graph.getLegendRenderer().setFixedPosition(5, 5);
    }

    @Override
    public void onPause(){
        super.onPause();
        handler.removeCallbacks(r);
    };

    public void click_button_clear(View view) {
        final TextView text_ret = (TextView) findViewById(R.id.text_return);
        text_ret.setText("");

        GraphView graph = (GraphView) findViewById(R.id.graph);
        graph.removeAllSeries();
        series = new LineGraphSeries<DataPoint>();
        series2 = new LineGraphSeries<DataPoint>();
        series3 = new LineGraphSeries<DataPoint>();
        series.setColor(Color.RED);
        series2.setColor(Color.GREEN);
        series3.setColor(Color.BLUE);
        series.setTitle("Roll");
        series2.setTitle("Pitch");
        series3.setTitle("Yaw");
        currentDataPoint = 0;
    }

    public void click_button_get(View view) {
        sendRequest();
    }
    private void sendRequest(){
        final TextView text_ret = (TextView) findViewById(R.id.text_return);

        String url = "http://"+ GlobalVar.CONFIG_IP_ADDRESS +"/ProjektAiR/get_chart_mes.php";
        RequestQueue queue = Volley.newRequestQueue(this);

        JsonArrayRequest jsonArrayRequest = new JsonArrayRequest(Request.Method.GET, url,null,
                new Response.Listener<JSONArray>() {
                    @Override
                    public void onResponse(JSONArray response) {

                        String xValue = "0.0";
                        String yValue = "0.0";
                        String zValue = "0.0";
                        try {
                            JSONObject objResx = response.getJSONObject(0);
                            xValue = objResx.getString("value");
                            JSONObject objResy = response.getJSONObject(1);
                            yValue = objResy.getString("value");
                            JSONObject objResz = response.getJSONObject(2);
                            zValue = objResz.getString("value");
                            //text_ret.setText("Response is: " + xValue + " " + yValue + " " + zValue);

                        } catch (JSONException e) {
                            e.printStackTrace();
                        }
                        series.appendData(new DataPoint(currentDataPoint, Double.parseDouble(xValue)), true, currentDataPoint + 1);
                        series2.appendData(new DataPoint(currentDataPoint, Double.parseDouble(yValue)), true, currentDataPoint + 1);
                        series3.appendData(new DataPoint(currentDataPoint, Double.parseDouble(zValue)), true, currentDataPoint + 1);
                        if (currentDataPoint % 5 == 0)
                        {
                            GraphView graph = (GraphView) findViewById(R.id.graph);
                            graph.removeAllSeries();
                            graph.addSeries(series);
                            graph.addSeries(series2);
                            graph.addSeries(series3);
                        }
                        currentDataPoint += 1;

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
                            textView.setText(name + ": " + dataStr);
                            linearLayout.addView(textView);
                        }
                        */
                    }



                }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                text_ret.setText("That didn't work!");
            }
        });
        jsonArrayRequest.setRetryPolicy(new DefaultRetryPolicy(100, 1,
                DefaultRetryPolicy.DEFAULT_BACKOFF_MULT));
// Add the request to the RequestQueue.
        queue.add(jsonArrayRequest);
    }
}