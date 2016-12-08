<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataGrid.aspx.cs" Inherits="YDT.Web.Manage.DataGrid" %>

<%@ Register Src="~/UserControls/UCEasyUIDataGrid.ascx" TagPrefix="UC" TagName="UCEasyUIDataGrid" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="/Styles/List.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        //处理按钮启用不启用功能
        function BtnDisabled(i, rows) {
            if (rows.Role_Name == "纪检监察负责人") {
                $('#EditButton_' + i).linkbutton({ disabled: true });
                $('#DelButton_' + i).linkbutton({ disabled: true });
            }
        }
    </script>
</head>
<body>
    <fieldset class="listfieldset">
        <legend>查询条件</legend>
        <table cellpadding="0" cellspacing="0" class="list_Index">
            <tr>
                <th>
                    角色名称
                </th>
                <form id="form1" name="form1">
                <td>
                    <input type="text" id="txtRoleName" name="txtRoleName" class="input3" value="" />
                </td>
                </form>
                <th style="width: 180px;">
                    <input id="btn_Search" type="image" src="/Images/buttons/chaXun.gif" />
                    &nbsp;
                    <input id="btnChongzhi" type="image" src="/Images/buttons/ChongZhiChaXun.gif" />
                </th>
            </tr>
        </table>
    </fieldset>
    <UC:UCEasyUIDataGrid ID="UCEasyUIDataGrid" runat="server" />
    <!--弹出框-->
    <div id="ui_AddButton" class="easyui-window" title="新增" style="width: 700px; height: 400px;"
        toolbar="#dlg-toolbar" resizable="true" closed="true"  >
        <iframe id="view_AddButton" scrolling="yes"  width="100%"  frameborder="0" height="360" ></iframe>
    </div>
    <div id="ui_EditButton" class="easyui-dialog" title="编辑" style="width: 700px; height: 400px;"
        toolbar="#dlg-toolbar" resizable="true" closed="true"  >
        <iframe id="view_EditButton" scrolling="yes"  width="100%"  frameborder="0" height="360" ></iframe>
    </div>
    
</body>
</html>
