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
                console.log("get mobileboard list result length: " + result.length);
                
                var htmlStr = "<table align=center width=100%>";
                for (var i = 0; i < result.length; i++) {
                    htmlStr += "<tr>" +
                        "<td><ui onclick=\"GetDetail(" + result[i].ID + ")\">";
                    htmlStr += "<li class='linkLi'>" + result[i].Title + "</li>";
                    htmlStr += "<li class='twoLi'>描述： " + result[i].Detail + "</li>";
                    htmlStr += "<li class='twoLi'>时间： " + formatTime(result[i].CreateTime) + "</li></td></tr>";
                    htmlStr += "<tr class='listSpan'><td></td></ul></tr>"
                }
                var div = document.getElementById("content");
                div.innerHTML = htmlStr + "</table>";
            }
        }
    }

    // var url = "http://jcw.huaweisoft.com:7070/BoardWCF/BoardWCF.svc/GetBoardData";
    var requstHeader = localStorage.requstHeader;
    var url = getServiceAddress() + "MobileBoard?" + requstHeader;

    //发送Http请求
    xmlHttp.open("GET", url, true);
    xmlHttp.send();
}

function GetDetail(i) {
    localStorage.BoardID = i;
    window.location.href = 'BoardDetail.htm';
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
    return da.getFullYear() + "/" + da.getMonth() + "/" + da.getDay();
}

function Back() {
    window.location.href = '../MainMenu.htm';
}