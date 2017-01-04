<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyDesktop.aspx.cs" Inherits="Shu.Manage.MyDesktop" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Styles/table.css" rel="stylesheet" />
    <script type="text/javascript" src="/Scripts/jquery-2.0.0.min.js"></script>
    <link href="/Content/themes/bootstrap/easyui.css" rel="stylesheet" />
    <link href="/Content/themes/icon.css" rel="stylesheet" />
    <script type="text/javascript" src="/Scripts/jquery.easyui-1.4.5.min.js" charset="utf-8"></script>
    <script type="text/javascript" src="/Scripts/locale/easyui-lang-zh_CN.js" charset="utf-8"></script>
    <script type="text/javascript">
        var iID = setInterval("Refresh()", 300000);//5分钟刷新一次
        $(function () {
            ListDataSource("MySysNotice");
            ListDataSource("MySysMessage");
            $('#tt').tabs({
                border: false,
                onSelect: function (title) {
                    if (title == "我的待办任务") RefreshMyWaitingTask();
                    if (title == "我的已办任务") RefreshMyToDoTask();
                }
            });
        });

        function Refresh() {
            RefreshMyWaitingTask();
            RefreshMyToDoTask();
        }

        function RefreshMyWaitingTask() {
            $('#chart1').attr("src", "/Workflow/MyWaitingTask.aspx?MyDesktop=true");
        }

        function RefreshMyToDoTask() {
            $('#chart2').attr("src", "/Workflow/MyToDoTask.aspx?MyDesktop=true");
        }

        function ListDataSource(type) {
            $.ajax({
                type: "post",
                url: "/Handler/DesktopHandler.ashx?method=" + type + "",
                data: {},
                dataType: "text",
                success: function (rows, data) {
                    if (type == "MySysNotice")
                        $("#list_show").html(rows);
                    if (type == "MySysMessage")
                        $("#list_show1").html(rows);
                }
            });
        }

        function CloseMyTeskDedailPage(returnURL) {

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="p1" class="easyui-panel" title="我的任务中心(5分钟自动刷新任务)" data-options="collapsible:true" style="height: 250px;">
                <div id="tt" class="easyui-tabs">
                    <div title="我的待办任务" style="padding: 10px">
                        <iframe id="chart1" scrolling="auto" frameborder="0" src="/Workflow/MyWaitingTask.aspx?MyDesktop=true" style="width: 100%; height: 100%;"></iframe>
                    </div>
                    <%-- <div title="我的已办任务" style="padding: 10px">
                        <iframe id="chart2" scrolling="auto" frameborder="0" src="#" style="width: 100%; height: 100%;"></iframe>
                    </div>--%>
                </div>
            </div>
            <table border="0" style="height: 250px; border-width: 0px; border-style: none; margin: 0px auto;">
                <tr>
                    <td style="width: 595px; min-width: 390px;">
                        <div id="p2" class="easyui-panel" title="<a href=javascript:void(0) style='color: #0060cc;' onclick=parent.showTabs('系统通知公告','/Sys/NoticeList.aspx')>系统通知公告</a>" data-options="collapsible:true" style="height: 230px; min-width: 390px;">
                            <div id="list_show" style="margin: 0 auto; width: 100%">
                            </div>
                        </div>
                    </td>
                    <td style="width: 595px; min-width: 390px;">
                        <div id="p3" class="easyui-panel" title="<a href=javascript:void(0) style='color: #0060cc;' onclick=parent.showTabs('系统消息提醒','/Sys/PerMessageList.aspx')>系统消息提醒</a>" data-options="collapsible:true" style="height: 230px; min-width: 390px;">
                            <div id="list_show1" style="margin: 0 auto; width: 100%">
                            </div>
                        </div>
                    </td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
