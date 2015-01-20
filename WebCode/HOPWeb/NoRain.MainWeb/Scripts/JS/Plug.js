
$(function () {
    easyloader.locale = "zh_CN";
    easyloader.theme = "metro";
    easyloader.load(['parser', 'validatebox', 'treegrid', 'combobox', 'form', 'dialog', 'messager','combotree'], InitialFunc);
});

function InitialFunc() {
    Initialize.initialDialog();
    Initialize.loadDataGrid();
    Initialize.initCombos();
    Initialize.initialValidate();
    Initialize.initialBtns();
}

window.rootPath = rootPath;
var currentUrl = window.rootPath + "/API/Function/";

//全局变量声明
var vars = {
    totalTimeout: 120 * 1000,
    eventUrl: rootPath.concat("/ConfigurationItem/GetConfigurationItems?configurationCategoryCode=", "EmergencyEventStatus")
};

//初始化
window.Initialize = {
    initialBtns: function () {
        $(".span4").click(function (ele) {
            var text = $(this).text();
            var start = text.indexOf("icon");
            var result = text.substring(start).replace(/(^\s+)|(\s+$)/g, "");

            $("#iconSample").attr("class", result);
            $("#ImageIndex").val(result.replace(" ", ""));
            $('#iconChoose').dialog('close');
        });
    },
    loadDataGrid: function () {
        $("#dgMain").treegrid({
            url: currentUrl,
            method: "get",
            pagination: false,
            rownumbers: true,
            fitColumns: true,
            nowrap: false,
            singleSelect: false,
            idField: 'ID',
            animate: false,
            treeField: 'Name',
            frozenColumns: [[
            { field: 'ck', checkbox: true },
              { field: 'Name', title: '名称', width: 200, align: 'left' }]],
            columns: [[
              { field: 'ControlID', title: '编号', width: 80, align: 'left' },
       {
           field: 'FunctionType', title: '功能类型', width: 50, align: 'center', formatter: function (value, row, index) {

               switch (value) {
                   case 1:
                       return "菜单项";
                       break;
                   case 2:
                       return "系统模块";
                       break;
                   case 3:
                       return "页面功能";
                       break;
               }

           }
       },
                   { field: 'Sort', title: '排序', width: 30, align: 'center' },
                    { field: 'Path', title: '模块Url', width: 100, align: 'left' },
                       {
                           field: 'Roles', title: '授权角色', width: 120, align: 'center', formatter: function (value, row, index) {
                               var roles = [];

                               for (var i = 0; i < row.Roles.length; i++) {
                                   roles.push(row.Roles[i].Name);
                               }
                               return roles.join(",");
                           }
                       },
            ]],
            loadFilter: function (items) {
                // var srcItems = treegridFilter(items, "-1", "children", "ID", "PID");
                try {
                    $("#PID").combotree({
                        width: $("#PID").width(),
                        data: combotreeFilter(items),
                        editable: false,
                        onSelect: function (record) {

                        }
                    });
                }
                catch (e) {
                    alert(e.message);
                }
                return items;
            },
            onDblClickRow: function (rowData) {
                $(this).treegrid("unselectAll");
                $(this).treegrid("select", rowData.ID);
                Edit();
            },
            onContextMenu: function (e, row) {
                e.preventDefault();
                $(this).treegrid('unselectAll');
                $(this).treegrid('select', row.ID);
                $('#mm').menu('show', {
                    left: e.pageX,
                    top: e.pageY
                });
            }
        });
        $(window).resize(resize);
        resize();
    },
    initialDialog: function () {
        $('#dlgMain').show();
        $("#dlgMain").dialog({
            title: "新增系统模块",
            modal: true,
            closed: true,
            buttons: [{
                iconCls: "icon-save",
                text: '保存',
                handler: function () {
                    save();
                }
            }, {
                iconCls: "icon-remove",
                text: '取消',
                handler: function () {
                    $('#dlgMain').dialog('close');
                }
            }]
        });

        $('#iconChoose').show();
        $("#iconChoose").dialog({
            title: "选择图标",
            modal: true,
            closed: true,
            buttons: [{
                iconCls: "icon-save",
                text: '保存',
                handler: function () {
                    saveIcon();
                }
            }, {
                iconCls: "icon-remove",
                text: '取消',
                handler: function () {
                    $('#iconChoose').dialog('close');
                }
            }]
        });

    },
    initCombos: function () {
        $("#CrisisHandleStatusName").combobox({
            valueField: 'ConfigurationItemId',
            textField: 'Name',
            url: vars.eventUrl,
            editable: false,
            onSelect: function (record) {
                $("#CrisisHandleStatus").val(record.ConfigurationItemId);
            }
        });

        $("#RoleIds").combobox({
            valueField: 'ID',
            textField: 'Name',
            multiple: true,
            method: "get",
            url: window.rootPath + "/API/Role/",
            editable: false,
            onSelect: function (record) {
                //  $("#CrisisHandleStatus").val(record.ConfigurationItemId);
            }
        });

    },
    initialValidate: function () {
        $("#fm").validVal();
    }
}

