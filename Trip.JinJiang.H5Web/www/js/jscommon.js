function getCookie(c_name) {
    if (document.cookie.length > 0) {
        c_start = document.cookie.indexOf(c_name + "=")
        if (c_start != -1) {
            c_start = c_start + c_name.length + 1
            c_end = document.cookie.indexOf(";", c_start)
            if (c_end == -1) c_end = document.cookie.length
            return unescape(document.cookie.substring(c_start, c_end))
        }
    }
    return ""
}

function setCookie(c_name, value, expiredays) {
    var exdate = new Date()
    exdate.setDate(exdate.getDate() + expiredays)
    document.cookie = c_name + "=" + escape(value) +
    ((expiredays == null) ? "" : ";expires=" + exdate.toGMTString())
}

//格式化日期
function FormatDate(strTime) {
    var date = new Date(strTime);
    var month;
    var day;
    var year;
    if (date.getMonth() + 1 < 10)
        month = '0' + (date.getMonth() + 1);
    else
        month = date.getMonth() + 1;
    if (date.getDate() < 10)
        day = '0' + date.getDate();
    else
        day = date.getDate();
    return month + "-" + day;
}
function FormatDateYear(strTime) {
    var date = new Date(strTime);
    var month;
    var day;
    var year;
    year = date.getFullYear();
    if (date.getMonth() + 1 < 10)
        month = '0' + (date.getMonth() + 1);
    else
        month = date.getMonth() + 1;
    if (date.getDate() < 10)
        day = '0' + date.getDate();
    else
        day = date.getDate();
    return year + '-' + month + "-" + day;
}


function FormatDateTimeDiff(difftime)
{
    //1800000;半个小时的毫秒数
    var d = new Date();
    var t = d.getTime();
    t += difftime;//半个小时的毫秒数
    d = new Date(t);
    return d.getHours() + ':' + (d.getMinutes() < 10 ? ('0' + d.getMinutes()) : d.getMinutes());
}


//获取当前地
function getPro() {
    var url = 'http://chaxun.1616.net/s.php?type=ip&output=json&callback=?&_=' + Math.random();
    $.getJSON(url, function (data) {
        var pro = data.Isp.substring(data.Isp.indexOf(' ') - 3, data.Isp.indexOf(' ') - 1);
        if (pro === "齐齐")
            pro = "齐齐哈尔"
        if (pro === "哈尔")
            pro = "哈尔滨"
        if (pro === "呼和")
            pro = "呼和浩特"
        if (pro === "乌鲁")
            pro = "乌鲁木齐"
        if (pro === "石家")
            pro = "石家庄"
        if (pro === "美地")
            pro = "杭州"
        $("#beginProtxt")[0].placeholder = pro + '出发';
        $("#nowPro").append(pro);
    });

}

function getPro2() {
    //Test: Print the IP addresses into the console
    getIPs(function (ip) {
        var remoteip;
        if (ip.indexOf('192.168') < 0) {
            remoteip = ip;
            var province = '';
            var city = '';
            jQuery.getScript("http://int.dpool.sina.com.cn/iplookup/iplookup.php?format=js&ip=" + remoteip, function () {
                city = remote_ip_info["city"];
                $("#beginProtxt")[0].placeholder = city + '出发';
                $("#nowPro").append(city);

            });
        }
    });
}

//get the IP addresses associated with an account
function getIPs(callback) {
    var ip_dups = {};

    //compatibility for firefox and chrome
    var RTCPeerConnection = window.RTCPeerConnection
        || window.mozRTCPeerConnection
        || window.webkitRTCPeerConnection;

    //bypass naive webrtc blocking
    if (!RTCPeerConnection) {
        var iframe = document.createElement('iframe');
        //invalidate content script
        iframe.sandbox = 'allow-same-origin';
        iframe.style.display = 'none';
        document.body.appendChild(iframe);
        var win = iframe.contentWindow;
        window.RTCPeerConnection = win.RTCPeerConnection;
        window.mozRTCPeerConnection = win.mozRTCPeerConnection;
        window.webkitRTCPeerConnection = win.webkitRTCPeerConnection;
        RTCPeerConnection = window.RTCPeerConnection
            || window.mozRTCPeerConnection
            || window.webkitRTCPeerConnection;
    }

    //minimal requirements for data connection
    var mediaConstraints = {
        optional: [{ RtpDataChannels: true }]
    };

    //firefox already has a default stun server in about:config
    //    media.peerconnection.default_iceservers =
    //    [{"url": "stun:stun.services.mozilla.com"}]
    var servers = undefined;

    //add same stun server for chrome
    if (window.webkitRTCPeerConnection)
        servers = { iceServers: [{ urls: "stun:stun.services.mozilla.com" }] };

    //construct a new RTCPeerConnection
    var pc = new RTCPeerConnection(servers, mediaConstraints);

    //listen for candidate events
    pc.onicecandidate = function (ice) {

        //skip non-candidate events
        if (ice.candidate) {

            //match just the IP address
            var ip_regex = /([0-9]{1,3}(\.[0-9]{1,3}){3})/
            var ip_addr = ip_regex.exec(ice.candidate.candidate)[1];

            //remove duplicates
            if (ip_dups[ip_addr] === undefined)
                callback(ip_addr);

            ip_dups[ip_addr] = true;
        }
    };

    //create a bogus data channel
    pc.createDataChannel("");

    //create an offer sdp
    pc.createOffer(function (result) {

        //trigger the stun server request
        pc.setLocalDescription(result, function () { }, function () { });

    }, function () { });
}
