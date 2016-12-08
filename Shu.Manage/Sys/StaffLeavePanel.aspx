<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaffLeavePanel.aspx.cs" Inherits="Shu.Manage.Sys.StaffLeavePanel" %>

<%@ Register Src="~/UserControls/UCEasyUIDataGrid.ascx" TagPrefix="UC" TagName="UCEasyUIDataGrid" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Styles/List.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/table.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-2.0.0.min.js"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <%--<script src="/Scripts/DatePicker/WdatePicker.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        //详细页面
        function addTab(title, url, ico) {
            window.parent.addTab(title, url, '')
        }

        function returnRowsValues(rowIndex, rowData) {
            //alert(rowData.ApplyBasisId)
            $('#chart').attr("src", "EmployeeLeaveSetting.aspx?UserInfoID=" + rowData.UserInfoID + "&roleid=" + rowData.UserInfo_RoleID + "&cond=0");
        }
    </script>
</head>
<body onresize="resizeGrid();" class="easyui-layout" style="overflow-y: hidden;" fit="true" scroll="no">
    <div region="north" split="true" title="&nbsp;员工列表(<font color=red>请鼠标双击行记录查看审批业务明细</font>)" style="height: 250px;">
        <div id="div_Select">
            <form id="form1" name="form1">
                <table cellpadding="0" cellspacing="0" class="list_Index">

                    <tr>
                        <th style="width: 50px; padding-left: 5px;">角色名称
                        </th>
                        <td style="width: 50px; padding-left: 5px;">
                            <select id="ddlRole" class="easyui-combobox" name="ddlRole" class="input4" style="width: 130px;">
                                <option></option>
                                <%listRole.ForEach(item =>
                                { %>
                                <option value="<%=item.RoleID %>"><%=item.Role_Name %></option>
                                <%}); %>
                            </select>
                        </td>
                        <td>
                            <a id="btn_Search" href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-search'" style="width: 80px">查询</a>
                            <a id="btnChongzhi" href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-reload'" style="width: 80px">重置</a>
                        </td>
                    </tr>
                </table>
            </form>
        </div>
        <UC:UCEasyUIDataGrid ID="UCEasyUIDataGrid" runat="server" />
    </div>
    <div region="center" style="overflow-y: hidden; overflow-x: hidden">
        <iframe id="chart" width="100%" height="100%" scrolling="yes" frameborder="0"></iframe>
    </div>
</body>
</html>
