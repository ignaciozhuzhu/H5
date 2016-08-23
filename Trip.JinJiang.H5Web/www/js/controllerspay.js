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
.controller('smsCtrl', function ($scope, $http) {
    var mcMemberCode = getCookie('mcMemberCode');
    if (!mcMemberCode) {
        layermyui('请先去登录.');
    }
})

//发送短信验证码
var sends = {
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
            var time = 30;
            $('.div-phone span').remove();
            $('.div-phone a').addClass('send0');
            timeCountDown();
            var timer = setInterval(timeCountDown, 1000);

            var memberid = "10057908";
            var orderNo = "1000160822000002";
            var price = 0.01;
            var txnTime = gettimenow();

            //传入后台操作
            $.ajax({
                url: "../../ajax/payHandler.ashx?fn=sms&memberid=" + memberid + "&orderNo=" + orderNo + "&price=" + price + "&txnTime=" + txnTime + "",
                type: 'post',
                success: function (response) {
                    //  debugger
                }
            });
        }
    }
}
