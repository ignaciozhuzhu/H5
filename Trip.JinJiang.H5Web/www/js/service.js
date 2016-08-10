angular.module('starter.service', [])

//
//.service('getdriverinfo_back', function () {
//    this.myFunc = function ($http, $scope, days) {
//        var nghttp = "../../ajax/apihandler.ashx?fn=getlinesbycategory";
//        //loading层
//        var mylayeruiwait = layer.load(1, {
//            shade: [0.5, '#ababab'] //0.1透明度的白色背景
//        });
//        $http.get(nghttp).success(function (response) {
//            //debugger
//            var my2data = { fn: "getlinecategoriecrm2", category: "outbound" };
//            var responsemine = response;
//            $.ajax({
//                url: "../../ajax/apihandler.ashx",
//                data: my2data,
//                type: "post",
//                success: function (text) {
//                    //debugger
//                    layer.close(mylayeruiwait);
//                    var d = eval("(" + text + ")");
//                    var arrayLinemm = new Array(0);
//                    for (var i = 0 ; i < d.rows.length; i++) {
//                        for (var j = 0 ; j < responsemine.lines.length; j++) {
//                            //debugger
//                            if (d.rows[i].lineId == responsemine.lines[j].lineId && responsemine.lines[j].days == days) {
//                                //往搜索结果中添加合集(1)
//                                if (responsemine.lines[j].imageUrls[0] === undefined || responsemine.lines[j].imageUrls[0] === null || responsemine.lines[j].imageUrls[0].indexOf('http') < 0)
//                                    responsemine.lines[j].imageUrls[0] = 'http://img5.imgtn.bdimg.com/it/u=45254662,160915219&fm=21&gp=0.jpg';
//                                responsemine.lines[j].lineCategory = d.rows[i].lineCategory;
//                                arrayLinemm.push(responsemine.lines[j]);
//                            }
//                        }
//                    }
//                    //往搜索结果中添加合集(2)
//                    //  debugger
//                    $scope.agencies = responsemine.agencies;
//                    $scope.linelists = arrayLinemm.sort(objectorderby("point"));
//                    $scope.open = !$scope.open;
//                }
//            })

//        });
//    }
//})

//天数筛选框 服务
.service('getdriverinfo', function () {
   // var a;
    this.myFunc = function ($http, $scope, days, my2data, fun) {
        if (!arguments[4]) fun = function () { };
        var nghttp = "../../ajax/apihandler.ashx?fn=getlinesbycategory";
        //loading层
        var mylayeruiwait = layer.load(1, {
            shade: [0.5, '#ababab'] //0.1透明度的白色背景
        });
        $http.get(nghttp).success(function (response) {
            //var my2data = { fn: "getlinecategoriecrm2", category: "outbound" };
            var responsemine = response;
            var nghttp2 = "../../ajax/apihandler.ashx?fn=" + my2data.fn + "&category=" + my2data.category + ""
            $http.get(nghttp2).success(function (text) {
                layer.close(mylayeruiwait);
                var d = text;
                var arrayLinemm = new Array(0);
                for (var i = 0 ; i < d.rows.length; i++) {
                    for (var j = 0 ; j < responsemine.lines.length; j++) {
                        //debugger
                        if (d.rows[i].lineId == responsemine.lines[j].lineId && responsemine.lines[j].days == days) {
                            //往搜索结果中添加合集(1)
                            if (responsemine.lines[j].imageUrls[0] === undefined || responsemine.lines[j].imageUrls[0] === null || responsemine.lines[j].imageUrls[0].indexOf('http') < 0)
                                responsemine.lines[j].imageUrls[0] = 'http://img5.imgtn.bdimg.com/it/u=45254662,160915219&fm=21&gp=0.jpg';
                            responsemine.lines[j].lineCategory = d.rows[i].lineCategory;
                            arrayLinemm.push(responsemine.lines[j]);
                        }
                    }
                    //往搜索结果中添加合集(2)
                    //  debugger
                    $scope.agencies = responsemine.agencies;
                    $scope.linelists = arrayLinemm.sort(objectorderby("point"));
                    fun();

                    // debugger
                }
            })

        });
    }
})
