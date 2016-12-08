
$(function () {
    $('input[id*=chkAll]').click(
                function () {
                    $('input:checkbox:not(:disabled)').not('input[id*=chkAll]').click(
                       function () {
                           if ($('input:checkbox:not(:disabled, :checked)').not('input[id*=chkAll]').length == 0) {
                               $('input[id*=chkAll]').attr('checked', true)
                           }
                           else {
                               $('input[id*=chkAll]').attr('checked', false)

                           }
                       }
                   ).attr('checked', this.checked);
                });


    AddTabRow();

    ModifyDateShow();

    //奇偶换色
    $("table[id*=GridView1] tbody tr:odd").css("background-color", "White");
    $("table[id*=GridView1] tbody tr:even").css("background-color", "#EBEFF2");

    //判断分页是否可用
    checkPaginShow();

    $("table[id*=GridView1]").find('img').hover(function () {
          $(this).css({ border: "1px solid #ccc", background: "#eee" })
    }, function () {
        $(this).css({ border: "0px", background: "#fff"})
    });
});




//判断分页是否可用
function checkPaginShow() {
    var isshow = $('input[id*=hid_IsShowPagin]').val();

    if (isshow == "false") {

        $('.pagination_table').hide();
    }
    else {

        $('.pagination_table').show();
    }
}


//或者checkbox选中的数量
function chenckLength() {
    return $(this).find('td').first().find('input[id*=chkbox]:checked').length;
//    return $('input[id*=chkbox]:checked').length;
}



//选择单行
function isChecked() {

    var len = chenckLength();



    if (len > 1) {

        alert('只能选择1条数据进行编辑!');

        return false;

    }

    if (len <= 0) {
        alert('请选择一条数据!');

        return false;
    }
}

function rowClick(title, url) {
    window.location.href = url;
}

//必须选择一条数据
function isCheckeds() {

    var len = chenckLength();


    if (len <= 0) {
        alert('请选择一条数据!');

        return false;
    }

    if (!confirm("您确定要删除该条数据吗？")) {
        return false;
    }
}

//发送网格化监督
function isCheckdx() {
    var len = chenckLength();


    if (len <= 0) {
        alert('请选择一条数据!');

        return false;
    }
}

/*#####################给表格动态添加行######################*/

function AddTabRow() {

    //获得需要显示的行数量

    var pagesize = $('select[id*=ddl_PageSize]').val();

    //获得table当前的行数量
    var currentTableRows = $('table[id*=GridView1] tr').length - 1;
 
    //获得 table th的列数量
    var currentTableThCount = $('table[id*=GridView1] tr th').not('.hidden').length;

    //比较行数量，设置还需要添加多少行
    var rowcount = pagesize - currentTableRows;

    var isShenHe = false; //是否有审核状态

    var tr = "";

    var thColspan = 0;

    var thtext = $('table[id*=GridView1] tr th:last').text(); //获取表头最后一列是否存在“操作”




    //判断是否存在操作列
    if (thtext == "操作") {

        //获得合并单元的列数
        thColspan = $('table[id*=GridView1] tr th:last').attr('colspan');

        if (thColspan == undefined) {

            thColspan = 0;
        }
    }

  

    if (thColspan != 0) {
        //获取总列数
        currentTableThCount += thColspan - 1;
    }



    //针对没有任何数据的情况后，后台自动添加了一行，但是出现了CheckBox
    //所以增加此方法，影藏添加行的checkbox
    if (currentTableRows == 1) {

        //循环 tr
        $('table[id*=GridView1] tr').not('th').each(function () {


            //找到对应的td行
            if ($(this).find('td').length != 0) {

                var tdvalue = "";

                //判断 如果TD不存在值 将会删除Checkbox
                $(this).find('td').each(function () {

                    var s = $(this).html();


                    if ((s.indexOf('INPUT') < 0) && (s.indexOf('input') < 0)) {

                        if ($(this).html() != "&nbsp;") {

                            tdvalue = "1";
                        }
                    }


                })

                

                if (tdvalue == "") {

                    $('input[id*=chkAll]').attr('disabled', true);

                    //设置隐藏checkbox
                    if ($(this).find('td').find(':checkbox').length == 1) {

                        $(this).find('td').find(':checkbox').css('display', 'none');
                    }

                    $(this).find(':button').each(function () {

                        $(this).css('display', 'none');
                    })
                }
                else {


                }
            }
        })

    }
    else {

       

    }

    //给删除按钮添加事件
    $('table[id*=GridView1] tr:not(:first)').each(function () {

        $(this).find(":button").each(function () {

            if ($(this).attr('value') == "删除") {

                var even = $(this).attr('onclick');
              
                $(this).removeAttr('onclick');

                $(this).bind("click", function () {

                    if (!confirm("您确定要删除该条数据吗？")) {

                        return false;
                    }
                    else {

                        var delInfo = even.split(':')[1]; //获取回传事件

                        eval(delInfo); //执行事件
                    }

                });


            }
        })

    })



    //循环添加行
    if (rowcount != 0) {

        for (var i = 0; i < rowcount; i++) {

            tr += "<tr>";

            for (var j = 0; j < currentTableThCount; j++) {

                tr += "<td>&nbsp;</td>";

            }

            tr += "</tr>";
        }
        //追加行到table的末尾
        $('table[id*=GridView1]').append(tr);
    }

}



