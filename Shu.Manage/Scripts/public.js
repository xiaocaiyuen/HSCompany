//$(document).ready(function () {
//    //Loading(false);
//    tableobjcss();
//    //Sizewidth();
//});
/**
表格隔行换色
**/
var applyjson = null;
function tableobjcss() {
    var $clicked, i = 0;
    $(".div-panel table tr").click(function (e) {
        if ($(this).find('td').attr("class") != "td_item") {
            if (i = 0) $clicked = $(e.target);
            if ($clicked != null && $clicked != $(e.target)) {
                $($clicked).parent().find("td").removeClass("selected");
            }
            $clicked = $(e.target);
            $clicked.parent().find("td").addClass("selected");
            i++;
        }
    })
    $('.div-panel table tr').hover(function () {
        if ($(this).find('td').attr("class") != "td_item") {
            $(this).find('td').addClass('navHover');
        }
    }, function () {
        $(this).find('td').removeClass('navHover');
    });
    /*$('.div-panel table tr').click(function () {
    if ($(this).find('td').attr("class") != "td_item") {
    $('.div-panel table tr').find('td').removeClass("selected");
    $(this).find('td').addClass("selected"); //添加选中样式   
    }
    }).hover(function () {
    if ($(this).find('td').attr("class") != "td_item") {
    $(this).find('td').addClass('navHover');
    }
    }, function () {
    $(this).find('td').removeClass('navHover');
    });*/
}
//自应大小
function Sizewidth() {
    $(".div-panel").css("width", $(window).width() + 13); //数据表格宽度
    $(".div-panel").css("height", $(window).height() - 15); //数据表格高度
    var h1 = $(window).height() - 35;
    var tab = $(".tableobj");
    var tabheight = tab.height();
    if (tabheight > h1) {
        $(".div-panel").css("overflow", "scroll");
        $(".div-panel").css("overflow-x", "hidden");
    }
}

/**
加载对话窗
**/
function Loading(bool) {
    if (bool) {
        top.$("#loading").show();
    } else {
        top.$("#loading").hide();
    }
}
function loadingHide() {
    $("#loading").hide();
}
/**
数据验证完整性
**/
function CheckDataValid(id) {
    if (!JudgeValidate(id)) {
        return false;
    }
    else {
        return true;
    }
}
/*
关闭弹出网页
*/
function Close_Show() {
    var api = frameElement.api, W = api.opener;
    api.close();
}
/*弹出全屏网页
/*objurl:       表示请求路径
/*objtitle:     标题名称
/*width:        宽度
/*height:       高度
---------------------------------------------------*/
function FullOpenWindow(objurl, objtitle, objw, objh, max, min) {
    $.dialog({
        max: max,
        min: min,
        lock: true,
        background: '#e4e4e4',
        opacity: 0.5, /* 透明度 */
        title: objtitle,
        content: 'url:' + objurl + ''
    }).max();
}

//objid：id
//objurld：地址
//title：标题
//width：宽度
//height：高度
function OpenDialog(objid, objurl, title, icon, width, height) {
    $('#' + objid).dialog({
        title: title,
        iconCls: icon,
        width: width,
        height: height,
        minimizable: true,
        maximizable: true,
        collapsible: true,
        resizable: true,
        href: 'DataGrid.aspx',
        modal: true
    });
}

/*弹出网页
/*objurl:       表示请求路径
/*objtitle:     标题名称
---------------------------------------------------*/
function OpenWindow(objurl, objtitle, objw, objh, max, min) {
    $.dialog({
        max: max,
        min: min,
        lock: true,
        background: '#e4e4e4',
        opacity: 0.5, /* 透明度 */
        title: objtitle,
        content: 'url:' + objurl + ''
    });
}
/*弹出网页
/*objurl:       表示请求路径
/*objtitle:     标题名称
/*width:        宽度
/*height:       高度
---------------------------------------------------*/
function OpenWindow3(objurl, objtitle, objw, objh, max, min) {
    $.dialog({
        max: max,
        min: min,
        width: objw,
        height: objh,
        lock: true,
        background: '#e4e4e4',
        opacity: 0.5, /* 透明度 */
        title: objtitle,
        content: 'url:' + objurl + ''
    });
}

