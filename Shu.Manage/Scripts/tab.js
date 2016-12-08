function addTab(subtitle, url, icon) {
    if (!$('#tabs').tabs('exists', subtitle)) {
        $('#tabs').tabs('add', {
            title: subtitle,
            content: createFrame(url),
            closable: true,
            icon: icon
        });
    } else {
        $('#tabs').tabs('select', subtitle);

        var currTab = $('#tabs').tabs('getSelected'); //这个也可以
        $('#tabs').tabs('update', { tab: currTab, options: { content: createFrame(url)} });


        $('#mm-tabupdate').click();
    }
    tabClose();
}


function addDesktop(subtitle, url, icon) {
    if (!$('#tabs').tabs('exists', subtitle)) {
        $('#tabs').tabs('add', {
            title: subtitle,
            content: createFrame(url),
            closable: false,
            icon: icon
        });
    } else {
        $('#tabs').tabs('select', subtitle);
        $('#mm-tabupdate').click();
    }
    //tabClose();
}



function createFrame(url) {
    var s = '<iframe scrolling="yes" frameborder="0"  src="' + url + '" style="width:100%;height:100%;overflow-x:visible;word-break:break-all;"></iframe>';
    return s;
}

function Closetab() {
    var tab = $('#tabs').tabs('getSelected');
    var index = $('#tabs').tabs('getTabIndex', tab);//获取当前选中tab的索引
    $('#tabs').tabs('close', index);
}

function ClosetabByTitle(title) {

    $('#tabs').tabs('close', title);

}

function tabClose() {
    /*双击关闭TAB选项卡*/
    $(".tabs-inner").dblclick(function () {
        var subtitle = $(this).children(".tabs-closable").text();
        $('#tabs').tabs('close', subtitle);
    })
    /*为选项卡绑定右键*/
    $(".tabs-inner").bind('contextmenu', function (e) {
        var subtitle = $(this).children(".tabs-closable").text();
        //alert(subtitle)
        if (subtitle != "我的看板" && subtitle != "") {
            $('#mm').menu('show', {
                left: e.pageX,
                top: e.pageY
            });

            //var subtitle = $(this).children(".tabs-closable").text();

            $('#mm').data("currtab", subtitle);
            $('#tabs').tabs('select', subtitle);
        }
        return false;
    });
}