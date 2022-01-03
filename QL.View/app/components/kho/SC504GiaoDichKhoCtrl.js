'use strict';
var app = angular.module('uiApp');

app.controller('SC504GiaoDichKhoCtrl',
    ['$scope', '$compile', '$resource', 'myAppConfig', 'ngProgress', 'toaster', 'svKho', 'svTuDien',
        function ($scope, $compile, $resource, myAppConfig, ngProgress, toaster, svKho, svTuDien) {
            $scope.iStatus = "";
            $scope.iPageIndex = 1;
            $scope.iPageSize = "20";
            $scope.TuNgay = moment().format('DD/MM/YYYY');
            $scope.DenNgay = moment().format('DD/MM/YYYY');
            $scope.refreshData = function (iPageIndex) {
                $scope.iPageIndex = iPageIndex;
                svKho.GetDanhSachGiaoDich({
                    iStatus: $scope.iStatus,
                    TuNgay: moment($scope.TuNgay, "DD/MM/YYYY").format("MM/DD/YYYY"),
                    DenNgay: moment($scope.DenNgay, "DD/MM/YYYY").format("MM/DD/YYYY"),
                    iPageIndex: $scope.iPageIndex,
                    iPageSize: $scope.iPageSize,
                }).$promise.then(
                    function (d) {
                        $scope.DsGiaoDich = d.List;
                        $scope.iTotal = d.iTotal != null ? d.iTotal : 0;
                        $scope.iTotalPage = Math.floor(($scope.iTotal - 1) / $scope.iPageSize) + 1;
                        var lstPage = GetlstPage($scope.iTotalPage, $scope.iPageIndex, 'refreshData');
                        $("#lstPage").html($compile(lstPage)($scope));
                    }, function (err) { ngProgress.complete(); });
            }
            $scope.refreshData(1);
//..................................................................................................
        }]);