var attendType = '';

function AddAttend() {
	var xmlHttp = CreateHttpRequest();
	if (xmlHttp == null) {
		alert("此实例只能在支持Ajax的浏览器中运行");
	}

	// Create result handler
	xmlHttp.onreadystatechange = function() {
		if (xmlHttp.readyState == 4) {
			if (xmlHttp.status == 200) {
				console.log("xmlHttp.responseText: " + xmlHttp.responseText);
				var result = eval("(" + xmlHttp.responseText + " )").Name;
				if (result == localStorage.userName) {
					window.location.href = 'AttendenceList.htm';
					alert("签到成功！");
				} else {
					alert("签到失败！");
				}
			} else {
				alert("签到失败！\n" + xmlHttp.status);
				console.log("xmlHttp.status: " + xmlHttp.status);
			}
		}
	}

	//初始化Json消息
	attendType = localStorage.attendType;
	position = localStorage.lat + "," + localStorage.lon
	var requstHeader = localStorage.requstHeader;
	var fileid = 0;
	var body = '{"position":"' + position + '","type":"' + attendType + '","fileId":"' + fileid + '"}';
	var url = getServiceAddress() + "MobileAttendence?" + requstHeader;

	console.log("geolocation request body: " + body);
	
	//发送Http请求
	xmlHttp.open("Post", url, true);
	xmlHttp.setRequestHeader("Content-type", "text/json;charset=utf-8");
	xmlHttp.send(body);
}

// 定位
function SetPosition(position) {

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