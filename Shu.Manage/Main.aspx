<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Shu.Manage.Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%=Shu.Comm.CommTools.GetXitongMingCheng() %>
    </title>
    <link href="/Styles/default.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/Menu.css" rel="stylesheet" type="text/css" />
    <link href="/Content/themes/bootstrap/easyui.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-2.0.0.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.easyui-1.4.5.min.js" type="text/javascript"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script src="/Scripts/Grid/Grid.js" type="text/javascript"></script>
    <script src="/Scripts/menu.js" type="text/javascript"></script>
    <script src="/Scripts/main.js" type="text/javascript"></script>
    <script src="/Scripts/tab.js" type="text/javascript"></script>
    <link href="/Styles/top.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/table.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        p {
            margin-right: 20px;
        }

        .logo {
            background: rgba(0, 0, 0, 0) url("/Images/main/logo.png") no-repeat scroll left top;
            float: left;
            height: 59px;
            width: 1280px;
        }
    </style>
</head>
<body class="easyui-layout" style="overflow-y: hidden" fit="true" scroll="no">
    <div id="dd">
    </div>
    <noscript>
        <div style="position: absolute; z-index: 100000; height: 2046px; top: 0px; left: 0px; width: 100%; background: white; text-align: center;">
            <img src="images/noscript.gif" alt='抱歉，请开启脚本支持！' />
        </div>
    </noscript>
    <div id="loading-mask" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%; background: #fff; z-index: 20000">
        <div id="pageloading" style="position: absolute; top: 50%; left: 50%; margin: -120px 0px 0px -120px; text-align: center; border: 0px solid #8DB2E3; width: 200px; height: 40px; font-size: 14px; padding: 10px; font-weight: bold; background: #fff; color: #15428B;">
            <img src="/Images/main/loader.gif" alt="" align="absmiddle" style="border: none;" />
            正在加载中,请稍候...
        </div>
    </div>
    <div region="north" split="true" style="height: 64px; overflow: hidden; background: url(/Images/main/bg.png) repeat-x; width: 100%; overflow: hidden; position: relative;">
        <div class="logo" id="logo">
        </div>
        <div class="user">
            <div class="userLeft">
            </div>
            <div class="userBg">
                <span style="color: #fff">
                    <a href="javascript:void(0)" style="color: #bed7f1;" onclick="showTabs('个人信息维护','/Manage/Sys/UserInfoModify.aspx');return false;">
                        <asp:Label ID="LoginName" runat="server"></asp:Label></a>,欢迎您！</span> | <a href="javascript:void(0);"
                            onclick="showPwd();"><span style="color: #bed7f1">修改密码</span></a> | <a href="javascript:void(0);"
                                onclick="javascript:if(confirm('您确定要退出系统吗？')) {LogOut();return false;}" target="_parent">
                                <span style="color: #bed7f1">安全退出</span></a>
            </div>
            <div class="userRight">
            </div>
        </div>
        <%--<div class="time">
            今天是：<span id="hometime"><script language="JavaScript" type="text/javascript"> <!--
                                        today = new Date();
                                        function initArray() {
                                            this.length = initArray.arguments.length
                                            for (var i = 0; i < this.length; i++) {
                                                this[i + 1] = initArray.arguments[i]
                                            }
                                        }
                                        var d = new initArray("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");
                                        document.write("", today.getFullYear(), "年", "", today.getMonth() + 1, "月", "", today.getDate(), "日 ", d[today.getDay() + 1]); //--></script>/span><</div>--%>
    </div>
    <div region="south" split="true" style="height: 30px;">
        <span style="text-align: left; font-weight: normal; line-height: 22px;">今天是：<span id="hometime"><script language="JavaScript" type="text/javascript"> <!--
    today = new Date();
    function initArray() {
        this.length = initArray.arguments.length
        for (var i = 0; i < this.length; i++) {
            this[i + 1] = initArray.arguments[i]
        }
    }
    var d = new initArray("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");
    document.write("", today.getFullYear(), "年", "", today.getMonth() + 1, "月", "", today.getDate(), "日 ", d[today.getDay() + 1]); //--></script></span>
            <span style="text-align: center; font-weight: normal; line-height: 22px;">(建议您使用IE9以上或火狐浏览器,分辨率设置为 1366*768)</span>
            <%--<span style="margin-left:20px;"><a href="javascript:void(0);" onclick="showTabs('系统必备工具下载','/Manage/Sys/SoftwareList.aspx');return false;" style="color :blue;">系统必备工具下载</a></span>--%>
            <div class="footer" style="font-weight: normal; line-height: 22px;">
                <a href="http://www.    hfheshun.net" target="_blank"><%=Shu.Comm.CommTools.GetConfigName("CompanyInfo")%></a>&nbsp;&nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp;&nbsp; &nbsp; <a href="javascript:void(0);" onclick="LoadMsg();return false;">
                <img src="../Images/Tree/x_01.gif" alt="系统消息" /></a>
            </div>
    </div>
    <div region="west" split="true" title="导航菜单" style="width: 180px;" id="west">
        <div id="nav">
            <!--  导航内容 -->
        </div>
    </div>
    <div id="mainPanle" region="center" style="background: #eee; overflow-y: hidden">
        <div id="tabs" class="easyui-tabs" fit="true" border="false">
        </div>
    </div>
    <div id="d2" class="easyui-dialog" title="修改密码" style="width: 300px; height: 200px; padding: 10px;"
        toolbar="#dlg-toolbar" buttons="#dlg-buttons" resizable="false"
        closed="true">
        <table style="width: 100%; display: none" id="t_midfypwd">
            <tr>
                <td>原密码：
                </td>
                <td>
                    <input id="txt_SrcUserPwd" type="password" size="20" maxlength="16" />
                </td>
            </tr>
            <tr>
                <td>新密码：
                </td>
                <td>
                    <input id="txt_NewUserPwd" type="password" size="20" maxlength="16" />
                </td>
            </tr>
            <tr>
                <td>确认密码：
                </td>
                <td>
                    <input id="txt_ConfirmNewUserPwd" type="password" size="20" maxlength="16" />
                </td>
            </tr>
        </table>
    </div>
    <div id="dlg-buttons" style="display: none;">
        <a href="#" class="easyui-linkbutton" onclick="ModifyPwd()">保存</a> <a href="#" class="easyui-linkbutton"
            onclick="closePwd();">关闭</a>
    </div>
    <div id="mm" class="easyui-menu" style="width: 150px;">
        <div id="tabupdate">
            刷新
        </div>
        <div class="menu-sep">
        </div>
        <div id="close">
            关闭
        </div>
        <div id="closeall">
            全部关闭
        </div>
        <div id="closeother">
            除此之外全部关闭
        </div>
        <div class="menu-sep">
        </div>
        <div id="closeright">
            当前页右侧全部关闭
        </div>
        <div id="closeleft">
            当前页左侧全部关闭
        </div>
        <div class="menu-sep">
        </div>
        <%--<div id="exit">
            退出</div>--%>
    </div>

    <div id="ui_Image" class="easyui-window" title="图片" style="width: 830px; height: 550px;" data-options="modal:false,draggable:true,resizable:false,collapsible:false"
        toolbar="#dlg-toolbar" resizable="true" closed="true">
        <iframe id="view_Image" scrolling="yes" width="99%" frameborder="0" height="510px"></iframe>
    </div>

    <!--弹出框-->
    <div id="uctextselects" class="easyui-dialog" title="车辆GPS定位" style="width: 880px; height: 450px;"
        toolbar="#dlg-toolbar" resizable="true" closed="true" modal="true">
        <iframe id="ucviews" scrolling="yes" width="100%" frameborder="0" height="350" style="overflow-x: hidden"></iframe>
    </div>


    <div id="UI-Task" class="easyui-window" title="流程任务处理" style="width: 830px; height: 550px;" data-options="modal:true,draggable:false,resizable:false,collapsible:false,maximizable:false,maximized:true,minimizable: false,shadow: true"
        toolbar="#dlg-toolbar" resizable="true" closed="true">
        <iframe id="view_Task" scrolling="yes" width="99%" frameborder="0" height="520px"></iframe>
    </div>

    <!--弹出框-->
    <div id="uctextTenantID" class="easyui-dialog" title="历史申请单" style="width: 880px; height: 420px;"
        toolbar="#dlg-toolbar" resizable="true" closed="true" modal="true">
        <iframe id="ucTenantIDviews" scrolling="yes" width="100%" frameborder="0" height="380" style="overflow-x: hidden"></iframe>
    </div>

    <div id="MaxPanel">
        <iframe id="view_MaxPanel" scrolling="yes" width="100%" frameborder="0" height="100%"></iframe>
        <%--<div id="cc" class="easyui-layout" style="width: 100%; height: 100%;">
            <div id="ccwest" data-options="region: 'west',title: ' ',split: true" style="width: 100px;"></div>
            <div data-options="region:'center',title:' ',href:'west_content.php'" style="height: 200px;"></div>
            <div data-options="region:'south',split:true" style="height: 100px;">
                <div id="ccsouth" class="easyui-layout" data-options="fit:true" style="width: 100%; height: 100%;">
                    <div id="ccsouthwest" data-options="region:'west',split:true" style="width: 100px;"></div>
                    <div data-options="region:'center'" style="height: 200px;">
                        <ul id="tt">
                        </ul>
                    </div>
                </div>
            </div>
        </div>--%>
    </div>

    <script type="text/javascript">
        $(function () {
            $('#UI-Task').window({
                onBeforeClose: function () { //当面板关闭之前触发的事件
                    if (true) {
                        if ($('#tabs').tabs('exists', "我的看板")) {
                            $('#tabs').tabs('close', '我的看板');
                            addDesktop("我的看板", "WelCome.htm", "");
                        }
                        if ($('#tabs').tabs('exists', "待办事项")) {
                            $('#tabs').tabs('close', '待办事项');
                            addDesktop("待办事项", "/Manage/Sys/SysPendingMatterList.aspx", "");
                        }
                    } else
                        return false;
                }
            });
            //MaxPanel();
        });

        function MaxPanel(url,width) {
            //layout_Panel();
            $('#MaxPanel').window({
                title: '分栏预览版',
                modal: true,
                maximized: true,
                collapsible: false,
                minimizable: false,
                maximizable: false,
            });
            $('#view_MaxPanel').attr('src', '/Manage/Business/ApplyColumnPreview.aspx?' + url + '&width=' + width);
        }

        
    </script>
</body>
</html>
