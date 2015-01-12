var testObject;

function Back() {
    window.location.href = 'Return.htm';
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

    var url = "http://jcw.huaweisoft.com:7070/MarketWCF/MarketWCF.svc/AddReturn";
    var code = document.getElementById("sel").value.split(',')[4];
    var name = document.getElementById("txtName").textContent;
    var type = document.getElementById("txtType").textContent;
    if (type == "手机") {
        type = 0;
    }
    else {
        type = 1;
    }

    var reason = document.getElementById("txtReason").value;
    var amount = document.getElementById("txtAmount").value;
    var barcode = document.getElementById("barcode").value;
    var creator = localStorage.userName;

    // Create result handler
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState == 4) {
            var result = eval("(" + xmlHttp.responseText + " )").d;
            if (result) {
                window.location.replace("Return.htm");
            }
            else {
                alert("输入数据有误，请重新输入！");
            }
        }
    }

    var body = '{"code":"' + code + '","name":"' + name + '","type":"' + type + '","reason":"' + reason + '","amount":"' + amount + '","barcode":"' + barcode + '","creator":"' + creator + '"}';

    //发送Http请求
    xmlHttp.open("Post", url, true);
    xmlHttp.setRequestHeader("Content-type", "text/json;charset=utf-8");
    xmlHttp.send(body);
}

function Reset() {
    document.getElementById("sel").value = "";
    document.getElementById("txtName").innerText = "";
    document.getElementById("txtType").innerText = "";
    document.getElementById("txtReason").value = "";
    document.getElementById("txtAmount").value = "0";
}

function GetAllItem() {
    onBodyLoad();
    var xmlHttp = CreateHttpRequest();
    if (xmlHttp == null) {
        alert("此实例只能在支持Ajax的浏览器中运行");
    }

    var url = "http://http://jcw.huaweisoft.com:7070/MarketWCF/MarketWCF.svc/GetAllItem";

    // Create result handler
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState == 4) {
            var result = eval("(" + xmlHttp.responseText + " )").d;
            var grpPhone = document.getElementById("grpPhone");
            var grpLap = document.getElementById("grpLap");
            testObject = result;

            var phoneHTML = "";
            var lapHTML = "";
            for (var i = 0; i < result.length; i++) {
                var innerText = "<option label='" + result[i].Code + "' value='" + result[i].Name + "," + result[i].Type + "," + result[i].Price + "," + result[i].Barcode + "," + result[i].Code + "'>";
                if (result[i].Type == 0) {
                    phoneHTML += innerText;
                }
                if (result[i].Type == 1) {
                    lapHTML += innerText;
                }
            }

            grpPhone.innerHTML = phoneHTML;
            grpLap.innerHTML = lapHTML;
            var select = document.getElementById("sel");
            select.value = "";
        }
    }

    //发送Http请求
    xmlHttp.open("Get", url, true);
    xmlHttp.setRequestHeader("Content-type", "text/json;charset=utf-8");
    xmlHttp.send("");
}

function SetData() {
    var selectItem = document.getElementById("sel").value;
    var array = selectItem.split(',');

    var name = document.getElementById("txtName");
    name.innerText = array[0];
    var type = document.getElementById("txtType");
    if (array[1] == 0) {
        type.innerText = "手机";
    } else {
        type.innerText = "平板电脑";
    }

    var barcode = document.getElementById("barcode");
    barcode.innerText = array[3];
}