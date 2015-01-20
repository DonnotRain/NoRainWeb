/// <reference path="jquery-1.10.2.intellisense.js" />

(function () {
    $.extend($, {
        jMask: function (name, message, renderTo) {

            if (name == "HideAll") {
                $(".mask-loading-container").hide();
                $(".mask-widget-overlay").hide();

            }
            //默认添加到Body中
            if (!renderTo) {
                renderTo = "body";
            }
            var mask = {};
            //判断元素的遮罩层集合
            if (!window.JqueryMasks || window.JqueryMasks[renderTo] || window.JqueryMasks[renderTo][name]) {

                window.JqueryMasks = window.JqueryMasks || [];
                window.JqueryMasks[renderTo] = window.JqueryMasks[renderTo] || [];

                if (!name) {
                    throw EventException("遮罩层名字不能为空");
                }

                function jqueryMask(ele) {

                }

                jqueryMask.prototype.show = function (text) {
                    if (!text) {
                        text = message;
                    }

                    //如果存在，则直接显示
                    if ($("#" + "mask-widget-" + name).length) {
                        //   $("#" + "mask-widget-" + name).html(text);
                        $("#" + "mask-widget-" + name).show();
                        $("#" + "mask-loading-" + name).html(text);
                        $("#" + "mask-loading-" + name).show();
                        return;
                    }

                    //否则创建Dom
                    var maskEle = $("<div  class=\"mask-widget-overlay mask-front\" ></div>");
                    maskEle.attr("id", "mask-widget-" + name);

                    maskEle.appendTo($(renderTo));

                    var loadingContainer = $("<div  class=\"mask-loading-container\" ></div>");

                    loadingContainer.attr("id", "mask-loading-" + name);

                    loadingContainer.html(text);
                    loadingContainer.appendTo($(renderTo));


                };

                jqueryMask.prototype.hide = function () {
                    $("#" + "mask-widget-" + name).hide();
                    $("#" + "mask-loading-" + name).hide();
                };

                mask = new jqueryMask();
                window.JqueryMasks[renderTo][name] = mask;

            } else {
                mask = window.JqueryMasks[renderTo][name];
            }

            return mask;
        }
    });
})(jQuery)

/*
                jQuery.Mask.default = {
        renderTo: $(document.body),
    enable: true,
    showCssClassName: 'mask-widget-overlay mask-front'
};

$.Mask.default = (typeof ps.renderTo == 'string' ?
     $(ps.renderTo) : ps.renderTo);
$.extend({}, $.Mask.default, setting);

$.Mask.default.show = function (text) {
    $("<div class=\"mask-widget-overlay mask-front\"></div>").appendTo($.Mask.default.renderTo);
};

$.Mask.default.hide = function () {
    $.Mask.default.renderTo.remove($.Mask.default.showCssClassName);
};
return $.Mask.default;
}








*/