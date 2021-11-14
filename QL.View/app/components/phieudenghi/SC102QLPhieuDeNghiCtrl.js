'use strict';
var app = angular.module('uiApp');

app.controller('SC102QLPhieuDeNghiCtrl',
    ['$scope', '$compile', '$resource', 'myAppConfig', 'ngProgress', 'toaster', 'svPhieuDeNghi', 'svMauPhieuIn'
        , 'svDanhMucKhoaPhong', 'svDanhMucCanBo',
        function ($scope, $compile, $resource, myAppConfig, ngProgress, toaster, svPhieuDeNghi, svMauPhieuIn
            , svDanhMucKhoaPhong, svDanhMucCanBo) {
            $scope.TuNgay = moment().format('DD/MM/YYYY');
            $scope.DenNgay = moment().format('DD/MM/YYYY');
            $scope.IdKhoa = "";
            $scope.sTrangThai = "GuiYeuCau";
            $scope.iPageIndex = 1;
            $scope.iPageSize = "20";
            $scope.IdCanBo = "00000000-0000-0000-0000-000000000000";
            $scope.DSPhieu = [];
            $scope.DSKhoaPhong = [];
            $scope.DSCanBo = [];

            $scope.refreshData = function (iPageIndex) {
                $scope.iPageIndex = iPageIndex;
                ngProgress.start();
                svPhieuDeNghi.getPhieuDeNghiByPage({
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
            svDanhMucCanBo.GetDanhSachByRole({
                sRole: 'NhanVien'
            }).$promise.then(
                function (d) {
                    $scope.DSCanBo = d.List;
                    $scope.DSCanBo.splice(0, 0, { HoVaTen: "-- Chọn cán bộ -- ", Id: '00000000-0000-0000-0000-000000000000' });
                }, function (err) { });

            $scope.PrintPhieu = function (phieu) {
                svMauPhieuIn.GetByMa({
                    sMa: 'PhieuDeNghi'
                }).$promise.then(
                    function (d) {
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
                        });

                    }, function (err) { ngProgress.complete(); });
            }
            $scope.SavePhanCong = function (phieu) {
                if (typeof phieu.IdCanBoThucHien === "undefined" || phieu.IdCanBoThucHien == null) {
                    toaster.pop('warning', "Thông báo", "Vui lòng chọn cán bộ.");
                    return;
                }
                ngProgress.start();
                svPhieuDeNghi.savePhanCongPhieuDeNghi(phieu).$promise.then(
                    function (d) {
                        toaster.pop('success', "Thông báo", "Lưu thông tin thành công.");
                        ngProgress.complete();
                    }, function (err) {
                        ngProgress.complete();
                        $scope.isDisabled = false;
                    });
            }
        }]);