function Call() {
	var xmlHttp = CreateHttpRequest();
	if (xmlHttp == null) {
		alert("此实例只能在支持Ajax的浏览器中运行");
	}

	// Create result handler
	xmlHttp.onreadystatechange = function() {
		if (xmlHttp.readyState == 4) {
			if (xmlHttp.status == 200) {
				var result = eval("(" + xmlHttp.responseText + " )");
				var htmlStr = "<table align=center width=100%>";
				for (var i = 0; i < result.length; i++) {
					var type = "";
					if (result[i].Type == 0) {
						type = "上班";
					} else if (result[i].Type == 1) {
						type = "下班";
					}
					htmlStr += "<tr>" + "<td><ui><li class='twoLi'><span style='float:left;'>" + formatTime(result[i].Time) + "</span><span style='float:right;margin-right:5%;'>" + type + "</span></li></td></tr>";
					htmlStr += "<tr class='listSpan'><td></td></tr>"
				}
				var div = document.getElementById("content");
				div.innerHTML = htmlStr + "</table>";
			} else {
				alert("获取签到数据失败!\n" + xmlHttp.status);
			}
		}
	}
	var requstHeader = localStorage.requstHeader;
	var url = getServiceAddress() + "MobileAttendence?" + requstHeader;
	//console.log("getAttendenceList url: " + url);

	xmlHttp.open("GET", url, true);
	xmlHttp.send();
}

//创建HttpRequest对象
function CreateHttpRequest() {
	var httpRequest;
	try {
		httpRequest = new XMLHttpRequest();
	} catch (e) {
		try {
			httpRequest = new ActiveXObject("Msxml2.XMLHTTP");
		} catch (e) {
			try {
				httpRequest = new ActiveXObject("Microsoft.XMLHTTP");
			} catch (e) {
				return null;
			}
		}
	}

	return httpRequest;
}

function renderTime(data) {
	var da = eval('new ' + data.replace('/', '', 'g').replace('/', '', 'g'));
	var hour = da.getHours();
	if (hour < 10) {
		hour = "0" + hour;
	}
	var minute = da.getMinutes();
	if (minute < 10) {
		minute = "0" + minute;
	}
	var second = da.getSeconds();
	if (second < 10) {
		second = "0" + second;
	}

	return da.getFullYear() + "/" + (da.getMonth() + 1) + "/" + da.getDate() + " " + hour + ":" + minute + ":" + second;
}

function formatTime(data) {
	//console.log(data);
	var timestamp = data.replace('T', ' ').replace(/.\d*$/, "");
	return timestamp;
}

function Back() {
	window.location.href = 'AttendManager.htm';
}

// 签到
var position = '';
var attendType = 0;

function Attend(type) {
	localStorage.attendType = type;
	if (confirm("是否拍摄照片？")) {
		window.location.href = 'Capture.htm';
	} else {
		AddAttend();
	}
}

// function GetLocation() {
// 	var cb = function(data) {
// 		localStorage.Latitude = data.testData1;
// 		localStorage.Longitude = data.testData2;
// 		console.log(localStorage.Longitude + ", " + localStorage.Latitude);
// 	};
// 	window.plugins.GetLocationPlugin.test("", "", cb);
// }