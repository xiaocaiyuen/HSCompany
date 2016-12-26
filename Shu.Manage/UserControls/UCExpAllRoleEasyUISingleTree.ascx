<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCExpAllRoleEasyUISingleTree.ascx.cs"
    Inherits="Shu.Manage.UserControls.UCExpAllRoleEasyUISingleTree" %>
<link href="/Content/themes/bootstrap/easyui.css" rel="stylesheet" />
<link href="/Content/themes/icon.css" rel="stylesheet" />
<script type="text/javascript" src="/Scripts/jquery.easyui-1.4.5.min.js" charset="utf-8"></script>
<script type="text/javascript">
    window.onload = function () {
        $('#tt2').tree({
            loadMsg: '加载中,请等候...',
            url: '/Handler/AsynDeptTree.ashx?Option=Role',
            onDblClick: function (node) {
                if (node.id.indexOf("_Dep") <= 0) {
                    $(this).tree('toggle', node.id);
                    parent.ReturnValues(node.id, node.text, node.iconCls, node.attributes);
                }
                else {
                    alert("请选择人员!");
                }
            },
            onClick: function (node) {
                if (node.state == "open") {
                    collapse(node);
                }
                if (node.state == "closed") {
                    expand(node);
                }
            }
        });
        $('#loading-mask').fadeOut();
    }

    function collapse(node) {
        $('#tt2').tree('collapse', node.target);
    }
    function expand(node) {
        $('#tt2').tree('expand', node.target);
    }


    var nodeFontStyle = "<a style='color:{1}'>{0}</a>";
    function Travel(treeId) {//参数为树的ID，注意不要添加#
        var roots = $('#' + treeId + '').tree('getRoots'), children, i, j;
        for (i = 0; i < roots.length; i++) {
            findChildren(treeId, roots[i]);
        }
    }

    function findChildren(treeId, childrenRoot) {
        //debugger
        var children, j;
        children = $('#' + treeId + '').tree('getChildren', childrenRoot.target);
        for (j = 0; j < children.length; j++) {
            if (children[j].text.toString().indexOf($('#txtSearch').val()) >= 0) {
                //$('#' + treeId + '').tree('select', children[j].target);
                if (children[j].text.toString().indexOf("style") >= 0) {
                    var s = children[j].text.toString().replace(/[^\u4e00-\u9fa5]/gi, "");
                    children[j].text = s.replace($('#txtSearch').val(), nodeFontStyle.replace("{0}", $('#txtSearch').val()).replace("{1}", "red"));
                }
                else {
                    children[j].text = children[j].text.replace($('#txtSearch').val(), nodeFontStyle.replace("{0}", $('#txtSearch').val()).replace("{1}", "red"));
                }
                $('#' + treeId + '').tree("update", children[j]);
                expand(childrenRoot);
                //break;
            }
            else {
                //debugger

                if (children[j].text.toString().indexOf("style") >= 0) {
                    var s = children[j].text.toString().replace(/[^\u4e00-\u9fa5]/gi, "");
                    children[j].text = nodeFontStyle.replace("{0}", s).replace("{1}", "Blank");
                }
                else {
                    children[j].text = nodeFontStyle.replace("{0}", children[j].text).replace("{1}", "Blank");
                }
                $('#' + treeId + '').tree("update", children[j]);
            }
            findChildren(treeId, children[j]);
        }
    }
</script>
<asp:HiddenField ID="hid_PerSeeCharge" runat="server" />
<asp:HiddenField ID="hid_DepSeeCharge" runat="server" />
<div id="loading-mask" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%; background: #D2E0F2; z-index: 20000">
    <div id="pageloading" style="position: absolute; top: 50%; left: 50%; margin: -120px 0px 0px -120px; text-align: center; border: 2px solid #8DB2E3; width: 200px; height: 40px; font-size: 14px; padding: 10px; font-weight: bold; background: #fff; color: #15428B;">
        <img src="../../Images/main/loader.gif" align="absmiddle" />
        正在加载中,请稍候...
    </div>
</div>
<div>
    <ul id="tt2">
    </ul>
</div>