/**
弹出提示框
msg: 显示消息
**/
function showAlertMsg(msg) {
    $.messager.alert("系统提示", msg, "info");
}

/**
弹出提示框
msg: 显示消息
**/
function showAlertMsgUrl(msg, url) {
    $.messager.alert("系统提示", msg, "info", function () {
        location.href = url;
        return false;
    });
}

/**
默认提示
msg: 显示消息
callBack：函数
**/
function showConfirmMsg(msg, callBack) {
    $.messager.confirm("系统提示", msg, callBack);
}
//获取查看复选框值
function checkboxallId() {
    var reVal = '';
    $('[name = checkbox]').each(function () {
        if ($(this).attr("checked")) {
            reVal += $(this).val() + ",";
        }
    });
    return reVal;
}
//接收地址栏参数
function GetQry(key) {
    var search = location.search.slice(1); //得到get方式提交的查询字符串
    var arr = search.split("&");
    for (var i = 0; i < arr.length; i++) {
        var ar = arr[i].split("=");
        if (ar[0] == key) {
            return ar[1];
        }
    }
}
// 文本框只允许数字
function keypress(obj) {
    $("#" + obj).bind("contextmenu", function () {
        return false;
    });
    $("#" + obj).css('ime-mode', 'disabled');
    $("#" + obj).keypress(function (e) {
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            return false;
        }
    });
}
// 只能输入数字和小数点
function numberbox(obj) {
    $("#" + obj).bind("contextmenu", function () {
        return false;
    });
    $("#" + obj).css('ime-mode', 'disabled');
    $("#" + obj).bind("keydown", function (e) {
        var key = window.event ? e.keyCode : e.which;
        if (isFullStop(key)) {
            return $(this).val().indexOf('.') < 0;
        }
        return (isSpecialKey(key)) || ((isNumber(key) && !e.shiftKey));
    });

}
function isNumber(key) {
    return key >= 48 && key <= 57
}

function isSpecialKey(key) {
    return key == 8 || key == 46 || (key >= 37 && key <= 40) || key == 35 || key == 36 || key == 9 || key == 13
}
function isFullStop(key) {
    return key == 190 || key == 110;
}

/*删除
/*url:        表示请求路径
--------------------------------------------------*/
function DeleteData(url, active, id, setKey) {
    var parm = 'active=' + active + '&id=' + id + setKey;
    if (id == undefined || id == "") {
        showAlertMsg('请选择至少一行数据');
    } else {
        showConfirmMsg("确定要删除吗？", function (r) {
            if (r) {
                getAjax(url, parm, function (rs) {
                    if (rs == "0") {
                        showAlertMsg("删除失败！");
                    }
                    else if (rs == "1") {
                        showAlertMsgUrl("删除成功！", url);
                    }
                    else if (rs == 2) {
                        showAlertMsg("该数据被关联,无法删除！");
                    }
                    else if (rs == 3) {
                        showAlertMsg("此数据不可以被删除！");
                    }
                    else {
                        showAlertMsg(rs);
                    }
                });
            }
        });
    }
}


/*删除
/*url:        表示请求路径
--------------------------------------------------*/
function ActiveData(url, active, id, setKey) {
    var parm = 'active=' + active + '&id=' + id + setKey;
    if (id == undefined || id == "") {
        showAlertMsg('请选择至少一行数据');
    } else {
        showConfirmMsg("确定要激活吗？", function (r) {
            if (r) {
                getAjax(url, parm, function (rs) {
                    if (rs == "0") {
                        showAlertMsg("激活失败！");
                    }
                    else if (rs == "1") {
                        showAlertMsgUrl("激活成功！", url);
                    }
                    else if (rs == 2) {
                        showAlertMsg("该数据被关联,无法激活！");
                    }
                    else if (rs == 3) {
                        showAlertMsg("此数据不可以被激活！");
                    }
                    else {
                        showAlertMsg(rs);
                    }
                });
            }
        });
    }
}


