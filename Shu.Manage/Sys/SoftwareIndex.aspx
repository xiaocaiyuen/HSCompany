<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SoftwareIndex.aspx.cs" Inherits="Shu.Manage.Sys.SoftwareIndex" %>

<%@ Register Src="~/UserControls/UCEasyUIDataGrid.ascx" TagPrefix="UC" TagName="UCEasyUIDataGrid" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Styles/List.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/table.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
</head>
<body>
    <fieldset id="fset_Select"  class="listfieldset">
         <legend>
            <img id="img_Select" alt="展开或折叠" src="/Images/main/down.png" onclick="FieldSetVisual('fset_Select','img_Select','sp_Select','div_Select','div_Button')" />
            <span id="sp_Select">展开查询</span>
        </legend>
        <div id="div_Select" style="float: left; width: 80%;display: none;">
            <table cellpadding="0" cellspacing="0" class="list_Index">
                <form id="form1" name="form1">
                <tr>
                    <th>
                        名称
                    </th>
                    <td>
                        <input type="text" id="Software_Name" name="Software_Name" class="input4" />
                    </td>
                    <th>
                        发布时间
                    </th>
                    <td>
                        <input type="text" id="txt_TimeFrom" name="txt_TimeFrom" class="input4"onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd' })" />
                        至
                        <input id="txt_TimeTo" type="text" name="txt_TimeTo" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd' })" class="input4"/>
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
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
    <UC:UCEasyUIDataGrid ID="UCEasyUIDataGrid" runat="server" />
</body>
</html>
