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
        var passwordPlain = $('#password')[0].value;  //明文密码
        //passwordPlain = 'xtsb1';
        var mh5pw = hex_md5(passwordPlain); //MD5密码
        var sha = hex_sha1(passwordPlain);
        var certificateType = $('#certificateType')[0].value;    //证件类型
        var certificateNo = $('#certificateNo')[0].value;  //证件号
        var email = $('#email')[0].value;  //邮箱
        var mobile = $('#phone')[0].value; //手机
        var title = $("input[name='Sex'][checked]").val();;  //称谓 
        var surname = $('#surname')[0].value;    //姓名

        var obj = document.getElementsByName("Sex")
        for (var i = 0; i < obj.length; i++) { //遍历Radio 
            if (obj[i].checked) {
                title = obj[i].value;
            }
        }

        //前端数据验证
        if ($('#phone')[0].value === "") {
            blockmyui('请输入手机号');
            return;
        }
        if ($('#email')[0].value === "") {
            blockmyui('请输入邮箱');
            return;
        }
        if (!isEmail($('#email')[0].value)) {
            blockmyui('邮箱格式不正确');
            return;
        }
        if ($('#password')[0].value === "") {
            blockmyui('请输入密码');
            return;
        }
        if (!isPassword($('#password')[0].value)) {
            blockmyui('密码过于简单,不能少于六位数');
            return;
        }
        if ($('#password2')[0].value === "") {
            blockmyui('请输入确认密码');
            return;
        }
        if ($('#password2')[0].value !== $('#password')[0].value) {
            blockmyui('两次密码不一致');
            return;
        }
        if (surname === "") {
            blockmyui('请输入姓名');
            return;
        }
        if (certificateNo === "") {
            blockmyui('请输入证件号');
            return;
        }
        if (!isIdCardNo(certificateNo)) {
            blockmyui('证件号不对');
            return;
        }
        if (title == "" || title == undefined || title == null) {
            blockmyui('请选择称谓');
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
        $http.get(nghttp).success(function (response) {
            //debugger
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

                alert('注册成功!将自动为您跳转回支付页面...');
                window.location.href = '#/app/payway/' + secureamount + '/' + groupid + '/' + pnum + '/' + cnum + '/' + amount;
            }
            else
                alert(jsonObj.crmResponseDto.retmsg);
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
//注册控制器
.controller('loginCtrl', function ($scope, $http) {


    $scope.login = function () {

        //需要传递到后台的XML报文串:
        var loginname = $('#loginname')[0].value;  //用户名
        var passwordPlain = $('#password')[0].value;  //明文密码
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
        $http.get(nghttp).success(function (response) {
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
                //setCookie('fullName', jsonObj.memberMergeDto.fullName, 1);
                //setCookie('cdsId', jsonObj.memberMergeDto.cdsId, 1);
                alert('登录成功!');
                window.location.href = '#/app/payway/' + secureamount + '/' + groupid + '/' + pnum + '/' + cnum + '/' + amount;
            }
            else {
                alert(jsonObj.memberMergeDto.remark);
            }
        })
    }

    $scope.$on("$ionicView.loaded", function () {

    })

})

//快速注册控制器
.controller('quickregisterCtrl', function ($scope, $http) {

    $scope.regist = function () {

        //debugger
        //需要传递到后台的XML报文串:
        var passwordPlain = $('#password')[0].value;  //明文密码
        //passwordPlain = 'xtsb1';
        var mh5pw = hex_md5(passwordPlain); //MD5密码
        var sha = hex_sha1(passwordPlain);
        var email = $('#email')[0].value;  //邮箱
        var mobile = $('#phone')[0].value; //手机

        //前端数据验证
        if ($('#phone')[0].value === "") {
            blockmyui('请输入手机号');
            return;
        }
        if ($('#email')[0].value === "") {
            blockmyui('请输入邮箱');
            return;
        }
        if (!isEmail($('#email')[0].value)) {
            blockmyui('邮箱格式不正确');
            return;
        }
        if ($('#password')[0].value === "") {
            blockmyui('请输入密码');
            return;
        }
        if (!isPassword($('#password')[0].value)) {
            blockmyui('密码过于简单,不能少于六位数');
            return;
        }
        if ($('#password2')[0].value === "") {
            blockmyui('请输入确认密码');
            return;
        }
        if ($('#password2')[0].value !== $('#password')[0].value) {
            blockmyui('两次密码不一致');
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
        $http.get(nghttp).success(function (response) {
            debugger
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

                alert('注册成功!将自动为您跳转回支付页面...');
                window.location.href = '#/app/payway/' + secureamount + '/' + groupid + '/' + pnum + '/' + cnum + '/' + amount;
            }
            else {
                alert(jsonObj.webMemberRegisterReturnDto.message);
                return;
            }
        })
    }

})

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

