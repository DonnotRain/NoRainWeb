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
                // 具有庞大的返回数据，怎么处理？
                console.log("get Sell list: " + result);

                var htmlStr = "<table align=center width=100%>";
                for (var i = 0; i < result.length; i++) {
                    htmlStr += "<tr>" +
                        "<td><li class='firstLi'>" + result[i].Name + "</li>";
                    htmlStr += "<li class='twoLi'>数量： " + result[i].Amount + "件</li>";
                    htmlStr += "<li class='twoLi'>销售时间： " + formatTime(result[i].Time) + "</li></td></tr>";
                    htmlStr += "<tr class='listSpan'><td></td></tr>"
                }
                var div = document.getElementById("content");
                div.innerHTML = htmlStr + "</table>";
            }
        }
    }

    var requstHeader = localStorage.requstHeader;
    var url = getServiceAddress() + "Sell?" + requstHeader;

    //发送Http请求
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
    return da.getFullYear() + "/" + (da.getMonth() + 1) + "/" + da.getDate();
}

function Back() {
    window.location.href = 'MarketManager.htm';
}

function Add() {
    window.location.href = 'AddSell.htm';
}