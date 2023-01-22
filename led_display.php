<?php
//include 'led_display_model.php';
$dispControlFile = 'led_display.json';

class LedDisplay {
    const SIZE_X = 8;
    const SIZE_Y = 8;

    function indexToId($x, $y) {
      // TODO: add validation
      return "LED" .$x .$y;
    }
    
    function postDataToJsonArray($postData){
      $jsonArry = array();
      $n = 0;
      for ($i = 0; $i < self::SIZE_X; $i++) {
        for ($j = 0; $j < self::SIZE_Y; $j++) {
          $ledId = $this->indexToId($i, $j);
          if(isset($postData[$ledId])){
            $jsonArry[$n] = json_decode($postData[$ledId]);
            $n=$n+1;
          }
        }
      }
      return json_encode($jsonArry);
    }
} 

switch($_SERVER['REQUEST_METHOD']) {
  
  case "GET": 
  // Parse input data and save as JSON array to text file
    $disp = new LedDisplay();
    file_put_contents($dispControlFile, $disp->postDataToJsonArray($_GET));
    echo file_get_contents($dispControlFile);
    break;
  
  case "POST": 
  // Parse input data and save as JSON array to text file
    $disp = new LedDisplay();
    file_put_contents($dispControlFile, $disp->postDataToJsonArray($_POST));
	echo "ACK";
    break;

  case "PUT": 
  // Save input JSON array directly to text file
    file_put_contents($dispControlFile, file_get_contents('php://input'));
	echo '["ACK"]';
    break;
}
$command = escapeshellcmd("python3 set_pixel_merged.py");
$last_line = exec($command, $all_lines);
	//foreach($all_lines as $line){
	//print($line);
	//}

?>
