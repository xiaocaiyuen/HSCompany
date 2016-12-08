var LODOP;
window.onload = function () {
    //var LODOP = document.getElementById("LODOP");
}

function printview() {
    //debugger
    LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
    if (LODOP == null) return false;

    $("#tabBtn").attr("style", "display:none;");


    $('body').find('table').each(function () {

        $(this).find('img').each(function () {

            if ($(this).attr('src').indexOf('tab_m1_sq')) {

                $(this).show();
            }

        })

        var id = $(this).attr('id');

        if (id == undefined || id == null) {
            $(this).css('width', '750px')
            $(this).attr('width', '750px');
        }
        else {

            if (id.indexOf('NotSet') < 0) {
                $(this).css('width', '750px')
                $(this).attr('width', '750px');
            }
        }
    })

    $('.tab_for').hide();
    LODOP.PRINT_INIT("打印");
    LODOP.NewPage();
    var strcss = "<head><link href=\"/Styles/table.css\" rel=\"stylesheet\" type=\"text/css\" /></head>";
    var s_innerhtml = strcss + "<body>";
    s_innerhtml += $('body').html();
    s_innerhtml += "</body>";

    LODOP.ADD_PRINT_HTM(40, 22, 800, 1020, s_innerhtml);
    LODOP.SET_PRINT_STYLEA(1, "HOrient", 2);
    LODOP.PREVIEW();



    $('body').find('table').each(function () {

        $(this).find('img').each(function () {

            if ($(this).attr('src').indexOf('tab_m1_sq')) {

                $(this).show();
            }

        })

        var id = $(this).attr('id');

        if (id == undefined || id == null) {
            $(this).css('width', '800px')
            $(this).attr('width', '800px');
        }
        else {

            if (id.indexOf('NotSet') < 0) {
                $(this).css('width', '800px')
                $(this).attr('width', '800px');
            }
        }
    })



    $('.tab_for').show();
  
    $("#tabBtn").attr("style","");
    return false;

   
}