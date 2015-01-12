function is_ie(){
    return navigator.userAgent.indexOf('MSIE')>0;
}
function is_ie_8(){
    return navigator.userAgent.indexOf('MSIE 8')>0 || navigator.userAgent.indexOf('MSIE 7')>0;
}
function is_chrome(){
    return navigator.userAgent.indexOf('Chrome')>0;
}
function is_firefox(){
    return navigator.userAgent.indexOf('Firefox')>0;
}
(function ($) {
    var root = this;
    /**
     * 豌豆荚下载弹层
     */
    var wdDownLoad = function (evt) {
        evt.preventDefault();
        var el = $('a[data-node=wdBtn]')[0];
        if(typeof root.wdapi_apkdl_m === 'function'){
            return root.wdapi_apkdl_m(el);
        }
        $.getScript('http://www.wandoujia.com/api/wdapi.js', function () {
            if(typeof root.wdapi_apkdl_m === 'function'){
                root.wdapi_apkdl_m(el);
            } else {
                window.location.href = el.href;
            }
        });
    };
    /**
     * 蒙板层
     */
    var Mask = function(spec){
        this.mask = $('.dialog-mask');
        if(!this.mask.length){
            this.mask = $('<div class="dialog-mask"></div>').appendTo('body');
        }
        this.clickCb = spec.clickCb || function(){};
        this.bindDOM();
    };
    Mask.prototype = {
        bindDOM: function(){
            var that = this;
            this.mask.click(function(){
                that.hide();
                that.clickCb();
            });
        },
        hide: function(){
            this.mask.hide();
        },
        show: function(){
            if($.browser.msie && parseInt($.browser.version, 10) === 6){ return; }
            this.mask.show();
        }
    };

    /**
     * android下载弹层
     */
    var Dialog = function(){
        var that = this;
        this.dialog = $('#androidDownload');
        this.mask = new Mask({
            clickCb: function(){
                that.hide();
            }
        });
        this.closeEl = this.dialog.find('[data-node=close]');
        this.wdBtn = this.dialog.find('[data-node=wdBtn]');
        this.bindDOM();
    };

    Dialog.prototype = {
        bindDOM: function(){
            var that = this;
            this.closeEl.click(function(){
                that.hide();
            });
            this.wdBtn.click(wdDownLoad);
        },
        show: function(){
            this.mask.show();
            this.dialog.show().addClass('dialog-open');
        },
        hide: function(){
            this.mask.hide();
            this.dialog.hide().removeClass('dialog-open');
        }
    };
    //将弹层引用挂到全局对象上
   // root.AndroidDialog = Dialog;
    var ado;
    root.getAndroidDialog = function () {
        if (!ado) {
            ado = new Dialog();
        }
        return ado;
    };
})(jQuery);

