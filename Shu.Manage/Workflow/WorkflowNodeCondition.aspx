<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkflowNodeCondition.aspx.cs" Inherits="Shu.Manage.Workflow.WorkflowNodeCondition" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Styles/table.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/List.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-2.0.0.min.js"></script>
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
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script type="text/javascript">

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="96%" class="tab" border="0" cellpadding="1" cellspacing="1">
                <tr>
                    <th width="20%">流程名称
                    </th>
                    <td width="30%">
                        <asp:DropDownList ID="ddlworkflowTasksEx" runat="server" CssClass="input4" OnSelectedIndexChanged="ddlworkflowTasksEx_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                    <th width="20%">流程节点
                    </th>
                    <td width="30%">
                        <asp:DropDownList ID="ddlWfConfigNodeID" runat="server" CssClass="input4">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th width="20%">条件描述
                    </th>
                    <td width="30%">
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="validate[required] input4" Style="width: 99%; border: 1px solid #88BBE2;"></asp:TextBox>
                    </td>
                    <th width="20%">条件内容
                    </th>
                    <td width="30%">
                        <asp:TextBox ID="txtContent" runat="server" CssClass="validate[required] input4" Style="width: 99%; border: 1px solid #88BBE2;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>参数类型
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlParameterType" runat="server" CssClass="input4">
                            <asp:ListItem Text="无" Value="无" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="范围" Value="范围"></asp:ListItem>
                            <asp:ListItem Text="固定" Value="固定"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <th>范围
                    </th>
                    <td>起<asp:TextBox ID="txtParameterStart" runat="server" Style="width: 100px; border: 1px solid #88BBE2;"></asp:TextBox>
                        止<asp:TextBox ID="txtParameterEnd" runat="server" Style="width: 100px; border: 1px solid #88BBE2;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th width="20%">对比值
                    </th>
                    <td width="30%">
                        <asp:TextBox ID="txtFixedParameter" runat="server" CssClass="validate[required] input4" Style="width: 99%; border: 1px solid #88BBE2;"></asp:TextBox>
                    </td>
                    <th width="20%">对比值类型
                    </th>
                    <td width="30%">
                        <asp:DropDownList ID="ddlFixedParameterType" runat="server" CssClass="input4">
                            <asp:ListItem Text="STRING" Value="STRING" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="FLOAT" Value="FLOAT"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th width="20%">下一步节点
                    </th>
                    <td width="30%">
                        <asp:DropDownList ID="ddlNextWfConfigNodeID" runat="server" CssClass="input4">
                        </asp:DropDownList>
                    </td>
                    <th width="20%">下一步操作URL
                    </th>
                    <td width="30%">
                        <asp:TextBox ID="txtNextWfOptionURL" runat="server" CssClass="validate[required] input4" Style="width: 99%; border: 1px solid #88BBE2;"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table width="96%" border="0" cellspacing="0" cellpadding="0" class="for">
                <tr>
                    <th colspan="7">
                        <asp:ImageButton ID="btn_Tijiao" runat="server" ImageUrl="~/Images/buttons/tijiao.gif"
                            Style="cursor: pointer; height: 24px; width: 50px;" OnClick="btn_Tijiao_Click" />
                        <a id="FanHui" href="WorkflowNodeConditionList.aspx">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/buttons/fanHui.gif" Style="cursor: pointer; height: 24px; width: 50px;" /></a>
                    </th>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
