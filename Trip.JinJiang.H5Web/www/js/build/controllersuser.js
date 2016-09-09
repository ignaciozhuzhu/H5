angular.module('starter.controllersuser', [])

//注册控制器
.controller('registerCtrl', ['$scope', '$http', function ($scope, $http) {
    nofollowfunc();
    var ipl;
    var ip4;
    var ip6;

    $scope.regist = function () {
        //debugger
        //需要传递到后台的XML报文串:
        var passwordPlain = $('.register #password')[0].value;  //明文密码
        //passwordPlain = 'xtsb1';
        var mh5pw = hex_md5(passwordPlain); //MD5密码
        var sha = hex_sha1(passwordPlain);
        var certificateType = $('.register #certificateType')[0].value;    //证件类型
        var certificateNo = $('.register #certificateNo')[0].value;  //证件号
        var email = $('.register #email')[0].value;  //邮箱
        var mobile = $('.register #phone')[0].value; //手机
        var title = $("input[name='Sex'][checked]").val();;  //称谓 
        var surname = $('.register #surname')[0].value;    //姓名

        var obj = document.getElementsByName("Sex")
        for (var i = 0; i < obj.length; i++) { //遍历Radio 
            if (obj[i].checked) {
                title = obj[i].value;
            }
        }

        //前端数据验证
        if ($('.register #phone')[0].value === "") {
            layermyui('请输入手机号');
            return;
        }
        if ($('.register #email')[0].value === "") {
            layermyui('请输入邮箱');
            return;
        }

        if (!checkMobile($('.register #phone')[0].value)) {
            layermyui('手机号格式不正确');
            return;
        }
        if (!isEmail($('.register #email')[0].value)) {
            layermyui('邮箱格式不正确');
            return;
        }
        if ($('.register #password')[0].value === "") {
            layermyui('请输入密码');
            return;
        }
        if (!isPassword($('.register #password')[0].value)) {
            layermyui('密码过于简单,不能少于六位数');
            return;
        }
        if ($('.register #password2')[0].value === "") {
            layermyui('请输入确认密码');
            return;
        }
        if ($('.register #password2')[0].value !== $('.register #password')[0].value) {
            layermyui('两次密码不一致');
            return;
        }
        if (surname === "") {
            layermyui('请输入姓名');
            return;
        }
        if (certificateNo === "") {
            layermyui('请输入证件号');
            return;
        }
        if (!isIdCardNo(certificateNo)) {
            layermyui('证件号不对');
            return;
        }
        if (title == "" || title == undefined || title == null) {
            layermyui('请选择称谓');
            return;
        }

        certificateType = 'ID';

        //懒得写入作为测试用例填入后台.
        //certificateNo = '33252211512';
        //email = '1122312@qq.com'; //<mcMemberCode>10059061</mcMemberCode>
        //mobile = '1511112';
        //title = 'Mr.';
        //surname = '赵云';

        var xml = "<memberRegisterDto><memberInfoDto>";
        xml += "<memberType>Silver Card</memberType>";
        xml += "<certificateNo>" + certificateNo + "</certificateNo>";
        xml += "<certificateType>" + certificateType + "</certificateType>";
        xml += "<email>" + email + "</email>";
        xml += "<mobile>" + mobile + "</mobile>";
        xml += "<scoreType>1</scoreType>";
        xml += "<title>" + title + "</title>";
        xml += "<surname>" + surname + "</surname>";
        xml += "<memberScoreType>SCORE</memberScoreType>";
        xml += "<registerSource>Website</registerSource>";
        xml += "<passsword>" + mh5pw + "</passsword>";
        xml += "<sha1pwd>" + sha + "</sha1pwd>";
        xml += "<ipAddress>" + ip4 + "</ipAddress>";
        xml += "</memberInfoDto></memberRegisterDto>";
        var nghttp = "../../ajax/userHandler.ashx?fn=regist&xml=" + xml + "";
        //alert('正在加载,请稍后...');
        //loading层
        var mylayeruiwait = layer.load(1, {
            shade: [0.5, '#ababab'] //0.1透明度的白色背景
        });
        $http.get(nghttp).success(function (response) {
            //debugger
            // $.unblockUI();
            find404admin(response);
            layer.close(mylayeruiwait);
            var x2js = new X2JS();
            var xmlText = response;
            var jsonObj = x2js.xml_str2json(xmlText);
            if (jsonObj.crmResponseDto.retcode == "00001") {
                var amount = getCookie('tolamount');
                var cnum = getCookie('cnum');
                var pnum = getCookie('pnum');
                var groupid = getCookie('groupid');
                var secureamount = getCookie('secureamount');

                //设置客户cookie信息.
                setCookie('mcMemberCode', jsonObj.crmResponseDto.mcMemberCode, 1);
                setCookie('phonenum', mobile, 1);
                //setCookie('fullName', jsonObj.memberMergeDto.fullName, 1);
                //setCookie('cdsId', jsonObj.memberMergeDto.cdsId, 1);

                if (groupid > 0) {
                    layermyui('注册成功!将自动为您跳转...');
                    window.location.href = '#/app/payway/' + secureamount + '/' + groupid + '/' + pnum + '/' + cnum + '/' + amount;
                }
                else {
                    layermyui('注册成功!将自动为您跳转回首页...');
                    window.location.href = '#/app/index';
                }
            }
            else
                layermyui(jsonObj.crmResponseDto.retmsg);
        })
    }

    $scope.$on("$ionicView.loaded", function () {

        getIPs2(function (ip) {
            var li = document.createElement("li");
            li.textContent = ip;
            //local IPs
            if (ip.match(/^(192\.168\.|169\.254\.|10\.|172\.(1[6-9]|2\d|3[01]))/)) {
                document.getElementsByTagName("ul")[0].appendChild(li);
                ipl = ip;
            }
                //IPv6 addresses
            else if (ip.match(/^[a-f0-9]{1,4}(:[a-f0-9]{1,4}){7}$/)) {
                ip6 = ip;
                document.getElementsByTagName("ul")[2].appendChild(li);
            }
                //assume the rest are public IPs
            else {
                ip4 = ip
                document.getElementsByTagName("ul")[1].appendChild(li);
            }
        });
    })

}])
//登录控制器
.controller('loginCtrl', ['$scope', '$http', function ($scope, $http) {
    nofollowfunc();

    $scope.login = function () {

        //需要传递到后台的XML报文串:
        var loginname = $('.login #loginname')[0].value;  //用户名
        var passwordPlain = $('.login #password')[0].value;  //明文密码
        //passwordPlain = 'xtbssb3';
        var mh5pw = hex_md5(passwordPlain); //MD5密码
        var sha = hex_sha1(passwordPlain);  //sha1密码
        //懒得写入作为测试用例填入后台.
        //loginname = '18505793685';

        if (!loginname) {
            layermyui('请输入手机号/邮箱');
            return;
        }
        if (!passwordPlain) {
            layermyui('请输入密码');
            return;
        }

        var xml = "<mergeLoginDto>";
        xml += "<loginName>" + loginname + "</loginName>";
        xml += "<md5>" + mh5pw + "</md5>";
        xml += "<sha1>" + sha + "</sha1>";
        xml += "</mergeLoginDto>";
        var nghttp = "../../ajax/userHandler.ashx?fn=login&xml=" + xml + "";
        //loading层
        var mylayeruiwait = layer.load(1, {
            shade: [0.5, '#ababab'] //0.1透明度的白色背景
        });
        $http.get(nghttp).success(function (response) {
            //debugger
            find404admin(response);
            //  $.unblockUI();
            layer.close(mylayeruiwait);
            var x2js = new X2JS();
            var xmlText = response;
            var jsonObj = x2js.xml_str2json(xmlText);
            if (jsonObj.memberMergeDto.remark === "登录成功") {
                var amount = getCookie('tolamount');
                var cnum = getCookie('cnum');
                var pnum = getCookie('pnum');
                var groupid = getCookie('groupid');
                var secureamount = 0;

                //设置客户cookie信息.
                setCookie('mcMemberCode', jsonObj.memberMergeDto.mcMemberCode, 1);
                setCookie('fullName', jsonObj.memberMergeDto.fullName, 1);
                setCookie('phonenum', loginname, 1);
                //setCookie('cdsId', jsonObj.memberMergeDto.cdsId, 1);
                layermyui('登录成功!');
                var ckmcMemberCode = getCookie('mcMemberCode');
                var linkbackpay = getCookie('linkbackpay');
                if (ckmcMemberCode !== "" && ckmcMemberCode !== undefined && ckmcMemberCode !== null) {
                    $('#account').empty().append('退出登录');
                    var fullName = getCookie('fullName');
                    $('#myinfofullName').empty().append(fullName);
                    $('#account').css({ "display": "block" })
                }
                //debugger
                if (groupid > 0 && linkbackpay === 'true') {
                    setCookie('linkbackpay', '', 1);
                    window.location.href = '#/app/fillorder/' + secureamount + '/' + groupid + '/' + pnum + '/' + cnum + '/' + amount;
                }
                else {
                    window.location.href = '#/app/index';
                }
            }
            else {
                layermyui(jsonObj.memberMergeDto.remark);
            }
        })
    }

    $scope.backhist = function () {
        window.history.back();
    }
}])

