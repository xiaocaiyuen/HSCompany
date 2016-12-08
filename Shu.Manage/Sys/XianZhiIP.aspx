<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XianZhiIP.aspx.cs" Inherits="YDT.Web.Manage.Sys.XianZhiIP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <link href="/Scripts/UI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/List.css" rel="stylesheet" type="text/css" />
     <script src="/Scripts/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="/Scripts/UI/jquery.easyui.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function CheckData() {
            var reg = /^((25[0-5]|2[0-4]\d|[01]?\d\d?)($|(?!\.$)\.)){4}$/;
            if (!reg.test($("#txtkaishi").val())) {
                alert("请在开始IP中填写正确IP地址！！ "); return false;
            }
            if (!reg.test($("#txtjieshu").val())) {
                alert("请在结束IP中填写正确IP地址！！ "); return false;
            }
            return true;
        }
       
        function coed(type) {

            window.parent.closed(type);

        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
            <div id="divCont" runat="server">
               
                <table width="100%" style="height: 160px;">
                    <tr>
                        <td style="text-align: right; width: 100px;">
                            开始IP<font color="red">*</font>
                        </td>
                        <td>
                           
                           
                           <asp:TextBox ID="txtkaishi" CssClass="validate[required]" runat="server" MaxLength="50"  Height="20px" Width="180px"></asp:TextBox>
                        </td>
                    </tr>
               
                     <tr>
                        <td style="text-align: right;">
                            结束IP<font color="red">*</font>
                        </td>
                        <td>
                            <asp:TextBox ID="txtjieshu" CssClass="validate[required]" runat="server" MaxLength="50" Height="20px" Width="180px"></asp:TextBox>
                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:ImageButton ImageUrl="~/Images/buttons/baocun.gif" runat="server" ID="btnSave"
                                OnClientClick="return CheckData()" OnClick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
            </div>
    </form>
</body>
</html>
