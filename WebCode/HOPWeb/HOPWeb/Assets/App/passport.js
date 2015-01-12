var inviteid = getQueryString("invitation");
var __html=''+
'<div id="loginDialog">' +
'    <div class="dialog-hd">' +
'        <h2><img src="/styles/images/logo-login.png" alt="" />登录有道云笔记</h2>' +
'        <a href="" class="close"></a>' +
'    </div>' +
'    <div class="dialog-bd">' +
'        <div id="invite-login-msg" class="login-dialog-top-tip" style="display:none;">' +
'              <p><span id="inviter">您的好友</span>&nbsp;邀请您一同使用有道云笔记！立刻登录</br>即可免费获得2G空间哦！</p>'+
'        </div>'+
'        <div class="right">' +
'            <form action="">' +
'                <div class="row">' +
'                    <div class="row-hd">' +
'                        <label for="user">帐&nbsp;号</label>' +
'                    </div>' +
'                    <div class="row-bd">' +
'                        <input type="text" class="text" tabindex="1" id="username" name="username" autocomplete="off" style="ime-mode:disabled" value="163,126邮箱可直接登录">' +
'                    </div>' +
'                </div>' +
'                <div class="row">' +
'                    <div class="row-hd">' +
'                        <label for="pass">密&nbsp;码</label>' +
'                    </div>' +
'                    <div class="row-bd">' +
'                        <input type="password" id="password" name="pass" tabindex="2" class="text">' +
'                    </div>' +
'                </div>' +
'                <div class="row indent">' +
'                    <div class="row-hd">' +
'                    </div>' +
'                    <div class="row-bd">' +
'                        <label for="savelogin" onMouseOver="showWarning(true);" onMouseOut="showWarning(false);" ><input tabindex="13" type="checkbox" id="savelogin" name="savelogin" value="1">自动登录</label>'+
'                        <a href="http://reg.163.com/getpasswd/RetakePassword.jsp" target="_blank">忘记密码</a>' +
'                    </div>' +
'                </div>' +
'                <div class="row btn-row">' +
'                    <div class="row-hd"></div>' +
'                    <div class="row-bd">' +
'                        <input class="btn blue-btn" id="submitBtn" type="submit" value="登    录">' +
'                    </div>' +
'                </div>' +
'            </form>' +
'        </div>' +
'        <div class="left">' +
'            <div id="openid" class="openid">' +
'                <a href="" class="sina">微博账号登录</a>' +
'                <a href="" class="qq">QQ账号登录</a>' +
'                <a href="" class="tqq">腾讯微博账号登录</a>' +
'                <a href="" class="ireg">注册网易通行证</a>' +
'            </div>' +
'        </div>' +
'    </div>' +
'</div>' +
''+
'<!--YOUDAO ACCOUNT TIPS HTML-->'+
'<div class="remember_psw1" id="remember_psw" >'+
'    <div class="login-tiparea-top"></div>'+
'    <a title="关闭" onClick="this.parentNode.style.display = \'none\';" href="javascript:void(0)" class="login-tiparea-close"></a>'+
'    <p>为了您的信息安全，请不要在网吧或公用电脑上使用此功能！</p>'+
'</div>'+
'<div class="login-tiparea hide" id="login_tiparea">'+
'    <p class="error-state">登录名或密码错误</p>'+
'    <p class="p-desc">1、通行证请输入邮箱全称</p>'+
'    <p class="p-desc2">163,126邮箱可直接登录</p>'+
'    <p class="p-desc">2、请检查邮箱地址及密码是否正确填写</p>'+
'    <a href="javascript:showWarning1(false)" title="关闭" class="login-tiparea-close">关闭</a>'+
'    <div class="login-tiparea-bottom"></div>'+
'</div>'+
'<!--div id = "passportloginfail">'+
'    <p class="">登录失败，用户名或密码错误!</p>'+
'</div-->'+
'<div id = "passportusernamelist" class="domainSelector" '+
'   style="display: none;"><table width="100%" cellspacing=0 cellpadding=0>   '+
'    <tr><td class="title">请选择或继续输入...</td></tr>'+
''+
'    <tr><td>&nbsp;</td></tr>'+
'</table></div>'+
'<div class="login-frame-wrapper"><iframe name="loginframe" onload="typeof afterLogin == \'function\'?afterLogin():undefined;"></iframe></div>'+
        '';

$(document.body).append(__html);
$('#loginDialog .close').click(function(evt) {
    evt.preventDefault();
     $('#login_tiparea').css('display','none');
     $('#loginDialog').hide();
     _logingDialogIsShowing = false;
});
$('#password').keypress(function(evt){
   // if ($.browser.msie) {
    if (evt.which === 13) {
        evt.preventDefault();
        logLogin('login2');
        submitLogin();
    }
  //  }
});
var changeLoginView = function(type){
    var isInvite = type === 'invite';
    var display = isInvite ? '' : 'none';
    $('#invite-login-msg').css('display', display);
    $('#invite-login-register').css('display', display);
    $('#invite-login-inner').attr('class', isInvite ? 'dialog-col' : '');
    $('#loginDialog').attr('class', isInvite
        ? 'dialog-login-invited'
        : 'login-dialog');

    isInvite && $('#loginDialog').css({
        'top': '25%'
    });
};

