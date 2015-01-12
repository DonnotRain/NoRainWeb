// 获取服务接口地址
function getServiceAddress() {
	var addr = "http://192.168.0.139:8012/api/";
	return addr;
}

//获取userName和corpCode
function getUserName() {
	var str = "userName=" + localStorage.userName + "&corpcode=" + localStorage.corpCode;
}

function formatTime(data) {
	//console.log(data);
	var timestamp = data.replace('T', ' ').replace(/.\W*$/, "");
	return timestamp;
}
﻿
// 获得coolie 的值
function cookie(name) {
	var cookieArray = document.cookie.split("; "); //得到分割的cookie名值对
	var cookie = new Object();
	for (var i = 0; i < cookieArray.length; i++) {
		var arr = cookieArray[i].split("=");       //将名和值分开
		if (arr[0] == name) return unescape(arr[1]); //如果是指定的cookie，则返回它的值
	}

	return "";
}

//删除cookie
function delCookie(name) {
	document.cookie = name + "=;expires=" + (new Date(0)).toGMTString();
}

//获取指定名称的cookie的值
function getCookie(objName) {
	var arrStr = document.cookie.split("; ");
	for (var i = 0; i < arrStr.length; i++) {
		var temp = arrStr[i].split("=");
		if (temp[0] == objName) return unescape(temp[1]);
	}
}

//添加cookie
function addCookie(objName, objValue, objHours) {
	var str = objName + "=" + escape(objValue);
	if (objHours > 0) {                               //为时不设定过期时间，浏览器关闭时cookie自动消失
		var date = new Date();
		var ms = objHours * 3600 * 1000;
		date.setTime(date.getTime() + ms);
		str += "; expires=" + date.toGMTString();
	}
	document.cookie = str;
}

//两个参数，一个是cookie的名子，一个是值
function SetCookie(name, value) {
	var Days = 30; //此 cookie 将被保存 30 天
	var exp = new Date();    //new Date("December 31, 9998");
	exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
	document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
}

//取cookies函数
function getCookie(name) {
	var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
	if (arr != null) return unescape(arr[2]); return null;
}

//删除cookie
function delCookie(name) {
	var exp = new Date();
	exp.setTime(exp.getTime() - 1);
	var cval = getCookie(name);
	if (cval != null) document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
}