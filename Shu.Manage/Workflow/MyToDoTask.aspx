<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyToDoTask.aspx.cs" Inherits="Shu.Manage.Workflow.MyToDoTask" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Styles/List.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/table.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script type="text/javascript">
    </script>
</head>
<body onresize="resizeGrid();">
    <fieldset id="fset_Select" class="listfieldset">
        <legend>
            <img id="img_Select" alt="展开或折叠" src="/Images/main/down.png" onclick="FieldSetVisual('fset_Select','img_Select','sp_Select','div_Select','div_Button')" />
            <span id="sp_Select">展开查询</span>
        </legend>
        <div id="div_Select" style="float: left; width: 80%; display: none;">
            <form id="form1" name="form1">
                <table cellpadding="0" cellspacing="0" class="list_Index">
                    <tr>
                        <th>客户名称
                        </th>

                        <td>
                            <input type="text" id="txtName" name="txtName" class="input4" value="" />
                        </td>
                    </tr>
                </table>
            </form>
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

    <div id="ui_OperatingButton" class="easyui-dialog" title="业务办理" maximized="true"  style="z-Index: 100px;"
        toolbar="#dlg-toolbar" resizable="true" closed="true"  >
        <iframe id="view_OperatingButton" scrolling="yes"  width="100%"  frameborder="0" height="100%" ></iframe>
    </div>

     <script type="text/javascript">
         $(function () {
             $('#ui_OperatingButton').dialog({
                 onBeforeClose: function () { //当面板关闭之前触发的事件
                     if (true) {
                         if (parent.$('#tabs').tabs('exists', "详细")) {
                             parent.$('#tabs').tabs('close', '详细');
                             //parent.addDesktop("我的待办任务", "MyWaitingTask.aspx", "");

                             var currTab = self.parent.$('#tabs').tabs('getSelected'); //获得当前tab
                             var url = $(currTab.panel('options').content).attr('src');
                             window.location.href = url;
                         }
                     } else
                         return false;
                 }
             });
         });
    </script>
</body>
</html>

