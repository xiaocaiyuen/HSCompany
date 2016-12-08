<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfoModify_T.aspx.cs"
    Inherits="YDT.Web.Manage.Sys.UserInfoModify_T" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="/Scripts/jquery-1.8.0.min.js" type="text/javascript"></script>
    <link href="/Styles/table.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <%--附件--%>
    <script src="/Scripts/UI/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="/Scripts/UI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/UI/themes/bootstrap/easyui.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/DatePicker/WdatePicker.js" type="text/javascript"></script>
    <%--录入验证 --%>
    <link href="/Styles/validationEngine.jquery.css" rel="stylesheet" type="text/css"
        charset="utf-8" />
    <script src="/Scripts/Validate/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="/Scripts/Validate/jquery.validationEngine-cn.js" type="text/javascript"
        charset="gb2312"></script>
    <script src="/Scripts/Validate/FormVaildate.js" type="text/javascript"></script>
    <%--身份证验证 --%>
    <script src="/Scripts/IdentityCard.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('#FanHui').click(function () {
                var depCode = $("#hid_Dep").val();
                $("#FanHui").attr('href', "UserInfoList_T.aspx?depCode=" + depCode + "");
            })
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hid_Dep" runat="server" />
    <asp:HiddenField ID="txt_Yhxx_DTcsrq" runat="server" />
    <asp:HiddenField ID="txt_Yhxx_Iage" runat="server" />
    <table width="96%" border="0" cellspacing="0" cellpadding="0" class="headerCss">
        <tr>
            <th colspan="4">
                用户信息维护
            </th>
        </tr>
    </table>
    <table width="96%" class="tab" border="0" cellpadding="10" cellspacing="1">
        <tr>
            <th width="20%">
                用户帐号
            </th>
            <td width="30%">
                <asp:Label ID="t_UserInfo_LoginUserName" runat="server"></asp:Label>
            </td>
            <th width="20%">
                用户姓名
            </th>
            <td width="30%">
                <asp:Label ID="t_UserInfo_FullName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th width="20%">
                用户密码<font color="red">*</font>
            </th>
            <td width="30%">
                <asp:TextBox ID="t_UserInfo_LoginUserPwd" runat="server" TextMode="Password" CssClass="input4 validate[required,length[6,16]]"></asp:TextBox>
            </td>
            <th width="20%">
                手机号码<font color="red">*</font>
            </th>
            <td width="30%">
                <asp:TextBox ID="t_UserInfo_PhoneNumber" runat="server" CssClass="input4 validate[required,custom[mobilephone]]"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th width="20%">
                民族
            </th>
            <td width="30%">
                <asp:DropDownList ID="t_UserInfo_Nation" runat="server" CssClass="input4">
                </asp:DropDownList>
            </td>
            <th width="20%">
                性别
            </th>
            <td width="30%">
                <asp:DropDownList ID="t_UserInfo_Sex" runat="server" CssClass="input4 ">
                    <asp:ListItem Text="-请选择-" Value=""></asp:ListItem>
                    <asp:ListItem Text="男" Value="男"></asp:ListItem>
                    <asp:ListItem Text="女" Value="女"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th width="20%">
                身份证
            </th>
            <td width="30%">
                <asp:TextBox ID="t_UserInfo_IdentityCred" onblur="isIdCardNo(this.value);" runat="server"
                    CssClass="input4 validate[length[0,18]]"></asp:TextBox>
            </td>
            <th width="20%">
                年龄
            </th>
            <td width="30%">
                <asp:Label ID="t_UserInfo_Age" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th width="20%">
                出生日期
            </th>
            <td width="30%">
                <asp:Label ID="t_UserInfo_DateBirth" runat="server"></asp:Label>
            </td>
            <th width="20%">
                用户状态
            </th>
            <td width="30%">
                <asp:Label ID="t_UserInfo_StatusName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th width="20%">
                所在部门
            </th>
            <td colspan="3">
                <asp:Label ID="t_Department_Name" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th width="20%">
                岗位
            </th>
            <td colspan="3">
                <asp:Label ID="t_UserInfo_PostName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th width="20%">
                籍贯
            </th>
            <td colspan="3">
                <asp:TextBox ID="t_UserInfo_Hometown" runat="server" CssClass="input4 validate[length[0,100]]"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th width="20%">
                家庭住址
            </th>
            <td colspan="3">
                <asp:TextBox ID="t_UserInfo_Address" runat="server" CssClass="input4 validate[length[0,100]]"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th width="20%">
                政治面貌
            </th>
            <td width="30%">
                <asp:DropDownList ID="t_UserInfo_PoliticalLandscape" runat="server" CssClass="input4 ">
                </asp:DropDownList>
            </td>
            <th width="20%">
                学历<font color="red"></font>
            </th>
            <td width="30%">
                <asp:DropDownList ID="t_UserInfo_EducationalLevel" runat="server" CssClass="input4 ">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th width="20%">
                毕业院校
            </th>
            <td width="30%">
                <asp:TextBox ID="t_UserInfo_Schools" runat="server" CssClass="input4 validate[length[0,40]]"></asp:TextBox>
            </td>
            <th width="20%">
                学习专业
            </th>
            <td width="30%">
                <asp:TextBox ID="t_UserInfo_Specialty" runat="server" CssClass="input4 validate[length[0,50]]"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th width="20%">
                参加工作时间
            </th>
            <td width="30%">
                <asp:TextBox ID="t_UserInfo_StartWorkDate" runat="server" onclick="WdatePicker({dateFmt:'yyyy年MM月dd日'})"
                    CssClass="input4"></asp:TextBox>
            </td>
            <th width="20%">
                入党时间
            </th>
            <td>
                <asp:TextBox ID="t_UserInfo_JoinPartyDate" runat="server" onclick="WdatePicker({dateFmt:'yyyy年MM月dd日'})"
                    CssClass="input4"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th width="20%">
                入伍时间
            </th>
            <td width="30%">
                <asp:TextBox ID="t_UserInfo_EnlistTime" runat="server" onclick="WdatePicker({dateFmt:'yyyy年MM月dd日'})"
                    CssClass="input4"></asp:TextBox>
            </td>
            <th width="20%">
                警衔
            </th>
            <td>
                <asp:TextBox ID="t_UserInfo_Badge" runat="server" CssClass="input4 validate[length[0,50]]"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th width="20%">
                入伍地
            </th>
            <td width="30%" colspan=3>
                <asp:TextBox ID="t_UserInfo_EnlistPlace" runat="server" 
                    CssClass="input4 validate[length[0,50]]"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th width="20%">
                现任职务
            </th>
            <td width="30%">
                <asp:TextBox ID="t_UserInfo_Position" runat="server" CssClass="input4 validate[length[0,100]]"></asp:TextBox>
            </td>
            <th width="20%">
                职务级别
            </th>
            <td>
                <asp:DropDownList ID="t_UserInfo_PositionLevel" runat="server" CssClass="input4">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th width="20%">
                现任职务时间
            </th>
            <td width="30%">
                <asp:TextBox ID="t_UserInfo_NowHoldPostTime" runat="server" onclick="WdatePicker({dateFmt:'yyyy年MM月dd日'})"
                    CssClass="input4"></asp:TextBox>
            </td>
            <th width="20%">
                职称
            </th>
            <td width="30%">
                <asp:DropDownList ID="t_UserInfo_OccupTitle" runat="server" CssClass="input4 ">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table width="96%" border="0" cellspacing="0" cellpadding="0" class="for">
        <tr>
            <th>
                <%-- <asp:ImageButton ID="btn_YuCun" runat="server" ImageUrl="~/Images/buttons/yucun.gif"
                        OnClick="btn_YuCun_Click" /> &nbsp;&nbsp;&nbsp;&nbsp;--%>
                <asp:ImageButton ID="btntijiao" runat="server" ImageUrl="~/Images/buttons/baocun.gif"
                    OnClick="btntijiao_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp; <a id="FanHui" href="#" onclick=" fanhui()">
                    <asp:Image ID="btn_FH" runat="server" ImageUrl="~/Images/buttons/fanHui.gif" /></a>
            </th>
        </tr>
    </table>
    <!--弹出框-->
    </form>
</body>
</html>
