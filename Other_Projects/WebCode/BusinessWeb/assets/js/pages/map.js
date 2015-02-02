var addlength = addrs.length,
merchantTip = !1,
merchantTipPoint = "";
if (merchantTip = "0" == merchantId || 0 >= merchantId.length ? !1 : !0) for (var i = 0; i < addlength; i++) if (addrs[i].id == merchantId) {
    merchantTipPoint = new BMap.Point(addrs[i].longitude, addrs[i].latitude);
    break;
}
var map = new BMap.Map("mapContainer"),
routePolicy = [BMAP_TRANSIT_POLICY_LEAST_TIME, BMAP_TRANSIT_POLICY_LEAST_TRANSFER, BMAP_TRANSIT_POLICY_LEAST_WALKING];
map.addControl(new BMap.NavigationControl);
map.enableScrollWheelZoom();
map.addControl(new BMap.OverviewMapControl);
for (var firstPoint = 0,
htmlOption = "",
i = 0; i < addlength; i++) merchantTip ? addrs[i].id == merchantId ? (htmlOption += "<option selected='selected' value='" + i + "'>" + addrs[i].label + "</option>", firstPoint = i) : htmlOption += "<option  value='" + i + "'>" + addrs[i].label + "</option>" : 0 == i ? (htmlOption += "<option selected='selected' value='0'>" + addrs[i].label + "</option>", firstPoint = i) : htmlOption += "<option  value='" + i + "'>" + addrs[i].label + "</option>";
$(".detail .info select").html(htmlOption);
var bugbaidu = 0;
function callback(b) {
    if (!(0 < bugbaidu)) {
        bugbaidu = 1;
        var a = null,
        d = 0,
        l;
        for (l in b) a = b[l],
        0 == a.error && (addrs[d].isBaiduCoordinate || (addrs[d].longitude = a.x, addrs[d].latitude = a.y), d++);
        NMMap();
    }
}
function transGoogleToBaidu() {
    for (var b = [], a = 0, d = addrs.length; a < d; a++) b.push(new BMap.Point(addrs[a].longitude, addrs[a].latitude));
    BMap.Convertor.transMore(b, 2, callback);
}
var markers = [],
markersQidian = [],
lastTitle = "",
endTitle = "",
keyWord = "",
curopenInfoIndex = 0,
Lmarkers = 0,
markersQidianL = 0,
qidianPoint = [],
points = [],
iconnohover = new BMap.Icon("http://nuomi.xnimg.cn/image_new/icon/markers_newnuomi1.png", new BMap.Size(26, 38), {
    infoWindowAnchor: new BMap.Size(12, 3),
    imageOffset: new BMap.Size(-125, -112)
}),
iconhover = new BMap.Icon("http://nuomi.xnimg.cn/image_new/icon/markers_newnuomi1.png", new BMap.Size(26, 38), {
    infoWindowAnchor: new BMap.Size(12, 3),
    imageOffset: new BMap.Size(-125, -160)
});
function getInfoHtml(b) {
    return 1 < addrs[b].phone.length ? "<div class='NMmapWindow'><b class='label'>" + addrs[b].label + "</b></br>\u5730\u5740\uff1a" + addrs[b].address + "</br>\u7535\u8bdd\uff1a" + addrs[b].phone + "<p class='serch-text'><span>\u8d77\u70b9\uff1a</span><span><input type='text'  value='' name='s' class='stext' id='point_end' /></span><span><a href='javascript:;' class='gj' onclick='bus_end(1);'>\u516c\u4ea4</a></span><span><a href='javascript:;' class='jc' onclick='bus_end(2);'>\u9a7e\u8f66</a></span></p></div>" : "<div class='NMmapWindow'><b class='label'>" + addrs[b].label + "</b></br>\u5730\u5740\uff1a" + addrs[b].address + "<p class='serch-text'><span>\u8d77\u70b9\uff1a</span><span><input type='text' value='' name='s' class='stext' id='point_end' /></span><span><a href='javascript:;' class='gj' onclick='bus_end(1);'>\u516c\u4ea4</a></span><span><a href='javascript:;' class='jc' onclick='bus_end(2);'>\u9a7e\u8f66</a></span></p></div>"
}
var firstCome = !1;
function refreshRightPanel(b) {
    $(".rightcont").empty();
    var a;
    a = "" + ('<div class="detalishop">\t\t\t\t<div class="detalicon bg" style="background-position:' + (280 < 29 * b ? "0px" : "-" + 24 * b + "px") + " " + (280 < 29 * b ? "-116px" : "-198px") + ';"></div>\t\t\t\t<div class="msg">\t\t\t\t<p><h1 class="label">' + addrs[b].label + "</h1></p>\t\t\t\t<p><b>\u5730\u5740\uff1a</b>" + addrs[b].address + "</p>\t\t\t\t<p><b>" + (1 < addrs[b].phone.length ? "\u7535\u8bdd\uff1a" : "") + "</b>" + addrs[b].phone + "</p>\t\t\t\t</div>\t\t\t</div>");
    firstCome ? ($(".rightcont").html(a), $(".detalishop").bind("mouseover",
    function () {
        var a = $(this).children(".detalicon").attr("y");
        $(this).children(".detalicon").css("background-position", a + "px -198px");
        new BMap.Icon("http://nuomi.xnimg.cn/image_new/icon/markers_newnuomi1.png", new BMap.Size(26, 38), {
            infoWindowAnchor: new BMap.Size(12, 3),
            imageOffset: new BMap.Size("-" + (280 < 29 * b ? 152 : 34 * b), 280 < 29 * b ? -177 : -73)
        });
        markers[b].setIcon(iconhover)
    }).bind("mouseout",
    function () {
        var a = $(this).children(".detalicon").attr("y");
        $(this).children(".detalicon").css("background-position", a + "px -198px");
        new BMap.Icon("http://nuomi.xnimg.cn/image_new/icon/markers_newnuomi1.png", new BMap.Size(26, 38), {
            infoWindowAnchor: new BMap.Size(12, 3),
            imageOffset: new BMap.Size("-" + (280 < 29 * b ? 152 : 29 * b), 280 < 29 * b ? -130 : 4)
        });
        markers[b].setIcon(iconnohover)
    })) : firstCome = !0
}
var tipfirst = 0;
function NMMap() {
    1 < addlength && $("#merchant-name").html($("#merchant-name").html() + "(" + addlength + "\u5bb6\u5206\u5e97)");
    var b = 0;
    $("#chooseAddr select").change(function () {
        if (1 != b) {
            b = 1;
            markers = [];
            markersQidian = [];
            keyWord = endTitle = lastTitle = "";
            markersQidianL = Lmarkers = curopenInfoIndex = 0;
            qidianPoint = [];
            points = [];
            map.clearOverlays();
            for (var a = 0,
            d = addrs.length; a < d; a++) {
                var l = new BMap.Point(addrs[a].longitude, addrs[a].latitude);
                points.push(l);
                new BMap.Icon("http://nuomi.xnimg.cn/image_new/icon/markers_newnuomi1.png", new BMap.Size(280 < 29 * a ? 12 : 26, 280 < 29 * a ? 16 : 38), {
                    infoWindowAnchor: new BMap.Size(12, 3),
                    imageOffset: new BMap.Size("-" + (280 < 29 * a ? 150 : 29 * a), 280 < 29 * a ? -130 : 4)
                });
                var m = new BMap.Marker(l, {
                    icon: iconnohover
                });
                points[a] = l;
                markers[a] = m;
                Lmarkers++;
                markers[1E4 * (a + 1)] = 0; (function (a) {
                    m.addEventListener("mouseover",
                    function () {
                        new BMap.Icon("http://nuomi.xnimg.cn/image_new/icon/markers_newnuomi1.png", new BMap.Size(280 < 29 * a ? 12 : 26, 280 < 29 * a ? 16 : 38), {
                            infoWindowAnchor: new BMap.Size(12, 3),
                            imageOffset: new BMap.Size("-" + (280 < 29 * a ? 152 : 34 * a), 280 < 29 * a ? -177 : -73)
                        });
                        this.setIcon(iconhover)
                    });
                    m.addEventListener("mouseout",
                    function () {
                        if (1 == markers[1E4 * (a + 1)]) return !1;
                        new BMap.Icon("http://nuomi.xnimg.cn/image_new/icon/markers_newnuomi1.png", new BMap.Size(280 < 29 * a ? 12 : 26, 280 < 29 * a ? 16 : 38), {
                            infoWindowAnchor: new BMap.Size(12, 3),
                            imageOffset: new BMap.Size("-" + (280 < 29 * a ? 152 : 29 * a), 280 < 29 * a ? -130 : 4)
                        });
                        this.setIcon(iconnohover)
                    });
                    m.addEventListener("click",
                    function () {
                        for (var b = 0; b < Lmarkers; b++) markers[1E4 * (b + 1)] = 0,
                        markers[b].setIcon(iconnohover);
                        new BMap.Icon("http://nuomi.xnimg.cn/image_new/icon/markers_newnuomi1.png", new BMap.Size(280 < 29 * a ? 12 : 26, 280 < 29 * a ? 16 : 38), {
                            infoWindowAnchor: new BMap.Size(12, 3),
                            imageOffset: new BMap.Size("-" + (280 < 29 * a ? 152 : 34 * a), 280 < 29 * a ? -177 : -73)
                        });
                        this.setIcon(iconhover);
                        b = new BMap.InfoWindow(getInfoHtml(a));
                        refreshRightPanel(a);
                        this.openInfoWindow(b);
                        markers[1E4 * (a + 1)] = 1
                    })
                })(a);
                map.addOverlay(m)
            } !merchantTip && 1 != addlength && map.setViewport(points)
        }
        1 == addlength && (a = $("#chooseAddr select option:selected"), endTitle = addrs[a.val()].label, d = new BMap.InfoWindow(getInfoHtml(a.val())), markers[a.val()].setIcon(iconhover), thisPoint = points[a.val()], markers[a.val()].openInfoWindow(d), markers[1E4 * (a.val() + 1)] = 1, curopenInfoIndex = a.val(), map.centerAndZoom(thisPoint, 15), refreshRightPanel(a.val()));
        a = $("#chooseAddr select option:selected");
        thisPoint = points[a.val()];
        "0" == merchantId && 0 == tipfirst ? tipfirst = 1 : (endTitle = addrs[a.val()].label, d = new BMap.InfoWindow(getInfoHtml(a.val())), markers[a.val()].setIcon(iconhover), markers[a.val()].openInfoWindow(d), markers[1E4 * (a.val() + 1)] = 1, curopenInfoIndex = a.val(), map.centerAndZoom(thisPoint, 15), refreshRightPanel(a.val()))
    });
    $("#chooseAddr select").change()
}
var searchBus = function (b, a, d) {
    function l(a, b, f, e) {
        return function () {
            var n = b.getPlan(f),
            d = [],
            g = function (b, e, d, f) {
                var c;
                if (1 == e) c = 34,
                d = new BMap.Icon("http://api.map.baidu.com/img/dest_markers.png", new BMap.Size(42, c), {
                    offset: new BMap.Size(14, 32),
                    imageOffset: new BMap.Size(0, 0 - d * c)
                });
                else {
                    var n = c = 25,
                    g = 0,
                    h = 0;
                    2 == d && (n = 21, g = 5, h = 1);
                    d = new BMap.Icon("http://api.map.baidu.com/img/trans_icons.png", new BMap.Size(22, n), {
                        offset: new BMap.Size(10, 11 + h),
                        imageOffset: new BMap.Size(0, 0 - d * c - g)
                    })
                }
                b = new BMap.Marker(b, {
                    icon: d
                });
                null != f && "" != f && b.setTitle(f);
                1 == e && b.setTop(!0);
                a.addOverlay(b)
            },
            m = function (a) {
                for (var b = 0; b < a.length; b++) d.push(a[b])
            };
            a.clearOverlays();
            for (var h = 0; h < n.getNumRoutes() ; h++) {
                var k = n.getRoute(h);
                0 < k.getDistance(!1) && a.addOverlay(new BMap.Polyline(k.getPath(), {
                    strokeStyle: "dashed",
                    strokeColor: "#30a208",
                    strokeOpacity: 0.75,
                    strokeWeight: 4,
                    enableMassClear: !0
                }))
            }
            for (h = 0; h < n.getNumLines() ; h++) k = n.getLine(h),
            m(k.getPath()),
            k.type == BMAP_LINE_TYPE_BUS ? (g(k.getGetOnStop().point, 2, 2, k.getGetOnStop().title), g(k.getGetOffStop().point, 2, 2, k.getGetOffStop().title)) : k.type == BMAP_LINE_TYPE_SUBWAY && (g(k.getGetOnStop().point, 2, 3, k.getGetOnStop().title), g(k.getGetOffStop().point, 2, 3, k.getGetOffStop().title)),
            a.addOverlay(new BMap.Polyline(k.getPath(), {
                strokeColor: "#0030ff",
                strokeOpacity: 0.5,
                strokeWeight: 6,
                enableMassClear: !0
            }));
            a.setViewport(d);
            g(b.getEnd().point, 1, 1);
            g(b.getStart().point, 1, 0);
            null != c && (c.style.backgroundColor = "#fff");
            e.style.backgroundColor = "#fff";
            c = e
        }
    }
    var m = new BMap.TransitRoute(map, {
        policy: d
    }),
    f = "<div class='gjlist' id='t1'><span class='gjhc' style='height:23px;font-weight:bold;' onclick=\"exchangeBC('1')\">\u516c\u4ea4\u6362\u4e58</span><span class='gjhc' style='border-bottom:1px solid #D2D2D2;' onclick=\"exchangeBC('2')\">\u9a7e\u8f66\u8def\u7ebf</span></div><div id='dvPolicy'> <input id='policy0' checked='true' type='radio' name='pickPolicy'/>\u8f83\u4fbf\u6377<input id='policy1' type='radio' name='pickPolicy'/>\u5c11\u6362\u4e58<input id='policy2' type='radio' name='pickPolicy'/>\u5c11\u6b65\u884c</div><div id='showresults'></div><div style='text-align:right;padding-top:6px;'><a href='javascript:;' onclick=\"bus_end(1)\" style='display:none;text-decoration:none;'>&lt;&lt;\u91cd\u65b0\u9009\u62e9\u8d77\u70b9</a></div>";
    $("#t1")[0] || ($(".rightcont").empty(), $(".rightcont").html(f));
    m.setSearchCompleteCallback(function (a) {
        if (m.getStatus() == BMAP_STATUS_SUCCESS) {
            document.getElementById("dvPolicy").onclick = function (a) {
                a = a || window.event;
                a = a.srcElement || a.target;
                if (a.tagName.toLowerCase() == "input") {
                    a = a.getAttribute("id").replace("policy", "");
                    map.clearOverlays();
                    searchBus(lastPoint, thisPoint, routePolicy[a])
                }
            };
            $("#showresults").empty();
            f = [];
            for (var b = 0; b < a.getNumPlans() && b < 5; b++) {
                var d = document.createElement("div"),
                e = l(map, a, b, d);
                d.style.lineHeight = "20px";
                d.style.backgroundColor = "#fff";
                d.style.marginTop = "10px";
                d.onclick = e;
                for (var e = a.getPlan(b), c = e.getNumLines(), o = 0; o < c; o++) {
                    var g = e.getLine(o);
                    if (g.type == BMAP_LINE_TYPE_SUBWAY) {
                        for (var j = 0; j < e.getNumRoutes() ; j++) if (e.getRoute(j).getIndex() == o) {
                            var h = " ";
                            switch (g.type) {
                                case BMAP_LINE_TYPE_BUS:
                                    h = "\u516c\u4ea4\u7ad9";
                                    break;
                                case BMAP_LINE_TYPE_SUBWAY:
                                    h = "\u5730\u94c1\u7ad9";
                                    break;
                                case BMAP_LINE_TYPE_FERRY:
                                    h = "\u8f6e\u6e21";
                                    break;
                                default:
                                    h = " "
                            }
                            f.push({
                                type: "0",
                                index: e.getRoute(j).getIndex(),
                                distance: e.getRoute(j).getDistance(),
                                descript: g.getGetOnStop().title,
                                lineType: h
                            })
                        }
                        f.push({
                            type: "1",
                            name: g.title,
                            stops: g.getNumViaStops(),
                            offstop: g.getGetOffStop().title,
                            distance: g.getDistance(),
                            descript: g.getGetOnStop().title
                        })
                    }
                    if (g.type == BMAP_LINE_TYPE_BUS) {
                        for (j = 0; j < e.getNumRoutes() ; j++) if (e.getRoute(j).getIndex() == o) {
                            h = " ";
                            switch (g.type) {
                                case BMAP_LINE_TYPE_BUS:
                                    h = "\u516c\u4ea4\u7ad9";
                                    break;
                                case BMAP_LINE_TYPE_SUBWAY:
                                    h = "\u5730\u94c1\u7ad9";
                                    break;
                                case BMAP_LINE_TYPE_FERRY:
                                    h = "\u8f6e\u6e21";
                                    break;
                                default:
                                    h = " "
                            }
                            f.push({
                                type: "0",
                                index: e.getRoute(j).getIndex(),
                                distance: e.getRoute(j).getDistance(),
                                descript: g.getGetOnStop().title,
                                lineType: h
                            })
                        }
                        f.push({
                            type: "2",
                            name: g.title,
                            stops: g.getNumViaStops(),
                            offstop: g.getGetOffStop().title,
                            distance: g.getDistance()
                        })
                    }
                    if (g.type == BMAP_LINE_TYPE_FERRY) {
                        for (j = 0; j < e.getNumRoutes() ; j++) if (e.getRoute(j).getIndex() == o) {
                            h = " ";
                            switch (g.type) {
                                case BMAP_LINE_TYPE_BUS:
                                    h = "\u516c\u4ea4\u7ad9";
                                    break;
                                case BMAP_LINE_TYPE_SUBWAY:
                                    h = "\u5730\u94c1\u7ad9";
                                    break;
                                case BMAP_LINE_TYPE_FERRY:
                                    h = "\u8f6e\u6e21";
                                    break;
                                default:
                                    h = " "
                            }
                            f.push({
                                type: "0",
                                index: e.getRoute(j).getIndex(),
                                distance: e.getRoute(j).getDistance(),
                                descript: e.getRoute(j).getNumSteps(),
                                lineType: h
                            })
                        }
                        f.push({
                            type: "3",
                            name: g.title,
                            stops: g.getNumViaStops(),
                            offstop: g.getGetOffStop().title,
                            distance: g.getDistance()
                        })
                    }
                }
                e.getRoute(e.getNumRoutes() - 1).getIndex() >= c && f.push({
                    type: "0",
                    index: e.getRoute(e.getNumRoutes() - 1).getIndex(),
                    distance: e.getRoute(e.getNumRoutes() - 1).getDistance(),
                    descript: "\u7ec8\u70b9"
                });
                e = "";
                for (c = 0; c < f.length; c++) e = f[c].type == 0 ? e + ("<div class='show items'><div class='show zoubu'>\u6b65\u884c\uff1a</div><p class='maptext'>\u6b65\u884c" + (Number(/[0-9]+/.exec(f[c].distance)[0]) > 10 ? f[c].distance : "10\u7c73") + "\u81f3<b>" + f[c].descript + (f[c].lineType ? f[c].lineType : "") + "</b></p></div><div class='show cphr'></div>") : f[c].type == 1 ? e + ("<div class='show items'><div class='show ditie'>\u5730\u94c1\uff1a</div><p class='maptext'>\u4e58\u5750<b>" + f[c].name.replace(/\(\S?\)/g, "") + "</b>\u7ecf\u8fc7<b>" + f[c].stops + "</b>\u7ad9 \u5728<b>" + f[c].offstop + "</b>\u7ad9\u4e0b\u8f66</p></div><div class='show cphr'></div>") : e + ("<div class='show items'><div class='show gongjiao'>\u516c\u4ea4\uff1a</div><p class='maptext'>\u4e58\u5750<b>" + f[c].name.replace(/\(\S?\)/g, "") + "</b>\u7ecf\u8fc7<b>" + f[c].stops + "</b>\u7ad9 \u5728<b>" + f[c].offstop + "</b>\u7ad9\u4e0b\u8f66</p></div><div class='show cphr'></div>");
                d.innerHTML = "<div class='show content'><div class='show items' style='padding:0;height:16px;'><span class='indexnum'>" + (b + 1) + "</span></div><div class='show items' style='padding-bottom:5px;'><div class='show qidian'>\u8d77\u70b9\uff1a</div><p class='maptext' style='padding:0 5px;'><span style='padding:2px 4px;font-weight:bold;background-color:#6688ee;color:#fff;'>" + lastTitle + "</span></p></div><div class='show cphr'></div>" + e + "<div class='show items' style='padding-top:5px;'><div class='show zhongdian'>\u7ec8\u70b9\uff1a</div><p class='maptext' style='padding:0 5px;'><span style='padding:2px 4px;font-weight:bold;background-color:#6688ee;color:#fff;'>" + endTitle + "</span></p></div>";
                document.getElementById("showresults").appendChild(d);
                if (b == 0) d.onclick();
                f = []
            }
            $("#showresults .content").bind("click",
            function () {
                $(this).css("border", "2px solid #6688EE");
                $("#showresults .content").css("opacity", 0.5);
                $(this).css("opacity", 1)
            });
            try {
                $("#showresults .content").eq(0).click()
            } catch (k) { }
        } else $("#showresults").html("<div class='show content'><div class='show items' style='padding:0;height:16px;'><span class='indexnum'>1</span></div><div class='show items' style='padding-bottom:5px;'><div class='show qidian'>\u8d77\u70b9\uff1a</div><p class='maptext' style='padding:0 5px;'><span style='padding:2px 4px;font-weight:bold;background-color:#6688ee;color:#fff;'>" + lastTitle + "</span></p></div><div class='show cphr'></div><div class='show items'><div class='show zoubu'>\u6b65\u884c\uff1a</div><p class='maptext'>\u8d77\u70b9\u4e0e\u7ec8\u70b9\u8ddd\u79bb\u8f83\u8fd1\uff0c\u60a8\u53ef\u6b65\u884c\u524d\u5f80</p></div><div class='show cphr'></div><div class='show items' style='padding-top:5px;'><div class='show zhongdian'>\u7ec8\u70b9\uff1a</div><p class='maptext' style='padding:0 5px;'><span style='padding:2px 4px;font-weight:bold;background-color:#6688ee;color:#fff;'>" + endTitle + "</span></p></div>")
    });
    m.search(b, a);
    var c = null
},
routePolicy2 = [BMAP_DRIVING_POLICY_LEAST_TIME, BMAP_DRIVING_POLICY_LEAST_DISTANCE, BMAP_DRIVING_POLICY_AVOID_HIGHWAYS],
searchCar = function (b, a, d) {
    var l = "<div class='gjlist' id='t2'><span class='gjhc' onclick=\"exchangeBC('1')\">\u516c\u4ea4\u6362\u4e58</span><span class='gjhc' style='height:23px;font-weight:bold;' onclick=\"exchangeBC('2')\">\u9a7e\u8f66\u8def\u7ebf</span></div><div id='dvPolicy'> <input id='policy0' checked='true' type='radio' name='pickPolicy'/>\u6700\u5c11\u65f6\u95f4<input id='policy1' type='radio' name='pickPolicy'/>\u6700\u77ed\u8ddd\u79bb<input id='policy2' type='radio' name='pickPolicy'/>\u907f\u5f00\u9ad8\u901f</div><div style='height:388px;overflow-x:hidden;overflow-y:auto;width:220px;'><div class='show items' style='padding-bottom:5px;padding-top:10px;'><div class='show qidian'>\u8d77\u70b9\uff1a</div><p class='maptext' style='padding:0 5px;float:left;'><span style='padding:2px 4px;font-weight:bold;background-color:#6688ee;color:#fff;'>" + lastTitle + "</span></p></div><div class='show cphr'></div><div id='showresults' style='height:auto;'></div><div class='show items' style='padding-top:5px;'><div class='show zhongdian'>\u7ec8\u70b9\uff1a</div><p class='maptext' style='padding:0 5px;float:left;'><span style='padding:2px 4px;font-weight:bold;background-color:#6688ee;color:#fff;float:left;'>" + endTitle + "</span></p></div></div><div style='display:none;text-align:right;padding-top:6px;'><a href='javascript:;' onclick=\"bus_end(2)\" style='text-decoration:none;'>&lt;&lt;\u91cd\u65b0\u9009\u62e9\u8d77\u70b9</a></div>";
    $("#t2")[0] || ($(".rightcont").empty(), $(".rightcont").html(l), document.getElementById("dvPolicy").onclick = function (d) {
        d = d || window.event;
        d = d.srcElement || d.target;
        "input" == d.tagName.toLowerCase() && (d = d.getAttribute("id").replace("policy", ""), map.clearOverlays(), searchCar(b, a, routePolicy2[d]))
    }); (new BMap.DrivingRoute(map, {
        renderOptions: {
            map: map,
            panel: "showresults"
        },
        policy: d
    })).search(b, a)
},
busOrCar = 1;
function bus_end(b) {
    if (document.getElementById("point_end") && 0 >= $.trim(document.getElementById("point_end").value).length) return !1;
    busOrCar = b;
    keyWord = document.getElementById("point_end") ? document.getElementById("point_end").value : keyWord;
    var a = new BMap.LocalSearch(map, {
        onSearchComplete: function (b) {
            var l = !1;
            if (a.getStatus() == BMAP_STATUS_SUCCESS) {
                if (!(980 <= $(".modelshowdialog").width())) {
                    $(".modelshowdialog").width(980);
                    try {
                        window.parent.refreshDialog.refresh()
                    } catch (m) { }
                }
                map.clearOverlays();
                startResults = b;
                var f = '<div class="qidianlist"><span>\u8bf7\u4ece\u5217\u8868\u4e2d\u9009\u62e9\u6700\u5408\u9002\u7684\u8d77\u70b9</span></div><ul class="allqidian">';
                markersQidianL = 0;
                qidianPoint = [];
                for (var c = 0; c < b.getCurrentNumPois() && 5 >= c; c++) {
                    qidianPoint[c] = [];
                    qidianPoint[c].title = b.getPoi(c).title;
                    qidianPoint[c].address = b.getPoi(c).address;
                    qidianPoint[c].lng = b.getPoi(c).point.lng;
                    qidianPoint[c].lat = b.getPoi(c).point.lat;
                    f += "<li id='" + 1E4 * (c + 1) + "'><span style='background-position:-128px -118px;' class='bg qidianicon'></span><span class='titlefind'>" + b.getPoi(c).title + "<br/>\u5730\u5740\uff1a" + b.getPoi(c).address + "</span><div></div><a href='javascript:;' class='xqdn' onclick=\"changeStart('" + c + "')\">\u9009\u4e3a\u8d77\u70b9</a></li>";
                    if (b.getPoi(c).title == keyWord) {
                        lastTitle = b.getPoi(c).title;
                        l = !0;
                        changeStart(c);
                        break
                    }
                    var r = iconnohover,
                    q = new BMap.Point(startResults.getPoi(c).point.lng, startResults.getPoi(c).point.lat),
                    p = new BMap.Marker(q, {
                        icon: r
                    });
                    markersQidian[c] = p;
                    markersQidianL++;
                    markersQidian[100 * (c + 1)] = 0; (function (a) {
                        p.addEventListener("mouseover",
                        function () {
                            new BMap.Icon("http://nuomi.xnimg.cn/image_new/icon/markers_newnuomi1.png", new BMap.Size(280 < 29 * a ? 12 : 26, 280 < 29 * a ? 16 : 38), {
                                infoWindowAnchor: new BMap.Size(12, 3),
                                imageOffset: new BMap.Size("-" + (280 < 29 * a ? 152 : 34 * a), 280 < 29 * a ? -177 : -73)
                            });
                            this.setIcon(iconhover)
                        });
                        p.addEventListener("mouseout",
                        function () {
                            if (1 == markersQidian[100 * (a + 1)]) return !1;
                            new BMap.Icon("http://nuomi.xnimg.cn/image_new/icon/markers_newnuomi1.png", new BMap.Size(280 < 29 * a ? 12 : 26, 280 < 29 * a ? 16 : 38), {
                                infoWindowAnchor: new BMap.Size(12, 3),
                                imageOffset: new BMap.Size("-" + (280 < 29 * a ? 152 : 29 * a), 280 < 29 * a ? -130 : 4)
                            });
                            this.setIcon(iconnohover)
                        });
                        p.addEventListener("click",
                        function () {
                            for (var b = 0; b < markersQidianL; b++) markersQidian[100 * (b + 1)] = 0,
                            markersQidian[b].setIcon(iconnohover);
                            new BMap.Icon("http://nuomi.xnimg.cn/image_new/icon/markers_newnuomi1.png", new BMap.Size(280 < 29 * a ? 12 : 26, 280 < 29 * a ? 16 : 38), {
                                infoWindowAnchor: new BMap.Size(12, 3),
                                imageOffset: new BMap.Size("-" + (280 < 29 * a ? 152 : 34 * a), 280 < 29 * a ? -177 : -73)
                            });
                            this.setIcon(iconhover);
                            openQidianinfoWindow(a);
                            markersQidian[100 * (a + 1)] = 1
                        })
                    })(c);
                    map.addOverlay(p);
                    map.setCenter(q);
                    map.setZoom(15)
                }
                if (l) return !1;
                $(".rightcont").html(f + "</ul>");
                $(".allqidian li").bind("mouseover",
                function () {
                    map.closeInfoWindow();
                    $(".allqidian li a").hide();
                    $(this).children("a").show();
                    var a = Number($(this).attr("id")) / 1E4 - 1;
                    markersQidian[a].setIcon(iconhover)
                }).bind("mouseout",
                function () {
                    var a = Number($(this).attr("id")) / 1E4 - 1;
                    new BMap.Icon("http://nuomi.xnimg.cn/image_new/icon/markers_newnuomi1.png", new BMap.Size(280 < 29 * a ? 12 : 26, 280 < 29 * a ? 16 : 38), {
                        infoWindowAnchor: new BMap.Size(12, 3),
                        imageOffset: new BMap.Size("-" + (280 < 29 * a ? 152 : 29 * a), 280 < 29 * a ? -130 : 4)
                    });
                    1 != markersQidian[100 * (a + 1)] && markersQidian[a].setIcon(iconnohover);
                    $(".allqidian li a").hide()
                });
                openQidianinfoWindow(0);
                markersQidian[100] = 1;
                map.setCenter(new BMap.Point(qidianPoint[0].lng, qidianPoint[0].lat))
            } else return alert("\u6ca1\u6709\u627e\u5230\u60a8\u7684\u8d77\u70b9\uff01\u8bf7\u91cd\u65b0\u8f93\u5165\u8d77\u70b9"),
            startResults = null,
            !1
        }
    });
    a.search(keyWord)
}
function openQidianinfoWindow(b) {
    var a = new BMap.InfoWindow("<p><b style='font-size:14px;color:#cc5522;'>" + qidianPoint[b].title + "</b></p><p>\u5730\u5740\uff1a" + qidianPoint[b].address + "</p><p style='text-align:center;padding-top:5px;'><a href='javascript:;' class='xqdn1' onclick=\"changeStart('" + b + "')\">\u9009\u4e3a\u8d77\u70b9</a></p>");
    markersQidian[b].openInfoWindow(a);
    markersQidian[b].setIcon(iconhover);
    $(".allqidian li a").hide();
    $("#" + 1E4 * (b + 1)).children("a").show()
}
var lastQidianIndex = 0;
function changeStart(b) {
    lastTitle = qidianPoint[b].title;
    lastPoint = new BMap.Point(qidianPoint[b].lng, qidianPoint[b].lat);
    1 == busOrCar ? searchBus(lastPoint, thisPoint, routePolicy[0]) : searchCar(lastPoint, thisPoint, routePolicy[0])
}
function exchangeBC(b) {
    map.clearOverlays();
    busOrCar = b;
    changeStart(lastQidianIndex)
}
map.centerAndZoom(new BMap.Point(addrs[firstPoint].longitude, addrs[firstPoint].latitude), 17);
NMMap();