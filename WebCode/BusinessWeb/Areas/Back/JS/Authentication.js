var layout = {
    logOut: function () {
        $.removeCookie('adminname');
        $.removeCookie('userid');
        location.reload(true);
    }
}

$(function () {
    $("#btnLogin").click(function () {

        if ($("#txtUserName").val() && $("#pwdPassword").val()) {
            var returnUrl = $("#hideUrl").val();
            var loginUrl = "../Authorize/AjaxLogin";

            //设置按钮不可用
            $("#btnLogin").prop("disabled", true);
            $("#btnLogin").html("<img src=\"/Content/Images/loging.gif\" />登录中…");
            //显示正在加载中...
            $.post(loginUrl, { username: $("#txtUserName").val(), password: $("#pwdPassword").val() }, function (data) {
                /* 可用状态
              <i class="icon-key"></i>
                                            登录
            */
                $("#btnLogin").prop("disabled", false);
                $("#btnLogin").html("<i class=\"icon-key\"></i>登录");

                if (data && data.IsSuccess) {
                    //保存登录Cookies
                    //判断是否勾选了记住密码
                    //勾选了记住密码，则设置Cookies有效期为30天
                    //adminname
                    //否则，不设置cookies有效期
                    if ($("#ckRememberPwd").prop("checked")) {
                        setCookie("adminname", encodeURIComponent($("#txtUserName").val()), "d30");
                        setCookie("userid", data.Data.userid, "d30");
                    }
                    else {
                        setCookie("adminname", encodeURIComponent($("#txtUserName").val()));
                        setCookie("userid", data.Data.userid);

                    }
                    if (returnUrl) {
                        location.href = returnUrl;
                    }
                    else {
                        location.href = homeUrl;
                    }
                          
                }
                else {
                    $.gritter.add({
                        // (string | mandatory) the heading of the notification
                        title: '登录失败',
                        // (string | mandatory) the text inside the notification
                        text: data.Message,
                        class_name: 'gritter-error' + ' gritter-light',
                        time: 3000,
                    });
                }
            });
        }
        else {
            $.gritter.add({
                // (string | mandatory) the heading of the notification
                title: '请输入完整的账号和密码!',
                // (string | mandatory) the text inside the notification
                text: '账号和密码输入不完整。请检查后重试。',
                class_name: 'gritter-warning',
                time: 3000,
            });

        }
    });
})