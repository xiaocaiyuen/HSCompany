//身份证显示出身年月
function isIdCardNo(icd) {
    if (icd != "") {
        var filter = /^[1-9]\d{5}[1-9]\d{3}(((0[13578]|1[02])(0[1-9]|[12]\d|3[0-1]))|((0[469]|11)(0[1-9]|[12]\d|30))|(02(0[1-9]|[12]\d)))(\d{4}|\d{3}[xX])$/;
        if (!filter.test(icd)) {
            $("#t_UserInfo_Age").html("");
            $("#t_UserInfo_DateBirth").html("");
            $("#txt_Yhxx_Iage").val("");
            $("#txt_Yhxx_DTcsrq").val("");
            alert("无效的身份证号码！");
            return;
        }
        var myDate = new Date();
        var Ctiem = icd.substring(6, 10);
        var Atiem = icd.substring(10, 12);
        var Btiem = icd.substring(12, 14);
        var Ntiem = myDate.getFullYear();
        var age = parseInt(Ntiem) - parseInt(Ctiem) + 1;
        $("#t_UserInfo_Age").html(age);
        $("#t_UserInfo_DateBirth").html(Ctiem + "年" + Atiem + "月" + Btiem + "日");
        $("#txt_Yhxx_Iage").val(age);
        $("#txt_Yhxx_DTcsrq").val(Ctiem + "年" + Atiem + "月" + Btiem + "日");
    }
}