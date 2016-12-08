<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyEdit.aspx.cs" Inherits="YDT.Web.Manage.Sys.CompanyEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="/Scripts/jquery-1.8.0.min.js" type="text/javascript"></script>
    <link href="/Styles/table.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/List.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link href="/Scripts/UI/themes/bootstrap/easyui.css" rel="stylesheet" />
    <link href="/Scripts/UI/themes/icon.css" rel="stylesheet" />
    <script src="/Scripts/UI/jquery.easyui.min.js" type="text/javascript"></script>
    <%--录入验证 --%>
    <link href="/Styles/validationEngine.jquery.css" rel="stylesheet" type="text/css" charset="utf-8" />
    <script src="../../Scripts/Validate/2.6.2Ciaoca/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="../../Scripts/Validate/2.6.2Ciaoca/jquery.validationEngine.min.js" type="text/javascript"></script>
    <script src="../../Scripts/Validate/2.6.2Ciaoca/jquery.validationEngine-zh_CN.js" type="text/javascript" charset="utf-8"></script>
    <style type="text/css">
        .table th {
        width: 140px;
        }
        .table td {
        width: 300px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("#form1").validationEngine({ promptPosition: "centerRight", validationEventTriggers: "keyup blur" });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <center>
            <div id="p1" class="easyui-panel" title=" 公司信息维护" data-options="collapsible:true">
            <table width="880" class="tab" border="0" cellpadding="1" cellspacing="1">
                 <tr>
                    <th>公司名称（中文）<font color="red">*</font>
                    </th>
                    <td>
                        <asp:TextBox ID="txtCompanyInfo_CNName" runat="server" CssClass="input4 validate[required,maxSize[100]]" MaxLength="100" Style="width: 240px;"></asp:TextBox>
                    </td>
                    <th>公司名称（英文）
                    </th>
                    <td>
                        <asp:TextBox ID="txtCompanyInfo_ENName" runat="server" CssClass="input4 validate[maxSize[100],custom[onlyLetterSp]]" MaxLength="100" Style="width: 240px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>公司简称
                    </th>
                    <td>
                        <asp:TextBox ID="txtCompanyInfo_Abbr" runat="server" CssClass="input4 validate[maxSize[50]]" MaxLength="50" Style="width: 240px;"></asp:TextBox>
                    </td>
                    <th>职工人数
                    </th>
                    <td>
                        <asp:TextBox ID="txtCompanyInfo_Number" runat="server" CssClass="input4 validate[custom[onlyNumberSp]]" MaxLength="8" Style="width: 240px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>公司类型
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlCompanyInfo_Type" runat="server" CssClass="input4" Style="width: 240px;"></asp:DropDownList>
                    </td>
                    <th>营业执照注册码<font color="red">*</font>
                    </th>
                    <td>
                        <asp:TextBox ID="txtCompanyInfo_LicenseNo" runat="server" CssClass="input4 validate[required,maxSize[50]]" MaxLength="50" Style="width: 240px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>收款单位代收代码
                    </th>
                    <td>
                        <asp:TextBox ID="txtCompanyInfo_ReplaceCollectionCode" runat="server" CssClass="input4 validate[maxSize[50]]" MaxLength="50" Style="width: 240px;"></asp:TextBox>
                    </td>
                    <th>银行信息<font color="red">*</font>
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlCompanyInfo_Bank" runat="server" CssClass="input4 validate[required]" Style="width: 240px;">

                        </asp:DropDownList>
                        <%--<asp:TextBox ID="TextBox1" runat="server" CssClass="input4 validate[maxSize[50]]" MaxLength="50" Style="width: 240px;"></asp:TextBox>--%>
                    </td>
                </tr>
                <tr>
                    <th>营业期限<font color="red">*</font>
                    </th>
                    <td>
                        <asp:TextBox ID="txtCompanyInfo_Deadline" CssClass="input4 validate[required]" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" runat="server" Style="width: 240px;" ></asp:TextBox>
                    </td>
                    <th>税务登记号<font color="red">*</font>
                    </th>
                    <td>
                        <asp:TextBox ID="txtCompanyInfo_TaxRegistrationNo" runat="server" CssClass="input4 validate[required,maxSize[50]]" MaxLength="50" Style="width: 240px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>成立日期<font color="red">*</font>
                    </th>
                    <td>
                        <asp:TextBox ID="txtCompanyInfo_FoundedDate" CssClass="input4 validate[required]" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" runat="server" Style="width: 240px;" ></asp:TextBox>
                    </td>
                    <th>注册资本（万元）<font color="red">*</font>
                    </th>
                    <td>
                        <asp:TextBox ID="txtCompanyInfo_Capital" runat="server" CssClass="input4 validate[required,custom[onlyNumber],maxSize[15]]" MaxLength="15" Style="width: 240px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>经营范围
                    </th>
                    <td>
                        <asp:TextBox ID="txtCompanyInfo_Scope" runat="server" CssClass="input4 validate[maxSize[200]]" MaxLength="200" Style="width: 240px;"></asp:TextBox>
                    </td>
                    <th>公司电话<font color="red">*</font>
                    </th>
                    <td>
                        <asp:TextBox ID="txtCompanyInfo_Phone" runat="server" CssClass="input4 validate[required,custom[phone]]" Style="width: 240px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>公司传真
                    </th>
                    <td>
                        <asp:TextBox ID="txtCompanyInfo_Fax" runat="server" CssClass="input4 validate[maxSize[10]]" MaxLength="10" Style="width: 240px;"></asp:TextBox>
                    </td>
                    <th>邮政编码
                    </th>
                    <td>
                        <asp:TextBox ID="txtCompanyInfo_PostalCode" runat="server" CssClass="input4 validate[custom[chinaZip]]" MaxLength="8" Style="width: 240px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>组织机构代码<font color="red">*</font>
                    </th>
                    <td>
                        <asp:TextBox ID="txtCompanyInfo_OrgaCode" runat="server" CssClass="input4 validate[required,maxSize[50]]" MaxLength="50" Style="width: 240px;"></asp:TextBox>
                    </td>
                    <th>企业邮箱<font color="red">*</font>
                    </th>
                    <td>
                        <asp:TextBox ID="txtCompanyInfo_EnterpriseMailbox" runat="server" CssClass="input4 validate[required,custom[email]]" MaxLength="50" Style="width: 240px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>公司地址
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtCompanyInfo_Address" runat="server" CssClass="input4 validate[maxSize[200]]" MaxLength="200" Style="width: 680px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>注册地址
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtCompanyInfo_RegisteredAddress" runat="server" CssClass="input4 validate[maxSize[200]]" MaxLength="200" Style="width: 680px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>备注
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtCompanyInfo_Remarks" runat="server" CssClass="input4" Style="width: 680px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>法人代表<font color="red">*</font>
                    </th>
                    <td>
                        <asp:TextBox ID="txtCompanyInfo_LegalRepresentative" runat="server" CssClass="input4 validate[required,maxSize[10]]" MaxLength="10" Style="width: 240px;"></asp:TextBox>
                    </td>
                    <th>法人身份证
                    </th>
                    <td>
                        <asp:TextBox ID="txtCompanyInfo_LegalCard" runat="server" CssClass="input4 validate[maxSize[18],custom[chinaId]]" MaxLength="18" Style="width: 240px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>法人办公电话
                    </th>
                    <td>
                        <asp:TextBox ID="txtCompanyInfo_LegalOfficePhone" runat="server" CssClass="input4 validate[custom[phone]]" MaxLength="15" Style="width: 240px;"></asp:TextBox>
                    </td>
                    <th>法人邮政编码
                    </th>
                    <td>
                        <asp:TextBox ID="txtCompanyInfo_LegalPostalCode" runat="server" CssClass="input4 validate[custom[chinaZip]]" MaxLength="50" Style="width: 240px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>法人手机号码
                    </th>
                    <td>
                        <asp:TextBox ID="txtCompanyInfo_LegalMobilePhone" runat="server" CssClass="input4 validate[custom[mobilephone]]" MaxLength="15" Style="width: 240px;"></asp:TextBox>
                    </td>
                    <th>法人邮箱
                    </th>
                    <td>
                        <asp:TextBox ID="txtCompanyInfo_LegalMail" runat="server" CssClass="input4 validate[custom[email]]" MaxLength="50" Style="width: 240px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>法人常驻地址
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtCompanyInfo_LegalPermanentAddress" runat="server" CssClass="input4 validate[maxSize[200]]" MaxLength="200" Style="width: 680px;"></asp:TextBox>
                    </td>
                </tr>
            </table>
            </div>
            <table width="880" border="0" cellspacing="0" cellpadding="0" class="for">
                <tr>
                    <th>
                        <asp:ImageButton ID="btn_Save" runat="server" ImageUrl="~/Images/buttons/baocun.gif"
                            OnClick="btnAdd_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp; <a id="FanHui" href="CompanyList.aspx">
                        <asp:Image ID="btn_FH" runat="server" ImageUrl="~/Images/buttons/fanHui.gif" /></a>
                    </th>
                </tr>
            </table>
        </center>
    </form>
</body>
</html>
