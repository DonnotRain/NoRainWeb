
$(function () {
    easyloader.locale = "zh_CN";
    easyloader.theme = "bootstrap";
    easyloader.load(['datagrid', 'combobox', 'form', 'dialog', 'messager'], InitialFunc);
});

//初始化
window.Initialize = {
    initialBtns: function () {
    },
    loadDataGrid: function () {
        $("#dgMain").datagrid({
            url: rootPath + "/API/ProductUnitPager",
            method: "get",
            pagination: true,
            fitColumns: true,
            nowrap: false,
            checkOnSelect: false,
            selectOnCheck: false,
            singleSelect: true,
            columns: [[
            { field: 'ck', checkbox: true },
            { field: 'Code', title: '编号', width: 100, align: 'center' },
            { field: 'Name', title: '名称', width: 100, align: 'center' }
            ]]
        });
        $(window).resize(resize);
        resize();
    },
    initialDialog: function () {
        $('#dlgMain').show();
        $("#dlgMain").dialog({
            title: "新增产品单位",
            modal: true,
            closed: true,
            buttons: [{
                text: '保存',
                handler: function () {
                    save();
                }
            }, {
                text: '取消',
                handler: function () {
                    $('#dlgMain').dialog('close');
                }
            }]
        });

    },
    initCombos: function () {
        //$("#CrisisHandleStatusName").combobox({
        //    valueField: 'ConfigurationItemId',
        //    textField: 'Name',
        //    url: vars.eventUrl,
        //    editable: false,
        //    onSelect: function (record) {
        //        $("#CrisisHandleStatus").val(record.ConfigurationItemId);
        //    }
        //});
    }
}
window.rootPath = getRootPath();
var currentUrl = window.rootPath + "/API/ProductUnit/";

//全局变量声明
var vars = {
    totalTimeout: 120 * 1000,
    eventUrl: rootPath.concat("/ConfigurationItem/GetConfigurationItems?configurationCategoryCode=", "EmergencyEventStatus")
};

//初始化
function InitialFunc() {
    //  $('#fm').attr(
    Initialize.initialDialog();
    Initialize.loadDataGrid();
    Initialize.initCombos();

    $("#div-content-wrap").show();
}

// 列表check事件
function onDgRowCheck() {
    if ($("#dgMain").datagrid("getChecked").length > 0) {
        $("#btnModify").removeClass("disabled");
        $("#btnDel").removeClass("disabled");
    } else {
        $("#btnModify").addClass("disabled");
        $("#btnDel").addClass("disabled");
    }
}

function employeeFormtter(value, record) {
    var item = getItem(zNodes, 'id', value);
    if (item) {
        return item.name;
    }
    return "(无相关数据)";
}

//添加产品重量单位
function Add() {
    $('#fm').form('clear');
    $('#dlgMain').dialog('open').dialog('setTitle', '添加产品重量单位');
    $("#ProjectCrisisStepSetId").val(0);
}

function Edit() {
    $('#fm').form('clear');

    var items = $("#dgMain").datagrid('getChecked');

    if (items && items.length > 0) {
        var item = items[0];

        for (var filed in item) {
            $("#" + filed).val(item[filed]);
        }

        $('#dlgMain').dialog('open').dialog('setTitle', '修改产品重量单位');
    }
    else {
        $.Show({ message: "未选取数据行，请先选取要操作的行" });
        //$.messager.alert("未选取数据行", "请先选取要编辑的行", 'info');
        return;
    }
}

function Delete() {
    var rows = $("#dgMain").datagrid('getChecked')
    if (!rows.length) {
        //   $.messager.alert("未选取数据行", "请先选取要删除的行", 'info');
        $.Show({ message: "未选取数据行，请先选取要删除的行" });
        return;
    }

    $.messager.confirm('确认', '确定要删除所选数据？', function (r) {
        if (r) {
            var ids = new Array();

            $.each(rows, function (rowIndex, rowData) {
                ids.push(rowData.Id);
            });

            $.CommonAjax({
                url: currentUrl + "DeleteSome/" + ids.join(","),
                type: "delete",
                success: function (data, textStatus) {

                    $.gritter.add({
                        // (string | mandatory) the heading of the notification
                        title: '成功!',
                        // (string | mandatory) the text inside the notification
                        text: '删除成功，正在刷新。',
                        class_name: 'gritter-success',
                        time: 3000,
                    });
                    reloadDataGrid();
                }
            })
        }
    });

}

function save() {
    var type = "post";
    if ($("#Id").val()) {
        type = "put";
    }
    var isValid = $("fm").form('validate');
    if (!isValid) {
        return;
    }

    if (!$("#Code").val() || !$("#Name").val()) {
        $.messager.alert("输入信息错误", "信息输入不完整", 'info');
        return;
    }

    var data = $("#fm").serialize();
    $.jMask("delete", "保存中，请稍后").show();
    $.CommonAjax({
        url: currentUrl,
        type: type,
        data: data,
        success: function(data, textStatus) {
            $.jMask("delete").hide();
            $('#dlgMain').dialog('close');
            $.gritter.add({
                // (string | mandatory) the heading of the notification
                title: '成功!',
                // (string | mandatory) the text inside the notification
                text: '保存成功，正在刷新。',
                class_name: 'gritter-success',
                time: 3000,
            });
            reloadDataGrid();
        }
    });
}

//重新加载dataGrid
function reloadDataGrid() {
    $("#dgMain").datagrid('reload');
}

// 重新布局
function resize() {
    var width = $(".dg-panel").width();
    var height = $(window).height() - 110;
    $("#dgMain").treegrid('resize', { // 重新布局DataGrid
        width: width
    });
}