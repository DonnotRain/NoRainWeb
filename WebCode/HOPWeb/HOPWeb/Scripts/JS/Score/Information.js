var Information = function () {
    var currentUrl = rootPath + "/API/Score/";
    //全局变量声明
    var vars = {
        totalTimeout: 120 * 1000
    };

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
                columns: [[
                { field: 'Code', title: '促销员编号', width: 100, align: 'center' },
                {
                    field: 'UserName', title: '促销员名称', width: 100, align: 'center'
                },
                {
                    field:'Title',title:'考试名称',width:100,align:'center'
                },
                {
                    field: "CreateTime", title: "考试时间", width: 100, align: 'center', formatter: function (value) {
                        return formatJSONDate(value);
                    }
                },
                {
                    field: 'Score', title: '考试成绩', width: 120, align: 'left', formatter: function (value) {
                        return value + "分";
                    }
                }
                ]]
               , onBeforeLoad: function (para) {
                   para.createTime = $("#createTime").val();
                   para.userId = $("#personName").combobox('getValue');
               }
            });
        },
        initCombobox: function () {
            $("#personName").combobox({
                valueField: 'ID',
                textField: 'UserName',
                method:'get',
                url:rootPath+'/api/user/GetAll'
            });
        }
    };

    // 上传数据 
    function ajaxUploadFile(postBack) {
        var file = $("#input-file").val();
        if (file == "") {
            postBack();
            return;
        }
        else {
            $.jMask("uploadFile", "上传文件中，请稍后").show();
            var url = rootPath + "/API/FileItem/Upload";
            $.ajaxFileUpload({
                url: url,
                secureuri: false,
                dataType: "json",
                data: {},
                fileElementId: "input-file",        //file的id    
                success: function (data, status) {
                    $("#FileID").val(data.ID);
                    var fileItemId = data.ID;
                    $("#spanFileName").html("");
                    $.Show({
                        message: "文件上传成功",
                        type: "success",
                        hideAfter: 3
                    });
                    $.jMask("uploadFile").hide();
                    postBack();
                },
                error: function (data, status, e) {
                    $.messager.alert("警告", e, "info");
                }
            });

        }
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
        if ($("#Type").combobox('getValue') == '0') {
            return;
        }

        //上传文件
        try {
            var postBack = function () {
                form_data = $("#fm").serializeArray();
                // form_data.Type = $("#Type").combobox('getValue');

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
            ajaxUploadFile(postBack);

        }
        catch (e) {
            $.jMask("HideAll");
            alert(e.message);
        }
    }

    // 设置学习类型
    function formatterType(value, row, index) {
        if (parseInt(value) === 1) {
            return "规章制度";
        } else if (parseInt(value) === 2) {
            return "商品资料";
        } else if (parseInt(value) === 3) {
            return "市场资料";
        } else {
            return "";
        }
    }

    //初始化
    function InitialFunc() {
        var width = $(".page-content").width();
        var height = $(window).height() - 130;
        $('#tabMain').show();
        $('#tabMain').tabs({
            width: width,
            height: height,
            onSelect: function () {
                //    resize();
            }
        });

        //Initialize.initialDialog();
        Initialize.loadDataGrid();
        Initialize.initCombobox();
        //Initialize.initialValidate();

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

    return {
        init: function () {
            easyloader.locale = "zh_CN";
            easyloader.theme = "metro";
            easyloader.load(['parser', 'validatebox', 'datagrid', 'combobox', 'form', 'dialog', 'messager', 'tabs', 'datebox'], InitialFunc);
        },
        reloadDataGrid: reloadDataGrid
    };
}();