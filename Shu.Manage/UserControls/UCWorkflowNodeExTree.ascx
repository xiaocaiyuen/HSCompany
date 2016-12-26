<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCWorkflowNodeExTree.ascx.cs" Inherits="Shu.Manage.UserControls.UCWorkflowNodeExTree" %>
<script type="text/javascript">
    window.onload = function () {
        WorkFlowNodeTree();
    }

    function WorkFlowNodeTree() {
        $('#tt2').tree({
            loadMsg: '加载中,请等候...',
            url: '/Handler/WorkflowNodeExHandler.ashx?active=Tree&id=<%=Request["id"] %>',
            //                        onDblClick: function (node) {
            //                            treeSelectd(node.id, node.text);
            //                        },
            onClick: function (node) {
                //                if (node.state == "open") {
                //                    collapse(node);
                //                }
                //                if (node.state == "closed") {
                //                    expand(node);
                //                }
                if ($('#tt2').tree('getParent', node.target)) {//判断是否是叶子节点
                    treeSelectd(node.id, node.text);
                }
                else {
                    alert('不是流程步骤');
                }
            },
            onContextMenu: function (e, node) {
                e.preventDefault();
                $(this).tree('select', node.target);
                if ($(this).tree('getParent', node.target)) {//判断是否是叶子节点
                    $('#mm').menu('show', {
                        left: e.pageX,
                        top: e.pageY
                    });
                }
            }
        });
        $('#loading-mask').fadeOut();
    }


    //    function append() {
    //        var t = $('#tt2');
    //        var node = t.tree('getSelected');
    //        t.tree('append', {
    //            parent: (node ? node.target : null),
    //            data: [{
    //                text: 'new item1'
    //            }, {
    //                text: 'new item2'
    //            }]
    //        });
    //    }
    function removeit() {
        var node = $('#tt2').tree('getSelected');
        var id = node.id;
        var noCache = Date();
        $.getJSON("/Handler/WorkflowNodeExHandler.ashx?active=Delete&id=" + id + "", { "noCache": noCache }, function (json) {
            $.messager.alert("系统提示", json.data, "info");
            $('#tt2').tree('remove', node.target);
        });
    }

    function collapse() {
        var node = $('#tt2').tree('getSelected');
        $('#tt2').tree('collapse', node.target);
    }
    function expand() {
        var node = $('#tt2').tree('getSelected');
        $('#tt2').tree('expand', node.target);
    }

    function windowOpen() {
        var win = $('#window1').window({
            left: 20,
            top: 100
        });
        $('#WorkflowNodeConfigID').val('');
        win.window('open');

    }

    function Open() {
        windowOpen();
        var noCache = Date();
        var id = $('#WorkflowNodeConfig_TasksInstanceID').val();
        $.getJSON("/Handler/WorkflowNodeExHandler.ashx?active=sort&id=" + id + "", { "noCache": noCache }, function (json) {
            $('#txtWorkflowNodeConfig_Setp').val(json.sort);
        });
    }

    var nodeFontStyle = "<a style='color:{1}'>{0}</a>";
    function Travel(treeId) {//参数为树的ID，注意不要添加#
        var roots = $('#' + treeId + '').tree('getRoots'), children, i, j;
        for (i = 0; i < roots.length; i++) {
            findChildren(treeId, roots[i]);
        }
    }

    function findChildren(treeId, childrenRoot) {
        //debugger
        var children, j;
        children = $('#' + treeId + '').tree('getChildren', childrenRoot.target);
        for (j = 0; j < children.length; j++) {
            if (children[j].text.toString().indexOf($('#txtSearch').val()) >= 0) {
                //$('#' + treeId + '').tree('select', children[j].target);
                if (children[j].text.toString().indexOf("style") >= 0) {
                    var s = children[j].text.toString().replace(/[^\u4e00-\u9fa5]/gi, "");
                    children[j].text = s.replace($('#txtSearch').val(), nodeFontStyle.replace("{0}", $('#txtSearch').val()).replace("{1}", "red"));
                }
                else {
                    children[j].text = children[j].text.replace($('#txtSearch').val(), nodeFontStyle.replace("{0}", $('#txtSearch').val()).replace("{1}", "red"));
                }
                $('#' + treeId + '').tree("update", children[j]);
                expand(childrenRoot);
                //break;
            }
            else {
                //debugger

                if (children[j].text.toString().indexOf("style") >= 0) {
                    var s = children[j].text.toString().replace(/[^\u4e00-\u9fa5]/gi, "");
                    children[j].text = nodeFontStyle.replace("{0}", s).replace("{1}", "Blank");
                }
                else {
                    children[j].text = nodeFontStyle.replace("{0}", children[j].text).replace("{1}", "Blank");
                }
                $('#' + treeId + '').tree("update", children[j]);
            }
            findChildren(treeId, children[j]);
        }
    }


    //关闭窗体
    function clearForm() {
        $('#window1').window('close');
    }

    //数据操作
    function AddSave() {
        //编辑操作
        var WorkflowNodeConfig_Name = escape($('#txtWorkflowNodeConfig_Name').val()); //标题
        var WorkflowNodeConfig_Setp = $('#txtWorkflowNodeConfig_Setp').val();
        var DesktopID = $('#WorkflowNodeConfigID').val();
        var id = $('#WorkflowNodeConfig_TasksInstanceID').val();
        var josnUrl = "";
        if (DesktopID == "") {
            var noCache = Date();
            $.getJSON("/Handler/WorkflowNodeExHandler.ashx?active=Add&id=" + id + "&WorkflowNodeConfig_Name=" + WorkflowNodeConfig_Name + "&WorkflowNodeConfig_Setp=" + WorkflowNodeConfig_Setp + "", { "noCache": noCache }, function (json) {
                $.messager.alert("系统提示", json.data, "info");
                $('#tt2').tree('reload', json.target);
            });
        }
        else {
            var noCache = Date();
            $.getJSON("/Handler/WorkflowNodeExHandler.ashx?active=Update&id=" + DesktopID + "&WorkflowNodeConfig_Name=" + WorkflowNodeConfig_Name + "&WorkflowNodeConfig_Setp=" + WorkflowNodeConfig_Setp + "", { "noCache": noCache }, function (json) {
                $.messager.alert("系统提示", json.data, "error");
                var node = $('#tt2').tree('getSelected');
                if (node) {
                    $('#tt2').tree('update', {
                        target: node.target,
                        text: json.text
                    });
                }
            });
        }
        $('#window1').window('close');
    }

    function reload() {
        var node = $('#tt2').tree('getSelected');
        //        if (node) {
        //            $('#tt2').tree('reload', node.target);
        //        }
        //        else {
        $('#tt2').tree('reload');
        //        }
    }

    function edite() {
        windowOpen();
        var node = $('#tt2').tree('getSelected');
        $('#txtWorkflowNodeConfig_Name').val(node.text);
        $('#txtWorkflowNodeConfig_Setp').val(node.attributes);
        $('#WorkflowNodeConfigID').val(node.id);
    }


    function up() {
        var node = $('#tt2').tree('getSelected');
        if ($('#tt2').tree('getParent', node.target)) {//判断是否是叶子节点
            var sort = node.attributes;
            var DesktopID = node.id;
            var id = $('#WorkflowNodeConfig_TasksInstanceID').val();
            var noCache = Date();
            $.getJSON("/Handler/WorkflowNodeExHandler.ashx?active=up&id=" + id + "&parid=" + DesktopID + "&sort=" + sort + "", { "noCache": noCache }, function (json) {
                if (json.data == "0") {
                    $.messager.alert("系统提示", "错误", "info");
                }
                else {
                    reload();
                }
            });
        }
        else {
            alert('不是流程步骤');
        }
    }

    function down() {
        var node = $('#tt2').tree('getSelected');
        if ($('#tt2').tree('getParent', node.target)) {//判断是否是叶子节点
            var sort = node.attributes;
            var DesktopID = node.id;
            var id = $('#WorkflowNodeConfig_TasksInstanceID').val();
            var noCache = Date();
            $.getJSON("/Handler/WorkflowNodeExHandler.ashx?active=down&id=" + id + "&parid=" + DesktopID + "&sort=" + sort + "", { "noCache": noCache }, function (json) {
                if (json.data == "0") {
                    $.messager.alert("系统提示", "错误", "info");
                }
                else {
                    reload();
                }
            });
        }
        else {
            alert('不是流程步骤');
        }
    }
