$(function(){
    function isIE () {
        var is_opera = /opera/i.test(navigator.userAgent); 
        return (/msie/i.test(navigator.userAgent) && !is_opera);
    }
    function createElement(tag,type,src){
        var x = document.createElement(tag);
        x.type = type;
        tag == 'script' ? x.src = src : '';
        if (tag == 'link') {
            x.href = src;
            x.rel="stylesheet";
        }
        return x;
    }
    
    function loadFile () {
        var head = document.getElementsByTagName('head')[0];
        if (!isIE() || ($.browser.msie && $.browser.version === "10.0")) {
            var loginJs = createElement('script','text/javascript','scripts/login.js?548225');
            var passportJs = createElement('script','text/javascript','scripts/passport.js?548225');
            var loginCss = createElement('link','text/css','styles/login.css?v=548225');

            head.appendChild(loginJs);
            head.appendChild(passportJs);
            head.appendChild(loginCss);
        }
        var yloginJs = createElement('script','text/javascript','scripts/ylogin.js?548225');
        head.appendChild(yloginJs);
    }
    var logo = $('#logo a');
    var link = logo.attr('href');
    logo.attr('href',link + window.location.search);
    loadFile();
});

