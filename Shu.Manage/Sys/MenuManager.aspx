<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuManager.aspx.cs" Inherits="Shu.Manage.Sys.MenuManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <title></title>
    <script type="text/javascript" src="/Scripts/jquery-2.0.0.min.js"></script>
    <link href="/Content/themes/bootstrap/easyui.css" rel="stylesheet" />
    <link href="/Content/themes/icon.css" rel="stylesheet" />
    <script type="text/javascript" src="/Scripts/jquery.easyui-1.4.5.min.js" charset="utf-8"></script>
    <script type="text/javascript" src="/Scripts/locale/easyui-lang-zh_CN.js" charset="utf-8"></script>
    <script src="/Scripts/MenuManager.js" type="text/javascript"></script>
</head>
<body>
    <input id="hid_Method" type="hidden" />
    <input id="hid_MenuID" type="hidden" />
    <div style="margin: 10px 0;">
        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="AddShow()">新增子节点</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="showedit()">编辑</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="del()">删除</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="reload()">刷新</a>
    </div>
    <table id="tg" class="easyui-treegrid" title="菜单管理" style="height: 400px"
        data-options="
				iconCls: 'icon-ok',
				rownumbers: true,
				animate: true,
				collapsible: true,
				fitColumns: true,
				url: '/Handler/MenuHandler.ashx?method=load',
				method: 'get',
				idField: 'id',
				treeField: 'name',
				showFooter: true
			">
        <thead>
            <tr>
                <th data-options="field:'name',width:180">菜单名称</th>
                <th data-options="field:'url',width:280,align:'right'">路径</th>
                <th data-options="field:'sort',width:50">排序</th>
                <th data-options="field:'Opt',width:380">操作按钮</th>

            </tr>
        </thead>
    </table>


    <!--新增-->
    <div id="w"
        class="easyui-window"
        title="菜单管理"
        data-options="modal:true,closed:true,iconCls:'icon-save'"
        style="width: 760px; height: 400px; padding: 10px;">
        <table>
            <tr>
                <td style="width: 80px;">菜单名称：</td>
                <td>
                    <input class="easyui-textbox" style="width: 200px" type="text" id="menuname" name="menuname" data-options="required:true"></input>
                </td>
            </tr>
            <tr>
                <td>地    址：</td>
                <td>
                    <input class="easyui-textbox" style="width: 200px" type="text" id="menuurl" name="menuurl" data-options="required:true"></input>
                </td>
            </tr>
            <tr>
                <td>排序号：</td>
                <td>
                    <input class="easyui-numberbox easyui-validatebox" style="width: 200px" type="text" id="menusort" name="menusort" data-options="required:true"></input>
                </td>
            </tr>
            <tr>
                <td>操作按钮：</td>
                <td>
                    <input id="chk1" type="checkbox" value="新增" />新增
                     <input id="chk2" type="checkbox" value="编辑" />编辑
                     <input id="chk40" type="checkbox" value="拷贝" />拷贝
                     <input id="chk3" type="checkbox" value="删除" />删除
                     <input id="chk4" type="checkbox" value="审核" />审核
                     <input id="chk5" type="checkbox" value="查看" />查看
                     <input id="chk6" type="checkbox" value="录入" />录入
                     <input id="chk7" type="checkbox" value="导出" />导出
                     <input id="chk9" type="checkbox" value="办理" />办理
                     <input id="chk8" type="checkbox" value="【导入】" />导入
                     <input id="chk20" type="checkbox" value="配置" />配置
                     <input id="chk21" type="checkbox" value="追踪" />追踪
                     <input id="chk35" type="checkbox" value="邮储银行导入" />邮储银行导入
                     <input id="chk36" type="checkbox" value="农业银行导入" />农业银行导入
                     <input id="chk361" type="checkbox" value="工商银行导入" />工商银行导入
                     <input id="chk37" type="checkbox" value="提交" />提交
                     <input id="chk43" type="checkbox" value="启用" />启用
                     <input id="chk41" type="checkbox" value="批量领单" />批量领单
                     <input id="chk42" type="checkbox" value="领单" />领单
                     <input id="chk44" type="checkbox" value="批量锁定" />批量锁定
                     <input id="chk45" type="checkbox" value="【锁定】" />锁定
                     <input id="chk46" type="checkbox" value="上班模式" />上班模式
                     <input id="chk47" type="checkbox" value="请假模式" />请假模式
                     <input id="chk48" type="checkbox" value="【锁定账号】" />锁定账号
                     <input id="chk49" type="checkbox" value="导 出Excel" />导出Excel
                     <input id="chk50" type="checkbox" value="归档" />归档
                </td>
            </tr>

        </table>
        <div id="LitContent">
            <table id="tab1" width="750">
                <tr>
                    <th width="25%">规则名称</th>
                    <th width="60%">规则编码</th>
                    <th width="15%">
                        <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add'" onclick="AddShow()">新增</a>
                    </th>
                </tr>
            </table>
        </div>
        <div style="text-align: center; padding: 5px">
            <a href="javascript:void(0)" class="easyui-linkbutton" onclick="AddSave();">保存</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" onclick="clearForm()">取消</a>
        </div>
    </div>

    <script type="text/javascript">

        var editingId;

        function AddShow() {

            clearForm();
            var node = $('#tg').treegrid('getSelected');

            if (node == undefined || node == null) {

                alert("请选择节点");
            }
            else {
                $.get("/Handler/MenuHandler.ashx?method=sort", { pcode: node.id }, function (data, textStatus) {
                    if (data != "0") {

                        $('#menusort').numberbox('setValue', data);
                        //$('#menusort').val(data);
                        $('#hid_Method').val('add');

                        $('#w').window('open');
                    } else {
                        reload();
                        clearForm();
                        alert('该数据不存在！');
                    }

                });


            }
        }


        function clearForm() {

            //$('input[id^=chk]:checked').each(function (index) {
            $(":checkbox").each(function () {
                //$(this).attr("checked", false);
                $(this).removeAttr("checked");
            })

            var st = "<table id='tab1' width=\"750\"><tr><th width=\"25%\">规则名称</th>";
            st += "  <th width='60%'>规则编码</th><th width='15%'><img id=\"img1\" onclick=\"addrow(&#39;tab1&#39;,true)\" src=\"../../Images/buttons/add.gif\" style=\"cursor: pointer; width:59px; height:25px;\" /></th>";
            st += " </tr> </table>";

            $("#LitContent").html(st);

            $('#w').window('close');
        }

        function showedit() {
            //clearForm();
            var node = $('#tg').treegrid('getSelected');
            //alert(node)
            if (node == undefined || node == null) {

                alert("请选择节点");
            }
            else {
                $('#w').window('open');
                $('#hid_Method').val('modify');
                $.ajax({
                    type: "post",
                    url: "/Handler/MenuHandler.ashx?method=See&code=" + node.id,
                    data: {},
                    dataType: "text",
                    success: function (str) {
                        var con = "<table id='tab1' width=\"750\"><tr><th width=\"25%\">规则名称</th>";
                        con += "  <th width='60%'>规则编码</th><th width='15%'><img id=\"img1\" onclick=\"addrow(&#39;tab1&#39;,true)\" src=\"../../Images/buttons/add.gif\" style=\"cursor: pointer; width:59px; height:25px;\" /></th></tr> ";
                        if (str.length > 0) {
                            var st = str.split('#');
                            for (var i = 0; i < st.length; i++) {
                                var da = st[i].split('@');
                                con += "<tr><td style='text-align:center'><input  style=\"width:150px\" type=\"text\" id=\"txt_r1c" + (i + 1) + "\"   value=\"" + da[0] + "\"/ ></td><td style='text-align:center'><input style=\"width:400px\" type=\"text\" id=\"txt_r1c" + (i + 1) + "\" value=\"" + da[1] + "\" /></td>";
                                con += "<td style='text-align:center'><img src='../../../Images/buttons/shanChu.gif' style=' cursor:pointer; width:59px; height:25px;' onclick=\"deleterow(this,'tab1')\" /></td></tr>";
                            }
                        }
                        con += " </table>";

                        $("#LitContent").html(con);
                    }
                });

                $.ajax({
                    type: "post",
                    url: "/Handler/MenuHandler.ashx?method=show&code=" + node.id + "&random=" + Math.random() + "",
                    data: {},
                    dataType: "json",
                    success: function (json) {
                        if (json != null) {
                            $('#menuname').textbox('setValue', json.Menu_Name);
                            $('#menuurl').textbox('setValue', json.Menu_Url);
                            $('#menusort').numberbox('setValue', json.Menu_Sequence);
                            $('#hid_MenuID').val(json.MenuID);
                            if (json.Menu_Operation != undefined || json.Menu_Operation != null) {
                                var opt = json.Menu_Operation.split(',');
                                for (var i = 0; i < opt.length; i++) {
                                    $(":checkbox").each(function () {
                                        if ($(this).attr('value') == opt[i]) {
                                            //$(this).attr("checked", "checked");
                                            $(this).prop("checked", true);
                                        }
                                    })
                                }
                            }
                        }
                        else {
                            reload();
                            clearForm();
                            alert('该数据不存在！');
                        }

                    }
                });
            }
        }

        function save() {
            if (editingId != undefined) {
                var t = $('#tg');
                t.treegrid('endEdit', editingId);
                editingId = undefined;
                var persons = 0;
                var rows = t.treegrid('getChildren');
                for (var i = 0; i < rows.length; i++) {
                    var p = parseInt(rows[i].persons);
                    if (!isNaN(p)) {
                        persons += p;
                    }
                }
                var frow = t.treegrid('getFooterRows')[0];
                frow.persons = persons;
                t.treegrid('reloadFooter');
            }
        }

        function reload() {

            jQuery.ajax({
                type: "post",
                async: false,
                url: "/Handler/MenuHandler.ashx?method=load",
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
            var seeCharge1 = GetContent("tab1");
            var path = "";

            var name1 = $('#menuname').val();

            var url1 = $('#menuurl').val();

            var sort1 = $('#menusort').val();

            var pcode1 = $('#tg').treegrid('getSelected').id;

            var opt1 = GetCheckboxValue();

            if ($('#hid_Method').val() == "add") {

                $.get("/Handler/MenuHandler.ashx?method=add", { name: name1, url: url1, sort: sort1, pcode: pcode1, opt: opt1, seeCharge: seeCharge1 },

            function (data, textStatus) {

                if (data == "1") {

                    reload();
                    clearForm();
                    alert('新增菜单成功!');
                }
                else {
                    alert('新增菜单失败!');
                }


            });
            }
            else {

                $.get("/Handler/MenuHandler.ashx?method=modify", { name: name1, url: url1, sort: sort1, id: $('#hid_MenuID').val(), opt: opt1, seeCharge: seeCharge1 },

            function (data, textStatus) {

                if (data == "1") {

                    reload();

                    clearForm();
                    //                    alert(11111111);
                    alert('修改菜单成功!');

                }
                else {

                    alert('修改菜单失败!(该数据也被删除!)');
                }


            });

            }

        }

        function GetCheckboxValue() {

            var str = "";
            $('input:checked').each(function () {

                str += $(this).val() + ',';
            })

            if (str.lastIndexOf(',') == str.length - 1) {

                str = str.substring(0, str.length - 1);
            }
            return str;
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
                    $.get("/Handler/MenuHandler.ashx?method=delete", { pcode: node.id }, function (data, textStatus) {

                        if (data == "1") {


                            reload();
                            alert("删除成功!");
                        }
                        else {

                            alert("该菜单下存在子菜单，请先删除!");
                        }
                    });


                }
            }
        }

    </script>

</body>
</html>
