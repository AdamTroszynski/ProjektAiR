var sampleTimeSec = 0.1;                  ///< sample time in sec
var sampleTimeMsec = sampleTimeSec*1000 ///< sample time in msec
var maxSamplesNumber = 100;               ///< maximum number of samples

var sample;
var maxsample;
var api = 1;

var xdata; ///< x-axis labels array: time stamps
var ydata; ///< y-axis data array: random value
var roll;
var pitch;
var yaw;
var lastTimeStamp; ///< most recent time stamp 

var chartContext;  ///< chart context i.e. object that "owns" chart
var chart;         ///< Chart.js object

var timer; ///< request timer
var http = "http://";
var ip1;
var file = "/ProjektAiR/get_chart_mes.php";
var file2 = "/ProjektAiR/skrypt/save.txt";
var file3 = "/ProjektAiR/skrypt/save.php";
var url
var url2;
var url3;
//const url = 'http://192.168.0.13:800/measurements.php'; ///< server app with JSON API
//const url = 'http://' + window.location.hostname + '/nocache/chartdata.json'

/**
* @brief Add new value to next data point.
* @param y New y-axis value 
*/
function addData(rol,pit,yaws){
  if(roll.length > maxSamplesNumber)
  {
    removeOldData();
    lastTimeStamp += sampleTimeSec;
    xdata.push(lastTimeStamp.toFixed(4));
  }
  roll.push(rol);
  pitch.push(pit);
  yaw.push(yaws);
  chart.update();
}

/**
* @brief Remove oldest data point.
*/
function removeOldData(){
  roll.splice(0,1);
  pitch.splice(0,1);
  yaw.splice(0,1);
  xdata.splice(0,1);
}

/**
* @brief Start request timer
*/
function startTimer(){
  timer = setInterval(ajaxJSON, sampleTimeMsec);
}
/**
* @brief Stop request timer
*/
function stopTimer(){
  clearInterval(timer);
}

/**
* @brief Send HTTP GET request to IoT server
*/

function setsettings(){
	
	sample = document.getElementById('sample').value;
	maxsample = document.getElementById('maxsample').value;
	ip1 = document.getElementById('ipadres').value;
	api = document.getElementById('apiver').value;
	
	document.getElementById('sampletime').innerHTML = sample;
	document.getElementById('samplenumber').innerHTML = maxsample;
};

function popset(){
  	url = http+ip1+file;
	url2 = http+ip1+file2;
	url3 = http+ip1+file3;
  };

function updatedata(){
	stopTimer();
	setsettings();
	
	ip1 = document.getElementById('ipadres').value;
	document.getElementById('sampletime').innerHTML = sample;
	document.getElementById('samplenumber').innerHTML = maxsample;

	sampleTimeMsec = sample;
	sampleTimeSec = sample/1000;
	maxSamplesNumber = maxsample;
	
	popset();
};

function saveToFile(setings){
  jsonString = JSON.stringify(setings);
  $.ajax({url3,
    data : {'jsonString':jsonString},
    type: 'POST'
  });
}

function ajaxJSON() {
  $.ajax(url, {
    type: 'GET', dataType: 'json',
    success: function(responseJSON, status, xhr) {
      addData(+responseJSON[0].value,+responseJSON[1].value,+responseJSON[2].value);
    }
  });
}

function ajaxJSON2() {
  $.ajax(url2, {
    type: 'GET', dataType: 'json',
    success: function(responseJSON, status, xhr) {
      addDatasave(+responseJSON.ip,+responseJSON.sample,+responseJSON.maxsample,+responseJSON.api);
    }
  });
}

function addDatasave(ipsave,samplesave,maxsamplesave,apisave){
	ip = ipsave;
	sample = samplesave;
	maxsample = maxsamplesave;
	api = apisave;
}

/**
* @brief Chart initialization
*/
function chartInit()
{
  xdata = [...Array(maxSamplesNumber).keys()]; 
  xdata.forEach(function(p, i) {this[i] = (this[i]*sampleTimeSec).toFixed(4);}, xdata);
  lastTimeStamp = +xdata[xdata.length-1]; 
	roll = [];
	pitch = [];
	yaw = [];
  chartContext = $("#chart")[0].getContext('2d');

  chart = new Chart(chartContext, {
    type: 'line',

    data: {
      labels: xdata,
      datasets: [{
        fill: false,
        label: 'roll',
        backgroundColor: 'rgb(255, 0, 0)',
        borderColor: 'rgb(255, 0, 0)',
        data: roll,
        lineTension: 0
      },
	  {
        fill: false,
        label: 'pitch',
        backgroundColor: 'rgb(0, 255, 0)',
        borderColor: 'rgb(0, 255, 0)',
        data: pitch,
        lineTension: 0
      },
	  {
        fill: false,
        label: 'yaw',
        backgroundColor: 'rgb(0, 0, 255)',
        borderColor: 'rgb(0, 0, 255)',
        data: yaw,
        lineTension: 0
      },
	  ]
    },

    options: {
      responsive: true,
      maintainAspectRatio: false,
      animation: false,
      scales: {
        yAxes: [{
          scaleLabel: {
            display: true,
            labelString: 'Random value'
          }
        }],
        xAxes: [{
          scaleLabel: {
            display: true,
            labelString: 'Time [s]'
          }
        }]
      }
    }
  });
  
  roll = chart.data.datasets[0].data;
  pitch = chart.data.datasets[1].data;
  yaw = chart.data.datasets[2].data;
  xdata = chart.data.labels;
}

$(document).ready(() => { 
  chartInit();
  updatedata();
  //ajaxJSON2();
  $.ajaxSetup({ cache: false }); // Web browser cache control
  $("#start").click(startTimer);
  $("#stop").click(stopTimer);
  $("#apply").click(updatedata);
  $("#sampletime").text(sampleTimeMsec.toString());
  $("#samplenumber").text(maxSamplesNumber.toString());
});
