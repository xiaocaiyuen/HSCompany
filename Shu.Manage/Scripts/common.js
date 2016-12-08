


function request(paras) {

    var url = location.href;
    var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
    var paraObj = {}
    for (i = 0; j = paraString[i]; i++) {
        paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
    }
    var returnValue = paraObj[paras.toLowerCase()];
    if (typeof (returnValue) == "undefined") {
        return "";
    } else {
        return returnValue;
    }
}

//添加收藏
function AddFavorite(sTitle) {

    var sURL = window.location.href

    try {
        window.external.addFavorite(sURL, sTitle);
    }
    catch (e) {
        try {
            window.sidebar.addPanel(sTitle, sURL, "");
        }
        catch (e) {
            alert("加入收藏失败，请使用Ctrl+D进行添加");
        }
    }
}


//alter
//title：标题
//message 信息内容
//type:包括error/info/question/warning
function AlterMessager(title, message, type) {

    if (type == "") {
        $.messager.alert(title, message);
    }
    else {
        $.messager.alert(title, message, type);
    }
}

//确认提示框弹出
//title：标题
//message 信息内容
function ConfirmMessager(title, message) {
    $.messager.confirm(title, message, function (r) {
        if (!r) {
            return false;

        }
    });
}


//右下角提示框
//title：标题
//message 信息内容
//timeout 多少时间关闭 毫秒单位：5000为5秒
function SlideMessage(title, message, timeout) {
    $.messager.show({
        title: title,
        msg: message,
        timeout: timeout,
        showType: 'slide'
    });
}

//显示或关闭密码修改窗体
//divID: 参数type
// type: open/close
function ShowOrCloseWin(divId, type) {
    $('#' + divId).dialog(type);
    $('#' + divId).parent().appendTo($("form:first"));
    return false;
}

// 设置FieldSet高度方法，支持IE浏览器、Firefox 
// 参数1：FieldSet的ID 
// 参数2：图片的ID，展开或收缩后更新图片SRC 
// 参数3：标题
// 参数4,5：FieldSet内部div或table的id 
function FieldSetVisual(pFieldSetID, pImageID, pTitleID, pTableID, pDivID) {
    var objFieldSet = document.getElementById(pFieldSetID);
    var objImage = document.getElementById(pImageID);
    var objTitle = document.getElementById(pTitleID);
    var objTable = document.getElementById(pTableID);
    var objDiv = document.getElementById(pDivID);
    objFieldSet.style.height = "24px";
    if (objTable.style.display == 'block') {
        objTable.style.display = 'none';
        objDiv.style.display = 'none';
        objFieldSet.style.height = "14px";
        objImage.src = "/Images/main/down.png";
        ///objTitle.innerHTML = "展开查询";
        $("#" + pTitleID + "").html("展开查询");
        //$("#divUCEasyUIDataGrid").css("margin-top", "-60px;")
    }
    else {
        objTable.style.display = 'block';
        objDiv.style.display = 'block';
        var heightFieldSet = parseInt(objFieldSet.style.height.substr(0, objFieldSet.style.height.length - 2));
        var heightTable = parseInt(objTable.offsetHeight);
        objFieldSet.style.height = heightFieldSet + heightTable + "px";
        objImage.src = "/Images/main/up.png";
        ///objTitle.innerHTML = "折叠查询";
        $("#" + pTitleID + "").html("折叠查询");

        //$("#divUCEasyUIDataGrid").css("margin-top", "0px;")
    }
}

//扩展String 去掉左右空格
String.prototype.trim = function () { return this.replace(/(^\s*)|(\s*$)/g, ""); }


