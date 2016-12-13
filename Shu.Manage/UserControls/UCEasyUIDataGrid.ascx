<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCEasyUIDataGrid.ascx.cs"
    Inherits="Shu.Manage.UserControls.UCEasyUIDataGrid" %>
<script type="text/javascript" src="/Scripts/jquery-2.0.0.min.js"></script>
<link href="/Content/themes/bootstrap/easyui.css" rel="stylesheet" />
<link href="/Content/themes/icon.css" rel="stylesheet" />
<script type="text/javascript" src="/Scripts/jquery.easyui-1.4.5.min.js" charset="utf-8"></script>
<script src="/Scripts/jquery.datagrid.js"></script>
<script type="text/javascript" src="/Scripts/locale/easyui-lang-zh_CN.js" charset="utf-8"></script>


<script type="text/javascript" src="/Scripts/public.js" charset="utf-8"></script>
<script type="text/javascript" src="/Scripts/json2.min.js" charset="utf-8"></script>
<%--<script src="/Scripts/EasyuiDatagridExcel.js" type="text/javascript"></script>
<script src="/Scripts/EasyuiDatagrid.js" type="text/javascript"></script>--%>
<style> .datagrid-cell-rownumber{ width:50px; text-align:center; margin:0px; padding:3px 0px; color:#000; } .datagrid-header-rownumber{ width:50px; text-align:center; margin:0px; padding:3px 0px; } </style>
<%var setTableKey = SetKey();
  var vsetTableKey = vSetKey();
  var currenturl = CurrentUrl;
  var imageURL = ImageURL;
%>
<script type="text/javascript">
    window.onload=function(){
        $('#grid').datagrid({
            onDblClickRow: function (rowIndex, rowData) {  
                <%if (IsDbOnClickRow)
                  {%>  
                parent.returnRowsValues(rowIndex,rowData); 
              <%}
                  else
                  {%>
                returnRowsValues(rowIndex,rowData); 
               <%}%>
            },
            url:'<%=currenturl.Contains("?")==true? currenturl+"&":currenturl+"?" %>active=List',
            method:'post',
            iconcls:'icon-save',
            collapsible:true,
            rownumbers:true,
            fitColumns: true,
            //width:'100%',
            width:function(){return document.body.clientWidth*0.9},
            height:'411',
            pagination:true,pageNumber:1,pageSize:12,pageList:[12,24,36,100],
            singleSelect:false,selectOnCheck:true,CheckOnSelect:false,
            toolbar:toolbar,onLoadSuccess:onLoadSuccess,rowStyler:rowStyler,
            queryParams:search('form1'),
            columns:[[
        <%if (IsShowCheckBox)
          {%>
            { field:'<%=TableKey %>',checkbox:true},
                <%} %>
            
            <%  int m = 0;
                foreach (XElement e in Node)
                { %>
            {  <%=m==RowClickNum? "formatter:hyperlink,":"" %> <%=m==RowImageNum? "formatter:imagelink,":"" %> <%=e.Attribute("width")!=null? "width:"+e.Attribute

("width").Value+",":""%>sortable:true, align:'center',field:'<%=e.Attribute("Key").Value%>', <%=e.Attribute("formatter")!=null? "formatter:"+e.Attribute("formatter").Value+",":""%><%=e.Attribute("styler")!=null? "styler:"+e.Attribute("styler").Value+",":""%>
                title:'<%=e.Attribute("Name").Value%>',<%=e.Attribute("Hidden")!=null? "hidden:true":""%>
            },
            <%m++;
                } %>
           {field:'caozuo',formatter:cat,title:'操作',align:'center'}
                
            ]]
        });
    };
var toolbar = [
    <% int cout = ToolbarButton().Count();
       int i = 0;
       ToolbarButton().ForEach(item =>
        {
            i++;
            if (item.Type == 1)
            {
    %>
        {
            id:'<%=item.Id %>',
            text: '<%=item.Name %>',
            iconCls: 'icon-<%=item.Icon %>',
            handler: function () { location.href = '<%=item.Url %>';return false;}
        }
        <% if (cout != i)
           {%>
        ,
        <%} %>
    <%}
            else if (item.Type == 2)
            { %>
       {
           id:'<%=item.Id %>',
           text: '<%=item.Name %>',
           iconCls: 'icon-<%=item.Icon %>',
           handler: function () { DeleteData('<%=currenturl %>','<%=item.Id %>', getSelections(),''); }
       }
      <% if (cout != i)
         {%>
        ,
        <%} %>
    <%}
            else if (item.Type == 3)
            { %>
       {
           id:'<%=item.Id %>',
           text: '<%=item.Name %>',
           iconCls: 'icon-<%=item.Icon %>',
           handler: function () {
              
               $('#ui_<%=item.Id %>').css('display','');
               $('#ui_<%=item.Id %>').dialog('open');
               $('#view_<%=item.Id %>').attr('src', '<%=item.Url%><%= item.Url==null? (item.Url.Contains("?")==true? "&":"?"):"?" %>'+GetUrlQueryString()+'&id='+getSelections()+'&type=<%=item.Id %>&json='+JSON.stringify(search('form1')));
           }
       }
        <% if (cout != i)
           {%>
        ,
        <%} %>
     <%}
            else if (item.Type == 5)
            { %>
       {
           id:'<%=item.Id %>',
           text: '<%=item.Name %>',
           iconCls: 'icon-<%=item.Icon %>',
           handler: function () {DataEval('<%=currenturl %>','<%=item.Id %>', getSelections(),'','<%=Ispaf %>');}
       }
      <% if (cout != i)
         {%>
        ,
        <%} %>
     <%}
            else if (item.Type == 99)
            { %>
       {
           id:'<%=item.Id %>',
           text: '<%=item.Name %>',
           iconCls: 'icon-<%=item.Icon %>',
           handler: function () { CheckOff('<%=currenturl %>','<%=item.Id %>', getSelections(),''); }
       }
      <% if (cout != i)
         {%>
        ,
        <%} %>
     <%}
      
            else if (item.Type == 98)
            { %>
       {
           id:'<%=item.Id %>',
           text: '<%=item.Name %>',
           iconCls: 'icon-<%=item.Icon %>',
           handler: function () { SendSms('<%=currenturl %>','<%=item.Id %>', getSelections(),''); }
       }
      <% if (cout != i)
         {%>
        ,
        <%} %>
    <%}else if (item.Type == 97)
            { %>
       {
           id:'<%=item.Id %>',
           text: '<%=item.Name %>',
           iconCls: 'icon-<%=item.Icon %>',
           handler: function () { Submit('<%=currenturl %>','<%=item.Id %>', getSelections(),''); }
       }
       <% if (cout != i)
         {%>
        ,
        <%} %>
    <%}else if (item.Type == 96)
            { %>
       {
           id:'<%=item.Id %>',
           text: '<%=item.Name %>',
           iconCls: 'icon-<%=item.Icon %>',
           handler: function () { OpenRate('<%=currenturl %>','<%=item.Id %>', getSelections()); }
       }
       <% if (cout != i)
         {%>
        ,
        <%} %>
    <%}else if (item.Type == 95)
            { %>
       {
           id:'<%=item.Id %>',
           text: '<%=item.Name %>',
           iconCls: 'icon-<%=item.Icon %>',
           handler: function () { PostLock('<%=currenturl %>','<%=item.Id %>', getSelections(),''); }
       }
       <% if (cout != i)
           {%>
        ,
        <%} %>
    <%}
            else if (item.Type == 93)
            { %>
       {
           id:'<%=item.Id %>',
           text: '<%=item.Name %>',
           iconCls: 'icon-<%=item.Icon %>',
           handler: function () { RevokeFenJie('<%=currenturl %>','<%=item.Id %>', getSelections(),''); }
       }
      <% if (cout != i)
         {%>
        ,
        <%} %>
    <%}

    else if (item.Type == 104)//导出excel
      { %>
       {
           id:'<%=item.Id %>',
           text: '<%=item.Name %>',
           iconCls: 'icon-<%=item.Icon %>',
           handler: function () { ExportExcel('<%=item.Url %>'); }
       }
      <% if (cout != i)
         {%>
        ,
        <%} %>
    <%}

            else
            { %>
      {
          id:'<%=item.Id %>',
          text: '<%=item.Name %>',
          iconCls: 'icon-<%=item.Icon %>',
          handler: function () { DeleteData('<%=currenturl %>','<%=item.Id %>', getSelections(),''); }
      }
      <% if (cout != i)
         {%>
        ,
        <%} %>
    <%}
        }); %>
];
    $("#btn_Search").click(function () {
        $('#grid').datagrid({ queryParams: search("form1") });   //点击搜索
    });

    $("#btnChongzhi").click(function () {
        //        $('#form1')[0].reset();  
        //        $('#grid').datagrid({ queryParams: null });   //点击搜索
        window.location.href=document.URL;
    });

    //将表单数据转为json
    function search(id) {
        var arr = $("#" + id).serializeArray();
        if(arr=="")
        {
            return "";
        }
        else
        {
            var jsonStr = "";
            jsonStr += '{';
            for (var i = 0; i < arr.length; i++) {
                jsonStr += '"' + arr[i].name + '":"' + arr[i].value + '",'
            }
            jsonStr = jsonStr.substring(0, (jsonStr.length - 1));
            jsonStr += '}'
            var json = JSON.parse(jsonStr);
            return json
        }
    }

    //获取选中行id
    function getSelections() {
        var selected = $('#grid').datagrid('getSelections');
        var count = selected.length;            //acquire the count of the selected rows  
        var ids = '';
        $.each(selected, function (i, rowval) {
            if (i)
                ids += ',';
            ids += rowval.<%=TableKey %>;
        })
        return ids;
    }
    function GridOptionShowDeleteToAdmin() {
        var currUserRole = '<%=bg.CurrUserInfo().RoleName %>';
        if (currUserRole.indexOf("系统管理员") >= 0) {
            $('.DelButton').linkbutton({ disabled: false });
        }
    }

    function GetUrlQueryString()
    {
        var url=String(window.document.location.href);
        var allargs = url.split("?")[1];
        if(allargs)
            return allargs;
        else
            return "";
    }
    $('#grid').datagrid('resize', {
        width:function(){return document.body.clientWidth*0.9;},
        height:411
    });
</script>
<div id="divUCEasyUIDataGrid" style="float: left; width: 99%; margin-left:5px;">
<table id="grid">
</table>
</div>
<script type="text/javascript">
    function resizeGrid(){
        $('#grid').datagrid('resize', {
            width:function(){return document.body.clientWidth*0.9;},
            height:411
        });
    }
    //自定义操作按钮
    var cat = function (value, rec,index) {
        var btn="";
        <%
    RoleSetButton().ForEach(item =>
    {
        if (item.Type == 1010) {
        %>   
          var btn = btn + '<a id="<%=item.Id %>_'+index+'" class="<%=item.Id %>" onclick="MyMyDesktopShowData(\'<%=item.Url %>\',\'<%=item.Type %>\',\'' + rec.<%=TableKey %> + '\',\'<%=vsetTableKey %>\')" href="###"><%=item.Name %></a>';
        <%}
        else if (item.Type == 1 || item.Type == 4 || item.Type == 6)
        {
        %>
           var btn = btn + '<a id="<%=item.Id %>_'+index+'" class="<%=item.Id %>" onclick="ShowData(\'<%=item.Url %>\',\'<%=item.Type %>\',\'' + rec.<%=TableKey %> + '\',\'<%=vsetTableKey %>\')" href="###"><%=item.Name %></a>';

        <%}
        else if (item.Type == 2)
        { %>
           <%if (item.Name == "下载")
             { %>
        var btn = btn + '<a id="<%=item.Id %>_'+index+'" class="<%=item.Id %>" onclick="Download(\'<%=DownloadURL %>\',\'' + rec.<%=TableKeyField %> + '\')" href="javascript:void(0)"><%=item.Name %></a>';
        <%}
           else if (item.Name == "撤销分解")
             { %>
        var btn = btn + '<a id="<%=item.Id %>_'+index+'" class="<%=item.Id %>" onclick="RevokeFenJie(\'<%=currenturl %>\',\'<%=item.Id %>\',\'' + rec.<%=TableKey %> + '\',\'<%=vsetTableKey %>\')" href="javascript:void(0)"><%=item.Name %></a>';
        <%}
             else
             { %>
        var btn = btn + '<a id="<%=item.Id %>_'+index+'" class="<%=item.Id %>" onclick="DeleteData(\'<%=currenturl %>\',\'<%=item.Id %>\',\'' + rec.<%=TableKey %> + '\',\'<%=vsetTableKey %>\')" href="javascript:void(0)"><%=item.Name %></a>';
        <%} %>

        <%}
        else if (item.Type == 3)
        {%>
        var btn = btn + '<a id="<%=item.Id %>_'+index+'" class="<%=item.Id %>" onclick="javascript:$(\'#ui_<%=item.Id %>\').dialog(\'open\');$(\'#view_<%=item.Id %>\').attr(\'src\', \'<%=item.Url.Contains('?')==true? item.Url+"$":item.Url+"?"%>id='+rec.<%=TableKey %>+'<%=vsetTableKey %>'+'&type=<%=item.Id %>&json='+JSON.stringify(search('form1')).replace(/"/g, '|')+'\');" href="javascript:void(0)"><%=item.Name %></a>';

       <%}
        else if (item.Type == 99)
        {%>
        var btn = btn + '<a id="<%=item.Id %>_'+index+'" class="<%=item.Id %>" onclick="CheckOff(\'<%=currenturl %>\',\'<%=item.Id %>\',\'' + rec.<%=TableKey %> + '\',\'<%=vsetTableKey %>\')" href="javascript:void(0)"><%=item.Name %></a>';

        <%}
        else if (item.Type == 94)
        {%>
        var btn = btn + '<a id="<%=item.Id %>_'+index+'" class="<%=item.Id %>" onclick="CancelCheck(\'<%=currenturl %>\',\'<%=item.Id %>\',\'' + rec.<%=TableKey %> + '\',\'<%=vsetTableKey %>\')" href="javascript:void(0)"><%=item.Name %></a>';

        <% }
        else if (item.Type == 97) { %>
        var btn = btn + '<a id="<%=item.Id %>_'+index+'" class="<%=item.Id %>" onclick="Submit(\'<%=currenturl %>\',\'<%=item.Id %>\',\'' + rec.<%=TableKey %> + '\',\'<%=vsetTableKey %>\')" href="javascript:void(0)"><%=item.Name %></a>';
        <%}
        else if (item.Type == 96) { %>
        var btn = btn + '<a id="<%=item.Id %>_'+index+'" class="<%=item.Id %>" onclick="OpenRate(\'<%=currenturl %>\',\'<%=item.Id %>\',\'' + rec.<%=TableKey %> + '\',\'<%=vsetTableKey %>\')" href="javascript:void(0)"><%=item.Name %></a>';
        <%}
        else if (item.Type == 95) { %>
        var btn = btn + '<a id="<%=item.Id %>_'+index+'" class="<%=item.Id %>" onclick="PostLock(\'<%=currenturl %>\',\'<%=item.Id %>\',\'' + rec.<%=TableKey %> + '\',\'<%=vsetTableKey %>\')" href="javascript:void(0)"><%=item.Name %></a>';
        <%}
        else if (item.Type == 101) { %>
        var btn = btn + '<a id="<%=item.Id %>_'+index+'" class="<%=item.Id %>" onclick="UserAccountSettings(\'<%=currenturl %>\',\'<%=item.Id %>\',\'' + rec.<%=TableKey %> + '\',\'<%=vsetTableKey %>\',\'0\')" href="javascript:void(0)"><%=item.Name %></a>';
        <%}
        else if (item.Type == 102) { %>
        var btn = btn + '<a id="<%=item.Id %>_'+index+'" class="<%=item.Id %>" onclick="UserAccountSettings(\'<%=currenturl %>\',\'<%=item.Id %>\',\'' + rec.<%=TableKey %> + '\',\'<%=vsetTableKey %>\',\'1\')" href="javascript:void(0)"><%=item.Name %></a>';
        <%}
        else if (item.Type == 103) { %>
        var btn = btn + '<a id="<%=item.Id %>_'+index+'" class="<%=item.Id %>" onclick="UserAccountSettings(\'<%=currenturl %>\',\'<%=item.Id %>\',\'' + rec.<%=TableKey %> + '\',\'<%=vsetTableKey %>\',\'2\')" href="javascript:void(0)"><%=item.Name %></a>';
        <%}
        else
        { %>
        var btn = btn + '<a id="<%=item.Id %>_'+index+'" class="<%=item.Id %>" onclick="DataEval(\'<%=currenturl %>\',\'<%=item.Id %>\',\'' + rec.<%=TableKey %> + '\',\'<%=vsetTableKey %>\')" href="javascript:void(0)"><%=item.Name %></a>';

        <%}
    }); %>
        return btn;
    };

    //初始化自定义操作按钮
    var onLoadSuccess = function (data) {
        
        <%RoleSetButton().ForEach(item =>
          {%>
        $('.<%=item.Id %>').linkbutton({ text: '<%=item.Name %>', plain: true,iconCls: 'icon-<%=item.Icon %>' });
        <%}); %>

         <%if( currenturl == "DecomposeDetailList.aspx")
           {%>
        $(this).datagrid('autoMergeCells', ['ParagraphManage_FundNumber']);
         <%}%>

        //按钮权限设置
        try
        {
            $.each(data.rows,function(i,rows){
                BtnDisabled(i,rows);
            });
        }
        catch(e)
        {
          
        }

        GridOptionShowDeleteToAdmin();//系统管理员权限


        //        var getRows = $("#grid").datagrid("getRows");
        //            $.each(getRows, function (i, rowval) {
        //        });
    };
    //超链接
    var hyperlink=function (value, rec,index) {
        return "<a class=\"\" href='javascript:void(0)' onclick=\"ShowData('<%=DetailURL%>','4','" + rec.<%=TableKey %> + "',\'<%=setTableKey %>\')\">"+value+"</a>"; 
    };


    //图片
    var imagelink=function (value, rec,index) {
        var id=rec.<%=TableKey %>;
        var strhtml = "<a class=\"\" href='javascript:void(0)' onclick=\"ImageOpen(\'"+id+"\')\">"; 
        strhtml=strhtml+imageUrl(value);
        strhtml=strhtml+"</a>"
        return strhtml;
    };

    <%if (imageURL != null)
      {%>
    function ImageOpen(id)
    {
        $('#ui_image').dialog('open');
        $('#view_image').attr('src', '<%=imageURL%><%= imageURL==null? (imageURL.Contains("?")==true? "&":"?"):"?" %>id='+id);
    }
    <%} %>



    //行样式
    var rowStyler=function(index,row){
        <%if( currenturl != "DecomposeDetailList.aspx"){%>
            if(index%2==1)
                //return 'background:#CCCCCC';
                return 'background:rgb(235, 239, 242);color:Black;font-weight:bold;'; 
        <%}%>
    }
    //选择行样式
    //    var onSelect=function(index,row)
    //    {
    //       $(this).datagrid('styler', '.datagrid-row-selected');
    //    }

    //    var loadFilter=function(data){
    //            
    //			return data;
    //    };
</script>
