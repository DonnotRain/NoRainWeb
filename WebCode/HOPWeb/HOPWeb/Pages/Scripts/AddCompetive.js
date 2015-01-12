function Back() {
    window.location.href = 'CompetiveList.htm';
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

function Submit() {

    var xmlHttp = CreateHttpRequest();
    if (xmlHttp == null) {
        alert("此实例只能在支持Ajax的浏览器中运行");
    }

    var url = "http://jcw.huaweisoft.com:7070/MarketWCF/MarketWCF.svc/AddInfomation";
    var title = document.getElementById("txtTitle").value;
    var detail = document.getElementById("txtDetail").value;
    var creator = localStorage.userName;

    // Create result handler
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState == 4) {
            var result = eval("(" + xmlHttp.responseText + " )").d;
            if (result) {
                var submit = document.getElementById("submit");
                submit.click();
                Back();
            }
            else {
                alert("输入数据有误，请重新输入！");
            }
        }
    }

    var body = '{"title":"' + title + '","detail":"' + detail + '","creator":"' + creator + '","fileID":"' + 0 + '"}';

    //发送Http请求
    xmlHttp.open("Post", url, true);
    xmlHttp.setRequestHeader("Content-type", "text/json;charset=utf-8");
    xmlHttp.send(body);
}

function Reset() {
    document.getElementById("txtTitle").value = "";
    document.getElementById("txtDetail").value = "";
}

function clickGetFile() {
    var getFile = document.getElementById("getFile");
    getFile.click();
}