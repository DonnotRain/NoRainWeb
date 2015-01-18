var PlugFunctions = {};
PlugFunctions.Login = {
    Vars:
    {
        gCurTheme: 0, //当前主题
        //所有主题
        aTheme: [{
            bgall: '/Content/images/login/bgall2.png',
            bgpic: '/Content/images/login/bgpic2.png'
        }, {
            bgall: '/Content/images/login/bgall1.png',
            bgpic: '/Content/images/login/bgpic1.png'
        }]
    } //end Vars

    , init: function () {
        //解决iframe首页嵌套登录页的问题
        if (window.parent != window) {
            window.parent.location.href = window.location.href;
        }
        //上下居中
        currentPlug.autoInCenter();

        // 随机背景
        var nRandom = currentPlug.fRandom(100); // 这里返回 [0,99] 
        currentVars.gCurTheme = nRandom % currentVars.aTheme.length;
        currentPlug.fThemeChange(currentVars.gCurTheme);
        //背景轮询
        setInterval(currentPlug.fNextTheme, 10000);


        $('#txtUserName').focus();
        //$('#txtUserName').val("admin");
        //$('#txtPwd').val("1");
        //$('#txtCode').val("HWMT");

        //登录
        $('#btnLogin').click(currentPlug.btnLogin);

        $(window).resize(function () {
            currentPlug.autoInCenter();
        });

        //验证码
        $('#imgValidateCode').click(function () {
            $(this).attr('src', "/Authorize/ValidateCode/" + Math.floor(Math.random() * 10000));
        });

        //enter
        $(document).keypress(function (e) {
            var key = e.which;
            if (key == 13) {
                e.preventDefault();
                $('#btnLogin').click();
            }
        });

        //前一主题
        $('#prevTheme').click(function () {
            currentPlug.fPrevTheme();
        });
        //下一主题
        $('#nextTheme').click(function () {
            currentPlug.fNextTheme();
        });
    } //end init

    /*根据情况判断上下是否需要居中*/
    , autoInCenter: function () {
        var dh = $(document).height();
        if (dh > 744) {
            $('#mainConent').css('top', (dh - 744) / 2);
        }
        else {
            $('#mainConent').css('top', 0);
        }
    } //end autoInCenter

    , btnLogin: function () {
        if ($('#txtUserName').val() == "") {

            Messenger().post({ message: "请填写用户名", type: "error", hideAfter: 3 });
            $('#txtUserName').focus();
            return;
        }
        if ($('#txtPwd').val() == "") {

            Messenger().post({ message: "请填写密码", type: "error", hideAfter: 3 });
            $('#txtPwd').focus();
            return;
        }
        if ($('#txtValidateCode').val() == "") {

            Messenger().post({ message: "请填写验证码", type: "error", hideAfter: 322 });
            $('#txtValidateCode').focus();
            return;
        }
        $('#btnLogin').prop("disabled", true);
        $('#btnLogin').html("登录中...");

        $.CommonAjax({
            url: rootPath + "/Authorize/Login",
            type: "post",
            data: {
                userName: $('#txtUserName').val().toUpperCase(),
                pwd: $('#txtPwd').val(),
                corpCode: $('#txtCode').val(),
                validateCode: $('#txtValidateCode').val(),
                url: returnUrl
            },
            success: function (backData) {
                if (backData.IsSuccess) {
                    $.cookie("UserName", backData.Data.Name, { path: '/' });
                    $.cookie("UserId", backData.Data.Id, { path: '/' });
                    $.cookie("CorpCode", backData.Data.CorpCode, { path: '/' });
                    $.cookie("CorpName", backData.Data.CorpName, { path: '/' });
                    if (backData.Data.ReturnUrl) {
                        location.href = backData.Data.ReturnUrl;
                    }
                    else {
                        location.href = "/Home/Index";
                    }
                }
                else {
                    Messenger().post({ message: backData.Message, type: "error", hideAfter: 3 });
                }
            },
            complete: function (data, textStatus) {
                $('#btnLogin').prop("disabled", false);
                $('#btnLogin').html("登  录");
            }
        });
    }

    /*换肤翻页*/
    , fThemeChange: function (index) {
        $('#center .bgall').css('background-image', 'url(' + currentVars.aTheme[index].bgall + ')');
        $('#center .bgpic').css('background-image', 'url(' + currentVars.aTheme[index].bgpic + ')');
    }

    , fNextTheme: function (n) {
        n = n || 1;
        currentVars.gCurTheme += n;
        if (currentVars.gCurTheme < 0) {
            currentVars.gCurTheme = currentVars.aTheme.length - 1;
        } else if (currentVars.gCurTheme >= currentVars.aTheme.length) {
            currentVars.gCurTheme = 0;
        }
        currentPlug.fThemeChange(currentVars.gCurTheme);
    }

    , fPrevTheme: function () {
        currentPlug.fNextTheme(-1);
    }

    /*返回小于或等于指定数字的最大整数*/
    , fRandom: function (nLength) {
        return Math.floor(nLength * Math.random());
    }

}  //end login

var currentPlug = PlugFunctions.Login;
var currentVars = currentPlug.Vars;
