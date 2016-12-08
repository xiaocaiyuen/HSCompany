<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserManager_T.aspx.cs" Inherits="YDT.Web.Manage.Sys.UserManager_T" %>
<%@ Register src="../../UserControls/UCExpAllDeptEasyUISingleTree.ascx" tagname="UCExpAllDeptEasyUISingleTree" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Styles/default.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/UI/themes/bootstrap/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/UI/themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="/Scripts/UI/jquery.easyui.min.js" type="text/javascript"></script>
     <script type="text/javascript">
         function treeSelectd(id, text) {
             $('#chart').attr('src', 'UserInfoList_T.aspx?depCode=' + id);
         }

         function RightLoad(depCode) {
             $('#chart').attr("src", "UserInfoList_T.aspx?depCode=" + depCode + "");
         }
    </script>
</head>
<body class="easyui-layout" style="overflow-y: hidden;"  fit="true"   scroll="no">
 <form id="form1" runat="server">
    <div region="west" split="true" title="&nbsp;部门列表" style="width: 210px;">
        <%--<iframe id="tree" width="100%" height="100%" scrolling="yes" frameborder="0" src="../../IFrame/IF_DepSelect.aspx">
        </iframe>--%>
         <uc1:UCExpAllDeptEasyUISingleTree ID="UCExpAllDeptEasyUISingleTree1" 
            runat="server" />
    </div>
    <div region="center" style="overflow-y: hidden; overflow-x: hidden">
        <iframe id="chart" width="100%" height="100%" scrolling="yes" frameborder="0" src="UserInfoList_T.aspx">
        </iframe>
    </div>
    </form>
</body>
</html>
