function showLoading(msg, delay) {
    /// <param name="msg" type="String">待显示的文本,非必填</param>
    /// <param name="delay" type="Int">延时显示的毫秒数，默认100毫秒显示,非必填</param>
    if (!delay)
        delay = 100;
    var loading = $('<div class="ajax-loading" style="display:none"><table height="100%" width="100%"><tr><td align="center"><p>' + (msg ? msg : '') + '</p></td></tr></table></div>');
    loading.appendTo('body');
    var s = setTimeout(function () {
        if ($(".ajax-loading").length > 0) {
            loading.show();
            $('.container,.login-box').addClass('blur');
        }
    }, delay);
    return {
        close: function () {
            clearTimeout(s);
            loading.remove();
            $('.container,.login-box').removeClass('blur');
        }
    }
}

window.httpPost = function (url, obj, res) {
    $.ajax({
        url: url, //路径
        headers: { "Content-Type": "application/json" },
        type: "post",
        dataType: "json",
        data: JSON.stringify(obj),
        beforeSend: function () {
            showLoading("", 0);
            return;
        },
        success: function (result) {
            $('.ajax-loading').remove();
            res(result);
        }
    });
}
window.httpGet = function (url, obj, res) {
    $.ajax({
        url: url, //路径
        headers: { "Content-Type": "application/json" },
        type: "get",
        dataType: "json",
        data: JSON.stringify(obj),
        beforeSend: function () {
            showLoading("", 0);
            return;
        },
        success: function (result) {
            $('.ajax-loading').remove();
            res(result);
        }
    });
}



Date.prototype.format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1, //月份
        "d+": this.getDate(), //日
        "h+": this.getHours(), //小时
        "m+": this.getMinutes(), //分
        "s+": this.getSeconds(), //秒
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度
        "S": this.getMilliseconds() //毫秒
    };
    fmt = fmt || "yyyy-MM-dd";
    if (/(y+)/.test(fmt))
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt))
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

function request(params) {

    var url = location.href;
    var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
    var paraObj = {};
    for (i = 0; j = paraString[i]; i++) {
        paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);

    }
    var returnValue = paraObj[params.toLowerCase()];

    if (typeof (returnValue) == "undefined") {
        return "";

    } else {
        return returnValue;

    }

}

/* 手机号码验证,无alert  */
function IsMobile(string) {
    var mobile = trim(string);
    //var rePhone = /0?(13|14|15|18)[0-9]{9}/;
    var rePhone = /^1[0-9]{10}$/;
    if (mobile == false) {
        return false;
    } else if (rePhone.test(mobile)) {
        return true;
    } else {
        return false;
    }
}
/* Email格式验证 */
function IsEmail(email) {
    if (email == false) return false;
    var patrn = /^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    if (patrn.test(email) == false) return false;
    else return true;
}
/* 火车票验证证件信息是否正确 */
function checkIDCard(P_type, number) {
    var P_num = number.replace(/\s+/g, "");
    // if (P_type == "100"||P_type == "101"||P_type == "102") {
    if (P_type == "1") {
        if (P_num.length != 15 && P_num.length != 18) {
            return false;
        } else {
            return DocumentNum(number);
        }
    } else if (P_type == "2" || P_type == "5") {
        if (P_num.length < 8 || P_num.length > 18) {
            return false;
        }
    }
    return true;
}
// 正则判断是否是电话或者手机
function IsTelephone(obj) {
    var mobile = trim(obj);
    var pattern = /(^[0-9]{3,4}\-[0-9]{5,8}$)|(^[0-9]{5,8}$)|(^\([0-9]{3,4}\)[0-9]{3,8}$)|(0?(13|14|15|16|17|18|19)[0-9]{9})/;
    if (pattern.test(mobile)) {
        return true;
    } else {
        return false;
    }
}

// 判断身份证号码
function IsVoucherNumber(voucherNumber) {
    var regex = /^\d{6}(19|20)*[\d]{2}((0[1-9])|(1[0-2]))([012][\d]|(30|31))\d{3}[xX\d]*$/;
    if (voucherNumber.length != 15 && voucherNumber.length != 18) {
        return false;
    } else if (!regex.test(voucherNumber)) {
        return false;
    }
    return true;
}



function trim(strSource) {
    var i;
    var sTemp;
    var iIndex;
    i = strSource.length;
    if (i == 0) return "";
    sTemp = strSource;
    for (iIndex = 0; iIndex < i; iIndex++) {
        if (strSource.substring(iIndex, iIndex + 1) == " ") {
        } else {
            sTemp = strSource.substring(iIndex, i);
            break;
        }
    }
    if (iIndex == i) return "";
    for (iIndex = sTemp.length; iIndex > 0; iIndex--) {
        if (sTemp.substring(iIndex - 1, iIndex) == " ") {
        } else {
            sTemp = sTemp.substring(0, iIndex);
            break;
        }
    }

    return sTemp;
}
function trimString(strSource) {
    var sTemp = strSource.replace(/\s+/g, "");
    return sTemp;
}

/* 字符串不能为空 */
function IsEmpty(string) {
    var str = string.replace(/\s+/g, "");
    if (str == '') {
        return true;
    } else {
        return false;
    }
}

/* 验证字符是否为中文 */
function IsChinese(str) {
    var reg = /^[\u4E00-\u9FA5]+$/;
    if (!reg.test(str)) {
        return false;
    }
    return true;
}

/* 验证字符是否为中文或者英文 */
function isNotChinese(str) {
    var lst = /^[\u4e00-\u9fa5\a-zA-Z\/]+$/;
    if (lst.test(str)) {
        return false;
    }
    return true;
}

/* 验证字符是否为数字 */
function strIsNaN(number) {
    var strP = /^\d+$/;
    if (strP.test(number)) {
        return false;
    }
    return true;
}

function pwdCheck(pwd) {
    var reg = /(?=.*[0-9])(?=.*[A-Z])(?=.[a-z])(?=.*[^a-zA-Z0-9]).{8,30}/;
    if (reg.test(pwd)) {
        return false;
    }
    return true;
}

function formArrToObj(arr) {
    var obj = {};
    for (var key in arr) {
        let keyTemp = arr[key].name;
        let valueTemp = arr[key].value;
        obj[keyTemp] = valueTemp;
    }

    return obj;
}


function genRandomStr(Length) {
    const constant = [
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];
    var rdStr = "";
    for (var i = 0; i < Length; i++) {
        var randomNumber = Math.floor(Math.random() * 62);
        rdStr += constant[randomNumber];
    }
    return rdStr;
}

function genRandomNum(Length) {
    const constant = [
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];
    var rdNo = "";
    for (var i = 0; i < Length; i++) {
        var randomNumber = Math.floor(Math.random() * 9);
        rdNo += constant[randomNumber];
    }
    return rdNo;
}

function subStrText(str, length) {
    str = str.replace(/<[^>]+>/g, "").replace(/&nbsp;/ig, "");;
    if (str.length > length) {
        return str.substr(0, length) + "......";
    }
    else {
        return str;
    }
}

function isNumeric(str) {
    return typeof str === 'string' &&
        str.trim() !== '' &&
        !Number.isNaN(Number(str));
}

function isDate(str) {
    if (typeof str !== 'string' || str.trim() === '') return false;
    const d = new Date(str);
    return !Number.isNaN(d.getTime());
}
