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

//取消订单控制器
.controller('registerCtrl', function ($scope, $http) {

    //   var ordercode = "1000160512000007";
    //  var nghttp = "../../ajax/apihandler.ashx?fn=cancelorder&ordercode=" + ordercode + "";
    //$http.get(nghttp).success(function (response) {
    //    // debugger

    //});
})
function myfocus() {
    //if ($('#password')[0].value == "") {
    //    //  alert()
    //    $('.item-tip').removeClass('item-tip-focus');
    //    $('.form-input').removeClass('form-input-focus');
    //}
    //else{
    //    $('.item-tip').addClass('item-tip-focus');
    //    $('.form-input').addClass('form-input-focus');

    //}

    if ($('#password')[0].value == "") {
        $('.item-tip').addClass('item-tip-focus');
        $('.form-input').addClass('form-input-focus');
    }

}


function myblur() {
    if ($('#password')[0].value == "") {
        $('.item-tip').removeClass('item-tip-focus');
        $('.form-input').removeClass('form-input-focus');
    }
    //else {
    //    $('.item-tip').addClass('item-tip-focus');
    //    $('.form-input').addClass('form-input-focus');

    //}

}