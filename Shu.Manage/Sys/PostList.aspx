<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PostList.aspx.cs" Inherits="Shu.Manage.Sys.PostList" %>

<%--<%@ Register src="../../UserControls/UCDepartmentTreeText.ascx" tagname="UCDepartmentTreeText" tagprefix="uc1" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Styles/List.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/table.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/dbClickRowsEnent.js"  type="text/javascript"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script type="text/javascript">



        window.onload = function () {
            //弹出窗 关闭按钮事件
            $('.panel-tool-close').click(function () {

                $('#hid_Type').val("");
                $('#UCDepartmentTreeText1_txt_depName').val("");
                $('#txtGwName').val("");
                $('#txtpx').val("");

            })

//            $('#AddButton').click(function () {
//                alert();
//                AddWin();
//                return false;

//            });

            if ($('#hid_Type').val() == "modify") {
                $('#d2').dialog('open');
                $('#d2').parent().appendTo($("form:first"))
                $('#div_1').show();
            }
            //            GridSubStr("UCGrid", 1, 15);
            //            GridSubStr("UCGrid", 2, 25);
        }

        function AddWin() {
            $('#d2').dialog('open');
            $('#d2').parent().appendTo($("form:first"))
            $('#div_1').show();
            $('#hid_Type').val('add');


            return false;
        }


        function CheckData() {
            var reg = /^[0-9]+$/;


            if ($("#UCDepartmentTreeText1_txt_depName").val() == "") {
                alert("部门不能为空"); return false;
            }
            if ($("#txtGwName").val() == "") {
                alert("岗位不能为空"); return false;
            }
            if ($("#txtpx").val() == "") {
                alert("岗位排序不能为空"); return false;
            }
            else if (!reg.test($("#txtpx").val())) {
                alert("岗位排序只能输入整数"); return false;
            }
            return true;
        }

        function ShowSelectDep() {

            $('#ucdeptextselect').dialog('open');

            $('#ucdepselect').show();

            var strsrc = $("#ucdepview").attr("src");

            if (strsrc == "" || strsrc == undefined) {

                $("#ucdepview").attr("src", '/Windows/Win_EasyUISelectDept.aspx');
            }
        }

        function ReturnValue(depCode, depName) {
            $("#hid_DepCode").val(depCode);
            $("#txt_depName").val(depName);
            closewinDep();
        }

        function closewinDep() {
            $('#ucdeptextselect').dialog('close');
            $('#ucdepselect').hide();
        }

        function  closed(type)
        {
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
<body onresize="resizeGrid();">
    <fieldset id="fset_Select" class="listfieldset">
         <legend>
            <img id="img_Select" alt="展开或折叠" src="/Images/main/down.png" onclick="FieldSetVisual('fset_Select','img_Select','sp_Select','div_Select','div_Button')" />
            <span id="sp_Select">展开查询</span>
        </legend>
        <div id="div_Select" style="float: left; width: 80%;display: none;">
            <form id="form1">
            <table cellpadding="0" cellspacing="0" class="list_Index">
                <%--<tr>
                <th>
                    所在部门
                </th>
                <td>
                    <asp:TextBox ID="txtDep" runat="server" CssClass="input3"></asp:TextBox>
                </td>
                <th>
                    岗位
                </th>
                <td>
                    <asp:TextBox ID="txtPost" runat="server" CssClass="input3"></asp:TextBox>
                </td>
                <th style="width: 180px;">
                    <asp:ImageButton ID="btn_Search" runat="server" ImageUrl="~/Images/buttons/chaXun.gif"
                        OnClick="btnSearch_Click" />
                    &nbsp;
                    <asp:ImageButton ID="btnChongzhi" ImageUrl="~/Images/buttons/ChongZhiChaXun.gif"
                        runat="server" OnClick="btnChongzhi_Click" />
                </th>
            </tr>--%>
                <tr>
                    <th>
                        所在部门
                    </th>
                    <td style="width:50px;">
                        <input type="text" id="txtDep" name="txtDepName" class="input4" />
                    </td>
                    <th>
                        岗位
                    </th>
                    <td>
                        <input type="text" id="txtPost" name="txtPost" class="input4" />
                    </td>
                </tr>
            </table>

            </form>
        </div>
        <div id="div_Button" style="float: right; width: 29%;margin-top:-40px;display: none;">
            <table>
                <tr>
                    <td>
                        <a id="btn_Search"  href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-search'" style="width:80px">查询</a>
                        <a id="btnChongzhi" href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-reload'" style="width:80px">重置</a>
                        <%--<input id="btn_Search" type="image" src="/Images/buttons/chaXun.gif" />
                        &nbsp;
                        <input id="btnChongzhi" type="image" src="/Images/buttons/ChongZhiChaXun.gif" />--%>
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
    <%--<UC:EasyUIGrid ID="UCEasyUIDataGrid" runat="server" />--%>
    <UC:ButtonGrid ID="UCEasyUIDataGrid" runat="server" />
    <!--弹出框-->
    <div id="ui_AddButton" class="easyui-window" title="新增" style="width: 400px; height: 250px;"
        toolbar="#dlg-toolbar" resizable="true" closed="true"  >
        <iframe id="view_AddButton" scrolling="yes"  width="100%"  frameborder="0" height="200px" ></iframe>
    </div>
    <div id="ui_EditButton" class="easyui-dialog" title="编辑" style="width: 400px; height: 250px;"
        toolbar="#dlg-toolbar" resizable="true" closed="true"  >
        <iframe id="view_EditButton" scrolling="yes"  width="100%"  frameborder="0" height="200px" ></iframe>
    </div>
</body>
</html>
