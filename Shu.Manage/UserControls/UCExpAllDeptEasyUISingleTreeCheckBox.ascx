<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCExpAllDeptEasyUISingleTreeCheckBox.ascx.cs" Inherits="Shu.Manage.UserControls.UCExpAllDeptEasyUISingleTreeCheckBox" %>

<script type="text/javascript">
    window.onload = function () {
        //alert($('input[id*=hid_DepSeeCharge]').val())
        $('#tt2').tree({
            checkbox: true,
            loadMsg: '加载中,请等候...',
            url: '/Handler/AsynDeptTree.ashx?Option=AllDeptData&pid=0&DepSelectedStr=' + $('input[id*=hid_DepSeeCharge]').val() + '',
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
    //获取选中的部门ID
    function getCheckedDepID() {
        var nodes = $('#tt2').tree('getChecked');
        var s = "";
        for (var i = 0; i < nodes.length; i++) {
            if (nodes[i].id != "001") {
                if (s != "") s += ",";
                s += nodes[i].id;
            }
        }
        return s;
    }
    //获取选中的部门名称
    function getCheckedDepName() {
        var nodes = $('#tt2').tree('getChecked');
        var s = "";
        for (var i = 0; i < nodes.length; i++) {
            if (nodes[i].id != "001") {
                if (s != "") s += ",";
                s += nodes[i].text;
            }
        }
        return s; ;
    }
</script>
<asp:HiddenField ID="hid_DepSeeCharge" runat="server" />
<div id="loading-mask" style="position: absolute; top: 0px; left: 0px; width: 100%;
    height: 100%; background: #D2E0F2; z-index: 20000">
    <div id="pageloading" style="position: absolute; top: 50%; left: 50%; margin: -120px 0px 0px -120px;
        text-align: center; border: 2px solid #8DB2E3; width: 200px; height: 40px; font-size: 14px;
        padding: 10px; font-weight: bold; background: #fff; color: #15428B;">
        <img src="../../Images/main/loader.gif" align="absmiddle" />
        正在加载中,请稍候...
    </div>
</div>
<div>
    <ul id="tt2">
    </ul>
</div>