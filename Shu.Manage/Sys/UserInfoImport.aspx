<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfoImport.aspx.cs"
    Inherits="YDT.Web.Manage.Sys.UserInfoImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/Scripts/jquery-1.8.0.min.js" type="text/javascript"></script>
    <link href="/Styles/table.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/UI/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="/Scripts/UI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/UI/themes/bootstrap/easyui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function sb() {

            form1.submit();
        }
        function col() {

            window.parent.colsd();
        }
    </script>
</head>
<body>
    <form id="form1" method=post enctype="multipart/form-data" runat=server>
    <div>
        <table width="380px" border="0" cellpadding="10" cellspacing="1">
            <tr>
                <th>
                    文&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;件：
                </th>
                <td>
                    <input id="File1" type="file" name="File1" />
                </td>
            </tr>
            <tr>
                <th>
                    模板下载：
                </th>
                <td>
                    <a style="cursor: pointer; color: Red" href="/Files/Download.aspx?path=/Files/TemplateDownload/用户信息.xlsx&filename=用户信息模板.xlsx">
                        用户信息模板.xlsx</a>
                </td>
            </tr>
        </table>
        <div style="margin-top: 10px; text-align: right">
            <a style="cursor: pointer;" class="easyui-linkbutton" onclick="sb()">导入</a>&nbsp;&nbsp;&nbsp;&nbsp;
            <a style="cursor: pointer;" class="easyui-linkbutton" onclick="col()">取消</a>&nbsp;&nbsp;&nbsp;&nbsp;</div>
    </div>
    </form>
</body>
</html>
