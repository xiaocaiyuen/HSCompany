<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalesConsultantList.aspx.cs" Inherits="YDT.Web.Manage.Sys.SalesConsultantList" %>

<%@ Register Src="~/UserControls/UCEasyUIDataGrid.ascx" TagPrefix="UC" TagName="UCEasyUIDataGrid" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <title></title>
    <link href="/Styles/List.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/table.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
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
                        <th>姓名
                        </th>
                        <td>
                            <input type="text" id="txtSalesConsultant_Name" name="txtSalesConsultant_Name" class="input4" />
                        </td>
                        <th>身份证号
                        </th>
                        <td>
                            <input type="text" id="txtSalesConsultant_IDCardNo" name="txtSalesConsultant_IDCardNo" class="input4" />
                        </td>
                        <th>手机号码
                        </th>
                        <td>
                            <input type="text" id="txtSalesConsultant_PhoneNum" name="txtSalesConsultant_PhoneNum" class="input4" />
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
</body>
</html>
