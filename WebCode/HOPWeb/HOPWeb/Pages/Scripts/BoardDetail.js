function Call() {
    id = localStorage.BoardID;
    var xmlHttp = CreateHttpRequest();
    if (xmlHttp == null) {
        alert("此实例只能在支持Ajax的浏览器中运行");
    }

    // Create result handler
    xmlHttp.onreadystatechange = function() {
        if (xmlHttp.readyState == 4) {
            if (xmlHttp.status == 200) {
                var result = eval("(" + xmlHttp.responseText + " )");
                var spanSubject = document.getElementById("spanSubject");
                var spanCreateTime = document.getElementById("spanCreateTime");
                var spanAttachment = document.getElementById("spanAttachment");
                var spanDetail = document.getElementById("spanDetail");
                spanSubject.innerText = result.Title;
                spanCreateTime.innerText = formatTime(result.CreateTime);
                spanDetail.innerText = result.Detail;
                // 没做文件下载
                if (result.FileID != 0) {
                    spanAttachment.innerHTML = "<img src='../Images/Word.png'/>"
                }
            }
        }
    }

    var id = localStorage.BoardID;
    var requstHeader = localStorage.requstHeader;
    var url = getServiceAddress() + "MobileBoard?" + requstHeader + "&id=" + id;
    console.log("get MobileBoard detaile url: " + url);

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
    return da.getFullYear() + "/" + da.getMonth() + "/" + da.getDay();
}

function Back() {
    window.location.href = 'BoardList.htm';
}