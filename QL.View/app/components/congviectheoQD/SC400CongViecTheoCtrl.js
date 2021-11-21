'use strict';
var app = angular.module('uiApp');

app.controller('SC400CongViecTheoCtrl',
    ['$scope', '$compile', '$resource', '$filter', 'myAppConfig', 'ngProgress', 'toaster', 'svBienBanNghiemThu', 'svMauPhieuIn',
        'svDanhMucKhoaPhong', 'svCongViectheoQD',
        function ($scope, $compile, $resource, $filter, myAppConfig, ngProgress, toaster, svBienBanNghiemThu, svMauPhieuIn
            , svDanhMucKhoaPhong, svCongViectheoQD) {

            $scope.TuNgay = moment().format('DD/MM/YYYY');
            $scope.DenNgay = moment().format('DD/MM/YYYY');
            $scope.iPageIndex = 1;
            $scope.iPageSize = "20";
            $scope.DSCongViec = [];
            $scope.DSKhoaPhong = [];
            $scope.CongViec = {};
            $scope.IdKhoa = "";
            $scope.IsKhoa = false;
            if (myAppConfig.Role == "Khoa") {
                $scope.IsKhoa = true;
                $scope.IdKhoa = myAppConfig.IdKhoa;
            }
            $scope.refreshData = function (iPageIndex) {
                $scope.iPageIndex = iPageIndex;
                ngProgress.start();
                svCongViectheoQD.getByPage({
                    TuNgay: moment($scope.TuNgay, "DD/MM/YYYY").format("MM/DD/YYYY"),
                    DenNgay: moment($scope.DenNgay, "DD/MM/YYYY").format("MM/DD/YYYY"),
                    iPageIndex: $scope.iPageIndex,
                    iPageSize: $scope.iPageSize,
                    IdKhoa: $scope.IdKhoa
                }).$promise.then(
                    function (d) {
                        $scope.DSCongViec = d.List;
                        $scope.iTotal = d.iTotal != null ? d.iTotal : 0;
                        $scope.iTotalPage = Math.floor(($scope.iTotal - 1) / $scope.iPageSize) + 1;
                        var lstPage = GetlstPage($scope.iTotalPage, $scope.iPageIndex, 'refreshData');
                        $("#lstPage").html($compile(lstPage)($scope));
                        ngProgress.complete();
                    }, function (err) { ngProgress.complete(); });
            }
            $scope.refreshData(1);
            $scope.loadThongTinPhieu = function (IdPhieu) {
                svPhieuDeNghi.GetPhieuById({
                    IdPhieu: IdPhieu
                }).$promise.then(
                    function (d) {
                        $scope.Phieu = d;
                        $scope.BienBan.TenKhoa = $scope.Phieu.TenKhoa;
                    }, function (err) { ngProgress.complete(); });
            }

            svDanhMucKhoaPhong.GetDanhSachKhoaPhong({
                iPageIndex: -1,
                iPageSize: -1
            }).$promise.then(
                function (d) {
                    $scope.DSKhoaPhong = d.List;
                }, function (err) { });

            $scope.ShowAddOrUpdateCongViec = function (cv) {
                if (cv != null) {
                    $scope.openPopup();
                    $scope.CongViec = cv;
                    //$scope.CongViec.IdCanBo = myAppConfig.IdCanBo;
                    for (var i = 0; i < $scope.DSKhoaPhong.length; i++) {
                        if ($scope.CongViec.DanhSachKhoa.indexOf($scope.DSKhoaPhong[i].Id) > -1) {
                            $scope.DSKhoaPhong[i].IsCheck = true;
                        } else {
                            $scope.DSKhoaPhong[i].IsCheck = false;
                        }
                    }
                } else {
                    $scope.openPopup();
                    $scope.CongViec = {};
                    $scope.CongViec.IdCanBo = myAppConfig.IdCanBo;
                    for (var i = 0; i < $scope.DSKhoaPhong.length; i++) {
                        $scope.DSKhoaPhong[i].IsCheck = false;
                    }
                }
            }
            $scope.openPopup = function () {
                $("#popupCongViec").bPopup({ escClose: false, modalClose: false });
                $("#popupCongViec").show();
            };
            $scope.closePopup = function () {
                $("#popupCongViec").bPopup({}).close();
            };
            $scope.saveCongViec = function () {
                ngProgress.start();
                $scope.isDisabled = true;
                $scope.CongViec.DanhSachKhoa = "";
                for (var i = 0; i < $scope.DSKhoaPhong.length; i++) {
                    if ($scope.DSKhoaPhong[i].IsCheck) {
                        $scope.CongViec.DanhSachKhoa += $scope.DSKhoaPhong[i].Id + ";";
                    }
                }
                svCongViectheoQD.saveCongViec($scope.CongViec).$promise.then(
                    function (d) {
                        toaster.pop('success', "Thông báo", "Lưu thông tin thành công.");
                        $scope.closePopup();
                        $scope.refreshData(1);
                        ngProgress.complete();
                        $scope.isDisabled = false;
                    }, function (err) {
                        ngProgress.complete();
                        $scope.isDisabled = false;
                    });
            }
        }]);