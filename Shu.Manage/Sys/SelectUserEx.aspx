<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectUserEx.aspx.cs" Inherits="YDT.Web.Manage.Sys.SelectUserEx" %>

<%@ Register src="~/UserControls/UCExpAllDeptEasyUICheckBoxTree.ascx" tagname="UCExpAllDeptEasyUICheckBoxTree" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../../Scripts/jquery-1.8.0.min.js" type="text/javascript"></script>
     <script src="/Scripts/UI/jquery.easyui.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" charset="utf-8">
        function back() {
            alert("操作成功");
            var s = "分管部门";
            window.parent.SetCount("您已添加" + $("#hidCount").val() + "个" + s);
            //window.parent.CloseWin();
            return false;
        }
        function CloseWin() {
            var s = "分管部门";
            window.parent.SetCount("您已添加" + $("#hidCount").val() + "个" + s);
            window.parent.CloseWin();
        }

        //确定选择
        function OkSelect() {
            var depCodeStr = getCheckedDepID();
            if (depCodeStr == "") {
                alert("请选择部门");
            }
            else {
                  var noCache = Date();
                  $.getJSON("/Handler/UserChargeDep.ashx?UserID=" + $("#hidUserID").val() + "&depCodeStr=" + depCodeStr, { "noCache": noCache }, function (json) {
                      if (json.data != "") {
                          //alert(json.data)
                          var depArray = depCodeStr.split(',');
                          //alert("操作成功");
                          var s = "分管部门";
                          window.parent.SetCount("您已添加" + depArray.length + "个分管部门");
                          //$("#btnSure").click();
                          window.parent.$('#d2').dialog('close');
                         
                      }
                  });
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server"  style="margin: 0; padding: 0; overflow-x: hidden">
    <asp:HiddenField ID="hidCount" runat="server" Value="0" />
    <asp:HiddenField ID="hidUserID" runat="server" Value="0" />
    <asp:HiddenField ID="hidDepList" runat="server"  />
    <div style="display: block; z-index: 1; border-bottom: 1px solid #8CC3F9;
        height: 28px; text-align: left; font-size: 12px; padding-left: 0px; padding-top: 10px;
        color: Red;">
        <asp:Button runat="server" ID="btnSure" Text="确认所选" style=" display:none;" OnClick="btnSure_Click" />
        <input type="button" value="确认所选" onclick="OkSelect();" />
        &nbsp;<input type="button" value="关闭" onclick="CloseWin()" />
    </div>
    <div style="position: absolute; top: 45px; left: 10px; width: 1px;padding-top: 20px;">
        <uc1:UCExpAllDeptEasyUICheckBoxTree ID="UCExpAllDeptEasyUICheckBoxTree1" 
            runat="server" />
    </div>
    </form>
</body>
</html>

