PlugFunctions.System.Category = function () {
    var rootPath = getRootPath();
    var components = ['parser', 'validatebox', 'datagrid', 'combobox', 'form', 'dialog', 'messager', 'layout', "treegrid", "combotree"];
    //初始化
    function initialFunc() {
        initLayout();
        categoryType.init();
        categoryItem.init();
    }

    function initLayout() {
        var width = $(".dg-panel").width();
        var height = $(window).height() - 140;
        $(".easyui_layout").width(width);
        $(".easyui_layout").height(height);
        $(".easyui_layout").show();
        $(".easyui_layout").layout({
            fit: true,
            width: width,
            height: height
        });
    }

    // 重新布局
    function resize() {
        var width = $(".dg-panel").width();
        var height = $(window).height() - 140;
        $(".easyui_layout").width(width);
        $(".easyui_layout").height(height);
        $(".easyui_layout").layout('resize');
    }

    var categoryType = function () {
        var currentUrl = rootPath + "/API/Category/";

        function initMainDg() {
            $('#dgBigClass').datagrid({
                method: "get",
                url: currentUrl,
                rownumbers: true,
                singleSelect: true,
                pagination: true,
                striped: true,
                fit: true,
                toolbar: "#bigClassTool",
                fitColumns: true,
                frozenColumns: [
                    [
                        { field: 'ck', checkbox: true },
                         { field: 'Code', title: '编号', width: 140, align: 'center' },
                        { field: 'Name', title: '名称', width: 140, align: 'center' }
                    ]
                ],
                columns: [
                    [
                        { field: 'Sort', title: '排序', width: 100, align: 'center' }
                    ]
                ],
                onSelect: function (rowIndex, rowData) {
                    $('#dgSmallClass').treegrid("load");
                }
                , onBeforeLoad: function (para) {
                    para.name = "";
                }
            });

            //var p = $('#dgBigClass').datagrid('getPager');
            //$(p).pagination({
            //    showPageList: false,
            //    showRefresh: false,
            //    displayMsg: ''
            //});
        };

        function initialDialog() {
            $('#dlgBigClass').show();
            $("#dlgBigClass").dialog({
                title: "新增分类类型",
                modal: true,
                closed: true,
                buttons: [
                    {
                        text: '保存',
                        handler: function () {
                            save();
                        }
                    }, {
                        text: '取消',
                        handler: function () {
                            $('#dlgBigClass').dialog('close');
                        }
                    }
                ]
            });
        };

        function initialValidate() {
            $("#fmBigClass").validVal({ language: "cn" });
        }

        function save() {

            //验证输入合法性
            var isValid = $("#fmBigClass").triggerHandler("submitForm");
            if (!isValid) {
                //  $.messager.alert("输入信息错误", "信息输入不完整", 'info');
                return;
            }
            var method = "post";
            if ($("#CategoryId").val()) {
                method = "put";
            }
            var data = $("#fmBigClass").serializeArray();

            $.jMask("save", "保存中，请稍后").show();
            $.CommonAjax({
                url: currentUrl,
                type: method,
                data: data,
                success: function (backdata, textStatus) {
                    $.jMask("save").hide();

                    $('#dlgBigClass').dialog('close');
                    $.gritter.add({
                        // (string | mandatory) the heading of the notification
                        title: '成功!',
                        // (string | mandatory) the text inside the notification
                        text: '保存成功。',
                        class_name: 'gritter-success',
                        time: 3000
                    });
                    $("#dgBigClass").datagrid("reload");

                }
            });
        }

        return {
            init: function () {
                initLayout();
                initMainDg();
                initialDialog();
                initialValidate();
                $(window).resize(resize);
            },
            Add: function () {
                $('#fmBigClass').form('clear');
                $('#dlgBigClass').dialog('open').dialog('setTitle', '添加分类类型');

            },
            Edit: function () {

                var item = $("#dgBigClass").datagrid("getSelected");
                if (item) {
                    var rowIndex = $("#dgBigClass").datagrid("getRowIndex", item);
                    $("#dgBigClass").datagrid("clearSelections").datagrid("selectRow", rowIndex);
                } else {
                    $.messager.alert("错误", "未选中任何记录", "error");
                    return;
                }

                $('#fmBigClass').form('clear');

                $("#CategoryId").val(item.Id);
                $("#Name").val(item.Name);
                $("#CategoryCode").val(item.Code);
                $("#CategorySort").numberbox("setValue", item.Sort);

                $('#dlgBigClass').dialog('open').dialog('setTitle', '编辑分类类型');
            },
            Delete: function () {

            },
            reload: function () {
                $("#dgBigClass").datagrid("reload");
            }
        };

    }();

    var categoryItem = function () {
        var currentUrl = rootPath + "/API/CategoryItem/";

        function initMainDg() {
            $('#dgSmallClass').treegrid({
                method: "get",
                url: currentUrl + "GetPager",
                rownumbers: true,
                pageList: [10, 20, 30],
                singleSelect: true,
                pagination: true,
                striped: true,
                fit: true,
                toolbar: "#smallClassTool",
                fitColumns: true,
                treeField: 'Content',
                frozenColumns: [[
                      { field: 'ck', checkbox: true },
                       { field: 'Content', title: '值', width: 140, align: 'left' },
                       { field: 'CategoryType', title: '所属分类', width: 140, align: 'center', formatter: function (value, row) { return value.Name; } },
                      { field: 'Code', title: '编号', width: 140, align: 'center' }
                ]],
                columns: [[
                    { field: 'Sort', title: '排序', width: 100, align: 'center' }
                ]],
                onBeforeLoad: function (row, param) {
                    var item = $("#dgBigClass").datagrid("getSelected");
                    param.name = "";
                    if (!item) {
                        param.categoryId = "";
                    }
                    else {
                        param.categoryId = item.Id;
                    }
                    if (row) {
                        param.ParentId = row.Id;
                    } else {
                        param.parentId = "";
                    }
                },
                loadFilter: function (data, parentId) {
                    if (parentId)
                        return data.rows;
                    return data;
                }
               , onLoadSuccess: function (row, data) {
                   var panel = $(this).datagrid("getPanel");
                   panel.find(".error-info").remove();
                   if (!data.rows || data.rows.length < 1) {
                       var emptyText = $('<div class="error-info" style="width:100%;color:red;position: relative;text-align: center;height: 30px;vertical-align: middle;padding-top: 4px;font-size: 14px;"></div>')
                               .html("暂无或无法获取信息");
                       emptyText.insertAfter(panel.find(".datagrid-view"));
                   }
               },
                onLoadError: function () {
                    var panel = $(this).datagrid("getPanel");
                    panel.find(".error-info").remove();

                    var emptyText = $('<div class="error-info" style="width:100%;color:red;position: relative;text-align: center;height: 30px;vertical-align: middle;padding-top: 4px;font-size: 14px;"></div>')
                            .html("获取数据出错");
                    emptyText.insertAfter(panel.find(".datagrid-view"));

                }
            });

            //var p = $('#dgSmallClass').datagrid('getPager');
            //$(p).pagination({
            //    showPageList: false,
            //    showRefresh: false,
            //    displayMsg: ''
            //});
        };

        function initLayout() {
            var width = $(".dg-panel").width();
            var height = $(window).height() - 140;
            $(".easyui_layout").width(width);
            $(".easyui_layout").height(height);
            $(".easyui_layout").show();
            $(".easyui_layout").layout({
                width: width,
                height: height
            });
        }

        function initialDialog() {
            $('#dlgSmallClass').show();
            $("#dlgSmallClass").dialog({
                title: "新增分类项",
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
                        $('#dlgSmallClass').dialog('close');
                    }
                }]
            });
        };

        function initParentCombo() {

            $.getJSON(rootPath + "/API/CategoryItem/", { categoryId: $("#dgBigClass").datagrid("getSelected").Id }, function (json) {
                $("#ParentId").combotree({
                    idField: 'id',
                    treeField: 'text',
                    data: json,
                    editable: false,
                    width: $("#ParentId").width(),
                    onLoadSuccess: function () {
                        if ($("#dgBigClass").datagrid("getSelected") && $("#dgBigClass").datagrid("getSelected").Id) {
                            $("#ParentId").combobox("setValue", $("#dgBigClass").datagrid("getSelected").Id);
                        }
                    }
                });
            });
        }

        function initialValidate() {
            $("#fmSmallClass").validVal({ language: "cn" });
        }

        function save() {


            //验证输入合法性
            var isValid = $("#fmSmallClass").triggerHandler("submitForm");
            if (!isValid) {
                //  $.messager.alert("输入信息错误", "信息输入不完整", 'info');
                return;
            }
            var method = "post";
            if ($("#ItemId").val()) {
                method = "put";
            }
            var data = $("#fmSmallClass").serializeArray();

            $.jMask("save", "保存中，请稍后").show();
            $.CommonAjax({
                url: currentUrl,
                type: method,
                data: data,
                success: function (data, textStatus) {
                    $.jMask("save").hide();

                    $('#dlgSmallClass').dialog('close');
                    $.gritter.add({
                        // (string | mandatory) the heading of the notification
                        title: '成功!',
                        // (string | mandatory) the text inside the notification
                        text: '保存成功。',
                        class_name: 'gritter-success',
                        time: 3000
                    });
                    $("#dgSmallClass").treegrid("reload");

                }
            });

        }
        return {
            init: function () {
                initMainDg();
                initialDialog();
                initLayout();
                initialValidate();
                //initParentCombo();
                // $(window).resize(resize);
            },
            Add: function () {
                $('#fmSmallClass').form('clear');
                initParentCombo();
                var item = $("#dgBigClass").datagrid("getSelected");
                if (item) {
                    var rowIndex = $("#dgBigClass").datagrid("getRowIndex", item);
                    $("#dgBigClass").datagrid("clearSelections").datagrid("selectRow", rowIndex);
                }
                else {
                    $.messager.alert("错误", "未选中大类", "error");
                    return;
                }

                $("#CategoryTypeCode").val(item.Code);
                $("#CategoryTypeId").val(item.Id);
                $("#BigNameInfo").html(item.Name);

                $('#dlgSmallClass').dialog('open').dialog('setTitle', '添加分类项');

            },
            Edit: function () {
                initParentCombo();
                var item = $("#dgSmallClass").treegrid("getSelected");
                if (item) {
                    var rowIndex = $("#dgSmallClass").datagrid("getRowIndex", item);
                    $("#dgSmallClass").treegrid("clearSelections").datagrid("selectRow", rowIndex);
                }
                else {
                    $.messager.alert("错误", "未选中任何记录", "error");
                    return;
                }

                $('#fmSmallClass').form('clear');
                for (var field in item) {
                    $("#" + field).val(item[field]);
                }
                var itemType = $("#dgBigClass").datagrid("getSelected");

                $("#ItemId").val(item.Id);          

                $("#ItemCode").val(item.Code);

                $("#ItemSort").numberbox("setValue", item.Sort);

                $("#BigNameInfo").html(itemType.Name);

                $('#dlgSmallClass').dialog('open').dialog('setTitle', '编辑分类项');
            },
            Delete: function () {

            },
            reload: function () {
                $("#dgSmallClass").treegrid("load");
            }
        };

    }();

    return {
        init: function () {
            easyloader.locale = "zh_CN";
            easyloader.theme = "metro";
            easyloader.load(components, initialFunc);
        },
        categoryType: categoryType,
        categoryItem: categoryItem
    };
}();