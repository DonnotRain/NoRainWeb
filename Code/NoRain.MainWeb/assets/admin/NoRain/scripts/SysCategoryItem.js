var SysCategoryItem = function () {

    //主表单相关变量
    var _itemCRUD = {
        url: rootPath + "/API/CategoryItem/",
        formMain: $("#fmItem"),
        formError: $('.alert-danger', $("#fmItem")),
        formSuccess: $('.alert-success', $("#fmItem")),
        formDialog: $('#itemDlg'),
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
                src: $("#tableItem"),
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
                dataTable: {
                    "orderClasses": false,
                    "bProcessing": true,
                    "serverSide": true,
                    "bStateSave": true,

                    "columnDefs": [
                           {
                               "searchable": false,
                               "orderable": false,
                               "targets": [0], "mData": "Id", "mRender": function (data, type, full) {
                                   return '<input type="checkbox" name="Id[]" value="' + data + '">';
                               }
                           },
                        { "aTargets": ["Code"], "mData": "Code" },
                       { "aTargets": ["ItemContent"], "mData": "ItemContent" },
                             { "aTargets": ["Description"], "mData": "Description" },
                        {
                            "aTargets": ["IsEnabled"], "mData": "IsEnabled", "mRender": function (data, type, full) {
                                return data ? '<span class="label label-sm label-success">已启用</span>' : '<span class="label label-sm label-danger">未启用</span>';
                            }
                        }
                    ],
                    "lengthMenu": [
                        [10, 20, 50, 100, 150, -1],
                        [10, 20, 50, 100, 150, "全部"] // change per page values here
                    ],
                    "pageLength": 10, // default record count per page
                    "ajax": {
                        "url": rootPath + "/API/CategoryItem/DataTablePager", // ajax source
                        "type": "GET" // request type
                    },
                    "order": [
                        [1, "asc"]
                    ]
                }
            });
            $('#tableItem tbody').on('click', 'tr', function () {
                $(this).toggleClass("selected");
                var checked = false;
                //先判断有没有被选中
                if ($(this).hasClass("selected")) checked = true;
                var checkEle = $(this).find('td:nth-child(1) input[type="checkbox"]');
                checkEle.attr("checked", checked);
                $.uniform.update(checkEle);
            });

            vars.mainGrid = grid;
        },
        initialValidate: function () {
            //IMPORTANT: update CKEDITOR textarea with actual content before submit
            _itemCRUD.formMain.on('submit', function () {
                for (var instanceName in CKEDITOR.instances) {
                    CKEDITOR.instances[instanceName].updateElement();
                }
            })

            _itemCRUD.formMain.validate({
                errorElement: 'span', //default input error message container
                errorClass: 'help-block help-block-error', // default input error message class
                focusInvalid: false, // do not focus the last invalid input
                ignore: "", // validate all fields including form hidden input
                rules: {
                    Sort_Iten: {
                        number: true
                    }
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
                    _itemCRUD.formSuccess.hide();
                    _itemCRUD.formError.show();
                    Metronic.scrollTo(_itemCRUD.formError, -200);
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
                    _itemCRUD.formSuccess.show();
                    _itemCRUD.formError.hide();
                    form[0].submit(); // submit the form
                }
            });

            ////apply validation on select2 dropdown value change, this only needed for chosen dropdown integration.
            //$('.mineSelect2', _itemCRUD.formMain).change(function () {
            //    _itemCRUD.formMain.validate().element($(this)); //revalidate the chosen dropdown value and show error or success message for the input
            //});

            //// initialize select2 tags
            //$("#select2_tags").change(function () {
            //    _itemCRUD.formMain.validate().element($(this)); //revalidate the chosen dropdown value and show error or success message for the input 
            //}).select2({
            //    tags: ["red", "green", "blue", "yellow", "pink"]
            //});

            //initialize datepicker
            $('.date-picker').datepicker({
                rtl: Metronic.isRTL(),
                autoclose: true
            });

            $('.date-picker .form-control').change(function () {
                _itemCRUD.formMain.validate().element($(this)); //revalidate the chosen dropdown value and show error or success message for the input 
            });
        },
        initInputs: function () {
            //init date pickers
            $('.date-picker').datepicker({
                rtl: Metronic.isRTL(),
                autoclose: true
            });
            $("#Sort_Item").TouchSpin({
                buttondown_class: 'btn green',
                buttonup_class: 'btn green',
                min: -1000000000,
                max: 1000000000,
                stepinterval: 50,
                maxboostedstep: 10000000,
                prefix: ''
            });
        }
    }

    //重新加载dataGrid
    function reloadDataGrid() {
        vars.mainGrid.setAjaxParam("Name", $("#conditionName").val());
        vars.mainGrid.setAjaxParam("Value", $("#conditionValue").val());

        vars.mainGrid.getDataTable().ajax.reload();
    }

    return {
        //main function to initiate the module
        init: function () {
            Initialize.loadDataGrid();
            Initialize.initialValidate();
            Initialize.initialBtns();
            Initialize.initInputs();
        }
        , Add: function () {    //添加系统模块

            //需要获取当前选中分类
            var typeItems = window.mainGrid.getDataTable().rows('.selected').data();
            if (typeItems && typeItems.length > 0) {

                var typeItem = typeItems[0];
                //页面中显示此分类
                $("#CategoryTypeId").val(typeItem.Id);
                $("#BelongCategory").html(typeItem.Name);
                _itemCRUD.formSuccess.hide();
                _itemCRUD.formError.hide();

                _itemCRUD.formMain[0].reset()
                _itemCRUD.formDialog.modal('show').find(".modal-title").html("新增分类项 分类：" + typeItem.Name);

                //编号可编辑
                $("#Code_Item").prop("readonly", false);
                //主动赋值Id
                $("#Id_Item").val(0);
            }
            else {
                toastr.error('未选取所属分类,选中分类表格中的项即可');
                return;
            }

        }
        , Edit: function () {
            //获取选中行
            var items = vars.mainGrid.getDataTable().rows('.selected').data();
            //需要获取当前选中分类
            var typeItems = window.mainGrid.getDataTable().rows('.selected').data();
            if (typeItems && typeItems.length > 0) {

                var typeItem = typeItems[0];
                //页面中显示此分类
                $("#CategoryTypeId").val(typeItem.Id);
                $("#BelongCategory").html(typeItem.Name);
            }
            if (items && items.length > 0) {
                _itemCRUD.formSuccess.hide();
                _itemCRUD.formError.hide();
                _itemCRUD.formMain[0].reset()
                _itemCRUD.formDialog.modal('show').find(".modal-title").html("编辑分类项");

                var item = items[0];
                _itemCRUD.editItem = item;
                for (var filed in item) {
                    $("#" + filed + "_Item").val(item[filed]);
                }
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
                        ids.push(rowData.Id);
                    });

                    Metronic.blockUI({ target: '#panel-item', message: "删除中……" });

                    $.CommonAjax({
                        url: _itemCRUD.url,
                        data: { "": ids.join(",") },
                        type: "delete",
                        success: function (data, textStatus) {
                            Metronic.unblockUI('#panel-item');
                            toastr.success('删除成功!');
                            reloadDataGrid();
                        }
                    });
                }
            });
        }
        , Save: function () {
            var type = "post";
            if ($("#Id_Item").val() && $("#Id_Item").val() != 0) {
                type = "put";
            }

            //验证输入合法性
            var result = _itemCRUD.formMain.valid();
            if (!result) {
                return;
            }
            _itemCRUD.formSuccess.show();
            _itemCRUD.formError.hide();

            var data = _itemCRUD.formMain.serializeArray();
            if (type == "post") NoRainTools.SetFormArrayValue(data, "IsEnabled", 1);

            //阻塞页面，防止再次提交
            Metronic.blockUI({
                target: '#itemDlg-content',
                message: "提交中……"
            });

            $.CommonAjax({
                targetBlock: '#itemDlg-content',
                url: _itemCRUD.url,
                type: type,
                data: data,
                success: function (data, textStatus) {
                    reloadDataGrid();
                    toastr.success('提交成功!')
                    Metronic.unblockUI('#itemDlg-content');
                    _itemCRUD.formDialog.modal('hide');
                }
            });
        }
    };
}();