/*核销
/*url:        表示请求路径
--------------------------------------------------*/
function CheckOff(url, active, id, setKey) {
    var parm = 'active=' + active + '&id=' + id + setKey;
    if (id == undefined || id == "") {
        showAlertMsg('请选择至少一行数据');
    } else {
        showConfirmMsg("核销后的数据将无法删除和修改，确定要核销吗？", function (r) {
            if (r) {
                getAjax(url, parm, function (rs) {
                    if (rs == '0') {
                        showAlertMsg("核销失败！");
                    }
                    else if (rs == "1") {
                        showAlertMsgUrl("核销成功！", url);
                    }
                    else {
                        showAlertMsg(rs);
                    }
                });
            }
        });
    }
}


/*撤回核销
/*url:        表示请求路径
--------------------------------------------------*/
function CancelCheck(url, active, id, setKey) {
    var parm = 'active=' + active + '&id=' + id + setKey;
    if (id == undefined || id == "") {
        showAlertMsg('请选择至少一行数据');
    } else {
        showConfirmMsg("确定要撤回核销吗？", function (r) {
            if (r) {
                getAjax(url, parm, function (rs) {
                   
                    if (rs == '0') {
                        showAlertMsg("撤回失败！");
                    }
                    else if (rs == "2") {
                        showAlertMsg("此申请已经发起提前还款，无法撤回核销！");
                    }
                    else if (rs == "1") {
                        showAlertMsgUrl("撤回成功！", url);
                    }
                    else {
                        showAlertMsg(rs);
                    }
                });
            }
        });
    }
}



/*用户账号设置
/*url:表示请求路径
/*opt:0:上班 1:请假 2:锁定
--------------------------------------------------*/
function UserAccountSettings(url, active, id, setKey,opt) {
    var parm = 'active=' + active + '&OptionCode=' + opt + '&id=' + id + setKey;
    if (id == undefined || id == "") {
        showAlertMsg('请选择至少一行数据');
    } else {
        showConfirmMsg("确定要做此操作吗？", function (r) {
            if (r) {
                getAjax(url, parm, function (rs) {
                    if (rs == '0') {
                        showAlertMsg("设置失败！");
                    }
                    else if (rs == "1") {
                        showAlertMsgUrl("设置成功！", url);
                    }
                    else {
                        showAlertMsg(rs);
                    }
                });
            }
        });
    }
}

/*撤销分解
/*url:        表示请求路径
--------------------------------------------------*/
function RevokeFenJie(url, active, id, setKey) {
    var parm = 'active=' + active + '&id=' + id + setKey;
    if (id == undefined || id == "") {
        showAlertMsg('请选择至少一行数据');
    } else {
        showConfirmMsg("确定要撤销分解吗？", function (r) {
            if (r) {
                getAjax(url, parm, function (rs) {

                    if (rs == '0') {
                        showAlertMsg("撤销分解失败！");
                    }
                    else if (rs == "1") {
                        showAlertMsgUrl("撤销分解成功！", url);
                    }
                    else {
                        showAlertMsg(rs);
                    }
                });
            }
        });
    }
}

/*领单
/*url:        表示请求路径
--------------------------------------------------*/
function PostLock(url, active, id, setKey) {
    var parm = 'active=' + active + '&id=' + id + setKey;
    if (id == undefined || id == "") {
        showAlertMsg('请选择至少一行数据');
    } else {
        showConfirmMsg("您确定要领单吗？", function (r) {
            if (r) {
                getAjax(url, parm, function (rs) {
                    if (rs == '0') {
                        showAlertMsg("领单失败！");
                    }
                    else if (rs == "1") {
                        showAlertMsgUrl("领单成功！", url);
                    }
                    else {
                        showAlertMsg(rs);
                    }
                });
            }
        });
    }
}

/*锁定
/*url:        表示请求路径
--------------------------------------------------*/
//function PostLock(url, active, id, setKey) {
//    var parm = 'active=' + active + '&id=' + id + setKey;
//    if (id == undefined || id == "") {
//        showAlertMsg('请选择至少一行数据');
//    } else {
//        showConfirmMsg("锁定后的数据将无法进行再次分配，确定要锁定吗？", function (r) {
//            if (r) {
//                getAjax(url, parm, function (rs) {
//                    if (rs == '0') {
//                        showAlertMsg("锁定失败！");
//                    }
//                    else if (rs == "1") {
//                        showAlertMsgUrl("锁定成功！", url);
//                    }
//                    else {
//                        showAlertMsg(rs);
//                    }
//                });
//            }
//        });
//    }
//}

