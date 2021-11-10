'use strict';
var app = angular.module('uiApp');

app.controller('SC100PhieuDeNghiCtrl',
    ['$scope', '$compile', '$resource', 'myAppConfig', 'ngProgress', 'toaster', 'svPhieuDeNghi', 'svDMCongViec',
        function ($scope, $compile, $resource, myAppConfig, ngProgress, toaster, svPhieuDeNghi, svDMCongViec) {
            $scope.NgayTao = moment().format('YYYY-MM-DD');
            $scope.TenKhoa = myAppConfig.TenKhoa;
            $scope.Phieu = {};
            $scope.Phieu.IdCanBoDeNghi = myAppConfig.IdCanBo;
            $scope.Phieu.IdKhoa = myAppConfig.IdKhoa;
            $scope.Phieu.TrangThai = "GuiYeuCau";
            $scope.loadDMCongViec = function () {
                svDMCongViec.GetDanhSachCongViec().$promise.then(
                    function (d) {
                        $scope.DSCongViec = d.List;
                    }, function (err) { ngProgress.complete(); });
            }
            $scope.loadThongTinPhieu = function (IdPhieu) {
                svPhieuDeNghi.GetPhieuById({
                    IdPhieu: IdPhieu
                }).$promise.then(
                    function (d) {
                        $scope.Phieu = d;
                    }, function (err) { ngProgress.complete(); });
            }
            var urlObj = $.deparam.querystring();
            if (urlObj.idPhieu) {
                $scope.loadThongTinPhieu(urlObj.idPhieu);
            };
            
            $scope.savePhieu = function () {
                if (typeof $scope.Phieu.IdCongViec === "undefined" || typeof $scope.Phieu.NoiDung === "undefined") {
                    toaster.pop('warning', "Thông báo", "Vui lòng chọn công việc.");
                    return;
                }
                ngProgress.start();
                $scope.isDisabled = true;
                svPhieuDeNghi.savePhieuDeNghi($scope.Phieu).$promise.then(
                    function (d) {
                        toaster.pop('success', "Thông báo", "Lưu thông tin thành công.");
                        ngProgress.complete();
                        window.location.href = "/PhieuDeNghi/SC101_DSPhieuDeNghi";
                    }, function (err) {
                        ngProgress.complete();
                        $scope.isDisabled = false;
                    });

            }
            $scope.loadDMCongViec();



            $scope.openPopupDemo = function () {
                $("#modelDemo").bPopup({ escClose: false, modalClose: false });
                $("#modelDemo").show();
            };
            $scope.closePopupDemo = function () {
                $("#modelDemo").bPopup({}).close();
            };
        }]);