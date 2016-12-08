<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractTemplate.aspx.cs" Inherits="YDT.Web.Manage.Sys.ContractTemplate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <link href="/Scripts/UI/themes/bootstrap/easyui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Scripts/UI/jquery.easyui.min.js"></script>
    <link href="/Styles/table.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/List.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/common.js"></script>
    <script>
        function Upfile(filename)
        {
           
            $('#ui_Import').dialog('open');
           // $('#ui_Import').parent().appendTo($("form:first"));
            $('#view_Import').attr("src", "/Manage/Sys/TemplateImport.aspx?temp=" + filename);
            $('#view_Import').show();
            
        }
        function colsd() {
            $('#ui_Import').dialog('close');
           
            window.location.href = window.location.href;
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">

    <div>
         <div id="titleScore" class="easyui-panel" title="合同模板管理" style="width: auto; min-width: 800px; height: auto; text-align: center">
             
                <table id="tbSetting1" border="0" cellpadding="0" cellspacing="1" class="tab" width="100%">
                  <tr >
                     
                     <th style="width:20%; display:none;">模板类别</th>
                     <th style="width:30%">模板名称</th>
                     <th style="width:15%">电子签章类别</th>
                     <th style="width:20%">电子签章关联</th>
                     <th style="width:15%; ">操作</th>
                 </tr>
                    <asp:Literal ID="LitContent1" runat="server"></asp:Literal>
                </table>
                
            </div>
     
      
    </div>
    </form>
    <div id="ui_Import" class="easyui-window" title="合同模板上传" data-options="modal:true,closed:true,iconCls:'icon-save'"  style="width: 400px; height: 240px; padding: 10px; top: 150px;">
   <%-- <div id="ui_Import" class="easyui-window" title="合同模板上传" style="width: 400px; height: 230px; display:none" 
        toolbar="#dlg-toolbar"  >--%>
        <iframe id="view_Import" scrolling="yes"  width="100%"  frameborder="0" height="180px" ></iframe>
    </div>
</body>
</html>
