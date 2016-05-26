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
    var ipl;
    var ip4;
    var ip6;
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

        //懒得写入作为测试用例填入后台.
        certificateType = 'ID';
        certificateNo = '332522115';
        email = '11223@qq.com'; //<mcMemberCode>10059061</mcMemberCode>
        mobile = '15111';
        title = 'Mr.';
        surname = '赵云';

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
           // debugger
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

