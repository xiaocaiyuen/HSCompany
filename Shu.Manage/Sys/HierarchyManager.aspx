<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HierarchyManager.aspx.cs" Inherits="YDT.Web.Manage.Sys.HierarchyManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Scripts/UI/themes/bootstrap/easyui.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="/Scripts/UI/jquery.easyui.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Scripts/UI/locale/easyui-lang-zh_CN.js" charset="utf-8"></script>
</head>
<body>
    <form id="form1" runat="server">
        <input id="hid_Method" type="hidden" />
        <input id="hid_MenuID" type="hidden" />
        <div style="margin: 10px 0;">
            <a href="javascript:void(0)" class="easyui-linkbutton" onclick="AddShow()">新增子节点</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" onclick="showedit()">编辑</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" onclick="del()">删除</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" onclick="reload()">刷新</a>
        </div>
        <table id="tg" class="easyui-treegrid" title="层级管理" style="height: 340px; width: 600px;"
            data-options="
				iconCls: 'icon-ok',
				rownumbers: true,
				animate: true,
				collapsible: true,
				fitColumns: true,
				url: '/Handler/HierarchyHandler.ashx?method=load',
				method: 'get',
				idField: 'id',
				treeField: 'name',
				showFooter: true
			">
            <thead>
                <tr>
                    <th data-options="field:'name',width:180">名称</th>
                    <th data-options="field:'code',width:180">编码</th>
                    <th data-options="field:'sort',width:50">排序</th>


                </tr>
            </thead>
        </table>

        <!--新增-->
        <div id="w"
            class="easyui-window"
            title="层级维护"
            data-options="modal:true,closed:true,iconCls:'icon-save'"
            style="width: 600px; height: 250px; padding: 10px;">
            <table>
                <tr>
                    <td>名称：</td>
                    <td>
                        <input class="easyui-validatebox" style="width: 200px" type="text" id="menuname" name="menuname" data-options="required:true"></input>
                    </td>
                </tr>

                <tr>
                    <td>排序号：</td>
                    <td>
                        <input class="easyui-numberbox easyui-validatebox" style="width: 200px" type="text" id="menusort" name="menusort" data-options="required:true"></input>
                    </td>
                </tr>
                <tr>
                    <td>备注：</td>
                    <td>
                        <textarea id="content" cols="50" rows="4"></textarea>
                    </td>

                </tr>

            </table>
            <div style="text-align: center; padding: 5px">
                <a href="javascript:void(0)" class="easyui-linkbutton" onclick="AddSave();">保存</a>
                <a href="javascript:void(0)" class="easyui-linkbutton" onclick="clearForm()">取消</a>
            </div>
        </div>

        <script type="text/javascript">

            var editingId;

            function AddShow() {

                $('#menuname').val();
                $('#content').val();
                $('#menusort').val()

                
                var node = $('#tg').treegrid('getSelected');
                if (node == undefined || node == null) {

                    alert("请选择节点");
                }
                else if (node.id == 0) {
                    alert("根节点不能添加子节点");
                }
                else {
                    $.get("/Handler/HierarchyHandler.ashx?method=sort", { pcode: node.id }, function (data, textStatus) {

                        $('#menusort').val(data);
                    });

                    $('#hid_Method').val('add');

                    $('#w').window('open');
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
                    $.getJSON("/Handler/HierarchyHandler.ashx?method=show&code=" + node.id + "&random=" + Math.random() + "", {}, function (json) {
                      
                        $('#menuname').val(json.Hierarchy_Name);
                        $('#content').val(json.Hierarchy_Remark);
                        $('#menusort').val(json.Hierarchy_Sequence)
                        $('#hid_MenuID').val(json.HierarchyID)


                        $('#w').window('open');


                    });
                }
            }



            function reload() {

                jQuery.ajax({
                    type: "post",
                    async: false,
                    url: "/Handler/HierarchyHandler.ashx?method=load",
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



                if ($('#hid_Method').val() == "add") {

                    $.get("/Handler/HierarchyHandler.ashx?method=add", { name: name1, content: content1, sort: sort1, pcode: pcode1 },

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

                    $.get("/Handler/HierarchyHandler.ashx?method=modify", { name: name1, content: content1, sort: sort1, id: $('#hid_MenuID').val() },

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
                        $.get("/Handler/HierarchyHandler.ashx?method=delete", { pcode: node.id }, function (data, textStatus) {

                            if (data == "1") {


                                reload();
                                alert("删除成功!");
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
