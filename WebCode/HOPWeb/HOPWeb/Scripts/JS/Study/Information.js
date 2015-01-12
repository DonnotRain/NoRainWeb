 var Information = function () {
    var currentUrl = rootPath + "/API/Study/";
    //全局变量声明
    var vars = {
        totalTimeout: 120 * 1000,
        um: undefined
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
                { field: 'Title', title: '主题', width: 100, align: 'center' },
                {
                    field: 'Detail', title: '描述', width: 100, align: 'center'
                },
                {
                    field: 'Type', title: '类型', width: 120, align: 'center', formatter: formatterType
                },
                {
                    field: 'Remark', title: '备注', width: 100, align: 'center', formatter: function (value) {
                        var returnValue = (value == null ? "" : value.stripHTML());
                        returnValue = returnValue.replace(/(\&nbsp\;)*/g, "");

                        return returnValue;
                    }
                }
                //{
                //    field:'Files', title: '附件', width: 100, align: 'center', formatter: function (value, row) {
                //        if (row.FileName != "" && row.FileName != null) {
                //            var imagePath = rootPath + "/api/FileItem/DownloadById?fileId=" + row.FileId;
                //            return "<a target='_blank' href='" + imagePath + "' style='color:blue' >" + row.FileName + "</a>";
                //        }
                //        return "无相关文件";
                //    }
                //}
                ]]
               , onBeforeLoad: function (para) {
                   para.beginTime = $("#studyBeginTime").val();
                   para.title = $("#studyName").val();
                   para.type = $("#studyType").combobox("getValue");
                   para.endTime = $("#studyEndTime").val();
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
        initialEditor: function () {
            vars.um = UM.getEditor('umEditor', {
                "AllowFiles":[".jpg",".jpeg"]
            });
            vars.um.execCommand('cleardoc');
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
                    $("#FileId ").val($(data).find("ID").text());
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
        if ($("#Type").combobox('getValue') == '0') {
            return;
        }

        //上传文件
        try {
            var postBack = function () {
                form_data = $("#fm").serializeArray();
                var remarkObject = { name: "Remark", value: vars.um.getContent() };
                form_data.push(remarkObject);

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
          //  ajaxUploadFile(postBack);
           postBack()
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

        Initialize.initialDialog();
        Initialize.loadDataGrid();
        Initialize.initialValidate();
        Initialize.initialEditor();

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
            easyloader.load(['parser', 'validatebox', 'datagrid', 'combobox', 'form', 'dialog', 'messager', 'tabs', 'datebox'], InitialFunc);
        },
        reloadDataGrid: reloadDataGrid,
        add: function () {
            $('#fm').form('clear');
            vars.um.execCommand('cleardoc');
            $("#spanFileName").text("");
            $('#dlgMain').dialog('open').dialog('setTitle', '添加外勤人员');
        },
        edit: function () {
            $('#fm').form('clear');
            vars.um.execCommand('cleardoc');
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

                $("#Type").combobox("setValue", item.Type);
                vars.um.setContent(item.Remark == null ? "" : item.Remark);
                $("#spanFileName").html((item.FileName !="" && item.FileName!=null) ? item.FileName : "");
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