<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfoAddOr.aspx.cs" Inherits="Shu.Manage.Sys.UserInfoAddOr" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Styles/table.css" rel="stylesheet" type="text/css" />
    <%--     <link href="/Styles/default.css" rel="stylesheet" type="text/css" />--%>

    <script type="text/javascript" src="/Scripts/jquery-2.0.0.min.js"></script>
    <link href="/Content/themes/bootstrap/easyui.css" rel="stylesheet" />
    <link href="/Content/themes/icon.css" rel="stylesheet" />
    <script type="text/javascript" src="/Scripts/jquery.easyui-1.4.5.min.js" charset="utf-8"></script>
    <script type="text/javascript" src="/Scripts/locale/easyui-lang-zh_CN.js" charset="utf-8"></script>

    <script src="/Scripts/DatePicker/WdatePicker.js" type="text/javascript"></script>
   <%-- <link href="/Styles/validationEngine.jquery.css" rel="stylesheet" type="text/css" charset="utf-8" />
    <script src="/Scripts/Validate/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="/Scripts/Validate/jquery.validationEngine-cn.js" type="text/javascript" charset="gb2312"></script>
    <script src="/Scripts/Validate/FormVaildate.js" type="text/javascript"></script>--%>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script type="text/javascript">

        function isIdCardNo(icd) {///身份证显示出身年月
            var myDate = new Date();
            var Ctiem = icd.substring(6, 10);
            var Atiem = icd.substring(10, 12);
            var Btiem = icd.substring(12, 14);
            var Ntiem = myDate.getFullYear();
            var age = parseInt(Ntiem) - parseInt(Ctiem) + 1;
            $("#t_UserInfo_Age").val(age);
            $("#t_UserInfo_DateBirth").val(Ctiem + "年" + Atiem + "月" + Btiem + "日");
        }

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
            $("#view").attr("src", 'SelectUserEx.aspx?id=' + $("#hid_AccountId").val() + '&type=' + str);
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

        <div>
            <div id="tt" class="easyui-tabs" style="height: 460px">
                <div title="用户管理" data-options="closable:false" style="overflow: auto; padding: 10px;">
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <asp:HiddenField ID="hid_JJ" runat="server" />
                    <asp:HiddenField ID="hid_Phone" runat="server" />
                    <asp:HiddenField ID="hid_LoginName" runat="server" />
                    <asp:HiddenField ID="hid_AccountId" runat="server" />
                    <center>
        <div id="titleScore" class="easyui-panel" title="用户管理" style="width: auto; min-width: 800px; height: auto; text-align: center">
        <table class="tab" cellpadding="1" cellspacing="1" width="780">
            <tr>
                <th>
                    用户账号<font color="red">*</font>
                </th>
                <td>
                    <asp:TextBox ID="txtLoginName" runat="server" CssClass="validate[length[1,20]] input4"></asp:TextBox>
                    
                </td>
                <th>
                    用户密码<font color="red">*</font>
                </th>
                <td>
                    <asp:TextBox ID="txtLoginPwd" runat="server" CssClass="validate[length[6,16]] input4" TextMode="Password"></asp:TextBox>
                    
                </td>
                <th>
                    用户姓名 <font color="red">*</font>
                </th>
                <td>
                    <asp:TextBox ID="txtManName" runat="server" CssClass="validate[length[1,16]] input4"></asp:TextBox>
                   
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
                    排序<font color="red">*</font>
                </th>
                <td>
                    <asp:TextBox ID="txtPx" CssClass="validate[required,custom[intNum]] input4" runat="server"></asp:TextBox>
                    
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
                    所在岗位<font color="red">*</font>
                </th>
                <td colspan="5" id="tdGw">
                    <asp:CheckBoxList ID="chbGwList" runat="server" RepeatDirection="Horizontal" RepeatColumns="5">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <th>
                    角色权限<font color="red">*</font>
                </th>
                <td colspan="5" id="tdJs">
                    <asp:CheckBoxList ID="ckbxlist" runat="server" RepeatDirection="Horizontal" RepeatColumns="5">
                    </asp:CheckBoxList>
                </td>
            </tr>
            
        </table>
        </div>
        <!--弹出框-->
        <div id="d2" class="easyui-dialog" title="部门选择" style="width: 280px; height: 400px;"
            toolbar="#dlg-toolbar" resizable="true" closed="true">
            <iframe id="view" scrolling="yes" width="100%" frameborder="0" height="350"></iframe>
        </div>
    </center>
                </div>
                <div title="用户其他信息维护" data-options="closable:false" style="overflow: auto; padding: 10px;">
                    <asp:HiddenField ID="hid_Dep" runat="server" />
                    <asp:HiddenField ID="txt_Yhxx_DTcsrq" runat="server" />
                    <asp:HiddenField ID="txt_Yhxx_Iage" runat="server" />
                    <div id="titleScore1" class="easyui-panel" title="用户信息维护" style="width: auto; min-width: 800px; height: auto; text-align: center">
                        <table width="96%" class="tab" border="0" cellpadding="1" cellspacing="1">
                            <tr style="display: none;">
                                <th width="20%">用户账号
                                </th>
                                <td width="30%">
                                    <asp:Label ID="t_UserInfo_LoginUserName" runat="server"></asp:Label>
                                </td>
                                <th width="20%">用户姓名
                                </th>
                                <td width="30%">
                                    <asp:Label ID="t_UserInfo_FullName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr style="display: none;">
                                <th width="20%">用户密码<font color="red">*</font>
                                </th>
                                <td width="30%">
                                    <asp:TextBox ID="t_UserInfo_LoginUserPwd" runat="server" TextMode="Password" CssClass="input4 validate[length[0,16]]"></asp:TextBox>
                                </td>
                                <th width="20%" style="display: none;">用户状态
                                </th>
                                <td width="30%" style="display: none;">
                                    <asp:Label ID="t_UserInfo_StatusName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th width="20%">民族
                                </th>
                                <td width="30%">
                                    <asp:DropDownList ID="t_UserInfo_Nation" runat="server" CssClass="input4">
                                    </asp:DropDownList>
                                </td>
                                <th width="20%">性别
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
                                <th width="20%">身份证
                                </th>
                                <td width="30%">
                                    <asp:TextBox ID="t_UserInfo_IdentityCred" onblur="isIdCardNo(this.value);" runat="server"
                                        CssClass="input4 validate[length[0,18]]"></asp:TextBox>
                                </td>
                                <th width="20%">年龄
                                </th>
                                <td width="30%">
                                    <asp:TextBox ID="t_UserInfo_Age" runat="server" CssClass="input4 validate[length[0,11],custom[intNum2]]" Width="20px"></asp:TextBox>岁
                                </td>
                            </tr>
                            <tr>
                                <th width="20%">出生日期
                                </th>
                                <td width="30%">
                                    <asp:TextBox ID="t_UserInfo_DateBirth" CssClass="input4 validate[length[0,50]]" onclick="WdatePicker({dateFmt:'yyyy年MM月dd日'})" runat="server"></asp:TextBox>
                                </td>

                                <th width="20%">手机号码<font color="red">*</font>
                                </th>
                                <td width="30%">
                                    <asp:TextBox ID="t_UserInfo_PhoneNumber" runat="server" CssClass="input4 validate[custom[mobilephone]]"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="display: none;">
                                <th width="20%">所在部门
                                </th>
                                <td colspan="3">
                                    <asp:Label ID="t_Department_Name" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr style="display: none;">
                                <th width="20%">岗位
                                </th>
                                <td colspan="3">
                                    <asp:Label ID="t_UserInfo_PostName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th width="20%">籍贯
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="t_UserInfo_Hometown" runat="server" CssClass="input4 validate[length[0,100]]"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th width="20%">家庭住址
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="t_UserInfo_Address" runat="server" CssClass="input4 validate[length[0,100]]"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th width="20%">政治面貌
                                </th>
                                <td width="30%">
                                    <asp:DropDownList ID="t_UserInfo_PoliticalLandscape" runat="server" CssClass="input4 ">
                                    </asp:DropDownList>
                                </td>
                                <th width="20%">学历<font color="red"></font>
                                </th>
                                <td width="30%">
                                    <asp:DropDownList ID="t_UserInfo_EducationalLevel" runat="server" CssClass="input4 ">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <th width="20%">毕业院校
                                </th>
                                <td width="30%">
                                    <asp:TextBox ID="t_UserInfo_Schools" runat="server" CssClass="input4 validate[length[0,40]]"></asp:TextBox>
                                </td>
                                <th width="20%">学习专业
                                </th>
                                <td width="30%">
                                    <asp:TextBox ID="t_UserInfo_Specialty" runat="server" CssClass="input4 validate[length[0,50]]"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th width="20%">参加工作时间
                                </th>
                                <td width="30%">
                                    <asp:TextBox ID="t_UserInfo_StartWorkDate" runat="server" onclick="WdatePicker({dateFmt:'yyyy年MM月dd日'})"
                                        CssClass="input4"></asp:TextBox>
                                </td>
                                <th width="20%">入党时间
                                </th>
                                <td>
                                    <asp:TextBox ID="t_UserInfo_JoinPartyDate" runat="server" onclick="WdatePicker({dateFmt:'yyyy年MM月dd日'})"
                                        CssClass="input4"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th width="20%">入伍时间
                                </th>
                                <td width="30%">
                                    <asp:TextBox ID="t_UserInfo_EnlistTime" runat="server" onclick="WdatePicker({dateFmt:'yyyy年MM月dd日'})"
                                        CssClass="input4"></asp:TextBox>
                                </td>
                                <th width="20%">警衔
                                </th>
                                <td>
                                    <asp:TextBox ID="t_UserInfo_Badge" runat="server" CssClass="input4 validate[length[0,50]]"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th width="20%">入伍地
                                </th>
                                <td width="30%" colspan="3">
                                    <asp:TextBox ID="t_UserInfo_EnlistPlace" runat="server"
                                        CssClass="input4 validate[length[0,50]]"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th width="20%">现任职务
                                </th>
                                <td width="30%">
                                    <asp:TextBox ID="t_UserInfo_Position" runat="server" CssClass="input4 validate[length[0,100]]"></asp:TextBox>
                                </td>
                                <th width="20%">职务级别
                                </th>
                                <td>
                                    <asp:DropDownList ID="t_UserInfo_PositionLevel" runat="server" CssClass="input4">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <th width="20%">现任职务时间
                                </th>
                                <td width="30%">
                                    <asp:TextBox ID="t_UserInfo_NowHoldPostTime" runat="server" onclick="WdatePicker({dateFmt:'yyyy年MM月dd日'})"
                                        CssClass="input4"></asp:TextBox>
                                </td>
                                <th width="20%">职称
                                </th>
                                <td width="30%">
                                    <asp:DropDownList ID="t_UserInfo_OccupTitle" runat="server" CssClass="input4 ">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>

                    <th>
                        <asp:ImageButton runat="server" OnClientClick="return isRoleChecked();" ID="btnAdd"
                            OnClick="btnAdd_Click" ImageUrl="~/Images/buttons/baocun.gif" />
                        &nbsp;<a href="#" id="FanHui"><asp:Image ID="btn_FH" runat="server" ImageUrl="~/Images/buttons/fanHui.gif"
                            Height="24px" Width="50px" /></a>


                        <%--<asp:ImageButton ID="btntijiao" runat="server" ImageUrl="~/Images/buttons/baocun.gif"
                    OnClick="btntijiao_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp; <a id="A1" href="#" onclick=" fanhui()">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/buttons/fanHui.gif" /></a>--%>
                    </th>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
