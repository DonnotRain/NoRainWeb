var _params=  null;
var _complete = false;
var _check_remote = false;
var _checkLoginUrl = "/auth/cq.json?app=web";
var _isLogin = false;
var _changeUrsUrl = "/auth/urs/login.json?app=web";
var _logOutUrl = "/auth/reset?app=web";
var _logingDialogIsShowing = false;
var inviteid = getQueryString("invitation");
var inviter = false;
var deleteCookie = function(name) {
    document.cookie = name+"=;path=/;domain=youdao.com;expires="+new Date(new Date().getTime()-1000).toGMTString()+";";
};
var checkFromYws = function(cb) {
    $.ajax({
        'type':'GET',
        'url': _checkLoginUrl,
        'data':'',
        'async':false,
        'cache':false,
        'success': function (data) {
            if(data.user && data.user.login) {
                _isLogin = true;
                cb(data);
            } else {
                loginFaild();
            }
        },
        'error':function() {
            loginFaild();
        }
    });
};


var checkFromUrs = function (cb) {
    $.ajax({
        'type':'GET',
        'url': _changeUrsUrl,
        'data':'',
        'async':false,
        'cache':false,
        'success': function (data) {
            if(data.user && data.user.login) {
                setCookieYNote('USER_INFO',data.user.id + '|' + data.user.name + '|' + data.user.email);
            }
            checkFromYws(cb);
        },
        'error':function() {
            loginFaild();
        }
    });
};

var loginFaild = function(){
    $(function(){
        bindDialog();
        inviteid && !_logingDialogIsShowing && getInviter({
            'id': inviteid,
            'successCb': function(data){
                data = data || {};
                inviter = data.inviter;
                if (!inviter) { return; }
                $('#inviter').html(inviter);
                changeLoginView('invite');
                $('#loginDialog').show();
                setfocus();
            },
            'errorCb': function(){}
        });
    });
};

var loginSuccess = function () {

};

var getSearch = function(name) {
    if (null == _params) {
        var sec = window.location.search;
        if (sec.indexOf('?') == 0) {
            sec = sec.substring(1);
        }
        var params = sec.split('&');
        _params = {};
        for (var i=0;i<params.length;i++) {
            var kv = params[i].split('=');
            _params[kv[0]] = kv[1]?kv[1]:null;
        }
    }
    return _params[name];
};

var getBackUrl = function(direct) {
    var loc = window.location;
    var url = loc.protocol+'//'+loc.host;
    var pathname = loc.pathname;

    url+=pathname;

    if (direct) {
        while (url.lastIndexOf('.html') !== -1) {
            url = url.substr(0, url.lastIndexOf('/'));
        }

        if (url[url.length - 1] !== '/') {
            url += '/';
        }

        url+='web/?version=548225';
    }
    return url;
};
var hideDialog = function() {
    $('#login_tiparea').css('display','none');
    $('#loginDialog').hide();
    _logingDialogIsShowing = false;
};
var emptyDialog = function() {
    $('#savelogin').attr('checked',false);
    $('#password').val('');
};
var getCookie = function(name) {
    var cookie = document.cookie;
    var cookies = cookie.split(';');
    for (var i=0;i<cookies.length;i++) {
        var kv = cookies[i].split('=');
        if ($.trim(kv[0]) == name) {
            return kv[1] && kv[1].length?kv[1]:null;
        }
    }
    return false;
};
var setCookieYNote = function(k,v,d) {
    if (!d) {
        d = new Date(new Date().getTime()+3600*24*3600*1000).toGMTString();
    }
    document.cookie = k+"="+v+";expires="+d+";path=/;domain=.note.youdao.com";
};
var checkLogin = function() {
    var userInfo = getCookie('USER_INFO');
    if (getCookie('YNOTE_LOGIN')) {
        var userinfo = {};
        var userArray = userInfo.split('|');
        userinfo.id = userArray[0];
        userinfo.name = userArray[1];
        userInfo.email = userArray[2];
        return userinfo;
    }
    return false;
};

