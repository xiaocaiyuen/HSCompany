<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DistrictManager.aspx.cs" Inherits="YDT.Web.Manage.Sys.DistrictManager" %>

<!DOCTYPE html>

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
            <a href="javascript:void(0)" class="easyui-linkbutton" onclick="showedit()">更换大区</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" onclick="reload()">刷新</a>
        </div>
        <table id="tg" class="easyui-treegrid" title="区域管理" style="height: 340px; width: 500px;"
            data-options="
				iconCls: 'icon-ok',
				rownumbers: true,
				animate: true,
				collapsible: true,
				fitColumns: true,
				url: '/Handler/DistrictHandler.ashx?method=load',
				method: 'get',
				idField: 'id',
				treeField: 'name',
				showFooter: true
			">
            <thead>
                <tr>
                    <th data-options="field:'name',width:180">名称</th>
                    <th data-options="field:'sort',width:50">排序</th>
                </tr>
            </thead>
        </table>

        <!--编辑-->
        <div id="w"
            class="easyui-window"
            title="区域维护"
            data-options="modal:true,closed:true,iconCls:'icon-save'"
            style="width: 350px; height: 200px; padding: 10px;">
            <table>
                <tr>
                    <td style="height: 34px;">省份：</td>
                    <td>
                        <input class="easyui-validatebox" style="width: 200px" type="text" id="menuname" name="menuname" readonly="readonly" data-options="required:true" />
                    </td>
                </tr>
                <tr style="margin-top:10px;">
                    <td  style="height: 34px;">区域选择：</td>
                    <td>
                        <select id="distSelect"  class="easyui-validatebox" style="width: 200px"></select>
                    </td>
                </tr>
            </table>
            <div style="text-align: center; padding: 5px">
                <a href="javascript:void(0)" class="easyui-linkbutton" onclick="ModifySave();">保存</a>
                <a href="javascript:void(0)" class="easyui-linkbutton" onclick="clearForm()">取消</a>
            </div>
        </div>
        <script type="text/javascript">
            function clearForm() {
                $('#w').window('close');
            }

            function showedit() {
                var node = $('#tg').treegrid('getSelected');
                if (!isNaN(node.id)) {
                    if (node == undefined || node == null) {
                        alert("请选择节点");
                    }
                    else {
                        $('#hid_Method').val('modify');
                        $.getJSON("/Handler/DistrictHandler.ashx?method=show&code=" + node.id + "&random=" + Math.random() + "", {}, function (json) {
                            $('#menuname').val(json.Province);
                            var sel = document.getElementById("distSelect");
                            for (var i = sel.options.length - 1; i >= 0; i--) {
                                sel.options.remove(i);
                            }
                            for (var i = 0; i < json.DistrictList.length; i++) {
                                var item = json.DistrictList[i];
                                sel.options.add(new Option(item.Name, item.Abbreviation));
                            }
                            $("#distSelect").val(json.District);
                            $('#hid_MenuID').val(json.ProvinceID)
                            $('#w').window('open');
                        });
                    }
                }
            }

            function reload() {
                jQuery.ajax({
                    type: "post",
                    async: false,
                    url: "/Handler/DistrictHandler.ashx?method=load",
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

            /*修改*/
            function ModifySave() {
                var path = "";

                var name1 = $('#menuname').val();
                var sel = $('#distSelect').val();

                $.get("/Handler/DistrictHandler.ashx?method=modify", { id: $('#hid_MenuID').val(), pinyin: sel }, function (data, textStatus) {
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
        </script>
    </form>
</body>
</html>
