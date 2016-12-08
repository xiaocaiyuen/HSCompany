<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoticeAdd.aspx.cs" Inherits="Shu.Manage.Sys.NoticeAdd" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" src="/Scripts/jquery-2.0.0.min.js"></script>
    <link href="/Styles/table.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/List.css" rel="stylesheet" type="text/css" />

    <%--录入验证 --%>
    <%--<link href="/Styles/validationEngine.jquery.css" rel="stylesheet" type="text/css" charset="utf-8" />
    <script src="../../Scripts/Validate/2.6.2Ciaoca/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="../../Scripts/Validate/2.6.2Ciaoca/jquery.validationEngine.min.js" type="text/javascript"></script>
    <script src="../../Scripts/Validate/2.6.2Ciaoca/jquery.validationEngine-zh_CN.js" type="text/javascript" charset="utf-8"></script>--%>
    <%--附件--%>
    <link href="/Scripts/uploadify/uploadify.css" rel="stylesheet" />
    <link href="/Content/themes/bootstrap/easyui.css" rel="stylesheet" />
    <link href="/Content/themes/icon.css" rel="stylesheet" />
    <script type="text/javascript" src="/Scripts/jquery.easyui-1.4.5.min.js" charset="utf-8"></script>
    <script type="text/javascript" src="/Scripts/locale/easyui-lang-zh_CN.js" charset="utf-8"></script>

    <script src="/Scripts/uploadify/jquery.uploadify.min.js" charset="utf-8"></script>
    <script src="/Scripts/ckeditor/ckeditor.js"></script>
    <script type="text/javascript">
        function RemoveFoucs() {
            $("#btn_TJ").focus();
        }
        function ShowSelectt() {

            $('#uctextselect1').dialog('open');
            $('#ucselect1').show();
            var ids = $("#hid_DepId").val();
            var url = 'DepSelect.aspx?hid_DepId=' + ids;
            $("#ucview1").attr("src", url);
        }

        function CloseView() {
            $('#ucselect1').hide();
            $('#uctextselect1').dialog('close');
        }

        function ReloadPage(ids, names) {
            $("#UCDepartmentTreeText1").val(names);
            $("#hid_DepId").val(ids);
            //alert(ids);
        }
    </script>
    <style type="text/css">
        #lstAttachment {
            margin-top: 5px;
            margin-bottom: 5px;
        }

        img {
            border: 0px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            //$("#form1").validationEngine({ promptPosition: "centerRight", validationEventTriggers: "keyup blur" });
        })
        function fanhui() {
            window.location.href = "/Sys/NoticeList.aspx";
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hid_Fnr" runat="server" />
        <asp:HiddenField ID="hid_YC" runat="server" />
        <asp:HiddenField ID="hid_Id" runat="server" />
        <asp:HiddenField ID="hid_LX" runat="server" />
        <asp:HiddenField ID="hid_DepId" runat="server" />
        <center>
            <div id="titleScore" class="easyui-panel" title="通知公告管理" style="width: auto; min-width: 800px; height: auto; text-align: center">
            <table width="780" class="tab" border="0" cellpadding="1" cellspacing="1">
                <tr>
                    <th width="120px">公告类型<font color="red">*</font>
                    </th>
                    <td width="330px" colspan="3">
                        <asp:DropDownList ID="ddlNotice_Type" runat="server" CssClass="input4" Style="width: 643px;"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th width="120px">标题<font color="red">*</font>
                    </th>
                    <td width="330px" colspan="3">
                        <asp:TextBox ID="Notice_Title" runat="server" CssClass="input4 validate[required,length[1,50]]" MaxLength="50" Style="width: 640px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th width="120px">发布范围<font color="red">*</font>
                    </th>
                    <td width="330px" colspan="3">
                        <asp:TextBox ID="UCDepartmentTreeText1" runat="server"
                            Style="width: 620px; border: 1px solid #88BBE2; height: 100px;" TextMode="MultiLine" ReadOnly="true" CssClass="input4 validate[required]"></asp:TextBox>
                        <span id="ucuserSelectSpan">
                            <img id="uctxtImg" alt='选择' src="/Images/uc/search.gif" style="width: 19; height: 19px; cursor: pointer; padding-top: 6px;"
                                onclick="ShowSelectt()" />
                    &nbsp;</td>
                </tr>
                <tr>
                    <th width="120px">正文内容
                    </th>
                    <td colspan="3" width="660px">
                        <CKEditor:CKEditorControl ID="Notice_Context" BasePath="~/ckeditor" runat="server">
                        </CKEditor:CKEditorControl>
                    </td>
                </tr>

                <tr>
                    <th width="120px">附件信息
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
                    &nbsp;&nbsp;&nbsp;&nbsp; <a id="FanHui" href="NoticeList.aspx">
                        <asp:Image ID="btn_FH" runat="server" ImageUrl="~/Images/buttons/fanHui.gif" /></a>
                    </th>
                </tr>
            </table>
        </center>
        <!--弹出框-->
        <div id="ucselect1" style="display: none;">
            <!--弹出框-->
            <div id="uctextselect1" class="easyui-dialog" title="部门选择" style="width: 380px; height: 400px;"
                toolbar="#dlg-toolbar" resizable="true" closed="true">
                <iframe id="ucview1" scrolling="yes" width="100%" frameborder="0" height="350" style="overflow-x: hidden"></iframe>
            </div>
        </div>
    </form>
</body>
</html>
