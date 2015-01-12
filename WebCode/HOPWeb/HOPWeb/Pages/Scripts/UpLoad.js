function uploadPhoto() {
    var getFile = document.getElementById("getFile");
    var imageURI = getFile.value;
    uploadFile(imageURI);
    try {
        var url = "http://jcw.huaweisoft.com:7070/MarketWCF/MarketWCF.svc/UpLoadFile";
        var xmlHttp = CreateHttpRequest();
        if (xmlHttp == null) {
            alert("此实例只能在支持Ajax的浏览器中运行");
        }

        // Create result handler
        xmlHttp.onreadystatechange = function () {
            alert(xmlHttp.readyState);
            if (xmlHttp.readyState == 4) {
                var result = eval("(" + xmlHttp.responseText + " )").d;
                alert(result);
            }
        }

        //发送Http请求
        xmlHttp.open("Post", url, true);
        xmlHttp.setRequestHeader("Content-type", "text/json;charset=utf-8");
        xmlHttp.send(null);

        fileSystem.root.getFile(imageURI, { create: true }, readFile, onError);
        alert('kk');
    }
    catch (e) {
        alert(e.Message);
    }
}

function readFile(f) {
    alert('23')
    var reader = new FileReader();
    reader.readAsBinaryString(f);
    var fileData = fileReader.result; d
    alert(aaa);
    alert('kd')
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

function uploadFile(pickUrl) {
    var imageURI = pickUrl;
    if (!imageURI)
        alert('请先选择本地图片');
    var options = new FileUploadOptions();
    options.fileKey = "file";
    options.mimeType = "text/html";
    options.fileName = imageURI.substr(imageURI.lastIndexOf('/') + 1);
    var ft = new FileTransfer();
    ft.upload(
                        imageURI,
                        encodeURI('http://jcw.huaweisoft.com:7070/MarketWCF/MarketWCF.svc/UpLoadFile'),
                        function () { alert('上传成功!'); },
                        function () { alert('上传失败!'); },
                        options);
}


function uploadAndSubmit() {
    var form = document.forms["demoForm"];
    alert(11);
    if (form["file"].form.elements.length > 0) {
        // 寻找表单域中的 <input type="file" ... /> 标签
        var file = form["file"].form.elements[0];
        // try sending 
        var reader = new FileReader();

        reader.onloadstart = function () {
            // 这个事件在读取开始时触发
            console.log("onloadstart");
            document.getElementById("bytesTotal").textContent = file.size;
        }
        reader.onprogress = function (p) {
            // 这个事件在读取进行中定时触发
            console.log("onprogress");
            document.getElementById("bytesRead").textContent = p.loaded;
        }

        reader.onload = function () {
            // 这个事件在读取成功结束后触发
            console.log("load complete");
        }

        reader.onloadend = function () {
            alert(12);
            try {
                if (reader.error) {
                    alert(16);
                } else {
                    alert(22);
                    document.getElementById("bytesRead").textContent = file.size;
                    // 构造 XMLHttpRequest 对象，发送文件 Binary 数据
                    alert(23);
                    var xhr = new XMLHttpRequest();
                    alert(24);
                    xhr.open(/* method */"POST",
                    /* target url */"http://jcw.huaweisoft.com:7070/MarketWCF/MarketWCF.svc/UpLoadFile"
                    /*, async, default to true */);
                    alert(25);
                    xhr.overrideMimeType("application/octet-stream");
                    alert(26);
                    alert(reader.result);
                    alert(file.getAsBinary());
                    xhr.sendAsBinary(reader.result);
                    alert(reader.result);
                    alert(27);
                    xhr.onreadystatechange = function () {
                        alert(xhr.readyState);
                        if (xhr.readyState == 4) {
                            if (xhr.status == 200) {
                                alert(17);
                            }
                        }
                    }
                }
            }
            catch (e) {
                alert(e.Message);
            }
        }

        alert(13);
        reader.readAsBinaryString(file);
        alert(14);
    } else {
        alert("Please choose a file.");
    }
} 