//文件下载
function Downloading(id) {



    var iWidth = 500; //弹出窗口的宽度;
    var iHeight = 300; //弹出窗口的高度

    var iTop = (window.screen.availHeight - 30 - iHeight) / 2; //获得窗口的垂直位置;

    var iLeft = (window.screen.availWidth - 10 - iWidth) / 2; //获得窗口的水平位置;

    //document.location.href = '/Files/Download.aspx?id=' + id;



    window.open('/Files/Download.aspx?id=' + id, 'newwindow', 'height=' + iHeight + ',width=' + iWidth + ',top=' + iTop + ',left=' + iLeft + ',toolbar=no,menubar=no,scrollbars=no, resizable=yes,location=no, status=no');

    //window.open('/Files/Download.aspx?id=' + id, 'newwindow', 'height=' + iHeight + ',width=' + iWidth + ',top=' + iTop + ',left=' + iLeft + ',toolbar=no,menubar=no,scrollbars=no, resizable=yes,location=no, status=no');


}

//获取ListBox选择的Value
function DownLoadFile() {
    var value = $("select[id*=lstAttachment]").val(); //取值
    if (value == null) {
        alert('请选择需要下载的附件！');
    }
    else {
        Downloading(value);
    }
    return false;
}

///获得系统当前时间:YYYY年MM月DD日
function GetCurrentTime() {
    var mydate = new Date();
    return mydate.toLocaleDateString();
}


/* 时间对象的格式化*/
function FormatEasyUiDate(dateString, dateformat) {
    //判断datetime 是否是js Date对象 
    if (dateString != "") {
        datetime = ParseEasyUiDate(dateString);
        var o = {
            "M+": datetime.getMonth() + 1,
            "d+": datetime.getDate(),
            "h+": datetime.getHours(),
            "m+": datetime.getMinutes(),
            "s+": datetime.getSeconds(),
            "q+": Math.floor((datetime.getMonth() + 3) / 3),
            "S": datetime.getMilliseconds()
        }

        if (/(y+)/.test(dateformat)) {
            dateformat = dateformat.replace(RegExp.$1, (datetime.getFullYear() + "").substr(4 - RegExp.$1.length));
        }
        for (var k in o) {
            if (new RegExp("(" + k + ")").test(dateformat)) {
                dateformat = dateformat.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
            }
        }
    }
    else {
        dateformat = dateString;
    }
    return dateformat;
}

function ParseEasyUiDate(dateString) {
    if (typeof dateString == "string") {
        //"yyyy-MM-dd"
        var results = dateString.match(/^ *(\d{4})[-|\/|\.|年](\d{1,2})[-|\/|\.|年](\d{1,2}) *$/);
        if (results && results.length > 3) {
            return new Date(parseInt(results[1], 10), (parseInt(results[2], 10) - 1), parseInt(results[3], 10));
        }
        //"yyyy-MM-dd hh:mm"
        results = dateString.match(/^ *(\d{4})[-|\/|\.|年](\d{1,2})[-|\/|\.|年](\d{1,2}) +(\d{1,2}):(\d{1,2}) *$/);
        if (results && results.length > 5) {
            return new Date(parseInt(results[1], 10), parseInt(results[2], 10) - 1, parseInt(results[3], 10), parseInt(results[4], 10), parseInt(results[5], 10));
        }
        //"yyyy-MM-dd hh:mm:ss"
        results = dateString.match(/^ *(\d{4})[-|\/|\.|年](\d{1,2})[-|\/|\.|年](\d{1,2}) +(\d{1,2}):(\d{1,2}):(\d{1,2}) *$/);
        if (results && results.length > 6) {
            return new Date(parseInt(results[1], 10), parseInt(results[2], 10) - 1, parseInt(results[3], 10), parseInt(results[4], 10), parseInt(results[5], 10), parseInt(results[6], 10));
        }
        //"yyyy-MM-dd hh:mm:ss SSS"
        results = dateString.match(/^ *(\d{4})[-|\/|\.|年](\d{1,2})[-|\/|\.|年](\d{1,2}) +(\d{1,2}):(\d{1,2}):(\d{1,2})\.(\d{1,9}) *$/);
        if (results && results.length > 7) {
            return new Date(parseInt(results[1], 10), parseInt(results[2], 10) - 1, parseInt(results[3], 10), parseInt(results[4], 10), parseInt(results[5], 10), parseInt(results[6], 10), parseInt(results[7], 10));
        }
    }
    return dateString;
}

