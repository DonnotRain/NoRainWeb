//获取网站根目录
function getRootPath() {
    var strFullPath = window.document.location.href;
    var strPath = window.document.location.pathname;
    var pos = strFullPath.indexOf(strPath);
    var prePath = strFullPath.substring(0, pos);
    var postPath = strPath.substring(0, strPath.substr(1).indexOf('/') + 1);
    return (prePath);
}

//读取cookies 
function getCookie(name) {
    var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");

    if (arr = document.cookie.match(reg))

        return unescape(arr[2]);
    else
        return null;
}

//删除cookies 
function delCookie(name) {
    var exp = new Date();
    exp.setTime(exp.getTime() - 1);
    var cval = getCookie(name);
    if (cval != null)
        document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
}
//如果需要设定自定义过期时间 
//那么把上面的setCookie　函数换成下面两个函数就ok; 

//设置Cookies 
function setCookie(name, value, time) {
    //如果设置有效期
    if (time) {
        var strsec = getsec(time);
        var exp = new Date();
        exp.setTime(exp.getTime() + strsec * 1);
        document.cookie = name + "=" + escape(value) + ";path=/;expires=" + exp.toGMTString();
    }
    else {
        document.cookie = name + "=" + escape(value) + ";path=/;";
    }
}

function getsec(str) {
    var str1 = str.substring(1, str.length) * 1;
    var str2 = str.substring(0, 1);
    if (str2 == "s") {
        return str1 * 1000;
    }
    else if (str2 == "h") {
        return str1 * 60 * 60 * 1000;
    }
    else if (str2 == "d") {
        return str1 * 24 * 60 * 60 * 1000;
    }
}

// 格式化日期
function formateDate(date, format) {
    if (!date) {
        return "";
    }
    if (!format) {
        format = "y/m/d";
    }
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var day = date.getDate();
    var hour = date.getHours();
    var minute = date.getMinutes();
    var second = date.getSeconds();
    var milliSec = date.getMilliseconds();
    format = format.replace(/y/g, year).replace(/m/g, month < 10 ? "0" + month : month).replace(/d/g, day < 10 ? "0" + day : day);
    format = format.replace(/h/g, hour < 10 ? "0" + hour : hour).replace(/M/g, minute < 10 ? "0" + minute : minute).replace(/s/g, second < 10 ? "0" + second : second).replace(/S/g, milliSec);
    return format;
}

//格式化形如/Date(1365640715187)/或2014-06-05T16:02:58.393的日期字符串
function formatJSONDate(dateStr, format) {
    if (!dateStr) {
        return "";
    }
    if (!format) {
        format = "y-m-d h:M:s";
    }
    var date = new Date();
    var maxStr = "yyyy-mm-dd hh:MM:ss";
    if (dateStr.indexOf("T") > 0) {
        if (dateStr.length > maxStr.length)
        {
            dateStr = dateStr.substring(0, maxStr.length - 1);
        }
        dateStr = dateStr.replace("T", " ").replace(/\-/g, "/");
        date = new Date(dateStr);
        
    } else {
        dateStr = dateStr.substring(dateStr.indexOf("(") + 1, dateStr.indexOf(")"));
        if (!/^\d+$/.test(dateStr)) {
            return "";
        }
        date.setTime(parseInt(dateStr));
    } 
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var day = date.getDate();
    var hour = date.getHours();
    var minute = date.getMinutes();
    var second = date.getSeconds();
    var milliSec = date.getMilliseconds();
    format = format.replace(/y/g, year).replace(/m/g, month < 10 ? "0" + month : month).replace(/d/g, day < 10 ? "0" + day : day);
    format = format.replace(/h/g, hour < 10 ? "0" + hour : hour).replace(/M/g, minute < 10 ? "0" + minute : minute).replace(/s/g, second < 10 ? "0" + second : second).replace(/S/g, milliSec);
    return format;
}


// 日期式化器
function dateFormatter(value, row, index) {
    if (!value) {
        return "";
    }

    return formatJSONDate(value, 'y-m-d');

}

// 日期式化器
function timeFormatter(value, row, index) {
    if (!value) {
        return "";
    }

    return formatJSONDate(value, 'y-m-d h:M');

}

