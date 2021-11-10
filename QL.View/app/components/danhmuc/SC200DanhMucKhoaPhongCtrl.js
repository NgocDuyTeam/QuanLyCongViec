'use strict';
var app = angular.module('uiApp');

app.controller('SC200DanhMucKhoaPhongCtrl',
    ['$scope', '$compile', '$resource', 'myAppConfig', 'ngProgress', 'toaster', 'svDanhMucKhoaPhong',
        function ($scope, $compile, $resource, myAppConfig, ngProgress, toaster, svDanhMucKhoaPhong) {
            $scope.NgayTao = moment().format('YYYY-MM-DD');
            $scope.loadDMKhoaPhong = function () {
                svDanhMucKhoaPhong.GetDanhSachKhoaPhong().$promise.then(
                    function (d) {
                        $scope.DSKhoaPhong = d.List;
                    }, function (err) { ngProgress.complete(); });
            }

            $scope.loadDMKhoaPhong();

        }]);