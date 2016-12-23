<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkflowConfigureManagerEx.aspx.cs" Inherits="Shu.Manage.Workflow.WorkflowConfigureManagerEx" %>

<%@ Register Src="~/UserControls/UCWorkflowNodeExTree.ascx" TagPrefix="UC" TagName="UCWorkflowNodeExTree" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Styles/default.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/UI/themes/bootstrap/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/UI/themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="/Scripts/UI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/common.js" type="text/javascript"></script>
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
    <form id="form1" runat="server">
    <div region="west" split="true" title="&nbsp;步骤维护:(鼠标点击节点步骤)" style="width: 210px;">
        <UC:UCWorkflowNodeExTree ID="UCWorkflowNodeExTree1" runat="server" />
    </div>
    <div region="center" style="overflow-y: hidden; overflow-x: hidden">
        <iframe id="chart" width="100%" height="100%" scrolling="yes" frameborder="0" src="WorkflowConfigureEx.aspx">
        </iframe>
    </div>
    </form>
</body>
</html>