//快速注册控制器
.controller('quickregisterCtrl', ['$scope', '$http', function ($scope, $http) {
    nofollowfunc();

    $scope.regist = function () {

        //debugger
        //需要传递到后台的XML报文串:
        var passwordPlain = $('.quickregister #password')[0].value;  //明文密码
        //passwordPlain = 'xtsb1';
        var mh5pw = hex_md5(passwordPlain); //MD5密码
        var sha = hex_sha1(passwordPlain);
        var email = $('.quickregister #email')[0].value;  //邮箱
        var mobile = $('.quickregister #phone')[0].value; //手机

        //前端数据验证
        if ($('.quickregister #phone')[0].value === "") {
            layermyui('请输入手机号');
            return;
        }
        if ($('.quickregister #email')[0].value === "") {
            layermyui('请输入邮箱');
            return;
        }
        if (!checkMobile($('.quickregister #phone')[0].value)) {
            layermyui('手机号格式不正确');
            return;
        }
        if (!isEmail($('.quickregister #email')[0].value)) {
            layermyui('邮箱格式不正确');
            return;
        }
        if ($('.quickregister #password')[0].value === "") {
            layermyui('请输入密码');
            return;
        }
        if (!isPassword($('.quickregister #password')[0].value)) {
            layermyui('密码过于简单,不能少于六位数');
            return;
        }
        if ($('.quickregister #password2')[0].value === "") {
            layermyui('请输入确认密码');
            return;
        }
        if ($('.quickregister #password2')[0].value !== $('.quickregister #password')[0].value) {
            layermyui('两次密码不一致');
            return;
        }

        var xml = "<webMemberDto>";
        xml += "<email>" + email + "</email>";
        xml += "<phone>" + mobile + "</phone>";
        xml += "<pwd>" + mh5pw + "</pwd>";
        xml += "<sha1pwd>" + sha + "</sha1pwd>";
        xml += "<registChannel>Website</registChannel>";
        xml += "<registTag>IOS|JJTRAVEL_IOS_1|JinJiang</registTag>";
        xml += "</webMemberDto>";
        var nghttp = "../../ajax/userHandler.ashx?fn=quickregist&xml=" + xml + "";
        //alert('正在提交,请稍后...');
        //loading层
        var mylayeruiwait = layer.load(1, {
            shade: [0.5, '#ababab'] //0.1透明度的白色背景
        });
        $http.get(nghttp).success(function (response) {
            //$.unblockUI();
            find404admin(response);
            layer.close(mylayeruiwait);
            //debugger
            var x2js = new X2JS();
            var xmlText = response;
            var jsonObj = x2js.xml_str2json(xmlText);
            var mcMemberCode;
            try {
                mcMemberCode = jsonObj.webMemberRegisterReturnDto.mcMemberCode;
            }
            catch (err) {
                mcMemberCode = "";
            }

            if (mcMemberCode !== "" && mcMemberCode !== undefined) {
                var amount = getCookie('tolamount');
                var cnum = getCookie('cnum');
                var pnum = getCookie('pnum');
                var groupid = getCookie('groupid');
                var secureamount = getCookie('secureamount');

                //设置客户cookie信息.
                setCookie('mcMemberCode', jsonObj.webMemberRegisterReturnDto.mcMemberCode, 1);
                setCookie('phonenum', mobile, 1);
                //setCookie('fullName', jsonObj.memberMergeDto.fullName, 1);
                //setCookie('cdsId', jsonObj.memberMergeDto.cdsId, 1);

                layermyui('注册成功!将自动为您跳转回支付页面...');
                if (groupid > 0) {
                    window.location.href = '#/app/payway/' + secureamount + '/' + groupid + '/' + pnum + '/' + cnum + '/' + amount;
                }
                else {
                    window.location.href = '#/app/index';
                }
            }
            else {
                layermyui(jsonObj.webMemberRegisterReturnDto.message);
                return;
            }
        })
    }

}])
//密码找回控制器
.controller('forgetpwdCtrl', ['$scope', '$http', function ($scope, $http) {
    nofollowfunc();

    $scope.forgetpwd = function () {
        if ($('.forgetpwd0 #name')[0].value === "") {
            layermyui('请输入姓名');
            return;
        }
        if ($('.forgetpwd0 #phone')[0].value === "") {
            layermyui('请输入手机');
            return;
        }
        if ($('.forgetpwd0 #code')[0].value === "") {
            layermyui('请输入验证码');
            return;
        }
        if (!isPassword($('.forgetpwd0 #newpwd')[0].value)) {
            layermyui('密码过于简单,不能少于六位数');
            return;
        }

        var validateCode = $('.forgetpwd0 #code')[0].value;
        var loginName = $('.forgetpwd0 #phone')[0].value;
        var fullName = $('.forgetpwd0 #name')[0].value;
        var mh5pw = hex_md5($('.forgetpwd0 #newpwd')[0].value); //MD5密码
        var sha = hex_sha1($('.forgetpwd0 #newpwd')[0].value);

        var nghttp = "../../ajax/userHandler.ashx?fn=forgetpwd";
        nghttp += "&validateCode=" + validateCode + "";
        nghttp += "&loginName=" + loginName + "";
        nghttp += "&fullName=" + fullName + "";
        nghttp += "&md5=" + mh5pw + "";
        nghttp += "&sha1=" + sha + "";
        //loading层
        var mylayeruiwait = layer.load(1, {
            shade: [0.5, '#ababab'] //0.1透明度的白色背景
        });
        $http.get(nghttp).success(function (response) {
            //debugger
            find404admin(response);
            layer.close(mylayeruiwait);
            var x2js = new X2JS();
            var xmlText = response;
            var jsonObj = x2js.xml_str2json(xmlText);
            if (jsonObj.memberBaseDto.rtcode === "success") {
                layermyui('密码修改完成,<br>将跳转至登录页面', 3000);
                window.setTimeout("window.location='#/app/user/login'", 3000);
                return;
            }
            else {
                layermyui('修改失败,请检查.');
                return;
            }
        })
    }

}])
//我的订单控制器
.controller('myorderCtrl', ['$scope', '$http', '$ionicScrollDelegate','$ionicHistory', function ($scope, $http, $ionicScrollDelegate, $ionicHistory) {
    nofollowfunc();
    //var mcMemberCode = "";
    var mcMemberCode = getCookie('mcMemberCode');
    var orderStatus = "";
    var payStatus = "";

    //40
    var a = $(".header0")[0].offsetTop;
    if (a < 40) {
        $(".header0").css({ "margin-top": "35px" });
    }
    else {
        $(".header0").css({ "margin-top": "0px" });
    }
    //如果是从首页按自带返回过来的
   // debugger
    if ($ionicHistory.viewHistory().backView == null) {
        $(".header0").css({ "margin-top": "0px" });
    }
    removeclassyellow();
    addclassyellow(0);
    var nghttp = "../../ajax/userHandler.ashx?fn=queryorder&mcMemberCode=" + mcMemberCode + "&orderStatus=" + orderStatus + "&payStatus=" + payStatus + "";
    getorders(nghttp);

    function getorders(mynghttp) {
        var mylayeruiwait = layer.load(1, {
            shade: [0.5, '#ababab'] //0.1透明度的白色背景
        });
        $http.get(mynghttp, { cache: false }).success(function (response) {
            //debugger
            layer.close(mylayeruiwait);
            find404admin(response);
            $scope.orders = response.orders;
            for (var i = 0; i < response.orders.length; i++) {

                if (response.orders[i].paymentAmount) {
                    $scope.orders[i].discountafterAmount = response.orders[i].paymentAmount;
                }
                else if (!response.orders[i].paymentAmount && response.orders[i].payStatusName != "已支付") {
                    $scope.orders[i].discountafterAmount = response.orders[i].amount;
                    $scope.orders[i].orderStatusName = "待确认-门店支付";
                    // debugger
                    //$(".doorremark").css({ "display": "none" });
                }
                //Math.ceil(response.orders[i].amount / response.orders[i].adultNum * 0.98) * response.orders[i].adultNum;
                if ($scope.orders[i].payStatusName == "已支付") {
                    $scope.orders[i].orderStatusName = "已支付";
                }
                if ($scope.orders[i].payStatusName == "待支付") {
                    $scope.orders[i].orderStatusName = "待支付";
                }
            }

            setTimeout(settypepng, 0);
        })
    }

    function addclassyellow(q) {
        $('.header0 div:eq(' + q + ')').css("color", "#f59609");
    }
    function removeclassyellow(q) {
        $('.header0 div').css("color", "#beb9c0");
    }

    $scope.seedetail = function ($event) {
        var ordercode = event.currentTarget.lastElementChild.innerText;
        window.location.href = '#/app/user/orderdetail/' + ordercode + '';
    }

    $scope.cancelorder = function ($event) {
        var ordercode = event.currentTarget.lastElementChild.innerText;
        var mcMemberCode = getCookie('mcMemberCode');
        if (event.currentTarget.parentNode.previousElementSibling.previousElementSibling.childNodes[1].children[3].innerText == "待支付") {
            var mylayeruiwait = layer.load(1, {
                shade: [0.5, '#ababab'] //0.1透明度的白色背景
            });
            $.ajax({
                url: "../../ajax/userHandler.ashx?fn=cancelorder&orderCode=" + ordercode + "&mcMemberCode=" + mcMemberCode,
                type: 'post',
                success: function (response) {
                    //debugger
                    layer.close(mylayeruiwait);
                    var d = eval("(" + response + ")");
                    if (!d.errorCode) {
                        alert("取消成功");
                        location.reload();
                        return;
                    }
                    else {
                        alert(d.errorMsg);
                        return;
                    }
                }
            });
        }
        else if (event.currentTarget.parentNode.previousElementSibling.previousElementSibling.childNodes[1].children[3].innerText == "已支付") {
            layermyui('您的订单为已支付订单，需确认退款金额，请及时与客服联系，客服电话10101666*3，客服工作时间：8点至21点。');
        }
    }

    $scope.li0 = function () {
        //debugger
        //自动滚动到头部
        $ionicScrollDelegate.$getByHandle('mainScroll').scrollTop();
        removeclassyellow();
        addclassyellow(0);
        var orderStatus = "";
        var payStatus = "";
        var mynghttp = "../../ajax/userHandler.ashx?fn=queryorder&mcMemberCode=" + mcMemberCode + "&orderStatus=" + orderStatus + "&payStatus=" + payStatus + "";
        getorders(mynghttp);
    }
    $scope.li1 = function () {
        //自动滚动到头部
        $ionicScrollDelegate.$getByHandle('mainScroll').scrollTop();
        removeclassyellow();
        addclassyellow(1);
        var orderStatus = "CONFIRMED";
        var payStatus = "PAY_WAITING";
        var mynghttp = "../../ajax/userHandler.ashx?fn=queryorder&mcMemberCode=" + mcMemberCode + "&orderStatus=" + orderStatus + "&payStatus=" + payStatus + "";
        getorders(mynghttp);
    }
    $scope.li2 = function () {
        //自动滚动到头部
        $ionicScrollDelegate.$getByHandle('mainScroll').scrollTop();
        removeclassyellow();
        addclassyellow(2);
        var orderStatus = "";
        var payStatus = "PAYED";
        var mynghttp = "../../ajax/userHandler.ashx?fn=queryorder&mcMemberCode=" + mcMemberCode + "&orderStatus=" + orderStatus + "&payStatus=" + payStatus + "";
        getorders(mynghttp);
    }
    $scope.li3 = function () {
        //自动滚动到头部
        $ionicScrollDelegate.$getByHandle('mainScroll').scrollTop();
        removeclassyellow();
        addclassyellow(3);
        var orderStatus = "CANCELED";
        var payStatus = ""
        var mynghttp = "../../ajax/userHandler.ashx?fn=queryorder&mcMemberCode=" + mcMemberCode + "&orderStatus=" + orderStatus + "&payStatus=" + payStatus + "";
        getorders(mynghttp);
    }
    $scope.li4 = function () {
        //自动滚动到头部
        $ionicScrollDelegate.$getByHandle('mainScroll').scrollTop();
        removeclassyellow();
        addclassyellow(4);
        var orderStatus = "FAILED";
        var payStatus = ""
        var mynghttp = "../../ajax/userHandler.ashx?fn=queryorder&mcMemberCode=" + mcMemberCode + "&orderStatus=" + orderStatus + "&payStatus=" + payStatus + "";
        getorders(mynghttp);

    }

}])
//订单详情控制器
.controller('orderdetailCtrl', ['$scope', '$http', function ($scope, $http) {
    nofollowfunc();

    //暂时作为测试用
    // var orderCode = 1000160531000003;
    var groupid;
    var cnum;
    var anum;
    var paymentAmount;
    var orderCode = getpbyurl(1);
    var nghttp = "../../ajax/userHandler.ashx?fn=queryorderdetail&code=" + orderCode;
    //loading层
    var mylayeruiwait = layer.load(1, {
        shade: [0.5, '#ababab'] //0.1透明度的白色背景
    });
    $http.get(nghttp).success(function (response) {
        find404admin(response);
        groupid = response.groupId;
        cnum = response.childNum;
        anum = response.adultNum;
        paymentAmount = response.paymentAmount;
        $scope.status = response.payStatus == "PAYED" ? "已支付" : "待支付";

        //如果是已支付或者是已取消的订单,则不显示去支付按钮
        if ($scope.status == "已支付" || response.orderStatus == "CANCELED") {
            $(".orderdetail .seebutton").css("display", "none");

            if (response.orderStatus == "CANCELED")
                $scope.status = "已取消"
        }
        $scope.lineName = response.lineName;
        //订单信息
        $scope.orderCode = response.orderCode;
        $scope.departrueDate = FormatDateYear(response.departrueDate);
        $scope.returnDate = FormatDateYear(response.returnDate);
        $scope.guestsNum = response.guests.length;

        //联系人信息
        $scope.contactName = response.contactName;
        $scope.contactMobile = response.contactMobile;
        $scope.contactEmail = response.contactEmail;

        //出行人信息
        $scope.guests = response.guests;

        //需支付金额
        $scope.paymentAmount = response.paymentAmount;

        //判断是否可以在线支付
        var nghttp = "../../ajax/apihandler.ashx?fn=queryrealtimerefresh&groupid=" + response.groupId + "";
        $http.get(nghttp).success(function (response) {
            layer.close(mylayeruiwait);
            if (response.payOnlineFlag == 0) {
                layermyui('不允许在线支付,请到门店支付');
                $scope.isDisabled = true;
                $(".orderbutton").attr("class", "orderbutton cancel");
                return;
            }
        })

    })

    //var accountName = 'JJE_APP_CLIENT_ALI_WAP_PAY';
    $scope.pay = function () {
        //首先做身份认证,判断是否已经登录,没有帐号的客户先注册.
        var mcMemberCode = getCookie('mcMemberCode');
        if (mcMemberCode != "" && mcMemberCode != undefined && mcMemberCode != null) {
            //其次再是发起付款
            //var orderNo = $scope.orderCode;
            //var amount = $scope.paymentAmount;
            //var nghttp = "../../ajax/apihandler.ashx?fn=pbppayorder&orderNo=" + orderNo + "&payAmount=" + amount + "&accountName=" + accountName + "";
            ////alert('正在加载,请稍后...');
            ////loading层
            //var mylayeruiwait = layer.load(1, {
            //    shade: [0.5, '#ababab'] //0.1透明度的白色背景
            //});
            //$http.get(nghttp).success(function (response) {
            //    //$.unblockUI();
            //    find404admin(response);
            //    layer.close(mylayeruiwait);
            //    window.location.href = response;
            //})

            //现在跳转到付款页即可,不需要直接跳支付宝(上面的逻辑是直接跳支付宝). 8.22 
            window.location.href = "#/app/payway/" + orderCode + "/" + groupid + "/" + anum + "/" + cnum + "/" + paymentAmount + "";
        }
        else {//把参数存入cookie
            setCookie('tolamount', amount, 1);
            setCookie('cnum', cnum, 1);
            setCookie('pnum', pnum, 1);
            setCookie('groupid', groupid, 1);
            setCookie('orderNo', orderCode, 1);
            //跳转至登录页
            window.location.href = '#/app/user/login';
        }

    }

}])
//个人中心控制器
.controller('myinfoCtrl', ['$scope', '$http', '$timeout', function ($scope, $http, $timeout) {
    nofollowfunc();
    //debugger
    //判断是否已经登录帐号,获取membercode 的cookie
    var ckmcMemberCode = getCookie('mcMemberCode');
    if (getCookie('fullName')) {
        $("#myinfofullName").empty().append(getCookie('fullName'));
        $('#account').css({ "display": "block" })
    }
    else {
        $('#account').css({ "display": "none" })
    }

    if (ckmcMemberCode !== "" && ckmcMemberCode !== undefined && ckmcMemberCode !== null) {
        $('#account').empty().append('退出登录');
    }
    $scope.seemyorder = function () {
        var ckmcMemberCode = getCookie('mcMemberCode');
        if (ckmcMemberCode == "" || ckmcMemberCode == undefined || ckmcMemberCode == null) {
            layermyui('请先登录');
            return;
        }
        else {
            window.location.href = '#/app/user/myorder';
        }
    }

    $scope.seemycoup = function () {
        var ckmcMemberCode = getCookie('mcMemberCode');
        if (ckmcMemberCode == "" || ckmcMemberCode == undefined || ckmcMemberCode == null) {
            layermyui('请先登录');
            return;
        }
        else {
            window.location.href = '#/app/user/mycoup';
        }
    }

    $scope.seemypoints = function () {
        var ckmcMemberCode = getCookie('mcMemberCode');
        if (ckmcMemberCode == "" || ckmcMemberCode == undefined || ckmcMemberCode == null) {
            layermyui('请先登录');
            return;
        }
        else {
            window.location.href = '#/app/user/mypoints';
        }
    }

    $scope.seemycollect = function () {
        var ckmcMemberCode = getCookie('mcMemberCode');
        if (ckmcMemberCode == "" || ckmcMemberCode == undefined || ckmcMemberCode == null) {
            layermyui('请先登录');
            return;
        }
        else {
            window.location.href = '#/app/user/mycollect';
        }
    }


    //注销
    $scope.zx = function () {

        var ckmcMemberCode = getCookie('mcMemberCode');
        if (ckmcMemberCode !== "" && ckmcMemberCode !== undefined && ckmcMemberCode !== null) {
            layer.open({
                title: '提示',
                content: '您确定要注销吗？',
                btn: ['嗯', '不要'],
                yes: function (index) {
                    setCookie('mcMemberCode', '', 1);
                    setCookie('fullName', '', 1);
                    setCookie('phonenum', '', 1);
                    $('#account').css({ "display": "none" })

                    layer.close(index);
                    layermyui('已注销!');

                    $("#myinfofullName").empty().append("点击登录");
                }
            });
        }
        else {
            window.location.href = '#/app/user/login';
        }
    }

    $scope.seemypoints = function () {
        var ckmcMemberCode = getCookie('mcMemberCode');
        if (ckmcMemberCode == "" || ckmcMemberCode == undefined || ckmcMemberCode == null) {
            layermyui('请先登录');
            return;
        }
        else {
            window.location.href = '#/app/user/mypoints';
        }
    }

}])
//短注册
.controller('shortregistCtrl', ['$scope', '$http', function ($scope, $http) {
    nofollowfunc();
    $scope.checkvalidate = function () {
        var phone = $(".checkpwd0 #phone")[0].value;
        var code = $(".checkpwd0 #code")[0].value;
        var nghttp = "../../ajax/userHandler.ashx?fn=checkvalidatecode4reg&phone=" + phone + "&code=" + code + "";
        //loading层
        var mylayeruiwait = layer.load(1, {
            shade: [0.5, '#ababab'] //0.1透明度的白色背景
        });
        $http.get(nghttp).success(function (response) {
            //debugger
            find404admin(response);
            layer.close(mylayeruiwait);
            var x2js = new X2JS();
            var xmlText = response;
            var jsonObj = x2js.xml_str2json(xmlText);
            if (jsonObj.validateCodeVerifyRespDto.rtcode == "success") {
                window.location.href = '#/app/user/quickregister2';
            }
            else {
                layermyui(jsonObj.validateCodeVerifyRespDto.errorMsg);
            }
        })

    }

}])


