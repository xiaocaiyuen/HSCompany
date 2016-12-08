<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadDataType.aspx.cs" Inherits="YDT.Web.Manage.Sys.UploadDataType" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <link href="/Scripts/UI/themes/bootstrap/easyui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Scripts/UI/jquery.easyui.min.js"></script>
    <link href="/Styles/table.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/List.css" rel="stylesheet" type="text/css" />
    <%--录入验证 --%>
    <link href="/Styles/validationEngine.jquery.css" rel="stylesheet" type="text/css" charset="utf-8" />
    <script src="/Scripts/Validate/2.6.2Ciaoca/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="/Scripts/Validate/2.6.2Ciaoca/jquery.validationEngine.min.js" type="text/javascript"></script>
    <script src="/Scripts/Validate/2.6.2Ciaoca/jquery.validationEngine-zh_CN.js" type="text/javascript" charset="utf-8"></script>
    <script>
        $(function () {
            $("#form1").validationEngine({ promptPosition: "centerRight", validationEventTriggers: "keyup blur" });

        });

        function getDatas() {
            var str1 = "";
            $('#tbSetting1').find('tr:gt(0)').each(function (trindex) {
                $(this).find('td').each(function (index) {
                    switch (index) {
                        case 0:
                            str1 += $('select', this).val();
                            str1 += ',';
                            break;
                        case 1:
                            str1 += $('input', this).val();
                            str1 += ',';
                            break;
                        case 2:
                            str1 += $('input', this).val();
                            str1 += ',';
                            break;
                        case 3:
                            str1 += $('input', this).val();
                            str1 += ',';
                            break;
                        case 4:
                            str1 += $('input', this).val();
                            str1 += ',';
                            break;

                    }
                })
                str1 += trindex + '|';
            })
            if (str1 != "") {
                str1 = str1.substring(0, str1.length - 1);
            }

            $("#hid_Data1").val(str1);

            var str2 = "";
            $('#tbSetting2').find('tr:gt(0)').each(function (trindex) {
                $(this).find('td').each(function (index) {
                    switch (index) {
                        case 0:
                            str2 += $('select', this).val();
                            str2 += ',';
                            break;
                        case 1:
                            str2 += $('input', this).val();
                            str2 += ',';
                            break;
                        case 2:
                            str2 += $('input', this).val();
                            str2 += ',';
                            break;
                        case 3:
                            str2 += $('input', this).val();
                            str2 += ',';
                            break;
                        case 4:
                            str2 += $('input', this).val();
                            str2 += ',';
                            break;
                    }
                })
                str2 += trindex + '|';
            })
            if (str2 != "") {
                str2 = str2.substring(0, str2.length - 1);
            }

            $("#hid_Data2").val(str2);

        }

        function AddRows(tab) {
            var str1 = "";
            str1 += "<tr>";
            str1 += "<td style=\"text-align:center;\">" + GetTypeDll() + "</td>";
            str1 += "<td><input type=\"text\" id=\"txtTypeName" + GetRandomNum() + "\" maxlength=\"50\"  class=\"required validate[required]\"  style=\"text-align: center; width:100%;\"  /></td>";
            str1 += "<td><input type=\"text\" id=\"txtCode" + GetRandomNum() + "\" maxlength=\"20\"  class=\"required validate[required]\"   /></td>";
            str1 += "<td><input type=\"text\" id=\"txtCode" + GetRandomNum() + "\" maxlength=\"120\"    style=\"width:100%;\"  /></td>";
            str1 += "<td style=\"display:none;\"><input type=\"text\" id=\"txtID" + GetRandomNum() + "\"   /></td>";
            str1 += "<td style=\"text-align:center;\"><a onclick=\"deleteRow(this)\" title=\"删除\"><img src=\"../../Images/buttons/cancel.png\" /></a></td>";
            str1 += "</tr>";
            $("#" + tab).append(str1);
        }


        function GetRandomNum() {
            var Min = 0;
            var Max = 1000000;
            var Range = Max - Min;
            var Rand = Math.random();
            return (Min + Math.round(Rand * Range));
        }

        function GetTypeDll() {

            var str = "";
            str += "<select id=\"txtTypeName" + GetRandomNum() + "\">";
            str += "<option value=\"申请类资料\">申请类资料</option>";
            str += "<option value=\"车辆影像资料\">车辆影像资料</option>";
            str += "<option value=\"初审上传\">初审上传</option>";
            str += "<option value=\"授信审批\">授信审批</option>";
            str += "<option value=\"放款类材料（签约）\">放款类材料（签约）</option>";
            str += "<option value=\"抵押类材料\">抵押类材料</option>";
            str += "<option value=\"放车类资料\">放车类资料</option>";
            str += "<option value=\"贷前：GPS\">贷前：GPS</option>";
            str += "<option value=\"贷后：变更卡号\">贷后：变更卡号</option>";
            str += "<option value=\"贷后：续保\">贷后：续保</option>";
            str += "</select>";
            return str;
        }

        function deleteRow(obj) {
            var row = obj.parentNode.parentNode; //A标签所在行
            var tb = row.parentNode; //当前表格
            var rowIndex = row.rowIndex; //A标签所在行下标
            tb.deleteRow(rowIndex); //删除当前行

        }

    </script>
</head>
<body>
    <form id="form1" runat="server"  autocomplete="off"  >
        <asp:HiddenField ID="hid_Data1" runat="server" />
        <asp:HiddenField ID="hid_Data2" runat="server" />
        <asp:HiddenField ID="hid_RowNum1" runat="server" />
        <asp:HiddenField ID="hid_RowNum2" runat="server" />
    <div>
         <div id="titleScore" class="easyui-panel" title="个人产品资料类别设置" style="width: auto; min-width: 800px; height: auto; text-align: center">
             
                <table id="tbSetting1" border="0" cellpadding="0" cellspacing="1" class="tab" width="100%">
                  <tr >
                     
                     <th style="width:20%">环节节点</th>
                     <th style="width:20%">类别名称</th>
                     <th style="width:10%">前缀编码</th>
                     <th style="width:45%">备注</th>
                    
                       <th style="display:none;"></th>
                       <th style="width:5%; text-align:center;"><a onclick="AddRows('tbSetting1')" title="新增个人产品资料类别"><img src="../../Images/buttons/edit_add.png" /></a></th>
                 </tr>
                    <asp:Literal ID="LitContent1" runat="server"></asp:Literal>
                </table>
                
            </div>
        <div id="titleScore2" class="easyui-panel" title="企业产品资料类别设置" style="width: auto; min-width: 800px; height: auto; text-align: center">
                <table id="tbSetting2" border="0" cellpadding="0" cellspacing="1" class="tab" width="100%">
                 <tr>
                    
                     <th style="width:20%">环节节点</th>
                     <th style="width:20%">类别名称</th>
                     <th style="width:10%">前缀编码</th>
                     <th style="width:45%">备注</th>
                      <th style="display:none;"></th>
                      <th style="width:5%; text-align:center;"><a onclick="AddRows('tbSetting2')" title="新增企业产品资料类别"><img src="../../Images/buttons/edit_add.png" /></a></th>
                 </tr>
                    <asp:Literal ID="LitContent2" runat="server"></asp:Literal>
                </table>
                
            </div>
        <table border="0" cellspacing="0" cellpadding="20" width="100%">
                    <tr>
                        <th>
                         
                                <asp:ImageButton ID="btn_Save" runat="server" ImageUrl="~/Images/buttons/baocun.gif" OnClick="btn_Save_Click" OnClientClick="getDatas()" />
                               
                        </th>
                    </tr>
                </table>
    </div>
    </form>
</body>
</html>
