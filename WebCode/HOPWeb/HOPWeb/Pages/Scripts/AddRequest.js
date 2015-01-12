function Back() {
    window.location.href = 'RequestList.htm';
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

    var requstHeader = localStorage.requstHeader;
    var url = getServiceAddress() + "MobileRequest?" + requstHeader;
    var title = document.getElementById("txtTitle").value;
    var detail = document.getElementById("txtDetail").value;
    var body = '{"title":"' + title + '","detail":"' + detail + '","creator":"' + creator + '","fileID":"' + 0 + '"}';

    // Create result handler
    xmlHttp.onreadystatechange = function() {
        if (xmlHttp.readyState == 4) {
            if (xmlHttp.status == 200) {
                var result = eval("(" + xmlHttp.responseText + " )");
                if (result.Title == title) {
                    Back();
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
    document.getElementById("txtTitle").value = "";
    document.getElementById("txtDetail").value = "";
}