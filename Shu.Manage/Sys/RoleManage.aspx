<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleManage.aspx.cs" Inherits="Shu.Manage.Sys.RoleManage" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Styles/table.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-2.0.0.min.js"></script>
    <link href="/Content/themes/bootstrap/easyui.css" rel="stylesheet" />
    <link href="/Content/themes/icon.css" rel="stylesheet" />
    <script type="text/javascript" src="/Scripts/jquery.easyui-1.4.5.min.js" charset="utf-8"></script>
    <script type="text/javascript" src="/Scripts/locale/easyui-lang-zh_CN.js" charset="utf-8"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>

    <%--<link href="/Styles/validationEngine.jquery.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/Validate/2.6.2Ciaoca/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="/Scripts/Validate/2.6.2Ciaoca/jquery.validationEngine-zh_CN.js" type="text/javascript"></script>
    <script src="/Scripts/Validate/2.6.2Ciaoca/jquery.validationEngine.min.js" type="text/javascript"></script>--%>

    <script type="text/javascript">
        var data = null;
        $(document).ready(function () {
            //$("#form1").validationEngine('attach', { promptPosition: "topRight", validationEventTriggers: "keyup blur focus" });

            Loadcombobox();

            //$('#ddlRoleDataMapping_Name').combobox('setValues', $('#hid_RoleDataMapping_Name').val().split(','));
        });
        var roleID = request("id")

        var menuRole = getMenuRole();

        function getMenuRole() {

            var temp = "";

            jQuery.ajax({
                type: "post",
                async: false,
                url: "/Handler/RoleHandler.ashx?id=" + roleID,
                data: {},
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                cache: false,
                success: function (json) {
                    temp = json;

                },
                error: function (err) {
                    alert(err);
                }
            });

            return temp;

        }

        function firstCheckBox(value, rowData, rowIndex) {

            var html = "";

            var isCode = false;

            if (roleID != "") {

                for (var i = 0; i < menuRole.length; i++) {

                    if (menuRole[i].RolePurview_MenuCode == value) {

                        isCode = true;
                        html = "<input type='checkbox' id='code_" + value + "' checked='checked' onclick=\"selectAll(this,'" + value + "')\"/>";
                        break;
                    }
                }
            }

            if (!isCode) {

                html = "<input type='checkbox' id='code_" + value + "' onclick=\"selectAll(this,'" + value + "')\"/>";
            }

            return html;

        }

        function selectAll(chk, menucode) {

            var id = $(chk).attr("id");

            if ($(chk).prop("checked") == true) {

                while (menucode.length >= 3) {

                    menucode = menucode.substring(0, (menucode.length - 3));

                    document.getElementById('code_' + menucode).checked = true;
                }

                $('input[id^=' + id + ']').prop('checked', true);
            }
            else {
                $('input[id^=' + id + ']').removeAttr('checked');
            }
        }

        function ShowCheckBox(value, rowData, rowIndex) {

            if (value) {

                var chkvalue = value.toString().split(',')

                var html = "";

                for (var i = 0; i < chkvalue.length; i++) {

                    var isRole = false;

                    if (roleID != "") {

                        for (var j = 0; j < menuRole.length; j++) {

                            if (menuRole[j].RolePurview_MenuCode == rowData.id) {

                                if (menuRole[j].RolePurview_OperatePurview != null) {

                                    if (menuRole[j].RolePurview_OperatePurview.indexOf(chkvalue[i]) >= 0) {
                                        html += '&nbsp;<input type="checkbox" id="optcode_' + rowData.id + '" checked="checked" value="' + chkvalue[i] + '">' + chkvalue[i] + ''

                                        isRole = true;

                                        break;
                                    }
                                }

                            }

                        }
                    }

                    if (!isRole) {

                        html += '&nbsp;<input type="checkbox" id="optcode_' + rowData.id + '" value="' + chkvalue[i] + '">' + chkvalue[i] + '';
                    }
                }

                return html;
            }
        }

        function ShowSeeCheckBox(value, rowData, rowIndex) {


            if (value) {

                var chkvalue = value.toString().split('#')

                var html = "";
                html += "&nbsp;<a onclick=\"ShowWin('" + rowData.id + "')\">维护查看范围</a>";
                return html;
            }

        }



        function save() {

            var menuCode = new Array();

            var saveData = "";

            $('input[id^=code_]:checked').each(function (index) {

                menuCode[index] = $(this).attr('id').split('_')[1];
            })

            for (var i = 0; i < menuCode.length; i++) {

                var name = "optcode_" + menuCode[i];
                var names = "See_" + menuCode[i];

                saveData += menuCode[i] + "_"

                $('input[id=' + name + ']:checked').each(function () {

                    saveData += $(this).attr('value') + ","
                })

                if (saveData.lastIndexOf(',') == saveData.length - 1) {

                    saveData = saveData.substring(0, saveData.length - 1);
                }


                saveData += "_";
                $('input[id=' + names + ']:checked').each(function () {

                    saveData += $(this).attr('value') + ","
                })
                if (saveData.lastIndexOf(',') == saveData.length - 1) {

                    saveData = saveData.substring(0, saveData.length - 1);
                }


                if (i < saveData.length - 1) {

                    saveData += "|";

                }

            }

            $("#hid_Save").val(saveData);

        }



        function ShowWin(menucode) {
            $('#hid_SelectedMenuCode').val(menucode);

            $('#tt2').tree({
                checkbox: true,
                loadMsg: '加载中,请等候...',

                url: '/Handler/RoleHandler.ashx?method=DepTree&pid=0&roleID=' + roleID + '&MenuCode=' + menucode,

                onClick: function (node) {
                    if (node.state == "open") {
                        collapse(node);
                    }
                    if (node.state == "closed") {
                        expand(node);
                    }
                }
            });
            $('#loading-mask').fadeOut();

            $.get("/Handler/RoleHandler.ashx?method=CheckSeeCharge", { roleId: roleID, menucodes: menucode }, function (data) {
                $("#LitCheckSee").html(data.split('#')[0]);
                $("#lblType").html(data.split('#')[1]);

            });

            $('#w').window('open');
        }

        function collapse(node) {
            $('#tt2').tree('collapse', node.target);
        }
        function expand(node) {
            $('#tt2').tree('expand', node.target);
        }

        //获取选中的部门ID
        function getCheckedDepID() {
            var nodes = $('#tt2').tree('getChecked');
            var s = "";
            for (var i = 0; i < nodes.length; i++) {
                if (nodes[i].id != "001") {
                    if (s != "") s += ",";
                    s += nodes[i].id;
                }
            }
            return s;
        }

        function AddSave() {
            var s = getCheckedDepID()

            var saveData = "";

            $('input[id^=See_]:checked').each(function (index) {
                saveData += $(this).val() + ",";
            })
            if (saveData.length > 0) {
                saveData = saveData.substring(0, saveData.length - 1);
            }
            saveData = saveData;
            var ddl = document.getElementById("ddlType");
            var index = ddl.selectedIndex;

            var Value = ddl.options[index].value;
            var code = $('#hid_SelectedMenuCode').val();
            //debugger
            var showType = $("#ddlShowType").val();
            //alert(showType)
            //$('#hid_RoleDataMapping_Name').val($("#ddlRoleDataMapping_Name").combobox("getValues"));

            $.get("/Handler/RoleHandler.ashx?method=SaveDep", { code: s, roleId: roleID, menucode: code, seeCharge: saveData, type: Value, showType: showType }, function (data) {
                if (data == "1") {
                    //$("#code_" + code).attr("checked", "checked");
                    $("#code_" + code).prop("checked", true);
                }


            });

            clearForm();
        }


        function clearForm() {
            $('#w').window('close');
            $('#hid_SelectedMenuCode').val("");
            $("#lblType").html("");
            $("#LitCheckSee").html("");
        }

        function Loadcombobox() {
            $.ajax({
                type: 'POST',
                dataType: 'json',
                async: false,
                url: '/Handler/AsynDeptTree.ashx?Option=DataMapping',
                success: function (msg) {
                    data = msg;
                }
            });
            //var data1 = [{ id: "", text: "所有" }].concat(data);//将‘所有’设置为第一个选项
            var data1 = [{ id: "", text: "" }].concat(data);//将‘所有’设置为第一个选项;
            //var dataStr = [],
            //    dataStr1 = [];
            //for (var i = 0; i < data1.length; i++) {
            //    if (i != 0) {
            //        dataStr.push(data1[i].id);
            //    }
            //    dataStr1.push(data1[i].id);
            //}


            //var $test = $("#ddlRoleDataMapping_Name");

            //$test.combobox({
            //    data: data1,
            //    valueField: 'id',
            //    textField: 'text',
            //    editable: true,
            //    multiple: true,
            //    width: 155,
            //    onSelect: function (r) {
            //        //data = $test.combobox('getData');
            //        //if (r.id == "") {//当选的是‘所有’这个选项
            //        //    $test.combobox("setValues", dataStr1).combobox("setText", '所有');//
            //        //} else {
            //        //    var valArr = $test.combobox("getValues");
            //        //    valArr.sort();//将值由小到大排序 以保持一致
            //        //    if (valArr.join(',') == dataStr1.join(',') || valArr.join(',') == dataStr1.join(',')) {
            //        //        $test.combobox("setValues", data).combobox("setText", '所有');//
            //        //    }
            //        //}
            //        $('#hid_RoleDataMapping_Name').val($test.combobox("getValues"));
            //    },
            //    onUnselect: function (r) {
            //        //if (r.id == '') {//当取消选择的是‘所有’这个选项
            //        //    $test.combobox("setValues", []).combobox("setText", '');
            //        //} else {
            //        //    var valArr = $test.combobox("getValues");
            //        //    if (valArr[0] == "") {
            //        //        valArr.shift();
            //        //        $test.combobox("setValues", valArr);
            //        //    }
            //        //}
            //    },
            //    filter: function (q, row) {
            //        var opts = $(this).combobox('options');
            //        return row[opts.textField].toLowerCase().indexOf(q.toLowerCase()) >= 0; // 同一转换成小写做比较，==0匹配首位，>=0匹配所有
            //    }
            //});
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hid_Save" runat="server" />
        <asp:HiddenField ID="hid_RoleID" runat="server" />
        <asp:HiddenField ID="hid_RoleName" runat="server" />
        <asp:HiddenField ID="hid_See" runat="server" />
        <asp:HiddenField ID="hid_SelectedMenuCode" runat="server" />
        <asp:HiddenField ID="hid_RoleDataMapping_Name" runat="server" />
        <div id="p1" class="easyui-panel" title="角色信息管理" data-options="collapsible:true">
            <table cellpadding="1" align="center" cellspacing="1" width="750" class="tab">
                <tr>
                    <th style="width: 100px;">角色名称<font color="red">*</font>
                    </th>
                    <td>

                        <asp:TextBox ID="txtRole_Name" runat="server" CssClass="validate[required,maxSize[15]]"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>排序<font color="red">*</font>
                    </th>
                    <td>
                        <asp:TextBox ID="txtRole_Sequence" runat="server" CssClass="validate[required,custom[onlyNumberSp]]"
                            Width="90px"></asp:TextBox>
                    </td>
                </tr>
                
                <tr>
                    <th>审批意见查看权限
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlShowType" runat="server">
                            <asp:ListItem Value="1">只能看到外部意见</asp:ListItem>
                            <asp:ListItem Value="2">内外部意见都能看到</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>备注
                    </th>
                    <td>
                        <asp:TextBox runat="server" ID="txtRole_Description" TextMode="MultiLine" Width="476px"
                            Height="62px"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td style="text-align: center" colspan="2">
                        <div id="divProc" style="display: none;">
                            <img src="/Images/main/loader.gif" align="absmiddle" />数据处理中,请稍后...
                        </div>
                        <div id="BtnDiv">
                            <asp:ImageButton runat="server" ID="btnSave" OnClientClick="return save();" OnClick="btnSave_Click" ImageUrl="~/Images/buttons/baocun.gif" />

                            &nbsp;
                <a id="FHA" href="RoleList.aspx">
                    <asp:Image ID="btn_FH" runat="server"
                        ImageUrl="~/Images/buttons/fanHui.gif" Style="height: 24px; width: 50px;" /></a>
                    </td>
                </tr>
            </table>
            <table id="tg" class="easyui-treegrid" style="height: 450px"
                data-options="
				iconCls: 'icon-ok',
				rownumbers: true,
				animate: true,
				collapsible: true,
				fitColumns: false,
				url: '/Handler/RoleHandler.ashx?method=load',
				method: 'get',
				idField: 'id',
				treeField: 'name',
				showFooter: true
                
			">
                <thead>
                    <tr>
                        <th data-options="field:'id',width:30,formatter:firstCheckBox"></th>
                        <th data-options="field:'name',width:210">菜单名称</th>
                        <th data-options="field:'Opt',width:550,formatter:ShowCheckBox">操作按钮</th>
                        <th data-options="field:'See',width:100,formatter:ShowSeeCheckBox">查看范围</th>
                    </tr>
                </thead>

            </table>
            <table cellpadding="10" align="center" cellspacing="1" width="750" class="tab">
                <tr>
                    <td style="text-align: center" colspan="2">
                        <div id="divProc" style="display: none;">
                            <img src="/Images/main/loader.gif" align="absmiddle" />数据处理中,请稍后...
                        </div>
                        <div id="BtnDiv">
                            <asp:ImageButton runat="server" ID="ImageButton1" OnClientClick="return save();" OnClick="btnSave_Click" ImageUrl="~/Images/buttons/baocun.gif" />

                            &nbsp;
                <a id="FHA" href="RoleList.aspx">
                    <asp:Image ID="Image1" runat="server"
                        ImageUrl="~/Images/buttons/fanHui.gif" Style="height: 24px; width: 50px;" /></a>
                    </td>
                </tr>
            </table>
            <div id="w"
                class="easyui-window"
                title="选择查看范围"
                data-options="modal:true,closed:true,iconCls:'icon-save'"
                style="width: 550px; height: 450px; padding: 10px; top: 290px;">
                <table>

                    <tr>
                        <td width="45%">

                            <div id="loading-mask" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%; background: #D2E0F2; z-index: 20000">
                                <div id="pageloading" style="position: absolute; top: 50%; left: 50%; margin: -120px 0px 0px -120px; text-align: center; border: 2px solid #8DB2E3; width: 200px; height: 40px; font-size: 14px; padding: 10px; font-weight: bold; background: #fff; color: #15428B;">
                                    <img src="../../Images/main/loader.gif" align="absmiddle" />
                                    正在加载中,请稍候...
                                </div>
                            </div>
                            <div>
                                <ul id="tt2">
                                </ul>
                            </div>

                        </td>
                        <td width="10%" style="vertical-align: top">

                            <asp:Label ID="lblType" runat="server"></asp:Label>


                        </td>
                        <td width="45%" style="vertical-align: top">
                            <asp:Label ID="LitCheckSee" runat="server"></asp:Label>

                        </td>
                    </tr>

                </table>
                <div style="text-align: center; padding: 5px">
                    <a href="javascript:void(0)" class="easyui-linkbutton" onclick="AddSave();">保存</a>
                    <a href="javascript:void(0)" class="easyui-linkbutton" onclick="clearForm()">取消</a>
                </div>
            </div>
        </div>
    </form>

</body>
</html>
