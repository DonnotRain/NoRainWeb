PlugFunctions.Sale.Leave = function () {

    var currentUrl = rootPath + "/API/StatisticsTrainingRecord/";
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
                showFooter: true,
                columns: [[
                { field: 'UserName', title: '销售人员', width: 100, align: 'center' },
                {
                    field: 'ExamCount', title: '考试次数', width: 60, align: 'center'
                }
                ]]
                , onBeforeLoad: function (para) {
                    para.beginDate = $("#conditionBeginTime").val();
                    para.endDate = $("#conditionEndTime").val();
                    para.userId = $("#conditionUserName").combobox('getValue');
                    para.corpCode = getCookie("CorpCode");
                }
                , loadFilter: function (data) {
                    if (data.footer.length > 0) {
                        data.footer[0].UserName = "<span style='color:#ff0000;font-size:14px;'>总计:</span>";
                    }

                    return data;
                }
                , onLoadSuccess: function (data) {
                    LoadCharts(data);
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
        },
        initialCombobox: function () {
            $("#conditionUserName").combobox({
                url: rootPath + "/api/user/GetAll",
                method: 'get',
                textField: 'UserName',
                valueField: 'ID'
            });
        }
    }
    //初始化
    function InitialFunc() {
        Initialize.initialCombobox();
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

    function LoadCharts(data) {
        var items = data.rows;

        var xAxis = [];
        var series = [];
        //找到全部类型，并加入到X轴
        for (var i = 0; i < items.length; i++) {
            //xAxis.push(items[i].UserName);
            var hasAxis = false;    //是否已存在此商品对应类型
            for (var tempIndex = 0; tempIndex < xAxis.length; tempIndex++) {
                if (xAxis[tempIndex] == items[i].UserName) {
                    hasAxis = true;
                    break;
                }
            }

            //如果不存在，加入
            if (!hasAxis) {
                xAxis.push(items[i].UserName);
            }
        }

        var data = [];
        for (var i = 0; i < items.length; i++) {
            for (var tempIndex = 0; tempIndex < xAxis.length; tempIndex++) {
                if (xAxis[tempIndex] == items[i].UserName) {
                    data.push(items[i].ExamCount);
                }
            }
        }

        series.push({
            name: "考试次数",
            data: data
        });

        $('#container').highcharts({

            chart: {
                type: 'column',
                options3d: {
                    enabled: true,
                    alpha: 5,
                    beta: 5,
                    viewDistance: 25,
                    depth: 40
                },
                marginTop: 80,
                marginRight: 40
            },

            title: {
                text: '考试统计表'
            },

            xAxis: {
                categories: xAxis
            },

            yAxis: {
                allowDecimals: false,
                min: 0,
                title: {
                    text: '请假时间(小时)'
                }
            },

            tooltip: {
                headerFormat: '<b>{point.key}</b><br>',
                pointFormat: '<span style="color:{series.color}">\u25CF</span> {series.name}: {point.y}'
            },

            plotOptions: {
                column: {
                    depth: 40
                }
            },

            series: series
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



