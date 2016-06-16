<%@ Page Title="" Language="C#" MasterPageFile="~/modules/admin/master/Header.Master" AutoEventWireup="true" CodeBehind="recommendstorage.aspx.cs" Inherits="Trip.JinJiang.H5Web.modules.admin.functions.recommendstorage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .box {
            border: 1px solid #ccc;
        }
        #example {
            left:45%;
        }
    </style>
    <div class="content-wrapper" ng-app="lhxApp" ng-controller="userCtrl">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>推荐产品查询区
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>H5</a></li>
                <li>推荐产品查询区</li>
            </ol>
        </section>

        <!-- Main content -->
        <section class="content">
            <div class="row">
                <div class="col-xs-12">
                    <div>
                        线路名称: 
                        <span style="padding-left: 10px">
                            <input id="linenamesc" type="text" style="height: 30px">
                        </span>
                        旅行社品牌: 
                        <span style="padding-left: 10px">
                            <input id="agencysc" type="text" style="height: 30px">
                        </span>
                        线路ID: 
                        <span style="padding-left: 10px">
                            <input id="lineidsc" type="text" style="height: 30px">
                        </span>
                        <input style="margin-left: 100px; width: 100px" ng-click="filtcategory()" type="button" value="查询">
                    </div>
                    <div>
                        产品类型: 
                        <span style="padding-left: 10px">
                            <input id="categorysc" type="text" style="height: 30px">
                        </span>
                        所属旅行社: 
                        <span style="padding-left: 10px">
                            <input id="agencysc2" type="text" style="height: 30px">
                        </span>
