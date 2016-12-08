<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Jalendar.aspx.cs" Inherits="YDT.Web.Manage.Sys.Jalendar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Scripts/UI/themes/bootstrap/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/jalendar/style/documentation.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/jalendar/style/jalendar.css" rel="stylesheet" type="text/css" />

    <script src="/Scripts/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="/Scripts/UI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/Scripts/UI/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="/Scripts/DatePicker/WdatePicker.js" type="text/javascript"></script>

    <%--    <script type="text/javascript" src="/Scripts/jalendar/js/jquery-1.10.2.min.js"></script>--%>
    <script type="text/javascript" src="/Scripts/jalendar/js/jalendar.js"></script>

    <script type="text/javascript">
        
        //获取当前日期  Format: Year-Month-Day
        function CurentTime() {
            var now = new Date();

            var year = now.getFullYear();       //年
            var month = now.getMonth() + 1;     //月
            var day = now.getDate();            //日

            var clock = year + "-";

            if (month < 10)
                clock += "0";
            clock += month + "-";

            if (day < 10)
                clock += "0";
            clock += day;

            return (clock);
        }

        var time = CurentTime();

        function reloadHolidy() {

            jQuery.ajax({
                type: "post",
                async: false,
                url: "/Handler/JalendarHandler.ashx?method=loadHolidy",
                data: {},
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                cache: false,
                success: function (data) {

                    $("#myId").append(data);

                },
                error: function (err) {
                    alert(err);
                }
            });
        }

        function reloadEvent(time) {

            jQuery.ajax({
                type: "post",
                async: false,
                url: "/Handler/JalendarHandler.ashx?method=load",
                data: { jTime: time },
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                cache: false,
                success: function (data) {

                    //var div = document.createElement("div");
                    //div.innerHTML = "<div  class=\"added-event\" data-date=\"13/1/2016\" data-time=\"22:00\" data-title=\"333322\"></div>";
                    //var oTest = document.getElementById("myId");
                    //oTest.appendChild(div);

                    //var strs = "<div class=\"added-event\" data-date=\"13/1/2016\" data-time=\"22:00\" data-title=\"Frocsi\"></div>";
                    //$("#myId").append(strs);

                    $("#myId").append(data);

                    // $("#myId").html(showAddedEvent());
                    //$("#myId").ready(function () {
                    //    showAddedEvent();
                    //});
                    //alert("1")
                    // $.showAddedEvent();
                    //jQuery(document).ready(function () { showAddedEvent(); })
                    //showAddedEvent();
                },
                error: function (err) {
                    alert(err);
                }
            });
        }

        $(function () {
            $('#myId').jalendar({
                customDay: CurentTime()
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server" style="min-width: 1180px;">
        <input id="hid_SelectedDay" type="hidden" />
        <input id="hid_Method" type="hidden" />
        <input id="hid_JalendarID" type="hidden" />

        <div id="myId" class="jalendar">

            <script type="text/javascript">
                reloadHolidy();
                reloadEvent(CurentTime());
            </script>

            <%-- <div class="added-event" data-date="13/1/2016" data-stime="s 22:00"  data-etime="e 22:00"    data-title="1233333" data-sid="id123"></div> --%>

        </div>

        <!--新增事件-->
        <div id="ww"
            class="easyui-window"
            title="日历备忘录维护"
            data-options="modal:true,closed:true,iconCls:'icon-save'"
            style="width: 600px;  padding: 10px; left:380px;top:120px; ">
            <table class="tab" border="0" cellpadding="10" cellspacing="1"  style ="height:250px;">
                <tr>
                    <td>事件内容：<font color="red">*</font></td>
                    <td colspan="3">
                        <textarea id="txt_Event" cols="50" rows="4" class="validate[required,length[1,500]]"></textarea>
                    </td>
                </tr>
                <tr>
                    <td>开始时间：<font color="red">*</font></td>
                    <td style="width: 200px;">
                        <asp:TextBox ID="txt_StartDate" runat="server" CssClass="input4 validate[required]" onclick="WdatePicker({isShowClear:false,readOnly:true,maxDate:'#F{$dp.$D(\'txt_EndDate\')}',dateFmt:'yyyy-MM-dd'})" Style="width: 185px;"></asp:TextBox>
                    </td>
                    <td style="width: 55px; text-align: left;">
                        <asp:DropDownList ID="ddlSH" runat="server"></asp:DropDownList>时
                    </td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddlSM" runat="server"></asp:DropDownList>分
                    </td>
                </tr>
                <tr>
                    <td>结束时间：<font color="red">*</font></td>
                    <td>
                        <asp:TextBox ID="txt_EndDate" runat="server" CssClass="input4 validate[required]" onclick="WdatePicker({isShowClear:false,readOnly:true,minDate:'#F{$dp.$D(\'txt_StartDate\')}',dateFmt:'yyyy-MM-dd'})" Style="width: 185px;"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlEH" runat="server"></asp:DropDownList>时
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlEM" runat="server"></asp:DropDownList>分
                    </td>
                </tr>
                <tr style="display: none">
                    <td>事件类型：<font color="red">*</font></td>
                    <td colspan="3">
                        <asp:DropDownList ID="ddl_Type" runat="server">
                            <asp:ListItem Value="0">私人</asp:ListItem>
                            <asp:ListItem Value="1">公开</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>事件状态：<font color="red">*</font></td>
                    <td colspan="3">
                        <asp:DropDownList ID="ddl_EventStatus" runat="server">
                            <asp:ListItem Value="0">未完成</asp:ListItem>
                            <asp:ListItem Value="1">已完成</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <div style="text-align: center; padding: 5px">
                <a href="javascript:void(0)" class="easyui-linkbutton" onclick="AddSaveEvent();" id="hfEvent">保存</a>
                <a href="javascript:void(0)" class="easyui-linkbutton" onclick="clearFormEvent()">取消</a>
            </div>
        </div>

        <script type="text/javascript">

            window.onload = function () {

                //reloadEvent(CurentTime())
            }


            /*点击新增事件*/
            $(this).on('click', '.addEvent', function () {
                AddShowEvent();
            });

            /*点击日期事件*/
            $(this).on('click', '.day.this-month', function () {
                var thisDay = $('.selected').attr('data-date');
               
                $('#hid_SelectedDay').val(thisDay);
                //reloadEvent(thisDay)
            });

            /*编辑事件*/
            $(this).on('click', '.event-single .editEvent', function () {
                var id = $('div[data-id=' + $(this).parents(".event-single").attr("data-id") + ']').attr("data-sid");

                EditShowEvent(id);
            });

            /*删除事件*/
            $(this).on('click', '.event-single .deleteEvent', function () {
                if (confirm("您确定要删除吗？")) {
                    var id = $('div[data-id=' + $(this).parents(".event-single").attr("data-id") + ']').attr("data-sid");
                    DeleteEvent(id);
                }
            });

            /*新增窗口*/
            function AddShowEvent() {
                $('#txt_Event').val('');
                $('#ddl_Type').val('');
                $('#ddl_EventStatus').val('');

                var thisDay = $("#myId").find('.selected').attr('data-date');

                if (thisDay == undefined || thisDay == null) {
                    $('#txt_StartDate').val(CurentTime());
                    $('#txt_EndDate').val(CurentTime());
                }
                else {
                    var now = new Date(thisDay);

                    var year = now.getFullYear();       //年
                    var month = now.getMonth() + 1;     //月
                    var day = now.getDate();            //日

                    var clock = year + "-";

                    if (month < 10)
                        clock += "0";
                    clock += month + "-";

                    if (day < 10)
                        clock += "0";
                    clock += day;

                    $('#txt_StartDate').val(clock);
                    $('#txt_EndDate').val(clock);
                }

                var objSelect = document.getElementById("ddlSH");
                objSelect.options[0].selected = true;

                var objSelect = document.getElementById("ddlSM");
                objSelect.options[0].selected = true;

                var objSelect = document.getElementById("ddlEH");
                objSelect.options[0].selected = true;

                var objSelect = document.getElementById("ddlEM");
                objSelect.options[0].selected = true;

                $('#hid_Method').val('add');

                $('#ww').window('open');
            }

            /*编辑窗口*/
            function EditShowEvent(id) {
                $('#hid_Method').val('modify');

                $.getJSON("/Handler/JalendarHandler.ashx?method=show&code=" + id + "&random=" + Math.random() + "", {}, function (json) {
                    //	                alert(json);
                    if (json != "0") {
                        $('#txt_Event').val(json.Jalendar_Event);
                        $('#txt_StartDate').val(json.Jalendar_StartDateSD);
                        $('#txt_EndDate').val(json.Jalendar_EndDateSD);
                        $('#hid_JalendarID').val(json.JalendarID);

                        var sd = new Date(json.Jalendar_StartDate);
                        var sHour = sd.getHours();
                        var sMinute = sd.getMinutes();
                        if (sHour < 10)
                        {
                            sHour = "0" + sHour;
                        }
                        if (sMinute < 10)
                        {
                            sMinute = "0" + sMinute;
                        }

                        var ed = new Date(json.Jalendar_EndDate);
                        var eHour = ed.getHours();
                        var eMinute = ed.getMinutes();
                        if (eHour < 10) {
                            eHour = "0" + eHour;
                        }
                        if (eMinute < 10) {
                            eMinute = "0" + eMinute;
                        }

                        var objSelect = document.getElementById("ddlSH");
                        for (var i = 0; i < objSelect.options.length; i++) {
                            if (sHour != null) {
                                if (objSelect.options[i].value == sHour) {
                                    objSelect.options[i].selected = true;
                                }
                            }
                            else {
                                objSelect.options[0].selected = true;
                            }
                        }

                        var objSelect = document.getElementById("ddlSM");
                        for (var i = 0; i < objSelect.options.length; i++) {
                            if (sMinute != null) {
                                if (objSelect.options[i].value == sMinute) {
                                    objSelect.options[i].selected = true;
                                }
                            }
                            else {
                                objSelect.options[0].selected = true;
                            }
                        }

                        var objSelect = document.getElementById("ddlEH");
                        for (var i = 0; i < objSelect.options.length; i++) {
                            if (eHour != null) {
                                if (objSelect.options[i].value == eHour) {
                                    objSelect.options[i].selected = true;
                                }
                            }
                            else {
                                objSelect.options[0].selected = true;
                            }
                        }

                        var objSelect = document.getElementById("ddlEM");
                        for (var i = 0; i < objSelect.options.length; i++) {
                            if (eMinute != null) {
                                if (objSelect.options[i].value == eMinute) {
                                    objSelect.options[i].selected = true;
                                }
                            }
                            else {
                                objSelect.options[0].selected = true;
                            }
                        }

                        var objSelect = document.getElementById("ddl_Type");
                        for (var i = 0; i < objSelect.options.length; i++) {
                            if (json.Jalendar_Type != null) {
                                if (objSelect.options[i].value == json.Jalendar_Type) {
                                    objSelect.options[i].selected = true;
                                }
                            }
                            else {
                                objSelect.options[0].selected = true;
                            }
                        }

                        var objSelect = document.getElementById("ddl_EventStatus");
                        for (var i = 0; i < objSelect.options.length; i++) {
                            if (json.Jalendar_EventStatus != null) {
                                if (objSelect.options[i].value == json.Jalendar_EventStatus) {
                                    objSelect.options[i].selected = true;
                                }
                            }
                            else {
                                objSelect.options[0].selected = true;
                            }
                        }

                        $('#ww').window('open');
                    }
                    else {
                        location.reload();
                        alert('该数据不存在！');
                    }
                });
            }

            /*关闭窗口*/
            function clearFormEvent() {
                $('#ww').window('close');
            }

            /*保存事件*/
            function AddSaveEvent() {
                var event = $('#txt_Event').val();

                var startDate = $('#txt_StartDate').val();

                var endDate = $('#txt_EndDate').val();

                var type = $('#ddl_Type').val();

                var eventStatus = $('#ddl_EventStatus').val();

                if (event == null || event == '') {
                    alert('事件不可为空！');
                    return;
                }

                if (startDate == null || startDate == '') {
                    alert('开始时间不可为空！');
                    return;
                }

                if (endDate == null || endDate == '') {
                    alert('结束时间不可为空！');
                    return;
                }

                var sh = $('#ddlSH').val();
                var sm = $('#ddlSM').val();

                var eh = $('#ddlEH').val();
                var em = $('#ddlEM').val();

                if (startDate == endDate)
                {
                    if (sh > eh) {
                        alert('开始时间不应小于结束时间！');
                        return;
                    }
                    else if (sh == eh) {
                        if (sm > em)
                        {
                            alert('开始时间不应小于结束时间！');
                            return;
                        }
                    }
                }
                startDate += " " + sh + ":" + sm;
                endDate += " " + eh + ":" + em;

                if ($('#hid_Method').val() == "add") {
                    $.get("/Handler/JalendarHandler.ashx?method=add", { jEvent: event, jStartDate: startDate, jEndDate: endDate, jType: type, jEventStatus: eventStatus },

                function (data, textStatus) {

                    if (data == "1") {
                        $('#ww').window('close');
                        alert('新增成功!');
                        location.reload();
                    }
                    else {
                        alert('新增失败!');
                    }
                });
                }
                else {

                    $.get("/Handler/JalendarHandler.ashx?method=modify", { id: $('#hid_JalendarID').val(), jEvent: event, jStartDate: startDate, jEndDate: endDate, jType: type, jEventStatus: eventStatus },

                function (data, textStatus) {

                    if (data == "1") {
                        $('#ww').window('close');
                        alert('修改成功!');
                        location.reload();
                    }
                    else {
                        alert('修改失败!(该数据不存在！)');
                    }
                });
                }
            }

            /*删除*/
            function DeleteEvent(id) {
                $.get("/Handler/JalendarHandler.ashx?method=delete", { pcode: id }, function (data, textStatus) {

                    if (data == "1") {
                        alert("删除成功!");
                        location.reload();
                    }
                    else {
                        alert("删除失败!");
                    }
                });
            }

        </script>

    </form>
</body>
</html>
