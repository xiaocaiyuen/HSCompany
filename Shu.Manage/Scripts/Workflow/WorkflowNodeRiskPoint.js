/*--------------添加行----------------*/
function addrowzq(tableID, isShowDel) {

    var colNum = $('#' + tableID + ' tr:eq(0)').find("th").length; //获得表头列数,最后一列为操作列

    var rowNum = $('#' + tableID + ' tr').length - 1; //获得行号，加载当前table中共存在多少行，去掉表头

    var tr = "";
    if (colNum > 0) {
        tr += "<tr>";

        for (var i = 0; i < colNum; i++) {

            var tdName = "txt_r" + rowNum + "c" + (i + 1);
            var tdhid = "hidcode_r" + rowNum + "c" + (i + 1);
            //            if (i == colNum - 1 || i == colNum - 2) {//如果等于最后一列
            //                tr += "<td style='display:none'>";
            //                if (i == colNum - 1) {
            //                    tr += "<font id='" + tdName + "'></font>";
            //                }
            //            }
            //            else {
            tr += "<td style='text-align:center'>";

            if (i == 0) {   //序号列

                tr += "<font>" + (rowNum + 1) + "</font>";

            }
            else if (i == 1) {  //
                tr += " <textarea name='" + tdName + "' class='validate[required]' id='" + tdName + "' style='width:99%;'></textarea>";
                //                tr += "<input type='text' id='" + tdName + "'  style='width:90px;' class=\"input4 validate[required]\" />";
            }
            else if (i == 2) {
                tr += " <textarea name='" + tdName + "' class='validate[required]' id='" + tdName + "' style='width:99%;'></textarea>";
//                tr += "<input type='text' id='" + tdName + "'  style='width:90px;' class=\"input4 validate[required]\" />";
            }
            else if (i == 3)
            {
                tr += "<div><input type='text' id='" + tdName + "'  style='width:88px;' ReadOnly='True' class=\"input4\" /><span id='ucuserSelectSpan'><img id='Img1' alt='选择' src='/Images/uc/search.gif' style='width: 19; height: 19px;cursor: pointer; padding-top: 2px;' onclick='ShowWarnIndex(" + rowNum + ")' /></span></div><input id='" + tdhid + "' type='hidden' />";
            }
            else if (i == 4) {
                tr += "<input id=\"Radio1\" type=\"radio\" name=\"" + rowNum + "\" value=\"0\"  checked=\"checked\" />启用<input id=\"Radio2\" type=\"radio\" name=\"" + rowNum + "\" value=\"1\" />不启用";
            }
            else {

                if (isShowDel) {

                    tr += " <img src='../../../Images/buttons/shanChu.gif' style=' cursor:pointer; width:59px; height:25px;' onclick=\"deleterowzq(this,'" + tableID + "')\" />";
                }
                else {

                    tr += "&nbsp;"
                }
            }
            tr += "</td>";
        }
        tr += "</tr>";
        $('#' + tableID).append(tr);
    }
}
function getVal() {
    var val = 0;

    $('#gwzq tr:not(:first)').each(function (rowindex) {
        var colindex = 0;
        $(this).find('td').each(function () {
            if (colindex == 3) {
                var txtval = $(this).find(':text').val();
                if (txtval.toString() == "") {
                    txtval = "0";
                }
                val = val + parseFloat(txtval);


            }
            colindex++;
        })

    })
    if (val.toString() > 0) {
        $("#lblAdd").html(val.toString());
    }
    else {
        $("#lblAdd").html("0");
    }




}
/*------------删除行------------*/
function deleterowzq(del, tableID) {

    $(del).parent().parent().remove();

    var index = 1;

    $("#" + tableID + ' tr:not(:first)').each(function () {


        $(this).find('td:eq(0)').find('font').text(index);

        index++;
    })

}




/*---------获得动态表格中text值---------*/
function GetDtableValuezq(tableID) {
    //记录动态表格保存信息  区分：列'^' 行‘|’
    var info = "";

    var rowNum = $('#' + tableID + ' tr').length - 1; //获得行号，加载当前table中共存在多少行，去掉表头

    var colNum = $('#' + tableID + ' th').length - 1; //获得表头列数,最后一列为操作列 去掉合计列

    $('#' + tableID + ' tr:not(:first)').each(function (rowindex) {
        //如果当前不是第一行,加上行标识符
        if (rowindex != 0) {

            info += "|";
        }

        var colindex = 0;

        $(this).find('td').each(function () {

            if (colindex != 0) {

                info += "^";
            }



            //if (colindex != colNum - 2) {

            if (colindex == 0) {

                info += $(this).find('font').text();
            }
            else if (colindex < 3) {
                info += $(this).find('textarea').val();
            }
            else if (colindex == 3) {
                info += $(this).find(':hidden').val();
            }
            else if (colindex == 4) {
                info += $(this).find('input:radio:checked').val();
            }

            colindex++;

        })


    })

    return info;
}


/*--------保存新增数据----------*/
function Save_Addzq(method) {

    var rtn = "";

    var value = GetDtableValuezq('gwzq');
    //    value = escape(value);
    //alert(value);
    $('#hid_GBManage').val(value);
    //    alert(value);
    //$('#hid_score').val($('#total2').text());
}


/***************************************保存数据到Fxpc_gwzqfkcs表***********************************************/
function add_gwzq() {


    return Save_Addzq('addgwzq');
}

function modify_gwzq() {


    return Save_Modifyzq('modifygwzq');

}

function Jisuandj(score) {

    var id = $('#hid_ID').val();

    var rtn = "";

    var path = "/Handler/Gwfx/Getdj.ashx?score=" + score;

    $.ajax({
        type: "Post",
        async: false, //设置同步
        url: path,
        dataType: "text",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            rtn = data;

        },
        error: function (err) {

            rtn = "ss";
        }
    });
    return rtn;
}


