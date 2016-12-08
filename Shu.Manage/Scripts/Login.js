function getimgcode() {
    var getimagecode = document.getElementById("cphContent_getcode");
    var rand = Math.random();
    getimagecode.src = "VerifyCode.aspx?randnum=" + rand + "";
}



$(function () {
    $("#txtUser").focus();
    //document.onkeydown = function (e) {
    //    var ev = document.all ? window.event : e;
    //    if (ev.keyCode == 13) {
    //        loginSubmit();
    //    }
    //}
})



/*
* 控制验证码转换为大写
*/


//重置
function regit() {
    $('input[id*=txt_UserName]').val("");
    $('input[id*=txt_Pwd]').val("");

}

/*
* 用户登录
*/

function loginSubmit() {
    if (true) {
        var username = $("#txtUser").val();
        //alert(username)
        var pwd = $("#txtPwd").val();
        //var verifyCode = $('input[id*=txt_VerifyCode]').val();
        if (username == "") {
            alert('请输入用户名!')
            $("#txtUser").focus();
            return false;
        }
        if (pwd == "") {
            alert('请输入密码!')
            $("#txtPwd").focus();
            return false;
        }
//        if (verifyCode == "") {
//            alert("请输入验证码!");
//            $('input[id*=txt_VerifyCode]').focus();
//            return false;
//        }
    }
}