/* 获取URL中参数值 */
function GetUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

function GetUrlVar(name) {
    return GetUrlVars()[name];
}

/*##################文档类直接打开或者下载  --fileName：文件名称 ；filePath：文件路径################*/
function OpenFile(filePath, filename) {

    var iWidth = 500; //弹出窗口的宽度;
    var iHeight = 300; //弹出窗口的高度

    var iTop = (window.screen.availHeight - 30 - iHeight) / 2; //获得窗口的垂直位置;

    var iLeft = (window.screen.availWidth - 10 - iWidth) / 2; //获得窗口的水平位置;

    window.open('/Files/Download.aspx?path=' + filePath + "&filename=" + filename + "", 'newwindow', 'height=' + iHeight + ',width=' + iWidth + ',top=' + iTop + ',left=' + iLeft + ',toolbar=no,menubar=no,scrollbars=no, resizable=yes,location=no, status=no');

}



/*返回按钮*/
function fanhui(url) {
    var flag = request("flag");
    if (flag == '1') {
        $('#FanHui').attr('href', url + '?flag=1');
    }
    if (flag == '2') {
        $('#FanHui').attr('href', url + '?flag=2');
    }
}



/*删除文件*/

function delfile() {

    if (confirm("附件删除后将不可恢复,您确定要删除该附件吗?")) {

        return true;
    }
    else {
        return false;
    }
}

//字符串截取
function LengthStr(s) {
    var len = 0;

    for (var i = 0; i < s.length; i++) {
        var a = s.charAt(i);
        var reg = /[^\u4E00-\u9FA5\uf900-\ufa2d]/g;   //匹配中文的规则
        if (!reg.exec(a)) {                                                    //匹配成功则长度+2
            len = len + 2;
        }
        else len = len + 1;

        if (len > 70) {
            s = s.substring(0, i) + '...';
            return s;
        }

    }
    return s;                                                                //如果长度超过指定长度 这例子中为40
    //则返回截取过的 否则返回原来的。
}

///风险A B C三等颜色变化index为从0开始数的列数
function FXColorShow(id, index) {
    $('table[id*=' + id + ']').find("tr").each(function () {
        $(this).find("td").eq(index).each(function () {
            Dcolor($(this));
        })
    })
}
function Dcolor(object) {
    var fxdj = object.text();
    if (fxdj.indexOf('A') >= 0 || fxdj.indexOf('红') >= 0) {
        object.attr('style', 'color:red');
    }
    else if (fxdj.indexOf('B') >= 0 || fxdj.indexOf('黄') >= 0) {
        object.attr('style', 'color:#FAA708');
    }
    else if (fxdj.indexOf('C') >= 0 || fxdj.indexOf('蓝') >= 0) {
        object.attr('style', 'color:blue');
    }
    else if (fxdj.indexOf('审核通过') >= 0) {
        object.attr('style', 'color:green');
    }
    else if (fxdj.indexOf('待') >= 0) {
        object.attr('style', 'color:red');
    }
    else if (fxdj.indexOf('撤销') >= 0) {
        object.attr('style', 'color:black');
    }
}

//截取GRID中文本带标签人列
function HTMLGridSubStr(gridId, columnNum, SubstrNum) {

    var cloumnIndex = columnNum - 1;

    $('table[id*=' + gridId + '] tr:not(:first)').each(function () {

        var str = $(this).find('td:eq(' + cloumnIndex + ')').text();
        $(this).find('td:eq(' + cloumnIndex + ')').html(str);
        var newstr = $(this).find('td:eq(' + cloumnIndex + ')').text();
        if (str.length > SubstrNum) {

            $(this).find('td:eq(' + cloumnIndex + ')').text(str.substring(0, SubstrNum) + "...");


        }

        $(this).find('td:eq(' + cloumnIndex + ')').attr('title', newstr);

        $(this).find('td:eq(' + cloumnIndex + ')').css('text-align', 'left');

        $(this).find('td:eq(' + cloumnIndex + ')').css('padding-left', '10px');


    })
}

