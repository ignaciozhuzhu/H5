function moredays(){if(counting++,counting%2==1){var a=dayslength/4;if(1>=a)return $(".linedetail .groupsheight").height(16),$(".linedetail .groupsinheight").height(16),void $(".daysgroup2").slideToggle();$(".linedetail .groupsheight").height(5+20*a),$(".linedetail .groupsinheight").height(5+20*a),$(".daysgroup2").slideToggle()}else $(".linedetail .groupsheight").height(16),$(".linedetail .groupsinheight").height(16),$(".daysgroup2").slideToggle()}function citySelect(){$("#divcontent").hide(),$("#bartitle").hide(),$("#divcityselect").show(),$("#divcityselect").click(function(a){"SPAN"===a.target.nodeName&&($("#divcontent").show(),$("#bartitle").show(),$("#divcityselect").hide(),$("#beginProtxt")[0].placeholder=a.target.innerText+"出发")})}function searchLines(){var a;$("#divdesselect .searchtxt")[0].value?(a=""!==$(".searchtxt")[1].value?$(".searchtxt")[1].value:""!==$(".searchtxt")[1].placeholder?$(".searchtxt")[1].placeholder:"",window.location.href="#/app/linelists#search="+a):$("#divdesselect .searchtxturl")[0].value?window.location.href=$("#divdesselect .searchtxturl")[0].value:(a=$(".searchtxt")[1].placeholder,window.location.href="#/app/linelists#search="+a)}function removeclassblue(){}function addclassblue(a,b){$(".forthCenter:eq("+a+")").addClass("contentblue"),$(".forthCenter:eq("+b+")").addClass("lineblue")}angular.module("starter.controllers",[]).controller("AppCtrl",["$scope","$ionicModal","$timeout","$ionicScrollDelegate",function(a,b,c,d){a.getScrollPosition=function(){var a=d.$getByHandle("indexDelegate").getScrollPosition().top;$(".teledown").css("top",a+document.documentElement.childNodes[2].scrollHeight-120)},a.loginData={},a.closeLogin=function(){a.modal.hide()},a.login=function(){a.modal.show()},a.doLogin=function(){console.log("Doing login",a.loginData),c(function(){a.closeLogin()},1e3)}}]).controller("LinelistsCtrl",["$scope","$http","filtbydaysev",function(a,b,c){followfunc();var d=decodeURI(getpbyurl2(1));a.listhistorygoback=function(){window.location.href="#/app/index",setTimeout(function(){$("#indexheadback").show(),$("#divcontent").hide(),$("#bartitle").hide(),$("#divdesselect").show(),$("h1.title div").length>0||$("h1.title").empty().append("线路搜索")},300)},tkdfunc(""+d+"旅游线路_"+d+"线路报价_"+d+"旅游攻略-锦江旅游",""+d+"旅游线路,"+d+"旅游报价,"+d+"旅游攻略,锦江旅游","上海到"+d+"旅游线路，锦江旅游提供上海到"+d+"旅游线路价格,汇集上海旅行社品牌旅游线路,为您提供优质的旅游服务。"),a.pagences="|",a.pdays="|";var e={fn:"getlinesbycategory",keyWord:d};a.my2data=e;var f=window.innerWidth,g=f/3*2;$(".dropdown-menu, .dropdown-menu-form").eq(0).css({left:-g/2,width:f}),$(".dropdown-menu, .dropdown-menu-form").eq(2).css({left:-g/2,width:f}),$(".dropdown-toggle").css({width:g/2}),a.example1data0=[],a.example1model0=[];var h=function(b){for(var c=0;c<b.length;c++)a.example1data0[c]={id:b[c].name,label:b[c].name}};c.getagencies(b,a,h,e),$(".dropdown-menu, .dropdown-menu-form").eq(1).css({left:-g,width:f}),$(".dropdown-menu, .dropdown-menu-form").eq(3).css({left:-g/2,width:f}),a.example1data=[],a.example1model=[];var i=function(b){for(var c=0;c<b.length;c++)a.example1data[c]={id:b[c].name,label:b[c].name+"天"}};c.getdays(b,a,i,e);var j=d;a.mylineCategoryName=d;var k=function(a){$(".title").empty().append(j+"<div>共"+a+"条</div>")};c.filtfunc(b,a,"|","|",e,k),$(".linelistback").css("display","none"),a.listent=function(){setCookie("ent2detail","search="+d,1)}}]).controller("LinelistsCtrl2",["$scope","$http","filtbydaysev",function(a,b,c){followfunc(),a.pagences="|",a.pdays="|";var d=location.href,e=d.substring(d.lastIndexOf("/")+1,d.length),f="";f="nearby"===e?{fn:"getnearby"}:"domestic"===e||"outbound"===e||"inbound"===e?{fn:"getlinecategoriecrm2",category:e}:{fn:"getlinecategoriecrm",category:e},a.my2data=f;var g=replaceCategory(e);a.mylineCategoryName=g,tkdfunc(""+g+"旅游线路_"+g+"线路报价_"+g+"旅游攻略-锦江旅游",""+g+"旅游线路,"+g+"旅游报价,"+g+"旅游攻略,锦江旅游","锦江旅游提供包括上海"+g+"、"+g+"线路报价，出国旅游景点等多样化旅行服务。锦江旅游是中国最优质的旅游线路和自助游一站式服务提供商。");var h=window.innerWidth,i=h/3*2;$(".dropdown-menu, .dropdown-menu-form").eq(0).css({left:-i/2,width:h}),$(".dropdown-menu, .dropdown-menu-form").eq(2).css({left:-i/2,width:h}),$(".dropdown-toggle").css({width:i/2}),a.example1data0=[],a.example1model0=[];var j=function(b){for(var c=0;c<b.length;c++)a.example1data0[c]={id:b[c].name,label:b[c].name}};c.getagencies(b,a,j,f),$(".dropdown-menu, .dropdown-menu-form").eq(1).css({left:-i,width:h}),$(".dropdown-menu, .dropdown-menu-form").eq(3).css({left:-i,width:h}),a.example1data=[],a.example1model=[];var k=function(b){for(var c=0;c<b.length;c++)a.example1data[c]={id:b[c].name,label:b[c].name+"天"}};c.getdays(b,a,k,f);var l=function(a){$(".title").empty().append(g+"<div>共"+a+"条</div>")};c.filtfunc(b,a,"|","|",f,l),a.listent=function(){setCookie("ent2detail",e,1)},a.listhistorygoback=function(){window.location.href="#/app/index",location.reload()},$(".linelistback").css("display","block"),a.listhistorygoback2=function(){window.location.href="#/app/index",setTimeout(function(){$("#indexheadback").show(),$("#divcontent").hide(),$("#bartitle").hide(),$("#divdesselect").show(),$("h1.title div").length>0||$("h1.title").empty().append("线路搜索")},300)},a.change1=function(b){a.orderByDay=""},a.change2=function(b){a.orderByPrice=""}}]).controller("indexCtrl",["$scope","$http","$ionicScrollDelegate",function(a,b,c){followfunc(),tkdfunc("锦江旅游度假_旅行社旅游线路_旅游景点攻略-锦江旅游","锦江旅游,旅游网站,出境游,境内游,周边游,自由行,锦江国际","锦江旅游提供包括旅游线路咨询预订,旅游酒店预订,旅游线路查询，旅游车辆租赁等多样化旅行服务。"),a.tomyinfo=function(){window.location.href="#/app/user/myinfo"},$(function(){$(".searchtxt").bind("keypress",function(a){"13"==a.keyCode&&(searchLines(this),document.activeElement.blur())})}),a.goback=function(){$("#divcontent").show(),$("#bartitle").show(),$("#divdesselect").hide(),$("#indexheadback").hide()},a.searchlines=function(){searchLines()};var d=navigator.userAgent.toLowerCase();navigator.userAgent.indexOf("UCBrowser")>-1?($(".imgh").css({"max-height":"70px","max-width":"70px"}),$(".imgo").css({"max-height":"45px","max-width":"45px","margin-top":"10%"}),$(".borderlefty").css({"margin-left":"-5%"})):($(".borderlefty").css({"margin-left":"3%"}),$(".imgo").css({"max-height":"45px","max-width":"45px"}),$(".borderlefty").css({width:"70%"})),/iphone|ipad|ipod/.test(d)&&navigator.userAgent.indexOf("UCBrowser")>-1&&($(".imgh").css({"max-height":"35px","max-width":"35px"}),$(".imgo").css({"max-height":"45px","max-width":"45px","margin-top":"10%"}),$(".borderlefty").css({"margin-left":"5%"})),a.getScrollPosition=function(){var a=c.$getByHandle("indexDelegate").getScrollPosition().top;$(".teledown").css("top",a+document.documentElement.childNodes[2].scrollHeight-120)};var e="../../ajax/apihandler.ashx?fn=getlinecategorys&status=true&pattern=S1";b.get(e).success(function(b){a.linecategorys=b.ds});var f="../../../ajax/lineCategoryHandler.ashx?fn=getpatternss2";b.get(f).success(function(b){a.linecategorys2=b.ds});var g="../../ajax/apihandler.ashx?fn=getlinecategorys2&status=true&pattern=S2";b.get(g).success(function(c){for(var d,e="",f=0;f<c.ds.length;f++)d=c.ds[f].imageUrls,e+=c.ds[f].lineId+",",d.indexOf("|")>0?d=d.substring(0,d.indexOf("|")):c.ds[f].imageUrls="","../../../modules/img/0"==c.ds[f].imgUrl&&(c.ds[f].imgUrl="images/error.jpg");e=e.substr(0,e.length-1),a.linecategorys2detail=c.ds;var g="../../ajax/apihandler.ashx?fn=getlinesprice&linearr="+e;b.get(g).success(function(b){for(var c=0;c<a.linecategorys2detail.length;c++)for(var d=0;d<b.length;d++)a.linecategorys2detail[c].lineId==b[d].id&&(a.linecategorys2detail[c].minPrice=b[d].minPrice)})}),a.indexent=function(){setCookie("ent2detail","index",1)},setCookie("ent2detail","index",1),a.desSelect=function(){$("#indexheadback").show(),$("#divcontent").hide(),$("#bartitle").hide(),$("#divdesselect").show(),$("#divdesselect").click(function(a){"searchDest"===a.target.className&&($(".searchtxt")[1].value=a.target.innerText)})},a.search0Dest=function(c){var d=c.currentTarget.lastElementChild.innerText,e="../../ajax/areaHandler.ashx?fn=getarea2list&aId="+d;b.get(e).success(function(b){a.dest=b.ds})},a.search1Dest=function(a){return a.currentTarget.childNodes[1].innerText?void(window.location.href=a.currentTarget.childNodes[1].innerText):($(".searchtxt")[1].value=a.currentTarget.innerText,$(".searchtxt")[1].placeholder="",void searchLines())};var h="../../ajax/areaHandler.ashx?fn=getarealist";b.get(h).success(function(b){a.area=b.ds}),a.seemore=function(a){var b=a.currentTarget.childNodes[0].innerText;b=b.toLowerCase(),$(".amore").href="/app/linelists/"+b,window.location.href="#/app/linelists/"+b},a.$on("$ionicView.loaded",function(){var c="../../ajax/bannerImgHandler.ashx?fn=getbannerimglist&status=true";b.get(c).success(function(a){for(var b=0;b<(a.ds.length>6?6:a.ds.length);b++){var c;c=0===a.ds[b].lineId?a.ds[b].H5Url:"#/app/linedetail/"+a.ds[b].lineId,$(".indexion #full-width-slider").append('<div class="rsContent"><a href="'+c+'"><img class="rsImg" src='+a.ds[b].imgUrl+" /></a></div>")}$(".indexion #full-width-slider").royalSlider({arrowsNav:!0,loop:!1,keyboardNavEnabled:!0,controlsInside:!1,imageScaleMode:"fill",arrowsNavAutoHide:!1,autoScaleSlider:!0,autoScaleSliderWidth:960,autoScaleSliderHeight:350,controlNavigation:"bullets",thumbsFitInViewport:!1,navigateByClick:!0,startSlideId:0,autoPlay:{enabled:!0,stopAtAction:!1},transitionType:"move",globalCaption:!0,deeplinking:{enabled:!0,change:!1},imgWidth:1400,imgHeight:680}),getPro()});var d=1,e="../../ajax/areaHandler.ashx?fn=getarea2list&aId="+d;b.get(e).success(function(b){a.dest=b.ds});var f="../../ajax/areaHandler.ashx?fn=getarea3list";b.get(f).success(function(a){$("#divdesselect .searchtxt").attr("placeholder",a.ds[0].searchName),$("#divdesselect .searchtxturl")[0].value=a.ds[0].H5Url})})}]).controller("lineDetailCtrl",["$scope","$http","$sce","$ionicScrollDelegate",function(a,b,c,d){function e(b,c){c=void 0==c,b?($(".collectimg").attr("src","img/收藏后.png"),a.ifcollect=!0,c&&layermyui("已收藏!")):($(".collectimg").attr("src","img/收藏前.png"),a.ifcollect=!1,c&&layermyui("已取消收藏!"))}function f(){for(var a=0;a<$(".linedetail .cltypetxt").length;a++)"酒店"==$(".linedetail .cltypetxt")[a].innerText&&($(".linedetail .cltypeimg")[a].className="cltypeimg trip_eat"),"景点"==$(".linedetail .cltypetxt")[a].innerText&&($(".linedetail .cltypeimg")[a].className="cltypeimg trip_spots"),"航班"==$(".linedetail .cltypetxt")[a].innerText&&($(".linedetail .cltypeimg")[a].className="cltypeimg trip_air"),"自由活动"==$(".linedetail .cltypetxt")[a].innerText&&($(".linedetail .cltypeimg")[a].className="cltypeimg trip_free"),"用餐"==$(".linedetail .cltypetxt")[a].innerText&&($(".linedetail .cltypeimg")[a].className="cltypeimg trip_eat"),"交通"==$(".linedetail .cltypetxt")[a].innerText&&($(".linedetail .cltypeimg")[a].className="cltypeimg trip_car"),"其他"==$(".linedetail .cltypetxt")[a].innerText&&($(".linedetail .cltypeimg")[a].className="cltypeimg trip_other"),"购物"==$(".linedetail .cltypetxt")[a].innerText&&($(".linedetail .cltypeimg")[a].className="cltypeimg trip_shop")}function g(a,b,c){$(".detailcontentbox").children().hide(),$(a).show(),$(".forthCenter").removeClass("contentblue lineblue"),$(b).addClass("contentblue"),$(c).addClass("lineblue"),d.resize()}followfunc();var h=location.href,i=h.substring(h.lastIndexOf("/")+1,h.length);$(".linedetail .ordernow").attr("href","#/app/indexdate/"+i),a.historygoback=function(){var a=getCookie("ent2detail");a.indexOf("search=")>-1?(window.location.href="#/app/linelists#"+a,location.reload()):"domestic"==a||"outbound"==a||"inbound"==a||"freetrip"==a||"sales"==a?(window.location.href="#/app/linelists/"+a,location.reload()):"index"!=a?window.history.back():window.history.back()};var j="../../ajax/apihandler.ashx?fn=getlinedetail&lineid="+i,k=layer.load(1,{shade:[.5,"#ababab"]});b.get(j).success(function(g){function h(){for(var b=[],c=0;c<g.visas[a.visaFatherSelect].visaMateriales.length;c++)g.visas[a.visaFatherSelect].visaMateriales[c].type.indexOf(a.typeNum)>-1&&b.push(g.visas[a.visaFatherSelect].visaMateriales[c]);a.visadetail=b,d.resize()}if(find404admin(g),layer.close(k),$(".linedetail .groupsheight").height(16),$(".linedetail .groupsinheight").height(16),null===g.line)return $(".title").empty().append("锦江旅游"),window.location.href="#/app/index",location.reload(),void layermyui("此线路暂无详细数据!",1500);a.linedetails=g.line,a.spprice=g.scoreRatio.ratio*g.minPrice,a.travelAgency=g.line.travelAgency,g.ifOnlineDiscount?$(".linedetail .ifpay").css("display","block"):$(".linedetail .ifpay").css("display","none"),g.line.payOnlineFlag?$(".linedetail .ifcoup").css("display","block"):($(".linedetail .ifcoup").css("display","none"),$(".linedetail .ifonline").css("display","none")),g.visas.length>0&&g.visas[0]?(a.visas=g.visas,a.visaini=g.visas[0],a.visadetail=g.visas[0].visaMateriales,a.visaFatherSelect=0,setTimeout(function(){$(".visa-fatherselect")[0].className="visa-fatherselect selected"},300),a.typeNum=1,$(".visaselect")[0].className="visaselect selected"):$(".tunbl4").css({display:"none"}),a.setVisa=function(b){for(var c=0;c<g.visas.length;c++)g.visas[c].visaType==b.currentTarget.innerText&&(a.visaini=g.visas[c],a.visaFatherSelect=c);h();for(var d=0;d<$(".visa-fatherselect").length;d++)$(".visa-fatherselect")[d].className="visa-fatherselect";b.currentTarget.className="visa-fatherselect selected"},a.setVisaMemberType=function(b){a.typeNum=b.currentTarget.title,h();for(var c=0;c<$(".visaselect").length;c++)$(".visaselect")[c].className="visaselect";b.currentTarget.className="visaselect selected"},a.TrustDangerousSnippet=function(a){return c.trustAsHtml(a)},a.seerules=function(){layer.alert("本起价是指为包含附加服务（如单人房差、保险费等）的基本价格。最终确认的产品价格将会随所选出行日期、人数及服务项目而相应变化。")};var i=g.line.name;tkdfunc(""+i+"-锦江旅游",""+i+",锦江旅游","上海出发"+i+", 锦江旅游是中国最优质的旅游线路和自助游一站式服务提供商。"),a.journeys=g.line.journeys.sort(sortbydayNumber),setTimeout(f,500),null!==g.line.lineFeature&&(a.featureContent=c.trustAsHtml(g.line.lineFeature.replace(/\n/g,"<br>"))),null!==g.line.priceInclude&&(a.priceIncludeContent=c.trustAsHtml(g.line.priceInclude.replace(/\n/g,"<br>"))),null!==g.line.priceExclusive&&(a.priceExclusiveContent=c.trustAsHtml(g.line.priceExclusive.replace(/\n/g,"<br>"))),null!==g.line.tips&&(a.tips=c.trustAsHtml(g.line.tips.replace(/\n/g,"<br>"))),g.line.groups=g.line.groups.sort(sortbydepartDate);for(var j=0;j<g.line.groups.length;j++)j!=g.line.groups.length-1?g.line.groups[j].departDate=FormatDate(g.line.groups[j].departDate)+",":g.line.groups[j].departDate=FormatDate(g.line.groups[j].departDate);var l,m=new Array(0),n=new Array(0);l=g.line.groups.length<=4?g.line.groups.length:4;for(var o=0;l>o;o++)m.push(g.line.groups[o]);if(a.group1=m,g.line.groups.length>4)for(var p=4;p<g.line.groups.length;p++)n.push(g.line.groups[p]);a.group2=n,dayslength=g.line.groups.length,4>=dayslength?$(".linedetail .gengduoimg").css("display","none"):$(".linedetail .gengduoimg").css("display","block"),a.groupcode="团号:"+g.line.groups[0].groupCode.substring(g.line.groups[0].groupCode.length-14,g.line.groups[0].groupCode.length),a.lineCategory=getcategoryNameByCode(g.line.lineCategory),a.price=g.minPrice,a.recommend=g.line.recommend,$(".linedetail .idline").show(),$(".linedetail .idfeature").hide(),$(".linedetail .idexpense").hide(),$(".linedetail .idvisa").hide(),$(".linedetail .tunbl1").addClass("contentblue"),$(".linedetail .tunbl5").addClass("lineblue");var q=getCookie("mcMemberCode");!function(){if(q){var c=a.linedetails.id,d="../../ajax/apihandler.ashx?fn=getifcollect&userMID="+q+"&lineID="+c,f=layer.load(1,{shade:[.5,"#ababab"]});b.get(d).success(function(a){layer.close(f),e("是"==a,!1)})}else $(".collectimg").css("display","none")}()}),a.collect=function(){var c=getCookie("mcMemberCode"),d=a.linedetails.id;if(a.ifcollect){if(!c)return void layermyui("请先登录");var f="../../ajax/apihandler.ashx?fn=cancelcollect&lineID="+d+"&userMID="+c,g=layer.load(1,{shade:[.5,"#ababab"]});b.get(f).success(function(a){layer.close(g),"操作成功!"==a&&e(!1)})}else{var h,i=a.linedetails.name,j=a.linedetails.title,k=a.price,l=a.linedetails.days;try{h=a.linedetails.images[0].url?a.linedetails.images[0].url:""}catch(m){h=""}if(!c)return void layermyui("收藏需先登录");var n="../../ajax/apihandler.ashx?fn=addcollect&title="+i+"&description="+j+"&price="+k+"&date="+l+"&imgurl="+h+"&userMID="+c+"&lineID="+d,o=layer.load(1,{shade:[.5,"#ababab"]});b.get(n).success(function(a){layer.close(o),"操作成功!"==a&&e(!0)})}},a.lineCl=function(){g(".linedetail .idline",".tunbl1",".tunbl5")},a.featureCl=function(){g(".linedetail .idfeature",".tunbl2",".tunbl6")},a.expenseCl=function(){g(".linedetail .idexpense",".tunbl3",".tunbl7")},a.visaCl=function(){g(".linedetail .idvisa",".tunbl4",".tunbl8")},a.$on("$ionicView.loaded",function(){var a=location.href,c=a.substring(a.lastIndexOf("/")+1,a.length),d="../../ajax/apihandler.ashx?fn=getlinedetail&lineid="+c;b.get(d).success(function(a){if(!a.line.images.length)return void $(".linedetail #full-width-slider").append('<div class="rsContent"><img class="rsImg" src="img/线路详情默认图片.png" style="height:200px !important;width:100%" /></div>');for(var b=0;b<a.line.images.length;b++){var c=a.line.images[b].url.substr(0,a.line.images[b].url.length-14)+"/1/interlace/1/w/440/h/230";$(".linedetail #full-width-slider").append('<div class="rsContent"><img class="rsImg" src='+c+" /></div>")}$(".linedetail #full-width-slider").royalSlider({arrowsNav:!0,loop:!1,keyboardNavEnabled:!0,controlsInside:!1,imageScaleMode:"fill",arrowsNavAutoHide:!1,autoScaleSlider:!0,autoScaleSliderWidth:960,autoScaleSliderHeight:350,controlNavigation:"bullets",thumbsFitInViewport:!1,navigateByClick:!0,startSlideId:0,autoPlay:{enabled:!0,stopAtAction:!1},transitionType:"move",globalCaption:!0,deeplinking:{enabled:!0,change:!1},imgWidth:1400,imgHeight:680})})})}]).controller("indexdateCtrl",["$scope","$http",function($scope,$http){nofollowfunc(),$scope.$on("$ionicView.loaded",function(){var url=location.href,lineid=url.substring(url.lastIndexOf("/")+1,url.length),nghttp="../../ajax/apihandler.ashx?fn=getlinedetail&lineid="+lineid,mylayeruiwait=layer.load(1,{shade:[.5,"#ababab"]});$http.get(nghttp).success(function(response){function intoCalendarTime(){var mindate="2017-05-01",maxdate="2016-01-01";data="[";for(var i=0;i<response.line.groups.length;i++){var date1=FormatDateYear(response.line.groups[i].departDate);if(daysBetween(datenow,date1)>preBookingDays){for(var groupid=response.line.groups[i].id,j=0;j<response.line.groups[i].prices.length;j++)"基本价"==response.line.groups[i].prices[j].offerType&&(minprice=getCurrentPrice(response.line.groups[i].prices[j].salePrice,response.line.groups[i].prices[j].vipPrice));var price1="¥"+minprice;data+='{"Date":"'+date1+'","Price":"'+price1+'","groupid":"'+groupid+'"},',date1>maxdate&&(maxdate=date1),mindate>date1&&(mindate=date1)}}data+="]",pickerEvent.setPriceArr(eval("("+data+")"));var maxmonth=maxdate.substr(maxdate.indexOf("-")+1,2),minmonth=mindate.substr(mindate.indexOf("-")+1,2);pickerEvent.setEndMonth(maxmonth),pickerEvent.setBeginMonth(minmonth),0==minmonth.indexOf("0")&&(minmonth=minmonth.substr(1,1)),pickerEvent.setMinMonth(minmonth),pickerEvent.Init("calendar")}layer.close(mylayeruiwait);var preBookingDays=response.line.preBookingDays,datenow=getNowFormatDate();intoCalendarTime(),$(".spinnerad").myspinner({max:10,min:1}),$(".spinnercd").myspinner({max:10}),adn=1,crn=0,$(".indexdate #sp01").click(function(){adn=this.children[0].children[1].value,$("#nextpick").attr("href",nextpickhref+"/"+adn+"/"+crn),maxpassenger(".indexdate #sp01",".indexdate #sp02",adn,crn,10)}),$(".indexdate #sp02").click(function(){crn=this.children[0].children[1].value,$("#nextpick").attr("href",nextpickhref+"/"+adn+"/"+crn),maxpassenger(".indexdate #sp01",".indexdate #sp02",adn,crn,10)})}),$scope.next=function(){var a=$(".numpera")[0].innerText;a<parseInt(adn)+parseInt(crn)?(layermyui("余位不足"),$("#nextpick").attr("href","")):10<parseInt(adn)+parseInt(crn)?(layermyui("成人+儿童最多支持10人"),$("#nextpick").attr("href","")):$("#nextpick").attr("href",nextpickhref+"/"+adn+"/"+crn)}})}]).controller("pickresourceCtrl2",["$scope","$http",function(a,b){nofollowfunc();var c=getpbyurl(1),d=getpbyurl(2),e=getpbyurl(3);setCookie("coupamount","",1),setCookie("coupcode","",1),setCookie("coupname","",1),a.pnum=d,a.cnum=c,c>0?$(".pickresource .childpricebox").css("display","block"):($(".pickresource .childpricebox").css("display","none"),$(".pickresource .adultpricebox").css("width","50%"),$(".pickresource .groupdatebox").css("width","50%")),$("#secureamount").empty().append("0"),$(".spinnerdif").myspinner({max:parseInt(c)+parseInt(d)});var f=0,g=layer.load(1,{shade:[.5,"#ababab"]}),h="../../ajax/apihandler.ashx?fn=queryrealtimerefresh&groupid="+e;b.get(h).success(function(b){function h(){roomdiff=j*roomdiffp1+(minprice-i)*roomdiffp2+samount1*d+samount2*d,setCookie("roomdiff",roomdiffp1,1),amountall=minprice*d+roomdiff+i*c,$(".pickresource .amount").empty().append(amountall),$("#amountct").empty().append(minprice*d),$("#nextfill").attr("href","#/app/fillorder/"+f+"/"+e+"/"+d+"/"+c+"/"+amountall)}layer.close(g);var i,j,k=b.payOnlineFlag;k?$(".usecoupbox").css({display:"block"}):$(".usecoupbox").css({display:"none"});for(var l=0;l<b.prices.length;l++)"基本价"==b.prices[l].offerType&&(minprice=getCurrentPrice(b.prices[l].salePrice,b.prices[l].vipPrice)),"儿童价"==b.prices[l].offerType&&(i=b.prices[l].salePrice),"单房差"==b.prices[l].offerType&&(j=b.prices[l].salePrice);a.minprice=minprice,i>0?(a.caddprice=minprice-i,a.cprice=i,$("#divchild").css("display","block")):(i=0,$("#divchild").css("display","none")),j>0?(a.dprice=j,$("#divdiff").css("display","block")):(j=0,$("#divdiff").css("display","none")),a.date=FormatDateYear(b.departDate),a.departurePlace=b.departurePlace,a.lineTitle=b.lineTitle;var m=50,n=80;samount1=0,samount2=0,$("#checkcancel").click(function(){0==this.checked?(samount1=0,f-=m):(samount1=m,f+=m),h(),$("#secureamount").empty().append(f)}),$("#checkaccident").click(function(){0==this.checked?(samount2=0,f-=n):(samount2=n,f+=n),h(),$("#secureamount").empty().append(f)}),roomdiff=0,roomdiffp1=0,roomdiffp2=0,$("#sp1").click(function(){roomdiffp1=this.children[0].children[1].value,h(),a.difnum=roomdiffp1,"0"==roomdiffp1?$(".dfcbox").css("display","none"):$(".dfcbox").css("display","block")}),$("#sp2").click(function(){roomdiffp2=this.children[0].children[1].value,h()}),$("#sp1").change(function(){roomdiffp1=this.children[0].children[1].value,h()}),$("#sp2").change(function(){roomdiffp2=this.children[0].children[1].value,h()}),h(),$(".black_overlay").css("height",$(".mytopdiv").height()),"0"==c?$(".rtbox").css("display","none"):$(".rtbox").css("display","block"),$(".dfcbox").css("display","none")}),a.getcoup=function(){return parseInt($("#sp1")[0].children[0].children[1].value)?void layermyui("选择优惠券与选择单房差不能交叉操作"):void(window.location.href="#/app/user/mycoup/"+amountall+"/"+e+"/"+d+"/"+c)},a.nonecoup=function(){$(".pickresource .getcouptext")[0].innerText="请选择",$(".pickresource .amount").empty().append(amountall),setCookie("coupamount","",1),$(".pickresource #sp1")[0].children[0].children[2].disabled=!1},a.costdetailnone=function(a){"费用明细"==a.srcElement.innerHTML?($(".black_overlay").css("display","block"),$("#costdetail").css("display","block")):($(".black_overlay").css("display","none"),$("#costdetail").css("display","none"))}}]).controller("fillorderCtrl",["$scope","$http",function(a,b){nofollowfunc();var c=getpbyurl(1),d=getpbyurl(2),e=getpbyurl(3),f=getpbyurl(4),g=(getpbyurl(5),{name:"",mobile:"",email:""});a.Connect=g,getCookie("phonenum")&&(a.Connect.mobile=getCookie("phonenum"));var h,i,j=0,k=0,l=0,m=0;a.amount=c,a.pnum=e,a.cnum=d,d>0?$("#childnumspan").css("display","inline"):$("#childnumspan").css("display","none");var n,o="../../ajax/apihandler.ashx?fn=queryrealtimerefresh&groupid="+f,p=layer.load(1,{shade:[.5,"#ababab"]});b.get(o).success(function(b){find404admin(b),layer.close(p),a.lineTitle=b.lineTitle,a.date=FormatDateYear(b.departDate),n=!!b.payOnlineFlag;for(var c=0;c<b.prices.length;c++)"基本价"==b.prices[c].offerType?(h=b.prices[c].id,j=getCurrentPrice(b.prices[c].salePrice,b.prices[c].vipPrice),i=b.prices[c].salePrice==b.prices[c].vipPrice?b.onlineDiscount:1):"儿童价"==b.prices[c].offerType?k=b.prices[c].salePrice:"单房差"==b.prices[c].offerType&&(l=b.prices[c].salePrice,m=b.prices[c].id);for(var d=new Array(0),f=0;e>f;f++)d.push(f);a.guests=d});var q=new Array(0);a.createorder=function(){function g(a,b){this.category=a,this.name=b}var o=0;getCookie("roomdiff")>0&&(o=getCookie("roomdiff"));var p=a.Connect.name,r=a.Connect.mobile,s=a.Connect.email;if(""==p||void 0==p||null==p)return void layermyui("请输入联系人");if(""==r||void 0==r||null==r)return void layermyui("请输入手机号");if(!checkMobile(r))return void layermyui("手机号格式不正确");for(var t=new g,u=0;e>u;u++)try{t=new g("ADULT",""==$(".inname")[u].value?"游客"+(parseInt(u)+1):$(".inname")[u].value),q.push(t)}catch(v){}for(var w="",x=0;e>x;x++)w+='{"category":"'+q[x].category+'","name":"'+q[x].name+'"},';w=w.substring(0,w.length-1);var y=Math.floor(j*(1-i))*e,z=getCookie("mcMemberCode"),A="";o>0&&(A+=',{ "copies": '+o+', "discountAmount": 0, "priceId": '+m+', "singlePrice": '+l+" }"),d>0&&(A+=',{ "copies": '+d+', "discountAmount": 0, "priceId": '+h+', "singlePrice": '+k+" }");var B=0,C="";if(getCookie("coupamount")&&n){B=getCookie("coupamount");var D=getCookie("coupcode"),E=getCookie("coupname");C=',"couponRuleName": "'+E+'","coupunCodes": ["'+D+'"],"couponNum": 1,"couponPrice": '+B,y=0}var F=layer.load(1,{shade:[.5,"#ababab"]}),G="../../ajax/userHandler.ashx?fn=getvalidateemailortel&phone="+r;b.get(G).success(function(a){function g(){var a=layer.load(1,{shade:[.5,"#ababab"]}),c="<webMemberDto><phone>"+r+"</phone><registChannel>Website</registChannel></webMemberDto>",d="../../ajax/userHandler.ashx?fn=quickregistorder&xml="+c;b.get(d).success(function(b){find404admin(b),layer.close(a);var c=(new X2JS).xml_str2json(b);c.webMemberRegisterReturnDto.mcMemberCode?(z=c.webMemberRegisterReturnDto.mcMemberCode,setCookie("mcMemberCode",c.webMemberRegisterReturnDto.mcMemberCode,1),setCookie("fullName",c.webMemberRegisterReturnDto.mcMemberCode,1),setCookie("phonenum",r,1),i()):layermyui("下单注册失败,请联系管理员!")})}function i(){var a=layer.load(1,{shade:[.5,"#ababab"]}),g='{"adultNum":'+e+',"amount":'+(parseInt(c)+parseInt(B))+',"channel":"E_BUSINESS_PLATFORM","childNum":'+d+',"contact":{"mobile":"'+r+'","name":"'+p+'","email":"'+s+'"},"couponAmount":'+B+',"groupId":'+f+',"guests":['+w+'],"mcMemberCode":"'+z+'","cardNo":"1231234","onLinePay":'+n+',"receivables":[{"copies":'+e+',"discountAmount":'+y+',"priceId":'+h+',"singlePrice":'+j+"}"+A+'],"scorePay":false '+C+"}",i="../../ajax/apihandler.ashx?fn=createorder&json="+g;b.get(i).success(function(b){find404admin(b),layer.close(a);var g=finderrorMsgadmin(b),h=b;if(g)return"mcMemberCode不能为空!"==g?(setCookie("tolamount",c,1),setCookie("cnum",d,1),setCookie("pnum",e,1),setCookie("groupid",f,1),setCookie("orderNo",h.orderNo,1),setCookie("linkbackpay","true",1),void(window.location.href="#/app/user/login")):void layermyui(g,2e3);if(n){for(var i=0;e>i;i++)setCookie("inname"+i,""==$(".inname")[i].value?"游客"+(parseInt(i)+1):$(".inname")[i].value,1);setCookie("orderNo",h.orderNo,1),c-=y,window.location.href="#/app/payway/"+h.orderNo+"/"+f+"/"+e+"/"+d+"/"+c}else layermyui("该订单仅支持门店支付"),setTimeout(function(){window.location.href="#/app/user/myorder"},3e3)})}find404admin(a),layer.close(F);var k=(new X2JS).xml_str2json(a);"false"==k.validateEmailOrPhoneRespDto.existFlag?g():i()})}}]).controller("paywayCtrl",["$scope","$http","getbindquerysev",function(a,b,c){nofollowfunc();var d=getCookie("roomdiff"),e=getpbyurl(1),f=getpbyurl(2),g=getpbyurl(3),h=getpbyurl(4),i=getpbyurl(5);a.amount=e;var j="../../ajax/apihandler.ashx?fn=queryrealtimerefresh&groupid="+h,k=layer.load(1,{shade:[.5,"#ababab"]});b.get(j).success(function(j){function l(a){this.name=a}find404admin(j),layer.close(k);for(var m,n=0,o=0,q=0;q<j.prices.length;q++)"基本价"==j.prices[q].offerType?m=getCurrentPrice(j.prices[q].salePrice,j.prices[q].vipPrice):"儿童价"==j.prices[q].offerType?n=j.prices[q].salePrice:"单房差"==j.prices[q].offerType&&(o=j.prices[q].salePrice);a.minprice=m,a.childprice=n,a.dfprice=o,a.groupCode=j.groupCode,a.lineTitle=j.lineTitle,a.pnum=g,a.cnum=f,f>0?($(".childgogo").css("display","inline"),$(".childgogo2").css("display","block")):($(".childgogo").css("display","none"),$(".childgogo2").css("display","none")),d>0?$(".dfgo").css("display","block"):$(".dfgo").css("display","none"),a.departDate=FormatDateYear(j.departDate),a.timepay=FormatDateTimeDiff(36e5);for(var r=new Array(0),s=0;g>s;s++)p=new l(getCookie("inname"+s)),r.push(p);a.inname=r,$(".innamebox").height(60+31*(g-2));var t="";return 0==j.payOnlineFlag?(layermyui("不允许在线支付,请到门店支付"),void $("[type=checkbox]").attr("disabled",!0)):(a.pay=function(){if(""==t)return void layermyui("请选择支付方式");var a=getCookie("mcMemberCode");if(""!=a&&void 0!=a&&null!=a){if("JJE_APP_UNION_PAY"==t){e=.01;var d=function(a){return a.userToken?void(window.location.href="#/app/card/sms/"+e+"/"+i):void(window.location.href="#/app/card/bind/"+i)};c.bindquetyfunc(b,d)}else if("JJE_APP_SCORE_PAY"==t){var j=100*e,k="../../ajax/apihandler.ashx?fn=pbppayorder&orderNo="+i+"&payAmount=0&accountName="+t+"&score="+j+"&Membercode="+a,l=layer.load(1,{shade:[.5,"#ababab"]});b.get(k).success(function(a){layer.close(l);var b=(new X2JS).xml_str2json(a);try{if(b.payCallBackResult.result)return layermyui("支付完成,已使用"+b.payCallBackResult.scoreAmount+"积分,可在个人中心查看"),void setTimeout(function(){window.location.href="#/app/card/paysuccess"},2e3)}catch(c){layermyui(a)}})}else if("JJE_APP_ECARD_PAY"==t){e=.01;var m="../../ajax/apihandler.ashx?fn=pbppayorder&orderNo="+i+"&payAmount="+e+"&accountName="+t,n=layer.load(1,{shade:[.5,"#ababab"]});b.get(m).success(function(a){layer.close(n);(new X2JS).xml_str2json(a);$(".payway").empty().append(a)})}else if("JJE_APP_CLIENT_ALI_WAP_PAY"==t){e=1;var o="../../ajax/apihandler.ashx?fn=pbppayorder&orderNo="+i+"&payAmount="+e+"&accountName="+t,p=layer.load(1,{shade:[.5,"#ababab"]});b.get(o).success(function(a){layer.close(p),window.location.href=a})}}else setCookie("tolamount",e,1),setCookie("cnum",f,1),setCookie("pnum",g,1),setCookie("groupid",h,1),setCookie("linkbackpay","true",1),window.location.href="#/app/user/login"},void(a.paywaySelect=function(a){$("[type=checkbox]").attr("checked",!1),$("[type=checkbox][value="+a.target.value+"]").attr("checked","true"),"SPAN"==a.target.parentNode.nodeName&&(t="支付宝"==a.target.parentNode.previousElementSibling.innerHTML?"JJE_APP_CLIENT_ALI_WAP_PAY":"银联"==a.target.parentNode.previousElementSibling.innerHTML?"JJE_APP_UNION_PAY":"积分支付"==a.target.parentNode.previousElementSibling.innerHTML?"JJE_APP_SCORE_PAY":"锦江e卡通"==a.target.parentNode.previousElementSibling.innerHTML?"JJE_APP_ECARD_PAY":"")}))})}]).controller("cancelorderCtrl",["$scope","$http",function(a,b){nofollowfunc();var c="../../ajax/apihandler.ashx?fn=cancelorder&ordercode=111";b.get(c).success(function(a){})}]);var dayslength,counting=0;