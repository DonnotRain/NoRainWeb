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
                console.log("getLeave result: " + result);
                var htmlStr = "<table align=center width=100%>";
                for (var i = 0; i < result.length; i++) {
                    var state = "";
                    // if (result[i].State == 0) {
                    state = "未批复";
                    // } else if (result[i].State == 1) {
                    //     state = "已批准";
                    // } else {
                    //     state = "已拒绝";
                    // }
                    htmlStr += "<tr>" +
                        "<td><ui><li class='firstLi'>" + result[i].Reason + "</li>";
                    htmlStr += "<li class='twoLi'>时间： " + formatTime(result[i].BeginDate) + " 至 " + formatTime(result[i].EndDate) + "</li>";
                    htmlStr += "<li class='twoLi'>时长： " + result[i].Duration + "小时</li>";
                    htmlStr += "<li class='twoLi'>状态： " + state + "</li></td></tr>";
                    htmlStr += "<tr class='listSpan'><td></td></tr>"
                }
                var div = document.getElementById("content");
                div.innerHTML = htmlStr + "</table>";
            }
        }
    }

    var requstHeader = localStorage.requstHeader;
    var url = getServiceAddress() + "MobileLeave?" + requstHeader;
    console.log("getMobileLeaveList url: " + url);

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
    var hour = da.getHours();
    if (hour < 10) {
        hour = "0" + hour;
    }
    var minute = da.getMinutes();
    if (minute < 10) {
        minute = "0" + minute;
    }

    return da.getFullYear() + "/" + (da.getMonth() + 1) + "/" + da.getDate() + " " + hour + ":" + minute;
}

function Back() {
    window.location.href = 'AttendManager.htm';
}

function Add() {
    window.location.href = 'AddLeave.htm';
}

function formatTime(data) {
    //console.log(data);
    var timestamp = data.replace('T', ' ').replace(/.\W*$/, "");
    return timestamp;
}