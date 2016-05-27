angular.module('starter.controllers', [])

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
                //   debugger
                var d = eval("(" + text + ")");
                var arrayLinemm = new Array(0);
                for (var i = 0 ; i < d.rows.length; i++) {
                    for (var j = 0 ; j < response.lines.length; j++) {
                        if (d.rows[i].lineId == response.lines[j].lineId) {
                            //往搜索结果中添加合集(1)
                            if (response.lines[j].imageUrls[0] === undefined || response.lines[j].imageUrls[0] === null || response.lines[j].imageUrls[0].indexOf('http') < 0)
                                response.lines[j].imageUrls[0] = 'http://img5.imgtn.bdimg.com/it/u=45254662,160915219&fm=21&gp=0.jpg';
                            response.lines[j].lineCategory = d.rows[i].lineCategory;
                            arrayLinemm.push(response.lines[j]);
                        }
                    }
                }

                //for (var i = 0; i < response.lines.length; i++) {
                //    if (response.lines[i].imageUrls[0] === undefined || response.lines[i].imageUrls[0] === null || response.lines[i].imageUrls[0].indexOf('http') < 0)
                //        response.lines[i].imageUrls[0] = 'http://img5.imgtn.bdimg.com/it/u=45254662,160915219&fm=21&gp=0.jpg'
                //    //往搜索结果中添加合集(1)
                //    if (response.lines[i].lineCategory == searchParam)
                //        arrayLinemm.push(response.lines[i])
                //}
                //往搜索结果中添加合集(2)
                $scope.linelists = arrayLinemm;
                $scope.agencies = response.agencies;

            }
        })

    });
})


