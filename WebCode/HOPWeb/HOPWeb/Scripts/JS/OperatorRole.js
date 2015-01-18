PlugFunctions.System.SysUserRole = function () {
    var rootPath = getRootPath();
    var components = ['parser', 'validatebox', 'datagrid', 'combobox', 'form', 'dialog', 'messager', 'layout', "datagrid", "combotree", "treegrid"];
    //初始化
    function initialFunc() {
        initLayout();
        role.init();
        SysUser.init();
        auth.init();
    }

    function initLayout() {
        var width = $(".dg-panel").width();
        var height = $(window).height() - 140;
        $(".easyui_layout").width(width);
        $(".easyui_layout").height(height);
        $(".easyui_layout").show();
        $(".easyui_layout").layout({
            fit: true,
            width: width,
            height: height
        });
    }

    // 重新布局
    function resize() {
        var width = $(".dg-panel").width();
        var height = $(window).height() - 140;
        $(".easyui_layout").width(width);
        $(".easyui_layout").height(height);
        $(".easyui_layout").layout('resize');
    }

    var role = function () {
        var currentUrl = rootPath + "/API/Role/";

        function initMainDg() {
            $('#dgRole').datagrid({
                method: "get",
                url: currentUrl,
                singleSelect: true,
                pagination: true,
                striped: true,
                fit: true,
                toolbar: "#RoleTool",
                fitColumns: true,
                columns: [
                    [
                        { field: 'ck', checkbox: true },
                         { field: 'Name', title: '角色名称', width: 140, align: 'center' },
                        { field: 'Note', title: '备注', width: 140, align: 'center' }
                    ]
                ],
                onSelect: function (rowData) {
                    $('#dgSysUser').datagrid("load");
                    $('#dgAuth').treegrid("load");
                }
                , onBeforeLoad: function (para) {
                    para.name = "";
                }
            });

            var p = $('#dgRole').datagrid('getPager');
            $(p).pagination({
                showPageList: false,
                showRefresh: false,
                displayMsg: ''
            });
        };

        function initialDialog() {
            $('#dlgRole').show();
            $("#dlgRole").dialog({
                title: "新增角色",
                modal: true,
                closed: true,
                buttons: [
                    {
                        text: '保存',
                        handler: function () {
                            save();
                        }
                    }, {
                        text: '取消',
                        handler: function () {
                            $('#dlgRole').dialog('close');
                        }
                    }
                ]
            });
        };

        function initialValidate() {
            $("#fmRole").validVal({ language: "cn" });
        }

        function save() {

            //验证输入合法性
            var isValid = $("#fmRole").triggerHandler("submitForm");
            if (!isValid) {
                //  $.messager.alert("输入信息错误", "信息输入不完整", 'info');
                return;
            }
            var method = "post";
            if ($("#RoleId").val()) {
                method = "put";
            }
            var data = $("#fmRole").serializeArray();

            $.jMask("save", "保存中，请稍后").show();
            $.CommonAjax({
                url: currentUrl,
                type: method,
                data: data,
                success: function (backdata, textStatus) {
                    $.jMask("save").hide();

                    $('#dlgRole').dialog('close');
                    $.gritter.add({
                        // (string | mandatory) the heading of the notification
                        title: '成功!',
                        // (string | mandatory) the text inside the notification
                        text: '保存成功。',
                        class_name: 'gritter-success',
                        time: 3000
                    });
                    $("#dgRole").datagrid("reload");

                }
            });
        }

        return {
            init: function () {
                initLayout();
                initMainDg();
                initialDialog();
                initialValidate();
                $(window).resize(resize);
            },
            Add: function () {
                $('#fmRole').form('clear');
                $('#dlgRole').dialog('open').dialog('setTitle', '添加角色');

            },
            Edit: function () {

                var item = $("#dgRole").datagrid("getSelected");
                if (item) {
                    var rowIndex = $("#dgRole").datagrid("getRowIndex", item);
                    $("#dgRole").datagrid("clearSelections").datagrid("selectRow", rowIndex);
                } else {
                    $.messager.alert("错误", "未选中任何记录", "error");
                    return;
                }

                $('#fmRole').form('clear');

                //   $('#fm').form('load', item);
                for (var filed in item) {
                    $("#" + filed).val(item[filed]);
                }

                $("#RoleId").val(item.ID);

                $('#dlgRole').dialog('open').dialog('setTitle', '编辑角色');
            },
            Delete: function () {
                var rows = $("#dgRole").datagrid('getChecked');
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
                                $("#dgRole").datagrid("reload");
                            }
                        });
                    }
                });
            },
            reload: function () {
                $("#dgRole").datagrid("reload");
            },
            Authorize: function () {

            }
        };
    }();

    var SysUser = function () {
        var currentUrl = rootPath + "/API/SysUser/";

        function initMainDg() {
            $('#dgSysUser').datagrid({
                method: "get",
                url: currentUrl + "GetPager",
                pageList: [10, 20, 30],
                singleSelect: false,
                pagination: true,
                striped: true,
                fit: true,
                toolbar: "#SysUserTool",
                fitColumns: true,
                idField: 'Id',
                treeField: 'Content',
                columns: [[
                      { field: 'ck', checkbox: true },
                      { field: 'Name', title: '登录名', width: 140, align: 'left' },
                      {
                          field: 'FullName', title: '姓名', width: 140, align: 'center', formatter: function (value, row) {
                              if (!value) {
                                  return value;
                              }
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      },
                      {
                          field: 'Address', title: '地址', width: 140, align: 'center', formatter: function (value, row) {
                              if (!value) {
                                  return value;
                              }
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      },
                      {
                          field: 'MobilePhone', title: '电话', width: 140, align: 'center', formatter: function (value, row) {
                              if (!value) {
                                  return value;
                              }
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      },
                      {
                          field: 'Roles', title: '所属角色', width: 140, align: 'center', formatter: function (value, row) {
                              var result = [];
                              for (var i = 0; i < value.length; i++) {
                                  result.push(value[i].Name);
                              }
                              var resultStr = result.join(",");
                              return "<span title='" + resultStr + "'>" + resultStr + "</span>";
                          }
                      }
                ]],
                onBeforeLoad: function (param) {
                    var item = $("#dgRole").datagrid("getSelected");
                    param.name = "";
                    if (!item) {
                        param.roleId = "";
                    }
                    else {
                        param.roleId = item.ID;
                    }
                }
               , onLoadSuccess: function (data) {
                   var panel = $(this).datagrid("getPanel");
                   panel.find(".error-info").remove();
                   if (!data.rows || data.rows.length < 1) {
                       var emptyText = $('<div class="error-info" style="width:100%;color:red;position: relative;text-align: center;height: 30px;vertical-align: middle;padding-top: 4px;font-size: 14px;"></div>')
                               .html("暂无或无法获取信息");
                       emptyText.insertAfter(panel.find(".datagrid-view"));
                   }
               },
                onLoadError: function () {
                    var panel = $(this).datagrid("getPanel");
                    panel.find(".error-info").remove();

                    var emptyText = $('<div class="error-info" style="width:100%;color:red;position: relative;text-align: center;height: 30px;vertical-align: middle;padding-top: 4px;font-size: 14px;"></div>')
                            .html("获取数据出错");
                    emptyText.insertAfter(panel.find(".datagrid-view"));

                }
            });

            //var p = $('#dgSysUser').datagrid('getPager');
            //$(p).pagination({
            //    showPageList: false,
            //    showRefresh: false,
            //    displayMsg: ''
            //});
        };

        function initLayout() {
            var width = $(".dg-panel").width();
            var height = $(window).height() - 140;
            $(".easyui_layout").width(width);
            $(".easyui_layout").height(height);
            $(".easyui_layout").show();
            $(".easyui_layout").layout({
                width: width,
                height: height
            });
        }

        function initialDialog() {
            $('#dlgSysUser').show();
            $("#dlgSysUser").dialog({
                title: "新增操作员",
                modal: true,
                closed: true,
                buttons: [{
                    text: '保存',
                    handler: function () {
                        save();
                    }
                }, {
                    text: '取消',
                    handler: function () {
                        $('#dlgSysUser').dialog('close');
                    }
                }]
            });
        };

        function initParentCombo(selectedIds) {
            $("#SysUserRoleIDs").combobox({
                url: rootPath + "/API/Role/",
                valueField: 'ID',
                textField: 'Name',
                method: "get",
                editable: false,
                width: $("#SysUserRoleIDs").width(),
                multiple: true,
                onLoadSuccess: function () {
                    if (selectedIds && selectedIds.length) {
                        $("#SysUserRoleIDs").combobox("setValues", selectedIds);
                    }
                }
            });
        }

        function initialValidate() {
            $("#fmSysUser").validVal({ language: "cn" });
        }

        function save() {

            //验证输入合法性
            var isValid = $("#fmSysUser").triggerHandler("submitForm");
            if (!isValid) {
                //  $.messager.alert("输入信息错误", "信息输入不完整", 'info');
                return;
            }

            $("#IsExpire").val($("#IsEnabledCheck").prop("checked"))
            $("#IsSuperAdmin").val($("#SuperAdmin").prop("checked"))

            var method = "post";
            if ($("#SysUserID").val()) {
                method = "put";
            }
            var data = $("#fmSysUser").serializeArray();

            $.jMask("save", "保存中，请稍后").show();
            $.CommonAjax({
                url: currentUrl,
                type: method,
                data: data,
                success: function (data, textStatus) {
                    $.jMask("save").hide();

                    $('#dlgSysUser').dialog('close');
                    $.gritter.add({
                        // (string | mandatory) the heading of the notification
                        title: '成功!',
                        // (string | mandatory) the text inside the notification
                        text: '保存成功。',
                        class_name: 'gritter-success',
                        time: 3000
                    });
                    $("#dgSysUser").datagrid("reload");

                }
            });

        }
        return {
            init: function () {
                initMainDg();
                initialDialog();
                //  initLayout();
                initialValidate();
                //initParentCombo();
                // $(window).resize(resize);
            },
            Add: function () {
                $('#fmSysUser').form('clear');

                var item = $("#dgRole").datagrid("getSelected");
                if (item) {
                    var rowIndex = $("#dgRole").datagrid("getRowIndex", item);
                    $("#dgRole").datagrid("clearSelections").datagrid("selectRow", rowIndex);
                    initParentCombo([item.ID]);
                }
                else {
                    initParentCombo([]);
                }

                $('#dlgSysUser').dialog('open').dialog('setTitle', '添加操作员');
            },
            Edit: function () {

                var item = $("#dgSysUser").datagrid("getSelected");
                if (item) {
                    var rowIndex = $("#dgSysUser").datagrid("getRowIndex", item);
                    $("#dgSysUser").datagrid("clearSelections").datagrid("selectRow", rowIndex);
                }
                else {
                    $.messager.alert("错误", "未选中任何记录", "error");
                    return;
                }
                $('#fmSysUser').form('clear');

                initParentCombo(item.RoleIds);
                $('#fmSysUser').form('load', item);
                for (var filed in item) {
                    $("#" + filed).val(item[filed]);
                }

                $('#dlgSysUser').dialog('open').dialog('setTitle', '编辑操作员');
            },
            Delete: function () {

            },
            reload: function () {
                $("#dgSysUser").datagrid("load");
            }
        };

    }();

    var auth = function () {
        var currentUrl = rootPath + "/API/Function/";

        var initMainDg = function () {
            $("#dgAuth").treegrid({
                url: currentUrl + "GetByRole",
                method: "get",
                pagination: false,
                rownumbers: true,
                fitColumns: true,
                nowrap: false,
                singleSelect: false,
                toolbar: "#AuthorizeTool",
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
                onBeforeLoad: function (row, param) {
                    var item = $("#dgRole").datagrid("getSelected");
                    if (!item) {
                        return false;
                    }
                    param.RoleId = item.ID;
                }
            });

        };

        function initialDialog() {
            //$('#dlgSysUser').show();
            //$("#dlgSysUser").dialog({

            //});
        };

        function initTree(roleId) {
            $("#treeMain").tree({
                url: rootPath + "/api/RoleFunctions?roleId=" + roleId,
                method: "get",
                animate: true,
                checkbox: true,
                cascadeCheck: false
            });
        }

        return {
            init: function () {
                //  initTree();
                initMainDg();
                initialDialog();
            },
            select: function () {

                var item = $("#dgRole").datagrid("getSelected");
                if (!item) {
                    $.Show({ message: "请先选择角色" });
                    return false;
                }

                initTree(item.ID);
                $("#dlgFunction").dialog("open");
            },
            save: function () {
                var nodes = $('#treeMain').tree('getChecked');
                var item = $("#dgRole").datagrid("getSelected");
                var data = "RoleId=" + item.ID;

                for (var i = 0; i < nodes.length; i++) {
                    data += "&FunctionId=" + nodes[i].id;
                }
                $.jMask("save", "保存中，请稍后").show();
                $.CommonAjax({
                    url: rootPath.concat("/Api/Role", "/SetRoleFunctions"),
                    type: "post",
                    data: data,
                    success: function (data, textStatus) {
                        $.jMask("save").hide();
                        $.gritter.add({
                            // (string | mandatory) the heading of the notification
                            title: '成功!',
                            // (string | mandatory) the text inside the notification
                            text: '保存成功。',
                            class_name: 'gritter-success',
                            time: 3000
                        });
                        $("#dlgFunction").dialog('close');
                        $("#dgAuth").treegrid("reload");
                    }
                });
            },
            reload: function () {
                $("#dgAuth").treegrid("reload");
            }
        };

    }();

    return {
        init: function () {
            easyloader.locale = "zh_CN";
            easyloader.theme = "metro";
            easyloader.load(components, initialFunc);
        },
        role: role,
        SysUser: SysUser,
        auth: auth
    };
}();