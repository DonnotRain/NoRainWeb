function Back() {
    window.location.href = 'LeaveList.htm';
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

function Submit() {

    var xmlHttp = CreateHttpRequest();
    if (xmlHttp == null) {
        alert("此实例只能在支持Ajax的浏览器中运行");
    }

    var beginDate = document.getElementById("txtBeginDate").value;
    var endDate = document.getElementById("txtEndDate").value;
    var duration = document.getElementById("txtDuration").value;
    var reason = document.getElementById("txtReason").value;

    var requstHeader = localStorage.requstHeader;
    var url = getServiceAddress() + "MobileLeave?" + requstHeader;
    var body = '{"beginDate":"' + beginDate + '","endDate":"' + endDate + '","duration":"' + duration + '","reason":"' + reason + '"}';

    // Create result handler
    xmlHttp.onreadystatechange = function() {
        if (xmlHttp.readyState == 4) {
            if (xmlHttp.status == 200) {
                var result = eval("(" + xmlHttp.responseText + " )");
                console.log("add leave result: " + result);
                if (result) {
                    window.location.href = 'LeaveList.htm';
                } else {
                    alert("输入数据有误，请重新输入！");
                }
            }
        }
    }

    //发送Http请求
    xmlHttp.open("Post", url, true);
    xmlHttp.setRequestHeader("Content-type", "text/json;charset=utf-8");
    xmlHttp.send(body);
}

function Reset() {
    document.getElementById("txtBeginDate").value = "";
    document.getElementById("txtEndDate").value = "";
    document.getElementById("txtDuration").value = "";
    document.getElementById("txtReason").value = "";
}