//主页控制器
.controller('indexCtrl', function ($scope, $http, $ionicScrollDelegate) {

    //实现自带搜索按钮跳转并失去焦点关闭键盘.
    $(function () {
        $('#searchtxt').bind('keypress', function (event) {
            if (event.keyCode == "13") {
                searchLines(this);
                document.activeElement.blur();
            }
        });
    });

    //滚动,下拉出电话
    $scope.getScrollPosition = function () {
        var scrolltop = $ionicScrollDelegate.$getByHandle('indexDelegate').getScrollPosition().top;
        $('#teledown').css('top', scrolltop + document.documentElement.childNodes[2].scrollHeight - 170);
    }



    //分类图标
    var nghttpcategory = "../../ajax/apihandler.ashx?fn=getlinecategorys&status=true";
    $http.get(nghttpcategory).success(function (response) {
        $scope.linecategorys = response.ds;
        if (response.ds.length / 5 > 0)
            $('.linecatebox').height(80);
        if (response.ds.length / 5 > 1)
            $('.linecatebox').height(160);
        if (response.ds.length / 5 > 2)
            $('.linecatebox').height(240);
        if (response.ds.length / 5 > 3)
            $('.linecatebox').height(320);
    })


    var nghttp = "../../ajax/apihandler.ashx?fn=getlinespromotion";
    $http.get(nghttp).success(function (response) {
        for (var i = 0; i < 8; i++) {
            if (response.lines[i].imageUrls[0] === undefined || response.lines[i].imageUrls[0] === null || response.lines[i].imageUrls[0].indexOf('http') < 0)
                response.lines[i].imageUrls[0] = 'http://img5.imgtn.bdimg.com/it/u=45254662,160915219&fm=21&gp=0.jpg'
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
        //轮播图
        var nghttpgg = "../../ajax/bannerImgHandler.ashx?fn=getbannerimglist&status=true";
        $http.get(nghttpgg).success(function (response) {
            // debugger
            for (var i = 0; i < 4; i++) { //response.ds.length
                $('#full-width-slider').append('<div class="rsContent"><img class="rsImg" src=' + response.ds[i].imgUrl + ' /></div>');
            }
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
        })



    });

})

//线路详情控制器
.controller('lineDetailCtrl', function ($scope, $http, $sce) {
    var url = location.href;
    var lineid = url.substring(url.lastIndexOf('/') + 1, url.length);
    $('#ordernow').attr('href', '#/app/indexdate/' + lineid);
    var nghttp = "../../ajax/apihandler.ashx?fn=getlinedetail&lineid=" + lineid + "";
    $http.get(nghttp).success(function (response) {
        //debugger
        //团框初始高度
        $("#groupsheight").height(16);
        $("#groupsinheight").height(16);

        //行程
        if (response.line === null) {
            alert('此线路暂无详细数据!');
            window.location.href = "#/app/index";
            return;
        }
        $scope.linedetails = response.line;
        $scope.journeys = response.line.journeys.sort(sortbydayNumber);
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

        //取团号
        $scope.groupcode = '团号:' + response.line.groups[0].groupCode.substring(response.line.groups[0].groupCode.length - 14, response.line.groups[0].groupCode.length);

        //取线路旅游类型
        //debugger
        //$scope.linecategory = response.linecategory;

        //取价格
        $scope.price = response.minPrice;
        //产品经理推荐
        $scope.recommend = response.line.recommend;

        $('#idline').show();
        $('#idfeature').hide();
        $('#idexpense').hide();

        $('.thirdCenter:eq(0)').addClass("contentblue");
        $('.thirdCenter:eq(3)').addClass("lineblue");
    });
    $scope.$on("$ionicView.loaded", function () {
    });

})


//日期选择控制器
.controller('indexdateCtrl', function ($scope, $http) {
    $scope.$on("$ionicView.loaded", function () {
        $('.spinner').spinner({});
        $('.spinner2').spinner2({});
        var url = location.href;
        var lineid = url.substring(url.lastIndexOf('/') + 1, url.length);
        var nghttp = "../../ajax/apihandler.ashx?fn=getlinedetail&lineid=" + lineid + "";
        $http.get(nghttp).success(function (response) {
            //debugger
            intoCalendarTime();
            adn = 1;
            crn = 0;
            $("#sp01").click(function () {
                adn = this.children[0].children[1].value;
                $('#nextpick').attr('href', nextpickhref + '/' + adn + '/' + crn);
            });
            $("#sp02").click(function () {
                crn = this.children[0].children[1].value;
                $('#nextpick').attr('href', nextpickhref + '/' + adn + '/' + crn);
            });

            function intoCalendarTime() {
                data = "[";
                for (var i = 0; i < response.line.groups.length; i++) {
                    var date1 = FormatDateYear(response.line.groups[i].departDate);
                    var groupid = response.line.groups[i].id;
                    for (var j = 0; j < response.line.groups[i].prices.length; j++) {
                        if (response.line.groups[i].prices[j].offerType == '基本价')
                            minprice = response.line.groups[i].prices[j].salePrice;
                    }
                    var price1 = "¥" + minprice;
                    data += '{"Date":"' + date1 + '","Price":"' + price1 + '","groupid":"' + groupid + '"},';
                }
                data += "]";
                pickerEvent.setPriceArr(eval("(" + data + ")"));
                pickerEvent.Init("calendar");
            }
        })
    });
})

//选择资源控制器2
.controller('pickresourceCtrl2', function ($scope, $http) {
    var cnum = getpbyurl(1);
    var pnum = getpbyurl(2);
    var groupid = getpbyurl(3);

    $scope.pnum = pnum;
    $('.spinner').spinner({});
    var amount = 0;
    var secureamount = 0;
    $('#secureamount').empty().append('0');

    var nghttp = "../../ajax/apihandler.ashx?fn=queryrealtimerefresh&groupid=" + groupid + "";
    $http.get(nghttp).success(function (response) {
        var cprice;
        var dprice;
        for (var j = 0; j < response.prices.length; j++) {
            if (response.prices[j].offerType == '基本价')
                minprice = response.prices[j].salePrice;
            if (response.prices[j].offerType == '儿童价')
                cprice = response.prices[j].salePrice;
            if (response.prices[j].offerType == '单房差')
                dprice = response.prices[j].salePrice;
        }
        $scope.minprice = minprice;
        if (cprice > 0) {
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
            roomdiff = dprice * roomdiffp1 + cprice * roomdiffp2 + samount1 * pnum + samount2 * pnum;
            amountall = minprice * pnum + roomdiff;
            $('#amount').empty().append(amountall);
            $('#amountct').empty().append(minprice * pnum);
            $('#nextfill').attr('href', '#/app/fillorder/' + secureamount + '/' + groupid + '/' + pnum + '/' + cnum + '/' + amountall);
        }
        subamount();
    })

})

//填写订单控制器
.controller('fillorderCtrl', function ($scope, $http) {
    $.blockUI();

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
    $scope.amount = amount;
    $scope.pnum = pnum;


    var nghttp = "../../ajax/apihandler.ashx?fn=queryrealtimerefresh&groupid=" + groupid + "";
    $http.get(nghttp).success(function (response) {
        $scope.lineTitle = response.lineTitle;
        $scope.date = FormatDateYear(response.departDate);
        for (var j = 0; j < response.prices.length; j++) {
            if (response.prices[j].offerType == '基本价')
                priceid = response.prices[j].id;
        }
        Discount = response.onlineDiscount;

        //出现的出行人行数.
        var arrayGuests = new Array(0);
        for (var i = 0; i < pnum; i++) {
            arrayGuests.push(i);
        }
        $scope.guests = arrayGuests;

        $.unblockUI();
    })

    var guestsarr = new Array(0);
    $scope.createorder = function () {
        $.blockUI({
            message: '<h6>正在加载,请稍后...</h6>'
        });
        var ConnectName = $scope.Connect.name;
        var ConnectMobile = $scope.Connect.mobile;
        var ConnectEmail = $scope.Connect.email;

        $('.inname:first')[0].value = ConnectName;

        function CGuest(category, name) {
            this.category = category;
            this.name = name;
        }
        var p = new CGuest();
        p = new CGuest('ADULT', 'guest1');
        guestsarr.push(p);
        p = new CGuest('ADULT', 'guest2');
        guestsarr.push(p);
        p = new CGuest('ADULT', 'guest3');
        guestsarr.push(p);
        p = new CGuest('ADULT', 'guest4');
        guestsarr.push(p);
        p = new CGuest('ADULT', 'guest5');
        guestsarr.push(p);

        var gueststring = "";
        for (var i = 0; i < pnum; i++) {
            gueststring += "{\"category\":\"" + guestsarr[i].category + "\",\"name\":\"" + guestsarr[i].name + "\"},";
        }
        gueststring = gueststring.substring(0, gueststring.length - 1);

        var discountAmount = Math.floor(amount * (1 - Discount)) * pnum;
        //动态成人数.
        //debugger
        json = "{\"adultNum\":" + pnum + ",\"amount\":" + amount + ",\"channel\":\"E_BUSINESS_PLATFORM\",\"childNum\":0,\"contact\":{\"mobile\":\"" + ConnectMobile + "\",\"name\":\"" + ConnectName + "\",\"email\":\"" + ConnectEmail + "\"},\"couponAmount\":0,\"groupId\":" + groupid + ",\"guests\":[" + gueststring + "],\"mcMemberCode\":\"1231234\",\"cardNo\":\"1231234\",\"onLinePay\":true,\"receivables\":[{\"copies\":" + pnum + ",\"discountAmount\":" + discountAmount + ",\"priceId\":" + priceid + ",\"singlePrice\":" + amount / pnum + "}],\"scorePay\":false}";
        //2人
        // json = "{\"adultNum\":" + pnum + ",\"amount\":" + amount + ",\"channel\":\"E_BUSINESS_PLATFORM\",\"childNum\":0,\"contact\":{\"mobile\":\"" + ConnectMobile + "\",\"name\":\"" + ConnectName + "\",\"email\":\"" + ConnectEmail + "\"},\"couponAmount\":0,\"groupId\":" + groupid + ",\"guests\":[{\"category\":\"" + guestsarr[0].category + "\",\"name\":\"" + guestsarr[0].name + "\"},{\"category\":\"" + guestsarr[1].category + "\",\"name\":\"" + guestsarr[1].name + "\"}],\"mcMemberCode\":\"1231234\",\"cardNo\":\"1231234\",\"onLinePay\":true,\"receivables\":[{\"copies\":" + pnum + ",\"discountAmount\":" + discountAmount + ",\"priceId\":" + priceid + ",\"singlePrice\":" + amount / pnum + "}],\"scorePay\":false}";
        //1人
        //json = "{\"adultNum\":1,\"amount\":" + amount + ",\"channel\":\"E_BUSINESS_PLATFORM\",\"childNum\":0,\"contact\":{\"mobile\":\"" + ConnectMobile + "\",\"name\":\"" + ConnectName + "\",\"email\":\"" + ConnectEmail + "\"},\"couponAmount\":0,\"groupId\":" + groupid + ",\"guests\":[{\"category\":\"" + guestsarr[0].category + "\",\"name\":\"" + guestsarr[0].name + "\"}],\"mcMemberCode\":\"1231234\",\"cardNo\":\"1231234\",\"onLinePay\":true,\"receivables\":[{\"copies\":1,\"discountAmount\":" + discountAmount + ",\"priceId\":" + priceid + ",\"singlePrice\":" + amount + "}],\"scorePay\":false}";



        $.ajax({
            url: "../../ajax/apihandler.ashx?fn=createorder&json=" + json + "",
            type: "post",
            success: function (text) {
                //出行人只显示成人,有几人就设置几个cookiename
                for (var i = 0; i < pnum; i++) {
                    setCookie('inname' + i, $('.inname')[i].value, 1);
                }
                $.unblockUI();
                var d = eval("(" + text + ")");
                //debugger
                setCookie('orderNo', d.orderNo, 1);
                window.location.href = '#/app/payway/' + secureamount + '/' + groupid + '/' + pnum + '/' + cnum + '/' + amount;
            }
        });
    }

})

//支付方式控制器
.controller('paywayCtrl', function ($scope, $http) {
    $.blockUI({
        message: '<h6>正在加载,请稍后...</h6>'
    });
    //清除登录用户cookie
    //setCookie('mcMemberCode','',1);
    var amount = getpbyurl(1);
    var cnum = getpbyurl(2);
    var pnum = getpbyurl(3);
    var groupid = getpbyurl(4);
    var secureamount = getpbyurl(5);
    $scope.amount = amount;
    var nghttp = "../../ajax/apihandler.ashx?fn=queryrealtimerefresh&groupid=" + groupid + "";
    $http.get(nghttp).success(function (response) {
        //debugger
        var minprice;
        for (var j = 0; j < response.prices.length; j++) {
            if (response.prices[j].offerType == '基本价')
                minprice = response.prices[j].salePrice;
        }
        $scope.minprice = minprice;
        $scope.secureamount = secureamount;
        $scope.groupCode = response.groupCode;
        $scope.lineTitle = response.lineTitle;
        $scope.pnum = pnum;
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

        $.unblockUI();

        var accountName = 'INNS_APP_CLIENT_ALI_WAP_PAY';
        $scope.pay = function () {
            //首先做身份认证,判断是否已经登录,没有帐号的客户先注册.
            var mcMemberCode = getCookie('mcMemberCode');
            if (mcMemberCode != "" && mcMemberCode != undefined && mcMemberCode != null) {
                //其次再是发起付款
                var orderNo = getCookie('orderNo');
                var nghttp = "../../ajax/apihandler.ashx?fn=pbppayorder&orderNo=" + orderNo + "&payAmount=" + amount + "&accountName=" + accountName + "";
                $http.get(nghttp).success(function (response) {
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
            //var nghttpAuth = "../../ajax/apihandler.ashx?fn=pbppayorder&orderNo=" + orderNo + "&payAmount=" + amount + "&accountName=" + accountName + "";
            //$http.get(nghttpAuth).success(function (response) {


            //})

            
        }
        $scope.paywaySelect = function ($event) {
            if ($event.target.parentNode.previousElementSibling.innerText == '支付宝')
                accountName = 'INNS_APP_CLIENT_ALI_WAP_PAY';
        }

    })

})

//取消订单控制器
.controller('cancelorderCtrl', function ($scope, $http) {
   // var ordercode = "1000160512000007";
    var nghttp = "../../ajax/apihandler.ashx?fn=cancelorder&ordercode=111";
    $http.get(nghttp).success(function (response) {
        // debugger

    });
})

.controller('PlaylistCtrl', function ($scope, $stateParams) {
});

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
    counting++;
    if (counting % 2 == 1) {
        var moha = dayslength / 4;
        $("#groupsheight").height(5 + 20 * moha);
        $("#groupsinheight").height(5 + 20 * moha);
        $(".daysgroup2").slideToggle();

    }
    else {
        $("#groupsheight").height(16);
        $("#groupsinheight").height(16);
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
//城市选择框
function desSelect() {
    $('#divcontent').hide();
    $('#bartitle').hide();
    $('#divdesselect').show();

    $('#divcityselect').click(function (event) {
        if (event.target.nodeName === 'SPAN') {
            alert(event.target.innerText);
            //$('#divcontent').show();
            //$('#bartitle').show();
            //$('#divcityselect').hide();
            //$("#beginProtxt")[0].placeholder = event.target.innerText + '出发';
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

function removeclassblue() {
    $('.thirdCenter').removeClass("contentblue");
    $('.thirdCenter').removeClass("lineblue");
}
function addclassblue(q, i) {
    $('.thirdCenter:eq(' + q + ')').addClass("contentblue");
    $('.thirdCenter:eq(' + i + ')').addClass("lineblue");
}

function lineCl() {
    $('#idfeature').hide();
    $('#idline').show();
    $('#idexpense').hide();
    removeclassblue();
    addclassblue(0, 3);
}

function featureCl() {
    $('#idfeature').show();
    $('#idline').hide();
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


function sortbydepartDate(a, b) {
    return a.departDate - b.departDate;
}
function sortbydayNumber(a, b) {
    return a.dayNumber - b.dayNumber;
}

//获取链接参数
function getpbyurl(typei) {
    function subs(href) {
        return href.substring(0, href.lastIndexOf('/'));
    }
    var localhref = location.href;
    var geturl = localhref;
    switch (typei) {
        case 1:
            break;
        case 2:
            geturl = subs(geturl);
            break;
        case 3:
            geturl = subs(subs(geturl));
            break;
        case 4:
            geturl = subs(subs(subs(geturl)));
            break;
        case 5:
            geturl = subs(subs(subs(subs(geturl))));
            break;
        case 6:
            geturl = subs(subs(subs(subs(subs(geturl)))));
            break;
    }
    return geturl.substring(geturl.lastIndexOf('/') + 1, geturl.length);
}
