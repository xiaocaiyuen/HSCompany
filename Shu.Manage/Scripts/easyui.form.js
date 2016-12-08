/**
*表单提交方法（post）（支持easyui）
*ActionDefinitionID:动作按钮编号
*ActionDefinitionName:动作按钮名称
*IsStartWF:是否启动工作流引擎（true:启动，false：不启动。）
*/
function submitForm(ActionDefinitionID, ActionDefinitionName, IsStartWF) {
    $.messager.progress();	// 显示进度条
    $('#form1').form('submit', {
        ajax: true,
        onSubmit: function (param) {
            param.ActionDefinitionID = ActionDefinitionID;
            param.ActionDefinitionName = ActionDefinitionName;
            param.IsStartWF = IsStartWF;

            if (IsStartWF) {
                var isValid = $(this).form('enableValidation').form('validate');
                if (!isValid) {
                    $.messager.progress('close');	// 如果表单是无效的则隐藏进度条
                }
                return isValid;	// 返回false终止表单提交
            }
        },
        success: function (data) {
            $.messager.progress('close');	// 如果提交成功则隐藏进度条
            var obj = jQuery.parseJSON(data);
            if (obj.error) {
                $.messager.alert('提示', obj.msg, 'info');
            }
            else {
                $.messager.alert('提示', obj.msg, 'info', function () {
                    if (IsStartWF) {
                        history.go(-1);
                        //window.parent.Closetab();
                        //parent.location.reload();
                    }
                    else {
                        //loadRemote();//表单重新赋值
                        Copy(obj.Data);
                    }
                });
            }
        },
        error: function (err) {
            $.messager.alert('错误', err, 'error');
        }
    });
}

/**表单复制方法（get）（支持easyui）*/
function loadRemote() {
    var applyjson = {};
    $.ajax({
        type: 'get',
        dataType: 'json',
        async: false,
        url: location.href + '&method=get',
        success: function (msg) {
            applyjson = msg;
            //msg 返回值： （如果不为空这表示附件数量不够，为空则通过）
            //msg格式： 类别名字+“_”+缺少数量+“|”+类别名字+“_”+缺少数量.........
        }
    });

    $('#form1').form('load', applyjson);
}

/**清空所有表单数据（支持easyui）*/
function clearForm() {
    $('#form1').form('clear');
}

/**表单自定义赋值
 *row：自定义的json数据源
*/
function Copy(row) {
    $('#form1').form('load', row);
}


/**更新ckeditor编辑器，修复提交From表单的问题
*/
function CKupdate() {
    for (instance in CKEDITOR.instances)
        CKEDITOR.instances[instance].updateElement();
}