var yd_get_elem = function(elemId) {
    return document.getElementById(elemId);
};
var _hint = '163,126邮箱可直接登录';
function validate(disableSubmit){
    var form = document.f;
    var name = form.username.value.trim();
    var password = form.password.value;
    var saveLogin = form.savelogin.checked;

    if((name.length < 1 || name == _hint)
        || (password.length < 1)
        || password.length > 25) {
        showWarning1(true);
        return false;
    }

    if(disableSubmit) {
        form.submit.disabled = true;
    }
    if (saveLogin) {
        markYNoteUser();
    }
    return true;
}

function alertSaveLoginNew(cb) {
    var saveWarningElem = yd_get_elem('saveloginwarning');
    saveWarningElem.style.display= cb.checked ? "" : "none";
}
function showWarning(show) {
    if (!show) {
        yd_get_elem('remember_psw').style.display = 'none';
        return;
    }
    var saveLogin = yd_get_elem('savelogin');
    var pos = getElemXY(saveLogin);
    var offsetY =  4 ;
    with(yd_get_elem('remember_psw').style) {
        display = 'block';
        left = (pos.x - 40) + 'px';
        top = (pos.y + saveLogin.offsetHeight + offsetY + 4) + 'px';
    }
}
function showWarning1(show) {
    var tip = yd_get_elem('login_tiparea');
    if (!show) {
        tip.style.display != 'none' && (tip.style.display = 'none')
        return;
    }

    var username = yd_get_elem('username');
    var pos = getElemXY(username);

    var offsetY =  4 ;

    with(tip.style) {
        display = 'block';
        left = (pos.x - 40) + 'px';
        top = (pos.y - 107) + 'px';
    }
}

function alertSaveLogin(cb) {
    if(!cb.checked) return;
    if(!confirm("选中后浏览器将在一个月内保持网易通行证的登录状态，请不要在网吧或公共机房上网时使用。您确认要选中吗？")) {
        cb.checked = false;
    }    
}
function setfocus(){ 
    if (yd_get_elem('username').value !== _hint) {
        yd_get_elem('password').focus();
    } else {
        yd_get_elem('username').style.color = '#bbb';
    }
}
    
document.domain = 'youdao.com';
