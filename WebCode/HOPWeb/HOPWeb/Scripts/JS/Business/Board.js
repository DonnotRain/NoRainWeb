PlugFunctions.Attendence.Board = function () {

    var currentUrl = rootPath + "/API/Board/";
    //全局变量声明
    var vars = {
        totalTimeout: 120 * 1000,
        um:undefined
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
                idField: "ID",
                columns: [[
                    { field: 'ck', checkbox: true },
                { field: 'Title', title: '标题', width: 100, align: 'center' },
                {
                    field: 'Detail', title: '正文', width: 100, align: 'center', formatter: function (value) {
                        var returnValue = (value == null ? "" : value.stripHTML());
                        returnValue = returnValue.replace(/(\&nbsp\;)*/g, "");

                        return returnValue;
                    }},
                {
                    field: 'ValidDate', title: '有效期至', width: 70, align: 'center', formatter: dateFormatter
                },
                {
                    field: 'CreateTime', title: '最后修改时间', width: 100, align: 'center', formatter: timeFormatter
                }
                  //{
                  //    field: 'Duration', title: '附件', width: 100, align: 'center', formatter: function (value, row) {

                  //        if (row.Files && row.Files.length > 0) {
                  //            var imagePath = rootPath + "/API/FileItem/DownloadById?fileId=" + row.Files[0].ID;
                  //            return "<a target='_blank' href='" + imagePath + "' style='color:blue' >" + row.Files[0].FileName + "</a>";
                  //        }
                  //        return "<span style='color:rgb(187, 69, 69)'>未上传</span>";
                  //    }
                  //}
                ]]
                , onBeforeLoad: function (para) {
                    para.beginDate = $("#conditionBeginTime").val();
                    para.endDate = $("#conditionEndTime").val();
                    para.userName = $("#conditionUserName").val();

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
    }
    //初始化
    function InitialFunc() {
        Initialize.initialDialog();
        Initialize.initialValidate();
        Initialize.loadDataGrid();
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
    // 上传数据 
    function ajaxUploadFile(postBack) {
        var file = $("#input-file").val();
        if (file == ""||file == undefined) {
            postBack();
            //  alert("请选择上传的图片");
            return;
        }
        else {
            //判断上传的文件的格式是否正确  
            //var filefullName = file.substring(file.lastIndexOf("\\") + 1);
            //var fileType = file.substring(file.lastIndexOf(".") + 1);
            //if (fileType != "png" && fileType != "jpg" && fileType != "bmp") {
            //    alert("上传文件格式错误");
            //    return;
            //}
            $.jMask("uploadFile", "上传文件中，请稍后").show();
            var url = rootPath + "/API/FileItem/Upload";
            $.ajaxFileUpload({
                url: url,
                secureuri: false,
                dataType: "json",
                data: { entityType: "TemplateVersion", "entityId": vars.currentVersionId },
                fileElementId: "input-file",        //file的id    
                success: function (data, status) {
                    $("#FileID").val(data.ID);
                    var fileItemId = data.ID;
                    //  $("#img-flow-image").attr("src", "/api/FileItem/DownloadById?fileId=" + fileItemId);
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

        //上传文件
        try {

            var postBack = function () {
                form_data = $("#fm").serializeArray();
                var detailObject = { name: "Detail", value: vars.um.getContent() };
                form_data.push(detailObject);

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

    return {
        init: function () {
            easyloader.locale = "zh_CN";
            easyloader.theme = "metro";
            easyloader.load(['parser', 'validatebox', 'datagrid', 'combobox', 'form', 'dialog', 'messager'], InitialFunc);
        }
       , reloadDataGrid: reloadDataGrid,
        add: function () {
            $('#fm').form('clear');
            vars.um.execCommand('cleardoc');
            $("#TargetZone").val("1");
            $("#TargetType").val("1");
            $('#dlgMain').dialog('open').dialog('setTitle', '添加系统公告');
        }
        , edit: function () {
            $('#fm').form('clear');
            vars.um.execCommand('cleardoc');
            var items = $("#dgMain").datagrid('getSelections');


            if (items && items.length > 0) {
                var item = items[0];

                $("#dgMain").datagrid('unselectAll');
                $("#dgMain").datagrid('selectRecord', item.ID);

                $('#fm').form('load', item);
                for (var filed in item) {
                    $("#" + filed).val(item[filed]);
                }
                //特殊的输入框 dateFormatter
                $("#ValidDate").val(dateFormatter(item.ValidDate));
                vars.um.setContent(item.Detail == null ? "" : item.Detail);

                $("#spanFileName").html((item.Files && item.Files.length) ? item.Files[0].FileName : "");
                $('#dlgMain').dialog('open').dialog('setTitle', '修改系统公告');
            }
            else {
                $.Show({ message: "未选取数据行", type: "error" });
                return;
            }
        },
        deleteItems: function () {
            var rows = $("#dgMain").datagrid('getSelections');
            if (!rows.length) {
                $.Show({ message: "未选取数据行", type: "error" });
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



