<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkflowConfigureEx.aspx.cs" Inherits="Shu.Manage.Workflow.WorkflowConfigureEx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Styles/table.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/List.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-2.0.0.min.js"></script>
    <link href="/Content/themes/bootstrap/easyui.css" rel="stylesheet" />
    <link href="/Content/themes/icon.css" rel="stylesheet" />
    <script type="text/javascript" src="/Scripts/jquery.easyui-1.4.5.min.js" charset="utf-8"></script>
    <script src="/Scripts/jquery.datagrid.js"></script>
    <script type="text/javascript" src="/Scripts/locale/easyui-lang-zh_CN.js" charset="utf-8"></script>
    <script src="/Scripts/DatePicker/WdatePicker.js" type="text/javascript"></script>
   <%-- <link href="/Styles/validationEngine.jquery.css" rel="stylesheet" type="text/css"
        charset="utf-8" />
    <script src="/Scripts/Validate/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="/Scripts/Validate/jquery.validationEngine-cn.js" type="text/javascript"
        charset="gb2312"></script>
    <script src="/Scripts/Validate/FormVaildate.js" type="text/javascript"></script>--%>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script src="/Scripts/Workflow/jiaose.js" type="text/javascript"></script>
    <script src="/Scripts/Workflow/WorkflowNodeRiskPoint.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ShowSelect() {
            var strsrc = $("#ucviews").attr("src");
            var drpval = $('#drpWorkflowNodeConfig_AuditMode').val();
            var url = "";
            if (drpval == "0") {
                url = '/Popup/Win_EasyUISelectRole.aspx?perseecharge=all&depseecharge=3';
            } else {
                url = '/Popup/Win_EasyUISelectUser.aspx?perseecharge=all&depseecharge=3';
            }
            $("#ucviews").attr("src", url);
            $('#uctextselects').dialog('open');
            $('#ucselects').show();
        }

        function ShowWarnIndex(code) {
            var url = '/Workflow/Matter_easyUISelect.aspx?code=' + code;

            $("#ifmWarnIndex").attr("src", url);
            $('#ucWarnIndex').dialog('open');
        }

        function Tijiao() {
            $('#hid_NodeConfigEx_DataMappingID').val($("#ddlNodeConfigEx_DataMappingID").combobox("getValues"));
            add_gwzq();
        }

        function ddlMechanismChange(obj) {
            if (obj.value == "2") {
                $("#divAutoOption").show();
            }
            else {
                $("#divAutoOption").hide();
            }

        }

        $(document).ready(function () {
            if ($("#ddlMechanism").val() == "2") {
                $("#divAutoOption").show();
                $("input[name='radioAutoOption'][value='" + $("#hidAutoOption").val() + "']").attr("checked", true);
            }
            else {
                $("#divAutoOption").hide();
            }

            //Loadcombobox();

            //$('#ddlNodeConfigEx_DataMappingID').combobox('setValues', $('#hid_NodeConfigEx_DataMappingID').val().split(','));
        });

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
            var data1 = [{ id: "", text: "" }].concat(data);//将‘所有’设置为第一个选项;

            var $test = $("#ddlNodeConfigEx_DataMappingID");

            $test.combobox({
                data: data1,
                valueField: 'id',
                textField: 'text',
                editable: true,
                multiple: true,
                width: 155,
                onSelect: function (r) {
                    //data = $test.combobox('getData');
                    //if (r.id == "") {//当选的是‘所有’这个选项
                    //    $test.combobox("setValues", dataStr1).combobox("setText", '所有');//
                    //} else {
                    //    var valArr = $test.combobox("getValues");
                    //    valArr.sort();//将值由小到大排序 以保持一致
                    //    if (valArr.join(',') == dataStr1.join(',') || valArr.join(',') == dataStr1.join(',')) {
                    //        $test.combobox("setValues", data).combobox("setText", '所有');//
                    //    }
                    //}
                    $('#hid_NodeConfigEx_DataMappingID').val($test.combobox("getValues"));
                },
                onUnselect: function (r) {
                    //if (r.id == '') {//当取消选择的是‘所有’这个选项
                    //    $test.combobox("setValues", []).combobox("setText", '');
                    //} else {
                    //    var valArr = $test.combobox("getValues");
                    //    if (valArr[0] == "") {
                    //        valArr.shift();
                    //        $test.combobox("setValues", valArr);
                    //    }
                    //}
                },
                filter: function (q, row) {
                    var opts = $(this).combobox('options');
                    return row[opts.textField].toLowerCase().indexOf(q.toLowerCase()) >= 0; // 同一转换成小写做比较，==0匹配首位，>=0匹配所有
                }
            });
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hid_DepCode" runat="server" />
        <asp:HiddenField ID="hid_GBManage" runat="server" />
        <asp:HiddenField ID="hid_NodeConfigEx_DataMappingID" runat="server" />
        <div>
            <table width="96%" class="tab" border="0" cellpadding="1" cellspacing="1">
                <tr>
                    <th width="20%">节点标识
                    </th>
                    <td width="30%" colspan="3">
                        <asp:DropDownList ID="ddlNodeSign" runat="server" Style="width: 150px;">
                            <asp:ListItem Text="开始" Value="START" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="过程" Value="PROCESS"></asp:ListItem>
                            <asp:ListItem Text="结束" Value="END"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th width="20%">步骤名称
                    </th>
                    <td width="30%">
                        <asp:TextBox ID="txtWorkflowNodeConfig_Name" CssClass="input4" runat="server"></asp:TextBox>
                    </td>
                    <th width="20%">步骤序号
                    </th>
                    <td width="30%">
                        <asp:TextBox ID="txtWorkflowNodeConfig_Setp" CssClass="input4" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th width="20%">步骤审核方式
                    </th>
                    <td width="30%">
                        <asp:DropDownList ID="drpWorkflowNodeConfig_AuditMode" runat="server" Style="width: 150px;">
                            <asp:ListItem Text="角色" Value="0"></asp:ListItem>
                            <%--<asp:ListItem Text="用户" Value="1"></asp:ListItem>--%>
                        </asp:DropDownList>
                    </td>
                    <th width="20%">步骤审核对象
                    </th>
                    <td width="30%">
                        <%--<asp:TextBox ID="txtWorkflowNodeConfig_AuditObjectID" CssClass="input4" runat="server"></asp:TextBox>--%>
                        <div id="div2">
                            <asp:TextBox ID="txt_Dep" runat="server" CssClass="input4" contentEditable="false"
                                Style="width: 100px; border: 1px solid #88BBE2; width: 150px;" ReadOnly="True"></asp:TextBox>
                            <span id="ucuserSelectSpan">
                                <img id="Img1" alt='选择' src="/Images/uc/search.gif" style="width: 19px; height: 19px; cursor: pointer; padding-top: 2px;"
                                    onclick="ShowSelect()" />
                            </span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <th>审批机制
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlMechanism" runat="server" Style="width: 150px;" onchange="ddlMechanismChange(this)">
                            <asp:ListItem Text="单线模式" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="人工分单模式" Value="1"></asp:ListItem>
                            <asp:ListItem Text="自动分单模式" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:HiddenField ID="hidAutoOption" runat="server" />
                    </td>
                    <td colspan="2">
                        <div id="divAutoOption" style="display: none;">
                            <input type="radio" name="radioAutoOption" value="1" />按区域或层级关系分配
                        <input type="radio" name="radioAutoOption" value="2" />按余量分配
                        <input type="radio" name="radioAutoOption" value="3" />按剩余单量分配
                        </div>
                    </td>
                </tr>
                <%--<tr>
                    <th>合同
                    </th>
                    <td>
                        <asp:CheckBox ID="ckoNodeConfigEx_IsDisplayContract" runat="server" Text="是否显示合同" />
                    </td>
                    <th>资料清单
                    </th>
                    <td>
                        <select id="ddlNodeConfigEx_DataMappingID" class="easyui-combobox validate[required]" name="ddlNodeConfigEx_DataMappingID"></select>
                    </td>
                </tr>--%>
            </table>
            <table width="96%" border="0" cellspacing="0" cellpadding="0" class="for">
                <tr>
                    <th colspan="7">
                        <asp:ImageButton ID="btn_Tijiao" runat="server" ImageUrl="~/Images/buttons/tijiao.gif"
                            Style="cursor: pointer; height: 24px; width: 50px;" OnClick="btn_Tijiao_Click"
                            OnClientClick="Tijiao();" />
                        <a id="FanHui" href="javascript:window.parent.Return();">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/buttons/fanHui.gif" Style="cursor: pointer; height: 24px; width: 50px;" /></a>
                    </th>
                </tr>
            </table>
        </div>

        <div id="ucselects" style="display: none;">
            <!--弹出框-->
            <div id="uctextselects" class="easyui-dialog" title="选择" style="width: 280px; height: 400px;"
                toolbar="#dlg-toolbar" resizable="true" closed="true">
                <iframe id="ucviews" scrolling="yes" width="100%" frameborder="0" height="350" style="overflow-x: hidden"></iframe>
            </div>
        </div>
    </form>
</body>
</html>
