﻿angular.module('starter.controllers', [])

.controller('AppCtrl', function ($scope, $ionicModal, $timeout) {

    // With the new view caching in Ionic, Controllers are only called
    // when they are recreated or on app start, instead of every page change.
    // To listen for when this page is active (for example, to refresh data),
    // listen for the $ionicView.enter event:
    //$scope.$on('$ionicView.enter', function(e) {
    //});

    // Form data for the login modal
    $scope.loginData = {};

    // Create the login modal that we will use later
    $ionicModal.fromTemplateUrl('templates/login.html', {
        scope: $scope
    }).then(function (modal) {
        $scope.modal = modal;
    });

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

.controller('PlaylistsCtrl', function ($scope) {
    $scope.playlists = [
      { title: 'Reggae', id: 1 },
      { title: 'Chill', id: 2 },
      { title: 'Dubstep', id: 3 },
      { title: 'Indie', id: 4 },
      { title: 'Rap', id: 5 },
      { title: 'Cowbell', id: 6 }
    ];
})

//线路列表控制器
.controller('LinelistsCtrl', function ($scope, $http) {
    var searchParam = request("search");
    var nghttp = "../../ajax/apihandler.ashx?fn=getlines";
    $http.get(nghttp).success(function (response) {
        var arrayLinemm = new Array(0);
        for (var i = 0; i < response.lines.length; i++) {
            if (response.lines[i].imageUrls[0] === undefined || response.lines[i].imageUrls[0].indexOf('http') < 0)
                response.lines[i].imageUrls[0] = 'http://img5.imgtn.bdimg.com/it/u=244697929,1321231894&fm=21&gp=0.jpg'
            //往搜索结果中添加合集(1)
            if (response.lines[i].lineName.indexOf(searchParam) > -1)
                arrayLinemm.push(response.lines[i])
        }
        //往搜索结果中添加合集(2)
        $scope.linelists = arrayLinemm;
        $scope.agencies = response.agencies;

    });
})
 //线路列表控制器2,唯独不一样的是路由
.controller('LinelistsCtrl2', function ($scope, $http) {
    var url = location.href;
    var lineCategory = url.substring(url.lastIndexOf('/') + 1, url.length);
    var searchParam = lineCategory;
    var nghttp = "../../ajax/apihandler.ashx?fn=getlinesbycategory&lineCategory=" + lineCategory + "";
    $http.get(nghttp).success(function (response) {

        $.ajax({
            url: "../../ajax/apihandler.ashx",
            data: { fn: "getlinecategoriecrm", category: lineCategory },
            type: "post",
            success: function (text) {
                var d = eval("(" + text + ")");

                for (var i = 0 ; i < d.rows.length; i++) {
                    for (var j = 0 ; j < response.lines.length; j++) {
                        if (d.rows[i].lineId == response.lines[j].lineId) {
                            response.lines[j].lineCategory = d.rows[i].lineCategory;
                        }
                    }
                }

                var arrayLinemm = new Array(0);
                for (var i = 0; i < response.lines.length; i++) {
                    if (response.lines[i].imageUrls[0] === undefined || response.lines[i].imageUrls[0].indexOf('http') < 0)
                        response.lines[i].imageUrls[0] = 'http://img5.imgtn.bdimg.com/it/u=244697929,1321231894&fm=21&gp=0.jpg'
                    //往搜索结果中添加合集(1)
                    if (response.lines[i].lineCategory == searchParam)
                        arrayLinemm.push(response.lines[i])
                }
                //往搜索结果中添加合集(2)
                $scope.linelists = arrayLinemm;
                $scope.agencies = response.agencies;
            }
        })

    });
})


//主页控制器
.controller('indexCtrl', function ($scope, $http) {
    var nghttp = "../../ajax/apihandler.ashx?fn=getlinespromotion";
    $http.get(nghttp).success(function (response) {
        for (var i = 0; i < 8; i++) {
            if (response.lines[i].imageUrls[0] === undefined || response.lines[i].imageUrls[0].indexOf('http') < 0)
                response.lines[i].imageUrls[0] = 'http://img5.imgtn.bdimg.com/it/u=244697929,1321231894&fm=21&gp=0.jpg'
        }
        var arrayLineP = new Array(0);
        arrayLineP.push(response.lines[0]);
        arrayLineP.push(response.lines[1]);
        arrayLineP.push(response.lines[2]);
        arrayLineP.push(response.lines[3]);

        var arrayLineN = new Array(0);
        arrayLineN.push(response.lines[4]);
        arrayLineN.push(response.lines[5]);
        arrayLineN.push(response.lines[6]);
        arrayLineN.push(response.lines[7]);

        $scope.linelistsP = arrayLineP;
        $scope.linelistsN = arrayLineN;
        $scope.agencies = response.agencies;

    });

    //自加载运行
    $scope.$on("$ionicView.loaded", function () {
        //自动加载播放滚动图片
        $('#full-width-slider').royalSlider({
            arrowsNav: true,
            loop: false,
            keyboardNavEnabled: true,
            controlsInside: false,
            imageScaleMode: 'fill',
            arrowsNavAutoHide: false,
            autoScaleSlider: true,
            autoScaleSliderWidth: 960,
            autoScaleSliderHeight: 350,
            controlNavigation: 'bullets',
            thumbsFitInViewport: false,
            navigateByClick: true,
            startSlideId: 0,
            autoPlay: false,
            transitionType: 'move',
            globalCaption: true,
            deeplinking: {
                enabled: true,
                change: false
            },

            imgWidth: 1400,
            imgHeight: 680
        });
        getPro();


    });

})

//线路详情控制器
.controller('lineDetailCtrl', function ($scope, $http, $sce) {
    var url = location.href;
    var lineid = url.substring(url.lastIndexOf('/') + 1, url.length);
    var nghttp = "../../ajax/apihandler.ashx?fn=getlinedetail&lineid=" + lineid + "";
    $http.get(nghttp).success(function (response) {
        $scope.linedetails = response.line;
        //行程
        $scope.journeys = response.line.journeys.sort(sortbydayNumber);
        //行程明细
        //$sce 是 angularJS 自带的安全处理模块，$sce.trustAsHtml(str) 方法便是将数据内容以 html 的形式进行解析并返回。将此过滤器添加到 ng-bind-html 、data-ng-bind-html  所绑定的数据中，便实现了在数据加载时对于 html 标签的自动转义。
        $scope.featureContent = $sce.trustAsHtml(response.line.lineFeature.replace(/\n/g, "<br>"));
        $scope.priceIncludeContent = $sce.trustAsHtml(response.line.priceInclude.replace(/\n/g, "<br>"));
        $scope.priceExclusiveContent = $sce.trustAsHtml(response.line.priceExclusive.replace(/\n/g, "<br>"));

        //取团日期
        response.line.groups = response.line.groups.sort(sortbydepartDate);
        for (var i = 0; i < response.line.groups.length; i++) {
            response.line.groups[i].departDate = FormatDate(response.line.groups[i].departDate);
        }

        //小于4天先取出4天
        var days4 = new Array(0);
        var daysmore = new Array(0);
        var smalllen;
        if (response.line.groups.length <= 4)
            smalllen = response.line.groups.length;
        else
            smalllen = 4;

        for (var i = 0; i < smalllen; i++) {
            days4.push(response.line.groups[i]);
        }
        $scope.group1 = days4;
        //大于4的话就取出剩余的
        if (response.line.groups.length > 4) {
            for (var i = 4; i < response.line.groups.length; i++) {
                daysmore.push(response.line.groups[i]);
            }
        }
        $scope.group2 = daysmore;

        //团框初始高度
        $("#groupsheight").height(16);
        $("#groupsinheight").height(8);
        //团日列表数量
        dayslength = response.line.groups.length;

        //取团号
        $scope.groupcode = response.line.groups[0].groupCode.substring(response.line.groups[0].groupCode.length - 14, response.line.groups[0].groupCode.length);
        //取价格
        $scope.price = response.minPrice;

    });
    $scope.$on("$ionicView.loaded", function () {
        //自动加载播放滚动图片
        $('#idfeature').show();
        $('#idline').hide();
        $('#idexpense').hide();

        $('.thirdCenter:eq(0)').addClass("contentblue");
        $('.thirdCenter:eq(3)').addClass("lineblue");
    });

})


.controller('PlaylistCtrl', function ($scope, $stateParams) {
});

//***************************以下公用方法***************************

function getlinecategoriecrm() {

}

var dayslength;
var counting = 0;
//更多团日期
function moredays() {
    counting++;
    if (counting % 2 == 1) {
        var moha = dayslength / 6;
        $("#groupsheight").height(40 + 18 * moha);
        $("#groupsinheight").height(30 + 18 * moha);
        $(".daysgroup2").slideToggle();

    }
    else {
        $("#groupsheight").height(16);
        $("#groupsinheight").height(8);
        $(".daysgroup2").slideToggle();
    }
}
//城市选择框
function citySelect() {
    $('#divcontent').hide();
    $('#bartitle').hide();
    $('#divcityselect').show();

    $('#divcityselect').click(function (event) {
        if (event.target.nodeName === 'SPAN') {
            $('#divcontent').show();
            $('#bartitle').show();
            $('#divcityselect').hide();
            $("#beginProtxt")[0].placeholder = event.target.innerText + '出发';
        }
    })
}
//获取URL的参数
function request(paras) {
    var url = location.href;
    var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
    var paraObj = {}
    for (i = 0, j = decodeURI(paraString[0]) ; i < 1; i++) {
        paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
    }
    var returnValue = paraObj[paras.toLowerCase()];
    if (typeof (returnValue) == "undefined") {
        return "";
    } else {
        return returnValue;
    }
}

//线路查询传参,前台点击事件
function searchLines() {
    var searchParam = $("#searchtxt")[0].value;
    window.location.href = '#/app/linelists?search=' + searchParam;
}

//获取当前地
function getPro() {
    var url = 'http://chaxun.1616.net/s.php?type=ip&output=json&callback=?&_=' + Math.random();
    $.getJSON(url, function (data) {
        var pro = data.Isp;
        pro = pro.substring(0, 2)
        if (pro.substring(0, 2) === "黑龙")
            pro = "黑龙江"
        if (pro.substring(0, 2) === "内蒙")
            pro = "内蒙古"
        $("#beginProtxt")[0].placeholder = pro + '出发';
        $("#nowPro").append(pro);
    });

}
function removeclassblue() {
    $('.thirdCenter').removeClass("contentblue");
    $('.thirdCenter').removeClass("lineblue");
}
function addclassblue(q, i) {
    $('.thirdCenter:eq(' + q + ')').addClass("contentblue");
    $('.thirdCenter:eq(' + i + ')').addClass("lineblue");
}

function featureCl() {
    $('#idfeature').show();
    $('#idline').hide();
    $('#idexpense').hide();
    removeclassblue();
    addclassblue(0, 3);
}

function lineCl() {
    $('#idfeature').hide();
    $('#idline').show();
    $('#idexpense').hide();
    removeclassblue();
    addclassblue(1, 4);
}

function expenseCl() {
    $('#idfeature').hide();
    $('#idline').hide();
    $('#idexpense').show();
    removeclassblue();
    addclassblue(2, 5);
}
//格式化日期
function FormatDate(strTime) {
    var date = new Date(strTime);
    var month;
    var day;
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

function sortbydepartDate(a, b) {
    return a.departDate - b.departDate;
}
function sortbydayNumber(a, b) {
    return a.dayNumber - b.dayNumber;
}