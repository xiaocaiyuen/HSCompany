<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SoftwareAdd.aspx.cs" Inherits="Shu.Manage.Sys.SoftwareAdd" %>
<%@ Register assembly="CKEditor.NET" namespace="CKEditor.NET" tagprefix="CKEditor" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/Scripts/jquery-1.8.0.min.js" type="text/javascript"></script>
    <link href="/Styles/table.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/List.css" rel="stylesheet" type="text/css" />

    <%--录入验证 --%>
    <link href="/Styles/validationEngine.jquery.css" rel="stylesheet" type="text/css" charset="utf-8" />
    <script src="../../Scripts/Validate/2.6.2Ciaoca/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="../../Scripts/Validate/2.6.2Ciaoca/jquery.validationEngine.min.js" type="text/javascript"></script>
    <script src="../../Scripts/Validate/2.6.2Ciaoca/jquery.validationEngine-zh_CN.js" type="text/javascript" charset="utf-8"></script>
    <%--附件--%>
    <link href="/JQuploadify/uploadify.css" rel="stylesheet" />
    <script charset="utf-8" src="/JQuploadify/jquery.uploadify.js" type="text/javascript"></script>
    <script src="/Scripts/UI/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="/Scripts/UI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/UI/themes/bootstrap/easyui.css" rel="stylesheet" type="text/css" />
    <script charset="utf-8" src="/Scripts/UI/easyui-lang-zh_CN.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server"><asp:HiddenField ID="hid_Fnr" runat="server" />
        <asp:HiddenField ID="hid_YC" runat="server" />
        <asp:HiddenField ID="hid_Id" runat="server" />
        <asp:HiddenField ID="hid_LX" runat="server" />
        <asp:HiddenField ID="hid_DepId" runat="server" />
        <center>
            <div id="titleScore" class="easyui-panel" title="系统必备工具管理" style="width: auto; min-width: 800px; height: auto; text-align: center">
            <table width="780" class="tab" border="0" cellpadding="1" cellspacing="1">
                <tr>
                    <th width="120px">工具软件名称<font color="red">*</font>
                    </th>
                    <td width="330px" colspan="3">
                        <asp:TextBox ID="Software_Title" runat="server" CssClass="input4 validate[required,length[1,50]]" MaxLength="50" Style="width: 640px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th width="120px">软件序号<font color="red">*</font>
                    </th>
                    <td width="330px" colspan="3">
                        <asp:TextBox ID="Software_No" runat="server" CssClass="input4 validate[required,length[1,2]]" MaxLength="50" Style="width: 140px;" Text="1" onkeypress="if (!((event.keyCode > 47 && event.keyCode < 58)||(event.keyCode > 95 && event.keyCode < 106))) event.returnValue = false;"></asp:TextBox>(软件提供下载时的排序依据)
                    </td>
                </tr>
                <tr>
                    <th width="120px">简介
                    </th>
                    <td colspan="3" width="660px">
                        <CKEditor:CKEditorControl ID="Software_Context" BasePath="~/ckeditor" runat="server">
                        </CKEditor:CKEditorControl>
                    </td>
                </tr>

                <tr>
                    <th width="120px">软件上传
                    </th>
                    <td width="660px" colspan="3" style="line-height: 20px;">
                        <UC:File ID="File" runat="server" />
                    </td>
                </tr>
            </table>
            </div>
            <table width="780" border="0" cellspacing="0" cellpadding="0" class="for">
                <tr>
                    <th>
                        <asp:ImageButton ID="btn_Save" runat="server" ImageUrl="~/Images/buttons/baocun.gif"
                            OnClick="btn_Save_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp; <a id="FanHui" href="SoftwareIndex.aspx">
                        <asp:Image ID="btn_FH" runat="server" ImageUrl="~/Images/buttons/fanHui.gif" /></a>
                    </th>
                </tr>
            </table>
        </center>
    </form>
</body>
</html>
