/*--------------添加行----------------*/
function addrow(tableID, isShowDel) {

    var colNum = $('#' + tableID + ' th').length; //获得表头列数,最后一列为操作列

    var rowNum = $('#' + tableID + ' tr').length - 1; //获得行号，加载当前table中共存在多少行，去掉表头
    var tr = "";

    if (colNum > 0) {
        tr += "<tr>";

        for (var i = 0; i < colNum; i++) {

            var tdName = "txt_r" + rowNum + "c" + (i + 1);
            tr += "<td style='text-align:center'>";

            if (i == 0) {
                tr += "<input  style=\"width:150px\" type=\"text\" id=\"" + tdName + "\"/ >";

            }
            else if (i == 1) {
                tr += "<input style=\"width:400px\" type=\"text\" id='" + tdName + "' />";

            }
            //            else if (i == 2) {
            //                tr += "<select id='select" + tdName + "' class='validate[required] ddl'><option value=''>请选择</option><option value='上墙'>上墙</option><option value='会议'>会议</option><option value='基层信息网'>基层信息网</option></select>";

            //            }
            //            else if (i == 3) {
            //                tr += "<textarea rows='3' id='" + tdName + "' style='width:95%;'/>";

            //            }
            else if (i == 2) {

                if (isShowDel) {

                    tr += " <img src='../../../Images/buttons/shanChu.gif' style=' cursor:pointer; width:59px; height:25px;' onclick=\"deleterow(this,'" + tableID + "')\" />";
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

/*------------删除行------------*/
function deleterow(del, tableID) {

    $(del).parent().parent().remove();

    var index = 1;

    $("#" + tableID + ' tr:not(:first)').each(function () {


        $(this).find('td:eq(0)').find('font').text(index);

        index++;

    })

}


/*---------获得动态表格中text值---------*/
function GetContent(tableID) {
    //记录动态表格保存信息  区分：列'^' 行‘|’
    var info = "";

    var rowindex = 0;

    var colNum = $('#' + tableID + ' th').length; //获得表头列数,最后一列为操作列

    $('#' + tableID + ' tr:not(:first)').each(function () {


        //如果当前不是第一行,加上行标识符
        if (rowindex != 0) {

            info += "|";
        }

        var colindex = 0;

        $(this).find('td').each(function () {

            if (colindex != 0 && colindex != colNum - 1) {

                info += "^";
            }

            if (colindex == 0) {

                info += $(this).find(':text').val();
            }
            else if (colindex == 1) {
                info += $(this).find(':text').val();
            }
            //            else if (colindex == 2) {
            //                info += $(this).find('select').val();
            //            }
            //            else if (colindex == 3) {//隐藏的id列
            //                info += $(this).find('textarea').val();
            //            }
            colindex++;
        })



        rowindex++;
    })
    return info;


}