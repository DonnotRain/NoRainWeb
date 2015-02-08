var SysFunction = function () {

    //$('#responsive').modal('show', 'fit')
    var currentUrl = window.rootPath + "/API/Function/";

    //全局变量声明
    var vars = {
        plugData: []
    };
    //初始化

    function InitialFunc() {
        Initialize.loadDataGrid();
        Initialize.initCombos();
        Initialize.initialValidate();
        Initialize.initialBtns();
    }

    //初始化
    var Initialize = {
        initialBtns: function () {
            $(".span4").click(function (ele) {
                var text = $(this).text();
                var start = text.indexOf("icon");
                var result = text.substring(start).replace(/(^\s+)|(\s+$)/g, "");

                $("#iconSample").attr("class", result);
                $("#ImageIndex").val(result.replace(" ", ""));
                $('#iconChoose').dialog('close');
            });
            $("#btnChooseIcon").click(function () {
                $('#iconChoose').modal('show').find(".modal-title").html("选择菜单图标");
            });
        },
        loadDataGrid: function () {
            $("#dgMain").treegrid({
                url: currentUrl,
                method: "get",
                pagination: false,
                rownumbers: true,
                fitColumns: true,
                nowrap: false,
                singleSelect: false,
                idField: 'ID',
                animate: false,
                treeField: 'Name',
                frozenColumns: [[
                { field: 'ck', checkbox: true },
                  { field: 'Name', title: '名称', width: 200, align: 'left' }]],
                columns: [[
                  { field: 'ControlID', title: '编号', width: 80, align: 'left' },
           {
               field: 'FunctionType', title: '功能类型', width: 50, align: 'center', formatter: function (value, row, index) {

                   switch (value) {
                       case 1:
                           return "菜单项";
                           break;
                       case 2:
                           return "系统模块";
                           break;
                       case 3:
                           return "页面功能";
                           break;
                   }

               }
           },
                       { field: 'Sort', title: '排序', width: 30, align: 'center' },
                        { field: 'Path', title: '模块Url', width: 100, align: 'left' },
                           {
                               field: 'Roles', title: '授权角色', width: 120, align: 'center', formatter: function (value, row, index) {
                                   var roles = [];

                                   for (var i = 0; i < row.Roles.length; i++) {
                                       roles.push(row.Roles[i].Name);
                                   }
                                   return roles.join(",");
                               }
                           },
                ]],
                onDblClickRow: function (rowData) {
                    $(this).treegrid("unselectAll");
                    $(this).treegrid("select", rowData.ID);
                    Edit();
                },
                onContextMenu: function (e, row) {
                    e.preventDefault();
                    $(this).treegrid('unselectAll');
                    $(this).treegrid('select', row.ID);
                    $('#mm').menu('show', {
                        left: e.pageX,
                        top: e.pageY
                    });
                }
            });
            $(window).resize(resize);
            resize();
        },
        initCombos: function () {

            //加载父节点
            $.getJSON(currentUrl + "GetAllTree", {}, function (json) {
                // alert("JSON Data: " + json.length);
                vars.plugData = json;
                $("#PID").zTreeCheck({}, json);
            });

            $.CommonAjax({
                url: window.rootPath + "/API/Role/",
                type: "get",
                success: function (data) {

                    //for (var i = 0; i < data.length; i++) {
                    //    data[i] = {
                    //        id: data[i].ID, text: data[i].Name
                    //    };
                    //}
                    NoRainTools.LoadSelectOption($("#RoleIds"), data, "Name", "ID", false);
                    $("#RoleIds").select2({})
                }
            });

        },
        initialValidate: function () {
            var mainForm = $("#fm")
            var error3 = $('.alert-danger', mainForm);
            var success3 = $('.alert-success', mainForm);

            //IMPORTANT: update CKEDITOR textarea with actual content before submit
            mainForm.on('submit', function () {
                for (var instanceName in CKEDITOR.instances) {
                    CKEDITOR.instances[instanceName].updateElement();
                }
            })

            mainForm.validate({
                errorElement: 'span', //default input error message container
                errorClass: 'help-block help-block-error', // default input error message class
                focusInvalid: false, // do not focus the last invalid input
                ignore: "", // validate all fields including form hidden input
                rules: {

                },
                messages: { // custom messages for radio buttons and checkboxes
                    //membership: {
                    //    required: "Please select a Membership type"
                    //},
                    //service: {
                    //    required: "Please select  at least 2 types of Service",
                    //    minlength: jQuery.validator.format("Please select  at least {0} types of Service")
                    //}
                },

                errorPlacement: function (error, element) { // render error placement for each input type
                    if (element.parent(".input-group").size() > 0) {
                        error.insertAfter(element.parent(".input-group"));
                    } else if (element.attr("data-error-container")) {
                        error.appendTo(element.attr("data-error-container"));
                    } else if (element.parents('.radio-list').size() > 0) {
                        error.appendTo(element.parents('.radio-list').attr("data-error-container"));
                    } else if (element.parents('.radio-inline').size() > 0) {
                        error.appendTo(element.parents('.radio-inline').attr("data-error-container"));
                    } else if (element.parents('.checkbox-list').size() > 0) {
                        error.appendTo(element.parents('.checkbox-list').attr("data-error-container"));
                    } else if (element.parents('.checkbox-inline').size() > 0) {
                        error.appendTo(element.parents('.checkbox-inline').attr("data-error-container"));
                    } else {
                        error.insertAfter(element); // for other inputs, just perform default behavior
                    }
                },

                invalidHandler: function (event, validator) { //display error alert on form submit   
                    success3.hide();
                    error3.show();
                    Metronic.scrollTo(error3, -200);
                },

                highlight: function (element) { // hightlight error inputs
                    $(element)
                         .closest('.form-group').addClass('has-error'); // set error class to the control group
                },

                unhighlight: function (element) { // revert the change done by hightlight
                    $(element)
                        .closest('.form-group').removeClass('has-error'); // set error class to the control group
                },

                success: function (label) {
                    label
                        .closest('.form-group').removeClass('has-error'); // set success class to the control group
                },

                submitHandler: function (form) {
                    success3.show();
                    error3.hide();
                    form[0].submit(); // submit the form
                }

            });

            //apply validation on select2 dropdown value change, this only needed for chosen dropdown integration.
            $('.mineSelect2', mainForm).change(function () {
                mainForm.validate().element($(this)); //revalidate the chosen dropdown value and show error or success message for the input
            });

            // initialize select2 tags
            $("#select2_tags").change(function () {
                mainForm.validate().element($(this)); //revalidate the chosen dropdown value and show error or success message for the input 
            }).select2({
                tags: ["red", "green", "blue", "yellow", "pink"]
            });

            //initialize datepicker
            $('.date-picker').datepicker({
                rtl: Metronic.isRTL(),
                autoclose: true
            });

            $('.date-picker .form-control').change(function () {
                mainForm.validate().element($(this)); //revalidate the chosen dropdown value and show error or success message for the input 
            });

        }
    }

    function chooseIcon() {
        $('#iconChoose').dialog('open');
    }

    //添加系统模块
    function Add() {

        $('#fm')[0].reset()
        $('#responsive').modal('show').find(".modal-title").html("新增功能插件");

        var items = $("#dgMain").treegrid('getChecked');

        $("#IsMenu").iCheck('check');
        $("#ImageIndex").val("");

        //编号可编辑
        $("#ControlID").prop("readonly", false);
        //主动赋值ID
        $("#ID").val(0);
    }

    function Edit() {
        $('#fm')[0].reset()
        $('#responsive').modal('show').find(".modal-title").html("编辑功能插件");

        var items = $("#dgMain").treegrid('getSelections');

        if (items && items.length > 0) {
            var item = items[0];

            for (var filed in item) {
                $("#" + filed).val(item[filed]);
            }

            //排序 
            $("#Sort").val(item.Sort);

            $("#iconSample").attr("class", item.ImageIndex);

            //几个按钮
            $("#IsMenu").iCheck((item.FunctionType == 1) ? 'check' : 'uncheck');
            $("#IsModule").iCheck((item.FunctionType == 2) ? 'check' : 'uncheck');
            $("#IsFunction").iCheck((item.FunctionType == 3) ? 'check' : 'uncheck');
            //上级模块
            $("#PID").zTreeCheck("setValue", item.PID);
            //角色
            var roleIds = [];
            for (var i = 0; i < item.Roles.length; i++) {
                roleIds.push(item.Roles[i].ID);
            }
            $("#RoleIds").select2("val", roleIds);
            //编号不可编辑
            $("#ControlID").prop("readonly", true);

            $('#dlgMain').dialog('open').dialog('setTitle', '修改系统模块');
        }
        else {
            $.messager.alert("未选取数据行", "请先选取要编辑的行", 'info');
            return;
        }
    }

    function Delete() {
        var rows = $("#dgMain").treegrid('getSelections');
        if (!rows.length) {
            $.messager.alert("未选取数据行", "请先选取要删除的行", 'info');
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
                    url: currentUrl + "DeleteSome/" + ids.join(","),
                    type: "delete",
                    success: function (data, textStatus) {
                        $.jMask("delete").hide();
                        $.gritter.add({
                            // (string | mandatory) the heading of the notification
                            title: '成功!',
                            // (string | mandatory) the text inside the notification
                            text: '删除成功。',
                            class_name: 'gritter-success',
                            time: 3000
                        });
                        reloadDataGrid();
                    }
                });
            }
        });
    }

    function Save() {
        var type = "post";
        if ($("#ID").val() && $("#ID").val() != 0) {
            type = "put";
        }

        //验证输入合法性
        var result = $("#fm").valid();
        if (!result) {
            return;
        }
        success3.show();
        error3.hide();

        var data = $("#fm").serializeArray();
        $.CommonAjax({
            url: currentUrl,
            type: type,
            data: data,
            success: function (data, textStatus) {
                $('#dlgMain').dialog('close');
                reloadDataGrid();
            }
        });
    }

    function saveIcon() {

    }

    //重新加载dataGrid
    function reloadDataGrid() {
        $("#dgMain").treegrid('reload');
        $("#dgMain").treegrid('unselectAll');
    }

    // 重新布局
    function resize() {
        var width = $(".dg-panel").width();
        if (width < 700) width = 700;
        var height = $(window).height() - 70;
        $("#dgMain").treegrid('resize', { // 重新布局DataGrid
            width: width
        });
    }

    return {
        init: function () {
            easyloader.locale = "zh_CN";
            easyloader.theme = "metro";
            easyloader.load(['parser', 'treegrid', 'messager', "form"], InitialFunc);
        }
        , reloadDataGrid: reloadDataGrid
        , Add: Add
        , Edit: Edit,
        Delete: Delete,
        Save: Save
    };
}();
