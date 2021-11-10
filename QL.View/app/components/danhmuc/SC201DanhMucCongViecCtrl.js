'use strict';
var app = angular.module('uiApp');

app.controller('SC201DanhMucCongViecCtrl',
    ['$scope', '$compile', '$resource', 'myAppConfig', 'ngProgress', 'toaster', 'svDanhMucCongViec',
        function ($scope, $compile, $resource, myAppConfig, ngProgress, toaster, svDanhMucCongViec) {
            $scope.NgayTao = moment().format('YYYY-MM-DD');
            $scope.loadDMCongViec = function () {
                svDanhMucCongViec.GetDanhSachCongViec().$promise.then(
                    function (d) {
                        $scope.DSCongViec = d.List;
                    }, function (err) { ngProgress.complete(); });
            }

            $scope.loadDMCongViec();

        }]);