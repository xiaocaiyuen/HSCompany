<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BankEdit.aspx.cs" Inherits="YDT.Web.Manage.Sys.BankEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="/Scripts/jquery-1.8.0.min.js" type="text/javascript"></script>
    <link href="/Styles/table.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/List.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/DatePicker/WdatePicker.js" type="text/javascript"></script>
    <%--录入验证 --%>
    <link href="/Styles/validationEngine.jquery.css" rel="stylesheet" type="text/css" charset="utf-8" />
    <script src="../../Scripts/Validate/2.6.2Ciaoca/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="../../Scripts/Validate/2.6.2Ciaoca/jquery.validationEngine.min.js" type="text/javascript"></script>
    <script src="../../Scripts/Validate/2.6.2Ciaoca/jquery.validationEngine-zh_CN.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript">
        $(function () {
            $("#form1").validationEngine({ promptPosition: "centerRight", validationEventTriggers: "keyup blur" });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <center>
            <table width="780" border="0" cellspacing="0" cellpadding="0" class="headerCss">
                <tr>
                    <th colspan="4">银行信息维护
                    </th>
                </tr>
            </table>
            <table width="780" class="tab" border="0" cellpadding="10" cellspacing="1">
                <tr>
                    <th width="120px">银行名称<font color="red">*</font>
                    </th>
                    <td width="270px">
                        <asp:TextBox ID="txtBank_Name" runat="server" CssClass="input4 validate[required,maxSize[50]]" MaxLength="50" Style="width: 240px;"></asp:TextBox>
                    </td>
                    <th width="120px">银行编号<font color="red">*</font>
                    </th>
                    <td width="270px">
                        <asp:TextBox ID="txtBank_No" runat="server" CssClass="input4 validate[required,maxSize[50]]" MaxLength="50" Style="width: 240px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th width="120px">联系人<font color="red">*</font>
                    </th>
                    <td width="270px">
                        <asp:TextBox ID="txtBank_Contact" runat="server" CssClass="input4 validate[required,maxSize[10]]" MaxLength="10" Style="width: 240px;"></asp:TextBox>
                    </td>
                    <th width="120px">联系电话<font color="red">*</font>
                    </th>
                    <td width="270px">
                        <asp:TextBox ID="txtBank_Telephone" runat="server" CssClass="input4 validate[required,custom[Telephone]]" Style="width: 240px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th width="120px">银行类型
                    </th>
                    <td width="270px">
                        <asp:DropDownList ID="ddlBank_Type" runat="server" CssClass="input4" Style="width: 240px;"></asp:DropDownList>
                    </td>
                    <th width="120px">银行地址
                    </th>
                    <td width="270px">
                        <asp:TextBox ID="txtBank_Address" runat="server" CssClass="input4 validate[required,maxSize[200]]" MaxLength="200" Style="width: 240px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th width="120px">银行账号<font color="red">*</font>
                    </th>
                    <td width="270px">
                        <asp:TextBox ID="txtBank_Account" runat="server" CssClass="input4 validate[required,maxSize[30]]" MaxLength="30" Style="width: 240px;"></asp:TextBox>
                    </td>
                    <th width="120px">账户名称<font color="red">*</font>
                    </th>
                    <td width="270px">
                        <asp:TextBox ID="txtBank_AccountName" runat="server" CssClass="input4 validate[required,maxSize[50]]" MaxLength="50" Style="width: 240px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th width="120px">账户类型
                    </th>
                    <td width="270px">
                         <asp:DropDownList ID="ddlBank_AccountType" runat="server" CssClass="input4" Style="width: 240px;"></asp:DropDownList>
                    </td>
                    <th width="120px">开户银行<font color="red">*</font>
                    </th>
                    <td width="270px">
                       <asp:TextBox ID="txtBank_OpenBName" runat="server" CssClass="input4 validate[required,maxSize[50]]" MaxLength="50" Style="width: 240px;"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table width="780" border="0" cellspacing="0" cellpadding="0" class="for">
                <tr>
                    <th>
                        <asp:ImageButton ID="btn_Save" runat="server" ImageUrl="~/Images/buttons/baocun.gif"
                            OnClick="btnAdd_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp; <a id="FanHui" href="BankList.aspx">
                        <asp:Image ID="btn_FH" runat="server" ImageUrl="~/Images/buttons/fanHui.gif" /></a>
                    </th>
                </tr>
            </table>
        </center>
    </form>
</body>
</html>