//快速注册控制器2
.controller('quickregister2Ctrl', ['$scope', '$http', function ($scope, $http) {
    nofollowfunc();
    $scope.regist = function () {
        //debugger
        //需要传递到后台的XML报文串:
        var passwordPlain = $('.quickregister #password')[0].value;  //明文密码
        //passwordPlain = 'xtsb1';
        var mh5pw = hex_md5(passwordPlain); //MD5密码
        var sha = hex_sha1(passwordPlain);
        var mobile = $('.quickregister #phone')[0].value; //手机

        //前端数据验证
        if ($('.quickregister #phone')[0].value === "") {
            layermyui('请输入手机号');
            return;
        }
        if ($('.quickregister #password')[0].value === "") {
            layermyui('请输入密码');
            return;
        }
        if (!checkMobile($('.quickregister #phone')[0].value)) {
            layermyui('手机号格式不正确');
            return;
        }
        if (!isPassword($('.quickregister #password')[0].value)) {
            layermyui('密码过于简单,不能少于六位数');
            return;
        }
        if ($('.quickregister #password2')[0].value === "") {
            layermyui('请输入确认密码');
            return;
        }
        if ($('.quickregister #password2')[0].value !== $('.quickregister #password')[0].value) {
            layermyui('两次密码不一致');
            return;
        }

        var xml = "<webMemberDto>";
        xml += "<email></email>";
        xml += "<phone>" + mobile + "</phone>";
        xml += "<pwd>" + mh5pw + "</pwd>";
        xml += "<sha1pwd>" + sha + "</sha1pwd>";
        xml += "<registChannel>Website</registChannel>";
        xml += "<registTag>IOS|JJTRAVEL_IOS_1|JinJiang</registTag>";
        xml += "</webMemberDto>";
        var nghttp = "../../ajax/userHandler.ashx?fn=quickregist&xml=" + xml + "";
        //alert('正在提交,请稍后...');
        //loading层
        var mylayeruiwait = layer.load(1, {
            shade: [0.5, '#ababab'] //0.1透明度的白色背景
        });
        $http.get(nghttp).success(function (response) {
            //$.unblockUI();
            find404admin(response);
            layer.close(mylayeruiwait);
            //debugger
            var x2js = new X2JS();
            var xmlText = response;
            var jsonObj = x2js.xml_str2json(xmlText);
            var mcMemberCode;
            try {
                mcMemberCode = jsonObj.webMemberRegisterReturnDto.mcMemberCode;
            }
            catch (err) {
                mcMemberCode = "";
            }

            if (mcMemberCode !== "" && mcMemberCode !== undefined) {
                var amount = getCookie('tolamount');
                var cnum = getCookie('cnum');
                var pnum = getCookie('pnum');
                var groupid = getCookie('groupid');
                var secureamount = getCookie('secureamount');

                //设置客户cookie信息.
                setCookie('mcMemberCode', jsonObj.webMemberRegisterReturnDto.mcMemberCode, 1);
                setCookie('phonenum', mobile, 1);
                //setCookie('fullName', jsonObj.memberMergeDto.fullName, 1);
                //setCookie('cdsId', jsonObj.memberMergeDto.cdsId, 1);

                layermyui('注册成功!将自动为您跳转...');
                if (groupid > 0) {
                    window.location.href = '#/app/payway/' + secureamount + '/' + groupid + '/' + pnum + '/' + cnum + '/' + amount;
                }
                else {
                    window.location.href = '#/app/index';
                }
            }
            else {
                layermyui(jsonObj.webMemberRegisterReturnDto.message);
                return;
            }
        })
    }

}])