/*发短信
/*url:        表示请求路径
--------------------------------------------------*/
function SendSms(url, active, id, setKey) {
    if (url.indexOf("id=") > 0)
    {
        var parm = 'active=' + active + '&ids=' + id + setKey;
    }
    else
     {
        var parm = 'active=' + active + '&id=' + id + setKey;
    }
    if (id == undefined || id == "") {
        showAlertMsg('请选择至少一行数据');
    } else {
        showConfirmMsg("确定给选中的逾期记录发送短信？", function (r) {
            if (r) {
                getAjax(url, parm, function (rs) {
                    if (rs == '0') {
                        showAlertMsg("发送失败！");
                    }
                    else if (rs == "1") {
                        showAlertMsgUrl("发送成功！", url);
                    }
                    else {
                        showAlertMsg(rs);
                    }
                });
            }
        });
    }
}

/*自定义事件
/*url:        表示请求路径
--------------------------------------------------*/
function DataEval(url, active, id, setKey, paf) {
    debugger;
    var parm = {};
    if ($("#form1").length > 0) {
        parm = search("form1");
    }
    if (parm == null || parm == undefined) {
        parm = {};
    }
    parm.active = active;
    parm.id = id;
   
    
    //if (id == undefined || id == "") {
    //    showAlertMsg('请选择至少一行数据');
    //} else {
        if (paf == "1") {
            pdfDoc(id);
        } else {
            getAjax(url, parm, function (rs) {
                var s = rs;
                if (rs.indexOf('javascript:') != -1) {
                    var excel = rs.replace('javascript:', '').split(",")
                    var path = excel[0];
                    var name = excel[1];
                    if (path == "'0'") {
                        showAlertMsg("没有相关数据！");
                    }
                    else {
                        wordDoc(path, name);
                    }


                }
                else {

                    showAlertMsgUrl(rs, url);
                }
            });
        }
    //}
}

/*下载
/*url:        表示请求路径
--------------------------------------------------*/
function Download(path, name) {
    var url = "/Files/Download.aspx?path=" + path + "/" + name + ".bak&filename=" + name;
    location.target = "_ablank";
    location.href = url;
}

function pdfDoc(id) {
    debugger;
    if (id != "0") {
        window.open("/Print/RegionalAuditToPDF.aspx?id=" + id + "", "");

    } else {
        showAlertMsg("导出失败！");
    }
}

function wordDoc(url, name) {
    if (url != "0") {
        window.open("/Files/Download.aspx?path=" + url + "&filename=" + name + ".xlsx", "");

    } else {
        showAlertMsg("导出失败！");
    }
}

/*跳转链接
/*url:        表示请求路径
/*type:       属性（1.当然页面跳转，4跳转到新页面）
/*id:         唯一编号
--------------------------------------------------*/
function ShowData(url, type, id, setKey) {
    var strurl;
    if (url.indexOf('?') > 0) {
        strurl = url + "&id=" + id + setKey;
    } else {
        strurl = url + "?id=" + id + setKey;
    }
    if (type == "1") {
        location.href = strurl;
        return false;
    }
    else {
        window.parent.addTab('详细', strurl, '');
    }
}

//从我的桌面点击详细
function MyMyDesktopShowData(url, type, id, setKey) {
    var strurl;
    if (url.indexOf('?') > 0) {
        strurl = url + "&MyDesktop=true&id=" + id + setKey;
    } else {
        strurl = url + "?MyDesktop=true&id=" + id + setKey;
    }
    if (type == "1") {
        location.href = strurl;
        return false;
    }
    if(type == "1010")
    {
        parent.parent.addTab('详细', strurl, '');
    }
    else {
        window.parent.addTab('详细', strurl, '');
    }
}

/*提交
/*url:        表示请求路径
--------------------------------------------------*/
function Submit(url, active, id, setKey) {

    fileNum(id, setKey);
    if (applyjson.length > 0) {
        var msg = '<div>';
        $.each(applyjson, function (index, value) {
            msg += value.filename + '缺少至少<span style="color:red;">' + value.num + '</span>个<br/>';
        });
        msg += "</div>";
        $.messager.alert("资料清单", msg);
        return;
    }

    var parm = 'active=' + active + '&id=' + id + setKey;
    if (id == undefined || id == "") {
        showAlertMsg('请选择至少一行数据');
    } else {
        showConfirmMsg("确认要提交吗？", function (r) {
            if (r) {
                getAjax(url, parm, function (rs) {
                    if (rs == "0") {
                        showAlertMsg("提交失败！");
                    }
                    else if (rs == "1") {
                        showAlertMsgUrl("提交成功！", url);
                    }
                    else {
                        showAlertMsg(rs);
                    }
                });
            }
        });
    }
}