// 对grid审核状态的控制
//gridviewID = 当前gridviewid
//auditIndex = 当前 审核状态所在的列的位置
// roleIndex =  当前 隐藏列对应的角色
function GridButtonDisabled(gridviewID, auditIndex, roleIndex) {

    var roleName = $('input[id*=hid_RoleName]').val();

    auditIndex = auditIndex - 1;

    roleIndex = roleIndex - 1;

    $("table[id*=" + gridviewID + "] tr:not(:first)").each(function () {


        $(this).find('td').eq(auditIndex).each(function () {

            td = $(this).text();



            if (td.indexOf('审核通过') >= 0) {

                $(this).css('color', 'green')

            }
            if (td.indexOf('待') >= 0) {

                $(this).css('color', 'red');

            }

            if (td.indexOf('审核退回') >= 0) {

                $(this).css('color', 'red');

            }

        })



        tdRoleName = $(this).find('td').eq(roleIndex).text();





        $(this).find(':button').each(function () {

            var btnText = $(this).attr('value');

            if (td == "审核通过") {

                if (btnText == "审核") {

                    $(this).attr('disabled', true);
                }

                if (btnText == "删除") {

                    $(this).attr('disabled', true);
                }
                if (btnText == "编辑") {

                    $(this).attr('disabled', true);
                }
                if (btnText == "修改") {

                    $(this).attr('disabled', true);
                }
                if (btnText == "签发") {

                    $(this).attr('disabled', true);
                }

            }

            if (td.indexOf('待审核') >= 0) {



                if (btnText == "删除") {

                    $(this).attr('disabled', true);
                }
                if (btnText == "编辑") {

                    $(this).attr('disabled', true);
                }
                if (btnText == "修改") {

                    $(this).attr('disabled', true);
                }

            }


            if (btnText == "审核") {

                if (td != "预存") {
                    if (td == "待审核" && roleName == tdRoleName) {
                        $(this).attr('disabled', false);
                    }
                    else {
                        $(this).attr('disabled', true);
                    }
                }
                else {
                    $(this).attr('disabled', true);
                }
            }

            if (td.indexOf('审核退回') >= 0) {



                if (btnText == "删除") {

                    $(this).attr('disabled', true);
                }


            }




            //监察建议列表的修改操作，不限角色，修改功能只提供给纪检人员 单独拿出来
            //                if (btnText == "修改") {

            //                    if (td != "预存") {

            //                        if (td == "审核退回" && $('span[id*=lbl_GridTitle]').text() == '监察建议书列表') {
            //                            $(this).attr('disabled', false);
            //                        }
            //                        else {
            //                            $(this).attr('disabled', true);
            //                        }
            //                    }
            //                    else {
            //                        $(this).attr('disabled', false);
            //                    }

            //                }

            //                if (btnText == "修改") {
            //                    if (jddxname == username && td.indexOf('预存') >= 0) {
            //                        $(this).attr('disabled', false);
            //                    }
            //                    else if (jddxname == username && td.indexOf('审核退回') >= 0) {
            //                        $(this).attr('disabled', false);
            //                    }

            //                }

            //                if (btnText == "删除") {
            //                    //                    if (td != "预存") {
            //                    if (jddxname == username && td.indexOf('预存') >= 0) {
            //                        $(this).attr('disabled', false);
            //                    }
            //                    else if (td.indexOf('回复') >= 0) {

            //                        $(this).attr('disabled', false);
            //                    }
            //                    else {
            //                        $(this).attr('disabled', true);
            //                    }
            //                    //                    }
            //                    //                    else {
            //                    //                        $(this).attr('disabled', true);
            //                    //                    }
            //                }
            //                if (btnText == "签发") {
            //                    
            //                    if (td.indexOf(username) >= 0 && td.indexOf('签发') >= 0) {
            //                        $(this).attr('disabled', false);
            //                    }
            //                    else {
            //                        $(this).attr('disabled', true);
            //                    }
            //                }
        })

    })
}






