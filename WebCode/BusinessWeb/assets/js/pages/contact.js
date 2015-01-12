var Contact = function () {

    return {

        //Map
        initMap: function () {

            // 百度地图API功能
            var map = new BMap.Map("map");                        // 创建Map实例
            // map.centerAndZoom(new BMap.Point(120.350525, 30.309225), 15);     // 初始化地图,设置中心点坐标和地图级别
            map.addControl(new BMap.NavigationControl());               // 添加平移缩放控件
            map.addControl(new BMap.ScaleControl());                    // 添加比例尺控件
            map.addControl(new BMap.OverviewMapControl());              //添加缩略地图控件
            map.enableScrollWheelZoom();                            //启用滚轮放大缩小
            map.addControl(new BMap.MapTypeControl());          //添加地图类型控件

            var sContent = "<div style='width:100%;margin:20px;'>杭州卓诚餐饮管理有限公司&杭州亿纳农副产品有限公司<div>";
            var point = new BMap.Point(120.350525, 30.309225);
            map.centerAndZoom(point, 21);
            var infoWindow = new BMap.InfoWindow(sContent);  // 创建信息窗口对象
            map.openInfoWindow(infoWindow, point); //开启信息窗口

            //function showInfo(e) {
            //    alert(e.point.lng + ", " + e.point.lat);
            //}
            //map.addEventListener("click", showInfo);

        }

    };
}();