var authUrl = {
        sina: '/auth/tsina?app=web&f=true&pe=1',
        qq: '/auth/qq/cqq?app=web&f=true&pe=1',
        tqq: '/auth/qq/wqq?app=web&f=true&pe=1',
        ireg: 'http://reg.163.com/reg/reg.jsp'
    };

var delegateAuth = function (e) {
    var evt = window.event ? window.event : e,
        target = evt.target || evt.srcElement,
        currentTarget = e ? e.currentTarget : this;
    evt.preventDefault && evt.preventDefault();
    if (target.tagName === 'A') {
        var className = target.className;
        window.open(authUrl[className]);
    }
    return false;
};

var openBtnBind = function () {
    var openId = document.getElementById('openid');
    if (openId && !openId.onclick) {
        openId.onclick = delegateAuth;
    }
};

openBtnBind();

var submitLogin = function(evt){
    var def = '163,126邮箱可直接登录';
    var name = $('#username').val().trim();
    var pwd = $('#password').val();
    var savelogin = !!$('#savelogin').attr('checked');
    if(!name || !pwd || name === def || pwd.length > 25){
         showWarning1(true);
         return false;
    }
    $('#submitBtn').disabled = true;
   if (savelogin) {
       makeYnoteUser();
   }
    logLogin('login2');
    YKit.dao.login.init({
        params: {
            username: name,
            password: pwd,
            savelogin: savelogin ? '1' : '0',
            product: 'note'
        },
        success: function(username){
            onLogined(true);
        },
        error: function(code, msg){
            var url = ['https://reg.163.com/logins.jsp'
                , '?username='+encodeURIComponent(name)
                , '&url='+encodeURIComponent(window.location.href.toString())];
            window.location.href = url.join('');
        }
    }).run();
    return false;
};

$('#submitBtn').on('click', submitLogin);

