
/*--------菜单点击操作---------*/

//二级菜单被点击 显示三级菜单
function ulClick(e) {

    var id = $(e).attr("id");

    $(e).find('span').css('font-weight', 'bold')

    if ($('#3_' + id + '_scroll').css('display') == 'block') {

        $('#3_' + id + '_scroll').css('display') == 'none';
    }
    else {

        $('#3_' + id + '_scroll').css('display') == 'block';
    }



    $('div[id^=3_' + id + '_]').each(function () {



        if ($(this).css('display') == 'block') {

            $(this).css('display', 'none');
        }
        else {


            $(this).css('display', 'block');
        }
    })


}

//如果三级菜单下有子节点，点击调用此方法
function ulClick1(e) {

    var id = $(e).attr("id");

    if ($('#4_' + id + '_scroll').css('display') == 'block') {

        $('#4_' + id + '_scroll').css('display') == 'none';
    }
    else {

        $('#4_' + id + '_scroll').css('display') == 'block';
    }



    $('div[id*=4_' + id + '_]').each(function () {



        if ($(this).css('display') == 'block') {

            $(this).css('display', 'none');
        }
        else {


            $(this).css('display', 'block');
        }
    })

}


//三级菜单被点击 显示内容页面
function liClick(name, url, iscolseble) {


    addTab(name, url, "", iscolseble);
}


/*--------修改密码---------*/


function showPwd() {

    $('#txt_SrcUserPwd').val('');

    $('#txt_NewUserPwd').val('');

    $('#txt_ConfirmNewUserPwd').val('');

    $('#d2').dialog('open');

    $('#t_midfypwd').show();
    $('# dlg-buttons').show();

}

function closePwd() {


    $('#d2').dialog('close');

    $('#t_midfypwd').hide();
    $('# dlg-buttons').hide();

}


function ModifyPwd() {


    var ypwd = $('#txt_SrcUserPwd').val().trim();

    var pwd = $('#txt_NewUserPwd').val().trim();

    var pwd2 = $('#txt_ConfirmNewUserPwd').val().trim();

    if (ypwd == "") {

        AlterMessager('提示', '原密码不能为空', 'warning');

        return false;
    }

    if (pwd == "") {

        AlterMessager('提示', '新密码不能为空', 'warning');

        return false;
    }
    else {
        var patrn = /^(\w){6,16}$/;

        //        if (!patrn.exec(pwd)) {
        //            AlterMessager('提示', '密码长度为6-16位之间', 'warning');
        //            return false;
        //        }

        if (pwd.length < 6 || pwd.length > 16) {
            AlterMessager('提示', '密码长度为6-16位之间', 'warning');
            return false;
        }
    }

    if (pwd2 == "") {

        AlterMessager('提示', '确认密码不能为空', 'warning');

        return false;

    }



    if (pwd != pwd2) {

        AlterMessager('错误', '两次输入的密码不一致', 'error');

        return false;
    }

    var url = "../Handler/IndexHandler.ashx?method=modifypwd&ypwd=" + escape(ypwd) + "&pwd=" + escape(pwd);

    $.ajax({
        type: "Post",
        url: url,
        dataType: 'text', //返回string格式数据

        cache: false,
        async: false, //设置同步
        success: function (data) {

            if (data == "success") {
                AlterMessager('信息', '修改密码成功', '');
                $('#d2').dialog('close');

            }

            if (data == "error_pwd") {
                AlterMessager('信息', '修改密码失败,稍后请重新尝试！', 'warning');
            }

            if (data == "error_ypwd") {
                AlterMessager('信息', '原密码不正确！', 'warning');
            }

        },
        error: function (err) {

            AlterMessager('网络错误', '数据库连接失败', 'error');

        }
    });


}
/*--------系统欢迎字符----------*/

function welcome() {

    var path = "../Handler/WelcomeHandler.ashx?method=showMsg";

    $.ajax({
        type: "Post",
        async: false, //设置同步
        url: path,
        dataType: "text",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            $('#sp_WelCome').html(data);

        },
        error: function (err) {

        }
    });

}

/*------退出------*/
function LogOut() {
    window.location.href = "/LoginOut.aspx";
}

function LoadMsg() {
    var path = "../Handler/MessageHandler.ashx?method=self";
    $.ajax({
        type: "Post",
        async: false, //设置同步
        url: path,
        dataType: "text",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data != "") {
                $.messager.show({
                    title: '系统消息',
                    height: 330,
                    width: 350,
                    msg: data,
                    timeout: 10000,
                    showType: 'show'
                });
            }
        },
        error: function (err) {

        }
    });
}

//删除消息
function deleteMessage(id)
{
    var path = "../Handler/MessageHandler.ashx?method=delete&id=" + id + "";
    $.ajax({
        type: "Post",
        async: false, //设置同步
        url: path,
        dataType: "text",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data == "true") {
                $("#" + id + "").hide();
            }
        },
        error: function (err) {

        }
    });
}


function stopCount() {
    clearTimeout(t);
}
/*------图片放大旋转------*/

/*------ImageShow未使用------*/
function ImageShow(imageUrl) {
    $('#ui_Image').css('display', '');
    $('#ui_Image').dialog('open');
    $('#view_Image').attr('src', '/Windows/Win_Images.aspx?imageUrl=' + imageUrl);

}

function ImageShows(imageUrl,dss) {
    $('#ui_Image').css('display', '');
    $('#ui_Image').dialog('open');
    $('#view_Image').attr('src', '/Windows/Win_Images.aspx?imageUrl=' + imageUrl + '&uls=' + dss);

}

/*------申请历史记录------*/
function TenantIDShow(imageUrl) {
    $('#uctextTenantID').css('display', '');
    $('#uctextTenantID').dialog('open');
    $('#ucTenantIDviews').attr('src', imageUrl);
}

/*------车辆GPS定位------*/
function GPSShow(imageUrl, title) {
    if (title == undefined || title == '')
    { }
    else
    { $('#uctextselects').dialog({ title: title }); }

    $('#uctextselects').dialog('open');
    $('#ucviews').attr('src', imageUrl);
}

function TaskShow(title, url) {
    $('#UI-Task').css('display', '');
    $('#UI-Task').dialog('open');
    $('#view_Task').attr('src', url);
}

function CloseTaskShow() {
    $('#UI-Task').dialog('close');
}

//关闭我的代办任务处理的详细页面
function CloseMyTeskDedailPage(url) {
    $('#tabs').tabs('select', "我的待办任务");
    var currTab = parent.$('#tabs').tabs('getSelected'); //获得当前tab
    //var url = $(currTab.panel('options').content).attr('src');
    self.parent.$('#tabs').tabs('update', {
        tab: currTab,
        options: {
            content: createFrame(url)
        }
    });

    $('#tabs').tabs('close', "详细");
}

//关闭我的代办任务处理的详细页面
function CloseMyDesktopDedailPage(url) {
    $('#tabs').tabs('select', "我的看板");
    var currTab = parent.$('#tabs').tabs('getSelected'); //获得当前tab
    parent.$('#tabs').tabs('update', {
        tab: currTab,
        options: {
            content: createFrame("MyDesktop.aspx")
        }
    });
    $('#tabs').tabs('close', "详细");
}