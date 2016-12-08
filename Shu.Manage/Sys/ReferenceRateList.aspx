<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReferenceRateList.aspx.cs" Inherits="YDT.Web.Manage.Sys.ReferenceRateList" %>

<%@ Register Src="~/UserControls/UCEasyUIDataGrid.ascx" TagPrefix="UC" TagName="UCEasyUIDataGrid" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <title></title>
    <link href="/Styles/List.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/table.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Scripts/dbClickRowsEnent.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
     <script type="text/javascript">
         //处理按钮启用不启用功能
         function BtnDisabled(i, rows) {
             //已启用的基准利率，不可再编辑、启用、删除
             if (rows.ReferenceRate_IsEnable == true) {
                 $('#EditButton_' + i).linkbutton({ disabled: true });
                 $('#OpenButton_' + i).linkbutton({ disabled: true });
                 $('#DelButton_' + i).linkbutton({ disabled: true });
             }
             else {
                 var nowDate = FormatEasyUiDate(new Date(), "yyyy-MM-dd");
                 var adjustDate = FormatEasyUiDate(rows.ReferenceRate_AdjustDate, "yyyy-MM-dd");
                 //未启用的基准利率，如果已过期，不可再编辑、启用
                 if (adjustDate < nowDate) {
                     $('#EditButton_' + i).linkbutton({ disabled: true });
                     $('#OpenButton_' + i).linkbutton({ disabled: true });
                 }
             }
         }
    </script>
</head>
<body onresize="resizeGrid();">
    <fieldset id="fset_Select" class="listfieldset">
        <legend>
            <img id="img_Select" alt="展开或折叠" src="/Images/main/down.png" onclick="FieldSetVisual('fset_Select','img_Select','sp_Select','div_Select','div_Button')" />
            <span id="sp_Select">展开查询</span>
        </legend>
        <div id="div_Select" style="float: left; width: 80%;display: none;">
            <table cellpadding="0" cellspacing="0" class="list_Index">
                <form id="form1" name="form1">
                    <tr>
                        <th>调整日期
                        </th>
                        <td>
                            <input type="text" id="txtReferenceRate_StartDate" name="txtReferenceRate_StartDate" runat="server" class="input4" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd' })" />
                            至
                            <input type="text" id="txtReferenceRate_EndDate" name="txtReferenceRate_EndDate" runat="server" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd' })" class="input4" />
                        </td>
                    </tr>
                </form>
            </table>
        </div>
        <div id="div_Button" style="float: right; width: 19%;display: none;">
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
    <!--弹出框-->
    <div id="ui_AddButton" class="easyui-window" title="新增" style="width: 400px; height: 250px;"
        toolbar="#dlg-toolbar" resizable="true" closed="true">
        <iframe id="view_AddButton" scrolling="yes" width="100%" frameborder="0" height="210px">
        </iframe>
    </div>
    <div id="ui_EditButton" class="easyui-dialog" title="编辑" style="width: 400px; height: 250px;"
        toolbar="#dlg-toolbar" resizable="true" closed="true">
        <iframe id="view_EditButton" scrolling="yes" width="100%" frameborder="0" height="210px">
        </iframe>
    </div>
</body>
</html>

