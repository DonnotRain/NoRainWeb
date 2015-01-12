function ChangePic(flag) {
	var picLightLoginButton = document.getElementById("picLightLoginButton");
	if (flag) {
		picLightLoginButton.src = "Images/DeepLoginButton.png";
	} else {
		if (picLightLoginButton.src = "Images/DeepLoginButton.png") {
			picLightLoginButton.src = "Images/LightLoginButton.png";
		}
	}
}

function Submit() {
	var xmlHttp = CreateHttpRequest();
	if (xmlHttp == null) {
		alert("此实例只能在支持Ajax的浏览器中运行");
	}

	var ip = getServiceAddress();
	var url = ip + "Authentication";
	var name = document.getElementById("txtUserName").value;
	var password = document.getElementById("txtPassWord").value;
	var corpcode = document.getElementById("txtCorpCode").value;
	// console.log("name: " + name);
	// console.log("corpcode: " + corpcode);
	url = url + "?name=" + name + "&password=" + password + "&corpcode=" + corpcode + "";

	//console.log(url);
	// Create result handler
	xmlHttp.onreadystatechange = function() {
		if (xmlHttp.readyState == 4) {
			if (xmlHttp.status == 200) {
				var result = eval("(" + xmlHttp.responseText + " )").UserName;
				if (result == name) {
					localStorage.userName = name;
					localStorage.corpCode = corpcode;
					localStorage.requstHeader = "userName=" + name + "&corpCode=" + corpcode;
					window.location.href = 'MainMenu.htm';
				} else {
					alert("用户名或密码或企业码有误，请重新输入！");
				}
			} else {
				alert("登录失败，请检查网络连接！\n" + xmlHttp.status);
				// window.location.href = 'MainMenu.htm';
			}
		}
	}

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