/*启用基准利率
/*url:        表示请求路径
/*active:       属性（1.当然页面跳转，4跳转到新页面）
/*id:         唯一编号
--------------------------------------------------*/
function OpenRate(url, active, id) {
    var parm = 'active=' + active + '&id=' + id;
    if (id == undefined || id == "") {
        showAlertMsg('请选择至少一行数据');
    } else {
        showConfirmMsg("确定要启用吗？", function (r) {
            if (r) {
                getAjax(url, parm, function (rs) {
                    if (rs == "0") {
                        showAlertMsg("此基准利率前还有其它未启用的基准利率，无法启用！");
                    }
                    else if (rs == "1") {
                        showAlertMsgUrl("启用成功！", url);
                    }
                    else {
                        showAlertMsg("启用失败！");
                    }
                });
            }
        });
    }
}

/*提交
/*url:        表示请求路径
--------------------------------------------------*/
//function Submit(url, parm) {
//    showConfirmMsg('确认要提交吗？', function (r) {
//        if (r) {
//            getAjax(url, parm, function (msg) {
//                if (parseInt(msg) > 0) {
//                    showAlertMsg("提交成功！");
//                    window.windowclose();
//                    window.Detailwindowclose();
//                }
//                else {
//                    showAlertMsg("提交失败！");
//                }
//            });
//        }
//    });
//}
/*审核
/*url:        表示请求路径
--------------------------------------------------*/
function Examine(url, parm) {
    showConfirmMsg('确认要审核吗？', function (r) {
        if (r) {
            getAjax(url, parm, function (msg) {
                if (parseInt(msg) > 0) {
                    showAlertMsg("审核成功！");
                    window.windowclose();
                    window.Detailwindowclose();
                }
                else {
                    showAlertMsg("审核失败！");
                }
            });
        }
    });
}

function ExportExcel(Url) {//导出Excel文件
    //getExcelXML有一个JSON对象的配置，配置项看了下只有title配置，为excel文档的标题

    var data = $('#grid').datagrid('getExcelXml', { title: Url }); //获取datagrid数据对应的excel需要的xml格式的内容
    //用ajax发动到动态页动态写入xls文件中
    var url = '/Handler/DatagridToExcel.ashx';
    //$.ajax({
    //    url: url, data: { data: data }, type: 'POST', dataType: 'text',
    //    success: function (fn) {
    //        showAlertMsg("导出excel成功！");
    //        window.location = '/Files/Download.aspx?path=' + fn + '&filename=' + escape(Url + '.xls'); //执行下载操作
    //    },
    //    error: function (xhr) {
    //        showAlertMsg('动态页有问题\nstatus：' + xhr.status + '\nresponseText：' + xhr.responseText)
    //    }
    //});
    //return false;

    var form = $("<form></form>")
    form.attr('action', url)
    form.attr('method', 'post')
    var inputData = $("<input type='hidden' name='data' />")
    inputData.attr('value', data)
    form.append(inputData)

    var fileName = $("<input type='hidden' name='fileName' />")
    fileName.attr('value', "" + Url + ".xls");
    form.append(fileName)
    form.appendTo("body")
    form.css('display', 'none')
    form.submit()
    return false;
}

/* 请求Ajax 带返回值，并弹出提示框提醒
--------------------------------------------------*/
function getAjax(url, parm, callBack) {
    $.ajax({
        type: 'post',
        dataType: "text",
        url: url,
        data: parm,
        cache: false,
        async: false,
        success: function (msg) {
            callBack(msg);
        }
    });
}
//标签切换0:属性操作；1：实体表操作
function GetTabMenuClick(e) {
    $("#menutab div").each(function () {
        this.className = "removesel";
    });
    e.className = "sel";
}
//清空所有input 输入框内的值
function resetInput() {
    $("input[type='text']").val("");
}
//刷新主表
function windowclose() {
    $('#grid').datagrid('clearSelections');
    $('#grid').datagrid('reload');
    $('#grid').datagrid({ loadMsg: '正在处理，请稍待。。。' });
}

