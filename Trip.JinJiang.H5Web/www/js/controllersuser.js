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
        }, 1000);
    };
})


//线路列表控制器
.controller('LinelistsCtrl', function ($scope, $http) {
    var searchParam = request("search");
    var nghttp = "../../ajax/apihandler.ashx?fn=getlines";
    $http.get(nghttp).success(function (response) {
        // debugger
        var arrayLinemm = new Array(0);
        for (var i = 0; i < response.lines.length; i++) {
            if (response.lines[i].imageUrls[0] === undefined || response.lines[i].imageUrls[0] === null || response.lines[i].imageUrls[0].indexOf('http') < 0)
                response.lines[i].imageUrls[0] = 'http://img5.imgtn.bdimg.com/it/u=45254662,160915219&fm=21&gp=0.jpg'
            //往搜索结果中添加合集(1)
            if (response.lines[i].lineName.indexOf(searchParam) > -1)
                arrayLinemm.push(response.lines[i])
        }
        //往搜索结果中添加合集(2)
        $scope.linelists = arrayLinemm;
        $scope.agencies = response.agencies;

    });
})

//注册控制器
.controller('registerCtrl', function ($scope, $http) {

    //   var ordercode = "1000160512000007";
    //  var nghttp = "../../ajax/apihandler.ashx?fn=cancelorder&ordercode=" + ordercode + "";
    //$http.get(nghttp).success(function (response) {
    //    // debugger

    //});

    $scope.regist = function () {
        debugger
        //需要传递到后台的XML报文串:
        var passwordPlain = $('#password')[0].value;  //明文密码
        passwordPlain = 'xtsb1';
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

        certificateType = 'ID';
        certificateNo = '332522115';
        email = '11223@qq.com'; //<mcMemberCode>10059061</mcMemberCode>
        mobile = '15111';
        title = 'Mr.';
        surname = '赵云';

        var json = "<memberRegisterDto><memberInfoDto><memberType>Silver Card</memberType><certificateNo>" + certificateNo + "</certificateNo><certificateType>" + certificateType + "</certificateType><email>" + email + "</email><mobile>" + mobile + "</mobile><scoreType>1</scoreType><title>" + title + "</title><surname>" + surname + "</surname><memberScoreType>SCORE</memberScoreType><registerSource>Website</registerSource><passsword>String</passsword><sha1pwd>String</sha1pwd>";
        var nghttp = "../../ajax/userHandler.ashx?fn=regist&json=" + json + "";
          $http.get(nghttp).success(function (response) {
              debugger
          })
    }
   

    $scope.$on("$ionicView.loaded", function () {
    })

})
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