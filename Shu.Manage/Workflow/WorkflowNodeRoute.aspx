<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkflowNodeRoute.aspx.cs" Inherits="Shu.Manage.Workflow.WorkflowNodeRoute" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Styles/table.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/List.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/UI/themes/bootstrap/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/UI/themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="/Scripts/UI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/Scripts/DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link href="/Styles/validationEngine.jquery.css" rel="stylesheet" type="text/css"
        charset="utf-8" />
    <script src="/Scripts/Validate/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="/Scripts/Validate/jquery.validationEngine-cn.js" type="text/javascript"
        charset="gb2312"></script>
    <script src="/Scripts/Validate/FormVaildate.js" type="text/javascript"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script type="text/javascript">

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="96%" class="tab" border="0" cellpadding="1" cellspacing="1">
            <tr>
                <th width="20%">
                    流程名称
                </th>
                <td width="30%">
                    <asp:DropDownList ID="ddlworkflowTasksEx" runat="server" CssClass="input4" OnSelectedIndexChanged="ddlworkflowTasksEx_SelectedIndexChanged" AutoPostBack="true" >
                    </asp:DropDownList>
                </td>
                <th width="20%">
                    流程节点
                </th>
                <td width="30%">
                    <asp:DropDownList ID="ddlWfConfigNodeID" runat="server" CssClass="input4" >
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th width="20%">
                    审批动作
                </th>
                <td width="30%">
                    <asp:DropDownList ID="ddlAuditActionDefinition" runat="server" style="width:150px;">
                    </asp:DropDownList>
                </td>
                <th width="20%">
                    下一步骤节点
                </th>
                <td width="30%">
                    <asp:DropDownList ID="ddlNextStep" runat="server" style="width:150px;">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th width="20%">
                    是否有条件流转
                </th>
                <td width="30%">
                    <asp:DropDownList ID="ddlIfCondJump" runat="server" style="width:150px;">
                        <asp:ListItem Text="否" Value="False"></asp:ListItem>
                        <asp:ListItem Text="是" Value="True"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th width="20%">
                    下一步操作URL
                </th>
                <td width="30%">
                    <asp:TextBox ID="txtNextWfOptionURL" CssClass="input4" runat="server" style="width:99%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th width="20%">
                    下一步操作人属性
                </th>
                <td width="30%" colspan="3">
                    <asp:DropDownList ID="ddlNextWfOptionUserAttribute" runat="server" style="width:150px;">
                        <asp:ListItem Text="全局角色" Value="0"></asp:ListItem>
                        <asp:ListItem Text="发起人用户" Value="1"></asp:ListItem>
                        <asp:ListItem Text="上一步用户" Value="2"></asp:ListItem>
                        <asp:ListItem Text="运营专员" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table width="96%" border="0" cellspacing="0" cellpadding="0" class="for">
                <tr>
                    <th colspan="7">
                        <asp:ImageButton ID="btn_Tijiao" runat="server" ImageUrl="~/Images/buttons/tijiao.gif"
                            Style="cursor: pointer; height: 24px; width: 50px;" OnClick="btn_Tijiao_Click"/>
                        <a id="FanHui" href="WorkflowNodeRouteList.aspx">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/buttons/fanHui.gif" Style="cursor: pointer;
                        height: 24px; width: 50px;" /></a>
                    </th>
                </tr>
        </table>
    </div>
    </form>
</body>
</html>