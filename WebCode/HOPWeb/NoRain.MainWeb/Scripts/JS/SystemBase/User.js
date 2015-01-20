PlugFunctions.SystemBase.User = function () {

    var currentUrl = rootPath + "/API/User/";
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
                { field: 'Code', title: '编号', width: 100, align: 'center' },
                {
                    field: 'UserName', title: '名称', width: 100, align: 'center'
                },
                 {
                     field: 'StoreName', title: '所属门店', width: 100, align: 'center'
                 },
                {
                    field: 'YearAge', title: '年资', width: 40, align: 'center'
                },
                 {
                     field: 'Mobile', title: '联系电话', width: 100, align: 'center'
                 },
                { field: 'Address', title: '家庭住址', width: 120, align: 'left' }
                ]]
                , onBeforeLoad: function (para) {
                    //int? begin, int? end, string name, string code, string storeName
                    para.begin = $("#conditionBegin").val();
                    para.end = $("#conditionEnd").val();
                    para.Name = $("#conditionName").val();
                    para.Code = $("#conditionCode").val() || "";
                    para.StoreName = $("#conditionStoreName").val();
                }
            });
        },
        initialDialog: function () {
            $('#dlgMain').show();
            $("#dlgMain").dialog({
                title: "新增公告",
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
        if (type == "post" && !$("#Password").val()) {
            $.Show({ message: "密码未输入", type: "error" });
            return;
        }

        if (!$("#StoreId").combobox("getValue")) {
            $.Show({ message: "门店未选择", type: "error" });
            return;
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
            $('#dlgMain').dialog('open').dialog('setTitle', '添加外勤人员');
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
                //特殊的输入框 dateFormatter
                $("#CreateTime").val(dateFormatter(item.CreateTime));
                $("#Password").val("");

                $("#StoreId").combobox("setValue", item.StoreId);
                $("#spanFileName").html((item.Files && item.Files.length) ? item.Files[0].FileName : "");
                $('#dlgMain').dialog('open').dialog('setTitle', '修改外勤人员');
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



