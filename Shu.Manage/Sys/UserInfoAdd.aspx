<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfoAdd.aspx.cs" Inherits="YDT.Web.Manage.Sys.UserInfoAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="/Styles/table.css" rel="stylesheet" type="text/css" />
<%--     <link href="/Styles/default.css" rel="stylesheet" type="text/css" />--%>
    <link href="/Scripts/UI/themes/bootstrap/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/UI/themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="/Scripts/UI/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="../../Styles/validationEngine.jquery.css" rel="stylesheet" type="text/css"
        charset="utf-8" />
    <script src="../../Scripts/Validate/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="../../Scripts/Validate/jquery.validationEngine-cn.js" type="text/javascript"
        charset="gb2312"></script>
    <script src="../../Scripts/Validate/FormVaildate.js" type="text/javascript"></script>
    <script src="../../Scripts/common.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('#FanHui').click(function () {
                var depCode = $("#hid_Dep").val();
                $("#FanHui").attr('href', "UserInfoList.aspx?depCode=" + depCode + "");
            })

           
        });


     //设置显示的内容
     function SetCount(str) {
         $("#lblFgbm").html(str);
     }

     //打开窗体
     function OpenForm(str) {
         ShowOrCloseWin('d2', 'open');
         $('#div_1').show();
         $("#view").attr("src", 'SelectUserEx.aspx?id=' + $("#hid_AccountId").val()+'&type='+str);
     }


     //关闭窗体
     function CloseWin() {
         ShowOrCloseWin('d2', 'close');
     }

     function back() {
         var vales = $("#hid_Dep").val();
         window.location.href = 'UserInfoList.aspx?depCode=' + vales;
     }

     function isRoleChecked() {
         //            var ckb = $(":checkbox:checked");
         var isGw = false;
         var isJs = false;
         $("#tdGw").find("input[type='checkbox']").each(function () {
             if ($(this).attr("checked") == 'checked') {
                 isGw = true;
             }
         })
         $("#tdJs").find("input[type='checkbox']").each(function () {
             if ($(this).attr("checked") == 'checked') {
                 isJs = true;
             }
         })

         if (!isGw) {
             alert('请至少选择一个岗位给用户！');
             return false;
         }
         if (!isJs) {
             alert('请至少选择一个角色给用户！');
             return false;
         }
     }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hid_Dep" runat="server" />
    <asp:HiddenField ID="hid_JJ" runat="server" />
    <asp:HiddenField ID="hid_Phone" runat="server" />
    <asp:HiddenField ID="hid_LoginName" runat="server" />
    <asp:HiddenField ID="hid_AccountId" runat="server" />
    <center>
        <table width="780" border="0" cellspacing="0" cellpadding="0" class="headerCss">
            <tr>
                <th colspan="4">
                    
                        用户管理
                </th>
            </tr>
        </table>
        <table class="tab" cellpadding="10" cellspacing="1" width="780">
            <tr>
                <th>
                    用户帐号
                </th>
                <td>
                    <asp:TextBox ID="txtLoginName" runat="server" CssClass="validate[length[1,20]]"></asp:TextBox>
                    <font color="red">*</font>
                </td>
                <th>
                    用户密码
                </th>
                <td>
                    <asp:TextBox ID="txtLoginPwd" runat="server" CssClass="validate[length[6,20]]" MaxLength="20" TextMode="Password"></asp:TextBox>
                    <font color="red">*</font>
                </td>
                <th>
                    用户姓名
                </th>
                <td>
                    <asp:TextBox ID="txtManName" runat="server" CssClass="validate[length[1,16]]"></asp:TextBox>
                    <font color="red">*</font>
                </td>
            </tr>
            <tr>
                <th>
                    所在部门
                </th>
                <td>
                    <asp:DropDownList runat="server" onchange="ChareDep()" ID="ddlDepart"  disabled="true"
                        Enabled="true" OnSelectedIndexChanged="ddlDepart_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <th>
                    排序
                </th>
                <td>
                    <asp:TextBox ID="txtPx" CssClass="validate[required,custom[intNum]]" runat="server"></asp:TextBox>
                </td>
                <th>
                    用户类型:
                </th>
                <td>
                    <asp:DropDownList ID="ddlUserType" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlUserType_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="trIsFg" runat="server">
                <th>
                    用户状态
                </th>
                 <td>
                    <asp:DropDownList ID="ddlStatus" runat="server">
                    </asp:DropDownList>
                </td>
              
                <th>
                    <asp:Label ID="lbl_FGtxt" runat="server"  Visible="false"></asp:Label>  
                </th>
                 <td colspan="3" align="left">
                     
                    <asp:Label ID="lblFgbm" runat="server" Text="" Visible="false"></asp:Label>
                     <asp:Literal ID="lt_Open" runat="server" Text="" Visible="false"></asp:Literal>
                    
                </td>
            </tr>
            <tr>
                <th>
                    所在岗位
                </th>
                <td colspan="5" id="tdGw">
                    <asp:CheckBoxList ID="chbGwList" runat="server" RepeatDirection="Horizontal" RepeatColumns="5">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <th>
                    角色权限
                </th>
                <td colspan="5" id="tdJs">
                    <asp:CheckBoxList ID="ckbxlist" runat="server" RepeatDirection="Horizontal" RepeatColumns="5">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <th colspan="6">
                    <asp:ImageButton runat="server" OnClientClick="return isRoleChecked();" ID="btnAdd"
                        OnClick="btnAdd_Click" ImageUrl="~/Images/buttons/baocun.gif" />
                    &nbsp;<a href="#" id="FanHui"><asp:Image ID="btn_FH" runat="server" ImageUrl="~/Images/buttons/fanHui.gif"
                        Height="24px" Width="50px" /></a>
                </th>
            </tr>
        </table>
        <!--弹出框-->
        <div id="d2" class="easyui-dialog" title="部门选择" style="width: 280px; height: 400px;"
            toolbar="#dlg-toolbar" resizable="true" closed="true">
            <iframe id="view" scrolling="yes" width="100%" frameborder="0" height="350"></iframe>
        </div>
    </center>
    </form>
</body>
</html>