var startLoginMonitor = function() {
    if (checkLogin()) {

    }
};
var bgLoginLazyLoad =function(){
  var el=document.createElement("iframe");
  el.id="yloginframe";
  el.src="http://account.youdao.com/login?back_url=http%3A%2F%2Faccount.youdao.com%2Fblank&relogin=1&service=myth&sync=1";
  if (!/msie/i.test(navigator.userAgent)) {
      el.style.display="none";
  }
  else {
      el.style.cssText="width:1px;height:1px";
  }
  el.onload = function() {
      afterLogin();
      _complete = true;
  };
  document.body.appendChild(el);
};
var checkFrameLoad = function() {
   if (document.getElementById('yloginframe').readyState === "complete") {
        if (!_complete) {
            afterLogin();
        }
        return;
   }
   setTimeout(checkFrameLoad,500);
};
var addToFav = function(el,params) {
    params = params || {};
    var title = params.title || document.title;
    var url = params.url || window.location.href;
    if (window.sidebar && window.sidebar.addPanel) {
        window.sidebar.addPanel(title,url,"","");
    }
    else if (document.all) {
        try {
            window.external.addFavorite(url,title);
        } catch(e) {
            try {
                window.external.addToFavoritesBar(url,title);
            }
            catch(e) {}
        }
    }
    else if (window.opera && window.print) {
        window.external.AddFavorite(url,title);
    }
};
var showAddToFav = function() {
    if ($('.fav-alert').length) return;
    var _isGecko = /gecko/i.test(navigator.userAgent);
    var _isWebKit = /webkit/i.test(navigator.userAgent);
    var bodyClass = _isWebKit ? 'ua-webkit' : (_isGecko ? 'ua-gecko': '');
    bodyClass.length && $(document.body).addClass(bodyClass);
    if (_isGecko || _isWebKit) {
        $('<div class="fav-alert">按Crtl+D把有道云笔记加入收藏夹，下次轻松访问。<a id="notalert" class="notalert">不再提示</a></div>').appendTo(document.body);
    }
    else {
        $('<div class="fav-alert">把有道云笔记加入收藏夹，下次轻松访问。<a class="fav-btn" style="behavior:url(#default#homepage)"><b>加为收藏</b></a><a id="notalert" class="notalert">不再提示</a></div>').appendTo(document.body);
    }
    if ($('.fav-alert').find('.fav-btn').length) {
        $('.fav-alert').find('.fav-btn').click(function() {
            addToFav(this);
            $('.fav-alert').hide();
            var i = new Image();
            i.src = "/yws/mapi/ilogrpt?method=pwl&s=favorite";
        });
    }
    $('#notalert').click(function() {
        $('.fav-alert').hide();
        markFav();
    });
};
var markFav = function() {
    setCookieYNote('YNOTE_FAV',1);
};
var isFront = function() {
    var path = window.location.pathname;
    if (path == '/' || path.toLowerCase() == "/index.html") {
        return true;
    }
    return false;
};
var isWebClipper = function() {
    var path = window.location.pathname;
    if (path.toLowerCase() == '/web-clipper.html') {
        return true;
    }
    return false;
};
var isDialogBind = false;

/**
 * @param spec {
 *  {String}        id      invitation id
 *  {Function}      successCb
 *  {Function}      errorCb
 * }
 */
var getInviter = (function(){
    var defSuccess = function(data){
        data = data || {};
        var type = 'normal';
        inviter = data.inviter;
        if (inviter) {
            $('#inviter').html(inviter);
            type = 'invite';
        }
        changeLoginView(type);
        $('#loginDialog').show();
        setfocus();
    };
    var defError = function(){
        changeLoginView('normal');
        $('#loginDialog').show();
        setfocus();
    };
    return function(spec){
        var id = spec.id;
        var successCb = spec.successCb || defSuccess;
        var errorCb = spec.errorCb || defError;
        $.ajax({
            'type': 'GET',
            'dataType': 'json',
            'url': '/yws/mapi/user?method=getinviter&keyfrom=web&invitation=' + id,
            'cache': false,
            'success': successCb,
            'error': errorCb
        });
    };
})();

