<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentManage.aspx.cs" Inherits="Shu.Manage.Sys.DepartmentManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="/Scripts/jquery-2.0.0.min.js"></script>
    <link href="/Content/themes/bootstrap/easyui.css" rel="stylesheet" />
    <link href="/Content/themes/icon.css" rel="stylesheet" />
    <script type="text/javascript" src="/Scripts/jquery.easyui-1.4.5.min.js" charset="utf-8"></script>
    <script type="text/javascript" src="/Scripts/locale/easyui-lang-zh_CN.js" charset="utf-8"></script>
</head>
<body>
    <form id="form1" runat="server">
        <input id="hid_Method" type="hidden" />
        <input id="hid_MenuID" type="hidden" />
        <asp:HiddenField ID="hid_dep" runat="server" />
        <div style="margin: 10px 0;">

            <asp:Literal ID="Literal1" runat="server"><a href="javascript:void(0)" class="easyui-linkbutton" onclick="AddShow()">新增部门</a></asp:Literal>
            <a href="javascript:void(0)" class="easyui-linkbutton" onclick="showedit()">编辑</a>
            <asp:Literal ID="Literal2" runat="server"><a href="javascript:void(0)" class="easyui-linkbutton" onclick="del()">删除</a></asp:Literal>
            <a href="javascript:void(0)" class="easyui-linkbutton" onclick="reload()">刷新</a>
            <%--<asp:Literal ID="Literal3" runat="server"><a href="javascript:void(0)" class="easyui-linkbutton" onclick="wordDoc()">导出Excel</a></asp:Literal>--%>
        </div>
        <table id="tg" class="easyui-treegrid" title="组织机构管理" style="height: 480px"
            data-options="
				iconCls: 'icon-ok',
				rownumbers: true,
				animate: true,
				collapsible: true,
				fitColumns: true,
				url: '/Handler/DepartmentHandler.ashx?method=load',
				method: 'get',
				idField: 'id',
				treeField: 'name',
				showFooter: true
			">
            <thead>
                <tr>
                    <th data-options="field:'name',width:180">名称</th>
                    <%--<th data-options="field:'type',width:50" style=" display:none;">类型</th>
                <th data-options="field:'personCharge',width:50" style=" display:none;">负责人</th>
                <th data-options="field:'teachers',width:50" style=" display:none;">教师人数</th>
                <th data-options="field:'students',width:50" style=" display:none;">在校学生人数</th>--%>
                    <th data-options="field:'sort',width:50">排序</th>
                </tr>
            </thead>
        </table>

        <!--新增-->
        <div id="w"
            class="easyui-window"
            title="组织机构维护"
            data-options="modal:true,closed:true,iconCls:'icon-save'"
            style="width: 600px; height: 350px; padding: 10px;">
            <table>
                <tr>
                    <td>机构类型：</td>
                    <td>
                        <asp:DropDownList ID="ddlDepType" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr style="display: none;">
                    <td>机构级别：</td>
                    <td>
                        <asp:DropDownList ID="ddlDepClass" CssClass="validate[required]" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>机构名称：</td>
                    <td>
                        <input class="easyui-validatebox" style="width: 200px" type="text" id="menuname" name="menuname" data-options="required:true"></input>
                    </td>
                </tr>
                <tr style="display: none;">
                    <td>负责人：</td>
                    <td>
                        <input class="easyui-validatebox" style="width: 200px" type="text" id="PersonCharge" name="PersonCharge" data-options="required:true"></input>
                    </td>
                </tr>
                <tr style="display: none;">
                    <td>教师人数：</td>
                    <td>
                        <input class="easyui-numberbox easyui-validatebox" style="width: 200px" type="text" id="Teachers" name="Teachers"></input>
                    </td>
                </tr>
                <tr style="display: none;">
                    <td>在校学生人数：</td>
                    <td>
                        <input class="easyui-numberbox easyui-validatebox" style="width: 200px" type="text" id="Students" name="Students"></input>
                    </td>
                </tr>
                <tr>
                    <td>排序号：</td>
                    <td>
                        <input class="easyui-numberbox" style="width: 200px" type="text" data-options="required:true" id="menusort" name="menusort" /></input>
                    </td>
                </tr>
                <tr>
                    <td>备注：</td>
                    <td>
                        <textarea id="content" cols="50" class="validate[required]" rows="4"></textarea>
                    </td>

                </tr>

            </table>
            <div style="text-align: center; padding: 5px">
                <a href="javascript:void(0)" class="easyui-linkbutton" onclick="AddSave();" id="hf">保存</a>
                <a href="javascript:void(0)" class="easyui-linkbutton" onclick="clearForm()">取消</a>
            </div>
        </div>

        <script type="text/javascript">
            function wordDoc() {
                jQuery.ajax({
                    type: "post",
                    async: false,
                    url: "/Manage/Sys/Educe.aspx",
                    data: {},
                    contentType: "application/json; charset=utf-8",
                    dataType: "text",
                    cache: false,
                    success: function (json) {
                        if (json != "0") {
                            //alert(json);
                            window.open("/Files/Download.aspx?path=" + json + "&filename=组织结构.xlsx", "");

                        } else {
                            alert("导出失败！");
                        }

                    }
                    //	            error: function (err) {
                    //	                alert(err);
                    //	            }
                });


            }
            var editingId;

            function AddShow() {
                $('#menuname').val('');
                $('#content').val('');
                $('#menusort').val()
                $('#ddlDepType').val('');

                var node = $('#tg').treegrid('getSelected');

                if (node == undefined || node == null) {

                    alert("请选择节点");
                }
                else {
                    $.get("/Handler/DepartmentHandler.ashx?method=sort", { pcode: node.id }, function (data, textStatus) {
                        //	                alert(data);
                        if (data != "0") {


                            $('#menusort').numberbox('setValue', data);

                            $('#hid_Method').val('add');

                            $('#w').window('open');
                        } else {
                            reload();
                            alert('该数据不存在！');
                        }

                    });


                }
            }

            function clearForm() {
                $('#w').window('close');
            }


            function showedit() {

                var node = $('#tg').treegrid('getSelected');

                if (node == undefined || node == null) {

                    alert("请选择节点");
                }
                else {
                    $('#hid_Method').val('modify');
                    var dep = $("#hid_dep").val();
                    $.getJSON("/Handler/DepartmentHandler.ashx?method=show&code=" + node.id + "&random=" + Math.random() + "", {}, function (json) {
                        //	                alert(json);
                        if (json != "0") {


                            $('#menuname').val(json.Department_Name);
                            $('#content').val(json.Department_Respon);
                            $('#menusort').numberbox('setValue', json.Department_Sequence)
                            $('#hid_MenuID').val(json.DepartmentID)

                            //$('#ddlDepType').val();

                            //debugger
                            $('#PersonCharge').val(json.Department_PersonCharge);
                            $('#Teachers').val(json.Department_Teachers);
                            $('#Students').val(json.Department_Students);

                            var objSelect = document.getElementById("ddlDepType");
                            for (var i = 0; i < objSelect.options.length; i++) {
                                if (json.Department_Type != null) {
                                    if (objSelect.options[i].value == json.Department_Type) {
                                        objSelect.options[i].selected = true;
                                    }
                                }

                                else {
                                    objSelect.options[0].selected = true;
                                }
                            }
                            var objSelect = document.getElementById("ddlDepClass");
                            for (var i = 0; i < objSelect.options.length; i++) {
                                if (json.Department_Class != null) {
                                    if (objSelect.options[i].value == json.Department_Class) {
                                        objSelect.options[i].selected = true;
                                    }
                                }

                                else {
                                    objSelect.options[0].selected = true;
                                }
                            }
                            $('#w').window('open');
                            //alert(json.Department_Sequence);
                            if (json.Department_Sequence == "0" && dep == "1") {

                                $("#hf").css('display', 'none');

                            } else {

                                $("#hf").css('display', '');

                            }
                        }
                        else {
                            reload();
                            alert('该数据不存在！');
                        }

                    });


                    //alert(px);
                    if (dep == "1") {

                        $("#menusort").attr('disabled', true);
                        //$("#ddlDepType").attr('disabled', true);
                        $("#menuname").attr('disabled', true);
                    }

                }
            }



            function reload() {

                jQuery.ajax({
                    type: "post",
                    async: false,
                    url: "/Handler/DepartmentHandler.ashx?method=load",
                    data: {},
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    cache: false,
                    success: function (json) {

                        $('#tg').treegrid('loadData', json);

                    },
                    error: function (err) {
                        alert(err);
                    }
                });
            }


            /*新增*/
            function AddSave() {

                var path = "";

                var name1 = $('#menuname').val();

                var content1 = $('#content').val();

                var sort1 = $('#menusort').val();

                var pcode1 = $('#tg').treegrid('getSelected').id;

                var type1 = $('#ddlDepType').val();
                var depClass1 = $('#ddlDepClass').val();
                var personCharge1 = $('#PersonCharge').val();
                var teachers1 = $('#Teachers').val();
                var students1 = $('#Students').val();

                if (type1 == null || type1 == '')
                {
                    alert('机构类型不可为空');
                    return;
                }

                if (name1 == null || name1 == '') {
                    alert('机构名称不可为空');
                    return;
                }

                if (sort1 == null || sort1 == '') {
                    alert('排序号不可为空');
                    return;
                }

                

                if ($('#hid_Method').val() == "add") {

                    $.get("/Handler/DepartmentHandler.ashx?method=add", { name: name1, content: content1, sort: sort1, pcode: pcode1, type: type1, depClass: depClass1, personCharge: personCharge1, teachers: teachers1, students: students1 },

                function (data, textStatus) {

                    if (data == "1") {

                        reload();
                        $('#w').window('close');
                        alert('新增成功!');
                    }
                    else {
                        alert('新增失败!');
                    }


                });
                }
                else {

                    $.get("/Handler/DepartmentHandler.ashx?method=modify", { name: name1, content: content1, sort: sort1, id: $('#hid_MenuID').val(), type: type1, depClass: depClass1, personCharge: personCharge1, teachers: teachers1, students: students1 },

                function (data, textStatus) {

                    if (data == "1") {

                        reload();

                        $('#w').window('close');

                        alert('修改成功!');

                    }
                    else {
                        alert('修改失败!(该数据不存在！)');
                    }


                });

                }
            }


            /*#################################*/

            /*###############删除##################*/

            function del() {

                if (confirm("您确定要删除吗？")) {
                    var node = $('#tg').treegrid('getSelected');

                    if (node == undefined || node == null) {

                        alert("请选择节点");
                    }
                    else {
                        $.get("/Handler/DepartmentHandler.ashx?method=delete", { pcode: node.id }, function (data, textStatus) {

                            if (data == "1") {


                                reload();
                                alert("删除成功!");
                            }
                            else if (data == "2") {
                                alert("该部门下有用户存在，不能删除!");
                            }
                            else {

                                alert("该节点下存在子节点，请先删除!");
                            }
                        });


                    }
                }
            }

        </script>

    </form>
</body>
</html>