/**
* 金额格式(保留2位小数)后格式化成金额形式
*/
function FormatCurrency(num) {
    num = num.toString().replace(/\$|\,/g, '');
    if (isNaN(num))
        num = "0";
    sign = (num == (num = Math.abs(num)));
    num = Math.floor(num * 100 + 0.50000000001);
    cents = num % 100;
    num = Math.floor(num / 100).toString();
    if (cents < 10)
        cents = "0" + cents;
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3) ; i++)
        num = num.substring(0, num.length - (4 * i + 3)) + '' +
                num.substring(num.length - (4 * i + 3));
    return (((sign) ? '' : '-') + num + '.' + cents);
}
/**
* 刷新页面
*/
function Replace() {
    window.location.href = window.location.href.replace('#', '');
}

/** 
* 获取本周、本季度、本月、上月的开始日期、结束日期 
*/
function fillstring(str) {
    if (str.length == 1) {
        str = "0" + str;
    }
    return (str);
}
var now = new Date();                    //当前日期     
var nowDayOfWeek = now.getDay();         //今天本周的第几天     
var nowDay = now.getDate();              //当前日     
var nowMonth = now.getMonth();           //当前月     
var nowYear = now.getYear();             //当前年     
nowYear += (nowYear < 2000) ? 1900 : 0;  //  
function quickseldate(type) {
    var begintime, endtime;
    if (type == "day") {
        begintime = getMonthStartDate();
        endtime = begintime;
    }
    else if (type == "week") {
        endtime = getWeekEndDate();
        begintime = getWeekStartDate(endtime);
    } else if (type == "month") {
        endtime = getMonthEndDate();
        begintime = getMonthStartDate(endtime);
    } else if (type == "quarter") {
        begintime = getQuarterStartDate();
        endtime = getQuarterEndDate();
    }
    document.getElementById("txtBeginTime").value = begintime;
    document.getElementById("txtEndTime").value = endtime;
}
//获取某月天数
function getDaysInMonth(year, month) {
    month = parseInt(month, 10) + 1;
    var temp = new Date(year + "/" + month + "/0");
    return temp.getDate();
}
//获得本月的开始日期     
function getMonthStartDate(Time) {
    var str = Time;
    var arry = str.split("-");
    var dt = new Date();
    var eRDate = new Date(arry[0], arry[1], arry[2]);
    var _Datenow = new Date();
    newDate = Date_Add("m ", -1, _Datenow);
    var Time = newDate.pattern("yyyy-MM-dd");
    return Time;
}
//获得本月的结束日期     
function getMonthEndDate() {
    var dt = new Date();
    y = dt.getFullYear();
    m = dt.getMonth() + 1;
    d = dt.getDate();
    return y + "-" + m + "-" + d;
}
//获得本周的开始日期     
function getWeekStartDate(Time) {
    var dt1 = Date.parse(Time.replace(/-/g, "/"));
    var date = new Date(dt1);
    date.setDate(date.getDate() - 7);
    return date.toString();
}
//获得本周的结束日期     
function getWeekEndDate() {
    var dt = new Date();
    y = dt.getFullYear();
    m = dt.getMonth() + 1;
    d = dt.getDate();
    return y + "-" + m + "-" + d;
}
//格式化日期：yyyy-MM-dd     
function formatDate(date) {
    var myyear = date.getFullYear();
    var mymonth = date.getMonth() + 1;
    var myweekday = date.getDate();

    if (mymonth < 10) {
        mymonth = "0" + mymonth;
    }
    if (myweekday < 10) {
        myweekday = "0" + myweekday;
    }
    return (myyear + "-" + mymonth + "-" + myweekday);
}
//获得某月的天数     
function getMonthDays(myMonth) {
    var monthStartDate = new Date(nowYear, myMonth, 1);
    var monthEndDate = new Date(nowYear, myMonth + 1, 1);
    var days = (monthEndDate - monthStartDate) / (1000 * 60 * 60 * 24);
    return days;
}
//获得本季度的开始月份     
function getQuarterStartMonth() {
    var quarterStartMonth = 0;
    if (nowMonth < 3) {
        quarterStartMonth = 0;
    }
    if (2 < nowMonth && nowMonth < 6) {
        quarterStartMonth = 3;
    }
    if (5 < nowMonth && nowMonth < 9) {
        quarterStartMonth = 6;
    }
    if (nowMonth > 8) {
        quarterStartMonth = 9;
    }
    return quarterStartMonth;
}
//获得本季度的开始日期     
function getQuarterStartDate() {
    var quarterStartDate = new Date(nowYear, getQuarterStartMonth(), 1);
    return formatDate(quarterStartDate);
}