//我的优惠券
.controller('mycoupCtrl', ['$scope', '$http', '$ionicHistory', function ($scope, $http, $ionicHistory) {
    nofollowfunc();

    var Membercode = getCookie('mcMemberCode');
    var nghttp = "../../ajax/userHandler.ashx?fn=getmembercoupinfo&Membercode=" + Membercode + "";
    var mylayeruiwait = layer.load(1, {
        shade: [0.5, '#ababab'] //0.1透明度的白色背景
    });
    $http.get(nghttp).success(function (response) {
        find404admin(response);
        layer.close(mylayeruiwait);
        var x2js = new X2JS();
        var xmlText = response;
        var jsonObj = x2js.xml_str2json(xmlText);
        //有优惠券
        try {
            for (var i = 0; i < jsonObj.queryAllCouponResultDto.results.length; i++) {
                jsonObj.queryAllCouponResultDto.results[i].startDate = FormatDateYear2(jsonObj.queryAllCouponResultDto.results[i].startDate, ".");
                jsonObj.queryAllCouponResultDto.results[i].endDate = FormatDateYear2(jsonObj.queryAllCouponResultDto.results[i].endDate, ".");
            }
            $scope.couplist = jsonObj.queryAllCouponResultDto.results;

            setTimeout(function () {
                //if (cnum == "mycoup") {
                $(".usebox").css({ "display": "none" });
                $(".timebox").css({ "display": "block" });
                $(".numbox").css({ "display": "block" });
                //}
                //else {
                //    $(".usebox").css({ "display": "block" });
                //     $(".timebox").css({ "display": "none" });
                //     $(".numbox").css({ "display": "none" });
                //   }
            }, 30)

            setTimeout(function () {
                for (var i = 0; i < $(".coupbox").length; i++) {
                    if ($(".coupbox")[i].innerText.indexOf("￥100") > -1)
                        $(".coupbox")[i].className = ('coupbox blue');
                    else if ($(".coupbox")[i].innerText.indexOf("￥50") > -1)
                        $(".coupbox")[i].className = ('coupbox pink');
                    else
                        $(".coupbox")[i].className = ('coupbox');
                }
            }, 50);
        }
        //无优惠券
        catch (err) {
            $(".nocoup").removeClass("displaynone");
        }
    })

    $scope.goBackHandler = function () {
        // $ionicHistory.goBack();   
        window.history.back();
    }

}])


