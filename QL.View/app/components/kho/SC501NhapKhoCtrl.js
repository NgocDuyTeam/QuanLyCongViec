'use strict';
var app = angular.module('uiApp');

app.controller('SC501NhapKhoCtrl',
    ['$scope', '$compile', '$resource', 'myAppConfig', 'ngProgress', 'toaster', 'svKho', 'svTuDien', 'svDanhMucSanPham',
        function ($scope, $compile, $resource, myAppConfig, ngProgress, toaster, svKho, svTuDien, svDanhMucSanPham) {
            $scope.sSearch = "";
            $scope.SanPham = {};
            $scope.DsSanPham = "";
            $scope.GiaoDich = {
                ChiTiet: [],
                LoaiGiaoDich: 1,
                IdNguoiTao: myAppConfig.IdCanBo,
                IdKhoa: myAppConfig.IdKhoa
            };
            $scope.refreshSanPham = function (key) {
                svDanhMucSanPham.GetDanhSach({
                    sSearch: key,
                    iPageIndex: 1,
                    iPageSize: 20,
                }).$promise.then(
                    function (d) {
                        $scope.DsSanPham = d.List;
                        $scope.sanpham = { TenSanPham: 'Chọn sản phẩm' };
                    }, function (err) { });
            }
            $scope.AddSanPham = function () {
                if (typeof $scope.SanPham.Id === 'undefined') {
                    toaster.pop('warning', "Thông báo", "Vui lòng chọn sản phẩm.");
                    return;
                }
                if (typeof $scope.SanPham.SoLuong === 'undefined' || $scope.SanPham.SoLuong == '' || $scope.SanPham.SoLuong == '0') {
                    toaster.pop('warning', "Thông báo", "Vui lòng nhập số lượng.");
                    $('#txtSoLuong').focus();
                    return;
                }
                $scope.SanPham.MaSanPham = $scope.SanPham.Ma;
                $scope.SanPham.IdSanPham = $scope.SanPham.Id;
                $scope.GiaoDich.ChiTiet.push($scope.SanPham);
                $scope.SanPham = {};
                $scope.sanpham = { TenSanPham: 'Chọn sản phẩm' };
            }
            $scope.DeleteSanPham = function (index) {
                $scope.GiaoDich.ChiTiet.splice(index, 1);
            }
            $scope.$watch('sanpham', function (newVal, oldVal) {
                if (typeof newVal === 'undefined' || newVal == "") return;
                $scope.SanPham = newVal;
            });
            $scope.NhapKho = function () {
                if ($scope.GiaoDich.ChiTiet.length == 0) {
                    toaster.pop('warning', "Thông báo", "Chưa có sản phẩm nào được chọn.");
                    return;
                }
                ngProgress.start();
                $scope.isDisabled = true;
                svKho.CreateGiaoDichKho($scope.GiaoDich).$promise.then(
                    function (d) {
                        setTimeout(function () { toaster.pop('success', "Thông báo", "Nhập kho thành công."); }, 2000);
                        ngProgress.complete();
                        $scope.isDisabled = false;
                        window.location.href = "/Kho/SC503TonKho";
                    }, function (err) {
                        toaster.pop('error', "Thông báo", err.data);
                        ngProgress.complete();
                        $scope.isDisabled = false;
                    });
            }
            //Xem chi tiet..................................................................................................
            $scope.isView = false;
            var urlObj = $.deparam.querystring();
            if (urlObj.idGiaoDich) {
                $scope.isView = true;
                svKho.GetGiaoDichChitiet({ IdGiaoDich: urlObj.idGiaoDich}).$promise.then(
                    function (d) {
                        $scope.GiaoDich = d;
                        ngProgress.complete();
                    }, function (err) {
                        ngProgress.complete();
                    });
            };
            $scope.BackGiaoDich = function () {
                window.location.href = "/Kho/SC504GiaoDichKho";
            }

        }]);