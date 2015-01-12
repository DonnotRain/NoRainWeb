var AttendanceStatistics = function () {

    var currentUrl = rootPath + "/API/StatisticsAttendence";
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
                { field: 'Code', title: '销售员编号', width: 100, align: 'center' },
                { field: 'UserName', title: '销售员姓名', width: 100, align: 'center' },
                {
                    field: 'Time', title: '统计时间', width: 100, align: 'center', formatter: function (value, row, index) {
                        if (row.TYear != null && row.TMonth != null) {
                            return row.TYear + '-' + row.TMonth;
                        } else {
                            return "";
                        }
                    }
                },
                {
                    field:'Wage',title:'基本工资',width:100,align:'center'
                },
                {
                    field: 'LateTimes', title: '迟到次数', width: 100, align: 'center', formatter: function (value) {
                        if (value == null) {
                            return 0;
                        } else {
                            return value;
                        }
                    }
                },
                {
                    field: 'EarlyTimes', title: '早退次数', width: 100, align: 'center', formatter: function (value) {
                        if (value == null) {
                            return 0;
                        } else {
                            return value;
                        }
                    }
                },
                {
                    field: 'ActualWage', title: '实发基本工资', width: 50, align: 'center', formatter: function (value, row) {
                        if (value == null && row != null) {
                            return "￥" + (parseInt(row.Wage) - parseInt(row.LateTimes == null ? 0 : row.LateTimes) * row.LateDeductMoney - parseInt(row.EarlyTimes == null ? 0 : row.EarlyTimes) * row.EarlyDeductMoney);
                        }
                        else {
                            return "￥" + value;
                        }
                    }
                }
                ]]
                , onBeforeLoad: function (para) {
                    para.userId = $("#conditionUser").combobox('getValue');
                    var timeValue = '';
                    if ($("#conditionOption").combobox('getValue') == 'year') {
                        timeValue = $("#yearBeginTime").val();
                    } else {
                        timeValue = $("#monthBeginTime").val();
                    }
                    para.time = timeValue === undefined ? "" : timeValue;

                }
                , loadFilter: function (data) {
                    if (data.footer.length > 0) {
                        data.footer[0].Wage = "<span style='color:#ff0000;font-size:14px;'>总计:</span>";
                    }
                    return data;

                }
                , onLoadSuccess: function (data) {
                    //加载图表
                    LoadCharts(data);
                }
            });
        },
        initialValidate: function () {
            $("#fm").validVal({ language: "cn" });
        },
        initialCombobox: function () {
            $("#conditionOption").combobox({
                onChange: function (newValue, oldValue) {
                    if (newValue == "year") {
                        $("#yearSpan").show();
                        $("#monthSpan").hide();
                    } else {
                        $("#yearSpan").hide();
                        $("#monthSpan").show();
                    }
                }
            });

            $("#conditionUser").combobox({
                url: rootPath + '/api/user/GetAll',
                method: 'get',
                textField: 'UserName',
                valueField: 'ID',
                disabled: false,
                multiple: false
            });
        }
    }

    function LoadCharts(data) {
        var items = data.rows;

        var xAxis = [];
        var series = [];
        //找到全部类型，并加入到X轴
        for (var i = 0, elem; (elem = items[i]) != null; i++) {
            var repeat = 0;
            var dateTime = elem.TYear + '-' + elem.TMonth;
            for (var j = 0; j < xAxis.length; j++) {
                if (xAxis[j] == dateTime) {
                    repeat = 1;
                    break;
                }
            }
            if (repeat) {
                continue;
            }
            else {
                xAxis.push(dateTime);
            }
        }

        // 构建数组容器
        var data = [];
        for (var i = 0, elem ; (elem = items[i]) != null; i++) {
            var repeat = 0;
            var userName = elem.UserName;
            for (var j = 0; j < data.length; j++) {
                if (data[j].name == userName) {
                    repeat = 1;
                    break;
                }
            }
            if (repeat) {
                continue;
            } else {
                var tempData = {};
                tempData.name = userName;
                tempData.data = [];
                for (var k = 0; k < xAxis.length; k++) {
                    tempData.data.push(null);
                }
                
                data.push(tempData);
            }
        }

        for (var i = 0,elem1; (elem1 = items[i])!=null; i++) {
            for (var j = 0, elem2; (elem2 = data[j]) != null; j++) {
                // 找到对应的名称的数据项
                if (elem1.UserName == elem2.name) {
                    var totalWage = parseInt(elem1.Wage || 0) - parseInt(elem1.LateTimes || 0) * elem1.LateDeductMoney - parseInt(elem1.EarlyTimes || 0) * elem1.EarlyDeductMoney;
                    var dateTime = elem1.TYear + "-" + elem1.TMonth;
                    // 找到对应数组的数据位置
                    for (var k = 0, elem3; (elem3 = xAxis[k]) != null; k++) {
                        if (elem3 == dateTime) {
                            elem2.data[k] = totalWage;
                        }
                    }
                    //elem2.data.push(totalWage);
                }
            }
        }

        $('#container').highcharts({

            chart: {
                type: 'column', 
                marginTop: 80,
                marginRight: 40
            },

            title: {
                text: '销售员实发基本工资统计图'
            },

            xAxis: {
                categories: xAxis
            },

            yAxis: {
                allowDecimals: false,
                min: 0,
                title: {
                    text: '实发基本工资(￥)'
                }
            },
            credits: {
                enabled:false
            },
            tooltip: {
                headerFormat: '<b>{point.key}</b><br>',
                pointFormat: '<span style="color:{series.color}">\u25CF</span> {series.name}: ￥{point.y}'
            },

            plotOptions: {
                pointPadding: 0.2,
                borderWidth: 0
            },

            series: data
        });
    }
    //初始化
    function InitialFunc() {
        Initialize.initialCombobox();
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
            easyloader.load(['parser', 'validatebox', 'datagrid', 'combobox', 'form', 'dialog', 'messager','datebox'], InitialFunc);
        }
       , reloadDataGrid: reloadDataGrid
    };
}();



