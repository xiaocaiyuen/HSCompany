<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Win_EasyUISelectUser.aspx.cs" Inherits="Shu.Manage.Popup.Win_EasyUISelectUser1" %>

<%@ Register src="~/UserControls/UCExpAllDeptUserEasyUISingleTree.ascx" tagname="UCExpAllDeptUserEasyUISingleTree" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/Scripts/jquery-2.0.0.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:UCExpAllDeptUserEasyUISingleTree ID="UCExpAllDeptUserEasyUISingleTree1" 
            runat="server" />
    </div>
    </form>
</body>
</html>
