var sampleTimeSec = 0.1;    
var sampleTimeMsec = sampleTimeSec*1000 
var maxSamplesNumber = 100;    


var ip2;
var sample;
var maxsample;
var timer2;
var file4 = "/ProjektAiR/get_chart_mes.php";
//var file = "/ProjektAiR/get_chart_mes.php";
var url4 = http+ip2+file4;

var testjson;
var counter = 0;

function startTimerList(){
  timer2 = setInterval(ajaxJSON3, sampleTimeMsec);
}
function stopTimerList(){
  clearInterval(timer2);
}

function ajaxJSON3() {
  $.ajax(url4, {
    type: 'GET', dataType: 'json',
    success: function(responseJSON, status, xhr) {
		testjson = responseJSON;
		createitem();
		counter = 1;			
    }
  });
}
function createitem(){
	if (counter == 0) {
		addItem()
	}
	else {
		removeItem()
		addItem()
	}
};


function addItem(){
	//var ul = document.getElementById("thelist");
	//var li = document.createElement("li");
	for (let i = 0; i < testjson.length; i++){
		var ul = document.getElementById("thelist");
		var li = document.createElement("li");
		li.setAttribute('id',i);
		li.appendChild(document.createTextNode("Pomiar:   " + testjson[i].name + "   =   " + testjson[i].value + " " + testjson[i].unit));
		ul.appendChild(li);
	}
}
function removeItem(){
	for (let i = 0; i < testjson.length; i++){
		var ul = document.getElementById("thelist");
		var item = document.getElementById(i);
		ul.removeChild(item);
	}


}

function setsettings(){
	sample = document.getElementById('sample').value;
	maxsample = document.getElementById('maxsample').value;
	ip2 = document.getElementById('ipadres').value;
	api = document.getElementById('apiver').value;
	
	document.getElementById('sampletimeT').innerHTML = sample;
	document.getElementById('samplenumberT').innerHTML = maxsample;
};
function setprop(){
	url4 = http+ip2+file4;
};
function updatedata2(){
	stopTimerList();
	setsettings();
	
	sampleTimeMsec = sample;
	sampleTimeSec = sample/1000;
	maxSamplesNumber = maxsample;
	
	setprop();


};
$(function(){
	updatedata2();
	$.ajaxSetup({ cache: false });
	$("#startlist").click(startTimerList);
	$("#stoplist").click(stopTimerList);
	$("#apply").click(updatedata2);
	$("#sampletime").text(sampleTimeMsec.toString());
	$("#samplenumber").text(maxSamplesNumber.toString());
});
