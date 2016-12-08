<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sys_Setting.aspx.cs" Inherits="YDT.Web.Manage.Sys.Sys_Setting" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Styles/table.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
     <table width="750" border="0" align="center" cellpadding="0" cellspacing="0" class="headerCss" >
        <tr>
            <td>
                系统设置管理
            </td>
        </tr>
    </table>
      <table cellpadding="10"  align="center" cellspacing="1" width="750" class="tab" >
      <tr>
            <th style=" width:200px;">&nbsp;预警短信设置</th>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <th style=" width:200px;">是否自动发送预警短信：</th>
            <td>
                <asp:DropDownList ID="ddlSetting_Value" runat="server">
                   <asp:ListItem Text="是" Value="是"></asp:ListItem>
                   <asp:ListItem Text="否" Value="否"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th><asp:Label ID="lblSetting_Name" runat="server"></asp:Label></th>
            <td>
              <span style=" color:Red;"> 注：* 请确保内容格式涵盖一下编码：
              <br />（1）"[user]" 表示：接收短信的人名
              <br />（2）"[warn]" 表示：预警的内容
                <br />
                上述编码信息，程序将自动替换成对应的数据信息。<br />
              <br />
              </span>
                <asp:TextBox ID="txtSetting_Remarks" runat="server" TextMode="MultiLine" Width="500"  Rows="6"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th style=" width:200px;">执法监督短信格式</th>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <th><asp:Label ID="lblSetting_Name2" runat="server"></asp:Label></th>
            <td>
              <span style=" color:Red;"> 注：* 请确保内容格式涵盖一下编码： 
                  <br />（1）"{0}" 表示：服务对象的姓名
                  <br />（2）"{1}" 表示：服务事项的名称
                  <br />上述编码信息，程序将自动替换成对应的数据信息。<br />
              <br />
              </span>
                <asp:TextBox ID="txtSetting_Remarks2" runat="server" TextMode="MultiLine" Width="500"  Rows="6"></asp:TextBox>
            </td>
        </tr>
      </table>
          <table width="750" border="0" cellspacing="0" cellpadding="0" class="for">
<tr>
    <th>
    <asp:ImageButton runat="server" ID="btnSave"  OnClick="btnSave_Click" ImageUrl="~/Images/buttons/baocun.gif" />
                &nbsp;
    </th>
  </tr>
</table>
    </form>
</body>
</html>