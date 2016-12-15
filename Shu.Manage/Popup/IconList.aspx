<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IconList.aspx.cs" Inherits="Shu.Manage.Popup.IconList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Styles/List.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/table.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-2.0.0.min.js"></script>
    <script type="text/javascript">
        $(function () {
            divresize();
            $(".divicons").click(function () {
                parent.Get_Menu_Img($(this).attr('title'));
            }).dblclick(function () {
                parent.OpenClose();
            });

        })
        /**自应表格高度**/
        function divresize() {
            resizeU();
            $(window).resize(resizeU);
            function resizeU() {
                $(".div-body").css("height", $(window).height() - 59);
            }
        }
    </script>
    <style type="text/css">
        .divicons {
            float: left;
            border: solid 1px #ccc;
            margin: 5px;
            padding: 5px;
            text-align: center;
            cursor: pointer;
        }

        .divicons:hover {
            color: #FFF;
            border: solid 1px #3399dd;
            background: #2288cc;
            filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#33bbee', endColorstr='#2288cc');
            background: linear-gradient(top, #33bbee, #2288cc);
            background: -moz-linear-gradient(top, #33bbee, #2288cc);
            background: -webkit-gradient(linear, 0% 0%, 0% 100%, from(#33bbee), to(#2288cc));
            text-shadow: -1px -1px 1px #1c6a9e;
            font-weight: bold;
        }
        /* 翻页-默认居中 */
        .m-page{margin:20px 0 0; text-align:center;line-height:32px;font-size:0;letter-spacing:-0.307em;*letter-spacing:normal;*word-spacing:-1px;word-wrap:normal;white-space:nowrap;color:#999;}
        .m-page a,.m-page i{display:inline-block;*display:inline;*zoom:1;vertical-align:top;padding:0 12px;margin-left:-1px;border:1px solid #ddd;font-size:12px;letter-spacing:normal;word-spacing:normal;text-shadow:0 1px #fff;background:#fff;-webkit-transition:background-color 0.3s;-moz-transition:background-color 0.3s;-ms-transition:background-color 0.3s;transition:background-color 0.3s;}
        .m-page a,.m-page a:hover{text-decoration:none;color:#39c;}
        .m-page a.first{margin-left:0;border-top-left-radius:5px;border-bottom-left-radius:5px;}
        .m-page a.last{margin-right:0;border-top-right-radius:5px;border-bottom-right-radius:5px;}
        .m-page a .pagearr{font-weight:bold;font-family:\5b8b\4f53;vertical-align:top;*vertical-align:middle;}
        .m-page a.pageprv .pagearr{margin-right:3px;}
        .m-page a.pagenxt .pagearr{margin-left:3px;}
        .m-page a:hover{background:#f5f5f5;}
        .m-page a:active{background:#f0f0f0;}
        .m-page a.z-crt,.m-page a.z-crt:hover,.m-page a.z-crt:active{cursor:default;color:#999;background:#f5f5f5;}
        .m-page a.z-dis,.m-page a.z-dis:hover,.m-page a.z-dis:active{cursor:default;color:#ccc;background:#fff;}
        .m-page span.currentPage{display:inline-block;*display:inline;*zoom:1;vertical-align:top;padding:0 12px;margin-left:-1px;border:1px solid #ddd;font-size:12px;letter-spacing:normal;word-spacing:normal;text-shadow:0 1px #fff;background:#f5f5f5;}
    </style>
</head>
<body>
    
    <div class="div-body">
        <%=strImg.ToString() %>
    </div>
    <!--分页-->
    <div class="clearfix"></div>
    <div class="m-page">
        <%=recordcount>0?pagehtml:""%>
    </div>
</body>
</html>
