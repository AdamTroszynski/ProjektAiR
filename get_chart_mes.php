<?php
//$command = escapeshellcmd("python get_chart_mes.py");
//$last_line = exec($command, $all_lines);
$fp = implode('',file("chart_data.txt"));
print($fp);
//print(last_line);
//foreach($all_lines as $line){
//	print($line);
//}