</script>
<input id="WorkflowNodeConfigID" type="hidden" value="" /><!--唯一编号-->
<input id="WorkflowNodeConfig_TasksInstanceID" type="hidden" value="<%=Request["id"] %>" /><!--唯一编号-->
<div style="margin-top: 5px;">
    <% BasePage bg = new BasePage();
        string rolename = bg.CurrUserInfo().RoleName;
        //bool SysAdminRoleNameOK = rolename.Contains(Constant.SysAdminRoleName);
        bool SuperAdminRoleNameOK = rolename.Contains(Constant.SuperAdminRoleName);
    %>
    <%  if (SuperAdminRoleNameOK)
        {%>
    <a id="btn" href="javascript:Open();" class="easyui-linkbutton" data-options="iconCls:'icon-add'">添加步骤</a>
    <%} %>
    <%--<a href="javascript:up()">
            <img src="/Scripts/UI/themes/icons/arrow_up.png" /></a> <a href="javascript:down()">
                <img src="/Scripts/UI/themes/icons/arrow_down.png" /></a>--%>
</div>
<div id="loading-mask" style="position: absolute; top: 0px; left: 0px; width: 100%; text-align: center; height: 100%; background: #D2E0F2; z-index: 20000">
    <div id="pageloading" style="position: absolute; top: 50%; left: 50%; margin: -120px 0px 0px -120px; text-align: center; border: 2px solid #8DB2E3; width: 200px; height: 40px; font-size: 14px; padding: 10px; font-weight: bold; background: #fff; color: #15428B;">
        <img src="../../Images/main/loader.gif" align="absmiddle" />
        正在加载中,请稍候...
    </div>