//或的本季度的结束日期     
function getQuarterEndDate() {
    var quarterEndMonth = getQuarterStartMonth() + 2;
    var quarterStartDate = new Date(nowYear, quarterEndMonth, getMonthDays(quarterEndMonth));
    return formatDate(quarterStartDate);
}
/*js的日期计算方法 本月，上月，下月*/
var currentmonth, currentday, currentweek
currentmonth = 0
currentday = 0
currentweek = 0
function showToDay() {
    var Nowdate = new Date();
    M = Number(Nowdate.getMonth()) + 1
    return Nowdate.getFullYear() + "-" + M + "-" + Nowdate.getDate();
}
function showMonthFirstDay(cases) {
    var Nowdate = new Date();
    switch (cases) {
        case "":
            var MonthFirstDay = new Date(Nowdate.getFullYear(), Nowdate.getMonth(), 1);
            return MonthFirstDay;
            break;
        case "n":
            var MonthFirstDay = new Date(Nowdate.getFullYear(), Nowdate.getMonth() + 1 + currentmonth, 1);
            return MonthFirstDay;
            break;
        case "p":
            var MonthFirstDay = new Date(Nowdate.getFullYear(), Nowdate.getMonth() - 1 + currentmonth, 1);
            return MonthFirstDay;
            break;
    }
}
function showMonthLastDay(cases) {
    var Nowdate = new Date();
    switch (cases) {
        case "":
            currentmonth = 0;
            currentday = 0;
            var MonthNextFirstDay = new Date(Nowdate.getFullYear(), Nowdate.getMonth() + 1 + currentmonth, 1);
            var MonthLastDay = new Date(MonthNextFirstDay - 86400000);
            return MonthLastDay;
            break;
        case "n":
            var MonthNextFirstDay = new Date(Nowdate.getFullYear(), Nowdate.getMonth() + 2 + currentmonth, 1);
            var MonthLastDay = new Date(MonthNextFirstDay - 86400000);
            return MonthLastDay;
            break;
        case "p":
            var MonthNextFirstDay = new Date(Nowdate.getFullYear(), Nowdate.getMonth() + currentmonth, 1);
            var MonthLastDay = new Date(MonthNextFirstDay - 86400000);
            return MonthLastDay;
            break;
    }
}
Date.prototype.toString = function () {
    return this.getFullYear() + "-" + (this.getMonth() + 1) + "-" + this.getDate();
}
function InputDate(s, StartTime, EndTime) {
    switch (s) {
        case 4: //上月
            document.getElementById(StartTime).value = showMonthFirstDay("p");
            document.getElementById(EndTime).value = showMonthLastDay("p");
            break;
        case 5: //本月
            document.getElementById(StartTime).value = showMonthFirstDay("");
            document.getElementById(EndTime).value = showMonthLastDay("");
            break;
        case 6: //下月
            document.getElementById(StartTime).value = showMonthFirstDay("n");
            document.getElementById(EndTime).value = showMonthLastDay("n");
            break;
        default:
            alert("未知的参数");
    }
}
//获取当前日期
function _getNow() {
    var dt = new Date();
    y = dt.getFullYear();
    m = dt.getMonth() + 1;
    d = dt.getDate();
    return y + "-" + m + "-" + d;
}
//获取当前日期
function _getNowYM() {
    var dt = new Date();
    y = dt.getFullYear();
    m = dt.getMonth() + 1;

    return y + "-" + m;
}
//上月
function Time_Up(Stattime, endtime) {
    javascript: InputDate(4, Stattime, endtime); currentmonth--;
}
//上月
function Time_Next(Stattime, endtime) {
    javascript: InputDate(6, Stattime, endtime); currentmonth++;
}
//上传file控件
var isIE = /msie/i.test(navigator.userAgent);
var isFF = /firefox/i.test(navigator.userAgent);
var detlaX, detlaY, ooo;
function beginDrag(me, evt) {
    e = evt || window.event;
    ooo = me;
    document.onmousemove = move;
    document.onmouseup = up;
    if (isIE) me.setCapture();
    if (isFF) window.captureEvents(Event.MOUSEMOVE | Event.MOUSEUP);
}
function move(evt) {
    e = evt || window.event;
    document.getElementById("f").style.left = e.clientX - 55 + "px";
    document.getElementById("f").style.top = e.clientY - 10 + "px";
}
function up() {
    document.onmousemove = null;
    document.onmouseup = null;
    if (isIE) ooo.releaseCapture();
    if (isFF) window.releaseEvents(Event.MOUSEMOVE | Event.MOUSEUP);
}

