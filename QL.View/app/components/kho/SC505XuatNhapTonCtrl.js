'use strict';
var app = angular.module('uiApp');

app.controller('SC505XuatNhapTonCtrl',
    ['$scope', '$compile', '$resource', 'myAppConfig', 'ngProgress', 'toaster', 'svKho', 'svTuDien',
        function ($scope, $compile, $resource, myAppConfig, ngProgress, toaster, svKho, svTuDien) {
            $scope.TuNgay = moment().format('DD/MM/YYYY');
            $scope.DenNgay = moment().format('DD/MM/YYYY');
            $scope.sSearch = "";
            $scope.FileExcelName = "";
            $scope.refreshData = function () {
                ngProgress.start();
                svKho.BCXuatNhapTon({
                    sSearch: $scope.sSearch,
                    TuNgay: moment($scope.TuNgay, "DD/MM/YYYY").format("MM/DD/YYYY"),
                    DenNgay: moment($scope.DenNgay, "DD/MM/YYYY").format("MM/DD/YYYY")
                }).$promise.then(
                    function (d) {
                        $scope.DsGiaoDich = d.List;
                        $scope.FileExcelName = d.FileExcelName;
                        ngProgress.complete();
                    }, function (err) { ngProgress.complete(); });
            }
            $scope.refreshData();
            $scope.XuatExcel = function () {
                window.open($scope.FileExcelName);
            }
//..................................................................................................
        }]);