$(function(){
    if(!Array.prototype.indexOf) {
        Array.prototype.indexOf = function(val){
            var value = this;
            for(var i =0; i < value.length; i++){
                if(value[i] == val) return i;
            }
            return -1;
        };
    }
    var $ver=$('#ver');
    if(is_chrome()){
        $ver.addClass('ver-chrome').find('.add-snap').attr('src','styles/images/chrome/p.png');
    } else if(is_firefox()){
        $ver.addClass('ver-firefox').find('.add-snap').attr('src','styles/images/firefox/p.png');
    } else {
        $ver.addClass('ver-ie');
        $ver.find('.btn-pos').text('收藏夹');
        $ver.find('.add-snap').attr('src','styles/images/ie6/p.png');
        if(is_ie_8()){
            $ver.find('.add-snap').attr('src','styles/images/ie8/p.png');
            $ver.addClass('ver-ie_8').find('.func-add em').text('3');
        }
    }

    $ver.fadeIn();

    if(navigator.userAgent.indexOf('MSIE 6')>0){
        $('#videoBtn').hover(function(){
                    $(this).find('b').fadeIn();
                },
                function(){
                    $(this).find('b').fadeOut();
                }
            );
        //min-width:960
        $body = $('body');
        $(window).resize(function(){
            $body.width($body.width<960?960:'auto');
        });
    }
    //$('a[rel=banner-item]').isTab(true);
    var $goTop = $('#goTopBtn').hide();
    var scrollTop=0;
    $(window).scroll(scrollHandler);
    function scrollHandler(){
        scrollTop = Math.max(document.documentElement.scrollTop-200, document.body.scrollTop-250);
        if(navigator.userAgent.indexOf('MSIE 6')<0) scrollTop > 0?$goTop.fadeIn():$goTop.fadeOut();
    };
});
(function($){
    $(function(){
        
        var $bannerTab = $('#bannerTab');
        //轮播控制对象
        var control = $bannerTab.isTab({'duration':12000});
        $('.widget-slide-index').isTab({'auto':true});
        //dialog
//        $dialog = $('.dialog');
//        $dialog.find('.dialog-close').click(hidedialog);
//        $dialogmask=$('<div class="dialog-mask"></div>').appendTo('body').click(hidedialog);
//        $('a[rel="dialog"]').click(function(){
//            $dialogmask.show();
//            (function(_this){
//                $('#'+_this.data('dialogname')).css('display','block').addClass('dialog-open');
//            })($(this));
//            return false;
//        });
//        function hidedialog(){
//            $dialog.hide().removeClass('dialog-open');
//            $dialogmask.hide();
//
//            return false;
//        }
        var androidDialog = window.getAndroidDialog();
        $('.download-btn-android').click(function (evt) {
            evt.preventDefault();
            androidDialog.show();
        });
        //关注条
        var minTop = 5;
        var container = $('#share');
        var maxTop = parseInt(container.css('top'));
        $(window).scroll(function() {
            var scrollT = document.body.scrollTop + document.documentElement.scrollTop;
            if (maxTop - scrollT < minTop) {
                container.css('top', minTop + scrollT);
            } else {
                container.css('top', maxTop);
            }
        });
        //日志
        $('a[class*="logit"]').logIt();
        //首页webclipper
        var btnClass={'noie':'download-btn-webclipper', 'ie':'download-btn-webclipper-1', 'chrome':'download-btn-webclipper-chrome'}, btnAlert={'noie':'把按钮拖拽到书签栏\n即可添加"网页剪报功能"', 'ie':'鼠标右键点击按钮，选择"添加到收藏夹…"\n即可添加"网页剪报功能"'}, btnTip={'chrome':'使用快捷键Ctrl+Shift+B，可打开书签栏','ie':'鼠标右键点击上面按钮，选择菜单中的“添加到收藏夹…”','anquan':'使用快捷键Ctrl+B，可打开收藏栏'}

        var btn=$('#indexWebclipperBtn'), tip=$('#indexWebclipperTip'),alertStr;
        if($.browser.msie){
            alertStr = btnAlert.ie;
            btn.removeClass(btnClass.noie).addClass(btnClass.ie);
            tip.text(btnTip.ie);
        }else{
            alertStr = btnAlert.noie;
            btn.removeClass(btnClass.ie).addClass(btnClass.noie);
            if($.browser.webkit) tip.text(btnTip.chrome);
        }
        if(navigator.userAgent.indexOf('360SE')>0) tip.text(btnTip.anquan);
        //chrome 插件
        if(is_chrome()) {
            alertStr = '';
            $('#webclipperBanner').addClass('extensionBanner');
            btn.removeClass(btnClass.noie, btnClass.ie).addClass(btnClass.chrome);
            tip.parent().hide();
            btn.click(function () {
                var img = new Image();
                img.src = 'http://note.youdao.com/yws/mapi/ilogrpt?method=putwcplog&chrome_clipper=download1';
                return;
            });
            btn.attr('href','http://note.youdao.com/downloads/YoudaoNote-chrome.crx');
            $('#webclipperBanner .play-video').hide();
        }
        if(alertStr) btn.click(function(){
            //if(is_chrome()) return true;
            alert(alertStr);
            return false;
        });
        //introBanner 控制
        var $showIntroBtn = $('.showIntroBtn');
        var $hideIntroBtn = $('.hideIntroBtn');
        var $introBanner = $('.introBanner');

        $showIntroBtn.click(function (evt) {
            evt.preventDefault();
            $introBanner.show();
            $bannerTab.find('.on').addClass('disable');
            control['pause']();
        });
        $hideIntroBtn.click( function (evt) {
            evt.preventDefault();
            $introBanner.hide();
            $bannerTab.find('.disable').removeClass('disable');
            control['play']();
        });
        //hash location直接定位某个tab
        //banner
        var bannerObj = {
            '#windowsBanner': true,
            '#mobileBanner': true,
            '#ipadBanner': true,
            '#webclipperBanner': true
        };
        var hash = window.location.hash;
        if (bannerObj[hash]) {
            $introBanner.hide();
            $bannerTab.find('.disable').removeClass('disable');
            control['pause']();
        }

    });
    $.fn.extend({
        logIt:function(name,src){
            var name=name||'logname';
            var src=src||'log.gif';
            var img=new Image();
            $(this).each(function(){
                var _this=$(this), type=_this.data(name);
                if(type) {
                    _this.live('click', function(){
                        img.src=src+'?type='+_this.data(name) + '&vendor=' + (window.vendor || '');
                    });
                }
            });
            return this;
        },
        isTab:function(param){//start:0, onClass:on, auto:false, duration:7000
            var $introBanner = $('.introBanner');
            var $bannerTab = $('#bannerTab');
            var tabs=$(this);
            var tab=tabs.find('a');
            var wrapper=$(tabs.data('wrapper')).css({'overflow':'hidden'});
            var container=$(tabs.data('container'));
            if(!(wrapper && container)) return this;

            param=param||{};
            var start = param.start || 0;
            var onClass = param.on || 'on';
            var auto=param.auto||false;
            var duration=param.duration||7000;

            var intval;
            var current=start;
            var len=tab.size();
            var w=0;
            var content = container.children().css({'position':'absolute','left':0,'top':0,'display':'none'});

            //锚点
            var hashArr=[];
            tab.each(function(){hashArr.push($(this).attr('href'));});
            var hash = location.hash;
            var hashIndex = hashArr.indexOf(hash) || 0;
            if(!!hash && hashIndex>=0) {
                auto=false;
                start=hashIndex;
                current=start;
            }

            content.each(function(){
                (function(_this){
                    _this.data('left',w);
                    w+=_this.width();
                })($(this));
            });
            //container.width(w);
            content.eq(start).show();

            tab.eq(start).addClass(onClass);
            tab.each(function(i){
                (function(_this){
                    _this.data('id',i);
                    _this.click(function(){
                        if(tabs.attr('id') === 'bannerTab') {
                            goTab(_this.data('id'), true);
                            var disableTabs = $bannerTab.find('.disable');
                            if (disableTabs.length) {
                                play();
                            }
                            $introBanner.hide();
                            disableTabs.removeClass('disable');
                        } else {
                            goTab(_this.data('id'));
                        }
                        _this.blur();
                        return false;
                    });
                })($(this));
            });

            if(auto){
                play();
                content.hover(pause,play);
                tab.hover(pause,play);
            }

            function goTab(i, isIntroBannerShow){
                tab.removeClass(onClass).eq(i).addClass(onClass);
                //container.animate({'margin-left':'-'+content.eq(i).data('left')+'px'});
                ($.browser.msie || isIntroBannerShow) ?content.not('eq['+i+']').hide().eq(i).show():content.not('eq['+i+']').fadeOut().eq(i).fadeIn();
                current=i;
            }
            function nextTab(){
                current++;
                if(current==len) current=0;
                return current;
            }
            function prevTab(){
                current--;
                if(current<0) current=len-1;
                return current;
            }
            function play(){
                intval=window.setInterval(function(){
                    goTab(nextTab());
                },duration);
                //alert('hover');
            }
            function pause(){
                intval=window.clearInterval(intval);
            }

            var control = {
                'pause' : pause,
                'play' : play,
                'prevTab' : prevTab,
                'nextTab' : nextTab
            };

            return control;
        }
    });
})($);
