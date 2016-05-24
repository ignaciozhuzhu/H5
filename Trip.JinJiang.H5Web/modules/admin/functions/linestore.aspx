﻿<%@ Page Title="" Language="C#" MasterPageFile="~/modules/admin/master/Header.Master" AutoEventWireup="true" CodeBehind="linestore.aspx.cs" Inherits="Trip.JinJiang.H5Web.modules.admin.functions.linestore" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper" ng-app="lhxApp"  ng-controller="userCtrl">
            <!-- Content Header (Page header) -->
            <section class="content-header">
                <h1>
                    线路库
                    <small>操作</small>
                </h1>
                <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>H5</a></li>
                <li><a href="#">二郎腿</a></li>
                <li class="active">线路库</li>
                </ol>
            </section>

            <div id="example" class="modal hide fade in" style="display: none; height: 200px; width: 300px; ">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal">×</a>
                    <h3>线路类型选择</h3>
                </div>
                <div class="modal-body">
                    <select id="sel">
                        <option value="FREETRIP">自由行</option>
                        <option value="GROUPTRIP">跟团游</option>
                        <option value="SHIP">游轮</option>
                        <option value="OUT">出境游</option>
                        <option value="ONSALE">特价游</option>
                    </select>
                    <input type="text" name="txt" id="txt" style="display:none">
                    <input type="button" name="btn" value="btn" id="btn" style="display:none">
                </div>
                <div class="modal-footer">
                    <a href="#" class="btn btn-success" ng-click="reloadRoute()">保存</a>
                    <a href="#" class="btn" data-dismiss="modal">关闭</a>
                </div>
            </div>

            <!-- <p><a data-toggle="modal" href="#example" class="btn btn-primary btn-large motaiclick">选起来</a></p>-->
            <!-- Main content -->
            <section class="content" >
                <div class="row">
                    <div class="col-xs-12">
                        <div>线路筛选: <span style="padding-left:10px"><input type="text" style="height:30px" ng-model="test"></span></div>
                        <div class="box">
                            <!-- /.box-header -->
                            <div class="box-body" >
                                <table id="example1" class="table table-bordered table-striped">
                                    <thead>
                                        <tr>
                                            <th ng-hide="true">ID</th>
                                            <th>名称</th>
                                            <th>标题</th>
                                            <th>目的地</th>
                                            <th>线路类型</th>
                                            <th>最低价</th>
                                            <th>操作</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr ng-repeat="x in lines | filter:test">
                                            <td ng-hide="true">{{x.lineId}}</td>
                                            <td>{{x.name}}</td>
                                            <td>{{x.title}}</td>
                                            <td>{{x.destinationInfo}}</td>
                                            <td>{{x.lineCategory}}</td>
                                            <td>{{x.originalPrice}}</td>
                                            <td><a ng-click="toggle($event)" data-toggle="modal" href="#example">编辑</a></td>
                                        </tr>
                                    </tbody>
                                </table>

                                <pager page-count="pageCount" current-page="currentPage" on-page-change="onPageChange()" first-text="首页" next-text="下一页" prev-text="上一页" last-text="尾页"></pager>
                            </div>
                            <!-- /.box-body -->
                        </div>
                        <!-- /.box -->
                    </div>
                    <!-- /.col -->
                </div>
                <!-- /.row -->
            </section>
            <!-- /.content -->
        </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderScripts" runat="server">
      <script type="text/javascript">

        var app = angular.module('lhxApp', [ 'ng-pagination']);
        var selectid;
        app.controller('userCtrl', function ($scope, $http, $window) {
            $scope.pageCount = 100;
            percount = 10;
            $scope.toggle = function ($event) {
                $("#example").attr("class", "modal fade in");
                selectid = $event.path[2].cells[0].innerText;
            };
            var nghttp = "../../../ajax/apihandler.ashx?fn=getlinesad";
            $http.get(nghttp).success(function (response) {
                responseCache = response;
                var arrayLine = new Array(0);
                for (var i = 0; i < percount; i++) {
                    arrayLine.push(responseCache.ds[i]);
                }
                $scope.lines = arrayLine;
            });
            $scope.onPageChange = function () {
                var arrayLine = new Array(0);
                var pagec = responseCache.ds.length - (percount * ($scope.currentPage - 1)) >= percount ? percount * $scope.currentPage : responseCache.ds.length;
                for (var i = percount * ($scope.currentPage - 1) ; i < pagec; i++) {
                    arrayLine.push(responseCache.ds[i]);
                }
                $scope.lines = arrayLine;
            };
            $scope.reloadRoute = function () {
                $.ajax({
                    url: "../../../ajax/apihandler.ashx?fn=updatelinesad",
                    type: "post",
                    data: { lineCategory: $('#sel')[0].value, lineid: selectid },
                    success: function (text) {
                        $window.location.reload();
                    }
                });

            }

        });

        function getlinecategorys() {
            $.ajax({
                url: "../../../ajax/apihandler.ashx?fn=getlinecategorys",
                type: "post",
                success: function (text) {
                    var d = eval("(" + text + ")");
                }
            });
        }

    </script>
</asp:Content>
