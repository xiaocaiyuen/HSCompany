<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SyS_XianZhiIP.aspx.cs" Inherits="YDT.Web.Manage.Sys.SyS_XianZhiIP" %>

<%@ Register Src="~/UserControls/UCEasyUIDataGrid.ascx" TagPrefix="UC" TagName="UCEasyUIDataGrid" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="/Scripts/jquery-1.8.0.min.js" type="text/javascript"></script>
     <link href="/Styles/List.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/table.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Scripts/dbClickRowsEnent.js"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        


        function closed(type) {
            if (type == "1") {
                $('#ui_AddButton').dialog('close');
            }
            else {
                $('#ui_EditButton').dialog('close');
            }

            window.location.href = window.location.href;
        }

    </script>
</head>
<body >
    <fieldset  id="fset_Select" class="listfieldset">
         <legend>
            <img id="img_Select" alt="展开或折叠" src="/Images/main/down.png" onclick="FieldSetVisual('fset_Select','img_Select','sp_Select','div_Select','div_Button')" />
            <span id="sp_Select">展开查询</span>
        </legend>
        <div  id="div_Select" style="float: left; width: 80%;display: none;">
            <table cellpadding="0" cellspacing="0" class="list_Index">
                <form id="form1" name="form1">
                   
                    <tr>
                        <th>开始IP
                        </th>
                        <td>
                            <input type="text" id="txt_kaishi" name="txt_kaishi" class="input4" value="" />
                        </td>
                        <th>结束IP
                        </th>
                        <td colspan="3">
                            <input type="text" id="txt_jieshu" name="txt_jieshu" class="input4" value="" />
                        </td>
                    </tr>
                 
                </form>
            </table>
        </div>
        <div id="div_Button" style="float: right; width: 19%; display: none;">
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
    <UC:UCEasyUIDataGrid ID="UCEasyUIDataGrid" runat="server" />
      <!--弹出框-->
    <div id="ui_AddButton" class="easyui-window" title="新增" style="width: 400px; height: 250px;"
        toolbar="#dlg-toolbar" resizable="true" closed="true" minimizable="false"  >
        <iframe id="view_AddButton" scrolling="yes"  width="100%"  frameborder="0" height="200px" ></iframe>
    </div>
    <div id="ui_EditButton" class="easyui-dialog" title="编辑" style="width: 400px; height: 250px;"
        toolbar="#dlg-toolbar" resizable="true" closed="true"  minimizable="false" >
        <iframe id="view_EditButton" scrolling="yes"  width="100%"  frameborder="0" height="200px" ></iframe>
    </div>
</body>
</html>
