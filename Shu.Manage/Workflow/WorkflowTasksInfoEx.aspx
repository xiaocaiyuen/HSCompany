<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkflowTasksInfoEx.aspx.cs" Inherits="Shu.Manage.Workflow.WorkflowTasksInfoEx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Styles/List.css" rel="stylesheet" type="text/css" />
    <link href="/Content/themes/bootstrap/easyui.css" rel="stylesheet" />
    <link href="/Content/themes/icon.css" rel="stylesheet" />
    <script type="text/javascript" src="/Scripts/jquery.easyui-1.4.5.min.js" charset="utf-8"></script>
    <script src="/Scripts/DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Scripts/locale/easyui-lang-zh_CN.js" charset="utf-8"></script>
    <%--<link href="/Styles/validationEngine.jquery.css" rel="stylesheet" type="text/css"
        charset="utf-8" />
    <script src="/Scripts/Validate/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="/Scripts/Validate/jquery.validationEngine-cn.js" type="text/javascript"
        charset="gb2312"></script>
    <script src="/Scripts/Validate/FormVaildate.js" type="text/javascript"></script>--%>
    <%--<script src="/Scripts/Organ/jiaose.js" type="text/javascript"></script>--%>
    
    <script type="text/javascript">
        function ShowSelect() {
            parent.ShowSelect();
        }

        function ReturnValues(code, text, depname, postname) {
            $('input[id*=hid_DepCode]').val(code);
            $('input[id*=txt_Dep]').val(text);
            $('span[id*=t_PersonDepName]').text(depname);
            $('span[id*=t_PersonPostName]').text(postname);
            $('#uctextselects').dialog('close');
            $('#ucselects').hide();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hid_DepCode" runat="server" />
    <asp:HiddenField ID="hid_id" runat="server" />
    <div id="divCont" runat="server">
        <table width="100%" style="height: 160px;">
            <tr>
                <td style="text-align: right; width: 100px;">
                    业务流程编码：
                </td>
                <td>
                    <asp:TextBox ID="txtWorkflowTasks_InstanceNo" runat="server" CssClass="input4 validate[length[1,50]]"
                        Style="width: 180px; line-height: 20px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    业务流程名称：
                </td>
                <td>
                    <asp:TextBox ID="txtWorkflowTasks_Name" CssClass="input4 validate[required]" runat="server"
                        Width="180px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    业务流程版本：
                </td>
                <td>
                    <asp:TextBox ID="txtWorkflowTasks_InstanceVerNo" CssClass="input4 validate[required,length[1,50]]"
                        runat="server" Width="180px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    业务类型：
                </td>
                <td>
                    <asp:DropDownList ID="drp_WorkflowTasks_Type" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    发布状态：
                </td>
                <td>
                    <asp:DropDownList ID="drp_WorkflowTasks_IsValid" runat="server">
                        <asp:ListItem Value="False" Text="未发布" Enabled="true"></asp:ListItem>
                        <asp:ListItem Value="True" Text="已发布"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center;">
                    <asp:ImageButton ImageUrl="~/Images/buttons/baocun.gif" runat="server" ID="btnSave"
                        OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
