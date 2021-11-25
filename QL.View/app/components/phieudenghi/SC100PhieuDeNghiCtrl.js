'use strict';
var app = angular.module('uiApp');

app.controller('SC100PhieuDeNghiCtrl',
    ['$scope', '$compile', '$resource', 'myAppConfig', 'ngProgress', 'toaster', 'svPhieuDeNghi', 'svDanhMucCongViec', 'svMauPhieuIn',
        function ($scope, $compile, $resource, myAppConfig, ngProgress, toaster, svPhieuDeNghi, svDanhMucCongViec, svMauPhieuIn) {
            $scope.NgayTao = moment().format('YYYY-MM-DD');
            $scope.TenKhoa = myAppConfig.TenKhoa;
            $scope.Phieu = {};
            $scope.Phieu.IdCanBoDeNghi = myAppConfig.IdCanBo;
            $scope.Phieu.IdKhoa = myAppConfig.IdKhoa;
            $scope.Phieu.TrangThai = "GuiYeuCau";
            $scope.Phieu.TenKhoa = myAppConfig.TenKhoa;
            $scope.Phieu.NgayTao = moment().format('MM/DD/YYYY');
            $scope.loadDMCongViec = function () {
                svDanhMucCongViec.GetDanhSachCongViec().$promise.then(
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
                        svMauPhieuIn.GetByMa({
                            sMa: 'PhieuDeNghi'
                        }).$promise.then(
                            function (d) {
                                var phieu = $scope.Phieu;
                                var strPrint = d.NoiDung.replace("[TenKhoa]", phieu.TenKhoa.toUpperCase())
                                    .replace("[TenKhoa1]", phieu.TenKhoa)
                                    .replace("[NoiDung]", phieu.NoiDung.replace(/\n/g, "<br/>"))
                                    .replace("[CongViec]", phieu.TenCongViec)
                                    .replace("[Ngay]", moment(phieu.NgayTao).format("DD"))
                                    .replace("[Thang]", moment(phieu.NgayTao).format("MM"))
                                    .replace("[Nam]", moment(phieu.NgayTao).format("YYYY"));
                                $("body").append(htmlMessageBox);
                                $("#printMessageBox").css("opacity", 444);
                                $("#printMessageBox").delay(1000).animate({ opacity: 0 }, 700, function () {
                                    $(this).remove();
                                    var newWin = window.open('', 'Print-Window', 'width=1366,height=768');
                                    newWin.document.open();
                                    newWin.document.write('<html><body onload="window.print()">' + strPrint + '</body></html>');
                                    newWin.document.close();
                                    setTimeout(function () { newWin.close(); }, 10);
                                    window.location.href = "/PhieuDeNghi/SC101_DSPhieuDeNghi";
                                });
                            }, function (err) { ngProgress.complete(); });
                    }, function (err) {
                        ngProgress.complete();
                        $scope.isDisabled = false;
                    });
            }
            $scope.loadDMCongViec();

         
        }]);