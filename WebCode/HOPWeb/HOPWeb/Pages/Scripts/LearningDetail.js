
function Call() {
    id = localStorage.LearningID;
    var xmlHttp = CreateHttpRequest();
    if (xmlHttp == null) {
        alert("此实例只能在支持Ajax的浏览器中运行");
    }

    // Create result handler
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState == 4) {
            var result = eval("(" + xmlHttp.responseText + " )").d;
            var spanSubject = document.getElementById("spanSubject");
            var spanCreateTime = document.getElementById("spanCreateTime");
            var spanType = document.getElementById("spanType");
            var spanAttachment = document.getElementById("spanAttachment");
            var spanDetail = document.getElementById("spanDetail");
            spanSubject.innerText = result.Title;
            spanCreateTime.innerText = renderTime(result.CreateTime);
            spanDetail.innerText = result.Detail;
            if (result.FileID != 0) {
                spanAttachment.innerHTML = "<img src='../Images/Word.png'/>"
            }
            spanType.innerText = GetLearningType(result.CreateTime.Type)
        }
    }

    var url = "http://jcw.huaweisoft.com:7070/TrainingWCF/TrainingWCF.svc/GetLearningDetail";
    var body = '{"id":"' + id + '"}';

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
    return da.getFullYear() + "/" + da.getMonth() + "/" + da.getDay();
}

function Back() {
    window.location.href = 'LearningList.htm';
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

function GetContent() {
    window.location.href = 'LearningContentDetail.htm';
}