//我的优惠券  下单可用
.controller('mycoupCtrl2', ['$scope', '$http', '$ionicHistory', function ($scope, $http, $ionicHistory) {
    nofollowfunc();
    var Membercode = getCookie('mcMemberCode');

    var cnum = getpbyurl(1);
    var pnum = getpbyurl(2);
    var groupid = getpbyurl(3);
    var amount = getpbyurl(4);

    var nghttp = "../../ajax/userHandler.ashx?fn=getmembercouporderinfo&Membercode=" + Membercode + "";
    var mylayeruiwait = layer.load(1, {
        shade: [0.5, '#ababab'] //0.1透明度的白色背景
    });
    $http.get(nghttp).success(function (response) {
        find404admin(response);
        layer.close(mylayeruiwait);
        var x2js = new X2JS();
        var xmlText = response;
        var jsonObj = x2js.xml_str2json(xmlText);

        try {
            if (jsonObj.resultDto.results.length) {
                for (var i = 0; i < jsonObj.resultDto.results.length; i++) {
                    //jsonObj.resultDto.results[i].startDate = FormatDateYear2(jsonObj.resultDto.results[i].startDate, ".");
                    //jsonObj.resultDto.results[i].endDate = FormatDateYear2(jsonObj.resultDto.results[i].endDate, ".");

                    jsonObj.resultDto.results[i].deduceAmount = jsonObj.resultDto.results[i].couponAmount;
                }
                $scope.couplist = jsonObj.resultDto.results;

                setTimeout(function () {
                    if (cnum == "mycoup") {
                        $(".usebox").css({ "display": "none" });
                        $(".timebox").css({ "display": "block" });
                        $(".numbox").css({ "display": "block" });
                    }
                    else {
                        $(".usebox").css({ "display": "block" });
                        $(".timebox").css({ "display": "none" });
                        $(".numbox").css({ "display": "none" });
                    }
                }, 30)

                setTimeout(function () {
                    for (var i = 0; i < $(".coupbox").length; i++) {
                        if ($(".coupbox")[i].innerText.indexOf("￥100") > -1)
                            $(".coupbox")[i].className = ('coupbox blue');
                        else if ($(".coupbox")[i].innerText.indexOf("￥50") > -1)
                            $(".coupbox")[i].className = ('coupbox pink');
                        else
                            $(".coupbox")[i].className = ('coupbox');
                    }
                }, 50);
            }
            else {
                $(".nocoup").removeClass("displaynone");
            }
        }
        catch (e) {
            $(".nocoup").removeClass("displaynone");
        }
    })

    $scope.useit = function (index) {
        var coupamount = $(".coupbox .deduceAmount")[index].innerHTML;
        var coupcode = $(".coupbox .coupcode")[index].innerHTML;
        var coupname = $(".coupbox .coupname")[index].innerHTML;
        setCookie('coupamount', coupamount, 1);
        setCookie('coupcode', coupcode, 1);
        setCookie('coupname', coupname, 1);
        //如果选择了优惠券,则显示该优惠值
        var amountnext = amount - coupamount;
        $(".pickresource .getcouptext")[0].innerText = "-" + coupamount;
        $('.pickresource .amount').empty().append(amountnext);
        $('.pickresource #nextfill').attr('href', '#/app/fillorder/' + 0 + '/' + groupid + '/' + pnum + '/' + cnum + '/' + amountnext);

        $(".pickresource .coupamount")[0].innerText = "-¥" + coupamount;

        $(".pickresource #sp1")[0].children[0].children[2].disabled = true;
        window.location.href = "#/app/pickresource/" + 0 + "/" + groupid + "/" + pnum + "/" + cnum + "";
    }

    $scope.goBackHandler = function () {
        // $ionicHistory.goBack();
        window.history.back();
    }

}])



