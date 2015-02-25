var SysFunction = function () {

    //当前功能中的变量
    var currentUrl = window.rootPath + "/API/Function/";
    var mainForm = $("#fm")
    var error3 = $('.alert-danger', mainForm);
    var success3 = $('.alert-success', mainForm);

    //全局变量声明
    var vars = {
        plugData: [],
        minTableWidth: 720
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

            $(".fa-item").click(function (ele) {
                var subEle = $(this).find("i");
                var result = subEle.attr("class");

                $("#iconSample").attr("class", result);
                $("#ImageIndex").val(result);
                $('#iconChoose').modal('hide');
            });

            $(" .glyphicons-demo ul li").click(function (ele) {
                var subEle = $(this).find("span.glyphicon");
                var result = subEle.attr("class");

                $("#iconSample").attr("class", result);
                $("#ImageIndex").val(result);
                $('#iconChoose').modal('hide');
            });
            $(" .simplelineicons-demo .item-box .item").click(function (ele) {
                var subEle = $(this).find("span");
                var result = subEle.attr("class");

                $("#iconSample").attr("class", result);
                $("#ImageIndex").val(result);
                $('#iconChoose').modal('hide');
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
                       case 4:
                           return "子系统";
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
                               {
                                   field: 'Others', title: '其他属性', width: 120, align: 'center', formatter: function (value, row, index) {
                                       var canDelete = '<span class="label label-success">可删除</span>';
                                       var canNotDelete = '<span class="label label-warning">不可删除</span>';
                                       var isEnabled = '<span class="label label-success">已启用</span>';
                                       var isNotEnabled = '<span class="label label-danger">未启用</span>';
                                       var result = "";
                                       if (row.IsEnabled) result += isEnabled;
                                       else result += isNotEnabled;
                                       if (row.IsCanDelete) result += canDelete;
                                       else result += canNotDelete;
                                       return result;
                                   }
                               }
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
                },
                onLoadSuccess: function () {
                    rebuildFunctionCombo();
                }

            });
            $(window).resize(resize);
            resize();
        },
        initCombos: function () {

            //加载父节点
            $.getJSON(currentUrl + "AlljsTreeData", {}, function (json) {
                var rootItem = NoRainTools.deepcopy(json[0]);
                rootItem["id"] = "-1";
                rootItem["name"] = "系统根目录";
                rootItem.icon = "icon-home";
                rootItem.text = "系统根目录";
                rootItem.children = json;
                rootItem.state = { opened: true };
                $("#PID").jsTreeCheck({}, [rootItem]);
            });

            $.CommonAjax({
                url: window.rootPath + "/API/Role/",
                type: "get",
                success: function (data) {
                    NoRainTools.LoadSelectOption($("#RoleIds"), data, "Name", "ID", false);
                    $("#RoleIds").select2({})
                }
            });

        },
        initialValidate: function () {

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

    //添加系统模块
    function Add() {
        success3.hide();
        error3.hide();
        $("#RoleIds").select2("val", []);
        $('#fm')[0].reset()
        $('#responsive').modal('show').find(".modal-title").html("新增功能插件");

        var items = $("#dgMain").treegrid('getChecked');

        $("#IsMenu").iCheck('check');
        $("#ImageIndex").val("");
        $("#IsEnabledCheck").iCheck('check');
        $("#AllowDeleteCheck").iCheck('check');

        //编号可编辑
        $("#ControlID").prop("readonly", false);
        //主动赋值ID
        $("#ID").val(0);
    }

    function Edit() {

        var items = $("#dgMain").treegrid('getSelections');

        if (items && items.length > 0) {
            success3.hide();
            error3.hide();
            $('#fm')[0].reset()
            $('#responsive').modal('show').find(".modal-title").html("编辑功能插件");

            var item = items[0];

            for (var filed in item) {
                $("#" + filed).val(item[filed]);
            }

            $("#iconSample").attr("class", item.ImageIndex);

            //需要先更新状态再设置值
            $(".icheck").iCheck('update');
            //几个选项
            $("#IsMenu").iCheck((item.FunctionType == 1) ? 'check' : 'uncheck');
            $("#IsModule").iCheck((item.FunctionType == 4) ? 'check' : 'uncheck');
            $("#IsNotMenu").iCheck((item.FunctionType == 2) ? 'check' : 'uncheck');
            $("#IsFunction").iCheck((item.FunctionType == 3) ? 'check' : 'uncheck');

            $("#IsEnabledCheck").iCheck(item.IsEnabled ? 'check' : 'uncheck');
            $("#AllowDeleteCheck").iCheck(item.IsCanDelete ? 'check' : 'uncheck');

            //上级模块
            $("#PID").jsTreeCheck("setValue", item.PID);

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

    function rebuildFunctionCombo() {
        //加载父节点
        $.getJSON(currentUrl + "AlljsTreeData", {}, function (json) {
            var rootItem = NoRainTools.deepcopy(json[0]);
            rootItem["id"] = "-1";
            rootItem["name"] = "系统根目录";
            rootItem.icon = "icon-home";
            rootItem.text = "系统根目录";
            rootItem.children = json;
            rootItem.state = { opened: true };
            $("#PID").jsTreeCheck({}, rootItem);
        });
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
                var isAllCanDelete = true;
                $.each(rows, function (rowIndex, rowData) {
                    ids.push(rowData.ID);
                    if (!rowData.IsCanDelete) {
                        isAllCanDelete = false;
                        return false;
                    }
                });
                if (!isAllCanDelete) {
                    toastr.error('有不可删除项，请检查后重试!');
                    return;
                }

                Metronic.blockUI({ target: '#page-content', message: "删除中……" });

                $.CommonAjax({
                    url: currentUrl + "DeleteSome/" + ids.join(","),
                    type: "delete",
                    success: function (data, textStatus) {
                        Metronic.unblockUI('#page-content');
                        toastr.success('删除成功!');
                        //   rebuildFunctionCombo();
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

        NoRainTools.SetFormArrayValue(data, "IsEnabled", $("#IsEnabledCheck").prop("checked"));
        NoRainTools.SetFormArrayValue(data, "IsCanDelete", $("#AllowDeleteCheck").prop("checked"));
        NoRainTools.SetFormArrayValue(data, "PID", $("#PID").attr("selectedvalue"));
        //阻塞页面，防止再次提交
        Metronic.blockUI({
            target: '#responsive-content',
            message: "提交中……"
        });

        $.CommonAjax({
            targetBlock: '#responsive-content',
            url: currentUrl,
            type: type,
            data: data,
            success: function (data, textStatus) {
                reloadDataGrid();
                toastr.success('提交成功!')
                Metronic.unblockUI('#responsive-content');
                $('#responsive').modal('hide');
                // rebuildFunctionCombo();
            }
        });
    }


    //重新加载dataGrid
    function reloadDataGrid() {
        $("#dgMain").treegrid('reload');
        $("#dgMain").treegrid('unselectAll');
    }

    // 重新布局
    function resize() {
        var width = $(".dg-panel").width();
        if (width < vars.minTableWidth) width = vars.minTableWidth;
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