</div>
<div>
    <ul id="tt2">
    </ul>
</div>
<div id="mm" class="easyui-menu" style="width: 120px;">


    <%  if (SuperAdminRoleNameOK)
        {%>
    <div onclick="Open()" data-options="iconCls:'icon-add'">
        添加
    </div>
    <%} %>
    <div onclick="edite()" data-options="iconCls:'icon-edit'">
        重命名
    </div>
    <%  if (SuperAdminRoleNameOK)
        {%>
    <div onclick="removeit()" data-options="iconCls:'icon-remove'">
        删除
    </div>
    <%} %>
    <%--<div class="menu-sep">
    </div>
    <div onclick="up()" data-options="iconCls:'icon-up'">
        上移</div>
    <div onclick="down()" data-options="iconCls:'icon-down'">
        下移</div>--%>
    <%--<div class="menu-sep">
    </div>
    <div onclick="expand()">
        展开</div>
    <div onclick="collapse()">
        闭合</div>--%>
</div>
<%--弹出框--%>
<div id="window1" class="easyui-window" title="新增" style="width: 270px; height: 130px;"
    toolbar="#dlg-toolbar" resizable="true" closed="true">
    <div id="divCont">
        <table width="100%">
            <tr>
                <td style="text-align: right; width: 100px;">步骤名称：
                </td>
                <td>
                    <input class="easyui-validatebox" class="input4" type="text" id="txtWorkflowNodeConfig_Name"
                        name="txtWorkflowNodeConfig_Name" data-options="required:true"></input>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">步骤序号：
                </td>
                <td>
                    <input class="easyui-numberbox easyui-validatebox" class="input4" type="text" id="txtWorkflowNodeConfig_Setp"
                        name="txtWorkflowNodeConfig_Setp" data-options="required:true"></input>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center;">
                    <a href="javascript:void(0)" class="easyui-linkbutton" onclick="AddSave();" id="hf">保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" onclick="clearForm()">取消</a>
                </td>
            </tr>
        </table>
    </div>
</div>
