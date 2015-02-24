var SysParameters = function () {

    //当前功能中的变量
    var currentUrl = window.rootPath + "/API/Parameter/";
    var mainForm = $("#fm")
    var error3 = $('.alert-danger', mainForm);
    var success3 = $('.alert-success', mainForm);

    //全局变量声明
    var vars = {
        minTableWidth: 720
    };

    //初始化方法
    var Initialize = {
        initialBtns: function () {

            //搜索
            $("#btnSearch").click(function () {
                reloadDataGrid();
            });
        },
        loadDataGrid: function () {

            var grid = new Datatable();

            grid.init({
                src: $("#tableMain"),
                onSuccess: function (grid) {
                    // execute some code after table records loaded
                },
                onError: function (grid) {
                    // execute some code on network or other general error  
                },
                onDataLoad: function (grid) {
                    // execute some code on ajax data load
                },
                loadingMessage: '加载中...',
                dataTable: { // here you can define a typical datatable settings from http://datatables.net/usage/options 

                    // Uncomment below line("dom" parameter) to fix the dropdown overflow issue in the datatable cells. The default datatable layout
                    // setup uses scrollable div(table-scrollable) with overflow:auto to enable vertical scroll(see: assets/global/scripts/datatable.js). 
                    // So when dropdowns used the scrollable div should be removed. 
                    //"dom": "<'row'<'col-md-8 col-sm-12'pli><'col-md-4 col-sm-12'<'table-group-actions pull-right'>>r>t<'row'<'col-md-8 col-sm-12'pli><'col-md-4 col-sm-12'>>",
                    "bProcessing": true,
                    "serverSide": true,
                    "bStateSave": true, // save datatable state(pagination, sort, etc) in cookie.
                    "aoColumnDefs": [
                           {
                               "aTargets": ['check'], "mData": "Id", "mRender": function (data, type, full) {
                                   return '<input type="checkbox" name="id[]" value="'+data+'">';
                               }
                           },
                           {
                               "aTargets": ["Name"], "mData": "Name", "mRender": function (data, type, full) {
                                   return data;
                               }
                           },
                        { "aTargets": [2], "mData": "ValueContent" },
                        {
                            "aTargets": [3], "mData": "IsEnabled", "mRender": function (data, type, full) {
                                return data;
                            }
                        }
                    ],
                    "lengthMenu": [
                        [10, 20, 50, 100, 150, -1],
                        [10, 20, 50, 100, 150, "全部"] // change per page values here
                    ],
                    "pageLength": 10, // default record count per page
                    "ajax": {
                        "url": rootPath + "/API/Parameter/DataTablePager", // ajax source
                        "type": "GET" // request type
                    },
                    "order": [
                        [1, "asc"]
                    ]// set first column as a default sort by asc
                }
            });

            // handle group actionsubmit button click
            grid.getTableWrapper().on('click', '.table-group-action-submit', function (e) {
                e.preventDefault();
                var action = $(".table-group-action-input", grid.getTableWrapper());
                if (action.val() != "" && grid.getSelectedRowsCount() > 0) {
                    grid.setAjaxParam("customActionType", "group_action");
                    grid.setAjaxParam("customActionName", action.val());
                    grid.setAjaxParam("id", grid.getSelectedRows());
                    grid.getDataTable().ajax.reload();
                    grid.clearAjaxParams();
                } else if (action.val() == "") {
                    Metronic.alert({
                        type: 'danger',
                        icon: 'warning',
                        message: 'Please select an action',
                        container: grid.getTableWrapper(),
                        place: 'prepend'
                    });
                } else if (grid.getSelectedRowsCount() === 0) {
                    Metronic.alert({
                        type: 'danger',
                        icon: 'warning',
                        message: 'No record selected',
                        container: grid.getTableWrapper(),
                        place: 'prepend'
                    });
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

            ////apply validation on select2 dropdown value change, this only needed for chosen dropdown integration.
            //$('.mineSelect2', mainForm).change(function () {
            //    mainForm.validate().element($(this)); //revalidate the chosen dropdown value and show error or success message for the input
            //});

            //// initialize select2 tags
            //$("#select2_tags").change(function () {
            //    mainForm.validate().element($(this)); //revalidate the chosen dropdown value and show error or success message for the input 
            //}).select2({
            //    tags: ["red", "green", "blue", "yellow", "pink"]
            //});

            //initialize datepicker
            $('.date-picker').datepicker({
                rtl: Metronic.isRTL(),
                autoclose: true
            });

            $('.date-picker .form-control').change(function () {
                mainForm.validate().element($(this)); //revalidate the chosen dropdown value and show error or success message for the input 
            });
        },
        initPickers: function () {
            //init date pickers
            $('.date-picker').datepicker({
                rtl: Metronic.isRTL(),
                autoclose: true
            });
        }
    }

    //重新加载dataGrid
    function reloadDataGrid() {
        //$("#dgMain").treegrid('reload');
        //$("#dgMain").treegrid('unselectAll');
    }

    return {
        //main function to initiate the module
        init: function () {
            Initialize.loadDataGrid();
            Initialize.initialValidate();
            Initialize.initialBtns();
            Initialize.initPickers();
        }
        , Add: function () {    //添加系统模块
            success3.hide();
            error3.hide();
           
            $('#fm')[0].reset()
            $('#mainDlg').modal('show').find(".modal-title").html("新增系统参数");
                      
            //编号可编辑
            $("#ValueContent").prop("readonly", false);
            //主动赋值ID
            $("#ID").val(0);
        }
        , Edit: function () {

            var items = $("#dgMain").treegrid('getSelections');

            if (items && items.length > 0) {
                success3.hide();
                error3.hide();
                $('#fm')[0].reset()
                $('#mainDlg').modal('show').find(".modal-title").html("编辑功能插件");

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
        , Delete: function () {
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
        , Save: function () {
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
                target: '#mainDlg-content',
                message: "提交中……"
            });

            $.CommonAjax({
                targetBlock: '#mainDlg-content',
                url: currentUrl,
                type: type,
                data: data,
                success: function (data, textStatus) {
                    reloadDataGrid();
                    toastr.success('提交成功!')
                    Metronic.unblockUI('#mainDlg-content');
                    $('#mainDlg').modal('hide');
                    // rebuildFunctionCombo();
                }
            });
        }
        // 搜索或刷新
        //, Search: function () {
        //    reloadDataGrid();
        //}
    };

}();