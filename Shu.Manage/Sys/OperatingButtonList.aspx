<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OperatingButtonList.aspx.cs" Inherits="Shu.Manage.Sys.OperatingButtonList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Styles/List.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/table.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/common.js" type="text/javascript"></script>
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
                        <th>标题
                        </th>
                        <td>
                            <input type="text" id="Notice_Title" name="Notice_Title" class="input4" />
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
    <UC:EasyUIGrid ID="EasyUIGrid" runat="server" />
    <div id="ui_AddButton" class="easyui-window" title="新增" style="width: 700px; height: 500px; display:none" 
        toolbar="#dlg-toolbar" resizable="true" closed="true"  >
        <iframe id="view_AddButton" scrolling="yes"  width="100%"  frameborder="0" height="98%" ></iframe>
    </div>
    <div id="ui_EditButton" class="easyui-window" title="新增" style="width: 700px; height: 500px; display:none" 
        toolbar="#dlg-toolbar" resizable="true" closed="true"  >
        <iframe id="view_EditButton" scrolling="yes"  width="100%"  frameborder="0" height="98%" ></iframe>
    </div>
</body>
</html>
