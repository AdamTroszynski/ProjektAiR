<?php
$data = $_POST['jsonString'];
//set mode of file to writable.
$file = fopen('save.json', 'a');
fwrite($file, $data);
fclose($file);
?>