//判断当用户是否分管了某个用户
function ChargeIsFg(userId_Zg, userId_Fg) {
    var result = "1";
    $.ajax({
        type: "POST",
        url: "/Handler/GetGrdtzb.ashx?type=charge&ZgId=" + userId_Zg + "&FgId=" + userId_Fg,
        dataType: 'text',
        cache: false,
        data: '',
        async: false,
        success: function (data) {
            result = data;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
        }
    });
    return result;
}

//判断当用户是否分管了某个用户
function ChargeIsDep(userId_Zg, userId_Fg) {
    var result = "1";
    $.ajax({
        type: "POST",
        url: "/Handler/GetGrdtzb.ashx?type=chargeDep&ZgId=" + userId_Zg + "&FgId=" + userId_Fg,
        dataType: 'text',
        cache: false,
        data: '',
        async: false,
        success: function (data) {
            result = data;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
    return result;
}


//预警处置列表用到
//设置操作按钮 姚
function SetButton(id, index, lx) {
    //        $("table[id*=" + id + "] tr:not(:first)").each(function () {
    //            var tdCon = $(this).find("td").eq(index).html();

    //            var tdid = $(this).find("td").eq(10).html();
    //            var loginRole = $("#hdnRole").val();
    //            var loginId = $("#hdnLoginId").val();
    //            if (tdCon.indexOf("签发") != -1) {
    //                //设置按钮不可用
    //                $(this).find("input[type='button']").each(function () {
    //                    if ($(this).val() == "回复") {
    //                        $(this).attr("disabled", "disabled")
    //                    }
    //                    else if ($(this).val() == "审核") {
    //                        $(this).attr("disabled", "disabled")
    //                    }
    //                    else if ($(this).val() == "签发") {
    //                        var isOk = false;
    //                        var qfRole = tdCon.substring(1, tdCon.length - 2);
    //                        var arrRole = $("#hdnRole").val().split(',');
    //                        for (var i = 0; i < arrRole.length; i++) {
    //                            if (arrRole[i].indexOf(qfRole) != -1) {
    //                                isOk = true; break;
    //                            }
    //                        }
    //                        if (isOk == false) {
    //                            $(this).attr("disabled", "disabled");
    //                        }
    //                        else {
    //                            if (loginRole.indexOf("监审室主任") != -1) {
    //                            }
    //                            else if (lx == "1" && loginRole.indexOf("副局长兼纪检组长") != -1 && ChargeIsFg(loginId, tdid) == "0") {
    //                                $(this).attr("disabled", "disabled");
    //                            }
    //                            else if (lx == "1" && loginRole.indexOf("负责人") != -1 && ChargeIsDep(loginId, tdid) == "0") {
    //                                $(this).attr("disabled", "disabled");
    //                            }
    //                        }
    //                    }
    //                })
    //            }
    //            else if (tdCon.indexOf("待") != -1 && tdCon.indexOf("审核") != -1) //待审核状态
    //            {
    //                $(this).find("input[type='button']").each(function () {
    //                    if ($(this).val() == "回复") {
    //                        $(this).attr("disabled", "disabled")
    //                    }
    //                    if ($(this).val() == "修改") {
    //                        $(this).attr("disabled", "disabled")
    //                    }
    //                    else if ($(this).val() == "删除") {
    //                        if ($("#hdnRole").val().indexOf("管理员") == -1) {
    //                            $(this).attr("disabled", "disabled")
    //                        }
    //                    }
    //                    else if ($(this).val() == "签发") {
    //                        $(this).attr("disabled", "disabled")
    //                    }
    //                    else if ($(this).val() == "审核") {
    //                        var isOk = false;
    //                        var qfRole = tdCon.substring(1, tdCon.length - 2);
    //                        var arrRole = $("#hdnRole").val().split(',');
    //                        for (var i = 0; i < arrRole.length; i++) {
    //                            if (arrRole[i].indexOf(qfRole) != -1) {
    //                                isOk = true; break;
    //                            }
    //                        }
    //                        if (isOk == false) {
    //                            $(this).attr("disabled", "disabled")
    //                        }
    //                        else {
    //                            if (loginRole.indexOf("监审室主任") != -1) {
    //                            }
    //                            else if (lx == "1" && loginRole.indexOf("副局长兼纪检组长") != -1 && ChargeIsFg(loginId, tdid) == "0") {
    //                                $(this).attr("disabled", "disabled");
    //                            }
    //                            else if (lx == "1" && loginRole.indexOf("负责人") != -1 && ChargeIsDep(loginId, tdid) == "0") {
    //                                $(this).attr("disabled", "disabled");
    //                            }
    //                        }
    //                    }
    //                })
    //            }
    //            else if (tdCon.indexOf("待回复") != -1 || tdCon.indexOf("退回") != -1) //待回复
    //            {
    //                $(this).find("input[type='button']").each(function () {
    //                    if ($(this).val() == "审核") {
    //                        $(this).attr("disabled", "disabled")
    //                    }
    //                    else if ($(this).val() == "删除") {
    //                        if ($("#hdnRole").val().indexOf("管理员") == -1) {
    //                            $(this).attr("disabled", "disabled")
    //                        }
    //                    }
    //                    else if ($(this).val() == "签发") {
    //                        $(this).attr("disabled", "disabled")
    //                    }
    //                    else if ($(this).val() == "回复" && tdid != $("#hdnLoginId").val()) {
    //                        $(this).attr("disabled", "disabled")
    //                    }
    //                })
    //            }
    //            else if (tdCon.indexOf("通过") != -1) //通过
    //            {
    //                $(this).find("input[type='button']").each(function () {
    //                    if ($(this).val() == "审核") {
    //                        $(this).attr("disabled", "disabled")
    //                    }
    //                    else if ($(this).val() == "删除") {
    //                        if ($("#hdnRole").val().indexOf("管理员") == -1) {
    //                            $(this).attr("disabled", "disabled")
    //                        }
    //                    }
    //                    else if ($(this).val() == "签发") {
    //                        $(this).attr("disabled", "disabled")
    //                    }
    //                    else if ($(this).val() == "回复") {
    //                        $(this).attr("disabled", "disabled")
    //                    }
    //                    else if ($(this).val() == "修改") {
    //                        $(this).attr("disabled", "disabled")
    //                    }
    //                })
    //            }
    //            else if (tdCon.indexOf("退回") != -1) //退回
    //            {
    //                $(this).find("input[type='button']").each(function () {
    //                    if ($(this).val() == "审核") {
    //                        $(this).attr("disabled", "disabled")
    //                    }
    //                    else if ($(this).val() == "签发") {
    //                        $(this).attr("disabled", "disabled")
    //                    }
    //                    else if ($(this).val() == "回复") {
    //                        $(this).attr("disabled", "disabled")
    //                    }

    //                })
    //            }
    //        })
}

function HidenTd(id, index) {
    $("table[id*=" + id + "] tr").each(function () {
        var tdCon = $(this).find("td").eq(index).hide();
        var tdCon = $(this).find("th").eq(index).hide();
    })
}



function CheckNull(data) {

    if (data == null) {
        return "";
    }
    else {

        return data;
    }
}