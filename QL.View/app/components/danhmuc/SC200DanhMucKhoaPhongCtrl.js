'use strict';
var app = angular.module('uiApp');

app.controller('SC200DanhMucKhoaPhongCtrl',
    ['$scope', '$compile', '$resource', 'myAppConfig', 'ngProgress', 'toaster', 'svDanhMucKhoaPhong',
        function ($scope, $compile, $resource, myAppConfig, ngProgress, toaster, svDanhMucKhoaPhong) {
            $scope.NgayTao = moment().format('YYYY-MM-DD');
            $scope.iPageIndex = 1;
            $scope.iPageSize = 1;
            $scope.KhoaPhong = {};


            $scope.AddKhoaPhong = function()

            $scope.loadDMKhoaPhong = function (iPageIndex) {
                $scope.iPageIndex = iPageIndex;
                ngProgress.start();
                svDanhMucKhoaPhong.GetDanhSachKhoaPhong(
                    {
                        iPageIndex: $scope.iPageIndex,
                        iPageSize: $scope.iPageSize
                    }
                ).$promise.then(
                    function (d) {
                        $scope.DSKhoaPhong = d.List;
                        $scope.iTotal = d.iTotal != null ? d.iTotal : 0;
                        $scope.iTotalPage = Math.floor(($scope.iTotal - 1) / $scope.iPageSize) + 1;
                        var lstPage = GetlstPage($scope.iTotalPage, $scope.iPageIndex, 'loadDMKhoaPhong');
                        $("#lstPage").html($compile(lstPage)($scope));
                        ngProgress.complete();
                    }, function (err) { ngProgress.complete(); });
            }

            $scope.loadDMKhoaPhong(1);


        }]);