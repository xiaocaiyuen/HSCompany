<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfigSetting.aspx.cs" Inherits="Shu.Manage.Sys.Configsetting" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <style type="text/css">
        * {
            font-size: 12px;
            margin: 0 auto;
        }

        #tbSetting th {
            width: 25%;
            text-align: center;
        }

        #tbSetting td {
            text-align: center;
        }
    </style>
    <script src="/Scripts/jquery.linq.min.js"></script>

    <script type="text/javascript" src="/Scripts/jquery-2.0.0.min.js"></script>
    <link href="/Content/themes/bootstrap/easyui.css" rel="stylesheet" />
    <link href="/Content/themes/icon.css" rel="stylesheet" />
    <script type="text/javascript" src="/Scripts/jquery.easyui-1.4.5.min.js" charset="utf-8"></script>
    <script type="text/javascript" src="/Scripts/locale/easyui-lang-zh_CN.js" charset="utf-8"></script>

    <link href="/Styles/table.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/List.css" rel="stylesheet" type="text/css" />

    <link href="/Scripts/uploadify/uploadify.css" rel="stylesheet" />
    <script src="/Scripts/uploadify/jquery.uploadify.min.js"></script>
    <script type="text/javascript">
        var itemList;

        /* 页面初始化 */
        $(function () {
            $("#formConfig").validationEngine({ promptPosition: "centerRight", validationEventTriggers: "keyup blur" });
            EvaluateInit();
        });

        /* 系统参数初始化 */
        function EvaluateInit() {
            $.ajax({
                type: "get",
                url: "/Handler/ConfigSettingHandler.ashx?Method=GetAllConfig&t=" + Math.random(),
                data: {},
                dataType: "json",
                success: function (json) {
                    if (json != null) {
                        // 初始化界面
                        UIInit(json);
                        itemList = json;
                    }
                }
            });
        }

        /* 初始化系统参数 */
        function UIInit(json) {
            // 初始化表格头
            var str = "";
            str += "<tr>";
            str += "<th>参数名称</th>";
            str += "<th>关键字</th>";
            str += "<th>参数值</th>";
            str += "<th>备注</th>";
            str += "</tr>";
            for (var i = 0; i < json.length; i++) {
                str += "<tr>";
                str += "<th id=\"Setting_Name" + i + "\">" + json[i].Setting_Name + "</th>";
                str += "<td><span id=\"Setting_Key" + i + "\" style=\"width: 95%;\">" + json[i].Setting_Key + "</span></td>";
                switch (json[i].Setting_ValueType) {
                    //普通文本框
                    case "1": str += "<td><input type=\"text\" id=\"Setting_Value" + i + "\" style=\"width: 95%;\" value=\"" + json[i].Setting_Value + "\" maxlength=\"200\" /></td>"; break;
                        //只能输入数字的文本框
                    case "2": str += "<td><input type=\"text\" id=\"Setting_Value" + i + "\" style=\"width: 95%;\" value=\"" + json[i].Setting_Value + "\" maxlength=\"10\" /></td>"; break;
                        //下拉框
                    case "3": str += "<td> <select id=\"Setting_Value" + i + "\" style=\"width:100px\"><option value=\"1\">是</option><option value=\"0\">否</option></select></td>"; break;
                        //只能输入小数的文本框
                    case "4": str += "<td><input type=\"text\" id=\"Setting_Value" + i + "\" style=\"width: 95%;\" value=\"" + json[i].Setting_Value + "\" maxlength=\"10\" /></td>"; break;
                }
                //str += "<td><input type=\"text\" id=\"Setting_Value" + i + "\" style=\"width: 95%;\" value=\"" + json[i].Setting_Value + "\" /></td>"
                str += "<td><span id=\"Setting_Remarks" + i + "\" style=\"width: 95%;\">" + json[i].Setting_Remarks + "</span></td>";
                str += "</tr>";
            }
            $("#tbSetting").html(str);

            //设置下拉框选择
            setTimeout(function () {
                for (var i = 0; i < json.length; i++) {
                    if (json[i].Setting_ValueType == "3") {
                        var selSorts = $("#Setting_Value" + i);

                        $.each(selSorts, function (index, sort) {
                            var ope = $(sort).find("option[value='" + json[i].Setting_Value + "']");
                            if (ope.length > 0)
                                ope[0].selected = true;
                        });
                    }
                }
            }, 1);

            $("#save").click(function () {
                Submit();
            });

            //  ImageShows('/Styles/login/beijing/0521ee50-0030-438e-9f42-d7c8c8d5087d.gif', 'wqw');

        }

        function Submit() {
            //$("#formConfig").submit();
            var List = [];
            for (var i = 0; i < itemList.length; i++) {
                var item = itemList[i];
                if (item.Setting_ValueType == "2") {
                    var filter = /^[0-9\ ]+$/;
                    if (!filter.test($("#Setting_Value" + i).val())) {
                        alert("【" + item.Setting_Name + "】只能输入整数！");
                        $("#Setting_Value" + i).focus();
                        return;
                    }
                }
                if (item.Setting_ValueType == "4") {
                    var filter = /^[0-9]+\.{0,1}[0-9]{1,5}$/;
                    if (!filter.test($("#Setting_Value" + i).val())) {
                        alert("【" + item.Setting_Name + "】只能输入小数！");
                        $("#Setting_Value" + i).focus();
                        return;
                    }
                }
                item.Setting_Value = $("#Setting_Value" + i).val();
                List.push(item);
            }
            $.ajax({
                type: "post",
                async: false,
                url: "/Handler/ConfigSettingHandler.ashx?Method=SaveConfigItemList&t=" + Math.random(),
                data: { JSON: JSON.stringify(List) },
                dataType: "text",
                success: function (data) {
                    alert(data);
                    document.getElementById("Button1").click();
                }
            });
        }


        function ImageShows(imageUrl, dss) {
            $('#ui_Image').css('display', '');
            $('#ui_Image').dialog('open');
            $('#view_Image').attr('src', '/Windows/Win_Images.aspx?imageUrl=' + imageUrl + '&uls=' + dss);

        }
    </script>
</head>
<body>
    <div style="width: auto; min-width: 1180px;">
        <form id="formConfig" runat="server">
            <!--系统参数设置-->

            <div>
                <div id="tt" class="easyui-tabs">
                    <%--<div id="titleScore" class="easyui-panel" title="系统参数设置" style="width: auto; min-width: 800px; height: auto; text-align: center">--%>
                    <div title="系统参数设置" style="padding: 10px">
                        <table id="tbSetting" border="0" cellpadding="0" cellspacing="1" class="tab" width="100%">
                        </table>

                    </div>
                    <div title="系统配置" style="padding: 10px">
                        <table border="0" cellpadding="0" cellspacing="1" class="tab" width="100%">
                            <tr>
                                <th style="width: 150px">登陆背景图片</th>
                                <td>
                                    <UC:FileImage ID="File" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th style="width: 150px">主框架LOGO背景图片</th>
                                <td>
                                    <UC:FileImage ID="File2" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="display: none">
                        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
                    </div>
                </div>
            </div>
            <table border="0" cellspacing="0" cellpadding="20" width="100%">
                <tr>
                    <th>
                        <a id="save">
                            <asp:Image ID="btn_FH" runat="server" ImageUrl="~/Images/buttons/baocun.gif" /></a>

                    </th>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
