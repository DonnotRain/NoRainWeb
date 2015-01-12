PlugFunctions.Sale.Index = function () {

    var currentUrl = rootPath + "/API/StatisticsSale/RealTimeAmount";
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
                { field: 'Code', title: '商品编号', width: 100, align: 'center' },
                { field: 'Name', title: '商品名称', width: 100, align: 'center' },
                {
                    field: 'TypeName', title: '商品类型', width: 60, align: 'center'
                },
                {
                    field: 'Total', title: '总销售金额', width: 50, align: 'center', formatter: function (value, row) {
                        return "￥" + value;
                    }
                },
                { field: 'TotalCount', title: '销售笔数', width: 50, align: 'center' }
                ]]
                , onBeforeLoad: function (para) {
                    para.beginDate = $("#conditionBeginTime").val();
                    para.endDate = $("#conditionEndTime").val();
                    para.Name = $("#conditionName").val();
                    para.Code = $("#conditionCode").val();
                    para.Type = $("#conditionType").val() || "";
                    para.StoreName = $("#conditionStoreName").val();

                }
                , loadFilter: function (data) {
                    data.footer[0].TypeName = "<span style='color:#ff0000;font-size:14px;'>总计:</span>";
                    return data;

                }
                , onLoadSuccess: function (data) {
                    //加载图表
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
        }
    }

    function LoadCharts(data) {
        var items = data.rows;

        var xAxis = [];
        var series = [];
        //找到全部类型，并加入到X轴
        for (var i = 0; i < items.length; i++) {
            var hasAxis = false;    //是否已存在此商品对应类型
            for (var tempIndex = 0; tempIndex < xAxis.length; tempIndex++) {
                if (xAxis[tempIndex] == items[i].TypeName) {
                    hasAxis = true;
                    break;
                }
            }

            //如果不存在，加入
            if (!hasAxis) {
                xAxis.push(items[i].TypeName);
            }
        }


        for (var i = 0; i < items.length; i++) {
            var typeIndex = 0;  //此类型序号
            //生成数据,当前类型的序号才有数值
            var data = [];

            for (var tempIndex = 0; tempIndex < xAxis.length; tempIndex++) {

                if (xAxis[tempIndex] == items[i].TypeName) {
                    typeIndex = tempIndex;
                    data.push(items[i].Total)
                } else {
                    data.push(0);
                }
            }

            //找到序号以后，生成serie
            var serie = {
                name: items[i].Name,
                data: data,
            };
            series.push(serie);
        }

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
                text: '各产品类型和产品销量统计图'
            },

            xAxis: {
                categories: xAxis
            },

            yAxis: {
                allowDecimals: false,
                min: 0,
                title: {
                    text: '销售额(￥)'
                }
            },

            tooltip: {
                headerFormat: '<b>{point.key}</b><br>',
                pointFormat: '<span style="color:{series.color}">\u25CF</span> {series.name}:  ￥{point.y} / ￥{point.stackTotal}'
            },

            plotOptions: {
                column: {
                    stacking: 'normal',
                    depth: 40
                }
            },

            series: series
        });
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



