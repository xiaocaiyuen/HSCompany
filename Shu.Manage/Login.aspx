<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Shu.Manage.Login" %>

<!DOCTYPE html>
<html lang="zh-CN" manifest="cache.manifest">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>和顺框架</title>
    <link rel="stylesheet" type="text/css" href="Styles/login/css/bootstrap-responsive.min.css" />
    <link rel="stylesheet" type="text/css" href="Styles/login/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="Styles/login/css/common.css" />
    <script src="/Scripts/jquery-2.0.0.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="Scripts/Login.js" type="text/javascript"></script>
    <%--<script type="text/javascript">
        $(document).ready(function () {

            ShowLog();
        });
        function ShowLog() {
            $.ajax({
                type: 'POST',
               // dataType: 'json',
                async: false,
                url: '/Handler/FileHandler.ashx?Option=beijing',
                success: function (da) {
                  
                    $("#img").attr("src", da);

                }
            });
        }
    </script>--%>
</head>
<body>
    <div id="myCarousel" class="carousel slide">
        <div class="carousel-inner">
            <div class="item active">
                <img src="Styles/login/img/examples/logo.jpg" alt="" id="img" />
                <div class="container" style="">
                   
                </div>
            </div>
        </div>
        <%--<a class="left carousel-control" href="#myCarousel" data-slide="prev">&lsaquo;</a>
        <a class="right carousel-control" href="#myCarousel" data-slide="next">&rsaquo;</a--%>>
    </div>
    <div class="container-login-bg" style="padding:10px;border: 2px solid #FFF;-moz-border-radius: 15px; -webkit-border-radius: 15px; border-radius:15px;">
    </div>
    <div class="container-login">
        <form id="form1" runat="server" class="form-signin">
            
            <h2 class="form-signin-heading" style="color: #777777; text-align:center;">用户登录</h2>
            <asp:TextBox ID="txtUser" runat="server" class="input-block-level" placeholder="用户名"></asp:TextBox>
            <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" class="input-block-level" placeholder="密码"></asp:TextBox>
            <label class="checkbox" style="color: #777777">
                <asp:CheckBox ID="ckbRememberLogin" runat="server" />
                <%--<input type="checkbox" value="remember-me" />--%>
            记住我
            </label>
            <asp:Button ID="BtnDl" runat="server" Text="登录"
                class="btn btn-large btn-primary" OnClientClick="return loginSubmit();" OnClick="BtnDl_Click" />
        </form>
    </div>
    <div class="container-marketing" style="font-size: 20px; text-align: center; color: #000000; font-weight:900;">
        &copy;
        <%=DateTime.Now.Year %>
        合肥和顺信息科技有限公司&middot;
    </div>

</body>
</html>

