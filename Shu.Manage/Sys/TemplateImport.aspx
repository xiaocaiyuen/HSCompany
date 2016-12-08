<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TemplateImport.aspx.cs" Inherits="YDT.Web.Manage.Sys.TemplateImport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/Scripts/jquery-1.8.0.min.js" type="text/javascript"></script>
  <%--  <link href="/Styles/table.css" rel="stylesheet" type="text/css" />--%>
    <script src="/Scripts/UI/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="/Scripts/UI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/UI/themes/bootstrap/easyui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function sb() {
            showConfirmMsg("上传模板后原模板将被覆盖，确定要上传吗？", function (r) {
                if (r) {
                    form1.submit();
                }
            });

            
        }
        function showConfirmMsg(msg, callBack) {
            $.messager.confirm("系统提示", msg, callBack);
        }
        function col() {

            window.parent.colsd();
        }
    </script>
</head>
<body>
    <form id="form1" method=post enctype="multipart/form-data" runat=server>
    <div>
        <table width="180px" border="0" cellpadding="10" cellspacing="1" style="font-size: 12px; font-style: normal; font-weight: normal; font-variant: normal; ">
            <tr>
                <th>
                    文&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;件：
                </th>
                <td>
                    <input id="File1" type="file" name="File1" />
                </td>
            </tr>
        </table>
        <div style="margin-top: 10px; text-align: center;">
            <a style="cursor: pointer;" class="easyui-linkbutton" onclick="sb()">确定</a>&nbsp;&nbsp;&nbsp;&nbsp;
            <a style="cursor: pointer;" class="easyui-linkbutton" onclick="col()">取消</a>&nbsp;&nbsp;&nbsp;&nbsp;</div>
    </div>
    </form>
</body>
</html>

