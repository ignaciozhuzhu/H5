angular.module('starter.controllerspay', [])

//银联卡绑定页1
.controller('bindcardCtrl', function ($scope, $http) {
    //for (var i = 0; i < $("form input").length; i++) {
    //    var forminputname = $("form")[0][i].name;
    //    $("[name =" + forminputname + "]").val("");
    //}
    var mcMemberCode = getCookie('mcMemberCode');
    if (!mcMemberCode) {
        layermyui('请先去登录.');
    }
    $("form").css({ "display": "none" });
    $("#memberid").val(mcMemberCode);

    $scope.setFormData = function () {
        var memberid = $("#memberid").val();
        var card = $("#card").val();
        var nghttp = "../../ajax/payHandler.ashx?fn=bindcard&memberid=" + memberid + "&card=" + card + "";
        //loading层
        var mylayeruiwait = layer.load(1, {
            shade: [0.5, '#ababab'] //0.1透明度的白色背景
        });
        $http.get(nghttp).success(function (response) {
            find404admin(response);
            layer.close(mylayeruiwait);
            var x2js = new X2JS();
            var xmlText = response;
            var jsonObj = x2js.xml_str2json(xmlText);

            for (var i = 0; i < $("form input").length; i++) {
                var forminputname = $("form")[0][i].name;
                $("[name =" + forminputname + "]").val(jsonObj.unionBindModule[forminputname]);
            }
            document.getElementById("fuxxme").click();
        })
    }

})


//银联卡 支付,短信校验页
.controller('smsCtrl', function ($scope, $http, getbindquerysev) {
    var orderNo = getpbyurl(1);
    var price = getpbyurl(2);
    //测试可以将price mock 为0.01
    //price = 0.01;

    $scope.changecard = function () {
        window.location.href = "#/app/card/bind/" + orderNo + "";
        location.reload();
    }

    var funcallback = function (object) {
        if (object.userToken) {
            $(".bindbox #card").val(object.userToken.preCardNo + "*************" + object.userToken.card);
            return;
        }
    }
    getbindquerysev.bindquetyfunc($http, funcallback);

    $scope.paybyunion = function () {
        var txnTime = $scope.txnTime;
        var smscode = $("#smscode").val();
        var card = $("#card").val();
        var accountName = "JJE_APP_UNION_PAY";
        var payAmount = $scope.payAmount;
        if (!smscode) {
            layermyui('请输入短信验证码.');
            return;
        }
        if (!card) {
            layermyui('请输入卡号.');
            return;
        }
        var nghttp = "../../ajax/apihandler.ashx?fn=pbppayorder&orderNo=" + orderNo + "&payAmount=" + payAmount + "&accountName=" + accountName + "&txnTime=" + txnTime + "&smscode=" + smscode + "";
        //loading层
        var mylayeruiwait = layer.load(1, {
            shade: [0.5, '#ababab'] //0.1透明度的白色背景
        });
        $http.get(nghttp).success(function (response) {
            layer.close(mylayeruiwait);
            var x2js = new X2JS();
            var xmlText = response;
            var jsonObj = x2js.xml_str2json(xmlText);
            if (response) {
                layermyui(response);
                return;
            }
            if (response == "") {
                layermyui("支付完成");
                setTimeout(function () {
                    window.location.href = "#/app/card/paysuccess";
                }, 2000)
                return;
            }
            if (jsonObj.errorMessage) {
                layermyui(jsonObj.errorMessage.message);
                return;
            }
        })
    }

    //发送短信验证码
    $scope.sends = {
        checked: 1,
        send: function () {
            function timeCountDown() {
                if (time == 0) {
                    clearInterval(timer);
                    $('.div-phone a').removeClass('send0').html("发送验证码");
                    sends.checked = 1;
                    return true;
                }
                else {
                    sends.checked = 0;
                    $('.div-phone a').html(time + "秒后再次发送");
                    time--;
                    return false;
                }
            }
            if (sends.checked == 1) {
                var memberid = getCookie('mcMemberCode');
                if (!memberid) {
                    layermyui('请先去登录.');
                }
                var time = 30;
                $('.div-phone span').remove();
                $('.div-phone a').addClass('send0');
                timeCountDown();
                var timer = setInterval(timeCountDown, 1000);
                var txnTime = gettimenow();
                $scope.txnTime = txnTime;
                $scope.payAmount = price;
                //传入后台操作
                $.ajax({
                    url: "../../ajax/payHandler.ashx?fn=sms&memberid=" + memberid + "&orderNo=" + orderNo + "&price=" + price + "&txnTime=" + txnTime + "",
                    type: 'post',
                    success: function (response) {
                        //这里的response 他们提供的接口 会返回空值.
                    }
                });
            }
        }
    }

})


//支付成功回调页
.controller('paysuccessCtrl', function ($scope, $http) {
    $scope.goindex = function () {
        window.location.href = "#/app/index";
    }
    $scope.goorder = function () {
        window.location.href = "#/app/user/myorder";
    }
    
})

