﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OperatingButtonInfo.aspx.cs" Inherits="Shu.Manage.Sys.OperatingButtonInfo" %>

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

    <%--<script src="/Scripts/ckeditor/ckeditor.js"></script>
    <script src="/Scripts/ckeditor/adapters/jquery.js"></script>--%>
</head>
<body>
    <form id="form1" method="post">
        <div id="tt" class="easyui-tabs" data-options="selected:0">
            <div title="基本信息" style="padding: 10px">
                <table border="0" cellpadding="1" cellspacing="1" class="tab">
                    <tr>
                        <th>按钮名称</th>
                        <td>
                            <input id="Name" name="Name" class="easyui-textbox" style="width: 98%;" data-options="required:true,readonly:false" />
                        </td>
                    </tr>
                    <tr>
                        <th>按钮标记</th>
                        <td>
                            <input id="Marker" name="Marker" class="easyui-textbox" style="width: 98%;" data-options="required:true,readonly:false" />
                        </td>
                    </tr>
                    <tr>
                        <th>按钮图标</th>
                        <td>
                            <input name="Icon" type="hidden" id="Icon" />
                            <img src="/Themes/Images/illustration.png" onerror="" id="Img_Button_Img" style="vertical-align: middle; padding-right: 10px;" />
                            <a href="javascript:void(0)" onclick="SelectOpenImg()" class="easyui-linkbutton" data-options="iconCls:'icon-no'">选择图标</a>
                        </td>
                    </tr>
                    <tr>
                        <th>按钮事件</th>
                        <td>
                            <input id="Event" name="Event" class="easyui-textbox" style="width: 98%;" data-options="required:true,readonly:false" />
                        </td>
                    </tr>
                    <tr>
                        <th>按钮类型</th>
                        <td>
                            <select id="Type" class="easyui-combobox" name="Type" style="width: 98%;" data-options="editable:false" validtype="ComRequired['--请选择--']" required="required">
                                <option value="0" selected="selected">工具栏</option>
                                <option value="1">列表操作栏</option>
                                <%--valueField:'DATADICT_CODE',textField:'DATADICT_NAME',url:'/Handler/AsynDeptTree.ashx?Option=DataDict&Code=1502',--%>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <th>按钮事件类型</th>
                        <td>
                            <select id="EventType" class="easyui-combobox" name="EventType" style="width: 98%;" data-options="editable:false" validtype="ComRequired['--请选择--']" required="required">
                                <option value="1" selected="selected">链接</option>
                                <option value="2">事件</option>
                                <option value="3">弹窗</option>
                                <option value="4">新页面</option>
                                <option value="5">自定义</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <th style="width: 30%;">排序</th>
                        <td style="width: 70%;">
                            <input id="Sort" name="Sort" class="easyui-numberspinner" style="width: 98%;" value="0" data-options="required:true,readonly:false" />
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
        </div>

        <div style="margin: 20px 0;"></div>
        <div id="p" class="easyui-panel" title="按钮" data-options="collapsible:false">
            <div style="text-align: center;">

                <%--<a href="javascript:void(0)" onclick="submitForm('WORKFLOW-ACTI-0000-0000-000000000008','保存',false);" class="easyui-linkbutton" data-options="iconCls:'icon-save'">保存</a>--%>&nbsp;&nbsp;&nbsp;
                
                <a href="javascript:void(0)" onclick="submitForm('WORKFLOW-ACTI-0000-0000-000000000001','提交',true);" class="easyui-linkbutton" data-options="iconCls:'icon-ok'">提交</a>&nbsp;&nbsp;&nbsp;

            </div>
        </div>
    </form>
    <script type="text/javascript">
        $(function () {
            loadRemote();
            //$('#Content').ckeditor();//文本编辑器
        });
    </script>
</body>
</html>