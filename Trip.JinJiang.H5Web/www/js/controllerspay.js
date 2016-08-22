angular.module('starter.controllerspay', [])

//银联卡绑定页1
.controller('bindcardCtrl', function ($scope, $http) {
    //for (var i = 0; i < $("form input").length; i++) {
    //    var forminputname = $("form")[0][i].name;
    //    $("[name =" + forminputname + "]").val("");
    //}
    $("form").css({ "display": "none" });

    $scope.setFormData = function () {
        var memberid = $("#memberid").val();
        var card = $("#card").val();
        var nghttp = "../../ajax/payHandler.ashx?fn=bindcard&memberid=" + memberid + "&card=" + card + "";
        //loading层
        var mylayeruiwait = layer.load(1, {
            shade: [0.5, '#ababab'] //0.1透明度的白色背景
        });
        $http.get(nghttp).success(function (response) {
            find404admin(response);
            layer.close(mylayeruiwait);
            var x2js = new X2JS();
            var xmlText = response;
            var jsonObj = x2js.xml_str2json(xmlText);

            for (var i = 0; i < $("form input").length; i++) {
                var forminputname = $("form")[0][i].name;
                $("[name =" + forminputname + "]").val(jsonObj.unionBindModule[forminputname]);
            }
            document.getElementById("fuxxme").click();
        })
    }

})

