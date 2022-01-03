'use strict';
var app = angular.module('uiApp');

app.controller('SC203DanhMucSanPhamCtrl',
    ['$scope', '$compile', '$resource', 'myAppConfig', 'ngProgress', 'toaster', 'svDanhMucSanPham', 'svTuDien',
        function ($scope, $compile, $resource, myAppConfig, ngProgress, toaster, svDanhMucSanPham, svTuDien) {
            $scope.sSearch = "";
            $scope.iPageIndex = 1;
            $scope.iPageSize = "20";
            $scope.refreshData = function (iPageIndex) {
                $scope.iPageIndex = iPageIndex;
                svDanhMucSanPham.GetDanhSach({
                    sSearch: $scope.sSearch,
                    iPageIndex: $scope.iPageIndex,
                    iPageSize: $scope.iPageSize,
                }).$promise.then(
                    function (d) {
                        $scope.DSSanPham = d.List;
                        $scope.iTotal = d.iTotal != null ? d.iTotal : 0;
                        $scope.iTotalPage = Math.floor(($scope.iTotal - 1) / $scope.iPageSize) + 1;
                        var lstPage = GetlstPage($scope.iTotalPage, $scope.iPageIndex, 'refreshData');
                        $("#lstPage").html($compile(lstPage)($scope));
                    }, function (err) { ngProgress.complete(); });
            }
            svTuDien.GetTuDienByLoai({
                sLoaiTuDien: 'DonViTinh'
            }).$promise.then(
                function (d) {
                    $scope.DSDonVitinh = d.List;
                }, function (err) { ngProgress.complete(); });
            $scope.refreshData(1);
//..................................................................................................
            $scope.ShowAddOrUpdate = function (sp) {
                if (sp != null) {
                    $scope.openPopup();
                    $scope.SanPham = sp;
                } else {
                    $scope.openPopup();
                    $scope.SanPham = {};
                }
                $('#txtTen').focus();
            }
            $scope.openPopup = function () {
                $("#popupSanPham").bPopup({ escClose: false, modalClose: false });
                $("#popupSanPham").show();
            };
            $scope.closePopup = function () {
                $("#popupSanPham").bPopup({}).close();
            };
            $scope.saveSanPham = function () {
                ngProgress.start();
                $scope.isDisabled = true;
                if ($scope.SanPham.TenSanPham == "" || typeof $scope.SanPham.IdDonVi == "undefined") {
                    toaster.pop('warning', "Thông báo", "Vui lòng nhập đủ thông tin.");
                    return;
                }
                svDanhMucSanPham.AddOrUpdate($scope.SanPham).$promise.then(
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
            $scope.DeleteSanPham = function (IdSanPham) {
                svDanhMucSanPham.DeleteById({
                    IdSanPham: IdSanPham
                }).$promise.then(
                    function (d) {
                        toaster.pop('success', "Thông báo", "Xóa thành công.");
                        $scope.refreshData(1);
                    }, function (err) {
                        toaster.pop('error', "Thông báo", err.data);
                        ngProgress.complete();
                    });
            }
        }]);