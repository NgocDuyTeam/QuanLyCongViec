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
                            setTimeout(function () { newWin.close(); }, 100);
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
            $scope.PrintBienBan = function (phieu) {
                var bb = phieu.lstBienBan[0];
                svMauPhieuIn.GetByMa({
                    sMa: 'BienBanNghiemThu1'
                }).$promise.then(
                    function (d) {
                        var ngaybatdau = "Bắt đầu " + moment(bb.NgayBatDau).format("HH")
                            + " giờ " + moment(bb.NgayBatDau).format("mm")
                            + " phút, ngày " + moment(bb.NgayBatDau).format("DD")
                            + " tháng " + moment(bb.NgayBatDau).format("MM")
                            + " năm " + moment(bb.NgayBatDau).format("YYYY");
                        var ngayketthuc = "Kết thúc " + moment(bb.NgayKetThuc).format("HH")
                            + " giờ " + moment(bb.NgayKetThuc).format("mm")
                            + " phút, ngày " + moment(bb.NgayKetThuc).format("DD")
                            + " tháng " + moment(bb.NgayKetThuc).format("MM")
                            + " năm " + moment(bb.NgayKetThuc).format("YYYY");
                        var strPrint = d.NoiDung.replace("[TenKhoa]", bb.TenKhoa)
                            .replace("[PhongQuanTriHoTen]", bb.ObjPhongQuanTri.HoVaTen)
                            .replace("[PhongQuanTriChucVu]", bb.ObjPhongQuanTri.ChucVu)
                            .replace("[Ngay]", moment(bb.NgayBatDau).format("DD"))
                            .replace("[Thang]", moment(bb.NgayBatDau).format("MM"))
                            .replace("[Nam]", moment(bb.NgayBatDau).format("YYYY"))
                            .replace("[NgayBatDau]", ngaybatdau)
                            .replace("[NgayKetThuc]", ngayketthuc)
                            .replace("[GoiThau]", bb.GoiThau.replace(/\n/g, "<br/>"))
                            .replace("[DoiTuongNghiemThu]", bb.DoiTuongNghiemThu.replace(/\n/g, "<br/>"))
                            .replace("[TenNhaThau]", bb.ObjNhaThau.TenNhaThau)
                            .replace("[NhaThauHoTen]", bb.ObjNhaThau.HoVaTen)
                            .replace("[NhaThauChucVu]", bb.ObjNhaThau.ChucVu)
                            .replace("[HopDongSo]", bb.HopDongKinhTe)
                            ;

                        var htmlTable = "";
                        for (var i = 0; i < bb.LstCongViec.length; i++) {
                            htmlTable += "<tr>";
                            htmlTable += "<td style='border: 1px solid black;padding-left:5px'>" + bb.LstCongViec[i].STT + "</td>";
                            htmlTable += "<td style='border: 1px solid black;padding-left:5px'>" + bb.LstCongViec[i].NoiDung + "</td>";
                            htmlTable += "<td style='border: 1px solid black;padding-left:5px;text-align:center'>" + bb.LstCongViec[i].DonVi + "</td>";
                            htmlTable += "<td style='border: 1px solid black;padding-left:5px;text-align:center'>" + bb.LstCongViec[i].KhoiLuong + "</td>";
                            htmlTable += "<td style='border: 1px solid black;padding-left:5px'>" + bb.LstCongViec[i].GhiChu + "</td>";
                            htmlTable += "</tr>";
                        }
                        strPrint = strPrint.replace("<tr></tr>", htmlTable);
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