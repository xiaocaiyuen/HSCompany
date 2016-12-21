<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCButton.ascx.cs" Inherits="Shu.Manage.UserControls.UCButton" %>
<input type="hidden" id="CZCD" name="CZCD" />
<table id="dgButton" class="easyui-datagrid" style="height: auto"
    data-options="iconCls: 'icon-edit',
                    toolbar: '#tb',
                    onClickCell: onClickCell,
                    onEndEdit: onEndEdit,
                    fitColumns: true,
                    loadMsg:'数据加载中请稍后……',
                    width:'100%',
                    singleSelect: true">
    <thead>
        <tr>
            <th data-options="field:'Id',width:100,hidden:true">编号</th>
            <th data-options="field:'Name',width:100">按钮名称</th>
            <th data-options="field:'Marker',width:100">按钮标记</th>
            <th data-options="field:'Icon',width:100,formatter:ImageShow" >按钮图标</th>
            <th data-options="field:'Event',width:100">按钮事件</th>
            <th data-options="field:'Type',width:100,
                            formatter:function(value,row){
                                return row.TypeName;
                            },
                            editor:{
                                type:'combobox',
                                options:{
                                valueField:'id',
                                textField:'name',
                                multiple:false,
                                data: [{
			                            id: '0',
			                            name: '工具栏'
		                            },{
			                            id: '1',
			                            name: '列表操作栏'
		                            }]
                            }
                            }">按钮类型</th>
            <th data-options="field:'EventType',width:100,
                            formatter:function(value,row){
                                return row.EventTypeName;
                            },
                            editor:{
                                type:'combobox',
                                options:{
                                valueField:'id',
                                textField:'name',
                                multiple:false,
                                data: [{
			                            id: '1',
			                            name: '链接'
		                            },{
			                            id: '2',
			                            name: '事件'
		                            },{
			                            id: '3',
			                            name: '弹窗'
		                            },{
			                            id: '4',
			                            name: '新页面'
		                            },{
			                            id: '5',
			                            name: '自定义'
		                            }]
                            }
                            }">按钮事件类型</th>
            <th data-options="field:'Sort',width:80,align:'right',editor:'textbox'">排序</th>
        </tr>
    </thead>
</table>

<div id="tb" style="height: auto">
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" onclick="append()">添加</a>
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true" onclick="removeit()">删除</a>
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-save',plain:true" onclick="accept()">保存</a>
</div>

<script type="text/javascript">
    var editIndex = undefined;
    function endEditing() {
        if (editIndex == undefined) { return true }
        if ($('#dgButton').datagrid('validateRow', editIndex)) {
            $('#dgButton').datagrid('endEdit', editIndex);
            editIndex = undefined;
            return true;
        } else {
            return false;
        }
    }
    function onClickCell(index, field) {
        if (editIndex != index) {
            if (endEditing()) {
                $('#dgButton').datagrid('selectRow', index)
                        .datagrid('beginEdit', index);
                var ed = $('#dgButton').datagrid('getEditor', { index: index, field: field });
                if (ed) {
                    ($(ed.target).data('textbox') ? $(ed.target).textbox('textbox') : $(ed.target)).focus();
                }
                editIndex = index;
            } else {
                setTimeout(function () {
                    $('#dgButton').datagrid('selectRow', editIndex);
                }, 0);
            }
        }
    }
    function onEndEdit(index, row) {
        var ed = $(this).datagrid('getEditor', {
            index: index,
            field: 'Type'
        });
        //row.Type = $(ed.target).combobox('getValue');
        row.TypeName = $(ed.target).combobox('getText');
        var ed1 = $(this).datagrid('getEditor', {
            index: index,
            field: 'EventType'
        });
        row.EventTypeName = $(ed1.target).combobox('getText');
    }
    function append() {

        $('#IconButton').window({
            title: '按钮图标',
            width: 600,
            height: 400,
            collapsible: false,
            minimizable: false,
            maximizable: false,
            draggable: false,
            resizable: false,
            modal: true
        });

        $('#view_IconMenuButton').attr('src', "/Popup/IconMenuButton.aspx");

        
    }
    function removeit() {
        if (editIndex == undefined) { return }
        $('#dgButton').datagrid('cancelEdit', editIndex).datagrid('deleteRow', editIndex);
        editIndex = undefined;
    }
    function accept() {
        if (endEditing()) {
            $('#dgButton').datagrid('acceptChanges');
        }
    }

    //显示为图片
    var ImageShow = function (value, rec, index) {
        var strhtml = "<img src=\"" + value + "\" onerror=\"this.src='/Content/themes/icons/large_picture.png'\" style=\"vertical-align: middle; padding-right: 10px;\" />";
        return strhtml;
    };

</script>
