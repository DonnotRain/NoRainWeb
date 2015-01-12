function setCookie(c_name,value,expiredays) {
    var exdate = new Date();
     exdate.setDate(exdate.getDate() + expiredays);
     // 使设置的有效时间正确。增加toGMTString()
     document.cookie = c_name + "=" +escape(value) + ((expiredays == null) ? ";domain=youdao.com" : ";domain=youdao.com;expires=" + exdate.toGMTString());
}

function setCookieEx(c_name,value,expiredays) {
    var exdate = new Date();
     exdate.setDate(exdate.getDate() + expiredays);
     // 使设置的有效时间正确。增加toGMTString()
     document.cookie = c_name + "=" +escape(value) + ((expiredays == null) ? "" : ";expires=" + exdate.toGMTString());
}

function getQueryString(name){
    // 如果链接没有参数，或者链接中不存在我们要获取的参数，直接返回空
    if(location.href.indexOf("?")==-1 || location.href.indexOf(name+'=')==-1)
    {
        return '';
    }
 
    // 获取链接中参数部分
    var queryString = location.href.substring(location.href.indexOf("?")+1);
 
    // 分离参数对 ?key=value&key2=value2
    var parameters = queryString.split("&");
 
    var pos, paraName, paraValue;
    for(var i=0; i<parameters.length; i++)
    {
        // 获取等号位置
        pos = parameters[i].indexOf('=');
        if(pos == -1) { continue; }
 
        // 获取name 和 value
        paraName = parameters[i].substring(0, pos);
        paraValue = parameters[i].substring(pos + 1);
 
        // 如果查询的name等于当前name，就返回当前值，同时，将链接中的+号还原成空格
        if(paraName == name)
        {
            return unescape(paraValue.replace(/\+/g, " "));
        }
    }
    return '';
}

function invite(){
    var code = getQueryString("inviteCode");
    setCookie('YNOTE_INVITE', code, 30);
}

function setVendor(){
    var vendor = getQueryString("vendor");
    if (vendor.length == 0) {
    } else {
        window.vendorCode = vendor;
        setCookieEx("vendor", vendor, 30);
    }
}

function invitation(spec){
    var code = getQueryString("invitation");
    if(!code){ return; }
    $.ajax({
        'type':'POST',
        'dataType': 'json',
        'url': '/yws/mapi/user?method=register&keyfrom=web&invitation=' + code,
        'cache':false,
        'success': spec.success,
        'error': spec.error
    });
};
