/**
 * @author ZhouJiequn | zhoujq@rd.netease.com
 * 有道云笔记首页专用工具集
 */
var YKit = (function(){
    var that = {};
    /**
     * util工具集注册函数
     * @param {String} ns 命名空间
     * @param {Object} maker
     */
    var register = function(ns, maker){
        var nsList = ns.split('.');
        var step = that;
        var k = null;
        while (k = nsList.shift()) {
            if (nsList.length) {
                if (step[k] === undefined) {
                    step[k] = {};
                }
                step = step[k];
            } else {
                if (step[k] === undefined) {
                    try {
                        step[k] = maker(that);
                    }catch(e){}
                }
            }
        }
    }
    that.register = register;
    return that;
})();

/**
 * MD5工具集
 * @id YKit.extra.MD5
 * @param {String} data
 */
YKit.register('extra.MD5', function(yk){
    var MD5_T = new Array(0x00000000, 0xd76aa478, 0xe8c7b756, 0x242070db, 0xc1bdceee, 0xf57c0faf, 0x4787c62a, 0xa8304613, 0xfd469501, 0x698098d8, 0x8b44f7af, 0xffff5bb1, 0x895cd7be, 0x6b901122, 0xfd987193, 0xa679438e, 0x49b40821, 0xf61e2562, 0xc040b340, 0x265e5a51, 0xe9b6c7aa, 0xd62f105d, 0x02441453, 0xd8a1e681, 0xe7d3fbc8, 0x21e1cde6, 0xc33707d6, 0xf4d50d87, 0x455a14ed, 0xa9e3e905, 0xfcefa3f8, 0x676f02d9, 0x8d2a4c8a, 0xfffa3942, 0x8771f681, 0x6d9d6122, 0xfde5380c, 0xa4beea44, 0x4bdecfa9, 0xf6bb4b60, 0xbebfbc70, 0x289b7ec6, 0xeaa127fa, 0xd4ef3085, 0x04881d05, 0xd9d4d039, 0xe6db99e5, 0x1fa27cf8, 0xc4ac5665, 0xf4292244, 0x432aff97, 0xab9423a7, 0xfc93a039, 0x655b59c3, 0x8f0ccc92, 0xffeff47d, 0x85845dd1, 0x6fa87e4f, 0xfe2ce6e0, 0xa3014314, 0x4e0811a1, 0xf7537e82, 0xbd3af235, 0x2ad7d2bb, 0xeb86d391);
    
    var MD5_round1 = new Array(new Array(0, 7, 1), new Array(1, 12, 2), new Array(2, 17, 3), new Array(3, 22, 4), new Array(4, 7, 5), new Array(5, 12, 6), new Array(6, 17, 7), new Array(7, 22, 8), new Array(8, 7, 9), new Array(9, 12, 10), new Array(10, 17, 11), new Array(11, 22, 12), new Array(12, 7, 13), new Array(13, 12, 14), new Array(14, 17, 15), new Array(15, 22, 16));
    
    var MD5_round2 = new Array(new Array(1, 5, 17), new Array(6, 9, 18), new Array(11, 14, 19), new Array(0, 20, 20), new Array(5, 5, 21), new Array(10, 9, 22), new Array(15, 14, 23), new Array(4, 20, 24), new Array(9, 5, 25), new Array(14, 9, 26), new Array(3, 14, 27), new Array(8, 20, 28), new Array(13, 5, 29), new Array(2, 9, 30), new Array(7, 14, 31), new Array(12, 20, 32));
    
    var MD5_round3 = new Array(new Array(5, 4, 33), new Array(8, 11, 34), new Array(11, 16, 35), new Array(14, 23, 36), new Array(1, 4, 37), new Array(4, 11, 38), new Array(7, 16, 39), new Array(10, 23, 40), new Array(13, 4, 41), new Array(0, 11, 42), new Array(3, 16, 43), new Array(6, 23, 44), new Array(9, 4, 45), new Array(12, 11, 46), new Array(15, 16, 47), new Array(2, 23, 48));
    
    var MD5_round4 = new Array(new Array(0, 6, 49), new Array(7, 10, 50), new Array(14, 15, 51), new Array(5, 21, 52), new Array(12, 6, 53), new Array(3, 10, 54), new Array(10, 15, 55), new Array(1, 21, 56), new Array(8, 6, 57), new Array(15, 10, 58), new Array(6, 15, 59), new Array(13, 21, 60), new Array(4, 6, 61), new Array(11, 10, 62), new Array(2, 15, 63), new Array(9, 21, 64));
    
    var MD5_F = function(x, y, z){
        return (x & y) | (~ x & z);
    };
    var MD5_G = function(x, y, z){
        return (x & z) | (y & ~ z);
    };
    var MD5_H = function(x, y, z){
        return x ^ y ^ z;
    };
    var MD5_I = function(x, y, z){
        return y ^ (x | ~ z);
    };
    
    var MD5_round = new Array(new Array(MD5_F, MD5_round1), new Array(MD5_G, MD5_round2), new Array(MD5_H, MD5_round3), new Array(MD5_I, MD5_round4));
    
    var MD5_pack = function(n32){
        return String.fromCharCode(n32 & 0xff) +
        String.fromCharCode((n32 >>> 8) & 0xff) +
        String.fromCharCode((n32 >>> 16) & 0xff) +
        String.fromCharCode((n32 >>> 24) & 0xff);
    };
    
    var MD5_unpack = function(s4){
        return s4.charCodeAt(0) | (s4.charCodeAt(1) << 8) |
        (s4.charCodeAt(2) << 16) |
        (s4.charCodeAt(3) << 24);
    };
    
    var MD5_number = function(n){
        while (n < 0) 
            n += 4294967296;
        while (n > 4294967295) 
            n -= 4294967296;
        return n;
    };
    
    var MD5_apply_round = function(x, s, f, abcd, r){
        var a, b, c, d;
        var kk, ss, ii;
        var t, u;
        
        a = abcd[0];
        b = abcd[1];
        c = abcd[2];
        d = abcd[3];
        kk = r[0];
        ss = r[1];
        ii = r[2];
        
        u = f(s[b], s[c], s[d]);
        t = s[a] + u + x[kk] + MD5_T[ii];
        t = MD5_number(t);
        t = ((t << ss) | (t >>> (32 - ss)));
        t += s[b];
        s[a] = MD5_number(t);
    };
    
    var MD5_hash = function(data){
        var abcd, x, state, s;
        var len, index, padLen, f, r;
        var i, j, k;
        var tmp;
        
        state = new Array(0x67452301, 0xefcdab89, 0x98badcfe, 0x10325476);
        len = data.length;
        index = len & 0x3f;
        padLen = (index < 56) ? (56 - index) : (120 - index);
        if (padLen > 0) {
            data += "\x80";
            for (i = 0; i < padLen - 1; i++) 
                data += "\x00";
        }
        data += MD5_pack(len * 8);
        data += MD5_pack(0);
        len += padLen + 8;
        abcd = new Array(0, 1, 2, 3);
        x = new Array(16);
        s = new Array(4);
        
        for (k = 0; k < len; k += 64) {
            for (i = 0, j = k; i < 16; i++, j += 4) {
                x[i] = data.charCodeAt(j) | (data.charCodeAt(j + 1) << 8) |
                (data.charCodeAt(j + 2) << 16) |
                (data.charCodeAt(j + 3) << 24);
            }
            for (i = 0; i < 4; i++) 
                s[i] = state[i];
            for (i = 0; i < 4; i++) {
                f = MD5_round[i][0];
                r = MD5_round[i][1];
                for (j = 0; j < 16; j++) {
                    MD5_apply_round(x, s, f, abcd, r[j]);
                    tmp = abcd[0];
                    abcd[0] = abcd[3];
                    abcd[3] = abcd[2];
                    abcd[2] = abcd[1];
                    abcd[1] = tmp;
                }
            }
            
            for (i = 0; i < 4; i++) {
                state[i] += s[i];
                state[i] = MD5_number(state[i]);
            }
        }
        
        return MD5_pack(state[0]) + MD5_pack(state[1]) + MD5_pack(state[2]) +
        MD5_pack(state[3]);
    };
    return function(data){
        var i, out, c;
        var bit128;
        
        bit128 = MD5_hash(data);
        out = "";
        for (i = 0; i < 16; i++) {
            c = bit128.charCodeAt(i);
            out += "0123456789abcdef".charAt((c >> 4) & 0xf);
            out += "0123456789abcdef".charAt(c & 0xf);
        }
        return out;
    };
});

