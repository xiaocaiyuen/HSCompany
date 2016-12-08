function ShowSelect() {
    var strsrc = $("#ucview").attr("src");
    var url = '/Windows/Win_EasyUISelectUser.aspx?perseecharge=all&depseecharge=all'
    if (strsrc == "" || strsrc == undefined) {
        $("#ucview").attr("src", url);
    }
    $('#uctextselect').dialog('open');
    $('#ucselect').show();
}
function ShowSelectDep() {
    var strsrc = $("#ucview").attr("src");
    var url = '/Windows/Win_EasyUISelectUser.aspx?perseecharge=all&depseecharge='
    if (strsrc == "" || strsrc == undefined) {
        $("#ucview").attr("src", url);
    }
    $('#uctextselect').dialog('open');
    $('#ucselect').show();
}
function ShowSelectSee() {
    var strsrc = $("#ucview").attr("src");
    var url = '/Windows/Win_EasyUISelectUser.aspx'
    if (strsrc == "" || strsrc == undefined) {
        $("#ucview").attr("src", url);
    }
    $('#uctextselect').dialog('open');
    $('#ucselect').show();
}
function ReturnValue(code, depname) {
    $('input[id*=hid_DepCode]').val(uid);
    $('input[id*=txt_Dep]').val(uname);
    $('#uctextselects').dialog('close');
    $('#ucselects').hide();
}

function ShowSelects() {
    var strsrc = $("#ucviews").attr("src");
    var url = '/Windows/Win_EasyUISelectDept.aspx?perseecharge=all&depseecharge=all'
    if (strsrc == "" || strsrc == undefined) {
        $("#ucviews").attr("src", url);
    }
    $('#uctextselects').dialog('open');
    $('#ucselects').show();
}

function GetSelectValue(uname, uid) {
    //debugger
    $('input[id*=hid_UserId]').val(uid);
    $('input[id*=txt_UserName]').val(uname);
    $('input[id*=hid_UserName]').val(uname);
    $('#uctextselect').dialog('close');
    $('#ucselect').hide();
    loadUserInfo(uid);
}


function loadUserInfo(userId) {
    $.getJSON("/Handler/UserInfoHandler.ashx?id=" + userId + "&random=" + Math.random()
                  , function (data) {
                      //debugger
                      //部门名称
                      $("#t_PersonDepName").html(CheckNull(data.Department_Name));
                      $("#hid_DepName").val(CheckNull(data.Department_Name));
                      $("#hid_DepID").val(CheckNull(data.UserInfo_DepCode));
                      //岗位
                      $("#t_PersonPostName").html(CheckNull(data.UserInfo_PostName));
                      $("#hid_PostName").val(CheckNull(data.UserInfo_PostName));
                      //性别
                      $("#t_UserInfo_Sex").html(CheckNull(data.UserInfo_Sex));
                      //出生年月
                      $("#t_UserInfo_DateBirth").html(CheckNull(data.UserInfo_DateBirth));
                      //民族
                      $("#t_UserInfo_Nation").html(CheckNull(data.UserInfo_Nation));
                      //入党时间
                      $("#t_UserInfo_JoinPartyDate").html(CheckNull(data.UserInfo_JoinPartyDate));
                      //参加工作时间
                      $("#t_UserInfo_StartWorkDate").html(CheckNull(data.UserInfo_StartWorkDate));
                      //文化程度
                      $("#t_UserInfo_EducationalLevel").html(CheckNull(data.UserInfo_EducationalLevel));
                      //静态系数（组织监测使用）
                      $("#lblJTXS").html(CheckNull(data.StaticModulus));
                      $("#hid_StaticModulus").val(CheckNull(data.StaticModulus));

                  })
}