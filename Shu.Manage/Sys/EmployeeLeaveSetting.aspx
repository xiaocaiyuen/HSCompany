<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeLeaveSetting.aspx.cs" Inherits="Shu.Manage.Sys.EmployeeLeaveSetting" %>

<%@ Register Src="~/UserControls/UCEasyUIDataGrid.ascx" TagPrefix="UC" TagName="UCEasyUIDataGrid" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Styles/table.css" rel="stylesheet" />
    <script src="../../Scripts/common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function AssignmentModeClick() {
            var ids = getSelections();
            if (ids == "") {
                showAlertMsg("请至少选择一条记录！");
            }
            else {
                var url = "EmployeeLeaveSetting.aspx?UserInfoID=" + $("#hidUserInfoID").val() + "&roleid=" + $("#hidRoleID").val() + "&cond=0";
                var parm = 'active=AssignmentMode&ids=' + ids + '';
                $("#divLoad").show();
                $("#btn_AssignmentMode").hide();
                getAjax(url, parm, function (rs) {
                    if (rs == "0") {
                        showAlertMsg("转入失败！");
                    }
                    else if (rs == "1") {
                        showAlertMsgUrl("转入成功！", url);
                    }
                    else {
                        showAlertMsg(rs);
                    }
                    $("#divLoad").hide();
                    $("#btn_AssignmentMode").show();
                });
            }
        }

        function ddlCondChange(obj)
        {
            alert(obj.value)
            var url = "EmployeeLeaveSetting.aspx?UserInfoID=" + $("#hidUserInfoID").val() + "&cond=" + obj.value + "&roleid=" + $("#hidRoleID").val() + "";
            alert(url)
            location.href = url;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hidUserInfoID" runat="server" />
        <asp:HiddenField ID="hidCond" runat="server" />
        <asp:HiddenField ID="hidRoleID" runat="server" />
        <table cellpadding="0" cellspacing="0" class="list_Index">
            <tr>
                <td style="width: 250px; padding-left: 5px;">
                    数据分类条件&nbsp;<select id="ddlCond"  name="ddlCond" onchange="ddlCondChange(this);"  style="width: 150px;">
                        <option value="0">已分单待审批处理数据</option>
                        <%--<option value="1">待分单数据</option>--%>
                    </select>
                </td>
                <td>
                     <a id="btn_AssignmentMode" href="javascript:void(0)"  onclick="AssignmentModeClick();" class="easyui-linkbutton" data-options="iconCls:'icon-distribution'" style="width: 180px">批量转入人工指派模式</a>
                     <div id="divLoad" style="display:none;"><img src="../../Images/main/loader.gif" alt="" />处理中...</div>
                </td>
            </tr>
        </table>
        <UC:UCEasyUIDataGrid ID="UCEasyUIDataGrid" runat="server" />
    </form>
</body>
</html>
