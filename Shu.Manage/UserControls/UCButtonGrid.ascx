<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCButtonGrid.ascx.cs"
    Inherits="Shu.Manage.UserControls.UCButtonGrid" %>
<script type="text/javascript" src="/Scripts/jquery-2.0.0.min.js"></script>
<link href="/Content/themes/bootstrap/easyui.css" rel="stylesheet" />
<link href="/Content/themes/icon.css" rel="stylesheet" />
<link href="/Content/Icons/iconButton.css" rel="stylesheet" />
<script type="text/javascript" src="/Scripts/jquery.easyui-1.4.5.min.js" charset="utf-8"></script>
<script src="/Scripts/jquery.datagrid.js"></script>
<script type="text/javascript" src="/Scripts/locale/easyui-lang-zh_CN.js" charset="utf-8"></script>
<script type="text/javascript" src="/Scripts/DatagridPublic.js" charset="utf-8"></script>
<script type="text/javascript" src="/Scripts/json2.min.js" charset="utf-8"></script>
<%--<script src="/Scripts/EasyuiDatagridExcel.js" type="text/javascript"></script>
<script src="/Scripts/EasyuiDatagrid.js" type="text/javascript"></script>--%>
<style>
    .datagrid-cell-rownumber {
        width: 50px;
        text-align: center;
        margin: 0px;
        padding: 3px 0px;
        color: #000;
    }

    .datagrid-header-rownumber {
        width: 50px;
        text-align: center;
        margin: 0px;
        padding: 3px 0px;
    }
</style>
<%var setTableKey = SetKey();
    var vsetTableKey = vSetKey();
    var currenturl = CurrentUrl;
    var imageURL = GetGridAttr("ImageURL");
    List<Shu.Model.Sys_MenuOperatingButton> ToolbarList = ToolbarButton();
    List<Shu.Model.Sys_MenuOperatingButton> RoleSetList = RoleSetButton();
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
            {  <%=m==RowClickNum? "formatter:hyperlink,":"" %> <%=e.Attribute("IsImage")!=null?e.Attribute("IsImage").Value.ToBoolean(false)==true ? "formatter:ImageShow,":"":"" %>  <%=e.Attribute("width")!=null? "width:"+e.Attribute

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
    <% int cout = ToolbarList.Count();
    int i = 0;
    ToolbarList.ForEach(item =>
    {
    i++;
    if (item.EventType != 5)
    { %>
       {
           id:'<%=item.Marker %>',
           text: '<%=item.Name %>',
           iconCls: 'icon-<%=item.IconName %>',
           handler: function () {OperatingButton('<%=currenturl %>',getSelections(), '','<%=item.ToJson().Replace("\"","|")%>');}
       }
      <% if (cout != i)
    {%>
        ,
        <%} %>
    <%}
    else
    { %>
      {
          id:'<%=item.Marker %>',
          text: '<%=item.Name %>',
          iconCls: 'icon-<%=item.IconName %>',
          handler: function () { DeleteData('<%=currenturl %>','<%=item.Marker %>', getSelections(),''); }
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
        $('#form1')[0].reset();
        $('#grid').datagrid({ queryParams: null });
        //window.location.href=document.URL;
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
        var count = selected.length;
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
<div id="divUCEasyUIDataGrid" style="float: left; width: 99%; margin-left: 5px;">
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
    RoleSetList.ForEach(item =>
    {
    if (item.EventType == 5)
    {%>
        var btn = btn + '<a id="<%=item.Marker %>_'+index+'" class="<%=item.Marker %>" onclick="DataEval(\'<%=currenturl %>\',\'<%=item.Marker %>\',\'' + rec.<%=TableKey %> + '\',\'<%=vsetTableKey %>\')" href="javascript:void(0)"><%=item.Name %></a>';
    <%
    }
    else
    { %>
        var btn = btn + '<a id="<%=item.Marker %>_'+index+'" class="<%=item.Marker %>" onclick="OperatingButton(\'<%=currenturl %>\',\'' + rec.<%=TableKey %> + '\',\'<%=vsetTableKey %>\',\'<%=item.ToJson().Replace("\"","|")%>\')" href="javascript:void(0)"><%=item.Name %></a>';
        <%}
    }); %>
        return btn;
    };

    //初始化自定义操作按钮
    var onLoadSuccess = function (data) {
        
        <%RoleSetList.ForEach(item =>
    {%>
        $('.<%=item.Marker %>').linkbutton({ text: '<%=item.Name %>', plain: true,iconCls: 'icon-<%=item.IconName %>' });
        <%}); %>

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
    };
    //超链接
    var hyperlink=function (value, rec,index) {
        return "<a class=\"\" href='javascript:void(0)' onclick=\"ShowData('<%=GetGridAttr("DetailURL")%>','4','" + rec.<%=TableKey %> + "',\'<%=setTableKey %>\')\">"+value+"</a>"; 
    };

    //显示为图片
    var ImageShow=function (value, rec,index) {
        var strhtml = "<img src=\""+value+"\" onerror=\"this.src='/Content/themes/icons/large_picture.png'\" style=\"vertical-align: middle; padding-right: 10px;\" />"; 
        return strhtml;
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
        <%if (currenturl != "DecomposeDetailList.aspx")
    {%>
        if(index%2==1)
            //return 'background:#CCCCCC';
            return 'background:rgb(235, 239, 242);color:Black;font-weight:bold;'; 
        <%}%>
    }
</script>
