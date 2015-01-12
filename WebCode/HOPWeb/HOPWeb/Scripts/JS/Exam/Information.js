var Information = function () {
    var currentUrl = rootPath + "/API/Exam/";
    //全局变量声明
    var vars = {
        totalTimeout: 120 * 1000,
        judgeId: 0,
        choiceId: 0,
        studyId: 0,
        judgeData: [],
        choiceData: [],
        studyData: []
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
                { field: 'ck', checkbox: true },
                { field: 'Title', title: '主题', width: 100, align: 'center' },
                {
                    field: 'Detail', title: '描述', width: 100, align: 'center'
                },
                {
                    field: 'Type', title: '类型', width: 120, align: 'left', formatter: formatterType
                },
                {
                    field: 'BeginTime', title: '试卷有效开始时间', width: 120, align: 'center', formatter: function (value) {
                        return formatJSONDate(value,'y-m-d');
                    }
                },
                {
                    field: 'ExpiredTime', title: '试卷有效结束时间', width: 120, align: 'center', formatter: function (value) {
                        return formatJSONDate(value, 'y-m-d');
                    }
                },
                {
                    field: 'Files', title: '附件', width: 100, align: 'center', formatter: function (value, row) {
                        if (row.FileName != "" && row.FileName != null) {
                            var imagePath = rootPath + "/api/FileItem/DownloadById?fileId=" + row.FileID;
                            return "<a target='_blank' href='" + imagePath + "' style='color:blue' >" + row.FileName + "</a>";
                        }
                        return "无相关文件";
                    }
                }
                ]]
               , onBeforeLoad: function (para) {
                   para.title = $("#examName").val();
                   para.type = $("#examType").combobox("getValue");
               }
            });

            $("#dgJudge").datagrid({
                data:vars.judgeData,
                pagination: false,
                fitColumns: true,
                nowrap: false,
                checkOnSelect: false,
                selectOnCheck: false,
                singleSelect: true,
                columns: [[
                { field: 'ck', checkbox: true },
                { field: 'Title', title: '主题', width: 200, align: 'center' },
                {
                    field: 'Answer', title: '答案', width: 100, align: 'center', formatter: formatterAnswer
                },
                {
                    field: 'Score', title: '分数', width: 100, align: 'center'
                },
                {
                    field: 'AnswerTime', title: '计时', width: 120, align: 'left'
                },
                {
                    field:'Seq',title:'顺序',width:120,align:'left'
                }
                ]]
            });

            $("#dgChoice").datagrid({
                data: vars.choiceData,
                pagination: false,
                fitColumns: true,
                nowrap: false,
                checkOnSelect: false,
                selectOnCheck: false,
                singleSelect: true,
                columns: [[
                { field: 'ck', checkbox: true },
                { field: 'Title', title: '题目', width: 200, align: 'center' },
                {
                    field: 'ChoiceA', title: '答案A', width: 100, align: 'center'
                },
                {
                    field: 'ChoiceB', title: '答案B', width: 100, align: 'center'
                },
                {
                    field: 'ChoiceC', title: '答案C', width: 100, align: 'center'
                },
                {
                    field: 'Answer', title: '标准答案', width: 100, align: 'center'
                },
                {
                    field: 'Score', title: '分数', width: 100, align: 'center'
                },
                {
                    field: 'AnswerTime', title: '计时', width: 120, align: 'left'
                },
                {
                    field: 'Seq', title: '顺序', width: 120, align: 'left'
                }
                ]]
            });
        },
        initialDialog: function () {
            $('#dlgMain').show();
            $("#dlgMain").dialog({
                title: "新增公告",
                modal: true,
                closed: true,
                buttons: [
                     {
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

            // 增加判断题
            $("#dlgJudge").show();
            $("#dlgJudge").dialog({
                title: "新增判断题",
                modal: true,
                closed: true,
                buttons: [{
                    iconCls: "icon-save",
                    text: '保存',
                    handler: function () {
                        saveJudge();
                    }
                }, {
                    iconCls: "icon-remove",
                    text: '取消',
                    handler: function () {
                        $("#dlgJudge").dialog('close');
                    }
                }]
            });

            // 增加选择题
            $("#dlgChoice").show();
            $("#dlgChoice").dialog({
                title: "新增选择题",
                modal: true,
                closed: true,
                buttons: [{
                    iconCls: "icon-save",
                    text: '保存',
                    handler: function () {
                        saveChoice();
                    }
                }, {
                    iconCls: "icon-remove",
                    text: '取消',
                    handler: function () {
                        $("#dlgChoice").dialog('close');
                    }
                }]
            });
        },
        initialValidate: function () {
            $("#fm").validVal({ language: "cn" });
            $("#fmJudge").validVal({ language: "cn" });
            $("#fmChoice").validVal({ language: "cn" });
        },
        initialTabs: function () {
            $('#tt').tabs({
                border: false,
                onSelect: function (title) {
                    
                }
            });
        },
        initialCombobox: function () {
            $("#Study").combobox({
                url: "/api/study/GetAll",
                method: "get",
                valueField: 'ID',
                textField: 'Title',
                multiple: true,
                panelHeight: 'auto'
            });
        },
        initialValidateTime: function () {
            $("#HasValidateTime").bind("change", function () {
                var value = $("#HasValidateTime").prop('checked');
                if (value) {
                    $("#validateTime").show();
                } else {
                    $("#validateTime").hide();
                }
            });
        }
    };

    // 增加判断题
    function saveJudge() {
        var type = "post";

        if ($("#JudgeID").val() && $("#JudgeID").val() != 0) {
            type = "put";
        }
        //验证输入合法性
        var form_data = $("#fmJudge").triggerHandler("submitForm");
        if (!form_data) {
            return;
        }

        if ($("#Answer").combobox('getValue') == "") {
            return;
        }

        //上传文件
        try {
            $.jMask("save", "保存中，请稍后").show();
            form_data = $("#fmJudge").serializeArray();
            if (type === "post") {
                var elements = formatterJson(form_data);
                vars.judgeId = guid();
                elements["ID"] = (vars.judgeId).toString();
                if (elements.Answer === "1") {
                    elements.Answer = true;
                }
                else {
                    elements.Answer = false;
                }
                vars.judgeData.push(elements);
            }
            else {
                var elements = formatterJson(form_data);
                for (var i = 0; i < vars.judgeData.length; i++) {
                    if (vars.judgeData[i].ID == elements["ID"]) {
                        var elem = vars.judgeData[i];
                        for (var j in elem) {
                            elem[j] = elements[j];
                        }
                        if (elem.Answer === "1") {
                            elem.Answer = true;
                        }
                        else {
                            elem.Answer = false;
                        }
                        break;
                    }
                }
            }
            $("#dgJudge").datagrid({
                data:vars.judgeData
            });
            $('#dlgJudge').dialog('close');
            $.jMask("save").hide();
            $.Show({
                message: "保存成功",
                type: "success",
                hideAfter: 3
            });
            $("#dgJudge").datagrid('reload');
        }
        catch (e) {
            $.jMask("HideAll");
            alert(e.message);
        }
    }

    // 增加选择题
    function saveChoice() {
        var type = "post";

        if ($("#ChoiceID").val() && $("#ChoiceID").val() != 0) {
            type = "put";
        }
        //验证输入合法性
        var form_data = $("#fmChoice").triggerHandler("submitForm");
        if (!form_data) {
            return;
        }
        if ($("#ChoiceAnswer").combobox('getValue')==""&&$("#ChoiceAnswer").combobox('getValue') == '0') {
            return;
        }

        //上传文件
        try {
            $.jMask("save", "保存中，请稍后").show();
            form_data = $("#fmChoice").serializeArray();
            if (type === "post") {
                var elements = formatterJson(form_data);
                vars.choiceId = guid();
                elements["ID"] = (vars.choiceId).toString();
                vars.choiceData.push(elements);
            }
            else {
                var elements = formatterJson(form_data);
                for (var i = 0; i < vars.choiceData.length; i++) {
                    if (vars.choiceData[i].ID == elements["ID"]) {
                        var elem = vars.choiceData[i];
                        for (var j in elem) {
                            elem[j] = elements[j];
                        }
                        break;
                    }
                }
            }
            $("#dgChoice").datagrid({
                data: vars.choiceData
            });
            $('#dlgChoice').dialog('close');
            $.jMask("save").hide();
            $.Show({
                message: "保存成功",
                type: "success",
                hideAfter: 3
            });
            $("#dgChoice").datagrid('reload');
        }
        catch (e) {
            $.jMask("HideAll");
            alert(e.message);
        }
    }

    // 格式化json
    function formatterJson(data) {
        var obj = {};
        for (var i = 0; i < data.length; i++) {
            obj[data[i].name] = data[i].value;
        }

        return obj;
    }

    // 格式化答案
    function formatterAnswer(value, row, index) {
        if (value) {
            return "正确";
        } else {
            return "错误";
        }
    }

    // 上传数据 
    function ajaxUploadFile(postBack) {
        var file = $("#input-file").val();
        if (file == "" || file == undefined) {
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
                data: {},
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

    // 保存考题
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
        if ($("#Type").combobox('getValue') == '') {
            $.Show({
                message: "类型不能为空",
                type: "error",
                hideAfter: 3
            });
            return;
        }

        if ($("#HasValidateTime").prop('checked')) {
            if ($("#BeginTime").val() == "" || $("#ExpiredTime").val() == "") {
                $.Show({
                    message: "开始时间和截止时间不能为空",
                    type: "error",
                    hideAfter: 3
                });
                return;
            }
        }

        //上传文件
        try {
            var postBack = function () {
                form_data = $("#fm").serializeArray();
                form_data = formatterJson(form_data);
                form_data["HasValidateTime"] = $("#HasValidateTime").prop('checked');
                form_data["TRN_StudyID"] = $("#Study").combobox('getValues');
                form_data["Judge"] = vars.judgeData;
                form_data["Choice"] = vars.choiceData;

                $.jMask("save", "保存中，请稍后").show();
                $.CommonAjax({
                    url: currentUrl,
                    type: type,
                    dataType: "json",
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

        Initialize.initialDialog();
        Initialize.loadDataGrid();
        Initialize.initialValidate();
        Initialize.initialTabs();
        Initialize.initialCombobox();
        Initialize.initialValidateTime();

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
            easyloader.load(['parser', 'validatebox', 'datagrid', 'combobox', 'form', 'dialog', 'messager', 'tabs', 'datebox','datetimebox'], InitialFunc);
        },
        reloadDataGrid: reloadDataGrid,
        add: function () {
            $('#fm').form('clear');
            vars.judgeData = [];
            vars.choiceData = [];
            vars.studyData = [];
            $("#dgJudge").datagrid('loadData', vars.judgeData);
            $("#dgChoice").datagrid('loadData', vars.choiceData);
            $("#spanFileName").text("");
            $("#HasValidateTime").prop('checked',false);
            $("#validateTime").hide();
            $('#dlgMain').dialog('open').dialog('setTitle', '新增考试题');
        },
        edit: function () {
            $('#fm').form('clear');
            var items = $("#dgMain").datagrid('getChecked');

            if (items && items.length > 0) {
                var item = items[0];

                $("#dgMain").datagrid('unselectAll');
                $("#dgMain").datagrid('selectRecord', item.ID);

                $('#fm').form('load', item);

                //特殊的输入框 dateFormatter
                $("#HasValidateTime").prop('checked',item.HasValidateTime);
                if (item.HasValidateTime) {
                    $("#BeginTime").val(formatterTime(item.BeginTime));
                    $("#ExpiredTime").val(formatterTime(item.ExpiredTime));
                    $("#validateTime").show();
                } else {
                    $("#validateTime").hide();
                }
                $("#Type").combobox("setValue", item.Type);
                if (item.TRN_StudyID != null && item.TRN_StudyID != undefined) {
                    $("#Study").combobox("setValues", item.TRN_StudyID);
                }
                $("#dgJudge").datagrid("loadData", item.Judge);
                $("#dgChoice").datagrid("loadData", item.Choice);
                vars.judgeData = item.Judge;
                vars.choiceData = item.Choice;
                $("#ID").val(item.ID);
                $('#dlgMain').dialog('open').dialog('setTitle', '修改考试题');
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
                    $.jMask("delete", "删除中，请稍后").show();
                    $.each(rows, function (rowIndex, rowData) {
                            ids.push(rowData.ID);
                    });
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
        },
        addJudge: function () {
            $('#fmJudge').form('clear');
            $("#JudgeID").val("");
            $('#dlgJudge').dialog('open').dialog('setTitle', '新增判断题');
        },
        editJudge: function () {
            $('#fmJudge').form('clear');
            var items = $("#dgJudge").datagrid('getChecked');

            if (items && items.length > 0) {
                var item = items[0];

                $("#dgJudge").datagrid('unselectAll');
                $("#dgJudge").datagrid('selectRecord', item.ID);

                $('#fmJudge').form('load', item);
                
                //特殊的输入框 dateFormatter
                $("#Answer").combobox("setValue", item.Answer);
                $("#JudgeID").val(item.ID);
                $('#dlgJudge').dialog('open').dialog('setTitle', '修改选择题');
            }
            else {
                $.Show({ message: "未勾选数据行", type: "error" });
                return;
            }
        },
        deleteJudge: function () {
            var rows = $("#dgJudge").datagrid('getChecked');
            if (!rows.length) {
                $.Show({ message: "未勾选数据行", type: "error" });
                return;
            }
            $.messager.confirm('确认', '确定要删除所选数据？', function (r) {
                if (r) {
                    var ids = new Array();
                    $.jMask("delete", "删除中，请稍后").show();
                    $.each(rows, function (rowIndex, rowData) {
                        for (var i = 0; i < vars.judgeData.length; i++) {
                            if (vars.judgeData[i].ID == rowData.ID) {
                                vars.judgeData.splice(i, 1);
                            }
                        }
                    });
                    $.jMask("delete").hide();
                    $.Show({ message: "删除成功", type: "success" });
                    $("#dgJudge").datagrid("loadData",vars.judgeData);
                }
            });
        },
        addChoice: function () {
            $('#fmChoice').form('clear');
            $("#ChoiceID").val("");
            $('#dlgChoice').dialog('open').dialog('setTitle', '新增选择题');
        },
        editChoice: function () {
            $('#fmChoice').form('clear');
            var items = $("#dgChoice").datagrid('getChecked');

            if (items && items.length > 0) {
                var item = items[0];

                $("#dgChoice").datagrid('unselectAll');
                $("#dgChoice").datagrid('selectRecord', item.ID);

                $('#fmChoice').form('load', item);

                //特殊的输入框 dateFormatter
                $("#ChoiceAnswer").combobox("setValue", item.Answer);
               
                $("#ChoiceID").val(item.ID);
                $('#dlgChoice').dialog('open').dialog('setTitle', '修改选择题');
            }
            else {
                $.Show({ message: "未勾选数据行", type: "error" });
                return;
            }
        },
        deleteChoice: function () {
            var rows = $("#dgChoice").datagrid('getChecked');
            if (!rows.length) {
                $.Show({ message: "未勾选数据行", type: "error" });
                return;
            }
            $.messager.confirm('确认', '确定要删除所选数据？', function (r) {
                if (r) {
                    var ids = new Array();
                    $.jMask("delete", "删除中，请稍后").show();
                    $.each(rows, function (rowIndex, rowData) {
                        for (var i = 0; i < vars.choiceData.length; i++) {
                            if (vars.choiceData[i].ID == rowData.ID) {
                                vars.choiceData.splice(i, 1);
                            }
                        }
                    });
                    $.jMask("delete").hide();
                    $.Show({ message: "删除成功", type: "success" });
                    $("#dgChoice").datagrid("loadData", vars.choiceData);
                }
            });
        }
    };
}();