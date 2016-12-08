

$(function () {

    var thColspan = GetColspan();

    if (thColspan == 0) {

        //删除最后一列
        $("#tab1 tr :last-child").remove();
    }

    else {

        if (thColspan = 2) {




//            $('table[id*=GridView1] tr:not(:first)').each(function () {

//                if ($(this).find('td').find(":button").val() == "详细") {


//                }


//                $(this).find('td').find(":button").each(function () {

//                    if ($(this).val() == "详细") {



//                        $("table[id*=GridView1] tr :nth-child(" + $(this).parent().index() + ")").remove();
//                    }

//                    return false;
//                })

//                return false;
//            });

           $("table[id*=GridView1] tr :nth-child(12)").remove();

          //  $('table[id*=GridView1] tr th:last').attr('colspan', thColspan - 1);
        }
    }
})


function GetColspan() {

    var thtext = $('table[id*=GridView1] tr th:last').text(); //获取表头最后一列是否存在“操作”

    var thColspan = 0;

    //判断是否存在操作列
    if (thtext == "操作") {

        //获得合并单元的列数
        thColspan = $('table[id*=GridView1] tr th:last').attr('colspan');

        if (thColspan == undefined) {

            thColspan = 0;
        }
    }

    return thColspan;
}