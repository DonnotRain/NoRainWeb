
$(function () {
    easyloader.locale = "zh_CN";
    easyloader.theme = "bootstrap";
    easyloader.load(['datagrid', 'combobox', 'form', 'dialog', 'messager', 'datagrid', 'combotree'], InitialFunc);
});

//初始化
function InitialFunc() {
    //  $('#fm').attr(
    Initialize.initialDialog();
    Initialize.loadDataGrid();
    Initialize.initCombos();
    Initialize.initialValidate();
    Initialize.initialBtns();
    $("#div-content-wrap").show();
}

window.rootPath = getRootPath();
var currentUrl = window.rootPath + "/API/Operator/";

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
            var result = text.substring(start, text.indexOf("("));

            $("#iconSample").attr("class", result);
            $("#ImageIndex").val(result);
            $('#iconChoose').dialog('close');
        });
    },
    loadDataGrid: function () {
        $("#dgMain").datagrid({
            url: rootPath + "/API/OperatorPager",
            method: "get",
            pagination: true,
            rownumbers: true,
            fitColumns: true,
            nowrap: false,
            singleSelect: false,
            animate: true,
            columns: [[
            { field: 'ck', checkbox: true },
              { field: 'Name', title: '名称', width: 100, align: 'left' },
               { field: 'Sort', title: '排序', width: 30, align: 'center' },
            { field: 'OperatorCode', title: '编号', width: 100, align: 'center' },
            { field: 'OperatorUrl', title: '模块Url', width: 100, align: 'center' }
            ]],
            onDblClickRow: function (rowData) {
                $(this).datagrid("unselectAll");
                $(this).datagrid("select", rowData.Id);
                Edit();
            },
            onContextMenu: function (e, row) {
                e.preventDefault();
                $(this).datagrid('unselectAll');
                $(this).datagrid('select', row.Id);
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
            title: "新增操作员",
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

        $('#iconChoose').show();
        $("#iconChoose").dialog({
            title: "选择图标",
            modal: true,
            closed: true,
            buttons: [{
                text: '保存',
                handler: function () {
                    saveIcon();
                }
            }, {
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
    },
    initialValidate: function () {
        $("#fm").validate({
            rules: {
                OperatorName: "required",
                OperatorCode: {
                    required: true
                }
            },
            messages: {
                OperatorName: "请输入插件名称",
                OperatorCode: {
                    required: "编号是必须的"
                },
                //invalidHandler:function (e, validator) {
                //    var errors = validator.numberOfInvalids();
                //    if (errors) {
                //        var message = errors == 1
                //            ? 'You missed 1 field. It has been highlighted below'
                //            : 'You missed ' + errors + ' fields.  They have been highlighted below';
                //        $("div.error span").html(message);
                //        $("div.error").show();
                //    } else {
                //        $("div.error").hide();
                //    }
                //},
                invalidHandler: function (errorMap, errorList) {
                    alert("Your form contains "
                       + this.numberOfInvalids()
                       + " errors, see details below.");
                    this.defaultShowErrors();
                },
                errorPlacement: function (error, element) {
                    alert(error);
                    error.appendTo(element.parent("td").next("td"));
                }
            }
        });
    }
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

function chooseIcon() {
    $('#iconChoose').dialog('open');
}

//添加操作员
function Add() {
    $('#fm').form('clear');
    $('#dlgMain').dialog('open').dialog('setTitle', '添加操作员');
    $("#ProjectCrisisStepSetId").val(0);


    var items = $("#dgMain").datagrid('getChecked');

    if (items && items.length > 0) {
        var item = items[0];
        $("#ParentId").combotree("setValue", item.Id);
    }

    $("#IsEnabledCheck").prop("checked", true);
    $("#AllowEditCheck").prop("checked", true);
    $("#AllowDeleteCheck").prop("checked", true);

    $("#ImageIndex").val($("#iconSample").attr("class"));

    //编号可编辑
    $("#OperatorCode").prop("readonly", false);
}

function Edit() {
    $('#fm').form('clear');

    var items = $("#dgMain").datagrid('getSelections');

    if (items && items.length > 0) {
        var item = items[0];

        for (var filed in item) {
            $("#" + filed).val(item[filed]);
        }

        //排序 
        $("#Sort").numberbox("setValue", item.Sort);

        $("#iconSample").attr("class", item.ImageIndex);

        //几个按钮
        $("#IsEnabledCheck").prop("checked", item.IsEnabled);
        $("#AllowEditCheck").prop("checked", item.IsCanEdit);
        $("#AllowDeleteCheck").prop("checked", item.IsCanDelete);
        //上级模块
        $("#ParentId").combotree("setValue", item.ParentId);

        //编号不可编辑
        $("#OperatorCode").prop("readonly", true);

        $('#dlgMain').dialog('open').dialog('setTitle', '修改操作员');
    }
    else {
        $.messager.alert("未选取数据行", "请先选取要编辑的行", 'info');
        return;
    }
}

function Delete() {
    var rows = $("#dgMain").datagrid('getChecked');
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
            });
        }
    });

}

function save() {
    var type = "post";
    if ($("#Id").val()) {
        type = "put";
    }

    //验证输入合法性
    var isValid = $("#fm").valid();
    if (!isValid) {
        return;
    }

    $("#IsCanEdit").val($("#AllowEditCheck").prop("checked"));
    $("#IsEnabled").val($("#IsEnabledCheck").prop("checked"));
    $("#IsCanDelete").val($("#AllowDeleteCheck").prop("checked"));

    var data = $("#fm").serialize();
    $.CommonAjax({
        url: currentUrl,
        type: type,
        data: data,
        success: function (data, textStatus) {
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

function saveIcon() {

}
//重新加载dataGrid
function reloadDataGrid() {

    var searchTxt = $("#inputSearchTxt").val();
    if (searchTxt === $("#inputSearchTxt").attr("mark")) {
        searchTxt = "";
    }
    var queryParams =
        {
            key: searchTxt
        };
    $("#dgMain").datagrid('reload');
}

// 重新布局
function resize() {
    var width = $(".dg-panel").width();
    var height = $(window).height() - 110;
    $("#dgMain").datagrid('resize', { // 重新布局DataGrid
        width: width,
    });
}

