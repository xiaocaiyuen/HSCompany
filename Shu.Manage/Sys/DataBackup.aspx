<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataBackup.aspx.cs" Inherits="YDT.Web.Manage.Sys.DataBackup" %>
<%@ Register Src="~/UserControls/UCEasyUIDataGrid.ascx" TagPrefix="UC" TagName="UCEasyUIDataGrid" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../Styles/table.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 182px;
        }
    </style>
</head>
<body>
    <form id="form2" name="form2" runat="server">

       <table width="780" border="0" cellspacing="0" cellpadding="0" class="headerCss">
    <tr>
      <th colspan="4" >数据备份</th>
     </tr>
    </table>
      <table  class="tab" cellpadding="10" cellspacing="1" width="780">
    <tr>
        <th class="style1">
            备份文件名</th>
        <td>
            <asp:Label ID="lblFileName" name="lblFileName" runat="server"></asp:Label>
        </td>
  </tr>
  <tr>
    <th class="style1">服务器备份路径</th>
    <td><asp:Label ID="lblFilePath" name="lblFilePath" runat="server"></asp:Label></td>
  </tr>
  <tr>
    <th colspan="2"><asp:ImageButton ID="btnSure" runat="server" 
            ImageUrl="/images/buttons/beifen.gif" onclick="btnSure_Click" /></th>
  </tr>
  </table>
    </form>
    <div style="width:780px;margin: 0px auto;">
<UC:UCEasyUIDataGrid ID="UCEasyUIDataGrid" runat="server" />
</div>
</body>
</html>
