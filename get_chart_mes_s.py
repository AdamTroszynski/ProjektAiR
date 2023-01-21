## @author  Dominik Luczak
## @date    2021-02-05
# @brief Stream by UDP accelerometer raw data in infinite loop.
# Run script using: python udp_stream_accelerometer_byte_sensehat.py
# Run command as default user (pi) or other user. If other user than pi is used add the user to groups used by peripherals.
from sense_hat import SenseHat
from time import sleep
from time import time
import socket
import struct

 
sense = SenseHat()

t = time()#time
t_previous = t

IPv4_destination_address = "192.168.0.21"
UDP_destination_port = 5000
udp_socket_handler = socket.socket(socket.AF_INET, socket.SOCK_DGRAM) # UDP

while 1:
	t = time()
	raw = sense.get_accelerometer_raw() #read data
	#conver data to byte array and send it by UDP
	# raw = {'y': 0.06288327276706696, 'x': -0.005430384073406458, 'z': 0.968207597732544}
	# convert float to bytes
	# https://docs.python.org/3/library/struct.html
	x_byte = struct.pack(">f", raw['x']) #>f float big-endian
	y_byte = struct.pack(">f", raw['y'])
	z_byte = struct.pack(">f", raw['z'])
	dt_measured = struct.pack(">f", t-t_previous) #>f float big-endian
	data_byte = x_byte + y_byte + z_byte + dt_measured
	udp_socket_handler.sendto(data_byte, (IPv4_destination_address, UDP_destination_port))
	#delay loop with selected tp
	t2 = time()
	delay = 0.05-(t2-t) #correct delay time
	if delay>0:
		sleep(delay)
	#remember previous time
	t_previous = t
