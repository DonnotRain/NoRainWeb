PlugFunctions.Attendence.Index = function () {

    var currentUrl = rootPath + "/API/ProductUnit/";
    //全局变量声明
    var vars = {
        markerItems: [],
        map: {}
    };
    //初始化
    var Initialize = {
        loadDataGrid: function () {
            $("#dgMain").datagrid({
                url: rootPath + "/API/Leave",
                method: "get",
                pagination: true,
                fitColumns: true,
                nowrap: false,
                checkOnSelect: false,
                selectOnCheck: false,
                singleSelect: true,
                columns: [[
                { field: 'ck', checkbox: true },
                { field: 'StoreName', title: '所属门店', width: 100, align: 'center' },
                { field: 'Name', title: '请假人', width: 100, align: 'center' },
                  {
                      field: 'BeginDate', title: '请假开始时间', width: 100, align: 'center', formatter: timeFormatter
                  },
                {
                    field: 'EndDate', title: '请假结束时间', width: 100, align: 'center', formatter: timeFormatter
                },
                  { field: 'Duration', title: '请假时长', width: 100, align: 'center' },
                { field: 'Reason', title: '请假原因', width: 100, align: 'center' },
                  {
                      field: 'State', title: '批复状态', width: 100, align: 'center', formatter: function (value, row) {
                          //葛红 #E3170D
                          //翠绿色 #0E9E4C
                          //橙色 #FF6100
                          var result = "";
                          var color = "#00C957";
                          switch (value) {
                              case 0:
                                  result = "已拒绝";
                                  color = "#E3170D";
                                  break;
                              case 1:
                                  result = "已同意";
                                  color = "#0E9E4C";
                                  break;
                              case -1:
                                  result = "未审批";
                                  color = "#FF6100";
                                  break;
                          }
                          return "<span style='color:" + color + "'> " + result + "</span>";
                      }
                  }
                ]]
                , onBeforeLoad: function (para) {
                    //IE下有异常，因为插件的加载顺序有问题
                    try {
                        para.beginDate = $("#conditionBeginTime").val();
                        para.endDate = $("#conditionEndTime").val();
                        para.state = "";
                        para.state = $("#Status").combobox("getValues") ? $("#Status").combobox("getValues").join(",") : "";
                    }
                    catch (e) {

                    }
                }
            });

            //促销员点名
            $("#dgAttendence").datagrid({
                url: rootPath + "/API/Attendence",
                method: "get",
                pagination: true,
                fitColumns: true,
                nowrap: false,
                checkOnSelect: false,
                selectOnCheck: false,
                singleSelect: true,
                columns: [[
                { field: 'ck', checkbox: true },
                { field: 'StoreName', title: '所属门店', width: 100, align: 'center' },
                { field: 'Name', title: '促销员名称', width: 60, align: 'center' },
                {
                    field: 'Type', title: '签到类型', width: 50, align: 'center', formatter: function (value, row) {
                        return value == "0" ? "上班" : "下班";
                    }
                },
                  {
                      field: 'Time', title: '签到时间', width: 100, align: 'left', formatter: timeFormatter
                  },
                  {
                      field: 'IsInRange', title: '是否在设置范围内', width: 100, align: 'center', formatter: function (value) {
                          if (value) {
                              return '是';
                          } else {
                              return '否';
                          }
                      }
                  },
                  {
                      field: 'Position', title: '签到位置', width: 50, align: 'center', formatter: function (value, row) {
                          var imagePath = rootPath + "/API/Attendence/Image?attendenceId=" + row.ID;
                          return "<a  href='javascript:void(0)' style='color:blue' onclick=" +
                              "PlugFunctions.Attendence.Index.showPosition('" + row.ID + "');" + ">定位</a>";
                      }
                  }, 
                  {
                      field: 'State', title: '签到拍照', width: 50, align: 'center', formatter: function (value, row) {
                          var imgUrl = rootPath + "/api/FileItem/DownloadById?fileId=" + row.Files[0].ID;

                          if (row.Files && row.Files.length > 0) {

                              if (row.Files[0].Extension.toUpperCase() == ".JPG" || row.Files[0].Extension.toUpperCase() == ".PNG" ||
                                  row.Files[0].Extension.toUpperCase() == ".GIF" || row.Files[0].Extension.toUpperCase() == ".BMP") {
                                  return '<a class="example-image-link" href="' + imgUrl + '" data-lightbox="example-View" data-title="' + row.Files[0].FileName + '">查看</a>';
                              }
                              else {
                                  return "<a target='_blank' href='" + imgUrl + "' style='color:blue' >" + row.Files[0].FileName + "</a>"
                              }
                          }
                          return "<span style='color:rgb(187, 69, 69)'>未上传</span>";
                      }
                  }
                ]]
                , onBeforeLoad: function (para) {
                    vars.markerItems = [];
                    para.beginDate = $("#signBeginTime").val();
                    para.endDate = $("#signEndTime").val();
                    para.UserName = $("#conditionUserName").val();
                },
                onLoadSuccess: function (data) {

                    var initMap = function () {

                        // 百度地图API功能
                        var map = new BMap.Map("containnerMap");                        // 创建Map实例
                        // map.centerAndZoom(new BMap.Point(120.350525, 30.309225), 15);     // 初始化地图,设置中心点坐标和地图级别
                        map.addControl(new BMap.NavigationControl());               // 添加平移缩放控件
                        map.addControl(new BMap.ScaleControl());                    // 添加比例尺控件
                        map.addControl(new BMap.OverviewMapControl());              //添加缩略地图控件
                        map.enableScrollWheelZoom();                            //启用滚轮放大缩小
                        map.addControl(new BMap.MapTypeControl());          //添加地图类型控件
                        vars.map = map;
                        var rows = data.rows;
                        for (var i = 0; i < rows.length; i++) {
                            var positionArray = rows[i].Position.split(",");

                            var markerPoint = new BMap.Point(positionArray[0], positionArray[1]);

                            var myIcon = new BMap.Icon(rootPath + "/Content/images/map/people.png", new BMap.Size(48, 48));

                            var marker = new BMap.Marker(markerPoint, { icon: myIcon });  // 创建标注
                            map.addOverlay(marker);              // 将标注添加到地图中

                            //创建信息窗口
                            marker.infoWindow = new BMap.InfoWindow(rows[i].Name + "   " + timeFormatter(rows[i].Time) + "<br /><img style='height:400px;width:auto' src='" + rootPath + '/api/FileItem/DownloadById?fileId=' + rows[i].Files[0].ID + "'/>" + "<br/>");
                            marker.addEventListener("click", function () {
                                this.openInfoWindow(this.infoWindow);
                            });

                            //添加额外属性
                            marker.ID = rows[i].ID;
                            marker.Point = markerPoint;

                            vars.markerItems.push(marker);
                            if (i == rows.length - 1) {
                                map.centerAndZoom(markerPoint, 18);
                            }
                        }
                    }

                    initMap();

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
        var width = $(".page-content").width();
        var height = $(window).height() - 130;
        $('#tabMain').show();
        $('#tabMain').tabs({
            width: width,
            height: height,
            onSelect: function () {
                //    resize();
            }
        });
        Initialize.initialDialog();
        Initialize.initialValidate();
        Initialize.loadDataGrid();
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
        //   $('#tabMain').tabs({ width: width, height: height })//.tabs("resize");

    }

    return {
        init: function () {
            easyloader.locale = "zh_CN";
            easyloader.theme = "metro";
            easyloader.load(['parser', 'validatebox', 'datagrid', 'combobox', 'form', 'dialog', 'messager', 'tabs'], InitialFunc);
        }
       , reloadDataGrid: reloadDataGrid
         , reloadSignDataGrid: function () {
             $("#dgAttendence").datagrid('reload');
         },
        add: function () {
            var items = $("#dgMain").datagrid('getChecked');

            if (items && items.length > 0) {
                var item = items[0];
                $.messager.confirm('提示', '是否通过审批?', function (r) {

                    $.jMask("save", "处理中,请稍候").show();
                    var flag = true;
                    if (r) {
                        flag = true;
                    } else {
                        flag = false;
                    }

                    $.CommonAjax({
                        url: rootPath + "/API/Leave?isPass=" + flag + "&ID=" + item.ID,
                        method: 'Put',
                        type: 'json',
                        success: function (data, textStatus) {
                            $('#dlgMain').dialog('close');
                            $.jMask("save").hide();
                            $.Show({
                                message: "处理成功",
                                type: "success",
                                hideAfter: 3
                            });
                            reloadDataGrid();
                        }
                    });

                });
            }
            else {
                $.Show({ message: "未选取数据行", type: "error" });
                return;
            }

        }
        , showPosition: function (id) {

            var item = null;

            for (var i = 0; i < vars.markerItems.length; i++) {
                if (vars.markerItems[i]["ID"] == id) {
                    item = vars.markerItems[i];
                    break;
                }
            }

            if (item) {
                vars.map.centerAndZoom(item.Point, 18);

                if (document.createEvent) {
                    var evObj = document.createEvent('MouseEvents');
                    evObj.initMouseEvent('click', true, true, document.defaultView, 0, 0, 0, 0, 0, false, false, false, false, 0, null);
                    item.dispatchEvent(evObj);
                }
                else if (document.createEventObject) {
                    item.fireEvent('click');
                }
            }
        }
    };
}();



