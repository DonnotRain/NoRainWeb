var browserName = '';

var getBrowserType = function() {
    var browser = {};
    var ua = window.navigator.userAgent.toLowerCase();
    var hasTrident = ua.indexOf('trident') !== -1;
    browser.firefox = ua.match(/firefox\/([\d\.]+)/);
    browser.ie = ua.match(/msie\s([\d\.]+)/);
    browser.lbbrowser = ua.match(/lbbrowser/);
    browser.chrome = ua.match(/chrome\/([\d\.]+)/);
    if (browser.ie && hasTrident) {
        browser.ie[1] = 9.0;
    }
    return browser;
};

function is360() {
    try {
        var b = navigator.userAgent;
        if (/(firefox|opera|lbbrowser|qqbrowser|tencenttraveler|bidubrowser|alibrowser|maxthon|se [0-9]\.x|greenbrowser|myie2|theworld|avast|comodo|avant)/ig.test(b)) {
            return ""
        }
        if (/(baidu|soso|sogou|youdao|jike|google|bing|msn|yahoo)/ig.test(b)) {
            return ""
        }
        if (/(360|qihu)/ig.test(b)) {
            return /MSIE/.test(b) ? "ie" : "chrome"
        }
        if (/chrome/ig.test(b)) {
            if (subtitleEnabled() && microdataEnabled() && scopedEnabled()) {
                return "chrome"
            }
        } else {
            if (/safari/ig.test(b)) {
                return ""
            }
        }
        if (/msie/ig.test(b) && !addSearchProviderEnabled()) {
            try {
                ("" + window.external) == "undefined"
            } catch (a) {
                return "ie"
            }
        }
        return ""
    } catch (a) {}
}

function subtitleEnabled() {
    return "track" in document.createElement("track")
}

function scopedEnabled() {
    return "scoped" in document.createElement("style")
}

function addSearchProviderEnabled() {
    return !!(window.external && typeof window.external.AddSearchProvider != "undefined" && typeof window.external.IsSearchProviderInstalled != "undefined")
}

function microdataEnabled() {
    var c = document.createElement("div");
    c.innerHTML = '<div style="display:none;" id="microdataItem" itemscope itemtype="http://example.net/user"><p>My name is <span id="microdataProperty" itemprop="name">Jason</span>.</p></div>';
    document.body.appendChild(c);
    var b = document.getElementById("microdataItem");
    var d = document.getElementById("microdataProperty");
    var a = true;
    a = a && !! ("itemValue" in d) && d.itemValue == "Jason";
    a = a && !! ("properties" in b) && b.properties.name[0].itemValue == "Jason";
    if ( !! document.getItems) {
        b = document.getItems("http://example.net/user")[0];
        a = (a && b.properties.name[0].itemValue == "Jason")
    }
    document.body.removeChild(c);
    return a;
};

var judgeByF2e = function() {
    if (is360() === 'chrome') {
        browserName = '360';
    } else if (browser.lbbrowser && browser.chrome) {
        browserName = '猎豹';
    } else if (browser.firefox) {
        browserName = 'Firefox';
    } else if (browser.ie) {
        browserName = 'IE';
    } else if (browser.chrome) {
        var ua = window.navigator.userAgent.toLowerCase();
        if (ua.indexOf('metasr') !== -1) {
            browserName = '搜狗';
        } else {
            browserName = 'Chrome';
        }
    }
};

var getBrowserName = function() {
    var browser = getBrowserType();
    window.browser = browser;
    judgeByF2e();
    return browserName;
};