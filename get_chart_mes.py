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
   
    e1 = {"orientX":round(o['roll'],3),"unit":"deg"}
    e2 = {"orientY":round(o['pitch'],3),"unit":"deg"}
    e3 = {"orientZ":round(o['yaw'],3),"unit":"deg"}
    e4 ={"gyroR":round(g['roll'],3),"unit":"deg"}
    e5 = {"gyroP":round(g['pitch'],3),"unit":"deg"}
    e6 = {"gyroY":round(g['yaw'],3),"unit":"deg"}
    e7 = {"accelX":round(a['x']*10.0,2),"unit":"m/s^2"}
    e8 = {"accelY":round(a['y']*10.0,2),"unit":"m/s^2"}
    e9 = {"accelZ":round(a['z']*10.0,2),"unit":"m/s^2"}
    e10 = {"temp1":round(t1,1),"unit":"C"}
    e11 = {"temp2":round(t2,1),"unit":"C"}
    e12 = {"hum":round(h,1),"unit":"%"}
    e13 = {"press":round(p,1),"unit":"hPa"}    
    z = [e1,e2,e3,e4,e5,e6,e7,e8,e9,e10,e11,e12,e13]

    #print(json.dumps(z))

    f = open("chart_data.txt",mode = "w")
    f.write(json.dumps(z))
    f.close()
    #sleep(2.1)

