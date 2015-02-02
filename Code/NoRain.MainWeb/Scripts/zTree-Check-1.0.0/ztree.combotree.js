// 树基本配置
var treeSetting = {
    check: {
        enable: true,
        chkStyle: "checkbox",
        radioType: "all"
    },
    view: {
        dblClickExpand: true
    },
    data: {
        simpleData: {
            enable: true
        }
    },
    callback: {
        onClick: function (e, treeId, treeNode) { // 树节点单击时
            var zTree = $.fn.zTree.getZTreeObj(treeId);
            zTree.checkNode(treeNode, !treeNode.checked, null, true);
            return false;
        },
        onCheck: function onCheck(e, treeId, treeNode) { // 树节点选择或取消选择时
            try {
                var r = zTree.setting.onBeforeCheck(e, treeId, treeNode);
                if (!r) {
                    return;
                }
            } catch (e) { }
            var zTree = $.fn.zTree.getZTreeObj(treeId),
            //nodes = zTree.getCheckedNodes(true),
            nodes = getCheckedNodes(zTree, false),
            v = "";
            for (var i = 0, l = nodes.length; i < l; i++) {
                v += nodes[i].name + ",";
            }
            if (v.length > 0) v = v.substring(0, v.length - 1);
            $("#" + zTree.setting.targetId).val(v);
            try {
                zTree.setting.onChecked(e, treeId, treeNode);
            } catch (e) { }
        },
        beforeCheck: function (treeId, treeNode) { // 树节点选择或取消选择前
            return !treeNode.isOrg;
        }
    }
};

var treeIds = [];
//初始化树
function initComboTree(options) {

    // 递归显示子节点
    function showChildren(zTree, node) {
        if (!zTree || !node) {
            return;
        }
        var children = node.children;
        if (children && children.length > 0) {
            zTree.showNodes(children);
            for (var i = 0; i < children.length; i++) {
                showChildren(zTree, children[i]);
            }
        }
    }

    try {
        if (zNodes && zNodes.length > 0) {
            for (var i = 0; i < zNodes.length; i++) {
                if (zNodes[i].isOrg) {
                    zNodes[i]["icon"] = "/Content/zTreeStyle/img/diy/group.gif";
                } else {
                    zNodes[i]["icon"] = "/Content/zTreeStyle/img/diy/people.gif";
                }
            }
        }
    } catch (ex) { }

    var target = $("#" + options.targetId);
    var treeW = options.treeWidth == "auto" ? target.width() : options.treeWidth;
    var treeHtml;

    var treeWrapHandler = $("#" + options.treeWrapId);
    if (treeWrapHandler[0]) {
        treeWrapHandler.remove();
    }
    if (options.enableSearch) {
        treeHtml = '<div id="' + options.treeWrapId + '" style="z-index:10000;background:white;display:none;position:absolute;overflow:auto;border:solid 1px #ddd;">' +
                '<div style="overflow:hidden;"><input id="' + options.treeWrapId + '_input" type="text" style="width:99%;height:22px;border-bottom:1px dotted gray"/></div><div style="height:' + (options.treeHeight - 27) + 'px;overflow:auto;"><ul id="' + options.treeId + '" class="ztree" style="margin-top:0;"></ul></div></div>'
    } else {
        treeHtml = '<div id="' + options.treeWrapId + '" style="z-index:10000;background:white;display:none;position:absolute;overflow:auto;border:solid 1px #ddd;">' +
                '<ul id="' + options.treeId + '" class="ztree" style="margin-top:0;"></ul></div>'
    }
    $("body").append(treeHtml);
    $("#" + options.treeWrapId).css({ width: treeW, height: options.treeHeight });

    $("#" + options.targetId).click(function () {
        var offset = $(this).offset();
        $("#" + options.treeWrapId).css({ left: offset.left + "px", top: offset.top + $(this).outerHeight() + "px" }).show();//.slideDown(100);
        $("body").bind("mousedown", function (e) {
            try {
                if (!(e.target.id == options.targetId || e.target.id == options.treeWrapId || $(e.target).parents("#" + options.treeWrapId).length > 0)) {
                    $("#" + options.treeWrapId).hide();//.fadeOut(100);
                    $("body").unbind("mousedown", this);
                }
            } catch (e) { }
        });
        if (options.enableSearch) {
            $("#" + options.treeWrapId + "_input").focus();
        }
    }).attr("readonly", "readonly");

    if (options.enableSearch) {
        $("#" + options.treeWrapId + "_input").attr("__tree-id", options.treeId).keyup(function (e) {
            var zTree = $.fn.zTree.getZTreeObj($(this).attr("__tree-id"));
            if (!zTree) {
                return;
            }
            var nodes = zTree.transformToArray(zTree.getNodes());
            if (!nodes || nodes.length < 1) {
                return;
            }
            var key = $(this).val();
            zTree.showNodes(nodes);
            var showedNodes = [];
            for (var i = 0; i < nodes.length; i++) { // 遍历所有节点
                if (nodes[i].name.indexOf(key) < 0) {
                    zTree.hideNode(nodes[i]);
                } else {
                    showedNodes.push(nodes[i]);
                }
            }
            var pNode = null, children = null;;
            for (var j = 0; j < showedNodes.length; j++) {
                showChildren(zTree, showedNodes[j]);// 递归显示子节点
                pNode = showedNodes[j].getParentNode();
                while (pNode) { // 递推显示父节点
                    zTree.showNode(pNode);
                    pNode = pNode.getParentNode()
                }
            }
            zTree.expandAll(true);
        });
    }

    var setting = $.extend(true, {}, treeSetting);
    setting.targetId = options.targetId;
    if (options.chkStyle == "radio") {
        setting.check.chkStyle = "radio";
    } else if (options.chkStyle == "checkbox") {
        setting.check.chkStyle = "checkbox";
    }
    if (options.onChecked) {
        setting.onChecked = options.onChecked;
    }

    if (options.onBeforeCheck) {
        setting.onBeforeCheck = options.onBeforeCheck;
    }
    if (options.check) {
        $.extend(setting.check, options.check)
    }

    var nodes = options.treeNodes;
    if (!nodes || nodes.length < 0) {
        try {
            nodes = zNodes;
        } catch (ex) { }
    }

    $.fn.zTree.init($("#" + options.treeId), setting, nodes);
    treeIds.push(options.treeId);
};

//获取值
function getComboTreeValue(treeId) {
    var zTree = $.fn.zTree.getZTreeObj(treeId);
    if (!zTree) {
        return [];
    }
    //nodes = zTree.getCheckedNodes(true);
    var nodes = getCheckedNodes(zTree, false);
    if (nodes == null || nodes.length < 1) {
        return [];
    }
    var values = [];
    for (var i = 0; i < nodes.length; i++) {
        values.push(nodes[i].id);
    }
    return values;
}

//获取节点对象
function getComboTreeRecords(treeId) {
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
}

// 获取checked的节点，ignoreHiddenNode：是否忽略隐藏节点
function getCheckedNodes(zTree, ignoreHiddenNode) {
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
}

//设置值
function setComboTreeValue(treeId, ids) {
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
}

// 清空值
function clearComboTreeValue(treeId) {
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
}

// 销毁树
function destroyComboTree(treeId) {
    try {
        $.fn.zTree.destroy(treeId);
        $("#" + treeId).parent().remove();
    } catch (ex) { }
}

// 销毁所有树
function destroyAllComboTree() {
    if (treeIds && treeIds.length > 0) {
        for (var i = 0; i < treeIds.length; i++) {
            destroyComboTree(treeIds[i]);
        }
    }
    treeIds = [];
}