//我的积分
.controller('mypointsCtrl', ['$scope', '$http', function ($scope, $http) {
    nofollowfunc();
    var Membercode = getCookie('mcMemberCode');
    var nghttp = "../../ajax/userHandler.ashx?fn=getmemberscoreinfo&Membercode=" + Membercode + "";
    var mylayeruiwait = layer.load(1, {
        shade: [0.5, '#ababab'] //0.1透明度的白色背景
    });
    $http.get(nghttp).success(function (response) {
        find404admin(response);
        layer.close(mylayeruiwait);
        var x2js = new X2JS();
        var xmlText = response;
        var jsonObj = x2js.xml_str2json(xmlText);
        $scope.memberinfo = jsonObj.memberScoreLevelInfoDto;
    })

    $scope.seehelp = function () {
        layer.alert('We R family!', { icon: 6 });
    }

}])



//我的收藏
.controller('mycollectCtrl', ['$scope', '$http', function ($scope, $http) {
    nofollowfunc();
    var Membercode = getCookie('mcMemberCode');
    var nghttp = "../../ajax/apihandler.ashx?fn=getcollectlist&Membercode=" + Membercode + "";
    var mylayeruiwait = layer.load(1, {
        shade: [0.5, '#ababab'] //0.1透明度的白色背景
    });
    $http.get(nghttp).success(function (response) {
        layer.close(mylayeruiwait);
        $scope.collectlist = response.ds;
    }).error(function (data, status, headers, config) {
        layermyui('暂无收藏');
        layer.close(mylayeruiwait);
    });


    $scope.listhistorygoback = function () {
        window.history.back();
    }

}])


