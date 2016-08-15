angular.module('starter.controllers', [])

.controller('AppCtrl', function ($scope, $ionicModal, $timeout, $ionicScrollDelegate) {

    //滚动,下拉出电话
    $scope.getScrollPosition = function () {
        var scrolltop = $ionicScrollDelegate.$getByHandle('indexDelegate').getScrollPosition().top;
        $('.teledown').css('top', scrolltop + document.documentElement.childNodes[2].scrollHeight - 120);
    }
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


//线路列表控制器,从搜索进来
.controller('LinelistsCtrl', function ($scope, $http, filtbydaysev) {
    followfunc();
    var searchParam = decodeURI(getpbyurl2(1));  // request("search"); // 
    var nghttp = "../../ajax/apihandler.ashx?fn=getlines";

    $scope.listhistorygoback = function () {
        window.location.href = "#/app/index";
        //  location.reload();

        setTimeout(function () {
            $('#indexheadback').show();
            $('#divcontent').hide();
            $('#bartitle').hide();
            $('#divdesselect').show();
            if ($("h1.title div").length > 0) { }
            else
                $("h1.title").empty().append("线路搜索");
        }, 500)
    }

    //tdk seo
    tkdfunc(
        "" + searchParam + "旅游线路_" + searchParam + "线路报价_" + searchParam + "旅游攻略-锦江旅游",
        "" + searchParam + "旅游线路," + searchParam + "旅游报价," + searchParam + "旅游攻略,锦江旅游",
        "上海到" + searchParam + "旅游线路，锦江旅游提供上海到" + searchParam + "旅游线路价格,汇集上海旅行社品牌旅游线路,为您提供优质的旅游服务。"
        )

    $scope.pagences = "|";
    $scope.pdays = "|";

    var my2data = { fn: "getlinesbycategory", keyWord: searchParam };
    $scope.my2data = my2data;
    //旅行社筛选框bg----------------------------------------------------------------------------------------------------------------
    var windowwidth = window.innerWidth;
    var windowwidthscrooldown = windowwidth / 3 * 2;
    $(".dropdown-menu, .dropdown-menu-form").eq(0).css({ left: -windowwidthscrooldown / 2, width: windowwidth });
    $(".dropdown-menu, .dropdown-menu-form").eq(2).css({ left: -windowwidthscrooldown / 2, width: windowwidth });
    $(".dropdown-toggle").css({ width: windowwidthscrooldown / 2 });

    $scope.example1data0 = new Array();
    $scope.example1model0 = new Array();
    var funcallback = function (_responseche) {
        for (var i = 0; i < _responseche.length; i++) {
            $scope.example1data0[i] = { id: _responseche[i].name, label: _responseche[i].name };
        }
    }
    filtbydaysev.getagencies($http, $scope, funcallback, my2data);
    //旅行社筛选框ed----------------------------------------------------------------------------------------------------------------

    //天数筛选框bg----------------------------------------------------------------------------------------------------------------
    $(".dropdown-menu, .dropdown-menu-form").eq(1).css({ left: -windowwidthscrooldown, width: windowwidth });
    $(".dropdown-menu, .dropdown-menu-form").eq(3).css({ left: -windowwidthscrooldown / 2, width: windowwidth });

    $scope.example1data = new Array();
    $scope.example1model = new Array();
    var funcallback = function (_responseche) {
        for (var i = 0; i < _responseche.length; i++) {
            $scope.example1data[i] = { id: _responseche[i].name, label: _responseche[i].name + "天" };
        }
    }
    filtbydaysev.getdays($http, $scope, funcallback, my2data);

    var mylineCategoryName = searchParam;
    $scope.mylineCategoryName = searchParam;
    var funcallback2 = function (_responseche) {
        $(".title").empty().append(mylineCategoryName + "<div>共" + _responseche + "条</div>");
    }
    filtbydaysev.filtfunc($http, $scope, "|", "|", my2data, funcallback2);
    //天数筛选框ed----------------------------------------------------------------------------------------------------------------

    $(".linelistback").css("display", "none");
    $scope.listent = function () {
        setCookie('ent2detail', 'search=' + searchParam + '', 1);
    }

})
 //线路列表控制器2,唯独不一样的是路由,从头部的圆圈分类进来
.controller('LinelistsCtrl2', function ($scope, $http, filtbydaysev) {
    followfunc();

    $scope.pagences = "|";
    $scope.pdays = "|";

    var url = location.href;
    var lineCategory = url.substring(url.lastIndexOf('/') + 1, url.length);
    var my2data = "";
    if (lineCategory === "nearby") {
        my2data = { fn: "getnearby" };
    }
    else if (lineCategory === "domestic" || lineCategory === "outbound" || lineCategory === "inbound") {
        my2data = { fn: "getlinecategoriecrm2", category: lineCategory };
    }
    else {
        my2data = { fn: "getlinecategoriecrm", category: lineCategory };
    }
    $scope.my2data = my2data;
    var mylineCategoryName = replaceCategory(lineCategory);
    $scope.mylineCategoryName = mylineCategoryName;

    //tdk seo
    tkdfunc(
        "" + mylineCategoryName + "旅游线路_" + mylineCategoryName + "线路报价_" + mylineCategoryName + "旅游攻略-锦江旅游",
        "" + mylineCategoryName + "旅游线路," + mylineCategoryName + "旅游报价," + mylineCategoryName + "旅游攻略,锦江旅游",
        "锦江旅游提供包括上海" + mylineCategoryName + "、" + mylineCategoryName + "线路报价，出国旅游景点等多样化旅行服务。锦江旅游是中国最优质的旅游线路和自助游一站式服务提供商。"
        )

    //旅行社筛选框bg----------------------------------------------------------------------------------------------------------------
    var windowwidth = window.innerWidth;
    var windowwidthscrooldown = windowwidth / 3 * 2;
    $(".dropdown-menu, .dropdown-menu-form").eq(0).css({ left: -windowwidthscrooldown / 2, width: windowwidth });
    $(".dropdown-menu, .dropdown-menu-form").eq(2).css({ left: -windowwidthscrooldown / 2, width: windowwidth });
    $(".dropdown-toggle").css({ width: windowwidthscrooldown / 2 });

    $scope.example1data0 = new Array();
    $scope.example1model0 = new Array();
    var funcallback = function (_responseche) {
        for (var i = 0; i < _responseche.length; i++) {
            $scope.example1data0[i] = { id: _responseche[i].name, label: _responseche[i].name };
        }
    }
    filtbydaysev.getagencies($http, $scope, funcallback, my2data);
    //旅行社筛选框ed----------------------------------------------------------------------------------------------------------------

    //天数筛选框bg----------------------------------------------------------------------------------------------------------------
    $(".dropdown-menu, .dropdown-menu-form").eq(1).css({ left: -windowwidthscrooldown, width: windowwidth });
    $(".dropdown-menu, .dropdown-menu-form").eq(3).css({ left: -windowwidthscrooldown, width: windowwidth });

    $scope.example1data = new Array();
    $scope.example1model = new Array();
    var funcallback = function (_responseche) {
        for (var i = 0; i < _responseche.length; i++) {
            $scope.example1data[i] = { id: _responseche[i].name, label: _responseche[i].name + "天" };
        }
    }
    filtbydaysev.getdays($http, $scope, funcallback, my2data);

    var funcallback2 = function (_responseche) {
        $(".title").empty().append(mylineCategoryName + "<div>共" + _responseche + "条</div>");
    }
    filtbydaysev.filtfunc($http, $scope, "|", "|", my2data, funcallback2);
    //天数筛选框ed----------------------------------------------------------------------------------------------------------------

    $scope.listent = function () {
        setCookie('ent2detail', lineCategory, 1);
    }
    $scope.listhistorygoback = function () {
        window.location.href = "#/app/index";
        location.reload();
    }
    //这是右边的回到搜索界面按钮
    $(".linelistback").css("display", "block");
    $scope.listhistorygoback2 = function () {
        // debugger
        window.location.href = "#/app/index";

        setTimeout(function () {
            $('#indexheadback').show();
            $('#divcontent').hide();
            $('#bartitle').hide();
            $('#divdesselect').show();
            if ($("h1.title div").length > 0) { }
            else
                $("h1.title").empty().append("线路搜索");
        }, 500)
    }

    $scope.change1 = function (x) {
        $scope.orderByDay = "";
    }
    $scope.change2 = function (x) {
        $scope.orderByPrice = "";
    }
})


//主页控制器
.controller('indexCtrl', function ($scope, $http, $ionicScrollDelegate) {
    // $("h1.title").empty().append("锦江2旅游");
    followfunc();
    tkdfunc(
        "锦江旅游度假_旅行社旅游线路_旅游景点攻略-锦江旅游",
        "锦江旅游,旅游网站,出境游,境内游,周边游,自由行,锦江国际",
        "锦江旅游提供包括旅游线路咨询预订,旅游酒店预订,旅游线路查询，旅游车辆租赁等多样化旅行服务。")
    $scope.tomyinfo = function () {
        window.location.href = "#/app/user/myinfo";
    }
    //实现自带搜索按钮跳转并失去焦点关闭键盘.
    $(function () {
        $('.searchtxt').bind('keypress', function (event) {
            if (event.keyCode == "13") {
                searchLines(this);
                document.activeElement.blur();
            }
        });
    });

    $scope.goback = function () {
        $('#divcontent').show();
        $('#bartitle').show();
        $('#divdesselect').hide();

        $('#indexheadback').hide();
    }
    $scope.searchlines = function () {
        searchLines();
    }
    var ua = navigator.userAgent.toLowerCase();
    if (navigator.userAgent.indexOf('UCBrowser') > -1) {
        $(".imgh").css({ "max-height": "70px", "max-width": "70px" })
        $(".imgo").css({ "max-height": "45px", "max-width": "45px", "margin-top": "10%" })
        $(".borderlefty").css({ "margin-left": "-5%" })
    }
    else {
        $(".borderlefty").css({ "margin-left": "3%" })
        $(".imgo").css({ "max-height": "45px", "max-width": "45px" })
        $(".borderlefty").css({ "width": "70%" })
    }
    if (/iphone|ipad|ipod/.test(ua) && navigator.userAgent.indexOf('UCBrowser') > -1) {
        $(".imgh").css({ "max-height": "35px", "max-width": "35px" })
        $(".imgo").css({ "max-height": "45px", "max-width": "45px", "margin-top": "10%" })
        $(".borderlefty").css({ "margin-left": "5%" });
    }

    //滚动,下拉出电话
    $scope.getScrollPosition = function () {
        var scrolltop = $ionicScrollDelegate.$getByHandle('indexDelegate').getScrollPosition().top;
        $('.teledown').css('top', scrolltop + document.documentElement.childNodes[2].scrollHeight - 120);
    }
    //分类图标
    var nghttpcategory = "../../ajax/apihandler.ashx?fn=getlinecategorys&status=true&pattern=S1";
    $http.get(nghttpcategory).success(function (response) {
        $scope.linecategorys = response.ds;
    })

    var nghttp02 = "../../../ajax/lineCategoryHandler.ashx?fn=getpatternss2";
    $http.get(nghttp02).success(function (response) {
        $scope.linecategorys2 = response.ds;
    })
    //分类样式S2
    var nghttppattern = "../../ajax/apihandler.ashx?fn=getlinecategorys2&status=true&pattern=S2";
    $http.get(nghttppattern).success(function (response) {
        var myimgurl;
        var linearr = "";
        for (var i = 0; i < response.ds.length; i++) {
            myimgurl = response.ds[i].imageUrls;
            linearr += response.ds[i].lineId + ",";
            if (myimgurl.indexOf('|') > 0) {
                myimgurl = myimgurl.substring(0, myimgurl.indexOf('|'));
                response.ds[i].imageUrls = myimgurl;
            }
            else
                response.ds[i].imageUrls = "";
        }
        linearr = linearr.substr(0, linearr.length - 1);
        $scope.linecategorys2detail = response.ds;

        //取价格
        var nghttppattern10 = "../../ajax/apihandler.ashx?fn=getlinesprice&linearr=" + linearr + "";
        $http.get(nghttppattern10).success(function (response) {
            for (var i = 0; i < $scope.linecategorys2detail.length; i++) {
                for (var j = 0; j < response.length; j++) {
                    if ($scope.linecategorys2detail[i].lineId == response[j].id) {
                        $scope.linecategorys2detail[i].minPrice = response[j].minPrice;
                    }
                }
            }
        })

    })

    $scope.indexent = function () {
        setCookie('ent2detail', 'index', 1);
    }
    setCookie('ent2detail', 'index', 1);
    //城目的地选择
    $scope.desSelect = function () {

        $('#indexheadback').show();
        $('#divcontent').hide();
        $('#bartitle').hide();
        $('#divdesselect').show();

        $('#divdesselect').click(function (event) {
            if (event.target.className === 'searchDest') {
                $(".searchtxt")[1].value = event.target.innerText;
            }
        })
    }

    //二级线路查询
    $scope.search0Dest = function (event) {
        var aId = event.currentTarget.lastElementChild.innerText;
        var nghttp = "../../ajax/areaHandler.ashx?fn=getarea2list&aId=" + aId + "";
        $http.get(nghttp).success(function (response) {
            $scope.dest = response.ds;
        })
    }
    //二级线路的选择
    $scope.search1Dest = function (event) {
        //如果有做过特殊关键词链接,则直接跳转不解释.否则就老老实实查询
        if (!event.currentTarget.childNodes[1].innerText) {
            $(".searchtxt")[1].value = event.currentTarget.innerText;
            $(".searchtxt")[1].placeholder = "";
            searchLines();
            return;
        }
        else {
            window.location.href = event.currentTarget.childNodes[1].innerText;
            return;
        }

    }
    var nghttp3 = "../../ajax/areaHandler.ashx?fn=getarealist";
    $http.get(nghttp3).success(function (response) {
        $scope.area = response.ds;
    })

    //查看更多
    $scope.seemore = function (event) {
        //debugger
        var thislineCategory = event.currentTarget.childNodes[0].innerText;
        thislineCategory = thislineCategory.toLowerCase();
        $('.amore').href = "/app/linelists/" + thislineCategory;
        window.location.href = "#/app/linelists/" + thislineCategory;
    }


    //自加载运行
    $scope.$on("$ionicView.loaded", function () {
        //自动加载播放滚动图片
        //轮播图
        var nghttpgg = "../../ajax/bannerImgHandler.ashx?fn=getbannerimglist&status=true";
        $http.get(nghttpgg).success(function (response) {
            for (var i = 0; i < (response.ds.length > 6 ? 6 : response.ds.length) ; i++) { //response.ds.length
                var urls;
                if (response.ds[i].lineId === 0) {
                    urls = response.ds[i].H5Url;
                }
                else {
                    urls = "#/app/linedetail/" + response.ds[i].lineId;
                }
                $('.indexion #full-width-slider').append('<div class="rsContent"><a href="' + urls + '"><img class="rsImg" src=' + response.ds[i].imgUrl + ' /></a></div>');
            }
            $('.indexion #full-width-slider').royalSlider({
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
        })

        //初始化二级目的地页面
        var aId = 1;
        var nghttp = "../../ajax/areaHandler.ashx?fn=getarea2list&aId=" + aId + "";
        $http.get(nghttp).success(function (response) {
            $scope.dest = response.ds;
        })

        //空搜关键词
        var nghttp = "../../ajax/areaHandler.ashx?fn=getarea3list";
        $http.get(nghttp).success(function (response) {
            $("#divdesselect .searchtxt").attr('placeholder', response.ds[0].searchName);
            $("#divdesselect .searchtxturl")[0].value = response.ds[0].H5Url;
        })

    });

})

//线路详情控制器
.controller('lineDetailCtrl', function ($scope, $http, $sce, $ionicScrollDelegate) {
    followfunc();
    var url = location.href;
    var lineid = url.substring(url.lastIndexOf('/') + 1, url.length);
    $('.linedetail .ordernow').attr('href', '#/app/indexdate/' + lineid);

    //页面详情页分两种情况返回,三个入口
    $scope.historygoback = function () {
        var ent2detail = getCookie('ent2detail');
        if (ent2detail.indexOf("search=") > -1) {
            window.location.href = "#/app/linelists#" + ent2detail + "";
            location.reload();
        }
        else if (ent2detail != "index") {
            window.location.href = "#/app/linelists/" + ent2detail;
            location.reload();
        }
        else {
            window.location.href = "#/app/index";
            location.reload();
        }
    }

    var nghttp = "../../ajax/apihandler.ashx?fn=getlinedetail&lineid=" + lineid + "";

    //loading层
    var mylayeruiwait = layer.load(1, {
        shade: [0.5, '#ababab'] //0.1透明度的白色背景
    });
    $http.get(nghttp).success(function (response) {
        //$.unblockUI();
        find404admin(response);
        layer.close(mylayeruiwait);
        //团框初始高度
        $(".linedetail .groupsheight").height(16);
        $(".linedetail .groupsinheight").height(16);

        //行程
        //debugger
        if (response.line === null) {
            $(".title").empty().append("锦江旅游");
            window.location.href = "#/app/index";
            location.reload();
            layermyui('此线路暂无详细数据!', 1500);
            // setTimeout(function () {  }, 500);
            return;
        }
        $scope.linedetails = response.line;
        var titlename = response.line.name;
        //tdk seo
        tkdfunc(
            "" + titlename + "-锦江旅游",
            "" + titlename + ",锦江旅游",
            "上海出发" + titlename + ", 锦江旅游是中国最优质的旅游线路和自助游一站式服务提供商。"
            );
        //$(".title").empty().append("" + titlename + "-锦江旅游");

        $scope.journeys = response.line.journeys.sort(sortbydayNumber);

        setTimeout(settypepng, 500);

        //行程明细
        //$sce 是 angularJS 自带的安全处理模块，$sce.trustAsHtml(str) 方法便是将数据内容以 html 的形式进行解析并返回。将此过滤器添加到 ng-bind-html 、data-ng-bind-html  所绑定的数据中，便实现了在数据加载时对于 html 标签的自动转义。
        if (response.line.lineFeature !== null)
            $scope.featureContent = $sce.trustAsHtml(response.line.lineFeature.replace(/\n/g, "<br>"));
        if (response.line.priceInclude !== null)
            $scope.priceIncludeContent = $sce.trustAsHtml(response.line.priceInclude.replace(/\n/g, "<br>"));
        if (response.line.priceExclusive !== null)
            $scope.priceExclusiveContent = $sce.trustAsHtml(response.line.priceExclusive.replace(/\n/g, "<br>"));

        //取团日期
        response.line.groups = response.line.groups.sort(sortbydepartDate);
        for (var i = 0; i < response.line.groups.length; i++) {
            for (var i = 0; i < response.line.groups.length; i++) {
                if (i != response.line.groups.length - 1) {
                    response.line.groups[i].departDate = FormatDate(response.line.groups[i].departDate) + ',';
                }
                else
                    response.line.groups[i].departDate = FormatDate(response.line.groups[i].departDate);
            }
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

        //团日列表数量
        dayslength = response.line.groups.length;
        if (dayslength <= 4) {
            $(".linedetail .gengduoimg").css("display", "none");
        }
        else {
            $(".linedetail .gengduoimg").css("display", "block");
        }

        //取团号
        $scope.groupcode = '团号:' + response.line.groups[0].groupCode.substring(response.line.groups[0].groupCode.length - 14, response.line.groups[0].groupCode.length);

        //取线路旅游类型
        $scope.lineCategory = getcategoryNameByCode(response.line.lineCategory);

        //取价格
        $scope.price = response.minPrice;
        //产品经理推荐
        $scope.recommend = response.line.recommend; // response.line.recommend.url;

        //取图片
        //if (response.line.images[0] === undefined || response.line.images[0] === null || response.line.images[0] === "")
        //    $scope.image = 'http://img5.imgtn.bdimg.com/it/u=45254662,160915219&fm=21&gp=0.jpg'
        //else {
        //    //debugger
        //    $scope.image = response.line.images[0].url.substr(0, response.line.images[0].url.length - 14) + "/1/interlace/1/w/440/h/230";
        //}

        $('.linedetail .idline').show();
        $('.linedetail .idfeature').hide();
        $('.linedetail .idexpense').hide();
        $(".linedetail .tunbl1").addClass("contentblue");
        $(".linedetail .tunbl4").addClass("lineblue");


    });
    //设置行程类型图标样式
    function settypepng() {
        for (var i = 0; i < $(".linedetail .cltypetxt").length ; i++) {
            if ($(".linedetail .cltypetxt")[i].innerText == "酒店") {
                $(".linedetail .cltypeimg")[i].className = "cltypeimg trip_eat";
            }
            if ($(".linedetail .cltypetxt")[i].innerText == "景点") {
                $(".linedetail .cltypeimg")[i].className = "cltypeimg trip_spots";
            }
            if ($(".linedetail .cltypetxt")[i].innerText == "航班") {
                $(".linedetail .cltypeimg")[i].className = "cltypeimg trip_air";
            }
            if ($(".linedetail .cltypetxt")[i].innerText == "自由活动") {
                $(".linedetail .cltypeimg")[i].className = "cltypeimg trip_free";
            }
            if ($(".linedetail .cltypetxt")[i].innerText == "用餐") {
                $(".linedetail .cltypeimg")[i].className = "cltypeimg trip_eat";
            }
            if ($(".linedetail .cltypetxt")[i].innerText == "交通") {
                $(".linedetail .cltypeimg")[i].className = "cltypeimg trip_car";
            }
            if ($(".linedetail .cltypetxt")[i].innerText == "其他") {
                $(".linedetail .cltypeimg")[i].className = "cltypeimg trip_other";
            }
            if ($(".linedetail .cltypetxt")[i].innerText == "购物") {
                $(".linedetail .cltypeimg")[i].className = "cltypeimg trip_shop";
            }

        }
    }

    $scope.lineCl = function () {
        $('.linedetail .idfeature').hide();
        $('.linedetail .idline').show();
        $('.linedetail .idexpense').hide();
        removeclassblue();
        $('.tunbl1').addClass("contentblue");
        $('.tunbl4').addClass("lineblue");
        $ionicScrollDelegate.resize();
    }

    $scope.featureCl = function () {
        $('.linedetail .idfeature').show();
        $('.linedetail .idline').hide();
        $('.linedetail .idexpense').hide();
        removeclassblue();
        $('.tunbl2').addClass("contentblue");
        $('.tunbl5').addClass("lineblue");
        $ionicScrollDelegate.resize();
    }

    $scope.expenseCl = function () {
        $('.linedetail .idfeature').hide();
        $('.linedetail .idline').hide();
        $('.linedetail .idexpense').show();
        removeclassblue();
        $('.tunbl3').addClass("contentblue");
        $('.tunbl6').addClass("lineblue");
        $ionicScrollDelegate.resize();
    }

    //自加载运行
    $scope.$on("$ionicView.loaded", function () {
        //自动加载播放滚动图片
        //轮播图
        var url = location.href;
        var lineid = url.substring(url.lastIndexOf('/') + 1, url.length);
        var nghttp = "../../ajax/apihandler.ashx?fn=getlinedetail&lineid=" + lineid + "";
        $http.get(nghttp).success(function (response) {
            //取图片
            if (!response.line.images.length) {
                layermyui('轮播图未设置');
            }
            else {
                for (var i = 0; i < response.line.images.length ; i++) {
                    var abc = response.line.images[i].url.substr(0, response.line.images[i].url.length - 14) + "/1/interlace/1/w/440/h/230";
                    $('.linedetail #full-width-slider').append('<div class="rsContent"><img class="rsImg" src=' + abc + ' /></div>');
                }
            }

            $('.linedetail #full-width-slider').royalSlider({
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
        })

    })


})

//日期选择控制器
.controller('indexdateCtrl', function ($scope, $http) {
    nofollowfunc();
    $scope.$on("$ionicView.loaded", function () {
        var url = location.href;
        var lineid = url.substring(url.lastIndexOf('/') + 1, url.length);
        var nghttp = "../../ajax/apihandler.ashx?fn=getlinedetail&lineid=" + lineid + "";

        var mylayeruiwait = layer.load(1, {
            shade: [0.5, '#ababab'] //0.1透明度的白色背景
        });
        $http.get(nghttp).success(function (response) {
            layer.close(mylayeruiwait);

            var preBookingDays = response.line.preBookingDays;
            var datenow = getNowFormatDate();
            intoCalendarTime();

            //var nummaxa = (totnum > 10 ? 10 : totnum), nummaxb = (totnum > 10 ? 10 : totnum);
            $('.spinnerad').myspinner({ max: 10, min: 1 });
            $('.spinnercd').myspinner({ max: 10 });

            adn = 1;
            crn = 0;
            $(".indexdate #sp01").click(function () {
                adn = this.children[0].children[1].value;
                $('#nextpick').attr('href', nextpickhref + '/' + adn + '/' + crn);
                maxpassenger(".indexdate #sp01", ".indexdate #sp02", adn, crn, 10);
            });
            $(".indexdate #sp02").click(function () {
                crn = this.children[0].children[1].value;
                $('#nextpick').attr('href', nextpickhref + '/' + adn + '/' + crn);
                maxpassenger(".indexdate #sp01", ".indexdate #sp02", adn, crn, 10);
            });
            function intoCalendarTime() {
                var mindate = "2018-01-01";
                var maxdate = "2016-01-01";
                data = "[";
                for (var i = 0; i < response.line.groups.length; i++) {
                    var date1 = FormatDateYear(response.line.groups[i].departDate);
                    if (daysBetween(datenow, date1) > preBookingDays) {
                        var groupid = response.line.groups[i].id;
                        for (var j = 0; j < response.line.groups[i].prices.length; j++) {
                            if (response.line.groups[i].prices[j].offerType == '基本价') {
                                //不等于取vipPrice 不打折,等于取salePrice 打折
                                minprice = getCurrentPrice(response.line.groups[i].prices[j].salePrice, response.line.groups[i].prices[j].vipPrice);
                            }
                        }
                        var price1 = "¥" + minprice;
                        data += '{"Date":"' + date1 + '","Price":"' + price1 + '","groupid":"' + groupid + '"},';
                        //选择最大月份
                        if (date1 > maxdate) {
                            maxdate = date1;
                        }
                        //选择最小月份
                        if (date1 < mindate) {
                            mindate = date1;
                        }
                    }
                }
                data += "]";
                pickerEvent.setPriceArr(eval("(" + data + ")"));

                //选择最大月份,选择最小月份
                var maxmonth = maxdate.substr(maxdate.indexOf("-") + 1, 2);
                var minmonth = mindate.substr(mindate.indexOf("-") + 1, 2);
                pickerEvent.setEndMonth(maxmonth);
                pickerEvent.setBeginMonth(minmonth);
                if (minmonth.indexOf("0") == 0)
                    minmonth = minmonth.substr(1, 1)
                pickerEvent.setMinMonth(minmonth);
                pickerEvent.Init("calendar");
            }
        })
        $scope.next = function () {
            var totnum = $('.numpera')[0].innerText;
            if (totnum < parseInt(adn) + parseInt(crn)) {
                layermyui('余位不足');
                $('#nextpick').attr('href', '');
            }
            else if (10 < parseInt(adn) + parseInt(crn)) {
                layermyui('成人+儿童最多支持10人');
                $('#nextpick').attr('href', '');
            }
            else
                $('#nextpick').attr('href', nextpickhref + '/' + adn + '/' + crn);
        }
    });
})

//选择资源控制器2
.controller('pickresourceCtrl2', function ($scope, $http) {
    nofollowfunc();
    var cnum = getpbyurl(1);
    var pnum = getpbyurl(2);
    var groupid = getpbyurl(3);

    $scope.pnum = pnum;
    $scope.cnum = cnum;
    if (cnum > 0) {
        $('#childpricebox').css('display', 'block');
    }
    else {
        $('#childpricebox').css('display', 'none');
        $('#adultpricebox').css("width", "50%");
        $('#groupdatebox').css("width", "50%");
    }
    $('#secureamount').empty().append('0');
    $('.spinnerdif').myspinner({ max: parseInt(cnum) + parseInt(pnum) });
    var amount = 0;
    var secureamount = 0;

    var mylayeruiwait = layer.load(1, {
        shade: [0.5, '#ababab'] //0.1透明度的白色背景
    });
    var nghttp = "../../ajax/apihandler.ashx?fn=queryrealtimerefresh&groupid=" + groupid + "";
    $http.get(nghttp).success(function (response) {
        layer.close(mylayeruiwait);
        var cprice;
        var dprice;
        for (var j = 0; j < response.prices.length; j++) {
            if (response.prices[j].offerType == '基本价') {
                //不等于取vipPrice 不打折,等于取salePrice 打折
                minprice = getCurrentPrice(response.prices[j].salePrice, response.prices[j].vipPrice);
            }
            if (response.prices[j].offerType == '儿童价')
                cprice = response.prices[j].salePrice;// getCurrentPrice(response.prices[j].salePrice, response.prices[j].vipPrice);
            if (response.prices[j].offerType == '单房差')
                dprice = response.prices[j].salePrice;// getCurrentPrice(response.prices[j].salePrice, response.prices[j].vipPrice);
        }
        $scope.minprice = minprice;
        if (cprice > 0) {
            $scope.caddprice = minprice - cprice;
            $scope.cprice = cprice;
            $('#divchild').css('display', 'block');
        }
        else {
            cprice = 0
            $('#divchild').css('display', 'none');
        }

        if (dprice > 0) {
            $scope.dprice = dprice;
            $('#divdiff').css('display', 'block');
        }
        else {
            dprice = 0
            $('#divdiff').css('display', 'none');
        }

        $scope.date = FormatDateYear(response.departDate);
        $scope.departurePlace = response.departurePlace;
        $scope.lineTitle = response.lineTitle;
        var cancelprice = 50;
        var accidentprice = 80;
        samount1 = 0;
        samount2 = 0;
        $("#checkcancel").click(function () {
            if (this.checked == false) {
                samount1 = 0;
                secureamount -= cancelprice;
            }
            else {
                samount1 = cancelprice;
                secureamount += cancelprice;
            }
            subamount();
            $('#secureamount').empty().append(secureamount);
        });
        $("#checkaccident").click(function () {
            if (this.checked == false) {
                samount2 = 0;
                secureamount -= accidentprice;
            }
            else {
                samount2 = accidentprice;
                secureamount += accidentprice;
            }
            subamount();
            $('#secureamount').empty().append(secureamount);
        });

        roomdiff = 0;
        roomdiffp1 = 0;
        roomdiffp2 = 0;
        $("#sp1").click(function () {
            roomdiffp1 = this.children[0].children[1].value;
            subamount();
        });
        $("#sp2").click(function () {
            roomdiffp2 = this.children[0].children[1].value;
            subamount();
        });
        $("#sp1").change(function () {
            roomdiffp1 = this.children[0].children[1].value;
            subamount();
        });
        $("#sp2").change(function () {
            roomdiffp2 = this.children[0].children[1].value;
            subamount();
        });
        function subamount() {
            roomdiff = dprice * roomdiffp1 + (minprice - cprice) * roomdiffp2 + samount1 * pnum + samount2 * pnum;
            setCookie('roomdiff', roomdiffp1, 1);
            amountall = minprice * pnum + roomdiff + cprice * cnum;
            $('#amount').empty().append(amountall);
            $('#amountct').empty().append(minprice * pnum);
            $('#nextfill').attr('href', '#/app/fillorder/' + secureamount + '/' + groupid + '/' + pnum + '/' + cnum + '/' + amountall);
        }
        subamount();
    })

})

//填写订单控制器
.controller('fillorderCtrl', function ($scope, $http) {
    nofollowfunc();

    var amount = getpbyurl(1);
    var cnum = getpbyurl(2);
    var pnum = getpbyurl(3);
    var groupid = getpbyurl(4);
    var secureamount = getpbyurl(5);

    var Connect = {
        name: '',
        mobile: '',
        email: ''
    };
    $scope.Connect = Connect;

    var priceid;
    var Discount;
    var salePrice = 0;
    var childPrice = 0;
    var roomdifPrice = 0;
    var roomdifPriceid = 0;
    $scope.amount = amount;
    $scope.pnum = pnum;
    $scope.cnum = cnum;
    if (cnum > 0)
        $('#childnumspan').css('display', 'inline');
    else
        $('#childnumspan').css('display', 'none');

    var nghttp = "../../ajax/apihandler.ashx?fn=queryrealtimerefresh&groupid=" + groupid + "";
    //loading层
    var mylayeruiwait = layer.load(1, {
        shade: [0.5, '#ababab'] //0.1透明度的白色背景
    });
    $http.get(nghttp).success(function (response) {
        find404admin(response);
        layer.close(mylayeruiwait);
        $scope.lineTitle = response.lineTitle;
        $scope.date = FormatDateYear(response.departDate);
        for (var j = 0; j < response.prices.length; j++) {
            if (response.prices[j].offerType == '基本价') {
                priceid = response.prices[j].id;
                //不等于取vipPrice 不打折,等于取salePrice 打折
                salePrice = getCurrentPrice(response.prices[j].salePrice, response.prices[j].vipPrice);
                //不等于取vipPrice 不打折,等于取salePrice 打折
                if (response.prices[j].salePrice == response.prices[j].vipPrice) {
                    Discount = response.onlineDiscount;
                }
                else {
                    Discount = 1;
                }
            }
            else if (response.prices[j].offerType == '儿童价') {
                childPrice = response.prices[j].salePrice;// getCurrentPrice(response.prices[j].salePrice, response.prices[j].vipPrice);
            }
            else if (response.prices[j].offerType == '单房差') {
                roomdifPrice = response.prices[j].salePrice;// getCurrentPrice(response.prices[j].salePrice, response.prices[j].vipPrice);
                roomdifPriceid = response.prices[j].id;
            }
        }


        //出现的出行人行数.
        var arrayGuests = new Array(0);
        for (var i = 0; i < pnum; i++) {
            arrayGuests.push(i);
        }
        $scope.guests = arrayGuests;

        //$.unblockUI();
    })

    var guestsarr = new Array(0);
    $scope.createorder = function () {
        var roomdiff = 0;
        if (getCookie('roomdiff') > 0)
            roomdiff = getCookie('roomdiff');
        //blockmyui('正在加载,请稍后...');
        var ConnectName = $scope.Connect.name;
        var ConnectMobile = $scope.Connect.mobile;
        var ConnectEmail = $scope.Connect.email;
        if (ConnectName == "" || ConnectName == undefined || ConnectName == null) {
            layermyui('请输入联系人');
            return;
        }
        if (ConnectMobile == "" || ConnectMobile == undefined || ConnectMobile == null) {
            layermyui('请输入手机号');
            return;
        }
        if (!checkMobile(ConnectMobile)) {
            layermyui('手机号格式不正确');
            return;
        }
        //if (ConnectEmail == "" || ConnectEmail == undefined || ConnectEmail == null) {
        //    layermyui('请输入邮箱');
        //    return;
        //}
        //if (!isEmail(ConnectEmail)) {
        //    layermyui('邮箱格式不正确');
        //    return;
        //}

        //     $('.inname:first')[0].value = ConnectName;

        function CGuest(category, name) {
            this.category = category;
            this.name = name;
        }
        var p = new CGuest();
        //  p = new CGuest('ADULT', $('.inname:first')[0].value);
        //   guestsarr.push(p);
        for (var i = 0 ; i < pnum; i++) {
            try {
                p = new CGuest('ADULT', $('.inname')[i].value == "" ? "游客" + (parseInt(i) + 1) : $('.inname')[i].value);
                guestsarr.push(p);
            }
            catch (e) { }
        }

        var gueststring = "";
        for (var i = 0; i < pnum; i++) {
            gueststring += "{\"category\":\"" + guestsarr[i].category + "\",\"name\":\"" + guestsarr[i].name + "\"},";
        }
        gueststring = gueststring.substring(0, gueststring.length - 1);

        //var discountAmount = Math.floor(amount * (1 - Discount));

        /*打折逻辑如下：
        disAmount = Math.floor(or.getSinglePrice().multiply(new BigDecimal(1 - group.getOnlineDiscount())).doubleValue());
        disAmount = disAmount * or.getCopies();*/
        //只有成人享受折扣价
        var discountAmount = Math.floor(salePrice * (1 - Discount)) * pnum;//+ Math.floor(childPrice * (1 - Discount)) * cnum;

        //动态成人数.
        var mcMemberCode = getCookie('mcMemberCode');
        var strcopyroom = "";
        if (roomdiff > 0) {
            strcopyroom += ",{ \"copies\": " + roomdiff + ", \"discountAmount\": 0, \"priceId\": " + roomdifPriceid + ", \"singlePrice\": " + roomdifPrice + " }"
        }
        if (cnum > 0) {
            strcopyroom += ",{ \"copies\": " + cnum + ", \"discountAmount\": 0, \"priceId\": " + priceid + ", \"singlePrice\": " + childPrice + " }"
        }
        json = "{\"adultNum\":" + pnum + ",\"amount\":" + amount + ",\"channel\":\"E_BUSINESS_PLATFORM\",\"childNum\":" + cnum + ",\"contact\":{\"mobile\":\"" + ConnectMobile + "\",\"name\":\"" + ConnectName + "\",\"email\":\"" + ConnectEmail + "\"},\"couponAmount\":0,\"groupId\":" + groupid + ",\"guests\":[" + gueststring + "],\"mcMemberCode\":\"" + mcMemberCode + "\",\"cardNo\":\"1231234\",\"onLinePay\":true,\"receivables\":[{\"copies\":" + pnum + ",\"discountAmount\":" + discountAmount + ",\"priceId\":" + priceid + ",\"singlePrice\":" + salePrice + "}" + strcopyroom + "],\"scorePay\":false}";

        //loading层
        var mylayeruiwait = layer.load(1, {
            shade: [0.5, '#ababab'] //0.1透明度的白色背景
        });
        $.ajax({
            url: "../../ajax/apihandler.ashx?fn=createorder&json=" + json + "",
            type: "post",
            success: function (text) {
                layer.close(mylayeruiwait);
                var errormsg = finderrorMsgadmin(text);
                if (errormsg) {
                    if (errormsg == "mcMemberCode不能为空!") {
                        //把参数存入cookie
                        setCookie('amount', amount, 1);
                        setCookie('cnum', cnum, 1);
                        setCookie('pnum', pnum, 1);
                        setCookie('groupid', groupid, 1);
                        setCookie('secureamount', secureamount, 1);
                        //将跳回支付该产品的cookie
                        setCookie('linkbackpay', 'true', 1);
                        //跳转至登录页
                        window.location.href = '#/app/user/login';
                        return;
                    }
                    else {
                        layermyui(errormsg, 3000);
                        return;
                    }
                }
                //出行人只显示成人,有几人就设置几个cookiename
                for (var i = 0; i < pnum; i++) {
                    setCookie('inname' + i, $('.inname')[i].value == "" ? "游客" + (parseInt(i) + 1) : $('.inname')[i].value, 1);
                }
                //$.unblockUI();
                var d = eval("(" + text + ")");
                setCookie('orderNo', d.orderNo, 1);
                amount = amount - discountAmount;
                window.location.href = '#/app/payway/' + secureamount + '/' + groupid + '/' + pnum + '/' + cnum + '/' + amount;
            }
        });
    }

})

//支付方式控制器
.controller('paywayCtrl', function ($scope, $http) {
    nofollowfunc();
    //blockmyui('正在加载,请稍后...');
    //清除登录用户cookie
    //setCookie('mcMemberCode','',1);
    var dfnum = getCookie('roomdiff');
    var amount = getpbyurl(1);
    var cnum = getpbyurl(2);
    var pnum = getpbyurl(3);
    var groupid = getpbyurl(4);
    var secureamount = getpbyurl(5);
    $scope.amount = amount;

    var nghttp = "../../ajax/apihandler.ashx?fn=queryrealtimerefresh&groupid=" + groupid + "";
    //loading层
    var mylayeruiwait = layer.load(1, {
        shade: [0.5, '#ababab'] //0.1透明度的白色背景
    });
    $http.get(nghttp).success(function (response) {
        //debugger
        find404admin(response);
        layer.close(mylayeruiwait);
        var minprice;
        var childprice = 0;
        var dfprice = 0;
        for (var j = 0; j < response.prices.length; j++) {
            if (response.prices[j].offerType == '基本价') {
                //不等于取vipPrice 不打折,等于取salePrice 打折
                minprice = getCurrentPrice(response.prices[j].salePrice, response.prices[j].vipPrice);
            }
            else if (response.prices[j].offerType == '儿童价')
                childprice = response.prices[j].salePrice;// getCurrentPrice(response.prices[j].salePrice, response.prices[j].vipPrice);
            else if (response.prices[j].offerType == '单房差')
                dfprice = response.prices[j].salePrice;// getCurrentPrice(response.prices[j].salePrice, response.prices[j].vipPrice);
        }
        $scope.minprice = minprice;
        $scope.childprice = childprice;
        $scope.dfprice = dfprice;
        $scope.secureamount = secureamount;
        $scope.groupCode = response.groupCode;
        $scope.lineTitle = response.lineTitle;
        $scope.pnum = pnum;
        $scope.cnum = cnum;
        if (cnum > 0) {
            $(".childgogo").css("display", "inline")
            $(".childgogo2").css("display", "block")
        }
        else {
            $(".childgogo").css("display", "none")
            $(".childgogo2").css("display", "none")
        }
        if (dfnum > 0) {
            $(".dfgo").css("display", "block")
        }
        else {
            $(".dfgo").css("display", "none")
        }
        $scope.departDate = FormatDateYear(response.departDate);
        $scope.timepay = FormatDateTimeDiff(3600000);


        //出行人只显示成人,有几人就显示几个cookiename-----------st
        var arrayinname = new Array(0);
        function CGuest(name) {
            this.name = name;
        }
        for (var i = 0; i < pnum; i++) {
            p = new CGuest(getCookie('inname' + i));
            arrayinname.push(p);
        }
        $scope.inname = arrayinname;

        $('.innamebox').height(60 + 31 * (pnum - 2));
        //出行人只显示成人,有几人就显示几个cookiename-----------ed

        //$.unblockUI();

        var accountName = '';

        //判断是否可以在线支付
        if (response.payOnlineFlag == 0) {
            layermyui('不允许在线支付,请到门店支付');
            $("[type=checkbox]").attr("disabled", true);
            return;
        }
        $scope.pay = function () {
            if (accountName == "") {
                layermyui('请选择支付方式');
                return;
            }
            //首先做身份认证,判断是否已经登录,没有帐号的客户先注册.
            var mcMemberCode = getCookie('mcMemberCode');
            if (mcMemberCode != "" && mcMemberCode != undefined && mcMemberCode != null) {
                //其次再是发起付款
                var orderNo = getCookie('orderNo');
                var nghttp = "../../ajax/apihandler.ashx?fn=pbppayorder&orderNo=" + orderNo + "&payAmount=" + amount + "&accountName=" + accountName + "";
                //loading层
                var mylayeruiwait = layer.load(1, {
                    shade: [0.5, '#ababab'] //0.1透明度的白色背景
                });
                $http.get(nghttp).success(function (response) {
                    layer.close(mylayeruiwait);
                    //setCookie('roomdiff', "", 1);
                    window.location.href = response;
                })
            }
            else {
                //把参数存入cookie
                setCookie('amount', amount, 1);
                setCookie('cnum', cnum, 1);
                setCookie('pnum', pnum, 1);
                setCookie('groupid', groupid, 1);
                setCookie('secureamount', secureamount, 1);
                //将跳回支付该产品的cookie
                //debugger
                setCookie('linkbackpay', 'true', 1);
                //跳转至登录页
                window.location.href = '#/app/user/login';
            }

        }
        $scope.paywaySelect = function ($event) {
            if ($event.target.parentNode.previousElementSibling.innerText == '支付宝')
                accountName = 'JJE_APP_CLIENT_ALI_WAP_PAY';
            else if ($event.target.parentNode.previousElementSibling.innerText == '微信支付')
                accountName = 'JJE_APP_WECHAT_PAY';
            else
                accountName = 'JJE_APP_CLIENT_ALI_WAP_PAY';
        }

    })

})

//取消订单控制器
.controller('cancelorderCtrl', function ($scope, $http) {
    nofollowfunc();
    // var ordercode = "1000160512000007";
    var nghttp = "../../ajax/apihandler.ashx?fn=cancelorder&ordercode=111";
    $http.get(nghttp).success(function (response) {
        // debugger

    });
})


//***************************以下公用方法***************************


function costdetail() {
    $(".black_overlay").css('display', 'block');
    $("#costdetail").css('display', 'block');
}

function costdetailnone() {
    $(".black_overlay").css('display', 'none');
    $("#costdetail").css('display', 'none');
}

var dayslength;
var counting = 0;
//更多团日期
function moredays() {
    // $(".linedetail .gengduoimg").css("display", "block");
    counting++;
    if (counting % 2 == 1) {
        var moha = dayslength / 4;
        if (moha <= 1) {
            //$(".linedetail .gengduoimg").css("display", "none");
            $(".linedetail .groupsheight").height(16);
            $(".linedetail .groupsinheight").height(16);
            $(".daysgroup2").slideToggle();
            return;
        }
        else {
            $(".linedetail .groupsheight").height(5 + 20 * moha);
            $(".linedetail .groupsinheight").height(5 + 20 * moha);
            $(".daysgroup2").slideToggle();
        }
    }
    else {
        $(".linedetail .groupsheight").height(16);
        $(".linedetail .groupsinheight").height(16);
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

//线路查询传参,前台点击事件
function searchLines() {
    // debugger
    //如果填充了则用填充的内容做筛选条件,若文字与空搜结果一致,按文字为标准来操作
    if ($("#divdesselect .searchtxt")[0].value) {
        var searchParam;
        if ($(".searchtxt")[1].value !== "")
            searchParam = $(".searchtxt")[1].value;
        else if ($(".searchtxt")[1].placeholder !== "")
            searchParam = $(".searchtxt")[1].placeholder;
        else
            searchParam = "";

        //if ($("#divdesselect .searchtxt")[0].value == $("#divdesselect .searchtxt")[0].placeholder &&  $("#divdesselect .searchtxturl")[0].value) {
        //    window.location.href = '#/app/linelists?search=' + searchParam;
        //}
        window.location.href = '#/app/linelists#search=' + searchParam;
    }
        //否则直接空搜
    else
        if ($("#divdesselect .searchtxturl")[0].value)
            window.location.href = $("#divdesselect .searchtxturl")[0].value;
        else {
            searchParam = $(".searchtxt")[1].placeholder;
            window.location.href = '#/app/linelists#search=' + searchParam;
        }
}

function removeclassblue() {
    $('.thirdCenter').removeClass("contentblue");
    $('.thirdCenter').removeClass("lineblue");
}
function addclassblue(q, i) {
    $('.thirdCenter:eq(' + q + ')').addClass("contentblue");
    $('.thirdCenter:eq(' + i + ')').addClass("lineblue");
}