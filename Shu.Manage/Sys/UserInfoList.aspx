<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfoList.aspx.cs" Inherits="Shu.Manage.Sys.UserInfoList" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Styles/List.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/table.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/dbClickRowsEnent.js"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script type="text/javascript">
        function colsd() {
            $('#ui_RedoButton').dialog('close');
            window.location.href = window.location.href;
        };
    </script>
</head>
<body onresize="resizeGrid();">
    <fieldset id="fset_Select"  class="listfieldset">
        <legend>
            <img id="img_Select" alt="展开或折叠" src="/Images/main/down.png" onclick="FieldSetVisual('fset_Select','img_Select','sp_Select','div_Select','div_Button')" />
            <span id="sp_Select">展开查询</span>
        </legend>
        <div  id="div_Select" style="float: left; width: 80%;display: none;">
            <table cellpadding="0" cellspacing="0" class="list_Index">
                <form id="form1" name="form1">
                <tr>
                    <th style="width:50px;">
                        部门
                    </th>
                    <td style="width:50px;">
                        <input type="text" id="txtDepName" name="txtDepName" class="input4" />
                    </td>
                    <th>
                        姓名
                    </th>
                    <td>
                        <input type="text" id="txtName" name="txtName" class="input4" />
                    </td>
                </tr>
                <tr>
                    <th>
                        角色
                    </th>
                    <td>
                        <input type="text" id="txtRole" name="txtRole" class="input4" />
                    </td>
                    <th>
                        岗位
                    </th>
                    <td>
                        <input type="text" id="txtPostName" name="txtPostName" class="input4" />
                    </td>
                </tr>
                </form>
            </table>
        </div>
        <div id="div_Button" style="float: right; width: 29%;margin-top:-40px;display: none;">
            <table>
                <tr>
                    <td>
                        <a id="btn_Search"  href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-search'" style="width:80px">查询</a>
                        <a id="btnChongzhi" href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-reload'" style="width:80px">重置</a>
                       <%-- <input id="btn_Search" type="image" src="/Images/buttons/chaXun.gif" />
                        &nbsp;
                        <input id="btnChongzhi" type="image" src="/Images/buttons/ChongZhiChaXun.gif" />--%>
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
    <UC:ButtonGrid ID="UCEasyUIDataGrid" runat="server" />
    <div id="ui_RedoButton" class="easyui-window" title="新增" style="width: 400px; height: 180px; display:none" 
        toolbar="#dlg-toolbar" resizable="true" closed="true"  >
        <iframe id="view_RedoButton" scrolling="yes"  width="100%"  frameborder="0" height="130px" ></iframe>
    </div>
</body>
</html>
