(function ($) {
    $.fn.validationEngineLanguage = function () { };
    $.validationEngineLanguage = {
        newLang: function () {
            $.validationEngineLanguage.allRules = {
                "required": {               // Add your regex rules here, you can take telephone as an example   
                    "regex": "none",
                    "alertText": "* 该值不能为空.",
                    "alertTextCheckboxMultiple": "* 请选择一个单选框.",
                    "alertTextCheckboxe": "* 请选择一个复选框."
                },
                "length": {
                    "regex": "none",
                    "alertText": "* 长度必须在 ",
                    "alertText2": " 至 ",
                    "alertText3": " 之间."
                },
                "comparenum": {
                    "regex": "none",
                    "alertText": "分值必须在 ",
                    "alertText2": " 至 ",
                    "alertText3": " 之间."
                },

                "lengthAs": {
                    "regex": "none",
                    "alertText": "* 您需要输入",
                    "alertText2": " 个数字."
                },
                "maxCheckbox": {
                    "regex": "none",
                    "alertText": "* 最多选择 ", //官方文档这里有问题   
                    "alertText2": " 项."
                },
                "minCheckbox": {
                    "regex": "none",
                    "alertText": "* 至少选择 ",
                    "alertText2": " 项."
                },
                "confirm": {
                    "regex": "none",
                    "alertText": "* 两次输入不一致,请重新输入."
                },
                "telephone": {
                    "regex": "/^(0[0-9]{2,3}\-)?([2-9][0-9]{6,7})+(\-[0-9]{1,4})?$/",
                    "alertText": "* 请输入有效的电话号码,如:010-29292929."
                },
                "fax": { "regex": "/^(0[0-9]{2,3}\-)?([1-9][0-9]{6,7})+(\-[0-9]{1,4})?$/",
                    "alertText": "* 请输入有效的传真号码,如:0551-29292929."
                },
                "mobilephone": {
                    "regex": "/(^0?[1][358][0-9]{9}$)/",
                    "alertText": "* 请输入有效的手机号码."
                },

                "phone": {
                    "regex": "/((^0?[1][358][0-9]{9}$)|^(0[0-9]{2,3}\-)?([2-9][0-9]{6,7})+(\-[0-9]{1,4})?$)/",
                    "alertText": "* 请输入有效的电话号码."
                },

                "email": {
                    "regex": "/^[a-zA-Z0-9_\.\-]+\@([a-zA-Z0-9\-]+\.)+[a-zA-Z0-9]{2,4}$/",
                    "alertText": "* 请输入有效的邮件地址."
                },
                "code": {
                    "regex": "/^SMDS\-[A-Z]{2}\-[0-9]{4}$/",
                    "alertText": "* 请按正确的格式输入，如：SMDS-CF-0001"
                },
                "date": {
                    "regex": "/^(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})-(((0[13578]|1[02])-(0[1-9]|[12][0-9]|3[01]))|((0[469]|11)-(0[1-9]|[12][0-9]|30))|(02-(0[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))-02-29)$/",
                    "alertText": "* 请输入有效的日期,如:2008-08-08."
                },
                "ip": {
                    "regex": "/^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/",
                    "alertText": "* 请输入有效的IP."
                },
                "chinese": {
                    "regex": "/^[\u4e00-\u9fa5]+$/",
                    "alertText": "* 请输入中文."
                },
                "url": {
                    "regex": "/^[a-zA-z]:\\/\\/[^s]$/", //这些验证请自己加强   
                    "alertText": "* 请输入有效的网址."
                },
                "zipcode": {
                    "regex": "/^[0-9]{6}$/",   //"regex": "/^[a-zA-Z0-9 ]{3,12}$/",  
                    "alertText": "* 请输入有效的邮政编码."
                },
                "qq": {
                    "regex": "/^[1-9]\d{4,9}$/",
                    "alertText": "* 请输入有效的QQ号码."
                },
                "onlyNumber": {
                    "regex": "/^[0-9]+\.{0,1}[0-9]{0,2}$/",   //已经添加小数点输入
                    "alertText": "* 该值为数字型、或小数型。"
                },
                "onlyNumberFour": {
                    "regex": "/^[0-9]+\.{0,1}[0-9]{0,4}$/",   //已经添加小数点输入,确定最多四位
                    "alertText": "* 该值为数字型、或小数型且保留小数点最多四位。"
                },
                
                "onlyIntMoreTherZero": {
                    "regex": "/^[0-9]*[1-9][0-9]*$/",   //已经添加小数点输入
                    "alertText": "* 该值为大于0的数字型。"
                },
                "onlyLetter": {
                    "regex": "/^[a-zA-Z]+$/",
                    "alertText": "* 请输入英文字母."
                },
                "noSpecialCaracters": {
                    "regex": "/^[0-9a-zA-Z]+$/",
                    "alertText": "* 请输入英文字母或数字."
                },
                "ajaxUser": {
                    "file": "validate.action", //ajax验证用户名，会post如下参数：validateError    ajaxUser；validateId user；validateValue  cccc   
                    "alertTextOk": "* 帐号可以使用.",
                    "alertTextLoad": "* 检查中, 请稍后...",
                    "alertText": "* 帐号不能使用."
                },
                "ajaxName": {
                    "file": "validateUser.php",
                    "alertText": "* This name is already taken",
                    "alertTextOk": "* This name is available",
                    "alertTextLoad": "* Loading, please wait"
                },
                "intNum": {
                    "regex": "/^[0-9]+$/",
                    "alertText": "* 该值为整数型。"
                }
                    ,
                "intNum2": {
                    "regex": "/^[1-9][0-9]{0,2}$/",
                    "alertText": "请输入1-999的整数。"
                },
                "intNum3": {
                    "regex": "/^[0-9]+\.{0,1}[0-9]{0,2}$/",   //已经添加小数点输入
                    "alertText": "请输入1-99的数值<br/>或带有两位小数的数值。"
                },
                "intNum4": {
                    "regex": "/^[1-9]{1}\d{1}\.\d{2}$/",   //小数点前面只能输入2为正整数，小数点后2位小数
                    "alertText": "请输入100以内的数字。"
                },
                "Age": {
                    "regex": "/^[1-9]{0,1}[0-9]{0,2}$/",
                    "alertText": "请输入正确的年龄！"
                },
                "cardNo": {
                    "regex":"/^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{4}$/",
                    "alertText": "身份证格式错误"
                   
                },
                "onlyRangeNumber": {
                    "regex": "/^(?:0|[1-9][0-9]?|100)$/",
                    "alertText": "该值只能为1到100的整数。"
                },
                "CompareNum": {
                    "nname": "CompareNum",
                    "alertText": "请输入整数值。 <br /> 或 题目数 必须小于等于 总题目数"
                },
                "onlyIntDropDownList": {
                    "regex": "/^[0-9]*[1-9][0-9]*$/",   //已经添加小数点输入
                    "alertText": "* 该选择值必须选择。"
                }
            }

        }
    }
})(jQuery);   
  
$(document).ready(function() {     
    $.validationEngineLanguage.newLang() 
});  