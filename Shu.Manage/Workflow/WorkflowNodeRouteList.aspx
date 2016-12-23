<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkflowNodeRouteList.aspx.cs" Inherits="Shu.Manage.Workflow.WorkflowNodeRouteList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Styles/List.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/table.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script type="text/javascript">
        //处理按钮启用不启用功能
        //        function BtnDisabled(i, rows) {
        //            var currUserRole = '<%=bg.CurrUserInfo().RoleName %>';
        //            if (currUserRole.indexOf('<%=Constant.SysAdminRoleName %>') < 0 && currUserRole.indexOf('<%=Constant.SuperAdminRoleName %>') < 0) {
        //                $('.DelButton').linkbutton({ disabled: true });
        //                $('.EditButton').linkbutton({ disabled: true });
        //            } 
        //        }
    </script>
</head>
<body onresize="resizeGrid();">
    <fieldset id="fset_Select" class="listfieldset">
        <legend>
            <img id="img_Select" alt="展开或折叠" src="/Images/main/down.png" onclick="FieldSetVisual('fset_Select','img_Select','sp_Select','div_Select','div_Button')" />
            <span id="sp_Select">展开查询</span>
        </legend>
        <div id="div_Select" style="float: left; width: 80%; display: none;">
            <table cellpadding="0" cellspacing="0" class="list_Index">
                <tr>
                    <th>流程任务名称
                    </th>
                    <form id="form1" name="form1">
                        <td>
                            <input type="text" id="txtName" name="txtName" class="input4" value="" />
                        </td>
                    </form>
                </tr>
            </table>
        </div>
        <div id="div_Button" style="float: right; width: 19%; display: none;">
            <table>
                <tr>
                    <td>
                        <a id="btn_Search" href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-search'" style="width: 80px">查询</a>
                        <a id="btnChongzhi" href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-reload'" style="width: 80px">重置</a>
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
    <UC:EasyUIGrid ID="UCEasyUIDataGrid" runat="server" />
</body>
</html>
