<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataDictonaryManager.aspx.cs" Inherits="Shu.Manage.Sys.DataDictonaryManager" %>

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
        <div style="margin: 10px 0;">
            <a href="javascript:void(0)" class="easyui-linkbutton" onclick="AddShow()">新增子节点</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" onclick="showedit()">编辑</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" onclick="del()">删除</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" onclick="reload()">刷新</a>
        </div>
        <table id="tg" class="easyui-treegrid" title="数据字典管理" style="height: 480px; "
            data-options="
				iconCls: 'icon-ok',
				rownumbers: true,
				animate: true,
				collapsible: true,
				fitColumns: true,
				url: '/Handler/DictionaryHandler.ashx?method=load',
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
            title="数据字典维护"
            data-options="modal:true,closed:true,iconCls:'icon-save'"
            style="width: 600px; height: 250px; padding: 10px;">
            <table>
                <tr>
                    <td>名称：</td>
                    <td>
                        <input class="easyui-textbox" style="width: 200px" type="text" id="menuname" name="menuname" data-options="required:true"></input>
                    </td>
                </tr>

                <tr>
                    <td>排序号：</td>
                    <td>
                        <input class="easyui-numberbox" style="width: 200px" type="text" id="menusort" name="menusort" data-options="required:true"></input>
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

                //$('#menuname').val();
                //$('#content').val();
                //$('#menusort').val()

                var node = $('#tg').treegrid('getSelected');

                if (node == undefined || node == null) {

                    alert("请选择节点");
                }
                else {
                    $.get("/Handler/DictionaryHandler.ashx?method=sort", { pcode: node.id }, function (data, textStatus) {

                        $('#menusort').numberbox('setValue', data);
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
                    $.getJSON("/Handler/DictionaryHandler.ashx?method=show&code=" + node.id + "&random=" + Math.random() + "", {}, function (json) {

                        $('#menuname').textbox('setValue',json.DataDict_Name);
                        $('#content').val(json.DataDict_Remark);
                        $('#menusort').numberbox('setValue', json.DataDict_Sequence)
                        $('#hid_MenuID').val(json.DataDictID)


                        $('#w').window('open');


                    });
                }
            }



            function reload() {

                jQuery.ajax({
                    type: "post",
                    async: false,
                    url: "/Handler/DictionaryHandler.ashx?method=load",
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

                    $.get("/Handler/DictionaryHandler.ashx?method=add", { name: name1, content: content1, sort: sort1, pcode: pcode1 },

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

                    $.get("/Handler/DictionaryHandler.ashx?method=modify", { name: name1, content: content1, sort: sort1, id: $('#hid_MenuID').val() },

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
                        $.get("/Handler/DictionaryHandler.ashx?method=delete", { pcode: node.id }, function (data, textStatus) {

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
