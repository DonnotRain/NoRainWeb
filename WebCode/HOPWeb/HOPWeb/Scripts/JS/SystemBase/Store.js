PlugFunctions.SystemBase.Store = function () {

    var currentUrl = rootPath + "/API/Store/";
    //全局变量声明
    var vars = {
        totalTimeout: 120 * 1000,
        markerItems: [],
        map: {}
    };
    //初始化
    var Initialize = {
        loadDataGrid: function () {
            $("#dgMain").datagrid({
                url: currentUrl,
                method: "get",
                pagination: true,
                fitColumns: true,
                checkOnSelect: false,
                selectOnCheck: false,
                singleSelect: false,
                nowrap: false,
                idField: "Id",
                columns: [[
                        { field: 'ck', checkbox: true },
                {
                    field: 'StoreName', title: '门店名称', width: 100, align: 'center'
                },
                  {
                      field: 'AbbrName', title: '简称', width: 60, align: 'center'
                  },
                 {
                     field: 'Address', title: '门店位置', width: 100, align: 'center'
                 },
                  {
                      field: 'Longitude', title: '所在经纬度', width: 60, align: 'center', formatter: function (value, row) {
                          var position = row.Longitude + "," + row.Latitude;
                          return "<a  href='javascript:void(0)' style='color:blue' onclick=" +
                              "PlugFunctions.SystemBase.Store.showPosition('" + row.Id + "');" + ">定位</a>";
                      }
                  },
                { field: 'UserCount', title: '促销员数量', width: 40, align: 'center' },
                { field: 'Detail', title: '门店描述', width: 140, align: 'center' }
                ]]
                , onBeforeLoad: function (para) {
                    para.begin = $("#conditionBegin").val();
                    para.end = $("#conditionEnd").val();
                    para.Name = $("#conditionName").val();
                    para.Address = $("#conditionAddress").val() || "";
                    para.BelongTo = $("#conditionBelongto").val();
                },
                onLoadSuccess: function (data) {
                    vars.markerItems = [];
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
                        //  var positionArray = rows[i].Position.split(",");

                        var markerPoint = new BMap.Point(rows[i].Longitude, rows[i].Latitude);

                        // var myIcon = new BMap.Icon(rootPath + "/Content/images/map/people.png", new BMap.Size(48, 48));

                        var marker = new BMap.Marker(markerPoint);  // 创建标注
                        map.addOverlay(marker);              // 将标注添加到地图中

                        //创建信息窗口
                        marker.infoWindow = new BMap.InfoWindow("门店名称：" + rows[i].StoreName + "<br/>" + "地址: " + rows[i].Address);
                        marker.addEventListener("click", function () {
                            this.openInfoWindow(this.infoWindow);
                        });

                        //添加额外属性
                        marker.Id = rows[i].Id;
                        marker.Point = markerPoint;

                        vars.markerItems.push(marker);
                        if (i == rows.length - 1) {
                            map.centerAndZoom(markerPoint, 18);
                        }
                    }


                }
            });
        },
        initialDialog: function () {
            $('#dlgMain').show();
            $("#dlgMain").dialog({
                title: "新增公告",
                modal: true,
                closed: true,
                buttons: [{
                    iconCls: "icon-save",
                    text: '保存',
                    handler: function () {
                        save();
                    }
                }, {
                    iconCls: "icon-remove",
                    text: '取消',
                    handler: function () {
                        $('#dlgMain').dialog('close');
                    }
                }]
            });

            $('#dlgMap').show();
            $("#dlgMap").dialog({
                title: "选择门店位置",
                modal: true,
                closed: true,
                buttons: [{
                    iconCls: "icon-save",
                    text: '确定',
                    handler: function () {
                        onPositionSelected();
                    }
                }, {
                    iconCls: "icon-remove",
                    text: '取消',
                    handler: function () {
                        $('#dlgMap').dialog('close');
                    }
                }]
            });
        },
        initialValidate: function () {
            $("#fm").validVal({ language: "cn" });
        },
        initialMap: function () {

            // 百度地图API功能
            var map = new BMap.Map("containMap");                        // 创建Map实例
            // map.centerAndZoom(new BMap.Point(120.350525, 30.309225), 15);     // 初始化地图,设置中心点坐标和地图级别
            map.addControl(new BMap.NavigationControl());               // 添加平移缩放控件
            map.addControl(new BMap.ScaleControl());                    // 添加比例尺控件
            map.addControl(new BMap.OverviewMapControl());              //添加缩略地图控件
            map.enableScrollWheelZoom();                            //启用滚轮放大缩小
            map.addControl(new BMap.MapTypeControl());          //添加地图类型控件
            //   vars.map = map;
            map.centerAndZoom("广州市", 15);                     // 初始化地图,设置中心点坐标和地图级别。

            // 创建地址解析器实例
            var myGeo = new BMap.Geocoder();
            // 将地址解析结果显示在地图上,并调整地图视野
            myGeo.getPoint("广州市天河区", function (point) {
                if (point) {
                    map.centerAndZoom(point, 16);
                    // map.addOverlay(new BMap.Marker(point));
                }
            }, "广州市");
            map.addEventListener("click", function (e) {
                $("#ChoosedLongitude").val(e.point.lng);
                $("#ChoosedLatitude").val(e.point.lat);
            });
        }
    }
    function onPositionSelected() {
        $("#dlgMap").dialog("close");
        $("#Longitude").val($("#ChoosedLongitude").val());
        $("#Latitude").val($("#ChoosedLatitude").val());
    }

    //初始化
    function InitialFunc() {
        Initialize.initialDialog();
        Initialize.loadDataGrid();
        Initialize.initialValidate();
        Initialize.initialMap();
        $("#btnChooseFile").click(chooseLatitute)

        window.onresize = resize;
        resize();
    }

    //重新加载dataGrid
    function reloadDataGrid() {
        $("#dgMain").datagrid('reload');
    }

    // 重新布局
    function resize(event) {
        //if (event) {
        //    event.preventDefault();
        //}
        var width = $(".page-content").width() - 10;
        var height = $(window).height() - 130;
        $("#dgMain").datagrid('resize', { // 重新布局DataGrid
            width: width
        });
    }

    //选择经纬度
    function chooseLatitute() {
        $("#dlgMap").dialog("open");
        $("#ChoosedLongitude").val($("#Longitude").val());
        $("#ChoosedLatitude").val($("#Latitude").val());
    }

    function save() {
        var type = "post";
        if ($("#Id").val() && $("#Id").val() != 0) {
            type = "put";
        }

        //验证输入合法性
        var form_data = $("#fm").triggerHandler("submitForm");
        if (!form_data) {
            return;
        }

        //上传文件
        try {
            form_data = $("#fm").triggerHandler("submitForm");

            $.jMask("save", "保存中，请稍后").show();
            $.CommonAjax({
                url: currentUrl,
                type: type,
                data: form_data,
                success: function (data, textStatus) {
                    $('#dlgMain').dialog('close');
                    $.jMask("save").hide();
                    $.Show({
                        message: "保存成功",
                        type: "success",
                        hideAfter: 3
                    });
                    reloadDataGrid();
                }
            });
        }
        catch (e) {
            $.jMask("HideAll");
            alert(e.message);
        }
    }

    return {
        init: function () {
            easyloader.locale = "zh_CN";
            easyloader.theme = "metro";
            easyloader.load(['parser', 'validatebox', 'datagrid', 'combobox', 'form', 'dialog', 'messager'], InitialFunc);
        }
       , reloadDataGrid: reloadDataGrid,
        add: function () {
            $('#fm').form('clear');
            $("#TargetZone").val("1");
            $("#TargetType").val("1");
            $('#dlgMain').dialog('open').dialog('setTitle', '添加门店');
        }
        , edit: function () {
            $('#fm').form('clear');
            var items = $("#dgMain").datagrid('getChecked');

            if (items && items.length > 0) {
                var item = items[0];

                $("#dgMain").datagrid('unselectAll');
                $("#dgMain").datagrid('selectRecord', item.Id);

                $('#fm').form('load', item);
                for (var filed in item) {
                    $("#" + filed).val(item[filed]);
                }
                //特殊的输入框 dateFormatter
                $("#ValidDate").val(dateFormatter(item.ValidDate));

                $("#spanFileName").html((item.Files && item.Files.length) ? item.Files[0].FileName : "");
                $('#dlgMain').dialog('open').dialog('setTitle', '修改门店');
            }
            else {
                $.Show({ message: "未选取数据行", type: "error" });
                return;
            }
        },
        deleteItems: function () {
            var rows = $("#dgMain").datagrid('getChecked');
            if (!rows.length) {
                $.Show({ message: "未选取数据行", type: "error" });
                return;
            }
            $.messager.confirm('确认', '确定要删除所选数据？', function (r) {
                if (r) {
                    var ids = new Array();

                    $.each(rows, function (rowIndex, rowData) {
                        ids.push(rowData.Id);
                    });
                    $.jMask("delete", "删除中，请稍后").show();
                    $.CommonAjax({
                        url: currentUrl,
                        type: "delete",
                        data: { "": ids.join(",") },
                        success: function (data, textStatus) {
                            $.jMask("delete").hide();
                            $.Show({ message: "删除成功", type: "success" });
                            reloadDataGrid();
                        }
                    });
                }
            });
        }, showPosition: function (id) {

            var item = null;

            for (var i = 0; i < vars.markerItems.length; i++) {
                if (vars.markerItems[i]["Id"] == id) {
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



