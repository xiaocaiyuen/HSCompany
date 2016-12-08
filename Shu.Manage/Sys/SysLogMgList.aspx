<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysLogMgList.aspx.cs" Inherits="Shu.Manage.Sys.SysLogMgList" %>

<%@ Register Src="~/UserControls/UCEasyUIDataGrid.ascx" TagPrefix="UC" TagName="UCEasyUIDataGrid" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="/Styles/List.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/table.css" rel="stylesheet" type="text/css" />
    <%--<script src="/Scripts/DatePicker/WdatePicker.js" type="text/javascript"></script>--%>
    <script src="/Scripts/dbClickRowsEnent.js"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script type="text/javascript">
        //屏蔽删除、批量删除按钮
        function BtnDisabled(i, rows) {
            $('#DeltBatchButton').hide();
            $('#DelButton_' + i).hide();
        }
    </script>
</head>
<body onresize="resizeGrid();">
    <fieldset  id="fset_Select" class="listfieldset">
         <legend>
            <img id="img_Select" alt="展开或折叠" src="/Images/main/down.png" onclick="FieldSetVisual('fset_Select','img_Select','sp_Select','div_Select','div_Button')" />
            <span id="sp_Select">展开查询</span>
        </legend>
        <div  id="div_Select" style="float: left; width: 80%;display: none;">
            <table cellpadding="0" cellspacing="0" class="list_Index">
                <form id="form1" name="form1">
                    <tr>
                        <th>人员名称
                        </th>
                        <td>
                            <input type="text" id="txt_FullName" name="txt_FullName" class="input4" value="" />
                        </td>
                        <th>部门
                        </th>
                        <td colspan="3">
                            <input type="text" id="txt_DepName" name="txt_DepName" class="input4" value="" />
                        </td>
                    </tr>
                    <tr>
                        <th>操作类型
                        </th>
                        <td>
                            <input type="text" id="txt_OperateType" name="txt_OperateType" class="input4" value="" />
                        </td>
                        <th>操作时间
                        </th>
                        <td>
                            <input type="text" id="txt_OperateDateFrom" name="txt_OperateDateFrom" class="input4"
                                onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd' })" />
                            &nbsp;至<input type="text" id="txt_OperateDateTo" name="txt_OperateDateTo" class="input4"
                                onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd' })" />
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
</body>
</html>
