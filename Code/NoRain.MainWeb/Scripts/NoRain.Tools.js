var NoRainTools = (function ($) {
    //jQuery扩展，清理表单

    $.fn.extend({
        clear: function () {
            return this.each(function () {
                $(this).find(':input').not(':button, :submit, :reset, :hidden').val('').removeAttr('checked').removeAttr('selected');
            });
        }
    });
    //内部方法
    var isArray = function (v) {
        //  Object.prototype.toString.call(o) === ‘[object Array]‘;
        return Object.prototype.toString.apply(v) === '[object Array]';
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
            if (dateStr.length > maxStr.length) {
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

    //扩展Web框架之Ajax方法
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
                Metronic.unblockUI(params.targetBlock);
                toastr.error("请求出错<br />" + response.Message || textStatus);
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

    //对象深拷贝，基于jQuery
    function deepcopy(source) {

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

    //去除全部HTML标签
    String.prototype.stripHTML = function () {
        var reTag = /<(?:.|\s)*?>/g;
        return this.replace(reTag, "");
    }

    /// By：wxt Date：2015/02/08
    ///加载下拉项
    ///$ele 目标元素
    ///data 数组
    ///textfield, valueField分别为显示的字段和值字段
    ///widthNullOption 是否需要请选择项
    function LoadSelectOption($ele, data, textField, valueField, widthNullOption) {
        $($ele).each(function (ele, index) {
            var $this = $(this).html("");
            if (widthNullOption) {
                var option = $("<option></option>").html("请选择").attr("selected", "selected");
                $this.append(option);
            }
            for (var i = 0; i < data.length; i++) {
                var option = $("<option></option>").html(data[i][textField]).attr("value", data[i][valueField]);
                $this.append(option);
            }
        });

        return $ele;
    }
    /// By：wxt Date：2015/02/08
    ///加载下拉项
    ///$ele 目标元素
    ///设置的值 数组或","分隔的字符串
    ///textfield, valueField分别为显示的字段和值字段
    ///widthNullOption 是否需要请选择项
    function SetSelectValues($ele, values) {
        if (!isArray(values)) {
            values = values.toString().split(',');
        }
        $($ele).each(function (ele, index) {
            var $this = $(this);
            var options = $this.children("option");
            $("option", $this).prop("selected", false);
            for (var i = 0; i < values.length; i++) {
                $("option[value='" + values[i] + "']", $this).prop("selected", true);
            }
        });

        return $ele;
    }


    //提示框用法示例
    var UIToastr = function () {
        return {
            //main function to initiate the module
            usages: function (option) {
                // Display a warning toast, with no title
                toastr.warning('My name is Inigo Montoya. You killed my father, prepare to die!')

                // Display a success toast, with a title
                toastr.success('Have fun storming the castle!', 'Miracle Max Says')

                // Display an error toast, with a title
                toastr.error('I do not think that word means what you think it means.', 'Inconceivable!')

                // Clears the current list of toasts
                toastr.clear()

            }

        };

    }();

    //设置表单数组中的一项值，没有则添加进来
    //array 序列化后的表单数组
    //filed 表单项的名称
    //要设置的值
    function SetFormArrayValue(array, filed, value) {
        var isHasFiled = false;
        for (var i = 0; i < array.length; i++) {
            if (array[i]["name"] == filed) {
                array[i]["value"] = value;
                isHasFiled = true;
                break;
            }
        }
        if (!isHasFiled) {
            array.push({ name: filed, value: value });
        }
    }

    //由序列化的表单值（jqueryserializeArray方法）
    //绑定到原纪录中
    //用于纪录的部分字段编辑更新
    function BindFormItem(serializeArray, srcItem) {
        try {
            for (var i = 0; i < serializeArray.length; i++) {
                srcItem[serializeArray[i].name] = serializeArray[i].value;
            }
        }
        catch (e) {
            throw new Error("转换序列换表单出错");
        }
    }

    return {
        //获取网站根目录
        getRootPath: function () {
            //优先从设置的根目录获取
            if (window.rootPath) return window.rootPath;
            var strFullPath = window.document.location.href;
            var strPath = window.document.location.pathname;
            var pos = strFullPath.indexOf(strPath);
            var prePath = strFullPath.substring(0, pos);
            var postPath = strPath.substring(0, strPath.substr(1).indexOf('/') + 1);
            return (prePath);
        }
        //读取cookies
        , getCookie: function (name) {
            var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");

            if (arr = document.cookie.match(reg))

                return unescape(arr[2]);
            else
                return null;
        }
        //删除cookies 
        , delCookie: function (name) {
            var exp = new Date();
            exp.setTime(exp.getTime() - 1);
            var cval = getCookie(name);
            if (cval != null)
                document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
        }
        //设置Cookies 
        , setCookie: function (name, value, time) {
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
       , dateFormatter: dateFormatter
        , timeFormatter: timeFormatter
        , deepcopy: deepcopy
        , LoadSelectOption: LoadSelectOption
        , SetSelectValues: SetSelectValues
        , SetFormArrayValue: SetFormArrayValue
        , BindFormItem: BindFormItem
    }

})(jQuery);