String.prototype.trim = function () {
    return this.replace(/(^\s*)|(\s*$)/g, "");
};
String.prototype.endsWith = function (pattern) {
    var d = this.length - pattern.length;
    return d >= 0 && this.lastIndexOf(pattern) === d;
};
var getElemXY = function (elem) {
    var sumTop = 0;
    var sumLeft = 0;
    // 参考prototype.js cumulativeOffset 增加对(elem != null)的检查
    while (elem) {
        sumLeft += elem.offsetLeft  || 0;
        sumTop += elem.offsetTop  || 0;
        elem = elem.offsetParent;
    }
    return {x:sumLeft, y:sumTop};
};
var isIE = (navigator&&navigator.userAgent.toLowerCase().indexOf("msie")!=-1);
var Passport = (function() {
    var _hint = '163,126邮箱可直接登录';
    var _inputElem = null;
    var _nameListElem = null;
    var userAgent = window.navigator.userAgent;
    var isPad = userAgent && userAgent.indexOf("iPad") != -1;
    var _isFirstTime = true; //!isPad; // iPad程序不能自动Trigger Input
    // 状态说明: -1表示无效状态, 0表示选中第一个,
    var _curSelIndex = -1;
    var DOMAINS = [
        "163.com",
        "126.com",
        "yeah.net",
        "vip.163.com",
        "vip.126.com",
        "popo.163.com",
        "188.com",
        "qq.com",
        "yahoo.com.cn",
        "sina.com",
        "gmail.com"];
    var _domainSelElem = null;

    // 获取给定elem的绝对坐标

    var $ = function(id) {
        return document.getElementById(id);
    };

    var _checkKeyDown = function (event) {
        event = event || window.event;
        if(_curSelIndex < 0) {
            _curSelIndex = 0;
            _setFocus(false);
        }
        var keyCode = event.keyCode;
        if (keyCode == 38 || keyCode == 40) {
            _setFocus(true);
            _upSelectIndex(keyCode == 38);
            _setFocus(false);
        }
    };
    var _checkEmailFinished = function() {
        var email = _inputElem.value.trim();
        for (var i = 0; i < DOMAINS.length; i++) {
            if (email.endsWith(DOMAINS[i])) {
                return true;
            }
        }
        return false;
    }
    /**
     * KeyUp事件中不处理Enter之外的其他Event
     */
    var _keyupProc = function (event) {
        event = event || window.event;
        var keyCode = event.keyCode;
        Passport.suggestNames();
        // 13 为Enter(确定选择了某一个domain)
        if (keyCode == 13) {
            // 如果已经输入完成的Email, 则需要输入Enter是把焦点切换到password
            if (!_checkEmailFinished()) {
                _doSelect();
            }
            // 优先考虑有iPhone效果的Case
            var password = $("password_password") || $("password");
            if (password) {
                password.focus();
            }
        } else {
            Passport.suggestNames();
        }
    };

    /**
     * KeyCode: 38: Up Arrow
     *          40: Down Array
     */
    var _keypressProc = function (event) {
        event = event || window.event;
        var keyCode = event.keyCode;
        // 如果是Enter, 则return
        if (keyCode == 13) {
            _preventEvent(event);
            return;
        } else {

            if (_isFirstTime) {
                _isFirstTime = false;
                if (_inputElem.value.trim() == _hint) {
                    _inputElem.value = "";
                    _inputElem.style.color = "#000000";
                }
            }
        }

        // 如果是Up or Down, 则
        if (keyCode == 38 || keyCode == 40) {

            _preventEvent(event);

            _setFocus(true);
            _upSelectIndex(keyCode == 38);
            _setFocus(false);

        } else {
            // 这段代码作用?
            if (keyCode == 108 || keyCode == 110 || keyCode == 111 || keyCode == 115) {
                setTimeout("Passport.suggestNames()", 20);
            }
        }
    };
    /** 将当前选择的TD的背景清除(isClear:true), 或者设置新的color */
    var _setFocus = function (isClear) {
        var x = _findTdElement(_curSelIndex);
        if (!x) return;
        try {
            x.style.backgroundColor = isClear ? "white": "#D5F1FF";
        } catch(e) {}
    };

    var _findTdElement = function (index) {
        try {
            var x = _nameListElem.firstChild.rows;
            for (var i = 0; i < x.length; ++i) {
                if (x[i].firstChild.idx == index) {
                    return x[i].firstChild;
                }
            }
        } catch (e) { }
        return false;
    };
    /**
     * isUp: 表明key是Up Key, 否则为Down Key
     */
    var _upSelectIndex = function (isUp) {
        var index = _curSelIndex;
        if (_nameListElem.firstChild == null) {
            return;
        }
        var x = _nameListElem.firstChild.rows;
        if (x.length == 0) {
            return;
        }

        var i;
        for (i = 0; i < x.length; ++i) {
            if (x[i].firstChild.idx == index) {
                break;
            }
        }
        // 如果是Up, 则
        if (isUp) {
            if (i == 0) {
                _curSelIndex = x[x.length - 1].firstChild.idx;
            } else {
                _curSelIndex = x[i - 1].firstChild.idx;
            }
        } else {
            if (i >= x.length - 1) {
                _curSelIndex = x[0].firstChild.idx;
            } else {
                _curSelIndex = x[i + 1].firstChild.idx;
            }
        }

    };

    /**
     * 隐藏DomainSelElem, 并且从domainSelElem中选择一个emailAddress
     */
    var _doSelect = function () {
        _hide(_domainSelElem);
        if (_inputElem.value.trim() == "") {
            return;
        }
        var currentUsernameTd = _findTdElement(_curSelIndex);
        if (currentUsernameTd) {
            // 关键点:
            _inputElem.value = currentUsernameTd.innerHTML;
        }
        _setFocus(false);
        // _curSelIndex = -1;

    };
    var _handle = function() {
        _domainSelElem = $("passportusernamelist");
        _nameListElem = _domainSelElem.firstChild.rows[1].firstChild;
        _curSelIndex = 0;
        _inputElem.onclick = function(){
            if (_isFirstTime && _inputElem.value.trim() == _hint) {
                _isFirstTime =  false;
                _inputElem.value = "";
                _inputElem.style.color = "#000000";
            }
        };
        _inputElem.onfocus = function(){
            if (!_isFirstTime && _inputElem.value.trim() == _hint) {
                _inputElem.value = "";
                _inputElem.style.color = "#000000";
            }

            // 关闭Warning
            try {
                window.showWarning1 && showWarning1(false);
            } catch (e) {
            }
        }

        var password = $("password_password") || $("password");
        password && (password.onfocus = function(){
            try {
                window.showWarning1 && showWarning1(false);
            } catch (e) {
            }
        });

        // 当失去焦点的时候，选择domain
        _inputElem.onblur = function () {
            var userInput = _inputElem.value.trim();
            if (userInput == "" || userInput == _hint) {
                _inputElem.value = _hint;
                _inputElem.style.color = "#bbbbbb";
                return;
            }

            // 生成username以及hostname
            var pos;
            if ((pos = userInput.indexOf("@")) < 0) {
                var username = userInput;
                var hostname = "";
            } else {
                var username = userInput.substr(0, pos);
                var hostname = userInput.substr(pos + 1, userInput.length);
            }
            // 如果hostname为空，则选择当前选择的domain, 默认为第一个
            if (hostname == "") {
                _inputElem.value = username + "@" + DOMAINS[_curSelIndex > 0 ? _curSelIndex : 0];
                _hide(_domainSelElem);
                return;
            }

            // 如果有有合适的domain, 则返回
            for (var i = 0; i < DOMAINS.length; ++i) {
                if (hostname == DOMAINS[i]) {
                    _hide(_domainSelElem);
                    return;
                }
            }

            _doSelect();
        };

        // 添加事件处理函数(Firefox)
        if (_inputElem.addEventListener) {
            // Hack For Safari
            var appVers = navigator.appVersion;
            var keypress = appVers.match(/Konqueror|Safari|KHTML/) ? 'keydown': "keypress";

            _inputElem.addEventListener(keypress, _keypressProc, false);
            _inputElem.addEventListener("keyup", _keyupProc, false);
        } else {
            _inputElem.attachEvent('onkeydown', _keypressProc); // _checkKeyDown
            // _inputElem.attachEvent("onkeypress", _keypressProc);
            _inputElem.attachEvent("onkeyup", _keyupProc);
            window.attachEvent("onunload", function() {
                _inputElem = null;
                _nameListElem = null;
                _domainSelElem = null;
            });
        }
    };
    var _preventEvent = function (event) {
        event.cancelBubble = true;
        event.returnValue = false;
        if (event.preventDefault) {
            event.preventDefault();
        }
        if (event.stopPropagation) {
            event.stopPropagation();
        }
    };
    var _hide = function(elem) {
        elem.style.display = "none";
    };
    var _createElem = function(tagName) {
        return document.createElement(tagName);
    };
    return {
       bind: function (input) {
          _inputElem = input;
          _handle();
       }, suggestNames:function () {
           var userInput = _inputElem.value.trim();
           // 防止XSS漏洞
           if (userInput == "" || (userInput.indexOf("<") != -1) || (userInput.indexOf(">") != -1)) {
               _hide(_domainSelElem);
               return;
           }

           // 生成username以及hostname
           var pos;
           if ((pos = userInput.indexOf("@")) < 0) {
               var username = userInput;
               var hostname = "";
           } else {
               var username = userInput.substr(0, pos);
               var hostname = userInput.substr(pos + 1, userInput.length);
           }

           // 生成usernames
           var usernames = [];
           if (hostname == "") {
                for (var i = 0; i < DOMAINS.length; ++i) {
                    usernames.push(username + "@" + DOMAINS[i]);
                }
            } else {
                for (var i = 0; i < DOMAINS.length; ++i) {
                    if (DOMAINS[i].indexOf(hostname) == 0) {
                        usernames.push(username + "@" + DOMAINS[i]);
                    }
                }
            }

            if (usernames.length == 0) {
                _hide(_domainSelElem);
                _curSelIndex = -1;
                return;
            }
            function setPos() {
                var xy = getElemXY(_inputElem);
                // 登录层隐藏的时候不在调整suggest层
                // 最好还是用原生js判断 保证一致性
                if (jQuery(_inputElem).is(':hidden')) {
                    return;
                }
                with(_domainSelElem.style) {
                    left = (xy.x - (isIE ? 1 : 0)) + "px";
                    top = (xy.y + _inputElem.offsetHeight - (isIE ? 1 : 0)) + "px";
                    width = (_inputElem.offsetWidth -2) + "px";
                    zIndex = "2000";
                    paddingRight = "0";
                    paddingLeft = "0";
                    paddingTop = "0";
                    paddingBottom = "0";
                    backgroundColor = "white";
                    display = "block";
                };
            }
            setPos();

            if (isIE) {
               window.attachEvent('onresize', setPos);
            } else {
               window.addEventListener('resize', setPos);
            }

            // 如设置属性?
            var myTable = _createElem("TABLE");
            myTable.width = "100%";
            myTable.cellSpacing = 0;
            myTable.cellPadding = 3;

            var tbody = _createElem("TBODY");
            myTable.appendChild(tbody);

            for (var i = 0; i < usernames.length; ++i) {
                var tr = _createElem("TR");
                var td = _createElem("TD");
                td.nowrap = "true";
                td.align = "left";
                td.innerHTML = usernames[i];
                td.idx = i;

                td.onmouseover = function () {
                    // this means current td
                    _setFocus(true);
                    _curSelIndex = this.idx;
                    _setFocus(false);
                    this.style.cursor = "pointer";
                };

                td.onmouseout = function () {};

                td.onclick = function () {
                    _doSelect();
                };

                tr.appendChild(td);
                tbody.appendChild(tr);
            }

            _nameListElem.innerHTML = "";
            _nameListElem.appendChild(myTable);
            _setFocus();
        }
    };
})();
Passport.bind(document.getElementById('username'));
