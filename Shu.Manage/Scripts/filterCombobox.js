$.extend($.fn.combobox.methods, {
    selectedIndex: function (jq, index) {
        if (!index) {
            index = 0;
        }
        $(jq).combobox({
            onLoadSuccess: function () {
                var opt = $(jq).combobox('options');
                var data = $(jq).combobox('getData');

                for (var i = 0; i < data.length; i++) {
                    if (i == index) {
                        $(jq).combobox('setValue', eval('data[index].' + opt.valueField));
                        break;
                    }
                }
            }
        });
    }
});

///为字符串添加模糊比较的方法
String.prototype.isLike = function (exp/*类似于SQL中的模糊查询字符串*/, i/*是否区分大小写*/) {
    var str = this;
    i = i == null ? false : i;
    if (exp.constructor == String) {

        /*首先将表达式中的‘_’替换成‘.’，但是‘[_]’表示对‘_’的转义，所以做特殊处理*/
        var s = exp.replace(/_/g, function (m, i) {
            if (i == 0 || i == exp.length - 1) {
                return ".";
            }
            else {
                if (exp.charAt(i - 1) == "[" && exp.charAt(i + 1) == "]") {
                    return m;
                }
                return ".";
            }
        });
        /*将表达式中的‘%’替换成‘.’，但是‘[%]’表示对‘%’的转义，所以做特殊处理*/
        s = s.replace(/%/g, function (m, i) {
            if (i == 0 || i == s.length - 1) {
                return ".*";
            }
            else {
                if (s.charAt(i - 1) == "[" && s.charAt(i + 1) == "]") {
                    return m;
                }
                return ".*";
            }
        });

        /*将表达式中的‘[_]’、‘[%]’分别替换为‘_’、‘%’*/

        s = s.replace(/\[_\]/g, "_").replace(/\[%\]/g, "%");

        /*对表达式处理完后构造一个新的正则表达式，用以判断当前字符串是否和给定的表达式相似*/

        var regex = new RegExp("^" + s, i ? "" : "i");
        return regex.test(this);
    }
    return false;
};

///为数组添加模糊查询方法
Array.prototype.selectLike = function (exp/*类似于SQL中的模糊查询字符串*/, fun) {
    var arr = [];
    if (fun && fun.constructor == Function) {
        for (var i = 0; i < this.length; i++) {
            if (fun(this[i], exp)) {
                arr.push(i);
            }
        }
    }
    else {
        for (var i = 0; i < this.length; i++) {
            if (this[i].isLike(exp, false)) {
                arr.push(i);
            }
        }
    }
    return arr;
};