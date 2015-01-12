var count = 0;

function Call() {
    id = localStorage.ExamID;
    seq = localStorage.JudgementForm;
    var xmlHttp = CreateHttpRequest();
    if (xmlHttp == null) {
        alert("此实例只能在支持Ajax的浏览器中运行");
    }

    // Create result handler
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState == 4) {
            var result = eval("(" + xmlHttp.responseText + " )").d;
            var spanSubject = document.getElementById("spanSubject");
            var spanTime = document.getElementById("spanTime");
            spanSubject.innerText = result.Title;
            spanTime.innerText = "计时:" + result.AnswerTime + "秒";
            count = result.AnswerTime;
            timeCount();
        }
    }

    var url = "http://jcw.huaweisoft.com:7070/TrainingWCF/TrainingWCF.svc/GetJudgeDetail";
    var body = '{"examid":"' + id + '","seq":"' + seq + '"}';

    //发送Http请求
    xmlHttp.open("Post", url, true);
    xmlHttp.setRequestHeader("Content-type", "text/json;charset=utf-8");
    xmlHttp.send(body);
}

function timeCount() {
    document.getElementById("spanTime").innerText = "计时:" + count + "秒";
    count = count - 1;
    if (count >= 0) {
        t = setTimeout("timeCount()", 1000);
    } else {
        window.location.href = 'ChoiceForm.htm';
    }
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

function GetNext() {
    localStorage.ChoiceForm = 1;
    count = -1;
}

function clickRadio(str) {
    var rad = document.getElementById('"' + str + '"');
    str.checked = "checked";
}
