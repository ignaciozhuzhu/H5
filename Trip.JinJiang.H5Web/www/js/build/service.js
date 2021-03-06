﻿angular.module('starter.service', [])

//天数筛选框 服务
.service('filtbydaysev', function () {
    this.filtfunc = function ($http, $scope, days, agence, my2data, fun, filttype) {
        // if (!arguments[4]) fun = function () { };
        var daysArray = days.split("|");
        var agenceArray = agence.split("|");
        var nghttp = "../../ajax/apihandler.ashx?fn=getlinesbycategory&businessCategory=" + my2data.category + "&keyWord=" + my2data.keyWord + "";
        //loading层
        var mylayeruiwait = layer.load(1, {
            shade: [0.5, '#ababab'] //0.1透明度的白色背景
        });
        $http.get(nghttp).success(function (response) {
            var responsemine = response;
            var nghttp2 = "../../ajax/apihandler.ashx?fn=" + my2data.fn + "&category=" + my2data.category + ""
            $http.get(nghttp2).success(function (text) {
                layer.close(mylayeruiwait);
                var d = text;
                var arrayLinemm = new Array(0);

                //往搜索结果中添加合集(1)
                var pusharrayline = function () {
                    if (responsemine.lines[j].imageUrls[0] === undefined || responsemine.lines[j].imageUrls[0] === null ||
                        responsemine.lines[j].imageUrls[0].indexOf('http') < 0)
                        responsemine.lines[j].imageUrls[0] = 'http://img5.imgtn.bdimg.com/it/u=45254662,160915219&fm=21&gp=0.jpg';
                    responsemine.lines[j].lineCategory = d.lines[i].lineCategory;
                    arrayLinemm.push(responsemine.lines[j]);
                }

                for (var i = 0 ; i < d.lines.length; i++) {
                    for (var j = 0 ; j < responsemine.lines.length; j++) {
                        //如果是默认加载或者是天数筛选
                        if (filttype == "day" || !filttype) {
                            // 检查是否有lineid一致,是就push进去这条数据.显示所有天数的
                            if (!daysArray[1]) {
                                if (d.lines[i].lineId == responsemine.lines[j].lineId) {
                                    pusharrayline();
                                }
                            }
                                // 检查是否有lineid一致并且 天数一致的,是就push进去这条数据.
                            else {
                                if (d.lines[i].lineId == responsemine.lines[j].lineId && days.indexOf('|' + responsemine.lines[j].days + '|') > -1) {
                                    if (agenceArray[1] && agence.indexOf('|' + responsemine.lines[j].agency + '|') > -1)
                                        pusharrayline();
                                    else if (!agenceArray[1])
                                        pusharrayline();
                                }
                            }
                        }
                        else if (filttype == "ag") {
                            // 检查是否有lineid一致,是就push进去这条数据.显示所有天数的
                            if (!agenceArray[1]) {
                                if (d.lines[i].lineId == responsemine.lines[j].lineId) {
                                    pusharrayline();
                                }
                            }
                                // 检查是否有lineid一致并且 天数一致的,是就push进去这条数据.
                            else {
                                if (d.lines[i].lineId == responsemine.lines[j].lineId && agence.indexOf('|' + responsemine.lines[j].agency + '|') > -1) {
                                    if (daysArray[1] && (days.indexOf('|' + responsemine.lines[j].days + '|') > -1))
                                        pusharrayline();
                                    else if (!daysArray[1])
                                        pusharrayline();
                                }
                            }
                        }
                    }
                }
                //往搜索结果中添加合集(2)
                $scope.agencies = responsemine.agencies;
                $scope.linelists = arrayLinemm.sort(objectorderby("point"));
                fun(arrayLinemm.length);
            })

        });
    }
    //获取所有天数列表 方法
    this.getdays = function ($http, $scope, funcallback, my2data) {
        var mylayeruiwait = layer.load(1, { shade: [0.5, '#ababab'] });
        var nghttp = "../../ajax/apihandler.ashx?fn=getlinesbycategory&businessCategory=" + my2data.category + "&keyWord=" + my2data.keyWord + "";
        $http.get(nghttp).success(function (response) {
            layer.close(mylayeruiwait);
            funcallback(response.days.sort(sortbyday0));
        })
    }
    //获取所有旅行社列表 方法
    this.getagencies = function ($http, $scope, funcallback, my2data) {
        var mylayeruiwait = layer.load(1, { shade: [0.5, '#ababab'] });
        var nghttp = "../../ajax/apihandler.ashx?fn=getlinesbycategory&businessCategory=" + my2data.category + "&keyWord=" + my2data.keyWord + "";
        $http.get(nghttp).success(function (response) {
            layer.close(mylayeruiwait);
            funcallback(response.agencies);
        })
    }

})



//获取帐号的绑定银行卡信息
.service('getbindquerysev', function () {
    this.bindquetyfunc = function ($http,funcallback) {
        var mcMemberCode = getCookie('mcMemberCode');
        //判断是否已经绑定卡
        var nghttp = "../../ajax/payhandler.ashx?fn=bindcardquery&memberid=" + mcMemberCode + "";
        //loading层
        var mylayeruiwait = layer.load(1, {
            shade: [0.5, '#ababab'] //0.1透明度的白色背景
        });
        $http.get(nghttp).success(function (response) {
            layer.close(mylayeruiwait);
            if (response) {
                var jsonObj = new X2JS().xml_str2json(response);
                funcallback(jsonObj);
            }
        })
    }
})