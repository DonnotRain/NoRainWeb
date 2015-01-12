
function Call() {
    var xmlHttp = CreateHttpRequest();
    if (xmlHttp == null) {
        alert("此实例只能在支持Ajax的浏览器中运行");
    }

    // Create result handler
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState == 4) {
            if (xmlHttp.responseText != "") {
                var result = eval("(" + xmlHttp.responseText + " )").d;
                var htmlStr = "<table align=center width=100%>";
                for (var i = 0; i < result.length; i++) {
                    htmlStr += "<tr>" +
                             "<td><ui onclick=\"GetDetail(" + result[i].ID + ")\">";
                    htmlStr += "<li class='linkLi'>" + result[i].Title + "</li>";
                    htmlStr += "<li class='twoLi'>描述： <div>" + result[i].Detail + "</div></li>";
                    htmlStr += "<li class='twoLi'>时间： " + renderTime(result[i].BeginTime) + "</li></td></tr>";
                    htmlStr += "<tr class='listSpan'><td></td></tr>"
                }
                var div = document.getElementById("content");
                div.innerHTML = htmlStr + "</table>";
            }
        }
    }

    var url = "http://jcw.huaweisoft.com:7070/TrainingWCF/TrainingWCF.svc/GetExamData";

    //发送Http请求
    xmlHttp.open("Post", url, true);
    xmlHttp.setRequestHeader("Content-type", "text/json;charset=utf-8");
    xmlHttp.send("");
}

function GetDetail(i) {
    localStorage.ExamID = i;
    window.location.href = 'ExamDetail.htm';
}

function GetLearningType(value) {
    var type = ""
    if (value == 0) {
        type = "市场";
    }
    else if (value == 1) {
        type = "商品";
    }
    else {
        type = "规章制度";
    }

    return type;
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
    return da.getFullYear() + "/" + da.getMonth() + "/" + da.getDay();
}

function Back() {
    window.location.href = 'TrainingManager.htm';
}