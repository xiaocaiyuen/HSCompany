//扩展 validatebox
$.extend($.fn.validatebox.defaults.rules, {
    ComRequired: {
        validator: function (value, param) {
            return value != param;
        },
        message: '请选择内容!'
    },
    CHS: {
        validator: function (value, param) {
            return /^[\u0391-\uFFE5]+$/.test($.trim(value));
        },
        message: '请输入汉字！'
    },
    EN: {
        validator: function (value, param) {
            return /^[A-Za-z]+$/.test($.trim(value));
        },
        message: '请输入字母！'
    },
    ZIP: {
        validator: function (value, param) {
            return /^[1-9]\d{5}$/.test($.trim(value));
        },
        message: '邮政编码不存在！'
    },
    IP: {
        validator: function (value, param) {
            return /^(([0-1]?\d{1,2}|2[0-4]\d|25[0-5])\.){3}([0-1]?\d{1,2}|2[0-4]\d|25[0-5])$/.test($.trim(value));
        },
        message: 'IP地址不正确！'
    },
    SubnetMask: {
        validator: function (value, param) {
            //return /^(([0-1]?\d{1,2}|2[0-4]\d|25[0-5])\.){3}([0-1]?\d{1,2}|2[0-4]\d|25[0-5])$/.test($.trim(value));
            return /^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$/.test($.trim(value));
        },
        message: '子网掩码不正确！'
    },
    Gateway: {
        validator: function (value, param) {
            return /^(([0-1]?\d{1,2}|2[0-4]\d|25[0-5])\.){3}([0-1]?\d{1,2}|2[0-4]\d|25[0-5])$/.test($.trim(value));
        },
        message: '默认网关不正确！'
    },
    Port: {
        validator: function (value, param) {
            if (value.length > 5) {
                return false;
            }
            if (/^\d+(\.\d+)?$/.test(value)) {
                if (parseInt(value) > 65535) {
                    return false;
                }
                else {
                    return true;
                }
            }
            else {
                return false;
            }
        },
        message: '端口号不正确！'
    },
    QQ: {
        validator: function (value, param) {
            return /^[1-9]\d{4,10}$/.test($.trim(value));
        },
        message: 'QQ号码不正确！'
    },
    mobile: {
        validator: function (value, param) {
            return /^((\(\d{2,3}\))|(\d{3}\-))?(13|14|15|18)\d{9}$/.test($.trim(value));
        },
        message: '手机号码不正确！'
    },
    Phone: {
        validator: function (value, param) {
            return /^((0\d{2,3})-)?(\d{7,8})(-(\d{3,}))?$/.test($.trim(value));
        },
        message: '电话号码不正确！'
    },
    email: {
        validator: function (value, param) {
            return /^[^\s\.]+@{1,1}[^\s\.]+(\.{1,1}[^\s\.]+)+$/.test($.trim(value));
        },
        message: '电子邮箱不正确！'
    },
    number: {
        validator: function (value, param) {
            return /^-?[0-9]\d*$/.test($.trim(value));
        },
        message: '请输入数字！'
    },
    integernumber: {
        validator: function (value, param) {
            return /^[1-9]\d*$/.test($.trim(value));
        },
        message: '请输入正整数！'
    },
    money: {
        validator: function (value, param) {
            value = $.trim(value);
            if (/^\d+(\.\d+)?$/.test(value)) {
                if (parseInt(value) > 9999999999) {
                    return false;
                }
                else {
                    return true;
                }
            }
            else {
                return false;
            }
        },
        message: '请输入正确的金额格式！'
    },
    decimal: {
        validator: function (value, param) {
            value = $.trim(value);
            if (/^\d+(\.\d+)?$/.test(value)) {
                return true;
            }
            else {
                return false;
            }
        },
        message: '请输入正确数字类型！'
    },
    idcard: {
        validator: function (value, param) {
            if (value.length != 18) {
                return false;
            }
            return /^(\d{6})(18|19|20)?(\d{2})([01]\d)([0123]\d)(\d{3})(\d|X)?$/.test($.trim(value));
            //return $.idCard($.trim(value));
        },
        message: '请输入正确的身份证号码！'
    }
});


$.fn.extend({
    //自动适用
    resizeDataGrid: function (heightMargin, widthMargin, minHeight, minWidth) {
        var height = $(document.body).height() - heightMargin;
        var width = $(document.body).width() - widthMargin;
        height = height < minHeight ? minHeight : height;
        width = width < minWidth ? minWidth : width;
        $(this).datagrid('resize', {
            height: height,
            width: width
        });
    }
});