/**
 * cookie工具集
 * @id YKit.extra.cookie
 * @param {String} key
 * @param {String} value
 * @param {Object} options
 */
YKit.register('extra.cookie', function(yk){
    return function(key, value, options){
        var $ = jQuery;
        var result = null;
        // key and value given, set cookie...
        if (arguments.length > 1 &&
        (value === null || typeof value !== 'object')) {
        
            options = $.extend({}, options);
            
            if (value === null) {
                options.expires = -1;
            }
            
            if (typeof options.expires === 'number') {
                var days = options.expires, t = options.expires = new Date();
                t.setDate(t.getDate() + days);
            }
            
            result = [encodeURIComponent(key), '=', options.raw ? String(value) : encodeURIComponent(String(value)), // use expires attribute, max-age is not supported by IE
 options.expires ? '; expires=' + options.expires.toUTCString() : '', options.path ? '; path=' + options.path : '', options.domain ? '; domain=' + options.domain : '', options.secure ? '; secure' : ''].join('');
            
            document.cookie = result;
            
            return result;
        }
        
        // key and possibly options given, get cookie...
        options = value || {};
        var decode = function(s){
            return s;
        };
        
        if (!options.raw) {
            decode = decodeURIComponent;
        }
        
        result = new RegExp('(?:^|; )' + encodeURIComponent(key) + '=([^;]*)').exec(document.cookie);
        
        return result ? decode(result[1]) : null;
    };
});

