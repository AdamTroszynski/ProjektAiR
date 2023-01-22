from sense_hat import SenseHat
import json

sense = SenseHat()

input_file = open('led_display.json')
jsonled = json.load(input_file)

#print(jsonled[0][0],",",jsonled[0][1],",",jsonled[0][2],",",jsonled[0][3],",",jsonled[0][4])
for i in range(len(jsonled)):
    x = jsonled[i][0]
    y = jsonled[i][1]
    r = jsonled[i][2]
    g = jsonled[i][3]
    b = jsonled[i][4]
    sense.set_pixel(x,y,r,g,b)
