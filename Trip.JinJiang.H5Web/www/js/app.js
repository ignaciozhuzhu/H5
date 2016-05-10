﻿// Ionic Starter App

// angular.module is a global place for creating, registering and retrieving Angular modules
// 'starter' is the name of this angular module example (also set in a <body> attribute in index.html)
// the 2nd parameter is an array of 'requires'
// 'starter.controllers' is found in controllers.js
angular.module('starter', ['ionic', 'starter.controllers'])

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

.config(function ($stateProvider, $urlRouterProvider) {
    $stateProvider

      .state('app', {
          url: '/app',
          abstract: true,
          templateUrl: 'templates/menu.html',
          controller: 'AppCtrl'
      })

    //线路列表---------------------------------------------
        .state('app.linelists', {
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
         url: '/indexdate',
         views: {
             'menuContent': {
                 templateUrl: 'templates/indexdate.html',
                 controller: 'indexdateCtrl'
             }
         }
     })
       //选择资源---------------------------------------------
     .state('app.pickresource', {
         url: '/pickresource',
         views: {
             'menuContent': {
                 templateUrl: 'templates/pickresource.html',
                 controller: 'pickresourceCtrl'
             }
         }
     })
       //填写订单---------------------------------------------
     .state('app.fillorder', {
         url: '/fillorder',
         views: {
             'menuContent': {
                 templateUrl: 'templates/fillorder.html',
                 controller: 'fillorderCtrl'
             }
         }
     })
    // if none of the above states are matched, use this as the fallback
    $urlRouterProvider.otherwise('/app/index');///app/playlists
});

