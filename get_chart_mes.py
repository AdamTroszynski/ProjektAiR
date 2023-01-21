import getopt
import sys
import json
from sense_hat import SenseHat
from time import sleep

sense = SenseHat()

while True:
    o = sense.get_orientation_degrees()
    t1 = sense.get_temperature_from_pressure()
    t2 = sense.get_temperature_from_humidity()
    h = sense.humidity
    p = sense.pressure
    g = sense.gyroscope
    a = sense.accelerometer_raw
   
    e1 = {"name":"orientX", "value":round(o['roll'],3),  "unit":"deg"}
    e2 = {"name":"orientY", "value":round(o['pitch'],3), "unit":"deg"}
    e3 = {"name":"orientZ", "value":round(o['yaw'],3),   "unit":"deg"}
    e4 = {"name":"gyroR",   "value":round(g['roll'],3),  "unit":"deg"}
    e5 = {"name":"gyroP",   "value":round(g['pitch'],3), "unit":"deg"}
    e6 = {"name":"gyroY",   "value":round(g['yaw'],3),   "unit":"deg"}
    e7 = {"name":"accelX",  "value":round(a['x']*10.0,2),"unit":"m/s^2"}
    e8 = {"name":"accelY",  "value":round(a['y']*10.0,2),"unit":"m/s^2"}
    e9 = {"name":"accelZ",  "value":round(a['z']*10.0,2),"unit":"m/s^2"}
    e10 = {"name":"temp1",  "value":round(t1,1),         "unit":"C"}
    e11 = {"name":"temp2",  "value":round(t2,1),         "unit":"C"}
    e12 = {"name":"hum",    "value":round(h,1),          "unit":"%"}
    e13 = {"name":"press",  "value":round(p,1),          "unit":"hPa"}    
    z = [e1,e2,e3,e4,e5,e6,e7,e8,e9,e10,e11,e12,e13]

    #print(json.dumps(z))

    f = open("chart_data.txt",mode = "w")
    f.write(json.dumps(z))
    f.close()
    #sleep(2.1)

