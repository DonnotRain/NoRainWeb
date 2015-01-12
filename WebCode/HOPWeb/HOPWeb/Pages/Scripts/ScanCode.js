function onBodyLoad() {
    document.addEventListener("deviceready", onDeviceReady, false);
}

function onDeviceReady() {
    // do your thing!
}

function callNativePlugin(returnSuccess) {
    HelloPlugin.callNativeFunction(nativePluginResultHandler, nativePluginErrorHandler, returnSuccess);
}

function nativePluginResultHandler(result) {
    alert("SUCCESS: \r\n" + result);
}

function nativePluginErrorHandler(error) {
    alert("ERROR: \r\n" + error);
}

function ScanCode() {
    window.plugins.barcodeScanner.scan(function (result) {
        var codeItem;
        for (var i = 0; i < testObject.length; i++) {
            if (testObject[i].Barcode == result.text) {
                codeItem = testObject[i];
            }
        }
        if (codeItem == null) {
            alert('未能查询到对应的条码!');
        }
        else {
            var select = document.getElementById("sel");
            select.value = codeItem.Name + ',' + codeItem.Type + ',' + codeItem.Price + ',' + codeItem.Barcode + ',' + codeItem.Code;
            SetData();
        }
    }, function (error) {
        alert("Scanning failed: " + error);
    }
 	);
}