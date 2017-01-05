<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfoModify.aspx.cs"
    Inherits="Shu.Manage.Sys.UserInfoModify" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Styles/table.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Scripts/jquery-2.0.0.min.js"></script>
    <link href="/Content/themes/bootstrap/easyui.css" rel="stylesheet" />
    <link href="/Content/themes/icon.css" rel="stylesheet" />
    <script type="text/javascript" src="/Scripts/jquery.easyui-1.4.5.min.js" charset="utf-8"></script>
    <script type="text/javascript" src="/Scripts/locale/easyui-lang-zh_CN.js" charset="utf-8"></script>
    <script src="/Scripts/DatePicker/WdatePicker.js" type="text/javascript"></script>
    <%--录入验证 --%>
    <link href="/Content/validationEngine.jquery.css" rel="stylesheet" type="text/css"
        charset="utf-8" />
    <script src="/Scripts/jquery.validationEngine.js"></script>
    <script src="/Scripts/jquery.validationEngine-cn.js"  charset="gb2312"></script>
    <script src="/Scripts/Validate/FormVaildate.js" type="text/javascript"></script>
    <%--身份证验证 --%>
    <script src="/Scripts/IdentityCard.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="txt_Yhxx_DTcsrq" runat="server" />
    <asp:HiddenField ID="txt_Yhxx_Iage" runat="server" />
    <div id="titleScore" class="easyui-panel" title="个人信息维护" style="width: auto; min-width: 800px; height: auto; text-align: center">
    <table width="780" class="tab" border="0" cellpadding="1" cellspacing="1">
        <tr>
            <th width="100px">
                用户帐号
            </th>
            <td width="220px">
                <asp:Label ID="t_UserInfo_LoginUserName" runat="server"></asp:Label>
            </td>
            <th width="80px">
                用户姓名
            </th>
            <td width="180px">
                <%--<asp:Label ID="t_UserInfo_FullName" runat="server"></asp:Label>--%>
                <asp:TextBox ID="t_UserInfo_FullName" runat="server" CssClass="input4 validate[required,length[1,50]]"></asp:TextBox>
            </td>
            <th width="80px">
                用户密码<font color="red">*</font>
            </th>
            <td width="120px">
                <asp:TextBox ID="t_UserInfo_LoginUserPwd" runat="server" TextMode="Password" CssClass="input4 validate[required,length[6,16]]"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th width="100px">
                民族
            </th>
            <td width="220px">
                <asp:DropDownList ID="t_UserInfo_Nation" runat="server" CssClass="input4">
                </asp:DropDownList>
            </td>
            <th width="80px">
                性别
            </th>
            <td width="180px">
                <asp:DropDownList ID="t_UserInfo_Sex" runat="server" CssClass="input4">
                    <asp:ListItem Text="-请选择-" Value=""></asp:ListItem>
                    <asp:ListItem Text="男" Value="男"></asp:ListItem>
                    <asp:ListItem Text="女" Value="女"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <th width="80px">
                手机号码<font color="red">*</font>
            </th>
            <td width="120px">
                <asp:TextBox ID="t_UserInfo_PhoneNumber" runat="server" CssClass="input4 validate[required,custom[mobilephone]]"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th width="100px">
                身份证
            </th>
            <td width="220px">
                <asp:TextBox ID="t_UserInfo_IdentityCred" onblur="isIdCardNo(this.value);" runat="server"
                    CssClass="input4 validate[length[0,18]]"></asp:TextBox>
            </td>
            <th width="80px">
                年龄
            </th>
            <td width="180px">
                <asp:Label ID="t_UserInfo_Age" runat="server"></asp:Label>
            </td>
            <th width="80px">
                出生日期
            </th>
            <td width="120px">
                <asp:Label ID="t_UserInfo_DateBirth" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th width="100px">
                所在部门
            </th>
            <td width="220px">
                <asp:Label ID="t_Department_Name" runat="server"></asp:Label>
            </td>
            <th width="80px">
                岗位
            </th>
            <td width="180px">
                <asp:Label ID="t_UserInfo_PostName" runat="server"></asp:Label>
            </td>
            <th width="80px">
                用户状态
            </th>
            <td width="120px">
                <asp:Label ID="t_UserInfo_StatusName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th width="100px">
                籍贯
            </th>
            <td colspan="3">
                <asp:TextBox ID="t_UserInfo_Hometown" runat="server" CssClass="input4 validate[length[0,100]]"></asp:TextBox>
            </td>
            <th width="80px">
                政治面貌
            </th>
            <td width="120px">
                <asp:DropDownList ID="t_UserInfo_PoliticalLandscape" runat="server" CssClass="input4">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th width="100px">
                家庭住址
            </th>
            <td colspan="3">
                <asp:TextBox ID="t_UserInfo_Address" runat="server" CssClass="input4 validate[length[0,100]]"></asp:TextBox>
            </td>
            <th width="80px">
                学历
            </th>
            <td width="120px">
                <asp:DropDownList ID="t_UserInfo_EducationalLevel" runat="server" CssClass="input4">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th width="100px">
                毕业院校
            </th>
            <td width="220px">
                <asp:TextBox ID="t_UserInfo_Schools" runat="server" CssClass="input4 validate[length[0,40]]"></asp:TextBox>
            </td>
            <th width="80px">
                学习专业
            </th>
            <td width="180px">
                <asp:TextBox ID="t_UserInfo_Specialty" runat="server" CssClass="input4 validate[length[0,50]]"></asp:TextBox>
            </td>
            <th width="80px">
                入党时间
            </th>
            <td width="120px">
                <asp:TextBox ID="t_UserInfo_JoinPartyDate" runat="server" onclick="WdatePicker({dateFmt:'yyyy年MM月dd日'})"
                    CssClass="input4"></asp:TextBox>
            </td>
        </tr>
        <tr style="display:none;">
            <th width="100px">
                入伍时间
            </th>
            <td >
                <asp:TextBox ID="t_UserInfo_EnlistTime" runat="server" onclick="WdatePicker({dateFmt:'yyyy年MM月dd日'})"
                    CssClass="input4"></asp:TextBox>
            </td>
            <th width="80px">
                警衔
            </th>
            <td colspan=3>
                <asp:TextBox ID="t_UserInfo_Badge" runat="server" CssClass="input4 validate[length[0,50]]"></asp:TextBox>
            </td>
        </tr>
        <tr style="display:none;">
            <th width="100px" >
                入伍地
            </th>
            <td colspan=5>
                <asp:TextBox ID="t_UserInfo_EnlistPlace" runat="server" CssClass="input4 validate[length[0,50]]"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th width="100px">
                参加工作时间
            </th>
            <td width="220px">
                <asp:TextBox ID="t_UserInfo_StartWorkDate" runat="server" onclick="WdatePicker({dateFmt:'yyyy年MM月dd日'})"
                    CssClass="input4"></asp:TextBox>
            </td>
            <th width="80px">
                现任职务
            </th>
            <td colspan="3">
                <asp:TextBox ID="t_UserInfo_Position" runat="server" CssClass="input4 validate[length[0,100]]"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th width="100px">
                现任职务时间
            </th>
            <td width="220px">
                <asp:TextBox ID="t_UserInfo_NowHoldPostTime" runat="server" CssClass="input4" onclick="WdatePicker({dateFmt:'yyyy年MM月dd日'})"></asp:TextBox>
            </td>
            <th width="80px">
                职称
            </th>
            <td width="180px">
                <asp:DropDownList ID="t_UserInfo_OccupTitle" runat="server" CssClass="input4">
                </asp:DropDownList>
            </td>
            <th width="80px">
                职务级别
            </th>
            <td width="120px">
                <asp:DropDownList ID="t_UserInfo_PositionLevel" runat="server" CssClass="input4">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    </div>
    <table width="780" border="0" cellspacing="0" cellpadding="0" class="for">
        <tr>
            <th>
                <%-- <asp:ImageButton ID="btn_YuCun" runat="server" ImageUrl="~/Images/buttons/yucun.gif"
                        OnClick="btn_YuCun_Click" /> &nbsp;&nbsp;&nbsp;&nbsp;--%>
                <asp:ImageButton ID="btntijiao" runat="server" ImageUrl="~/Images/buttons/baocun.gif"
                    OnClick="btntijiao_Click" />
                <%--&nbsp;&nbsp;&nbsp;&nbsp;
                <img src="/Images/buttons/win_guanbi.gif" style="cursor: pointer;" onclick="javascript:window.parent.Closetab()"
                    alt="" width="50" height="24" />--%>
            </th>
        </tr>
    </table>
    <!--弹出框-->
    </form>
</body>
</html>
