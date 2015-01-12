 var Information = function () {
    var currentUrl = rootPath + "/API/Arrange/";
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
                {field:'ck',checkbox:true},
                { field: 'Title', title: '学习/任务名称', width: 100, align: 'center' },
                {
                    field: 'Detail', title: '描述', width: 100, align: 'center'
                },
                {
                    field: 'Type', title: '类型', width: 120, align: 'left', formatter: formatterType
                },
                {
                    field: 'ExamTime', title: '考试时间', width: 120, align: 'left', formatter: function (value) {
                        return formatJSONDate(value,'y-m-d');
                    }
                },
                {
                    field:'Files', title: '附件', width: 100, align: 'center', formatter: function (value, row) {
                        if (row.FileName != "" && row.FileName != null) {
                            var imagePath = rootPath + "/api/FileItem/DownloadById?fileId=" + row.FileID;
                            return "<a target='_blank' href='" + imagePath + "' style='color:blue' >" + row.FileName + "</a>";
                        }
                        return "无相关文件";
                    }
                }
                ]]
               , onBeforeLoad: function (para) {
                   para.title = $("#studyName").val();
                   para.type = $("#studyType").combobox("getValue");
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
        intialCombobox: function () {
            $("#Type").combobox({
                onChange: function (newValue, oldValue) {
                    if (newValue == '1') {
                        $("#EntityIds").combobox({
                            url: rootPath + '/api/study/GetAll',
                            method: 'get',
                            textField: 'Title',
                            valueField: 'ID',
                            disabled: false,
                            multiple: true
                        });
                    } else if (newValue == '2') {
                        $("#EntityIds").combobox({
                            url: rootPath + '/api/Exam/GetAll',
                            method: 'get',
                            textField: 'Title',
                            valueField: 'ID',
                            disabled: false,
                            multiple: false
                        });
                    }
                }
            });
        },
        initialSaveCombobox: function () {
            $("#EntityIds").combobox('clear');
            $("#EntityIds").combobox({
                disabled: true,
                onLoadSuccess: function () {
                }
            });
        },
        initialEditCombobox: function (type, entityIds) {
            $("#Type").combobox("setValue", type);
            var arrayIds = entityIds.split(',');
            if (type == '1') {
                $("#EntityIds").combobox({
                    url: rootPath + '/api/study/GetAll',
                    method: 'get',
                    textField: 'Title',
                    valueField: 'ID',
                    disabled: false,
                    multiple: true,
                    onLoadSuccess: function () {
                        $("#EntityIds").combobox("setValues", arrayIds);
                        arrayIds = [];
                        type = 0;
                    }
                });
            } else if (type == '2') {
                $("#EntityIds").combobox({
                    url: rootPath + '/api/Exam/GetAll',
                    method: 'get',
                    textField: 'Title',
                    valueField: 'ID',
                    disabled: false,
                    multiple: false,
                    onLoadSuccess: function () {
                        $("#EntityIds").combobox("setValues", arrayIds);
                        arrayIds = [];
                        type = 0;
                    }
                });
            }
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
                dataType: "xml",
                data: { },
                fileElementId: "input-file",        //file的id    
                success: function (data, status) {
                    $("#FileID").val($(data).find("ID").text());
                    var fileItemId = $(data).find("ID").text();
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
        if ($("#Type").combobox('getValue') == '' && $("#Type").combobox('getValue')==undefined) {
            return;
        }

        if ($("#EntityIds").combobox("getValues").length == 0) {
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
            return "学习";
        } else if (parseInt(value) === 2) {
            return "考试";
        }  else {
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

        Initialize.initialDialog();
        Initialize.loadDataGrid();
        Initialize.initialValidate();
        Initialize.intialCombobox();

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
        init:function(){
            easyloader.locale = "zh_CN";
            easyloader.theme = "metro";
            easyloader.load(['parser', 'validatebox', 'datagrid', 'combobox', 'form', 'dialog', 'messager', 'tabs','datebox'], InitialFunc);
        },
        reloadDataGrid: reloadDataGrid,
        add: function () {
            $('#fm').form('clear');
            Initialize.initialSaveCombobox();
            $("#spanFileName").text("");
            $('#dlgMain').dialog('open').dialog('setTitle', '添加考试/学习安排');
        },
        edit: function () {
            $('#fm').form('clear');
            var items = $("#dgMain").datagrid('getChecked');

            if (items && items.length > 0) {
                var item = items[0];

                $("#dgMain").datagrid('unselectAll');
                $("#dgMain").datagrid('selectRecord', item.ID);
                item.EntityIds = item.EntityID;

                $('#fm').form('load', item);
                for (var filed in item) {
                    $("#" + filed).val(item[filed]);
                }
                //特殊的输入框 dateFormatter
                Initialize.initialEditCombobox(item.Type, item.EntityID);
                $("#spanFileName").html((item.FileName != "" && item.FileName != null) ? item.FileName : "");
                $("#ExamTime").val(formatterTime(item.ExamTime));
                $('#dlgMain').dialog('open').dialog('setTitle', '修改考试/学习安排');
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