'use strict';
var app = angular.module('uiApp');

app.controller('SC200DanhMucKhoaPhongCtrl',
    ['$scope', '$compile', '$resource', 'myAppConfig', 'ngProgress', 'toaster', 'svDanhMucKhoaPhong',
        function ($scope, $compile, $resource, myAppConfig, ngProgress, toaster, svDanhMucKhoaPhong) {
            $scope.NgayTao = moment().format('YYYY-MM-DD');
            $scope.iPageIndex = 1;
            $scope.iPageSize = 20;
            $scope.KhoaPhong = {};


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
            $scope.addKhoaPhong = function () {
                if (typeof $scope.KhoaPhong.Ma === "undefined" || typeof $scope.KhoaPhong.Ten === "undefined") {
                    toaster.pop('warning', "Thông báo", "Vui lòng Nhập Mã, Tên, Khoa Phòng.");
                    return;
                }
                ngProgress.start();
                $scope.isDisabled = true;
                svDanhMucKhoaPhong.saveKhoaPhong($scope.KhoaPhong).$promise.then(
                    function (d) {
                        toaster.pop('success', "Thông báo", "Lưu thông tin thành công.");
                        ngProgress.complete();
                        window.location.href = "/DanhMuc/SC200_DanhMucKhoaPhong";
                    }, function (err) {
                        ngProgress.complete();
                        $scope.isDisabled = false;
                    }
                );
            }
            $scope.DeleteKhoaPhong = function (IdKhoa) {
                svDanhMucKhoaPhong.DeleteKhoaPhongById({
                    IdKhoa: IdKhoa
                }).$promise.then(
                    function (d) {
                        toaster.pop('success', "Thông báo", "Xóa thành công.");
                        $scope.loadDMKhoaPhong(1);
                    }, function (err) { ngProgress.complete(); });
            }
            $scope.huy = function () {
                window.location.href = "/DanhMuc/SC200_DanhMucKhoaPhong";
            }

            $scope.loadDMKhoaPhong(1);


        }]);