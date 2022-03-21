'use strict';
var app = angular.module('uiApp');

app.controller('SC506BCXuatKhoaPhongCtrl',
    ['$scope', '$compile', '$resource', 'myAppConfig', 'ngProgress', 'toaster', 'svKho', 'svDanhMucKhoaPhong',
        function ($scope, $compile, $resource, myAppConfig, ngProgress, toaster, svKho, svDanhMucKhoaPhong) {
            $scope.TuNgay = moment().format('DD/MM/YYYY');
            $scope.DenNgay = moment().format('DD/MM/YYYY');
            $scope.IdKhoa = "00000000-0000-0000-0000-000000000000";
            $scope.sSearch = "";
            $scope.FileExcelName = "";
            svDanhMucKhoaPhong.GetDanhSachKhoaPhong({
                iPageIndex: -1,
                iPageSize: 1
            }).$promise.then(
                function (d) {
                    $scope.DSKhoaPhong = d.List;
                    $scope.DSKhoaPhong.splice(0, 0, { Ten: "-- Tất cả -- ", Id: "00000000-0000-0000-0000-000000000000" });
                }, function (err) { });

            $scope.refreshData = function () {
                if ($scope.IdKhoa == "") {
                    $scope.DsGiaoDich = [];
                }
                ngProgress.start();
                svKho.BCXuatKhoaPhong({
                    sSearch: $scope.sSearch,
                    TuNgay: moment($scope.TuNgay, "DD/MM/YYYY").format("MM/DD/YYYY"),
                    DenNgay: moment($scope.DenNgay, "DD/MM/YYYY").format("MM/DD/YYYY"),
                    IdKhoa: $scope.IdKhoa
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