/*############################对日期格式的处理###################################*/
function ModifyDateShow() {

    //循环当前行
    $('table[id*=GridView1] tr').each(function () {

        $(this).find('td').each(function () {

            var tdvalue = $(this).text();

           

            if (IsDateTime(tdvalue)) {

                var cnDate = DateTimeToCnDate(tdvalue);


                $(this).text(cnDate);
            }
        })
    })

    //判断是否是日期格式如果是，返回日期去掉时间部分
}

function ExamListDateShow() {
    //循环当前行
    $('table[id*=UCGrid_GridView1] tr').each(function () {
        $(this).find('td').each(function () {
            var tdvalue = $(this).text();
            tdvalue = tdvalue.replace("年", "-").replace("月", "-").replace("日", "");
            //alert(IsDateTime(tdvalue))
            if (IsDateTime(tdvalue)) {
                var cnDate = DateTimeToCnDateHms(tdvalue);
                $(this).text(cnDate);
            }
        })
    })
    //判断是否是日期格式如果是，返回日期去掉时间部分
}


//验证日期格式 YYYY-MM-DD hh:ss:mm
function IsDateTime(date_str) {

    var myReg = /^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d$/

    if (!myReg.exec(date_str)) {

        return false
    }
    else {

        return true
    }

}


//将 YYYY-MM-DD hh:ss:mm格式转化成功 年 月 日
function DateTimeToCnDate(mytime) {



    //截取日期 和时间两部分 strtemp[0] = 2012-12-01  strtemp[1] = 12:12:02
    var strtemp = mytime.split(" ");
   
    var time = "";

    if (strtemp[1] != "0:00:00" && strtemp[1] != "00:00:00") {

        var timetemp = strtemp[1].split(':')

        //time = timetemp[0] + "时" + timetemp[1] + "分";//  + timetemp[2] + "秒";
    }

    //截取日期部分 将日期拆分出出来 对应的 年 月 日
    var date = strtemp[0].split("-");

    //重组日期格式
    var value = date[0] + "年" + date[1] + "月" + date[2] + "日" + time;

    return value;


}

//将 YYYY-MM-DD hh:ss:mm格式转化成功 年 月 日
function DateTimeToCnDateHms(mytime) {


    //截取日期 和时间两部分 strtemp[0] = 2012-12-01  strtemp[1] = 12:12:02
    var strtemp = mytime.split(" ");

    var time = "";

    if (strtemp[1] != "0:00:00" && strtemp[1] != "00:00:00") {

        var timetemp = strtemp[1].split(':')

        time = timetemp[0] + "时" + timetemp[1] + "分";//  + timetemp[2] + "秒";
    }

    //截取日期部分 将日期拆分出出来 对应的 年 月 日
    var date = strtemp[0].split("-");

    //重组日期格式
    var value = date[0] + "年" + date[1] + "月" + date[2] + "日" + time;

    return value;


}


function RowFocus(td) {
    $(td).css('color', 'blue');
    $(td).removeClass();
    $(td).addClass("rowFocusCss");
}

function RowBlur(td) {
    $(td).css('color', '#000000');
    $(td).removeClass();
    $(td).addClass("rowBlurCss");
}

//详细页面
function showTab(url) {

    window.parent.addTab('详细', url, '')
}

//详细页面
function showTabs(tabName, url) {
    window.parent.addTab(tabName, url, '');
}

///截取表格字符长度
function GridSubStr(gridId, columnNum, SubstrNum) {

    var cloumnIndex = columnNum - 1;
    var realSubstrNum;
    if (window.screen.width > 1024) {
        realSubstrNum = Math.round(SubstrNum * window.screen.width / 1000 * 1.6);
    }
    else {
        realSubstrNum = SubstrNum;
    }

    $('table[id*=' + gridId + '] tr:not(:first)').each(function () {

        var str = $(this).find('td:eq(' + cloumnIndex + ')').text();
        //            alert(realSubstrNum);
        //            alert(str.length);
        if ($.trim(str).length > realSubstrNum) {

            $(this).find('td:eq(' + cloumnIndex + ')').text(str.substring(0, realSubstrNum) + "...");


        }

        $(this).find('td:eq(' + cloumnIndex + ')').attr('title', str);

        $(this).find('td:eq(' + cloumnIndex + ')').css('text-align', 'left');

        $(this).find('td:eq(' + cloumnIndex + ')').css('padding-left', '10px');


    })
}


