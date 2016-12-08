<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysPendingMatterList.aspx.cs" Inherits="YDT.Web.Manage.Sys.SysPendingMatterList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <%--<link href="/Styles/table.css" rel="stylesheet" type="text/css" />--%>
    <link href="/Styles/default.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.8.0.min.js" type="text/javascript"></script>
    <style type="text/css">
        body {
            margin: 0px;
            padding: 0px;
            color: #000;
            text-decoration: none;
            background-color: #ffffff;
            font: normal 12px/22px Verdana, Geneva, sans-serif;
        }

        div, dl, dt, dd, ol, ul, li, h1, h2, h3, h4, h5, h6, pre, form, fieldset, input, textarea, blockquote, p {
            padding: 0;
            margin: 0;
        }

        h1, h2, h3, h4, h5, h6 {
            font-size: 12px;
            font-style: normal;
            font-weight: normal;
            font-variant: normal;
        }

        ol, ul, li {
            list-style: none;
        }

        div {
            background: url(none);
            margin: 0px;
            padding: 0px;
            border-style: none;
        }
        /*img { border-style: none;
    border-color: inherit;
    border-width: 0;
    vertical-align:top;
    width: 14px;
    height: 16px;
}*/
        a {
            font-size: 12px;
            color: #000;
            text-decoration: none;
        }

            a:visited {
                text-decoration: none;
            }

            a:hover {
                color: #F00;
                text-decoration: underline;
            }

            a:active {
                color: #999;
            }

        .tab th {
            text-align: center;
            line-height: 50px;
        }

        .tab td {
            text-align: center;
            line-height: 30px;
        }
    </style>
    <script type="text/javascript">

        function SendFloat(num, uName, uid) {


            window.parent.GetFloat(num, uName, uid);
        }

        function TaskShowClick(title, url) {
            //debugger
            parent.TaskShow(title, url);
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hid_Count" runat="server" Value="0" />
        <div align="center">
            <table class="grid" cellspacing="0" cellpadding="1" border="0" id="UCGrid_GridView1"
                style="height: 100%; width: 780px; border-collapse: collapse; margin-top: 20px;"
                align="center">
                <tr>
                    <th width="40"></th>
                    <th width="120">任务状态</th>
                    <th width="90">申请编号</th>
                    <th width="90">客户姓名</th>
                    <th width="90">车辆类型</th>
                    <%--<th width="80">融资金额</th>--%>
                    <th width="140">申请日期</th>
                    <th>操作</th>
                </tr>
                <asp:Literal ID="LitLCT" runat="server"></asp:Literal>
            </table>
        </div>
    </form>
</body>
</html>
