PlugFunctions.System.Parameter = function () {

    var currentUrl = rootPath + "/API/Parameter/";
    //全局变量声明
    var vars = {
        totalTimeout: 120 * 1000
    };
    //初始化
    var Initialize = {
        loadDataGrid: function () {
            $("#dgMain").datagrid({
                url: currentUrl,
                method: "get",
                pagination: true,
                fitColumns: true,
                nowrap: false,
                checkOnSelect: false,
                selectOnCheck: false,
                singleSelect: true,
                idField: "ID",
                columns: [[
                        { field: 'ck', checkbox: true },
                {
                    field: 'Name', title: '参数名称', width: 100, align: 'center'
                },
                 {
                     field: 'Value', title: '参数值', width: 100, align: 'center'
                 }
                ]]
                , onBeforeLoad: function (para) {
                    para.Name = $("#conditionName").val() || "";
                    para.Value = $("#conditionValue").val() || "";
                }
            });
        },
        initialDialog: function () {
            $('#dlgMain').show();
            $("#dlgMain").dialog({
                title: "新增系统参数",
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
        },
        initialValidate: function () {
            $("#fm").validVal({ language: "cn" });
        },
        initForm: function () {
            $("#StoreId").combobox({
                url: rootPath + "/API/Store",
                valueField: 'Id',
                textField: 'StoreName',
                method: "get",
                editable: false
            });
        }
    }
    //初始化
    function InitialFunc() {
        Initialize.initialDialog();
        Initialize.loadDataGrid();
        Initialize.initialValidate();
        Initialize.initForm();

        window.onresize = resize;
        resize();
    }

    //重新加载dataGrid
    function reloadDataGrid() {
        $("#dgMain").datagrid('reload');
    }

    // 重新布局
    function resize(event) {
        if (event) {
            event.preventDefault();
        }
        var width = $(".page-content").width() - 10;
        var height = $(window).height() - 130;
        $("#dgMain").datagrid('resize', { // 重新布局DataGrid
            width: width
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

        //上传文件
        try {
            form_data = $("#fm").serializeArray();

            $.jMask("save", "保存中，请稍后").show();
            $.CommonAjax({
                url: currentUrl,
                type: type,
                data: form_data,
                success: function (data, textStatus) {
                    $('#dlgMain').dialog('close');
                    $.jMask("save").hide();
                    $.Show({
                        message: "保存成功",
                        type: "success",
                        hideAfter: 3
                    });
                    reloadDataGrid();
                }
            });
        }
        catch (e) {
            $.jMask("HideAll");
            alert(e.message);
        }
    }
    return {
        init: function () {
            easyloader.locale = "zh_CN";
            easyloader.theme = "metro";
            easyloader.load(['parser', 'validatebox', 'datagrid', 'combobox', 'form', 'dialog', 'messager'], InitialFunc);
        }
        , reloadDataGrid: reloadDataGrid
        , add: function () {
            $('#fm').form('clear');
            $("#TargetZone").val("1");
            $("#TargetType").val("1");
            $('#dlgMain').dialog('open').dialog('setTitle', '添加系统参数');
        }
        , edit: function () {
            $('#fm').form('clear');
            var items = $("#dgMain").datagrid('getChecked');

            if (items && items.length > 0) {
                var item = items[0];

                $("#dgMain").datagrid('unselectAll');
                $("#dgMain").datagrid('selectRecord', item.ID);

                $('#fm').form('load', item);
                for (var filed in item) {
                    $("#" + filed).val(item[filed]);
                }

                $('#dlgMain').dialog('open').dialog('setTitle', '修改系统参数');
            }
            else {
                $.Show({ message: "未勾选数据行", type: "error" });
                return;
            }
        },
        deleteItems: function () {
            var rows = $("#dgMain").datagrid('getChecked');
            if (!rows.length) {
                $.Show({ message: "未勾选数据行", type: "error" });
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
                        url: currentUrl,
                        type: "delete",
                        data: { "": ids.join(",") },
                        success: function (data, textStatus) {
                            $.jMask("delete").hide();
                            $.Show({ message: "删除成功", type: "success" });
                            reloadDataGrid();
                        }
                    });
                }
            });
        }
    };
}();



