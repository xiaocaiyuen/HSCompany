<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReferenceRateInfo.aspx.cs" Inherits="YDT.Web.Manage.Sys.ReferenceRateInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Scripts/UI/themes/bootstrap/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/List.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="/Scripts/UI/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="/Styles/validationEngine.jquery.css" rel="stylesheet" type="text/css"
        charset="utf-8" />
    <script src="/Scripts/Validate/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="/Scripts/Validate/jquery.validationEngine-cn.js" type="text/javascript"
        charset="gb2312"></script>
    <script src="/Scripts/DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Scripts/Validate/FormVaildate.js" type="text/javascript"></script>  
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hid_Flag" runat="server" />
    <div id="divCont" runat="server">
        <table width="100%" style="height: 160px; margin-top:30px;">
            <tr>
                <td style="text-align: right; width: 80px;">
                    基准利率：
                </td>
                <td>
                    <asp:TextBox ID="txtReferenceRate_InterestRate" runat="server" CssClass="input4 validate[required,custom[onlyNumber]]" MaxLength="10"
                        Style="width: 180px; line-height: 20px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    调整日期：
                </td>
                <td>
                    <asp:TextBox ID="txtReferenceRate_AdjustDate" CssClass="input4 validate[required]" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',isShowClear:false,minDate:'%y-%M-%d'})" 
                        Width="180px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center;">
                    <asp:ImageButton ImageUrl="~/Images/buttons/baocun.gif" runat="server" ID="btnSave"
                        OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