function combotreeFilter(source) {

    var idField = "ID";
    var textField = "Name";
    var childrenField = "children";

    var filterFunc = function (items) {
        for (var i = 0; i < items.length; i++) {
            items[i].text = items[i]["Name"];
            items[i].id = items[i]["ID"];
            if (items[i].children && items[i].children.length) {
                filterFunc(items[i].children);
            }
        }
    }

    filterFunc(source);

    var rootItem = {};

    rootItem = deepcopy(source[0]);

    rootItem[childrenField] = source;
    rootItem[idField] = "-1";
    rootItem[textField] = "根节点";
    rootItem.iconCls = "none icon-th-large   " + " icon-fixed-width";
    rootItem.text = "根节点";
    rootItem.id = "-1";

    return [rootItem];
}


function chooseIcon() {
    $('#iconChoose').dialog('open');
}

//添加系统模块
function Add() {
    $('#fm').form('clear');
    $('#dlgMain').dialog('open').dialog('setTitle', '添加系统模块');
    $("#ProjectCrisisStepSetId").val(0);

    var items = $("#dgMain").treegrid('getChecked');

    if (items && items.length > 0) {
        var item = items[0];
        $("#PID").combotree("setValue", item.ID);
    }

    $("#IsMenu").prop("checked", true);
    $("#IsModule").prop("checked", false);
    $("#IsFunction").prop("checked", false);

    $("#ImageIndex").val("");

    //编号可编辑
    $("#ControlID").prop("readonly", false);
    //主动赋值ID
    $("#ID").val(0);
}

function Edit() {
    $('#fm').form('clear');

    var items = $("#dgMain").treegrid('getSelections');

    if (items && items.length > 0) {
        var item = items[0];

        for (var filed in item) {
            $("#" + filed).val(item[filed]);
        }

        //排序 
        $("#Sort").numberbox("setValue", item.Sort);

        $("#iconSample").attr("class", item.ImageIndex);

        //几个按钮
        $("#IsMenu").prop("checked", (item.FunctionType == 1));
        $("#IsModule").prop("checked", (item.FunctionType == 2));
        $("#IsFunction").prop("checked", (item.FunctionType == 3));
        //上级模块
        $("#PID").combotree("setValue", item.PID);

        //角色
        var roleIds = [];
        for (var i = 0; i < item.Roles.length; i++) {
            roleIds.push(item.Roles[i].ID);
        }
        $("#RoleIds").combobox("setValues", roleIds);


        //编号不可编辑
        $("#ControlID").prop("readonly", true);

        $('#dlgMain').dialog('open').dialog('setTitle', '修改系统模块');
    }
    else {
        $.messager.alert("未选取数据行", "请先选取要编辑的行", 'info');
        return;
    }
}

function Delete() {
    var rows = $("#dgMain").treegrid('getSelections');
    if (!rows.length) {
        $.messager.alert("未选取数据行", "请先选取要删除的行", 'info');
        return;
    }
    $.messager.confirm('确认', '确定要删除所选数据？', function (r) {
        if (r) {
            var ids = new Array();

            $.each(rows, function (rowIndex, rowData) {
                ids.push(rowData.ID);
            });
            $.jMask("delete", "删除中，请稍后").show();
            $.CommonAjax({
                url: currentUrl + "DeleteSome/" + ids.join(","),
                type: "delete",
                success: function (data, textStatus) {
                    $.jMask("delete").hide();
                    $.gritter.add({
                        // (string | mandatory) the heading of the notification
                        title: '成功!',
                        // (string | mandatory) the text inside the notification
                        text: '删除成功。',
                        class_name: 'gritter-success',
                        time: 3000
                    });
                    reloadDataGrid();
                }
            });
        }
    });
}

function save() {
    var type = "post";
    if ($("#ID").val() && $("#ID").val() != 0) {
        type = "put";
    }

    //验证输入合法性
    var form_data = $("#fm").triggerHandler("submitForm");
    if (!form_data) {
        return;
    }
    if (!$("input[name='PID']").val()) {
        $.messager.alert("未选择父节点", "请先选取父节点", 'info');
        return;
    }

    var data = $("#fm").serializeArray();

    $.jMask("save", "保存中，请稍后").show();
    $.CommonAjax({
        url: currentUrl,
        type: type,
        data: data,
        success: function (data, textStatus) {
            $('#dlgMain').dialog('close');
            $.jMask("save").hide();
            $.gritter.add({
                // (string | mandatory) the heading of the notification
                title: '成功!',
                // (string | mandatory) the text inside the notification
                text: '保存成功。',
                class_name: 'gritter-success',
                time: 3000
            });
            reloadDataGrid();
        }
    });
}

function saveIcon() {

}

//重新加载dataGrid
function reloadDataGrid() {
    $("#dgMain").treegrid('reload');
    $("#dgMain").treegrid('unselectAll');
}

// 重新布局
function resize() {
    var width = $(".dg-panel").width();
    var height = $(window).height() - 70;
    $("#dgMain").treegrid('resize', { // 重新布局DataGrid
        width: width
    });
}
