<?php
$x=0; $y=0; $r=0; $g=0; $b=0; 

if(isset($_GET['x'])) $x=(int)$_GET['x'];
if(isset($_GET['y'])) $y=(int)$_GET['y'];
if(isset($_GET['r'])) $r=(int)$_GET['r'];
if(isset($_GET['g'])) $g=(int)$_GET['g'];
if(isset($_GET['b'])) $b=(int)$_GET['b'];

$arguments = " $x $y $r $g $b";
$command = escapeshellcmd("python3 set_pixel_cli.py");
$last_line = exec($command.$arguments, $all_lines);
foreach($all_lines as $line){
	print($line);
}
