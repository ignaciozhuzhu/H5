﻿<%@ Page Title="" Language="C#" MasterPageFile="~/modules/admin/master/Header.Master" AutoEventWireup="true" CodeBehind="linecategory.aspx.cs" Inherits="Trip.JinJiang.H5Web.modules.admin.functions.linecategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper" ng-app="lhxApp" ng-controller="userCtrl">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>线路分类管理
                <small>操作</small>
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>H5</a></li>
                <li><%--<a href="#">二郎腿</a>--%></li>
                <li>轮播图管理</li>
            </ol>
        </section>

        <form id="example" class="modal hide fade in" style="display: none; height: 330px; width: 270px;">
            <div class="modal-header">
                <a class="close" data-dismiss="modal">×</a>
                <h3>线路类型编辑</h3>
            </div>
            <div class="modal-body">
                <div>
                    <div>分类名称:</div>
                    <div>
                        <input id="categoryName" type="text" style="height: 30px" />
                    </div>
                </div>
                <div>
                    <div>分类编码:</div>
                    <div>
                        <input id="lineCategory" type="text" style="height: 30px" />
                    </div>
                    <div>图标:</div>
                    <input id="File1" name="File1" type="file" />
                </div>
                <input type="text" name="txt" id="txt" style="display: none">
                <input type="button" name="btn" value="btn" id="btn" style="display: none">
            </div>
            <div class="modal-footer">
                <a href="#" class="btn btn-success" ng-click="reloadRoute()">保存</a>
                <a href="#" class="btn" data-dismiss="modal">关闭</a>
            </div>
        </form>

        <form id="example0" class="modal hide fade in" style="display: none; height: 120px; width: 270px;">
            <div class="modal-header">
                <a class="close" data-dismiss="modal">×</a>
                <h3>确认禁(可)用吗?</h3>
            </div>
            <div class="modal-footer">
                <a href="#" class="btn btn-success" ng-click="confirmEn()">确认</a>
                <a href="#" class="btn" data-dismiss="modal">取消</a>
            </div>
        </form>
        <form id="example2" class="modal hide fade in" style="display: none; height: 120px; width: 270px;">
            <div class="modal-header">
                <a class="close" data-dismiss="modal">×</a>
                <h3>确认删除吗?</h3>
            </div>
            <div class="modal-footer">
                <a href="#" class="btn btn-success" ng-click="confirmDel()">确认</a>
                <a href="#" class="btn" data-dismiss="modal">取消</a>
            </div>
        </form>

        <!-- Main content -->
        <section class="content">
            <div class="row">
                <div class="col-xs-12">

                    <div class="box">
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div><img src='../../img/add.png' style="margin-left: 1%"><a ng-click="add()" data-toggle="modal" href="#example" >添加</a></div>

                            <table id="example1" class="table table-bordered table-striped">
                                <thead>
                                    <tr>
                                        <th ng-hide="true">Id</th>
                                        <th>分类名称</th>
                                        <th>分类编码</th>
                                        <th>图标</th>
                                        <th>状态</th>
                                        <th>操作</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr ng-repeat="x in lines">
                                        <td ng-hide="true">{{x.Id}}</td>
                                        <td>{{x.categoryName}}</td>
                                        <td>{{x.lineCategory}}</td>
                                        <td>
                                            <img src="{{x.imgUrl}}" style="width: 30px; height: 30px"></td>
                                        <td>{{x.status===true?'可用':'禁用'}}</td>
                                        <td>
                                            <img src='../../img/edit.png'><a ng-click="edit($event)" data-toggle="modal" href="#example">修改</a>
                                            <img style="margin-left: 5%" src='../../img/disable.png'><a ng-click="changeen($event)" data-toggle="modal" href="#example0">禁(可)用</a>
                                            <img style="margin-left: 5%" src='../../img/delete.png'><a ng-click="delete($event)" data-toggle="modal" href="#example2">删除</a>
                                        </td>
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
        $('.treeview-menu .treeviewli2').addClass('active');
        var app = angular.module('lhxApp', ['ng-pagination']);
        var selectid;
        function modalclass() {
            $("#example").attr("class", "modal fade in");
        }
        function modalclass1() {
            $("#example0").attr("class", "modal fade in");
        }
        function modalclass2() {
            $("#example2").attr("class", "modal fade in");
        }

        app.controller('userCtrl', function ($scope, $http, $window) {
            //$scope.pageCount = 100;
            percount = 10;
            $scope.add = function () {
                modalclass();
                selectid = "";
                $('#categoryName')[0].value = "";
                $('#lineCategory')[0].value = "";
                $('#lineCategory').removeAttr('disabled');
            };
            $scope.edit = function ($event) {
                modalclass();
                selectid = $event.path[2].cells[0].innerText;
                $('#categoryName')[0].value = $event.path[2].cells[1].innerText;
                $('#lineCategory')[0].value = $event.path[2].cells[2].innerText;
                $('#lineCategory').attr("disabled", "disabled");
            };
            $scope.changeen = function ($event) {
                modalclass1();
                selectid = $event.path[2].cells[0].innerText;
            };
            $scope.delete = function ($event) {
                modalclass2();
                selectid = $event.path[2].cells[0].innerText;
            };
            $scope.confirmEn = function () {
                $.ajax({
                    url: "../../../ajax/apihandler.ashx?fn=enlinecategory",
                    type: "post",
                    data: { Id: selectid },
                    success: function (text) {
                        $window.location.reload();
                    }
                });
            };
            $scope.confirmDel = function () {
                $.ajax({
                    url: "../../../ajax/apihandler.ashx?fn=dellinecategory",
                    type: "post",
                    data: { Id: selectid },
                    success: function (text) {
                        $window.location.reload();
                    }
                });
            };
            var nghttp = "../../../ajax/apihandler.ashx?fn=getlinecategorys";
            $http.get(nghttp).success(function (response) {
                $scope.pageCount = Math.ceil(response.ds.length / percount);
                responseCache = response;
                var arrayLine = new Array(0);
                var pagec = responseCache.ds.length - (percount * ($scope.currentPage - 1)) >= percount ? percount * $scope.currentPage : responseCache.ds.length;
                for (var i = 0; i < pagec; i++) {
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
                var path = document.getElementById("File1").value;
                var img = document.getElementById("img1");
                if ($.trim(path) == "") {
                    alert("请选择要上传的文件");
                    return;
                }

                //未选中行说明是新增.
                if (selectid === null || selectid === undefined || selectid === "") {
                    $("#example").ajaxSubmit({
                        success: function (str) {
                            if (str != null && str != "undefined") {
                                if (str == "操作成功!") {
                                    alert(str);
                                    $window.location.reload();
                                }
                                else if (str == "2") { alert("只能上传jpg或png格式的图片"); }
                                else if (str == "3") { alert("图片不能大于1M"); }
                                else if (str == "4") { alert("请选择要上传的文件!!"); }
                                else { alert('操作失败！'); }
                            }
                            else alert('操作失败！');
                        },
                        error: function (error) { alert(error); },
                        url: '../../../ajax/lineCategoryHandler.ashx?fn=addcategory',
                        type: "post",
                        data: { categoryName: $('#categoryName')[0].value, lineCategory: $('#lineCategory')[0].value },
                        dataType: "text"
                    });

                }
                else {
                    $("#example").ajaxSubmit({
                        success: function (str) {
                            if (str != null && str != "undefined") {
                                if (str == "操作成功!") {
                                    alert(str);
                                    $window.location.reload();
                                }
                                else if (str == "2") { alert("只能上传jpg或png格式的图片"); }
                                else if (str == "3") { alert("图片不能大于1M"); }
                                else if (str == "4") { alert("请选择要上传的文件!!"); }
                                else { alert('操作失败！'); }
                            }
                            else alert('操作失败！');
                        },
                        error: function (error) { alert(error); },
                        url: '../../../ajax/lineCategoryHandler.ashx?fn=editlinecategory',
                        type: "post",
                        data: { categoryName: $('#categoryName')[0].value, lineCategory: $('#lineCategory')[0].value, Id: selectid },
                        dataType: "text"
                    });

                }

            }

        });

    </script>
</asp:Content>
