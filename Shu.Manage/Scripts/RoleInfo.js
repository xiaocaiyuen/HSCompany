
var lExpend = "lminus.gif";
var lPinch = "lplus.gif";
var tExpend = "tminus.gif";
var tPinch = "tplus.gif";

function ClickNode(img, isBottom, tableId) {
    var imgId = img.src.substring(img.src.lastIndexOf("/") + 1);
    var url = img.src.substring(0, img.src.lastIndexOf("/") + 1);
    var oldTrId = img.parentElement.parentElement.id;
    var newTrId = oldTrId.substring(oldTrId.indexOf("_") + 1);

    if (isBottom) {
        if (imgId == lExpend) {
            img.src = url + lPinch;
            img.parentElement.id = lPinch;
            PinchNode(newTrId, oldTrId, tableId);
        }
        else {
            img.src = url + lExpend;
            img.parentElement.id = lExpend;
            ExpendNode(newTrId, oldTrId, tableId);
        }
    }
    else {
        if (imgId == tExpend) {
            img.src = url + tPinch;
            img.parentElement.id = tPinch;
            PinchNode(newTrId, oldTrId, tableId);

        }
        else {
            img.src = url + tExpend;
            img.parentElement.id = tExpend;
            ExpendNode(newTrId, oldTrId, tableId);
        }
    }
}

function ExpendNode(newId, oldId, tableId) {
    var tree = document.getElementById(tableId);
    for (var i = 0; i < tree.rows.length; i++) {
        if (tree.rows[i].id.indexOf(newId) != -1 && tree.rows[i].id != oldId) {
            var isExpend = true;
            var pId = tree.rows[i].id;

            while (pId != oldId) {
                for (var j = 0; j < 2; j++)
                    pId = pId.substring(0, pId.lastIndexOf("/"));
                var parent = document.getElementById(pId);
                if (parent != null) {
                    var tempId = parent.cells[2].id;
                    if (tempId == lExpend || tempId == tExpend || tempId == "")
                        ;
                    else {
                        isExpend = false;
                        break;
                    }
                }
                else
                    break;
            }
            if (isExpend)
                tree.rows[i].style.display = "block";
        }
    }
}

function PinchNode(newId, oldId, tableId) {
    var tree = document.getElementById(tableId);
    for (var i = 0; i < tree.rows.length; i++) {
        if (tree.rows[i].id.indexOf(newId) != -1 && tree.rows[i].id != oldId) {
            tree.rows[i].style.display = "none";
        }
    }
}


/***********************************************************设置选中********************************/
function SetChecked(obj, tableId) {

    var oldTrId = obj.parentElement.parentElement.id;
    var newTrId = oldTrId.substring(oldTrId.indexOf("_") + 1);
    var result = false; //确认是否选中
    SetResult(newTrId, oldTrId, tableId);
}

function SetResult(newId, oldId, tableId) {
    var tree = document.getElementById(tableId);
    for (var i = 0; i < tree.rows.length; i++) {
        if (tree.rows[i].id.indexOf(newId) != -1 && tree.rows[i].id != oldId) {
            var id = tree.rows[i].id;
            var ele = $("tr[id='" + id + "'] input[type='checkbox']");

        }
    }
}

//function ExpendNode(newId, oldId, tableId) {
//    var tree = document.getElementById(tableId);
//    for (var i = 0; i < tree.rows.length; i++) {
//        if (tree.rows[i].id.indexOf(newId) != -1 && tree.rows[i].id != oldId) {
//            var isExpend = true;
//            var pId = tree.rows[i].id;

//            while (pId != oldId) {
//                for (var j = 0; j < 2; j++)
//                    pId = pId.substring(0, pId.lastIndexOf("/"));
//                var parent = document.getElementById(pId);
//                if (parent != null) {
//                    var tempId = parent.cells[2].id;
//                    if (tempId == lExpend || tempId == tExpend || tempId == "")
//                        ;
//                    else {
//                        isExpend = false;
//                        break;
//                    }
//                }
//                else
//                    break;
//            }
//            if (isExpend)
//                tree.rows[i].style.display = "block";
//        }
//    }
//}

//function PinchNode(newId, oldId, tableId) {
//    var tree = document.getElementById(tableId);
//    for (var i = 0; i < tree.rows.length; i++) {
//        if (tree.rows[i].id.indexOf(newId) != -1 && tree.rows[i].id != oldId) {
//            tree.rows[i].style.display = "none";
//        }
//    }
//}

var modifyId = "";
var choosedId = "";
var choosedIndex;
function ChooseTree(obj) {
    var cTrId = obj.id.substring(0, obj.id.indexOf("_chbbox"));
    var treeTable = document.getElementById("grvMenuList");
    for (var i = 0; i < treeTable.rows.length; i++) {
        if (treeTable.rows[i].id.indexOf(cTrId) != -1 && treeTable.rows[i].id != cTrId) {
            document.getElementById(treeTable.rows[i].id + "_chbbox").checked = obj.checked;
        }
    }
    choosedId = "";
    choosedIndex = new Array();
    for (var i = 1; i < treeTable.rows.length; i++) {
        if (document.getElementById(treeTable.rows[i].id + "_chbbox").checked) {
            choosedId += treeTable.rows[i].id.substring(treeTable.rows[i].id.lastIndexOf("/") + 1) + ",";
            choosedIndex.push(i);
        }
    }

    choosedId = choosedId.substring(0, choosedId.length - 1);

    //设置其父节点的选中
    if (obj.name.length != 2) {

        var name = obj.name.substring(0, obj.name.length - 3);
        SetParentCheck(name);
    }
}

function SetParentCheck(pname) {

    $("input[name='" + pname + "']").each(function () {
        $(this).attr("checked", "checked");
    })
    var length = pname.length;
    if (length > 2) {
        var name = pname.substring(0, pname.length - 3);
        SetParentCheck(name);
    }
}

//获得所选择的权限
function GetCheckCz() {
    var result = "";
    $("input[type='checkbox']").each(function () {
        var obj = this;
        var id = $(this).attr("id");
        var index = $(this).attr("index");
        var check = $(this).attr("checked");
        if (id.indexOf("_chbbox") > 0 && check == "checked") {
            result += $(this).parent().attr("name") + "-";
            for (var i = 0; i <= 10; i++) {
                var cid = "grvMenuList_cz_" + index + "_" + i + "_" + index;
                if ($("input[id*=" + cid + "]").attr("checked") == "checked") {
                    result += $("#" + cid).attr("value") + ",";
                }
            }
            result += "|";
        }
    })
    $("#hdnValue").val(result);

}