//发送短信验证码2
var sends2 = {
    checked: 1,
    send: function () {
        var numbers = /^1\d{10}$/;
        var val = $('.forgetpwd0 #phone').val().replace(/\s+/g, ""); //获取输入手机号码
        if ($('.div-phone').find('span').length == 0 && $('.div-phone a').attr('class') == 'send1 activated') {
            if (!numbers.test(val) || val.length == 0) {
                $('.div-phone').append('<span class="error">手机格式错误</span>');
                return false;
            }
        }
        if (numbers.test(val)) {
            var time = 30;
            $('.div-phone span').remove();
            function timeCountDown() {
                if (time == 0) {
                    clearInterval(timer);
                    $('.div-phone a').addClass('send1').removeClass('send0').html("发送验证码");
                    sends.checked = 1;
                    return true;
                }
                $('.div-phone a').html(time + "秒后再次发送");
                time--;
                return false;
                sends.checked = 0;
            }
            $('.div-phone a').addClass('send0').removeClass('send1');
            timeCountDown();
            var timer = setInterval(timeCountDown, 1000);
            //传入后台操作
            $.ajax({
                url: "../../ajax/userHandler.ashx?fn=sendvalidatecode4reg&phone=" + $('#phone').val(),
                type: 'post',
                success: function (response) {
                    //debugger
                    //验证码已发送
                    //debugger
                    //var x2js = new X2JS();
                    //var xmlText = response;
                    //var jsonObj = x2js.xml_str2json(xmlText);
                    // if(jsonObj.rtcode=="success")
                }
            });
        }
    }
}

