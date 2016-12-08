<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PostInfo.aspx.cs" Inherits="Shu.Manage.Sys.PostInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Scripts/UI/themes/bootstrap/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/List.css" rel="stylesheet" type="text/css" />
     <script src="/Scripts/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="/Scripts/UI/jquery.easyui.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function CheckData() {
            var reg = /^[0-9]+$/;
            if ($("#UCDepartmentTreeText1_txt_depName").val() == "") {
                alert("部门不能为空"); return false;
            }
            if ($("#txtGwName").val() == "") {
                alert("岗位不能为空"); return false;
            }
            if ($("#txtpx").val() == "") {
                alert("岗位排序不能为空"); return false;
            }
            else if (!reg.test($("#txtpx").val())) {
                alert("岗位排序只能输入整数"); return false;
            }
            return true;
        }
        function coed(type) {

            window.parent.closed(type);
           
         };
    </script>
</head>
<body>
    <form id="form1" runat="server">
            <div id="divCont" runat="server">
                <table width="100%" style="height: 160px;">
                    <tr>
                        <td style="text-align: right; width: 100px;">
                            所在部门：
                        </td>
                        <td>
                            <%--<uc1:UCDepartmentTreeText ID="UCDepartmentTreeText1" runat="server" />--%>
                            <asp:TextBox ID="txt_depName"  runat="server" CssClass="validate[length[1,50]]" ReadOnly="true"
                                Style="width: 180px; line-height: 20px;"></asp:TextBox>
                           <%-- <span id="ucdepSelectSpan">
                                <img id="uctxtImg" alt='选择' src="/Images/uc/search.gif" style="width: 20; height: 20px;
                                    cursor: pointer; padding-top: 6px; line-height: 20px;" onclick="ShowSelectDep()" />
                            </span>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            岗位：
                        </td>
                        <td>
                            <asp:TextBox ID="txtGwName" CssClass="validate[required]" runat="server" MaxLength="50"  Width="180px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            排列序号：
                        </td>
                        <td>
                            <asp:TextBox ID="txtpx" CssClass="validate[required,custom[intNum]]" MaxLength="5" runat="server"
                                Width="180px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:ImageButton ImageUrl="~/Images/buttons/baocun.gif" runat="server" ID="btnSave"
                                OnClientClick="return CheckData()" OnClick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
            </div>
    </form>
</body>
</html>
