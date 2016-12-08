<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CityManager.aspx.cs" Inherits="Shu.Manage.Sys.CityManage" %>

<!DOCTYPE html>

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
        <table id="tg" class="easyui-treegrid" title="城市管理" style="height: 540px;"
            data-options="
				iconCls: 'icon-ok',
				rownumbers: true,
				animate: true,
				collapsible: true,
				fitColumns: true,
				url: '/Handler/CityHandler.ashx?method=load',
				method: 'get',
				idField: 'id',
				treeField: 'name',
				showFooter: true
			">
            <thead>
                <tr>
                    <th data-options="field:'name',width:180">名称</th>
                    <th data-options="field:'zipcode',width:180">邮编</th>
                    <th data-options="field:'areacode',width:180">区号</th>
                    <th data-options="field:'code',width:180">编码</th>
                    <th data-options="field:'sort',width:50">排序</th>
                </tr>
            </thead>
        </table>

        <!--新增-->
        <div id="w"
            class="easyui-window"
            title="城市维护"
            data-options="modal:true,closed:true,iconCls:'icon-save'"
            style="width: 350px; height: 200px; padding: 10px;">
            <table>
                <tr>
                    <td>名称：</td>
                    <td>
                        <input class="easyui-textbox" style="width: 200px" type="text" id="menuname" name="menuname" data-options="required:true" />
                    </td>
                </tr>
                <tr>
                    <td>排序号：</td>
                    <td>
                        <input class="easyui-numberbox" style="width: 200px" type="text" id="menusort" name="menusort" data-options="required:true" />
                    </td>
                </tr>
                <tr>
                    <td>邮编：</td>
                    <td>
                        <input class="easyui-textbox" style="width: 200px" type="text" id="zipcode" name="zipcode" data-options="required:false" />
                    </td>
                </tr>
                <tr>
                    <td>电话区号：</td>
                    <td>
                        <input class="easyui-textbox" style="width: 200px" type="text" id="areacode" name="areacode" data-options="required:false" />
                    </td>
                </tr>
            </table>
            <div style="text-align: center; padding: 5px">
                <a href="javascript:void(0)" class="easyui-linkbutton" onclick="AddSave();">保存</a>
                <a href="javascript:void(0)" class="easyui-linkbutton" onclick="clearForm()">取消</a>
            </div>
        </div>
        <div id="coverShow" style="position: fixed;z-index: 2;top: 40%;left: 40%; width:220px; border: 1px solid #C1CDDA;">
            <table  align="center"; border="0" style="border-collapse: collapse; width:100%; height: 100px; min-height: 100px;" >
                <tr>
                    <td style="font-size:12px; padding-left:15px; height:30px; background-color:#eee"><strong>正在加载，请稍后......</strong></td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#f5f5f5">
                        <%--<img src="~/assets/images/loading.gif" />--%>
                    </td>
                </tr>
            </table>
        </div>
        <script type="text/javascript">

            var editingId;
            var second = 0;
            startTimer();
            var myVar;
            function AddShow() {
                var node = $('#tg').treegrid('getSelected');
                if (node == undefined || node == null) {

                    alert("请选择节点");
                }
                else if (node.id == 0) {
                    alert("根节点不能添加子节点");
                }
                else {
                    $.get("/Handler/CityHandler.ashx?method=sort", { pcode: node.id }, function (data, textStatus) {
                        $('#menusort').val(data);
                    });

                    $('#hid_Method').val('add');

                    $('#w').window('open');
                }
            }

            function startTimer() {
                /*setInterval() 间隔指定的毫秒数不停地执行指定的代码*/
                myVar = setInterval(function () { hideCover(); }, 500);
            }
            function hideCover() {
                if (second >= 1) {
                    var covershow = document.getElementById("coverShow");
                    covershow.style.display = 'none';
                    clearInterval(myVar);
                }
                second++;
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
                    $.getJSON("/Handler/CityHandler.ashx?method=show&code=" + node.id + "&random=" + Math.random() + "", {}, function (json) {

                        $('#menuname').textbox('setValue', json.Name);
                        $('#menusort').numberbox('setValue', json.Sort)
                        $('#zipcode').textbox('setValue', json.PostCode);
                        $('#areacode').textbox('setValue', json.AreaCode);
                        $('#hid_MenuID').val(json.ID)
                        $('#w').window('open');


                    });
                }
            }

            function reload() {
                jQuery.ajax({
                    type: "post",
                    async: false,
                    url: "/Handler/CityHandler.ashx?method=load",
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
                if (name1 == "") {
                    alert("城市名称不可为空。");
                    return false;
                }
                var sort1 = $('#menusort').val();
                if (sort1 == "") {
                    alert("城市排序不可为空。");
                    return false;
                }
                var zipCode= $('#zipcode').val();
                var areaCode = $('#areacode').val();

                var pcode1 = $('#tg').treegrid('getSelected').id;

                if ($('#hid_Method').val() == "add") {
                    $.get("/Handler/CityHandler.ashx?method=add", { name: name1, sort: sort1, pcode: pcode1, zipcode: zipCode, areacode: areaCode }, function (data, textStatus) {
                        if (data == "1") {
                            reload();
                            $('#w').window('close');
                            alert('新增成功!');
                        }
                        else {
                            alert('新增失败!');
                        }
                    });
                } else {
                    $.get("/Handler/CityHandler.ashx?method=modify", { name: name1, sort: sort1, id: $('#hid_MenuID').val(), zipcode: zipCode, areacode: areaCode }, function (data, textStatus) {
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
                        $.get("/Handler/CityHandler.ashx?method=delete", { pcode: node.id }, function (data, textStatus) {
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
