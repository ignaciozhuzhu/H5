angular.module('starter.controllersuser', [])

.controller('AppCtrl2', function ($scope, $ionicModal, $timeout) {

    // With the new view caching in Ionic, Controllers are only called
    // when they are recreated or on app start, instead of every page change.
    // To listen for when this page is active (for example, to refresh data),
    // listen for the $ionicView.enter event:
    //$scope.$on('$ionicView.enter', function(e) {
    //});

    // Form data for the login modal
    $scope.loginData = {};

    // Create the login modal that we will use later
    //$ionicModal.fromTemplateUrl('templates/login.html', {
    //    scope: $scope
    //}).then(function (modal) {
    //    $scope.modal = modal;
    //});

    // Triggered in the login modal to close it
    $scope.closeLogin = function () {
        $scope.modal.hide();
    };

    // Open the login modal
    $scope.login = function () {
        $scope.modal.show();
    };

    // Perform the login action when the user submits the login form
    $scope.doLogin = function () {
        console.log('Doing login', $scope.loginData);

        // Simulate a login delay. Remove this and replace with your login
        // code if using a login system
        $timeout(function () {
            $scope.closeLogin();
        });
    };
})


//注册控制器
.controller('registerCtrl', function ($scope, $http) {
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
                var amount = getCookie('amount');
                var cnum = getCookie('cnum');
                var pnum = getCookie('pnum');
                var groupid = getCookie('groupid');
                var secureamount = getCookie('secureamount');

                //设置客户cookie信息.
                setCookie('mcMemberCode', jsonObj.crmResponseDto.mcMemberCode, 1);
                //setCookie('fullName', jsonObj.memberMergeDto.fullName, 1);
                //setCookie('cdsId', jsonObj.memberMergeDto.cdsId, 1);

                if (groupid > 0) {
                    layermyui('注册成功!将自动为您跳转回支付页面...');
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

})
//登录控制器
.controller('loginCtrl', function ($scope, $http) {


    $scope.login = function () {

        //需要传递到后台的XML报文串:
        var loginname = $('.login #loginname')[0].value;  //用户名
        var passwordPlain = $('.login #password')[0].value;  //明文密码
        //passwordPlain = 'xtbssb3';
        var mh5pw = hex_md5(passwordPlain); //MD5密码
        var sha = hex_sha1(passwordPlain);  //sha1密码
        //懒得写入作为测试用例填入后台.
        //loginname = '18505793685';

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
                var amount = getCookie('amount');
                var cnum = getCookie('cnum');
                var pnum = getCookie('pnum');
                var groupid = getCookie('groupid');
                var secureamount = getCookie('secureamount');

                //设置客户cookie信息.
                setCookie('mcMemberCode', jsonObj.memberMergeDto.mcMemberCode, 1);
                setCookie('fullName', jsonObj.memberMergeDto.fullName, 1);
                //setCookie('cdsId', jsonObj.memberMergeDto.cdsId, 1);
                layermyui('登录成功!');
                var ckmcMemberCode = getCookie('mcMemberCode');
                var linkbackpay = getCookie('linkbackpay');
                if (ckmcMemberCode !== "" && ckmcMemberCode !== undefined && ckmcMemberCode !== null) {
                    $('#account').empty().append('退出当前账户');
                    var fullName = getCookie('fullName');
                    $('#myinfofullName').empty().append(fullName);
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

})

//快速注册控制器
.controller('quickregisterCtrl', function ($scope, $http) {

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
                var amount = getCookie('amount');
                var cnum = getCookie('cnum');
                var pnum = getCookie('pnum');
                var groupid = getCookie('groupid');
                var secureamount = getCookie('secureamount');

                //设置客户cookie信息.
                setCookie('mcMemberCode', jsonObj.webMemberRegisterReturnDto.mcMemberCode, 1);
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

})
//密码找回控制器
.controller('forgetpwdCtrl', function ($scope, $http) {

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

})
//我的订单控制器
.controller('myorderCtrl', function ($scope, $http) {
    //var mcMemberCode = "";
    var mcMemberCode = getCookie('mcMemberCode');
    var orderStatus = "";
    var payStatus = "";

    //debugger
    //40
    var a = $(".header0")[0].offsetTop;
    if (a < 40) {
        $(".header0").css({ "margin-top": "35px" });
    }
    else {
        $(".header0").css({ "margin-top": "0px" });
    }
    removeclassyellow();
    addclassyellow(0);
    var nghttp = "../../ajax/userHandler.ashx?fn=queryorder&mcMemberCode=" + mcMemberCode + "&orderStatus=" + orderStatus + "&payStatus=" + payStatus + "";
    getorders(nghttp);

    function getorders(mynghttp) {
        $http.get(mynghttp).success(function (response) {
            //debugger
            find404admin(response);
            $scope.orders = response.orders;
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
        if (event.currentTarget.parentNode.previousElementSibling.previousElementSibling.innerText.indexOf("待支付") > -1) {
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
        else {
            layermyui('您的订单为已支付订单，需确认退款金额，请及时与客服联系，客服电话10101666*3，客服工作时间：8点至21·点。');
        }
    }

    $scope.li0 = function () {
        removeclassyellow();
        addclassyellow(0);
        var mynghttp = "../../ajax/userHandler.ashx?fn=queryorder&mcMemberCode=" + mcMemberCode + "&orderStatus=" + orderStatus + "&payStatus=" + payStatus + "";
        getorders(mynghttp);
    }
    $scope.li1 = function () {
        removeclassyellow();
        addclassyellow(1);
        var payStatus = "PAY_WAITING";
        var mynghttp = "../../ajax/userHandler.ashx?fn=queryorder&mcMemberCode=" + mcMemberCode + "&orderStatus=" + orderStatus + "&payStatus=" + payStatus + "";
        getorders(mynghttp);
    }
    $scope.li2 = function () {
        removeclassyellow();
        addclassyellow(2);
        var payStatus = "PAYED";
        var mynghttp = "../../ajax/userHandler.ashx?fn=queryorder&mcMemberCode=" + mcMemberCode + "&orderStatus=" + orderStatus + "&payStatus=" + payStatus + "";
        getorders(mynghttp);
    }
    $scope.li3 = function () {
        removeclassyellow();
        addclassyellow(3);
        orderStatus = "CANCELED";
        var mynghttp = "../../ajax/userHandler.ashx?fn=queryorder&mcMemberCode=" + mcMemberCode + "&orderStatus=" + orderStatus + "&payStatus=" + payStatus + "";
        getorders(mynghttp);
    }
    $scope.li4 = function () {
        removeclassyellow();
        addclassyellow(4);
        orderStatus = "CONFIRMED";
        var mynghttp = "../../ajax/userHandler.ashx?fn=queryorder&mcMemberCode=" + mcMemberCode + "&orderStatus=" + orderStatus + "&payStatus=" + payStatus + "";
        getorders(mynghttp);

    }

})
//订单详情控制器
.controller('orderdetailCtrl', function ($scope, $http) {

    //暂时作为测试用
    // var orderCode = 1000160531000003;
    var orderCode = getpbyurl(1);
    var nghttp = "../../ajax/userHandler.ashx?fn=queryorderdetail&code=" + orderCode;

    $http.get(nghttp).success(function (response) {

        find404admin(response);
        $scope.status = response.payStatus == "PAYED" ? "已支付" : "待支付";

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

    })

    var accountName = 'INNS_APP_CLIENT_ALI_WAP_PAY';
    $scope.pay = function () {
        //首先做身份认证,判断是否已经登录,没有帐号的客户先注册.
        var mcMemberCode = getCookie('mcMemberCode');
        if (mcMemberCode != "" && mcMemberCode != undefined && mcMemberCode != null) {
            //其次再是发起付款
            var orderNo = $scope.orderCode;
            var amount = $scope.paymentAmount;
            var nghttp = "../../ajax/apihandler.ashx?fn=pbppayorder&orderNo=" + orderNo + "&payAmount=" + amount + "&accountName=" + accountName + "";
            //alert('正在加载,请稍后...');
            //loading层
            var mylayeruiwait = layer.load(1, {
                shade: [0.5, '#ababab'] //0.1透明度的白色背景
            });
            $http.get(nghttp).success(function (response) {
                //$.unblockUI();
                find404admin(response);
                layer.close(mylayeruiwait);
                window.location.href = response;
            })
        }
        else {//把参数存入cookie
            setCookie('amount', amount, 1);
            setCookie('cnum', cnum, 1);
            setCookie('pnum', pnum, 1);
            setCookie('groupid', groupid, 1);
            setCookie('secureamount', secureamount, 1);
            //跳转至登录页
            window.location.href = '#/app/user/login';
        }

    }

})
//个人中心控制器
.controller('myinfoCtrl', function ($scope, $http) {

    //判断是否已经登录帐号,获取membercode 的cookie
    var ckmcMemberCode = getCookie('mcMemberCode');
    $scope.fullName = getCookie('fullName');
    if (ckmcMemberCode !== "" && ckmcMemberCode !== undefined && ckmcMemberCode !== null) {
        $('#account').empty().append('注销');
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
                    $('#account').empty().append('登录');
                    $scope.fullName = '';
                    layer.close(index);
                    layermyui('已注销!');
                }
            });
        }
        else {
            window.location.href = '#/app/user/login';
        }
    }

})
//短注册
.controller('shortregistCtrl', function ($scope, $http) {
    //判断是否已经登录帐号,获取membercode 的cookie
    //var ckmcMemberCode = getCookie('mcMemberCode');
    //$scope.fullName = getCookie('fullName');
    //if (ckmcMemberCode !== "" && ckmcMemberCode !== undefined && ckmcMemberCode !== null) {
    //    $('#account').empty().append('注销');
    //}
    //$scope.seemyorder = function () {
    //    var ckmcMemberCode = getCookie('mcMemberCode');
    //    if (ckmcMemberCode == "" || ckmcMemberCode == undefined || ckmcMemberCode == null) {
    //        layermyui('请先登录');
    //        return;
    //    }
    //    else {
    //        window.location.href = '#/app/user/myorder';
    //    }
    //}

})


//快速注册控制器2
.controller('quickregister2Ctrl', function ($scope, $http) {
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
                var amount = getCookie('amount');
                var cnum = getCookie('cnum');
                var pnum = getCookie('pnum');
                var groupid = getCookie('groupid');
                var secureamount = getCookie('secureamount');

                //设置客户cookie信息.
                setCookie('mcMemberCode', jsonObj.webMemberRegisterReturnDto.mcMemberCode, 1);
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

})


//发送短信验证码
var sends = {
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
            //debugger
            //传入后台操作
            $.ajax({
                url: "../../ajax/userHandler.ashx?fn=sendvalidatecode4reg&phone=" + $('#phone').val(),
                type: 'post',
                success: function (response) {
                    debugger
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
//function login_reg_input(b) {
//    SetInputCss(b);
//    $(b).mouseup(function () { SetInputCss(b); });
//    $(b).blur(function () {
//        if ($.trim($(this).val()) == "") {
//            $(this).removeClass("form-input-focus");
//            $(this).prev().removeClass("item-tip-focus");
//        }
//    });
//    $(b).focus(function () {
//        if (!$(this).hasClass("form-input-focus")) { $(this).addClass("form-input-focus"); $(this).prev().addClass("item-tip-focus"); }
//    });
//    $(".item-tip").click(function () {
//        $(this).next().focus();
//    });
//}
//function SetInputCss(b) {
//    debugger
//    $(b).each(function () {
//        if ($.trim($(this).val()) != "") {
//            $(this).addClass("form-input-focus");
//            $(this).prev().addClass("item-tip-focus");
//        }
//    });
//}
//login_reg_input(".form-input");

function myblur(ob) {
    if ($(ob)[0].value == "") {
        $(ob)[0].previousElementSibling.className = 'item-tip';
        $(ob)[0].className = ('form-input');
    }
}

function myfocust(ob) {
    // debugger
    ob = ob.nextElementSibling;
    if ($(ob)[0].value == "") {
        $(ob)[0].previousElementSibling.className = 'item-tip item-tip-focus';
        $(ob)[0].className = ('form-input form-input-focus');

    }
    //$(ob)[0].focus();
    // document.getElementById("loginname").focus();
}

function myblurt(ob) {
    ob = ob.nextElementSibling;
    if ($(ob)[0].value == "") {
        $(ob)[0].previousElementSibling.className = 'item-tip';
        $(ob)[0].className = ('form-input');

    }
    //  document.getElementById("loginname").focus();
}

