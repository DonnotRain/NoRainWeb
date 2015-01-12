PlugFunctions.Sale.Index = function () {

    var currentUrl = rootPath + "/API/Sale/";
    //全局变量声明
    var vars = {
        totalTimeout: 120 * 1000
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
                checkOnSelect: false,
                selectOnCheck: false,
                singleSelect: true,
                columns: [[
                { field: 'Code', title: '商品编号', width: 100, align: 'center' },
                { field: 'Name', title: '商品名称', width: 100, align: 'center' },
                {
                    field: 'TypeName', title: '商品类型', width: 60, align: 'center'
                },
                {
                    field: 'StoreName', title: '销售门店', width: 100, align: 'center'
                },
                {
                    field: 'Time', title: '销售时间', width: 120, align: 'center', formatter: timeFormatter
                },
                { field: 'Creator', title: '促销员名称', width: 100, align: 'center' },
                {
                    field: 'TotalPrice', title: '销售金额', width: 50, align: 'center', formatter: function (value, row) {
                        return "￥" + value;
                    }
                }
                ]]
                , onBeforeLoad: function (para) {
                    para.beginDate = $("#conditionBeginTime").val();
                    para.endDate = $("#conditionEndTime").val();
                    para.Name = $("#conditionName").val();
                    para.Code = $("#conditionCode").val();
                    para.Type = $("#conditionType").val() || "";
                    para.StoreName = $("#conditionStoreName").val();

                }
            });
        },
        initialDialog: function () {
            $('#dlgMain').show();
            $("#dlgMain").dialog({
                title: "查看地图",
                modal: true,
                closed: true,
                buttons: [{
                    text: '确定',
                    handler: function () {
                        $('#dlgMain').dialog('close');
                    }
                }]
            });

        },
        initialValidate: function () {
            $("#fm").validVal({ language: "cn" });
        }
    }
    //初始化
    function InitialFunc() {
        Initialize.initialDialog();
        Initialize.loadDataGrid();
        Initialize.initialValidate();

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
            easyloader.load(['parser', 'validatebox', 'datagrid', 'combobox', 'form', 'dialog', 'messager'], InitialFunc);
        }
       , reloadDataGrid: reloadDataGrid
    };
}();



