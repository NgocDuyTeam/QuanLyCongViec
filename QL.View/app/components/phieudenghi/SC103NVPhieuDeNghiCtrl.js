'use strict';
var app = angular.module('uiApp');

app.controller('SC103NVPhieuDeNghiCtrl',
    ['$scope', '$compile', '$resource', 'myAppConfig', 'ngProgress', 'toaster', 'svPhieuDeNghi'
        , 'svDanhMucKhoaPhong', 'svDanhMucCanBo',
        function ($scope, $compile, $resource, myAppConfig, ngProgress, toaster, svPhieuDeNghi
            , svDanhMucKhoaPhong, svDanhMucCanBo) {
            $scope.TuNgay = moment().format('DD/MM/YYYY');
            $scope.DenNgay = moment().format('DD/MM/YYYY');
            $scope.IdKhoa = "";
            $scope.sTrangThai = "DaPhanViec";
            $scope.iPageIndex = 1;
            $scope.iPageSize = "20";
            $scope.IdCanBo = myAppConfig.IdCanBo;
            $scope.DSPhieu = [];
            $scope.DSKhoaPhong = [];
            $scope.DSCanBo = [];

            $scope.refreshData = function (iPageIndex) {
                $scope.iPageIndex = iPageIndex;
                ngProgress.start();
                svPhieuDeNghi.getPhieuDeNghiByPagenv({
                    IdKhoa: $scope.IdKhoa,
                    TuNgay: moment($scope.TuNgay, "DD/MM/YYYY").format("MM/DD/YYYY"),
                    DenNgay: moment($scope.DenNgay, "DD/MM/YYYY").format("MM/DD/YYYY"),
                    sTrangThai: $scope.sTrangThai,
                    iPageIndex: $scope.iPageIndex,
                    iPageSize: $scope.iPageSize,
                    IdCanBo: $scope.IdCanBo
                }).$promise.then(
                    function (d) {
                        $scope.DSPhieu = d.List;
                        $scope.iTotal = d.iTotal != null ? d.iTotal : 0;
                        $scope.iTotalPage = Math.floor(($scope.iTotal - 1) / $scope.iPageSize) + 1;
                        var lstPage = GetlstPage($scope.iTotalPage, $scope.iPageIndex, 'refreshData');
                        $("#lstPage").html($compile(lstPage)($scope));
                        ngProgress.complete();
                    }, function (err) { ngProgress.complete(); });
            }
            $scope.refreshData(1);

            svDanhMucKhoaPhong.GetDanhSachKhoaPhong({
                iPageIndex: -1,
                iPageSize: 1
            }).$promise.then(
                function (d) {
                    $scope.DSKhoaPhong = d.List;
                    $scope.DSKhoaPhong.splice(0, 0, { Ten: "-- Tất cả -- ", Id: "" });
                }, function (err) { });
        

        
            $scope.SaveTrangThaiHT = function (phieu) {
                if (typeof phieu.IdCanBoThucHien === "undefined" || phieu.IdCanBoThucHien == null) {
                    toaster.pop('warning', "Thông báo", "Vui lòng chọn cán bộ.");
                    return;
                }
                ngProgress.start();
                svPhieuDeNghi.SaveTrangThaiHT(phieu).$promise.then(
                    function (d) {
                        toaster.pop('success', "Thông báo", "Lưu thông tin thành công.");
                        ngProgress.complete();
                        $scope.refreshData(1);

                    }, function (err) {
                        ngProgress.complete();
                        $scope.isDisabled = false;
                    });
            }
        }]);