/***************************当前日期，加月份，减月份******
var _Datenow = new Date();
//2加两个月.  -2减月份
newDate = Date_Add("m ", 2, _Datenow);
alert(newDate.pattern("yyyy-MM-dd"));
********************************************/
/***********当天日期*************/
function Get_DateTime() {
    var dt = new Date();
    y = dt.getFullYear();
    m = dt.getMonth() + 1;
    d = dt.getDate();
    return y + "-" + m + "-" + d;
}
function Date_Add(interval, number, date) {
    /* 
    *   功能:实现VBScript的DateAdd功能. 
    *   参数:interval,字符串表达式，表示要添加的时间间隔. 
    *   参数:number,数值表达式，表示要添加的时间间隔的个数. 
    *   参数:date,时间对象. 
    *   返回:新的时间对象. 
    *   var   now   =   new   Date(); 
    *   var   newDate   =   DateAdd( "d ",5,now); 
    *---------------   DateAdd(interval,number,date)   ----------------- 
    */
    switch (interval) {
        case "y ":
            {
                date.setFullYear(date.getFullYear() + number);
                return date;
                break;
            }
        case "q ":
            {
                date.setMonth(date.getMonth() + number * 3);
                return date;
                break;
            }
        case "m ":
            {
                date.setMonth(date.getMonth() + number);
                return date;
                break;
            }
        case "w ":
            {
                date.setDate(date.getDate() + number * 7);
                return date;
                break;
            }
        case "d ":
            {
                date.setDate(date.getDate() + number);
                return date;
                break;
            }
        case "h ":
            {
                date.setHours(date.getHours() + number);
                return date;
                break;
            }
        case "m ":
            {
                date.setMinutes(date.getMinutes() + number);
                return date;
                break;
            }
        case "s ":
            {
                date.setSeconds(date.getSeconds() + number);
                return date;
                break;
            }
        default:
            {
                date.setDate(d.getDate() + number);
                return date;
                break;
            }
    }
}
Date.prototype.pattern = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1, //月份      
        "d+": this.getDate(), //日      
        "h+": this.getHours() % 12 == 0 ? 12 : this.getHours() % 12, //小时      
        "H+": this.getHours(), //小时      
        "m+": this.getMinutes(), //分      
        "s+": this.getSeconds(), //秒      
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度      
        "S": this.getMilliseconds() //毫秒      
    };
    var week = {
        "0": "/u65e5",
        "1": "/u4e00",
        "2": "/u4e8c",
        "3": "/u4e09",
        "4": "/u56db",
        "5": "/u4e94",
        "6": "/u516d"
    };
    if (/(y+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    if (/(E+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, ((RegExp.$1.length > 1) ? (RegExp.$1.length > 2 ? "/u661f/u671f" : "/u5468") : "") + week[this.getDay() + ""]);
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        }
    }
    return fmt;
}
//保留两位小数    
//功能：将浮点数四舍五入，取小数点后2位   
function toDecimal(x) {
    var f = parseFloat(x);
    if (isNaN(f)) {
        return;
    }
    f = Math.round(x * 100) / 100;
    return f;
}

$.extend({
    getUrlVars: function (url) {
        var vars = [], hash;
        var hashes = url.split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    },
    getUrlVar: function (url, name) {
        return $.getUrlVars(url)[name];
    }
});