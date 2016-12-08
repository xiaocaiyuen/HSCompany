<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuditAreaMappingPanel.aspx.cs" Inherits="YDT.Web.Manage.Sys.AuditAreaMappingPanel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Styles/default.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/UI/themes/bootstrap/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/UI/themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="/Scripts/UI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script type="text/javascript">
        function treeSelectd(id, text) {
            //alert($('#ddlRole').combobox('getValue'))
            $('#chart').attr('src', 'AuditAreaMappingSetting.aspx?roleID=' + $('#ddlRole').combobox('getValue') + '&userID=' + id);
        }

        $(document).ready(function () {
            $('#ddlRole').combobox({
                onSelect: function (json) {
                    if(json.value!="")
                    {
                        $('#tt2').tree({
                            loadMsg: '加载中,请等候...',
                            url: '/Handler/AuditAreaRoleUserTree.ashx?Option=RoleUserTree&roleID=' + json.value + '',
                            onClick: function (node) {
                                if ($('#tt2').tree('getParent', node.target)) {//判断是否是叶子节点
                                    treeSelectd(node.id, node.text);
                                }
                                else {
                                    alert('请选择用户节点！');
                                }
                            }
                        });
                    }
                }
                ,
                filter: function (q, row) {
                    var opts = $(this).combobox('options');
                    return row[opts.textField].toLowerCase().indexOf(q.toLowerCase()) >= 0; // 同一转换成小写做比较，==0匹配首位，>=0匹配所有
                }
            });
        })
    </script>
</head>
<body class="easyui-layout" style="overflow-y: hidden;" fit="true" scroll="no">
    <form id="form1" runat="server">
    <div region="west" split="true" title="角色人员" style="width: 210px;">
       <div style="padding-top:5px;padding-left:5px;">
          角色名称：<select id="ddlRole" class="easyui-combobox" name="ddlRole" class="input4" style="width: 130px;">
                       <option></option>
                       <%listRole.ForEach(item =>
                         { %>
                             <option value="<%=item.RoleID %>"><%=item.Role_Name %></option>
                        <%}); %>
                </select>
       </div>
        <hr style="height:1px;border:none;border-top:1px dashed #0066CC;" />
        <div>
           <ul id="tt2">
           </ul>
       </div>
    </div>
    <div region="center" style="overflow-y: hidden; overflow-x: hidden">
        <iframe id="chart" width="100%" height="100%" scrolling="yes" frameborder="0">
        </iframe>
    </div>
    </form>
</body>
</html>