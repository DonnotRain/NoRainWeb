//document.write("<script language='javascript' src='jquery.js'></script>");
//document.write("<script language='javascript' src='jquery.mobile-1.3.2.js'></script>");
//document.write("<script language='javascript' src='Scripts/cordova.js'></script>");

function onConfirm(button) {
	if (button == 1)
		navigator.app.exitApp();
	//选择了确定才执行退出
}

function myDeviceready() {
	// console.log('设备加载完成');
	document.addEventListener('backbutton', onBackKeyDown, false);
}

//点击返回按钮的事件
function onBackKeyDown() {
	//getCurrentLocation();
	// console.log(window.location.href);
	if (window.location.href == "file:///android_asset/www/Login.htm" || window.location.href == "file:///android_asset/www/MainMenu.htm") {
		navigator.notification.confirm('确定退出 3C外勤通 吗？', onConfirm, '提示', ['确定', '取消']);
	} else {
		history.go(-1);
	}
}


$(document).ready(function() {
	document.addEventListener("deviceready", myDeviceready, false);
});

/* cordova.js 引用冲突了 */
// 如果直接使用$(function(){...})形式，会出现'$'未定义的错误；网上搜索的解决办法：
// 因为加载'jquery.js'需要大约2ms的时间，所以使用$(function(){...})的形式会报错，
// 应该使用setTimeout()等待'jquery.js'加载完成再执行。
//setTimeout(function() {
//	document.addEventListener('deviceready', myDeviceready, false);
//}, 10);