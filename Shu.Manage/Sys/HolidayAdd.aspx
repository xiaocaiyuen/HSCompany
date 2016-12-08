<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HolidayAdd.aspx.cs" Inherits="YDT.Web.Manage.Sys.HolidayAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="/Scripts/jquery-1.8.0.min.js" type="text/javascript"></script>
    <link href="/Styles/table.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/List.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/DatePicker/WdatePicker.js" type="text/javascript"></script>
    <%--录入验证 --%>
    <link href="/Styles/validationEngine.jquery.css" rel="stylesheet" type="text/css" charset="utf-8" />
    <script src="../../Scripts/Validate/2.6.2Ciaoca/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="../../Scripts/Validate/2.6.2Ciaoca/jquery.validationEngine.min.js" type="text/javascript"></script>
    <script src="../../Scripts/Validate/2.6.2Ciaoca/jquery.validationEngine-zh_CN.js" type="text/javascript" charset="utf-8"></script>
    <style type="text/css">
        .table th {
            width: 140px;
        }

        .table td {
            width: 300px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("#form1").validationEngine({ promptPosition: "centerRight", validationEventTriggers: "keyup blur" });

            var now = new Date();
            var year = now.getFullYear();       //年
            var month = now.getMonth() + 1;     //月
            var day = now.getDate() + 1;         //日

            var endDate = year + "年";
            if (month < 10)
                endDate += "0";
            endDate += month + "月";

            if (day < 10)
                endDate += "0";
            endDate += day + "日";
          
            $("#txtEndDate").val(endDate);
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:TextBox ID="txtEndDate" runat="server" Style="display: none" ></asp:TextBox>

        <center>
            <table width="880" border="0" cellspacing="0" cellpadding="0" class="headerCss">
                <tr>
                    <th colspan="4">节假日信息维护
                    </th>
                </tr>
            </table>
            <table width="880" class="tab" border="0" cellpadding="10" cellspacing="1">
                <tr>
                    <th>节假日名称<font color="red">*</font>
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtHoliday_Name" runat="server" CssClass="input4 validate[required,maxSize[50]]" MaxLength="50" Style="width: 500px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>节假日开始日期<font color="red">*</font>
                    </th>
                    <td>
                        <asp:TextBox ID="txtHoliday_StartDate" runat="server" CssClass="input4 validate[required]" onclick="WdatePicker({isShowClear:false,readOnly:true,minDate:'%y-%M-{%d+1}',maxDate:'#F{$dp.$D(\'txtHoliday_EndDate\')}', dateFmt:'yyyy年MM月dd日'})" Style="width: 240px;"></asp:TextBox>
                    </td>
                    <th>节假日结束日期<font color="red">*</font>
                    </th>
                    <td>
                        <asp:TextBox ID="txtHoliday_EndDate" runat="server" CssClass="input4 validate[required]" onclick="WdatePicker({isShowClear:false,readOnly:true,minDate:'#F{$dp.$D(\'txtHoliday_StartDate\')|| $dp.$D(\'txtEndDate\')}',dateFmt:'yyyy年MM月dd日'})" Style="width: 240px;"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table width="880" border="0" cellspacing="0" cellpadding="0" class="for">
                <tr>
                    <th>
                        <asp:ImageButton ID="btn_Save" runat="server" ImageUrl="~/Images/buttons/baocun.gif"
                            OnClick="btnAdd_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp; <a id="FanHui" href="HolidayList.aspx">
                        <asp:Image ID="btn_FH" runat="server" ImageUrl="~/Images/buttons/fanHui.gif" /></a>
                    </th>
                </tr>
            </table>
        </center>
    </form>
</body>
</html>
