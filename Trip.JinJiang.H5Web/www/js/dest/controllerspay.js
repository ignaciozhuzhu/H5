angular.module("starter.controllerspay",[]).controller("bindcardCtrl",["$scope","$http",function(a,b){var c=getCookie("mcMemberCode");c||layermyui("请先去登录."),$("form").css({display:"none"}),$("#memberid").val(c),a.setFormData=function(){var a=$("#memberid").val(),c=$("#card").val(),d="../../ajax/payHandler.ashx?fn=bindcard&memberid="+a+"&card="+c,e=layer.load(1,{shade:[.5,"#ababab"]});b.get(d).success(function(a){find404admin(a),layer.close(e);for(var b=new X2JS,c=a,d=b.xml_str2json(c),f=0;f<$("form input").length;f++){var g=$("form")[0][f].name;$("[name ="+g+"]").val(d.unionBindModule[g])}document.getElementById("fuxxme").click()})}}]).controller("smsCtrl",["$scope","$http","getbindquerysev",function(a,b,c){var d=getpbyurl(1),e=getpbyurl(2);a.changecard=function(){window.location.href="#/app/card/bind/"+d,location.reload()};var f=function(a){return a.userToken?void $(".bindbox #card").val(a.userToken.preCardNo+"*************"+a.userToken.card):void 0};c.bindquetyfunc(b,f),a.paybyunion=function(){var c=a.txnTime,e=$("#smscode").val(),f=$("#card").val(),g="JJE_APP_UNION_PAY",h=a.payAmount;if(!e)return void layermyui("请输入短信验证码.");if(!f)return void layermyui("请输入卡号.");var i="../../ajax/apihandler.ashx?fn=pbppayorder&orderNo="+d+"&payAmount="+h+"&accountName="+g+"&txnTime="+c+"&smscode="+e,j=layer.load(1,{shade:[.5,"#ababab"]});b.get(i).success(function(a){layer.close(j);var b=new X2JS,c=a,d=b.xml_str2json(c);return a?void layermyui(a):""==a?(layermyui("支付完成"),void setTimeout(function(){window.location.href="#/app/card/paysuccess"},2e3)):d.errorMessage?void layermyui(d.errorMessage.message):void 0})},a.sends={checked:1,send:function(){function b(){return 0==f?(clearInterval(g),$(".div-phone a").removeClass("send0").html("发送验证码"),sends.checked=1,!0):(sends.checked=0,$(".div-phone a").html(f+"秒后再次发送"),f--,!1)}if(1==sends.checked){var c=getCookie("mcMemberCode");c||layermyui("请先去登录.");var f=30;$(".div-phone span").remove(),$(".div-phone a").addClass("send0"),b();var g=setInterval(b,1e3),h=gettimenow();a.txnTime=h,a.payAmount=e,$.ajax({url:"../../ajax/payHandler.ashx?fn=sms&memberid="+c+"&orderNo="+d+"&price="+e+"&txnTime="+h,type:"post",success:function(a){}})}}}}]).controller("paysuccessCtrl",["$scope","$http",function(a,b){a.goindex=function(){window.location.href="#/app/index"},a.goorder=function(){window.location.href="#/app/user/myorder"}}]);