<%--                        产品区域: 
                        <span style="padding-left: 10px">
                            <input id="selectedcate" type="text" style="height: 30px">
                        </span>--%>
                    </div>
                    <div class="box">
                        <!-- /.box-header -->
                        <div class="box-body">
                            <table id="example1" class="table table-bordered table-striped">
                                <thead>
                                    <tr>
                                        <th ng-hide="true">ID</th>
                                        <th>ID</th>
                                        <th>线路名称</th>
                                        <th>所属旅行社</th>
                                        <th>品牌</th>
                                        <th>价格</th>
                                        <th>操作</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr ng-repeat="x in lines">
                                        <td ng-hide="true">{{x.Id}}</td>
                                        <td>{{x.lineId}}</td>
                                        <td>{{x.lineName}}</td>
                                        <td>{{x.agency}}</td>
                                        <td>{{x.brand}}</td>
                                        <td>{{x.originalPrice}}</td>
                                        <td>
                                            <img src='../../img/edit.png'><a ng-click="toggle($event)" data-toggle="modal" href="#example">修改</a></td>
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

        <div id="example" class="modal hide fade in" style="display: none; height: 380px; width: 600px;">
            <div class="modal-header">
                <a class="close" data-dismiss="modal">×</a>
                <h3>推荐/修改旅游营销产品</h3>
            </div>
            <div class="modal-body">
                <div><input type="text" style="display:none" id="lineid"></div>
                <div><input type="text" style="display:none" id="agency"></div>
                <div>线路:<input type="text" style="height: 30px;margin-left:30px;width:400px;" disabled name="txt" id="linetitle"></div>
                <div>标题:<input type="text" style="height: 30px;margin-left:30px;width:400px" name="txt" id="title"></div>
                <div>排序:<input type="number" style="height: 30px;margin-left:30px;width:161px" name="txt" id="order"></div>
                <div>
                    <div><p style="float:left">图片:</p><p style="margin-left:60px">尺寸建议(宽412px 高202px)</p></div>
                    <input id="File1" name="File1" type="file" />
                    <img id="imgurl2" style="height: 22px" />
                </div>
            </div>
            <div class="modal-footer">
                <a href="#" class="btn btn-success" ng-click="reloadRoute()">保存</a>
                <a href="#" class="btn" data-dismiss="modal">关闭</a>
            </div>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderScripts" runat="server">
    <script type="text/javascript">

        $('.treeview-menu .treeviewli6').addClass('active');
        var app = angular.module('lhxApp', ['ng-pagination']);
        var lineid;
        var travelagency;
        app.controller('userCtrl', function ($scope, $http, $window) {
            // $scope.pageCount = 100;
            percount = 10;
            $scope.toggle = function ($event) {
                $("#example").attr("class", "modal fade in");
                lineid = $event.path[2].cells[1].innerText;
                travelagency = $event.path[2].cells[3].innerText;
            };

            var nghttp0 = "../../../ajax/apihandler.ashx?fn=getlinecategorys0";
            $http.get(nghttp0).success(function (response) {
                //  debugger
                var responseCache0 = response;
                var arrayLine = new Array(0);
                for (var i = 0; i < responseCache0.ds.length; i++) {
                    arrayLine.push(responseCache0.ds[i]);
                }
                $scope.linecates = arrayLine;
            });

            var linenamesc = $('#linenamesc').val();
            var agencysc = $('#agencysc').val();
            var lineidsc = $('#lineidsc').val();
            var categorysc = $('#categorysc').val();
            var agencysc2 = $('#agencysc2').val();
            var nghttp = "../../../ajax/apihandler.ashx?fn=getlinesadsearch2&linenamesc=" + linenamesc + "&agencysc=" + agencysc + "&lineidsc=" + lineidsc + "&categorysc=" + categorysc + "&agencysc2=" + agencysc2 + "";
            $http.get(nghttp).success(function (response) {
                //替换旅行社名字
                for (var i = 0; i < response.lines.length;i++){
                    response.lines[i].agency = replaceAgency(response.lines[i].agency);
                }
                $scope.pageCount = Math.ceil(response.lines.length / percount);
                responseCache = response;
                var arrayLine = new Array(0);
                var mypercount = percount > response.lines.length ? response.lines.length : percount;
                for (var i = 0; i < mypercount; i++) {
                    arrayLine.push(responseCache.lines[i]);
                }
                $scope.lines = arrayLine;
            });
            $scope.onPageChange = function () {
                var arrayLine = new Array(0);
                var pagec = responseCache.lines.length - (percount * ($scope.currentPage - 1)) >= percount ? percount * $scope.currentPage : responseCache.lines.length;
                for (var i = percount * ($scope.currentPage - 1) ; i < pagec; i++) {
                    arrayLine.push(responseCache.lines[i]);
                }
                $scope.lines = arrayLine;
            };
            $scope.reloadRoute = function () {
                $.ajax({
                    url: "../../../ajax/recommendHandler.ashx?fn=addrecommend",
                    type: "post",
                    data: {
                        lineTitle: $('#title')[0].value,
                        travelAgency: travelagency,
                        lineId: lineid,
                        order: $('#order')[0].value
                    },
                    success: function (text) {
                        $window.location.reload();
                    }
                });

            }
            $scope.filtcategory = function () {
                //线路列表bg
                var linenamesc = $('#linenamesc').val();
                var agencysc = $('#agencysc').val();
                var lineidsc = $('#lineidsc').val();
                var categorysc = $('#categorysc').val();
                var agencysc2 = $('#agencysc2').val();
                var nghttp = "../../../ajax/apihandler.ashx?fn=getlinesadsearch2&linenamesc=" + linenamesc + "&agencysc=" + agencysc + "&lineidsc=" + lineidsc + "&categorysc=" + categorysc + "&agencysc2=" + agencysc2 + "";
                $http.get(nghttp).success(function (response) {
                    //debugger
                    //替换旅行社名字
                    for (var i = 0; i < response.lines.length; i++) {
                        response.lines[i].agency = replaceAgency(response.lines[i].agency);
                    }
                    $scope.pageCount = Math.ceil(response.lines.length / percount);
                    responseCache2 = response;
                    var arrayLine = new Array(0);
                    var mypercount = percount > responseCache2.lines.length ? responseCache2.lines.length : percount;
                    for (var i = 0; i < mypercount; i++) {
                        arrayLine.push(responseCache2.lines[i]);
                    }
                    $scope.lines = arrayLine;
                });
                $scope.onPageChange = function () {
                    var arrayLine = new Array(0);
                    var pagec = responseCache2.lines.length - (percount * ($scope.currentPage - 1)) >= percount ? percount * $scope.currentPage : responseCache2.lines.length;
                    for (var i = percount * ($scope.currentPage - 1) ; i < pagec; i++) {
                        arrayLine.push(responseCache2.lines[i]);
                    }
                    $scope.lines = arrayLine;
                };
                //线路列表ed
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
        function replaceAgency(agencycode) {
            //var agencycode;
            //debugger
            agencycode = agencycode.replace("SHT", "上海华亭海外旅游有限公司");
            agencycode = agencycode.replace("STD", "旅游事业部");
            agencycode = agencycode.replace("SWD", "上海锦江国际旅游股份有限公司网点分公司");
            agencycode = agencycode.replace("STS", "上海旅行社有限公司");
            agencycode = agencycode.replace("SHZ", "浙江锦旅国旅旅行社有限公司");
            agencycode = agencycode.replace("CTH", "上海中旅国际旅行社有限公司");
            agencycode = agencycode.replace("CYR", "上海中旅杨休国际旅行社有限公司");
            agencycode = agencycode.replace("SIT", "信息中心");
            agencycode = agencycode.replace("SHA", "上海国旅国际旅行社有限公司");
            agencycode = agencycode.replace("BJT", "北京锦江国际旅行社有限公司");
            agencycode = agencycode.replace("SJT", "上海锦江旅游有限公司");
            return agencycode;
        }

    </script>
</asp:Content>
