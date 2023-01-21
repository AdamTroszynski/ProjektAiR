<?php
$command = escapeshellcmd("python3 get_pixel.py");
$last_line = exec($command, $all_lines);
foreach($all_lines as $line){
	print($line);
}