//扩展通用Ajax方法
$.CommonAjax = function (params) {
    var defaultParams = {
        type: "post",
        //  url: "test.json",
        data: {},
        dataType: "json",
        timeout: 20000,
        async: true,
        cache: true,
        beforeSend: function (XMLHttpRequest) {
            this;   //调用本次ajax请求时传递的options参数
        },
        //要求为Function类型的参数，请求完成后调用的回调函数（请求成功或失败时均调用）。
        //参数：XMLHttpRequest对象和一个描述成功请求类型的字符串。
        complete: function (XMLHttpRequest, textStatus) {
            this;    //调用本次ajax请求时传递的options参数
        },
        success: function (data, textStatus) {
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            var response = XMLHttpRequest.responseText;
            try {

                response = JSON.parse(response);
            }
            catch (ex) {
                response = {};
                response.Message = XMLHttpRequest.responseText;
            }
            $.jMask("HideAll");
            Messenger().post({ message: "请求出错<br />" + response.Message || textStatus, type: "error", hideAfter: 3 });
        },
        contentType: "application/x-www-form-urlencoded",
        dataFilter: function (data, type) {
            return data;
        },
        global: true//示是否触发全局ajax事件。设置为false将不会触发全局
    };
    var data = $.extend(defaultParams, params);
    $.ajax(data);
}
/*
message: The text of the message
type: info, error or success are understood by the provided themes. You can also pass your own string, and that class will be added.
theme: What theme class should be applied to the message? Defaults to the theme set for Messenger in general.
id: A unique id. If supplied, only one message with that ID will be shown at a time.
singleton: Hide the newer message if there is an id collision, as opposed to the older message.
actions: Action links to put in the message, see the 'Actions' section on this page.
hideAfter: Hide the message after the provided number of seconds
hideOnNavigate: Hide the message if Backbone client-side navigation occurs
showCloseButton: Should a close button be added to the message?
closeButtonText: Specify the text the close button should use (default ×)

*/
$.Show = function (params) {
    var defaultParams = {
        message: "操作错误",
        type: "error",
        hideAfter: 3
    };
    var data = $.extend(defaultParams, params);

    Messenger().post(data);
}

function treegridFilter(source, firstValue, childrenField, idField, parentIdField) {


    var filterFunc = function (idValue) {
        var items = [];
        for (var i = 0; i < source.length; i++) {

            if (source[i][parentIdField] == idValue) {
                source[i][childrenField] = filterFunc(source[i][idField]);
                source[i].iconCls = "none icon-search icon-fixed-width";
                items.push(source[i]);
                //清除图标样式

                // source.splice(i, 1);
                //   i -= 1;
            }
        }
        return items;
    }

    //如果没有传默认的第一级父节点Id,则自己判断
    if (!firstValue) {
        for (var i = 0; i < source.length; i++) {

            var item = source[i];
            var hasParent = false;
            for (var j = 0; j < source.length; j++) {
                if (source[j][idField] == item[parentIdField]) {
                    hasParent = true;
                    break;;
                }
            }
            if (!hasParent) {
                firstValue = item[idField];
            }
        }
    }

    var firstItems = filterFunc(firstValue);
    return firstItems;
}

//对象深拷贝
function deepcopy(source) {
    var isArray = function (v) {
      //  Object.prototype.toString.call(o) === ‘[object Array]‘;
        return  Object.prototype.toString.apply(v) === '[object Array]';
    }
    var destination = {};

    if (isArray(source)) {
        destination = [];
    }

    $.extend(true, destination, source);
    return destination;
}

// 生成guid
function guid() {
    function S4() {
        return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
    }
    return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
}

function formatterTime(value,format) {
    var position = value.indexOf('T');
    var time = value.substr(0, position);
    var time2 = value.substr(position + 1);
    var date = new Date(time + ' ' + time2);
    if (format == undefined) {
        format="yyyy-MM-dd"
    }

    var value = format.replace(/yyyy/g, date.getFullYear()).replace(/MM/g, date.getMonth()).replace(/dd/g, date.getDate()).replace(/HH/g, date.getHours()).replace(/mm/g, date.getMinutes()).replace(/ss/g, date.getSeconds());

    return value;
}

String.prototype.stripHTML = function () {
    var reTag = /<(?:.|\s)*?>/g;
    return this.replace(reTag, "");
}
