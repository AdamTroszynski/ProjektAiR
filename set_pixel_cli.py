import sys
from sense_hat import SenseHat

argv = sys.argv[1:]

sense = SenseHat()
x = int(argv[0])
y = int(argv[1])
r = int(argv[2])
g = int(argv[3])
b = int(argv[4])
sense.set_pixel(x,y,r,g,b)
