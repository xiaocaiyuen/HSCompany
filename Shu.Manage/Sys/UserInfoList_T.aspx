<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfoList_T.aspx.cs" Inherits="YDT.Web.Manage.Sys.UserInfoList_T" %>

<%@ Register Src="~/UserControls/UCGrid.ascx" TagName="UCGrid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Styles/List.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.8.0.min.js" type="text/javascript"></script>
     <script type="text/javascript">
         window.onload = function () {
             GridSubStr("UCGrid", 2, 12);
             GridSubStr("UCGrid", 3, 8);
         }
         //
        </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="HidDepCode" runat="server" />
    <fieldset class="listfieldset">
        <legend>查询条件</legend>
        <table cellpadding="0" cellspacing="0" class="list_Index">
            <tr>
                <th>
                    部门
                </th>
                <td>
                    <asp:TextBox ID="txtDepName" runat="server" CssClass="input3"></asp:TextBox>
                </td>
                <th>
                    岗位
                </th>
                <td>
                    <asp:TextBox ID="txtPostName" runat="server" CssClass="input3"></asp:TextBox>
                </td>
                <th style="width: 180px;">
                    <asp:ImageButton ID="btn_Search" runat="server" ImageUrl="~/Images/buttons/chaXun.gif"
                        OnClick="btnSearch_Click" />
                    &nbsp;
                    <asp:ImageButton ID="btnChongzhi" ImageUrl="~/Images/buttons/ChongZhiChaXun.gif"
                        runat="server" OnClick="btnChongzhi_Click" />
                </th>
            </tr>
        </table>
    </fieldset>
    <uc1:UCGrid ID="UCGrid" runat="server" />
    </form>
</body>
</html>
