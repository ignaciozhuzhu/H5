$(function () {
    $("body").keydown(function (event) {
        var type = event.srcElement.type;
        if ( event.keyCode == 8) {
            if ((type != 'text') && (type != 'textarea') && (type != 'password')) {
                window.event.keyCode = 0;
                window.event.returnValue = false;
            }
        }else if (event.keyCode == 116) {
            window.event.keyCode = 0;
            window.event.returnValue = false;
        }
    });
    $("body").mouseover(function () {
        window.parent.lastgotime = new Date();
    });

    $("body").keydown(function () {
        window.parent.lastgotime = new Date();
    });
    $("<input>").keydown(function () {
        window.parent.lastgotime = new Date();
    });
})


function getCookie(check_name) {
    var a_all_cookies = document.cookie.split(';');
    var a_temp_cookie = '';
    var cookie_name = '';
    var cookie_value = '';
    var b_cookie_found = false;
    for (i = 0; i < a_all_cookies.length; i++) {
        a_temp_cookie = a_all_cookies[i].split('=');
        cookie_name = a_temp_cookie[0].replace(/^\s+|\s+$/g, '');
        if (cookie_name == check_name) {
            b_cookie_found = true;
            if (a_temp_cookie.length > 1)
                cookie_value = unescape(a_temp_cookie[1].replace(/^\s+|\s+$/g, ''));
            return cookie_value;
        }
        a_temp_cookie = null;
        cookie_name = '';
    }
    if (!b_cookie_found)
        return null;
}
function setCookie(sName, sValue) {
    try {
        var sCookie = sName + "=" + encodeURIComponent(sValue);
        document.cookie = sCookie;
    }
    catch (e) { }
}
function jsdatetostring(d) {
    if (d == null || d == "") {
        return "";
    } else {
        return d.getFullYear() + "-" + (d.getMonth() / 1 + 1) + "-" + (d.getDate() / 1);
    }

}
function jsdateTimetostring(d) {
    if (d == null || d == "") {
        return "";
    } else {
        return d.getFullYear() + "-" + (d.getMonth() / 1 + 1) + "-" + (d.getDate() / 1);
    }

}
function jsdateandtimetostring(d) {
    if (d == null || d == "") {
        return "";
    } else {
        return d.getFullYear() + "-" + (d.getMonth() / 1 + 1) + "-" + (d.getDate() / 1) + " " + d.getHours() + ":" + d.getMinutes() + ":" + d.getSeconds();
    }

}
//获取URL的参数
function request(paras) {
    var url = location.href;
    var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
    var paraObj = {}
    for (i = 0; j = paraString[i]; i++) {
        paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
    }
    var returnValue = paraObj[paras.toLowerCase()];
    if (typeof (returnValue) == "undefined") {
        return "";
    } else {
        return returnValue;
    }
}
String.prototype.replaceAll = function (reallyDo, replaceWith, ignoreCase) {
    if (!RegExp.prototype.isPrototypeOf(reallyDo)) {
        return this.replace(new RegExp(reallyDo, (ignoreCase ? "gi" : "g")), replaceWith);
    } else {
        return this.replace(reallyDo, replaceWith);
    }
}