//发送短信验证码
var sends = {
    checked: 1,
    send: function () {
        var numbers = /^1\d{10}$/;
        var val = $('.checkpwd0 #phone').val().replace(/\s+/g, ""); //获取输入手机号码
        if ($('.div-phone').find('span').length == 0 && $('.div-phone a').attr('class') == 'send1 activated') {
            if (!numbers.test(val) || val.length == 0) {
                $('.div-phone').append('<span class="error">手机格式错误</span>');
                return false;
            }
        }
        if (numbers.test(val)) {
            var time = 30;
            $('.div-phone span').remove();
            function timeCountDown() {
                if (time == 0) {
                    clearInterval(timer);
                    $('.div-phone a').addClass('send1').removeClass('send0').html("发送验证码");
                    sends.checked = 1;
                    return true;
                }
                $('.div-phone a').html(time + "秒后再次发送");
                time--;
                return false;
                sends.checked = 0;
            }
            $('.div-phone a').addClass('send0').removeClass('send1');
            timeCountDown();
            var timer = setInterval(timeCountDown, 1000);
            //传入后台操作
            $.ajax({
                url: "../../ajax/userHandler.ashx?fn=sendvalidatecode4reg&phone=" + $('#phone').val(),
                type: 'post',
                success: function (response) {
                    //debugger
                    //验证码已发送
                    //debugger
                    //var x2js = new X2JS();
                    //var xmlText = response;
                    //var jsonObj = x2js.xml_str2json(xmlText);
                    // if(jsonObj.rtcode=="success")
                }
            });
        }
    }
}


//以下公用方法--------------------------------------------------
function myfocus(ob) {
    if ($(ob)[0].value == "") {
        $(ob)[0].previousElementSibling.className = 'item-tip item-tip-focus';
        $(ob)[0].className = ('form-input form-input-focus');
    }
}
function myblur(ob) {
    if ($(ob)[0].value == "") {
        $(ob)[0].previousElementSibling.className = 'item-tip';
        $(ob)[0].className = ('form-input');
    }
}

function myfocust(ob) {
    ob = ob.nextElementSibling;
    if ($(ob)[0].value == "") {
        $(ob)[0].previousElementSibling.className = 'item-tip item-tip-focus';
        $(ob)[0].className = ('form-input form-input-focus');
    }
}

function myblurt(ob) {
    ob = ob.nextElementSibling;
    if ($(ob)[0].value == "") {
        $(ob)[0].previousElementSibling.className = 'item-tip';
        $(ob)[0].className = ('form-input');

    }
}

function settypepng() {
    for (var i = 0; i < $(".seebutton .cancel").length ; i++) {
        if ($(".seebutton .cancel")[i].parentNode.previousElementSibling.previousElementSibling.innerText.indexOf("已取消") > -1) {
            $(".seebutton .cancel")[i].className = "seebutton cancel displaynone";
        }
    }
}
