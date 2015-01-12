function Call() {
    var xmlHttp = CreateHttpRequest();
    if (xmlHttp == null) {
        alert("此实例只能在支持Ajax的浏览器中运行");
    }

    // Create result handler
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState == 4) {
            var result = eval("(" + xmlHttp.responseText + " )").d;
            var htmlStr = "<table align=center width=100%>";
            for (var i = 0; i < result.length; i++) {
                htmlStr += "<tr>" +
                             "<td><li class='firstLi'>" + result[i].Name + "</li>";
                htmlStr += "<li class='twoLi'>数量：" + result[i].Amount + "</li>";
                htmlStr += "<li class='twoLi'>退货时间：" + renderTime(result[i].Time) + "</li>";
                htmlStr += "<li class='twoLi'>原因： " + result[i].Reason + "</li></td></tr>";
                htmlStr += "<tr class='listSpan'><td></td></tr>"
            }
            var div = document.getElementById("content");
            div.innerHTML = htmlStr + "</table>";
        }
    }
    var name = localStorage.userName;
    var body = '{"userName":"' + name + '"}';
    var url = "http://jcw.huaweisoft.com:7070/MarketWCF/MarketWCF.svc/GetReturnData";

    //发送Http请求
    xmlHttp.open("Post", url, true);
    xmlHttp.setRequestHeader("Content-type", "text/json;charset=utf-8");
    xmlHttp.send(body);
}

//创建HttpRequest对象
function CreateHttpRequest() {
    var httpRequest;
    try {
        httpRequest = new XMLHttpRequest();
    }
    catch (e) {
        try {
            httpRequest = new ActiveXObject("Msxml2.XMLHTTP");
        }
        catch (e) {
            try {
                httpRequest = new ActiveXObject("Microsoft.XMLHTTP");
            }
            catch (e) {
                return null;
            }
        }
    }

    return httpRequest;
}

function renderTime(data) {
    var da = eval('new ' + data.replace('/', '', 'g').replace('/', '', 'g'));
    return da.getFullYear() + "/" + (da.getMonth() + 1) + "/" + da.getDate();
}

function Back() {
    window.location.href = 'MarketManager.htm';
}

function Add() {
    window.location.href = 'AddReturn.htm';
}