<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkflowConfigureManagerEx.aspx.cs" Inherits="Shu.Manage.Workflow.WorkflowConfigureManagerEx" %>

<%@ Register Src="~/UserControls/UCWorkflowNodeExTree.ascx" TagPrefix="UC" TagName="UCWorkflowNodeExTree" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Styles/default.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-2.0.0.min.js"></script>
    <link href="/Content/themes/bootstrap/easyui.css" rel="stylesheet" />
    <link href="/Content/themes/icon.css" rel="stylesheet" />
    <script type="text/javascript" src="/Scripts/jquery.easyui-1.4.5.min.js" charset="utf-8"></script>
    <script type="text/javascript" src="/Scripts/locale/easyui-lang-zh_CN.js" charset="utf-8"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script type="text/javascript">
        function treeSelectd(id, text) {
            $('#chart').attr('src', 'WorkflowConfigureEx.aspx?id=' + id);
        }

        //详细页面
        function addTab(title, url, ico) {
            window.parent.addTab(title, url, '')
        }

        function RightLoad(depCode) {
            $('#chart').attr("src", "WorkflowConfigureEx.aspx?id=" + depCode + "");
        }

        function Return() {
            window.location.href = 'WorkflowTasksExList.aspx';
        }
    </script>
</head>
<body class="easyui-layout" style="overflow-y: hidden;" fit="true" scroll="no">
    <div region="west" split="true" title="&nbsp;步骤维护:(鼠标点击节点步骤)" style="width: 210px;">
        <UC:UCWorkflowNodeExTree ID="UCWorkflowNodeExTree1" runat="server" />
    </div>
    <div region="center" style="overflow-y: hidden; overflow-x: hidden">
        <iframe id="chart" width="100%" height="100%" scrolling="yes" frameborder="0" src="WorkflowConfigureEx.aspx"></iframe>
    </div>
</body>
</html>
