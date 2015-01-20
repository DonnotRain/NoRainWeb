
jQuery(function ($) {
    PlugFunctions.Dashboard.Init();

    $('.easy-pie-chart.percentage').each(function () {
        var $box = $(this).closest('.infobox');
        var barColor = $(this).data('color') || (!$box.hasClass('infobox-dark') ? $box.css('color') : 'rgba(255,255,255,0.95)');
        var trackColor = barColor == 'rgba(255,255,255,0.95)' ? 'rgba(255,255,255,0.25)' : '#E2E2E2';
        var size = parseInt($(this).data('size')) || 50;
        $(this).easyPieChart({
            barColor: barColor,
            trackColor: trackColor,
            scaleColor: false,
            lineCap: 'butt',
            lineWidth: parseInt(size / 10),
            animate: /msie\s*(8|7|6)/.test(navigator.userAgent.toLowerCase()) ? false : 1000,
            size: size
        });
    })

    $('.sparkline').each(function () {
        var $box = $(this).closest('.infobox');
        var barColor = !$box.hasClass('infobox-dark') ? $box.css('color') : '#FFF';
        $(this).sparkline('html', { tagValuesAttribute: 'data-values', type: 'bar', barColor: barColor, chartRangeMin: $(this).data('min') || 0 });
    });

})

