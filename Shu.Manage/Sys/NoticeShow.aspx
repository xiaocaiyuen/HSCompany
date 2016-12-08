<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoticeShow.aspx.cs" Inherits="Shu.Manage.Sys.NoticeShow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Styles/table.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-2.0.0.min.js"></script>
    <script src="/Print/CheckActivX.js" type="text/javascript"></script>
    <object id="LODOP_OB" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA" width="0" height="0"> 
	    <embed id="LODOP_EM" type="application/x-print-lodop" width="0" height="0" pluginspage="install_lodop32.exe"></embed>
    </object>
    <script src="/Scripts/print.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="780" border="0" align="center" cellpadding="0" cellspacing="0" 
        style="border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #000000; margin-top:20px; font-size:16px;">
        <tr>
            <th>
                
                    <asp:Label ID="Notice_Title" runat="server"></asp:Label>
                    <br />
            </th>
        </tr>
    </table>
    <table width="780" class="tab" border="0" cellpadding="10" cellspacing="0" >
        <tr>
            <td width="780">
             <br />
               <div style=" margin-left:25px;">
               
               <samp id="sp"></samp>
                 <asp:TextBox ID="Notice_Context" runat="server"  TextMode="MultiLine" style=" display:none"  ></asp:TextBox>
<script type="text/javascript">
    window.onload = function () {
        $("#sp").html($("#Notice_Context").val());
    }
    
    </script>
            </div>
                <br />
                <br />
            </td>
        </tr>
    </table>
    <table width="780"  border="0" cellpadding="10" cellspacing="0" align="center" >
        <tr>
            <th width="100px" align="left">
            相关附件
                :</th>
             <td>
                <asp:Label ID="lblFj" runat="server" ></asp:Label>
            </td>
        </tr>
    </table>
    <table width="780" border="0" cellspacing="0" cellpadding="0" align="center" id="tabBtn">
        <tr>
            <td align="center">
            <br />
            <img alt="" src="/Images/buttons/dayins.gif" style="cursor: pointer;" onclick="return printview();" />
                <img src="/Images/buttons/win_guanbi.gif" style="cursor: pointer;" onclick="javascript:window.parent.Closetab()"
                    alt="" width="50" height="24" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
