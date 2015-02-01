/// <reference path="jquery-1.10.2.intellisense.js" />
(function ($) {
    var privateFunction = function () {
        // 代码在这里运行
    }

    var methods = {
        init: function (options, zNodes) {
            return this.each(function () {
                var $this = $(this);
                var settings = $this.data('zTreeCheck');
                var treeId = $this.attr("id") + "_ulTree";
                var containerId = $this.attr("id") + "_divContainer";

                if (typeof (settings) == 'undefined') {

                    var defaults = {
                        check: {
                            enable: true,
                            chkStyle: "radio",
                            radioType: "all"
                        },
                        view: {
                            dblClickExpand: false
                        },
                        data: {
                            simpleData: {
                                enable: true
                            }
                        }
                    }

                    settings = $.extend({}, defaults, options);

                    settings.callback = {
                        onClick: onClick,
                        onCheck: onCheck
                    };
                    settings.treeId = treeId;
                    settings.containerId = containerId;

                    $this.data('zTreeCheck', settings);
                    $this.prop("readonly", true);
                    $this.click(showMenu);
                    //根据文本框和传入参数，构建zTree树的HTML元素 

                    var divContainer = $('<div id="menuContent" class="menuContent" style="display:none;z-index:99999; position: absolute;"></div>').attr("id",
                       containerId).attr("class", containerId);

                    var ulTree = $('<ul id="treeDemo" class="ztree" style="margin-top:0; width:auto; height:auto;"></ul>').attr("id",
                       treeId).attr("class", "ztree").css("max-height", $(window).height() / 2 + "px");

                    //加入
                    divContainer.append(ulTree);
                    if ($this.parents("form").length) {
                        divContainer.appendTo($this.parents("form"));
                    }
                    else {
                        divContainer.appendTo($("body"));
                    }
                } else {
                    settings = $.extend({}, settings, options);
                }

                $.fn.zTree.init(ulTree, settings, zNodes);

                function onClick(e, treeId, treeNode) {
                    var zTree = $.fn.zTree.getZTreeObj(treeId);
                    zTree.checkNode(treeNode, !treeNode.checked, null, true);

                    //执行自定义回调
                    if (defaults.callback && defaults.callback.onClick) {
                        defaults.callback.onClick(e, treeId, treeNode);
                    }

                    return false;
                }

                function onCheck(e, treeId, treeNode) {
                    var zTree = $.fn.zTree.getZTreeObj(treeId),
                    nodes = zTree.getCheckedNodes(true),
                    v = "";
                    checkedValue = "";
                    for (var i = 0, l = nodes.length; i < l; i++) {
                        v += nodes[i].name + ",";
                        checkedValue += nodes[i].id + ",";
                    }
                    if (v.length > 0) v = v.substring(0, v.length - 1);
                    if (checkedValue.length > 0) checkedValue = checkedValue.substring(0, checkedValue.length - 1);
                    var cityObj = $this;
                    cityObj.val(v);
                    cityObj.attr("selectedValue", checkedValue);
                    //执行自定义回调
                    if (defaults.callback && defaults.callback.onCheck) {
                        defaults.callback.onCheck(e, treeId, treeNode);
                    }
                }

                function showMenu() {
                    var cityObj = $this;
                    var parentOffSet = divContainer.parent().offset();
                    var cityOffset = cityObj.offset();
                    divContainer.css({ left: cityOffset.left - parentOffSet.left + "px", top: cityOffset.top - parentOffSet.top + 15 + cityObj.outerHeight() + "px" }).slideDown("fast");

                    $("body").bind("mousedown", onBodyDown);
                }
                function hideMenu() {
                    divContainer.fadeOut("fast");
                    $("body").unbind("mousedown", onBodyDown);
                }
                function onBodyDown(event) {
                    if (!(event.target.id == "menuBtn" || event.target.id == $this.attr("id") || event.target.id == containerId || $(event.target).parents("#" + containerId).length > 0)) {
                        hideMenu();
                    }
                }
            });
        },
        //获取值
        getValue: function () {
            // return this.each(function () {
            var $this = $(this);
            var settings = $this.data('zTreeCheck');
            if (!settings) {
                return "";
            }
            var treeId = settings.treeId

            var zTree = $.fn.zTree.getZTreeObj(treeId);
            if (!zTree) {
                return "";
            }
            //nodes = zTree.getCheckedNodes(true);
            var nodes = zTree.getCheckedNodes(zTree, false);
            if (nodes == null || nodes.length < 1) {
                return "";
            }
            var values = [];
            for (var i = 0; i < nodes.length; i++) {
                values.push(nodes[i].id);
            }
            if (values.length > 0)
                return values.join(",");
            return values[0];
            //   });

        },
        //获取节点对象
        getRecords: function (treeId) {
            return this.each(function () {
                var $this = $(this);
                var settings = $this.data('zTreeCheck');
                var treeId = settings.treeId

                var zTree = $.fn.zTree.getZTreeObj(treeId);
                if (!zTree) {
                    return [];
                }
                //nodes = zTree.getCheckedNodes(true);
                var nodes = getCheckedNodes(zTree, false);
                if (nodes == null || nodes.length < 1) {
                    return [];
                }
                return nodes;
            });
        },
        // 获取checked的节点，ignoreHiddenNode：是否忽略隐藏节点
        getCheckedNodes: function (ignoreHiddenNode) {
            return this.each(function () {
                var $this = $(this);
                var settings = $this.data('zTreeCheck');
                var treeId = settings.treeId
                var zTree = $.fn.zTree.getZTreeObj(treeId);

                if (!zTree) {
                    return [];
                }
                if (ignoreHiddenNode) {
                    return zTree.getCheckedNodes(true);
                }
                var nodes = zTree.transformToArray(zTree.getNodes());
                if (!nodes || nodes.length < 1) {
                    return [];
                }
                var checkedNodes = [];
                for (var i = 0; i < nodes.length; i++) {
                    if (nodes[i].checked) {
                        checkedNodes.push(nodes[i]);
                    }
                }

                return checkedNodes;
            });
        },
        //设置值
        setValue: function (ids) {
            return this.each(function () {
                var $this = $(this);
                var settings = $this.data('zTreeCheck');
                var treeId = settings.treeId
                var zTree = $.fn.zTree.getZTreeObj(treeId);

                if (!zTree) {
                    return;
                }
                if (!ids || ids == "" || ids.length < 1) {
                    clearComboTreeValue(treeId);
                    return;
                }
                if (typeof (ids) === "string") {
                    ids = ids.split(',');
                }
                if (ids.length == 0) {
                    clearComboTreeValue(treeId);
                    return;
                }
                for (var i = 0; i < ids.length; i++) {
                    try {
                        zTree.checkNode(zTree.getNodeByParam("id", ids[i], null), true, true);
                    } catch (e) { }
                }

                var nodes = zTree.getCheckedNodes(true),
                        v = "";
                for (var i = 0, l = nodes.length; i < l; i++) {
                    v += nodes[i].name + ",";
                }
                if (v.length > 0) v = v.substring(0, v.length - 1);
                $("#" + zTree.setting.targetId).val(v);
            });
        },
        // 清空值
        clear: function () {
            return this.each(function () {
                var $this = $(this);
                var settings = $this.data('zTreeCheck');
                var treeId = settings.treeId
                var zTree = $.fn.zTree.getZTreeObj(treeId);

                if (!zTree) {
                    return;
                }
                $("#" + zTree.setting.targetId).val("");
                if (zTree.setting.check.chkStyle == "checkbox") {
                    zTree.checkAllNodes(false);
                } else {
                    var nodes = getCheckedNodes(zTree, false);
                    if (nodes && nodes.length > 0) {
                        zTree.checkNode(nodes[0], false, true);
                    }
                }
            });
        },
        // 销毁树
        destroy: function () {
            return this.each(function () {
                var $this = $(this);
                var settings = $this.data('zTreeCheck');
                var treeId = settings.treeId

                try {
                    $.fn.zTree.destroy(treeId);
                    $("#" + treeId).parent().remove();
                } catch (ex) { }
            });
        },
        // 销毁所有树
        destroyAll: function () {
            return this.each(function () {
                var $this = $(this);
                var settings = $this.data('zTreeCheck');
                var treeId = settings.treeId

                if (treeIds && treeIds.length > 0) {
                    for (var i = 0; i < treeIds.length; i++) {
                        destroyComboTree(treeIds[i]);
                    }
                }
                treeIds = [];

            });
        }
    }

    $.fn.zTreeCheck = function () {
        var method = arguments[0];

        if (methods[method]) {
            method = methods[method];
            arguments = Array.prototype.slice.call(arguments, 1);
        } else if (typeof (method) == 'object' || !method) {
            method = methods.init;
        } else {
            $.error('Method ' + method + ' does not exist on jQuery.zTreeCheck');
            return this;
        }

        return method.apply(this, arguments);
    }

})(jQuery);