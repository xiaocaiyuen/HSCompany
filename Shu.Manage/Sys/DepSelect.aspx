<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepSelect.aspx.cs" Inherits="Shu.Manage.Sys.DepSelect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="/UserControls/UCExpAllDeptEasyUISingleTreeCheckBox.ascx" TagName="UcDepEasyUI" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        ol, ul, li {
            list-style: none;
            text-align: left;
        }
    </style>
    <%--<link href="../../Scripts/UI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/DatePicker/Year_WdatePicker.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="../../Scripts/UI/jquery.easyui.min.js" type="text/javascript"></script>--%>
    <script type="text/javascript" src="/Scripts/jquery-2.0.0.min.js"></script>
    <link href="/Content/themes/bootstrap/easyui.css" rel="stylesheet" />
    <link href="/Content/themes/icon.css" rel="stylesheet" />
    <script type="text/javascript" src="/Scripts/jquery.easyui-1.4.5.min.js" charset="utf-8"></script>
    <script type="text/javascript" src="/Scripts/locale/easyui-lang-zh_CN.js" charset="utf-8"></script>
    <script type="text/javascript">

        function ReturnOK() {
            var chkDepID = getCheckedDepID();
            if (chkDepID == "") {
                alert('请选择部门！');
                return false;
            }
            var chkName = getCheckedDepName();
            ReloadGrid(chkDepID, chkName);
        }


        function closeMy() {
            window.parent.CloseView();
            return false;
        }

        function ReloadGrid(ids, names) {
            window.parent.ReloadPage(ids, names);
            closeMy();
        }
    </script>
</head>
<body style="margin: 0; padding: 0; overflow-x: hidden">
    <form id="form2" runat="server">
        <table cellpadding="0" cellspacing="0" width="580">
            <tr>
                <td style="width: 150px;">
                    <uc1:ucdepeasyui id="UcDepEasyUI1" runat="server" />
                </td>
                <td style="vertical-align: top;">
                    <table cellpadding="0" cellspacing="0" style="padding-left: 20px; font-size: 12px;">
                        <tr>
                            <td colspan="2" style="text-align: center; height: 30px">
                                <img alt="" id="btnSave" src="/Images/buttons/queding1.gif" onclick="ReturnOK();" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
