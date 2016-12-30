<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModuleInfo.aspx.cs" Inherits="Shu.Manage.Sys.ModuleInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>首次登记</title>
    <link href="/Styles/table.css" rel="stylesheet" />
    <script type="text/javascript" src="/Scripts/jquery-2.0.0.min.js"></script>
    <link href="/Content/themes/bootstrap/easyui.css" rel="stylesheet" />
    <link href="/Content/themes/icon.css" rel="stylesheet" />
    <script type="text/javascript" src="/Scripts/jquery.easyui-1.4.5.min.js"></script>
    <script type="text/javascript" src="/Scripts/locale/easyui-lang-zh_CN.js"></script>
    <script src="/Scripts/jquery.extend.js" type="text/javascript"></script>
    <script src="/Scripts/easyui.form.js" type="text/javascript"></script>

    <script src="/Scripts/ckeditor/ckeditor.js"></script>
    <script src="/Scripts/ckeditor/adapters/jquery.js"></script>
</head>
<body>
    <form id="form1" method="post">
        <div id="tt" class="easyui-tabs" data-options="selected:0">
            <div title="基本信息" style="padding: 10px">
                <table border="0" cellpadding="1" cellspacing="1" class="tab">
                    <tr>
                        <th >模块名称</th>
                        <td colspan="3">
                            <input id="Name" name="Name" class="easyui-textbox" style="width: 98%;" data-options="required:true,readonly:false" />
                        </td>
                    </tr>
                    <tr>
                        <th style="width: 15%;">是否授权</th>
                        <td style="width: 35%;">
                            <select id="IsAccredit" class="easyui-combobox" name="IsAccredit" style="width: 95%;" data-options="editable:false" validtype="ComRequired['--请选择--']" required="required">
                                <option selected="selected" value="true">是</option>
                                <option value="false">否</option>
                            </select>
                        </td>
                        <th style="width: 15%;">授权IP</th>
                        <td style="width: 35%;">
                            <input id="IPAddress" class="easyui-textbox" name="IPAddress" style="width: 95%;" data-options="editable:true" required="required" />
                        </td>
                    </tr>
                    <tr>
                        <th style="width: 15%;">域名地址</th>
                        <td style="width: 35%;">
                            <input id="DomainUrl" name="DomainUrl" class="easyui-textbox" style="width: 95%;" value="0" data-options="required:true,readonly:false" />
                        </td>
                        <th style="width: 15%;">排序</th>
                        <td style="width: 35%;">
                            <input id="Sort" name="Sort" class="easyui-numberspinner" style="width: 95%;" value="0" data-options="required:true,readonly:false" />
                        </td>
                    </tr>
                    <tr>
                        <th>备注</th>
                        <td colspan="3">
                            <input class="easyui-textbox" id="Remark" name="Remark" data-options="multiline:true" style="width: 98%; height: 100px" />
                        </td>
                    </tr>
                </table>
            </div>
            <div title="详细内容" style="padding: 10px">
                <textarea class="easyui-validatebox" id="Content" name="Content" style="width:98%"></textarea>
            </div>
        </div>

        <div style="margin: 20px 0;"></div>
        <div id="p" class="easyui-panel" title="按钮" data-options="collapsible:false">
            <div style="text-align: center;">
    
                <a href="javascript:void(0)" onclick="CKupdate();submitForm('WORKFLOW-ACTI-0000-0000-000000000008','保存',false);" class="easyui-linkbutton" data-options="iconCls:'icon-save'">保存</a>&nbsp;&nbsp;&nbsp;
                
                <a href="javascript:void(0)" onclick="CKupdate();submitForm('WORKFLOW-ACTI-0000-0000-000000000001','提交',true);" class="easyui-linkbutton" data-options="iconCls:'icon-ok'">提交</a>&nbsp;&nbsp;&nbsp;                <a href="javascript:void(0)" onclick="clearForm()" class="easyui-linkbutton" data-options="iconCls:'icon-reload'">重置</a>&nbsp;&nbsp;&nbsp;                <a href="javascript:void(0)" onclick="window.parent.Closetab()" class="easyui-linkbutton" data-options="iconCls:'icon-no'">取消</a>

            </div>
        </div>
    </form>
    <script type="text/javascript">
        $(function () {
            loadRemote();
            $('#Content').ckeditor();//文本编辑器
        });
    </script>
</body>
</html>