var openLoginDialog = function(){
    emptyDialog();
      /*last login id*/
       var pinfo = getCookie('P_INFO');
       if (pinfo) {
          var username = pinfo.split('|')[0];
          if (username && username.toLowerCase() != "null") {
              $('#username').val(username).css('color', '#000000');
          }
      }
      else {
           $('#username').css('color', '#BBBBBB');
      }
      if (!inviteid) {
          changeLoginView('normal');
          $('#loginDialog').show();
          return;
      }
      if(inviter){
           $('#inviter').html(inviter);
           changeLoginView('invite');
           $('#loginDialog').show();
      } else if(inviter === false){
          getInviter({'id': inviteid});
      } else {
           changeLoginView('normal');
           $('#loginDialog').show();
      }
      _logingDialogIsShowing = true;
};

var bindDialog = function() {
  if (isDialogBind) {
      return;
  }
  $('#back_url').val(getBackUrl(true));
  $('.login-btn').click(function(evt) {
      evt.preventDefault();
      openLoginDialog();
  });
   $('#loginDialog .dialog-close').click(function() {
      hideDialog();
  });
$('.btns-row a').click(function () {
        logLogin('openId');
    });

  if (getSearch('keyfrom') == 'client' && !isWebClipper()) {
      $('.login-btn').click();
  }
  // if (!getCookie('YNOTE_FAV') && isFront()) {
  //     showAddToFav();
  //     markFav();
  // }
  isDialogBind = true;
};
var addSearchToLink = function(clz,k,v) {
   $(clz).each(function() {
       if ($(this).attr('href')) {
           var link = $(this).attr('href');
           if (/auto=1/.test(link)) {
               return;
           }
           if (link.indexOf('?') != -1) {
               if (link.indexOf('?') == (link.length - 1)) {
                   link += ''+k+'='+encodeURIComponent(v);
               }
               else {
                   link += '&'+k+'='+encodeURIComponent(v);
               }
           }
           else {
               link += '?'+k+'='+encodeURIComponent(v);
           }
           $(this).attr('href',link);
       }
   });
};

var makeYnoteUser = function () {
    setCookieYNote('YNOTE_USER',1);
};

var changeSuccessUI = function(){
    var html =
      '<a href="web/setting?type=setmeal" class="login-vip-link logit" data-logname="index.siteNav.vipLink" target="_blank"><img src="images/vip-logo.png" alt="">扩容套餐</a><span class="vip-link-blank">&nbsp;|&nbsp;</span>' +
      '<a href="web/?version=548225" class="login-btn" onclick="makeYnoteUser();">进入网页版</a><span class="vip-link-blank">&nbsp;|&nbsp;</span>'+
      '<a href="javascript:;" onclick="logout(event);">退出</a>';
    $('#loginPanel').html(html);
    $('.mac-login').html('<a href="web/?version=548225" class="login-btn" onclick="makeYnoteUser();">进入网页版</a>');
    $('.mac-reg').html('<a href="javascript:;" onclick="logout(event);">退出</a>');
    addSearchToLink('.nav a','auto','1');
    // if (!getCookie('YNOTE_FAV') && isFront()) {
    //     showAddToFav();
    //     markFav();
    // }
};

