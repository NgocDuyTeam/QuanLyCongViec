'use strict';
var app = angular.module('uiApp');

app.controller('SC101DSPhieuDeNghiCtrl',
    ['$scope', '$compile', '$resource', 'myAppConfig', 'ngProgress', 'toaster', 'svPhieuDeNghi','svMauPhieuIn',
        function ($scope, $compile, $resource, myAppConfig, ngProgress, toaster, svPhieuDeNghi, svMauPhieuIn) {
            $scope.TuNgay = moment().format('DD/MM/YYYY');
            $scope.DenNgay = moment().format('DD/MM/YYYY');
            $scope.IdKhoa = myAppConfig.IdKhoa;
            $scope.sTrangThai = "GuiYeuCau";
            $scope.iPageIndex = 1;
            $scope.iPageSize = "20";
            $scope.DSPhieu = [];

            $scope.refreshData = function (iPageIndex) {
                $scope.iPageIndex = iPageIndex;
                ngProgress.start();
                svPhieuDeNghi.getPhieuDeNghiByPage({
                    IdKhoa: $scope.IdKhoa,
                    TuNgay: moment($scope.TuNgay, "DD/MM/YYYY").format("MM/DD/YYYY"),
                    DenNgay: moment($scope.DenNgay, "DD/MM/YYYY").format("MM/DD/YYYY"),
                    sTrangThai: $scope.sTrangThai,
                    iPageIndex: $scope.iPageIndex,
                    iPageSize: $scope.iPageSize
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
            $scope.DeletePhieu = function (IdPhieu) {
                svPhieuDeNghi.DeletePhieuById({
                    IdPhieu: IdPhieu
                }).$promise.then(
                    function (d) {
                        toaster.pop('success', "Thông báo", "Xóa thành công.");
                        $scope.refreshData(1);
                    }, function (err) { ngProgress.complete(); });
            }
            $scope.PrintPhieu = function (phieu) {
                svMauPhieuIn.GetByMa({
                    sMa: 'PhieuDeNghi'
                }).$promise.then(
                    function (d) {
                        var strPrint = d.NoiDung.replace("[TenKhoa]", phieu.TenKhoa)
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
        }]);