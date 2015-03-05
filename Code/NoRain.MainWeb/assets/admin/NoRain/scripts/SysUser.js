﻿var SysUsers = function () {

    //角色编辑相关变量
    var _roleCRUD = {
        url: window.rootPath + "/API/SysUser/",
        formMain: $("#fm"),
        formError: $('.alert-danger', $("#fm")),
        formSuccess: $('.alert-success', $("#fm")),
        formDialog: $('#mainDlg'),
        editItem: null   //正在编辑的项
    };

    //全局变量声明
    var vars = {
        minTableWidth: 720,
        mainGrid: {}
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
                loadingMessage: '加载中...',
                dataTable: {
                    "orderClasses": false,
                    "bProcessing": true,
                    "serverSide": true,
                    "bStateSave": true,
                    "orderMulti": false,
                    "sortMulti": false,
                    "columnDefs": [
                           {
                               
                               "orderable": false,
                               "targets": [0], "mData": "ID", "mRender": function (data, type, full) {
                                   return '<input type="checkbox" name="Id[]" value="' + data + '">';
                               }
                           },
                           {  "aTargets": ["Name"], "mData": "Name" },
                           {  "orderable": false, "aTargets": ["FullName"], "mData": "FullName" },
                           {
                                "orderable": false, "aTargets": ["Phone"], "mData": "MobilePhone", "mRender": function (data, type, full) {

                                   return (full.MobilePhone ? ('<span class="label label-sm label-success label-mini">移动电话</span>' + full.MobilePhone + "<br />") : "") +
                                        (full.OfficePhone ? ('<span class="label label-sm label-info label-mini">办公电话</span>' + full.OfficePhone + "<br />") : "") +
                                        (full.HomePhone ? ('<span class="label label-sm label-warning label-mini">家庭电话</span>' + full.HomePhone) : "");

                               }
                           },
                           {
                                "orderable": false, "aTargets": ["Roles"], "mData": "RoleIds", "mRender": function (data, type, row) {
                                   var roles = [];

                                   for (var i = 0; i < row.Roles.length; i++) {
                                       roles.push(row.Roles[i].Name + "<br />");
                                   }
                                   return roles.join(" ");
                               }
                           },
                                  { "orderable": false, "aTargets": ["Address"], "mData": "Address" },
                           {  "orderable": false, "aTargets": ["Email"], "mData": "Email" },
                           {
                                "orderable": false, "aTargets": ["Options"], "mData": "IsEnabled", "mRender": function (data, type, full) {
                                   return (data ? '<span class="label label-sm label-success">已启用</span>' : '<span class="label label-sm label-danger">未启用</span>') +
                                       (full.IsSuperAdmin ? '<span class="label label-sm label-success">超级管理员</span>' : '');
                               }
                           }
                    ],
                    "lengthMenu": [
                        [10, 20, 50, 100, 150, -1],
                        [10, 20, 50, 100, 150, "全部"] // change per page values here
                    ],
                    "pageLength": 20, // default record count per page
                    "ajax": {
                        "url": rootPath + "/API/SysUser/DataTablePager", // ajax source
                        "type": "GET" // request type
                    },
                    "order": [
                        [1, "asc"]
                    ]
                }
            });
            $('#tableMain tbody').on('click', 'tr', function () {
                $(this).toggleClass("selected");
                var checked = false;
                //先判断有没有被选中
                if ($(this).hasClass("selected")) checked = true;
                var checkEle = $(this).find('td:nth-child(1) input[type="checkbox"]');
                checkEle.attr("checked", checked);
                $.uniform.update(checkEle);
            });

            vars.mainGrid = grid;

            $(".form-search").submit(function (e) {
                reloadDataGrid();
                return false;
            });
        },
        initialValidate: function () {
            //IMPORTANT: update CKEDITOR textarea with actual content before submit
            _roleCRUD.formMain.on('submit', function () {
                for (var instanceName in CKEDITOR.instances) {
                    CKEDITOR.instances[instanceName].updateElement();
                }
            })

            _roleCRUD.formMain.validate({
                errorElement: 'span', //default input error message container
                errorClass: 'help-block help-block-error', // default input error message class
                focusInvalid: false, // do not focus the last invalid input
                ignore: "", // validate all fields including form hidden input
                rules: {
                    Password: {
                        minlength: 6,
                        required: true
                    },
                    PasswordConfirm: {
                        minlength: 6,
                        required: true,
                        equalTo: "#Password"
                    }
                },
                messages: { // custom messages for radio buttons and checkboxes
                    PasswordConfirm: {
                        equalTo: "密码确认错误"
                    }
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
                    _roleCRUD.formSuccess.hide();
                    _roleCRUD.formError.show();
                    Metronic.scrollTo(_roleCRUD.formError, -200);
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
                    _roleCRUD.formSuccess.show();
                    _roleCRUD.formError.hide();
                    form[0].submit(); // submit the form
                }
            });

            //apply validation on select2 dropdown value change, this only needed for chosen dropdown integration.
            $('.mineSelect2', _roleCRUD.formMain).change(function () {
                _roleCRUD.formMain.validate().element($(this)); //revalidate the chosen dropdown value and show error or success message for the input
            });

            // initialize select2 tags
            $("#select2_tags").change(function () {
                _roleCRUD.formMain.validate().element($(this)); //revalidate the chosen dropdown value and show error or success message for the input 
            }).select2({
                tags: ["red", "green", "blue", "yellow", "pink"]
            });

            //initialize datepicker
            $('.date-picker').datepicker({
                rtl: Metronic.isRTL(),
                autoclose: true
            });

            $('.date-picker .form-control').change(function () {
                _roleCRUD.formMain.validate().element($(this)); //revalidate the chosen dropdown value and show error or success message for the input 
            });
        },
        initPickers: function () {
            //init date pickers
            $('.date-picker').datepicker({
                rtl: Metronic.isRTL(),
                autoclose: true
            });

            $.CommonAjax({
                url: window.rootPath + "/API/Role/",
                type: "get",
                success: function (data) {
                    NoRainTools.LoadSelectOption($("#RoleIds"), data, "Name", "ID", false);
                    $("#RoleIds").select2({})
                }
            });
        }
    }

    //重新加载dataGrid
    function reloadDataGrid() {
        vars.mainGrid.setAjaxParam("Name", $("#conditionName").val());
        vars.mainGrid.getDataTable().ajax.reload();
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
            _roleCRUD.formSuccess.hide();
            _roleCRUD.formError.hide();

            //重置表单
            $('#fm')[0].reset();
            $("#RoleIds").select2("val", []);

            $('#mainDlg').modal('show').find(".modal-title").html("新增系统用户");

            //编号可编辑
            $("#ValueContent").prop("readonly", false);
            //主动赋值Id
            $("#Id").val(0);
        }
        , Edit: function () {
            //获取选中行
            var items = vars.mainGrid.getDataTable().rows('.selected').data();

            if (items && items.length > 0) {
                _roleCRUD.formSuccess.hide();
                _roleCRUD.formError.hide();
                $('#fm')[0].reset()
                $('#mainDlg').modal('show').find(".modal-title").html("编辑系统用户");

                var item = items[0];

                for (var filed in item) {
                    $("#" + filed).val(item[filed]);
                }
                //角色
                var roleIds = [];
                for (var i = 0; i < item.Roles.length; i++) {
                    roleIds.push(item.Roles[i].ID);
                }

                $("#Password").val("");
                $("#RoleIds").select2("val", roleIds);

            }
            else {
                toastr.error('未选取数据行,请先选取要编辑的行');
                return;
            }
        }
        , Delete: function () {
            var rows = vars.mainGrid.getDataTable().rows('.selected').data();

            if (!rows.length) {
                toastr.error('未选取数据行,请先选取要删除的行');
                return;
            }
            bootbox.confirm("确定要删除所选数据?", function (result) {
                if (result) {
                    var ids = new Array();

                    //转换伪数组
                    rows = Array.prototype.slice.call(rows);

                    $.each(rows, function (rowIndex, rowData) {
                        ids.push(rowData.ID);
                    });

                    Metronic.blockUI({ target: '#page-content', message: "删除中……" });

                    $.CommonAjax({
                        url: _roleCRUD.url,
                        data: { "": ids.join(",") },
                        type: "delete",
                        success: function (data, textStatus) {
                            Metronic.unblockUI('#page-content');
                            toastr.success('删除成功!');
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
            _roleCRUD.formSuccess.show();
            _roleCRUD.formError.hide();

            var data = $("#fm").serializeArray();
            if (type == "post") NoRainTools.SetFormArrayValue(data, "IsEnabled", 1);

            //阻塞页面，防止再次提交
            Metronic.blockUI({
                target: '#responsive-content',
                message: "提交中……"
            });

            $.CommonAjax({
                targetBlock: '#responsive-content',
                url: _roleCRUD.url,
                type: type,
                data: data,
                success: function (data, textStatus) {
                    reloadDataGrid();
                    toastr.success('提交成功!')
                    Metronic.unblockUI('#responsive-content');
                    $('#mainDlg').modal('hide');
                }
            });
        }
        , Export: function (type, page) {
            var data = vars.mainGrid.getDataTable().ajax.params();

            var setAjaxParam = function (name, value) {
                data[name] = value;
            }

            setAjaxParam("Name", $("#conditionName").val());
            setAjaxParam("Value", $("#conditionValue").val());

            if (page == "_ALL") {
                data.needPager = false;
            }
            else data.needPager = true;

            window.open(_roleCRUD.url + "ExportExcel?" + $.param(data), "_blank");
        }
        , Print: function () {

        }
    };

}();