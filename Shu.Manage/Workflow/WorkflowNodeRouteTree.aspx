<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkflowNodeRouteTree.aspx.cs" Inherits="Shu.Manage.Workflow.WorkflowNodeRouteTree" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Styles/default.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/UI/themes/bootstrap/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/UI/themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="/Scripts/UI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script src="/Scripts/UI/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="/Scripts/UI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/UI/themes/bootstrap/easyui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/UI/locale/easyui-lang-zh_CN.js" charset="utf-8"></script>
    <script type="text/javascript">
        function treeSelectd(id, text) {
            $('#chart').attr('src', 'WorkflowNodeRoute.aspx?id=' + id);
        }

        //详细页面
        function addTab(title, url, ico) {
            window.parent.addTab(title, url, '')
        }

        function RightLoad(depCode) {
            $('#chart').attr("src", "WorkflowNodeRoute.aspx?id=" + depCode + "");
        }

        function Return() {
            window.location.href = 'WorkflowNodeRouteTree.aspx';
        }
    </script>
    <script type="text/javascript">
        window.onload = function () {
            WorkFlowNodeTree();
        }

        function WorkFlowNodeTree() {
            $('#tt2').tree({
                loadMsg: '加载中,请等候...',
                url: '/Handler/WorkflowNodeRouteHandler.ashx?active=Tree',
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


        function Open() {
            $('#chart').attr("src", "WorkflowNodeRoute.aspx");
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
                    $.messager.alert("系统提示", json.text, "error");
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

        function removeit() {
            var node = $('#tt2').tree('getSelected');
            var id = node.id;
            var noCache = Date();
            $.getJSON("/Handler/WorkflowNodeRouteHandler.ashx?active=Delete&id=" + id + "", { "noCache": noCache }, function (json) {
                $.messager.alert("系统提示", json.data, "info");
                $('#tt2').tree('remove', node.target);
            });
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
</head>
<body class="easyui-layout" style="overflow-y: hidden;" fit="true" scroll="no">
    <form id="form1" runat="server">
        <div region="west" split="true" title="&nbsp;步骤维护:(鼠标点击节点步骤)" style="width: 210px;">
            <input id="WorkflowNodeConfigID" type="hidden" value="" /><!--唯一编号-->
            <input id="WorkflowNodeConfig_TasksInstanceID" type="hidden" value="" /><!--唯一编号-->
            <div style="margin-top: 5px;">
                <% BasePage bg = new BasePage();
                    string rolename = bg.CurrUserInfo().RoleName;
                    //bool SysAdminRoleNameOK = rolename.Contains(Constant.SysAdminRoleName);
                    bool SuperAdminRoleNameOK = rolename.Contains(Constant.SuperAdminRoleName);
                %>
                <%  if (SuperAdminRoleNameOK)
                    {%>
                <a id="btn" href="javascript:Open();" class="easyui-linkbutton" data-options="iconCls:'icon-add'">添加节点路由</a>
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
        </div>
        <div region="center" style="overflow-y: hidden; overflow-x: hidden">
            <iframe id="chart" width="100%" height="100%" scrolling="yes" frameborder="0" src=""></iframe>
        </div>

        <div id="mm" class="easyui-menu" style="width: 120px;">
            <div onclick="removeit()" data-options="iconCls:'icon-remove'">
                删除
            </div>
        </div>
    </form>
</body>
</html>
