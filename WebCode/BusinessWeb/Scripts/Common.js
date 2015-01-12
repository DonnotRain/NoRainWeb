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
            $.gritter.add({
                // (string | mandatory) the heading of the notification
                title: '请求出错!',
                // (string | mandatory) the text inside the notification
                text: response.Message || textStatus,
                class_name: 'gritter-error'
                // time: 3000,
            });
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

$.Show = function (params) {
    var defaultParams = {
        message: "操作错误",
        type: "error",
        hideAfter: 3
    };
    var data = $.extend(defaultParams, params);

    Messenger().post(data);
}


//The full list of options:
//message: The text of the message
//type: info, error or success are understood by the provided themes. You can also pass your own string, and that class will be added.
//    theme: What theme class should be applied to the message? Defaults to the theme set for Messenger in general.
//    id: A unique id. If supplied, only one message with that ID will be shown at a time.
//    singleton: Hide the newer message if there is an id collision, as opposed to the older message.
//    actions: Action links to put in the message, see the 'Actions' section on this page.
//    hideAfter: Hide the message after the provided number of seconds
//hideOnNavigate: Hide the message if Backbone client-side navigation occurs
//showCloseButton: Should a close button be added to the message?
//closeButtonText: Specify the text the close button should use (default ×)
//Messenger also includes aliases which set the type for you: Messenger().error(), Messenger().success(), and messenger().info().

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
function deepcopy(destination, source) {
    if (source instanceof Date) {
        destination = new Date();
        destination.setTime(source.getTime());
        return destination;
    }

    for (var p in source) {
        if (typeof (source[p]) == "array" || typeof (source[p]) == "object") {
            destination[p] = typeof (source[p]) == "array" ? [] : {};
            arguments.callee(destination[p], source[p]);
        } else {
            destination[p] = source[p];
        }
    }

}


function combogridFilter(source, firstValue, childrenField, idField, parentIdField, textField) {


    var filterFunc = function (idValue) {
        var items = [];
        for (var i = 0; i < source.length; i++) {

            if (source[i][parentIdField] == idValue) {
                source[i][childrenField] = filterFunc(source[i][idField]);
                source[i].iconCls = "none " + source[i]["ImageIndex"] + " icon-fixed-width";
                source[i].text = source[i][textField];
                source[i].id = source[i][idField];
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
    var rootItem = {};
    deepcopy(rootItem, firstItems[0]);

    rootItem[childrenField] = firstItems;
    rootItem[idField] = firstValue;
    rootItem[textField] = "根节点";
    rootItem.iconCls = "none icon-th-large   " + " icon-fixed-width";
    rootItem.text = "根节点";
    rootItem.id = firstValue;

    return [rootItem];
}