PlugFunctions.Dashboard = function () {

    //私有变量和方法



    //其他信息
    function loadOtherInfo() {

    }

    //统计信息 
    function loadBaseStatic(dateType) {
        //请假
        $.CommonAjax({
            url: rootPath + "/API/Statistics/Leave?number=10",
            type: "GET",
            data: { "CorpCode": "HWMT", "dateType": dateType },
            success: function (data, textStatus) {
                $("#StaLeave").html(data.CurrentMonthAmount);
                $("#StaLeaveRate").html(data.UpPercent);

                var items = data.TopItems;
                var ele = $("#tblLeave").children("tbody");
                var htmlStr = "";

                for (var i = 0; i < items.length; i++) {
                    htmlStr += "<tr>";
                    htmlStr += "<td>" + items[i].Name + "</td>";
                    htmlStr += "<td>" + items[i].Reason + "</td>";
                    htmlStr += "<td>" + timeFormatter(items[i].BeginDate) + "</td>";
                    htmlStr += "<td>" + timeFormatter(items[i].EndDate) + "</td>";
                    htmlStr += "<td>" + items[i].Duration + "</td>";
                    htmlStr += "</tr>"
                }
                ele.html(htmlStr);
            }
        });

        //需求
        $.CommonAjax({
            url: rootPath + "/API/Statistics/Request?number=10",
            type: "GET",
            data: { "CorpCode": "HWMT", "dateType": dateType },
            success: function (data, textStatus) {
                var ele = $("#StaRequest");
                var eleRate = $("#StaRequestRate");
                ele.html(data.CurrentMonthAmount);
                eleRate.html(data.UpPercent + "%");
                if (data.UpPercent > 0) {
                    eleRate.addClass("stat-success").removeClass("stat-important");
                }
                else {
                    eleRate.addClass("stat-important").removeClass("stat-success");
                }
            }
        });

        //退货
        $.CommonAjax({
            url: rootPath + "/API/Statistics/Return?number=10",
            type: "GET",
            data: { "CorpCode": "HWMT", "dateType": dateType },
            success: function (data, textStatus) {
                $("#StaReturn").html(data.CurrentMonthAmount);
                $("#StaReturnRate").html(data.UpPercent + "%");

                if (data.UpPercent > 0) {
                    $("#StaReturnRate").parent().addClass("badge-important").removeClass("badge-success").children("i").removeClass("icon-arrow-down").addClass("icon-arrow-up");
                }
                else {
                    $("#StaReturnRate").parent().addClass("badge-success").removeClass("badge-important").children("i").removeClass("icon-arrow-up").addClass("icon-arrow-down");
                }

                var items = data.TopItems;
                var ele = $("#tblReturn").children("tbody");
                var htmlStr = "";

                for (var i = 0; i < items.length; i++) {
                    htmlStr += "<tr>";
                    htmlStr += "<td>" + items[i].Name + "</td>";
                    htmlStr += "<td>" + items[i].Amount + "</td>";
                    htmlStr += "<td>" + items[i].Reason + "</td>";
                    htmlStr += "<td>" + items[i].Creator + "</td>";
                    htmlStr += "<td>" + timeFormatter(items[i].Time) + "</td>";
                    htmlStr += "</tr>"
                }
                ele.html(htmlStr);
            }
        });

        //销售
        $.CommonAjax({
            url: rootPath + "/API/Statistics/Sale?number=10",
            type: "GET",
            data: { "CorpCode": "HWMT", "dateType": dateType },
            success: function (data, textStatus) {
                $("#StaSaleAmount").html(data.CurrentMonthAmount);
                $("#StaSaleRate").html(data.UpPercent);
                $("#StaSaleTime").html(data.CurrentMonthTime);
                $("#StaSaleTimeRate").html(data.UpPercentTime);

                if (data.UpPercent > 0) {
                    $("#StaSaleRate").parent().addClass("badge-success").removeClass("badge-important").children("i").removeClass("icon-arrow-down").addClass("icon-arrow-up");
                }
                else {
                    $("#StaSaleRate").parent().addClass("badge-important").removeClass("badge-success").children("i").removeClass("icon-arrow-up").addClass("icon-arrow-down");
                }

                if (data.UpPercentTime > 0) {
                    $("#StaSaleTimeRate").addClass("stat-success").removeClass("stat-important");
                }
                else {
                    $("#StaSaleTimeRate").addClass("stat-important").removeClass("stat-success");;
                }
            }
        });

        //培训 
        $.CommonAjax({
            url: rootPath + "/API/Statistics/Training?number=10",
            type: "GET",
            data: { "CorpCode": "HWMT", "dateType": dateType },
            success: function (data, textStatus) {
                $("#StaTraining").html(data.CurrentMonthAmount);


                var items = data.TopItems;
                var ele = $("#tblTrain").children("tbody");
                var htmlStr = "";

                for (var i = 0; i < items.length; i++) {
                    htmlStr += "<tr>";
                    htmlStr += "<td>" + (items[i].Type == '2' ? '考试' : '培训') + "</td>";
                    htmlStr += "<td>" + items[i].Title + "</td>";
                    htmlStr += "<td>" + items[i].Detail + "</td>";
                    htmlStr += "<td>" + timeFormatter(items[i].ExamTime) + "</td>";
                    htmlStr += "</tr>"
                }
                ele.html(htmlStr);
            }
        });

        //公告
        $.CommonAjax({
            url: rootPath + "/API/Statistics/Board?number=10",
            type: "GET",
            data: { "CorpCode": "HWMT", "dateType": dateType },
            success: function (data, textStatus) {
                $("#StaBoard").html(data.CurrentMonthAmount);
                $("#StaBoardRate").html(data.UpPercent);
            }
        });

        //竞争
        $.CommonAjax({
            url: rootPath + "/API/Statistics/Information?number=10",
            type: "GET",
            data: { "CorpCode": "HWMT", "dateType": dateType },
            success: function (data, textStatus) {
                var ele = $("#tblInformation").children("tbody");
                var htmlStr = "";

                for (var i = 0; i < data.length; i++) {
                    htmlStr += "<tr>";
                    htmlStr += "<td>" + data[i].Title + "</td>";
                    htmlStr += "<td>" + data[i].Detail + "</td>";
                    htmlStr += "<td>" + data[i].Creator + "</td>";
                    htmlStr += "<td>" + timeFormatter(data[i].CreateTime) + "</td>";
                    htmlStr += "</tr>"
                }
                ele.html(htmlStr);
            }
        });
    }

    //商品分类销售额比例
    function loadTypeRate(dateType) {

        function drawPieChart(placeholder, data, position) {
            $.plot(placeholder, data, {
                series: {
                    pie: {
                        show: true,
                        tilt: 0.8,
                        highlight: {
                            opacity: 0.25
                        },
                        stroke: {
                            color: '#fff',
                            width: 2
                        },
                        startAngle: 2
                    }
                },
                legend: {
                    show: true,
                    position: position || "ne",
                    labelBoxBorderColor: null,
                    margin: [-30, 15]
                }
              ,
                grid: {
                    hoverable: true,
                    clickable: true
                }
            })
        }

        function getRandomColor() {

            var colorStr = Math.floor(Math.random() * 0xFFFFFF).toString(16).toUpperCase();

            return "#" + "000000".substring(0, 6 - colorStr) + colorStr;
        }

        var placeholder = $('#piechart-placeholder').css({ 'width': '90%', 'min-height': '150px' });

        //商品类型销售比例
        $.CommonAjax({
            url: rootPath + "/API/Statistics/SaleRate",
            type: "GET",
            data: { CorpCode: "HWMT", dateType: dateType },
            success: function (result, textStatus) {
                var data = [
      //{ label: "手机", data: 38.7, color: "#68BC31" },
      //{ label: "平板", data: 24.5, color: "#2091CF" },
      //{ label: "智能手环", data: 8.2, color: "#AF4E96" },
      //{ label: "智能手表", data: 18.6, color: "#DA5430" },
      //{ label: "学习机", data: 10, color: "#FEE074" }
                ]
                for (var field in result) {
                    data.push({ label: field, data: result[field], color: getRandomColor() });
                }

                drawPieChart(placeholder, data);

                /**
                we saved the drawing function and the data to redraw with different position later when switching to RTL mode dynamically
                so that's not needed actually.
                */
                placeholder.data('chart', data);
                placeholder.data('draw', drawPieChart);

            }
        });

        var $tooltip = $("<div class='tooltip top in'><div class='tooltip-inner'></div></div>").hide().appendTo('body');
        var previousPoint = null;

        placeholder.on('plothover', function (event, pos, item) {
            if (item) {
                if (previousPoint != item.seriesIndex) {
                    previousPoint = item.seriesIndex;
                    var tip = item.series['label'] + " : " + item.series['percent'] + '%';
                    $tooltip.show().children(0).text(tip);
                }
                $tooltip.css({ top: pos.pageY + 10, left: pos.pageX + 10 });
            } else {
                $tooltip.hide();
                previousPoint = null;
            }

        });

    }
    //--结束商品分类比例

    //商品销售额折线图
    function loadItemsAmount(dateType) {
        //商品销售金额数据
        $.CommonAjax({
            url: rootPath + "/API/Statistics/SaleChange",
            type: "GET",
            data: { CorpCode: "HWMT", dateType: dateType },
            success: function (result, textStatus) {
                var data = [
                ]

                for (var i = 0; i < result.length; i++) {
                    var currentItem = null; //用于判断是否已经存在此标签

                    for (var j = 0; j < data.length; j++) {
                        //判断名称是否
                        if (data[j].name == result[i].Name) {
                            currentItem = data[j];
                            break;
                        }
                    }  //data循环结束

                    if (!currentItem) {
                        currentItem = { name: result[i].Name, data: [] };
                        data.push(currentItem);
                    }
                    var date = new Date(result[i].TimeOfDay.replace('-', '/'));

                    currentItem.data.push([Date.UTC(date.getFullYear(), date.getMonth(), date.getDay()), Number(result[i].Total)]);

                }   //result循环结束

                $('#sales-charts').highcharts({
                    chart: {
                        zoomType: 'x',
                        type: 'spline'
                    },
                    title: {
                        text: '商品销售情况折线图',
                        style: {
                            color: '#3E576F',
                            fontSize: '14px'
                        }
                    },
                    subtitle: {
                        text: '商品销售额日报表'
                    },
                    xAxis: {
                        type: 'datetime',
                        dateTimeLabelFormats: { // don't display the dummy year
                            day: '%b%e号',
                            month: "%b%e号",
                            year: "%y年%b%e号"
                        }
                    },
                    yAxis: {
                        title: {
                            text: '￥(元)'
                        },
                        min: 0
                    },
                    tooltip: {
                        formatter: function () {
                            return '<b>' + this.series.name + '</b><br>' +
                            Highcharts.dateFormat('%b%e号', this.x) + ': ￥' + this.y;
                        }
                    },
                    series: data
                });
            }
        });
    } //--商品销售额折线图

    //点击响应事件
    function onDropdownClick() {

        //获取a标签下的文本，去除全部空字符
        var dateType = $(this).children("a").text().replace(/(^\s*)|(\s*$)/g, '');

        var parent = $(this).parent("ul");
        //找到按钮，改变文本
        parent.parent("div").children("button").html(dateType + '<i class="icon-angle-down icon-on-right bigger-110"></i>');
        //去除样式类
        parent.children("li").removeClass("active").children("a").removeClass("blue").children("i").addClass("invisible");
        //加入样式类
        $(this).addClass("active").children("a").addClass("blue").children("i").removeClass("invisible");

        var targetFunc = parent.attr("data-target");

        var targetType = '';

        switch (dateType) {
            case "本周":
                targetType = "week";
                break;
            case "上周":
                targetType = "lastweek";
                break;
            case "本月":
                targetType = "month";
                break;
            case "上月":
                targetType = "lastmonth";
                break;
        }
        if (targetFunc == "BaseStatic") {
            loadBaseStatic(targetType);
        }
        else if (targetFunc == "TypeRate") {
            loadTypeRate(targetType);
        }
        else if (targetFunc == "ItemsAmount") {
            loadItemsAmount(targetType);
        }

    }

    return {
        Init: function () {
            loadBaseStatic('week');
            loadOtherInfo();
            loadTypeRate('week');
            loadItemsAmount('week');
            $(".dropdown-menu li").on('click', onDropdownClick);
        }
    };
}();