var loadScript1 = function(src){
    var x = document.createElement('script');
    x.type = 'text/javascript';
    x.src = src;
    return x;
};
var sendSms = function(checkFirstLogin, userName) {
    $.ajax({
        url: '/yws/mapi/device?method=notify',
        data: '',
        type: 'GET',
        success: function () {
            YKit.extra.storage.set(checkFirstLogin, 'logined');
            setTimeout(function() {
                window.location.href = "web/?version=548225";
            }, 100);
        },
        error: function () {
            logLogin(userName + '-setLocalStorageFail');
            setTimeout(function() {
                window.location.href = "web/?version=548225";
            }, 100);
        }
    });
}
var checkSmsNotify = function () {
    $.ajax({
        url: '/yws/mapi/user?method=get&keyfrom=web',
        data: '',
        type: 'GET',
        success: function (res) {
            var userName = res.uid;
            if (userName) {
              var data = userName.split('|');
              if (data && data[0]) {
                  userName = data[0];
              }
            } else {
              window.location.href = "web/?version=548225";
            }
            userName = userName.replace('@','_');
            var checkFirstLogin = userName + '-firstLogin';
            var value = YKit.extra.storage.get(checkFirstLogin);
            if (value === 'null' || value === null) {
                if (res.wn) {
                    sendSms(checkFirstLogin, userName);
                } else {
                    window.location.href = "web/?version=548225";
                }
            } else {
                window.location.href = "web/?version=548225";
            }
        }
    });
};

var onLogined = function (autoLogin) {
    $('#submitBtn').disabled = false;
    hideDialog();
    changeSuccessUI();
    if (inviteid) {
      invitation({
          'success': function(data){
                data = data || {};
                if(data.error){ return; }
                var loginJs = loadScript1('scripts/successDialog.js?366163');
                $('head').append(loginJs);
          },
          'error': function(){
              checkSmsNotify();
          }
      });
    } else if (window.preventAutoLogin) {
        return;
    } else {
      if(autoLogin) {
        checkSmsNotify();
      } else if (getSearch('auto') == 1 || window.vendor !== undefined || (!getCookie('YNOTE_USER') && !!getCookie('P_INFO')) || (getSearch('keyfrom') == 'client' && isWebClipper())) {
        return;
      } else {
            checkSmsNotify();
      }
    }
    onLogined = function(){};
};

var doSuccessLogin = function() {
    window.vendor = getSearch('vendor');
    if (window.preventAutoLogin || getSearch('auto') == 1 ||
        window.vendor !== undefined ||
        (!getCookie('YNOTE_USER') && !!getCookie('P_INFO')) ||
        (getSearch('keyfrom') == 'client' && isWebClipper())
    ) {
          changeSuccessUI();
    }
    else {
        checkSmsNotify();
    }
};
var afterLogin = function () {
  if (getCookie('YNOTE_LOGIN')) {
      doSuccessLogin();
      return true;
  }
  bindDialog();
  return false;
};

var doLogin = function() {
  if (getCookie('YNOTE_LOGIN')) {
      onLogined();
      return true;
  }
  else {
    $(function() {
        bgLoginLazyLoad();
        checkFrameLoad();
    });
  }
};

var logLogin = function(type) {
    var i=new Image();
    i.src="/yws/mapi/ilogrpt?method=putwcplog&keyfrom=wcp&login=" + type;
    return true;
};

var logEntry = function() {
    var type = getSearch('entry');
    if(!type) {
        return true;
    }
    var i = new Image();
    i.src ='/yws/mapi/ilogrpt?method=pwl&entry=' + type ;
    return true;
};

var logout = function (event) {
     $.ajax({
            'type' : 'GET',
            'url' : _logOutUrl,
            'data' : '',
            'async' : false,
            'cache' : false,
            'success' : function (data) {
                if (data.urs) {
                    location.href = 'http://account.youdao.com/logout?' +
                    'service=ynote.web&' + 'back_url=' + location.protocol +
                    '//' + location.host;
                } else {
                    location.href = location.protocol + '//' + location.host;
                }
            },
            'error' : function () {}
        });
    if(event.preventDefault) {
        event.preventDefault();
    } else {
        return false;
    }
};
logEntry();
checkFromUrs(doLogin);


//监控第三方账号登录

var listenCookie = (function () {
    var timer = null;
    return function (callback) {
        clearInterval(timer);
        timer = setInterval(function () {
            var islogin = getCookie('YNOTE_LOGIN');
            if (islogin) {
                callback();
                clearInterval(timer);
            }
        }, 3000);
    };
})();

listenCookie(onLogined);


// setTimeout(function(){
 //      checkFromUrs(doLogin);
 //    }, 3000);
