<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PhotoGallery.aspx.cs" Inherits="Shu.Manage.PhotoGallery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Styles/default.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-2.0.0.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function imgreturn(url) {
            parent.GetImgValue(url);
        }
    </script>
</head>
<body>
    <div>
        <%foreach (var img in imagename)
          { %>
        <a href="javascript:void(0)" onclick="imgreturn('<%=img %>')">
            <img src="<%=img %>" />
        </a>
        <%} %>
    </div>
</body>
</html>
