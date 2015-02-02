PlugFunctions.Product.Index = function () {

    var rootPath = getRootPath();
    var currentUrl = rootPath + "/API/ProductUnit/";
    //全局变量声明
    var vars = {
        totalTimeout: 120 * 1000,
        eventUrl: rootPath.concat("/ConfigurationItem/GetConfigurationItems?configurationCategoryCode=", "EmergencyEventStatus")
    };
    //初始化
    var Initialize = {
        initialBtns: function () {

            //$("#btnAdd").click(Add);
            //$("#btnModify").click(Modify);
            //$("#btnDel").click(Delete);

        },
        loadDataGrid: function () {
            $("#dgMain").datagrid({
                url: rootPath + "/API/Product",
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
                title: "新增处理过程",
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
      
        },
        initialValidate: function () {
            $("#fm").validVal({language:"cn"});
        }
    }
    //初始化
    function InitialFunc() {
        //  $('#fm').attr(
        Initialize.initialDialog();
        Initialize.loadDataGrid();
        Initialize.initCombos();
        Initialize.initialValidate();
        $("#div-content-wrap").show();
    }
    function save() {

        var type = "post";
        if ($("#Id").val()) {
            type = "put";
        }

        //验证输入合法性
        var isValid = $("#fm").triggerHandler("submitForm");
        if (!isValid) {
            //  $.messager.alert("输入信息错误", "信息输入不完整", 'info');
            return;
        }

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

    //重新加载dataGrid
    function reloadDataGrid() {
        $("#dgMain").datagrid('reload');
    }

    // 重新布局
    function resize() {
        var width = $(".dg-panel").width();
        var height = $(window).height() - 110;
        $("#dgMain").datagrid('resize', { // 重新布局DataGrid
            width: width
        });
    }


    return {
        init: function () {
            easyloader.locale = "zh_CN";
            easyloader.theme = "bootstrap";
            easyloader.load(['datagrid', 'combobox', 'form', 'dialog', 'messager'], InitialFunc);
        }
        //添加产品重量单位
        , Add: function () {
            $('#fm').form('clear');
            $('#dlgMain').dialog('open').dialog('setTitle', '添加产品重量单位');
            $("#ProjectCrisisStepSetId").val(0);
        }
        , Edit: function () {
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
                $.messager.alert("未选取数据行", "请先选取要编辑的行", 'info');
                return;
            }
        }
        , Delete: function () {
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
                    });
                }
            });

        }
    };
}();



