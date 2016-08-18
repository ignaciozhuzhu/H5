// Ionic Starter App

// angular.module is a global place for creating, registering and retrieving Angular modules
// 'starter' is the name of this angular module example (also set in a <body> attribute in index.html)
// the 2nd parameter is an array of 'requires'
// 'starter.controllers' is found in controllers.js
angular.module('starter', ['ionic', 'starter.controllers', 'starter.controllersuser', 'starter.service', 'starter.directive', 'angularjs-dropdown-multiselect'])

.run(function ($ionicPlatform) {
    $ionicPlatform.ready(function () {
        // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
        // for form inputs)
        if (window.cordova && window.cordova.plugins.Keyboard) {
            cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);
            cordova.plugins.Keyboard.disableScroll(true);

        }
        if (window.StatusBar) {
            // org.apache.cordova.statusbar required
            StatusBar.styleDefault();
        }
    });
})

.config(function ($stateProvider, $urlRouterProvider, $ionicConfigProvider) {
    // $ionicConfigProvider.views.maxCache(0);

    $ionicConfigProvider.backButton.text("");
    $ionicConfigProvider.backButton.previousTitleText(false);
    $stateProvider

      .state('app', {
          url: '/app',
          abstract: true,
          templateUrl: 'templates/menu.html',
          controller: 'AppCtrl'
      })

    //线路列表---------------------------------------------
        .state('app.linelists', {
            cache: false,
            url: '/linelists',
            views: {
                'menuContent': {
                    templateUrl: 'templates/linelists.html',
                    controller: 'LinelistsCtrl'
                }
            }
        })
      .state('app.linelists2', {
          url: '/linelists/:linecategory',
          views: {
              'menuContent': {
                  templateUrl: 'templates/linelists.html',
                  controller: 'LinelistsCtrl2'
              }
          }
      })
        //主页------------------------------------------------
      .state('app.index', {
          cache: false,
          url: '/index',
          views: {
              'menuContent': {
                  templateUrl: 'templates/index.html',
                  controller: 'indexCtrl'
              }
          }
      })
        //线路列表---------------------------------------------
      .state('app.linedetail', {
          cache: false,
          url: '/linedetail/:lineId',
          views: {
              'menuContent': {
                  templateUrl: 'templates/linedetail.html',
                  controller: 'lineDetailCtrl'
              }
          }
      })
       //以下订单流程-----------------------------------------
       //选择日期---------------------------------------------
     .state('app.indexdate', {
         url: '/indexdate/:lineid',
         views: {
             'menuContent': {
                 templateUrl: 'templates/indexdate.html',
                 controller: 'indexdateCtrl'
             }
         }
     })
       //选择资源---------------------------------------------
      .state('app.pickresource2', {
          url: '/pickresource/:groupid/:pnum/:cnum',
          views: {
              'menuContent': {
                  templateUrl: 'templates/pickresource.html',
                  controller: 'pickresourceCtrl2'
              }
          }
      })
       //填写订单---------------------------------------------
     .state('app.fillorder', {
         url: '/fillorder/:secureamount/:groupid/:pnum/:cnum/:amount',
         views: {
             'menuContent': {
                 templateUrl: 'templates/fillorder.html',
                 controller: 'fillorderCtrl'
             }
         }
     })
       //支付方式---------------------------------------------
     .state('app.payway', {
         url: '/payway/:secureamount/:groupid/:pnum/:cnum/:amount',
         views: {
             'menuContent': {
                 templateUrl: 'templates/payway.html',
                 controller: 'paywayCtrl'
             }
         }
     })
       //取消订单---------------------------------------------
     .state('app.cancelorder', {
         url: '/cancelorder',
         views: {
             'menuContent': {
                 templateUrl: 'templates/cancelorder.html',
                 controller: 'cancelorderCtrl'
             }
         }
     })

      //会员模块---------------------------------------------
     .state('app.register', {
         url: '/user/register',
         views: {
             'menuContent': {
                 templateUrl: 'templates/user/register.html',
                 controller: 'registerCtrl'
             }
         }
     })
     .state('app.login', {
         url: '/user/login',
         views: {
             'menuContent': {
                 templateUrl: 'templates/user/login.html',
                 controller: 'loginCtrl'
             }
         }
     })
     .state('app.quickregister', {
         url: '/user/quickregister',
         views: {
             'menuContent': {
                 templateUrl: 'templates/user/quickregister.html',
                 controller: 'quickregisterCtrl'
             }
         }
     })
        //忘记密码
        .state('app.forgetpwd', {
         url: '/user/forgetpwd',
         views: {
             'menuContent': {
                 templateUrl: 'templates/user/forgetpwd.html',
                 controller: 'forgetpwdCtrl'
             }
         }
        })
        //我的订单
        .state('app.myorder', {
            //cache: false,
            url: '/user/myorder',
            views: {
                'menuContent': {
                    templateUrl: 'templates/user/myorder.html',
                    controller: 'myorderCtrl'
                }
            }
        })
        //我的积分
        .state('app.mypoints', {
            //cache: false,
            url: '/user/mypoints',
            views: {
                'menuContent': {
                    templateUrl: 'templates/user/mypoints.html',
                    controller: 'mypointsCtrl'
                }
            }
        })
        //我的优惠券-->选择
        .state('app.mycoup', {
            url: '/user/mycoup/:groupid/:pnum/:cnum',
            views: {
                'menuContent': {
                    templateUrl: 'templates/user/mycoup.html',
                    controller: 'mycoupCtrl2'
                }
            }
        })
        //我的优惠券-->查看
        .state('app.mycoup2', {
            url: '/user/mycoup',
            views: {
                'menuContent': {
                    templateUrl: 'templates/user/mycoup.html',
                    controller: 'mycoupCtrl'
                }
            }
        })
        //订单详情
        .state('app.orderdetail', {
            url: '/user/orderdetail/:ordercode',
            views: {
                'menuContent': {
                    templateUrl: 'templates/user/orderdetail.html',
                    controller: 'orderdetailCtrl'
                }
            }
        })
        //个人中心
        .state('app.myinfo', {
            url: '/user/myinfo',
            views: {
                'menuContent': {
                    templateUrl: 'templates/user/myinfo.html',
                    controller: 'myinfoCtrl'
                }
            }
        })
        //个人中心
        .state('app.shortregist', {
            url: '/user/shortregist',
            views: {
                'menuContent': {
                    templateUrl: 'templates/user/shortregist.html',
                    controller: 'shortregistCtrl'
                }
            }
        })
     .state('app.quickregister2', {
         url: '/user/quickregister2',
         views: {
             'menuContent': {
                 templateUrl: 'templates/user/quickregister2.html',
                 controller: 'quickregister2Ctrl'
             }
         }
     })

    // if none of the above states are matched, use this as the fallback
    $urlRouterProvider.otherwise('/app/index');///app/playlists
});