/**
 * 网易通行证登录JavaScript版
 * @id YKit.dao.login
 * @author barton.ding
 * date: 2010-12-14
 *
 * @example
 *
 * - 登录 -
 * dao.login.init({
 *    params: {
 *        username: 'dingzongqiu@126.com',
 *        password: '0123456789',
 *    },
 *    start: function(){
 *        // todo: then start login
 *    },
 *    end: function(){
 *        // todo: then end login
 *    },
 *    success: function(username){
 *        // todo: then login success
 *    },
 *    error: function(errorMsg, username){
 *        // todo: then login success
 *    }
 * }).run();
 *
 * - 判断是否登录 -
 * dao.login.check()
 * 已登录返回 true，未登录返还 false
 */
YKit.register('dao.login', function(yk){
    var $ = jQuery;
    // 登录失败的错误类型
    var ErrorType = {
        '411': '次数限制：您的IP地址有异常登录行为',
        '412': '次数限制：您尝试的次数已经太多,请过一段时间再试',
        '414': '次数限制：您的IP登录失败次数过多,请稍后再试',
        '415': '次数限制：您今天登录错误次数已经太多,请明天再试',
        '416': '次数限制：您的IP今天登录过于频繁，请稍后再试',
        '417': '次数限制：您的IP今天登录次数过多，请明天再试',
        '418': '次数限制：您今天登录次数过多,请明天再试',
        '419': '次数限制：您的登录操作过于频繁，请稍候再试',
        '420': '用户名不存在',
        '422': '帐号被锁定，请您解锁后再登录!',
        '428': '账号异常',
        '460': '密码不正确',
        '500': '系统繁忙，请您稍后再试！',
        '503': '系统维护，请您稍后再试！'
    },
    // 通行证登录URL
    authUrl = 'https://reg.163.com/logins.jsp', 
    // 默认参数配置
    defaultParams = {
        // 用户名
        username: '',
        // 口令，明文或密码
        password: '',
        /*
         * 0 表示password经过md5加密，对用户密码做MD5前需要对密码中的'和\做转换，其前再加上一个\字符。
         * 1 表示password为明文。
         */
        type: '0',
        // 验证通过后重定向到的URL，如果此参数为空，转向URS登录成功的页面
        url: '/loginCallback.html?v=548225',
        // 验证失败后重定向到的URL，如果此参数为空，转向URS登录失败的页面。
        url2: '/loginCallback.html?v=548225',
        // 产品标识
        product: 'note',
        // 是否保留用户登录信息，设置cookie:NTES_PASSPORT。1保留，0不保留
        savelogin: '0',
        // 还需要设置的其他跨域cookie的域，多个域之间以,分隔，如：domains=163.com,126.com目前domains限制为：
        domains: '163.com,126.com,yeah.net,youdao.com,yodao.com',
        noRedirect: 1
    },
    // 是否登录的 cookie key
    COOKIE_KEY = 'NTES_LOGINED';
    
    // 更新默认参数的url
    (function(){
        var p = window.location.protocol,
            h = window.location.host,
            pth = window.location.pathname,
            urlp = [p, '//', h];
        
        // 如果项目不是部署在ROOT下，需特殊处理
        if (pth.indexOf('atranslate') !== -1) {
            urlp.push('/atranslate');
        }
        
        defaultParams['url'] = urlp.join('') + defaultParams['url'];
        defaultParams['url2'] = urlp.join('') + defaultParams['url2'];
    })();
    
    var NeteaseLogin = function(){
        // 登录所需参数
        this.params = {};
        // 成功登录的回调
        this.success = null;
        // 登录失败的回调
        this.error = null;
        // 开始登录的回调
        this.start = null;
        // 登录结束的回调
        this.end = null;
        // 保存登录中使用到的iframe
        this.iframe = null;
    };
    
    NeteaseLogin.prototype = {
        init: function(conf){
            // 生成一份全新的参数列表，防止默认参数被污染
            this.params = $.extend({}, defaultParams, conf.params);
            this.md5Password();
            // 登录成功的回调设置
            if ($.isFunction(conf['success'])) {
                this.success = conf['success'];
            }
            // 登录失败的回调设置
            if ($.isFunction(conf['error'])) {
                this.error = conf['error'];
            }
            // 开始登录的回调设置
            if ($.isFunction(conf['start'])) {
                this.start = conf['start'];
            }
            // 登录完毕的回调设置
            if ($.isFunction(conf['end'])) {
                this.end = conf['end'];
            }
            return this;
        },
        md5Password: function(){
            var pswd = this.params['password'];
            if (pswd.length === 0) { return; }
            pswd = pswd.replace(/(\'|\\)/g, '\\$1');
            this.params['password'] = yk.extra.MD5(pswd);
        },
        check: function(){
            var cookie = yk.extra.cookie;
            //document.domain = window.location.hostname; //'youdao.com';
            var cv = cookie(COOKIE_KEY);
            var result = (cv === null || cv === 'false') ? false : true;
            if (result) {
                return true;
            } else {
                // step 1. 10s 检查一次(访问任意的页面, 刚好getUserAlertNums需要访问)
                var params = {
                    call: "getUserAlertNums",
                    t2: "" + ((+new Date()) / 10000)
                };
                $.ajax({
                    url: '/userview',
                    type: 'GET',
                    dataType: 'json',
                    data: params,
                    timeout: 30000,
                    cache: true, // NO CACHE
                    success: function(data){
                    }
                });
                
                if (cookie("NTES_SESS") != null) {
                    return true;
                }
                return false;
            }
        },
        run: function(){
            // 已登录情况下,不再重复登录
            /*
             if(this.check()){
             return this;
             }*/
            if (this.iframe === null) {
                this.iframe = $('<iframe style="position:absolute;left:0;top:0;width:0;height:0;"></iframe>').appendTo('body');
            }
            // 执行开始登录的回调
            if ($.isFunction(this.start)) {
                this.start();
            }
            // 采用 iframe 方式开始登录
            this.iframe.attr('src', this.generateUrl());
            
            return this;
        },
        generateUrl: function(){
            var ps = [];
            for (var key in this.params) {
                ps[ps.length] = key + '=' + window.encodeURIComponent(this.params[key]);
            }
            ps[ps.length] = 'timestamp=' + $.now();
            return authUrl + '?' + ps.join('&');
        },
        callbackHandler: function(urlParams){
            var cookie = yk.extra.cookie;
            var domain = this.getDomain();
            // 登录完毕的回调
            if ($.isFunction(this.end)) {
                this.end();
            }
            //console.log(urlParams);
            // 登录失败
            if (urlParams['errorUsername']) {
                if (domain.length > 0) {
                    cookie(COOKIE_KEY, null, {
                        path: '/',
                        domain: domain
                    });
                }
                
                if ($.isFunction(this.error)) {
                    this.error.call(
                        this,
                        urlParams['errorType'],
                        ErrorType[urlParams['errorType']],
                        urlParams['errorUsername']
                    );
                    if (urlParams['errorType'] === '428') {
                        location.href = decodeURIComponent(urlParams['url']) + '&url=' + encodeURIComponent(location);
                    }
                }
            } else { // 登录成功
                // 将Cookie写入当前的Host下面(Session Cookie)
                cookie(COOKIE_KEY, 'true', {
                    path: '/',
                    domain: domain
                });
                if ($.isFunction(this.success)) {
                    this.success.call(
                        this,
                        urlParams['username']
                    );
                }
            }
        },
        getDomain: function(){
            var domain = "", hostname = window.location.hostname;
            
            if (hostname.indexOf(".163.com") != -1) {
                domain = ".163.com";
            } else if (hostname.indexOf(".youdao.com") != -1) {
                domain = ".youdao.com";
            }
            return domain;
        }
    };
    return new NeteaseLogin();
});

/**
 * 获取滚动条位置
 * @id YKit.extra.getScrollPos
 */
YKit.register('extra.getScrollPos', function(yk){
    return function(){
        var dd = document.documentElement;
        var db = document.body;
        return {
            top: Math.max(window.pageYOffset || 0, dd.scrollTop, db.scrollTop),
            left: Math.max(window.pageXOffset || 0, dd.scrollLeft, db.scrollLeft)
        };
    };
});

/**
 * LocalStorage
 * @id YKit.extra.storage
 * @alias STK.core.util.storage
 * @author WK | wukan@staff.sina.com.cn
 */
YKit.register('extra.storage', function(yk){
    var objDS = window.localStorage;
    if (objDS) {
        return {
            get: function(key){
                return unescape(objDS.getItem(key));
            },
            set: function(key, value, exp){
                objDS.setItem(key, escape(value));
            },
            del: function(key){
                objDS.removeItem(key);
            },
            clear: function(){
                objDS.clear();
            },
            getAll: function(){
                var l = objDS.length, key = null, ac = [];
                for (var i = 0; i < l; i++) {
                    key = objDS.key(i), ac.push(key + '=' + this.getKey(key));
                }
                return ac.join('; ');
            }
        };
    }
    else 
        if (window.ActiveXObject) {
            store = document.documentElement;
            STORE_NAME = 'localstorage';
            try {
                store.addBehavior('#default#userdata');
                store.save('localstorage');
            } 
            catch (e) {
            //throw "don't support userData";
            }
            
            return {
                set: function(key, value){
                    store.setAttribute(key, value);
                    store.save(STORE_NAME);
                },
                get: function(key){
                    store.load(STORE_NAME);
                    return store.getAttribute(key);
                },
                del: function(key){
                    store.removeAttribute(key);
                    store.save(STORE_NAME);
                }
            };
        }
        else {
            return {
                get: function(key){
                    var aCookie = document.cookie.split("; "), l = aCookie.length, aCrumb = [];
                    for (var i = 0; i < l; i++) {
                        aCrumb = aCookie[i].split("=");
                        if (key === aCrumb[0]) {
                            return unescape(aCrumb[1]);
                        }
                    }
                    return null;
                },
                set: function(key, value, exp){
                    if (!(exp && typeof exp === date)) {
                        exp = new Date(), exp.setDate(exp.getDate() + 100000);
                    }
                    document.cookie = key + "=" + escape(value) + "; expires=" + exp.toGMTString();
                },
                del: function(key){
                    document.cookie = key + "=''; expires=Fri, 31 Dec 1999 23:59:59 GMT;";
                },
                clear: function(){
                    var aCookie = document.cookie.split("; "), l = aCookie.length, aCrumb = [];
                    for (var i = 0; i < l; i++) {
                        aCrumb = aCookie[i].split("=");
                        this.deleteKey(aCrumb[0]);
                    }
                },
                getAll: function(){
                    return unescape(document.cookie.toString());
                }
            };
        }
});
/**
 * 对话框基础组件
 * @id YKit.ui.dialog
 * @param {String} template
 * @param {Object} spec
 */
YKit.register('ui.dialog', function(yk){
    var $ = jQuery;
    var Dialog = function(temp, spec){
        if(!(this instanceof Dialog)){
            return new Dialog(temp, spec);
        }
        this.dialog = $(temp);
        this.titleEl = $('[data-node=title]', this.dialog);
        this.contentEL = $('[data-node=content]', this.dialog);
        this.closeEL = $('[data-node=close]', this.dialog);
        this.conf = $.extend({
            'title': '提示',
            'content': '',
            'timeout': 0
        }, spec || {});
        this.hide();
        this.setTitle();
        this.setContent();
        this._bindEvent();
        $('body').append(this.dialog);
    };
    Dialog.prototype = {
        _bindEvent: function(){
           var that = this;
           this.closeEL.click(function(){
               that.hide();
           });
        },
        
        show: function(){
            this.dialog.show();
        },
        
        hide: function(){
            this.dialog.hide();
        },
        
        setTitle: function(str){
            this.titleEl.html(str || this.conf.title);
        },
        
        setMiddle: function(){
            var sPos = yk.extra.getScrollPos();
            var wH = $(window).height();
            var wW = $(window).width();
            var dH = this.dialog.height();
            var dW = this.dialog.width();
            var left = (wW - dW)/2 + 'px';
            var top = sPos.top + (wH - dH)/2 + 'px';
            this.setPosition({
                top: top,
                left: left
            });
        },
        
        setContent: function(html){
            this.contentEL.html('');
            this.contentEL.append(html || this.conf.content);
        },
        
        setPosition: function(pos){
            this.dialog.css('top', pos.top);
            this.dialog.css('left', pos.left);
        }
    };
    
    return Dialog;
});

YKit.register('extra.formatYDCookie', function(yk){
    return function (spec) {
        $.ajax({
            'type':'GET',
            'url': '/auth/cq.json?app=web',
            'success': function (data) {
                if(data.user && data.user.login) {
                    typeof spec.success === 'function' && spec.success(data);
                } else {
                    typeof spec.error === 'function' && spec.error();
                }
            },
            'error':function() {
                typeof spec.error === 'function' && spec.error();
            }
        });
    };
});

YKit.register('extra.checkNetEaseUser', function(yk){
    var cookie = yk.extra.cookie;
    var formatYDCookie = yk.extra.formatYDCookie;
    return function (spec) {
        $.ajax({
            'type':'GET',
            'cache': false,
            'url': '/auth/urs/login.json?app=web',
            'success': function (data) {
                if(!data.login) {
                    typeof spec.error === 'function' && spec.error();
                    return;
                }
                cookie('USER_INFO',data.id + '|' + data.name + '|' + data.email, {
                    'expires': new Date(new Date().getTime()+3600*24*3600*1000),
                    'path': '/',
                    'domain': '.note.youdao.com'
                });
                formatYDCookie(spec);
            },
            'error':function() {
                typeof spec.error === 'function' && spec.error();
            }
        });
    };
});

YKit.register('dom.placeholder', function (yk) {
    var hasPlaceholderSupport = (function() {
        var temp = 'placeholder' in document.createElement('input');
        hasPlaceholderSupport = function () {
            return temp;
        };
        return temp;
    })();
    return function (spec) {
        var that = {};
        var el = spec.el;
        var wmark = spec.wmark;
        var getVal = function () {
            var val = $.trim(el.val());
            return val === wmark ? '' : val;
        };
        var setVal = function (val) {
            if(val) {
                el.css('color', '#000000');
            } else {
                el.css('color', '#c9c9c9');
                el.val(wmark);
            }
        };
        if (hasPlaceholderSupport) {
            el.attr('placeholder', wmark);
        } else {
            setVal(getVal());
            el.focus(function () {
                el.css('color', '#000000');
                getVal() || el.val('');
            });
            el.blur(function () {
                setVal(getVal());
            });
        }
        that.getVal = getVal;
        return that;
    };
});