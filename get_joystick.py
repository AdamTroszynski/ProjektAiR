import json
from sense_hat import SenseHat 

class Data:
	def __init__(self):
		self.mClicks = 0
		self.xPos = 0
		self.yPos = 0

d = Data()

sense = SenseHat()
#f = open("joystick.dat", "w")



while 1:
    for event in sense.stick.get_events():
        if event.action == 'pressed':
            d.mClicks += 1
        if event.action == 'pressed' and event.direction == 'up':
            d.yPos += 1
        if event.action == 'pressed' and event.direction == 'down':
            d.yPos -= 1
        if event.action == 'pressed' and event.direction == 'right':
            d.xPos += 1
        if event.action == 'pressed' and event.direction == 'left':
            d.xPos -= 1         
        f = open("joystick.dat",mode = "w")
        f.write(json.dumps(d.__dict__))
        f.close()

