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
            $scope.DeleteCongViec = function (IdCV) {
                svCongViectheoQD.DeleteCongViecById({
                    IdCongViec: IdCV
                }).$promise.then(
                    function (d) {
                        toaster.pop('success', "Thông báo", "Xóa thành công.");
                        $scope.refreshData(1);
                    }, function (err) {
                        toaster.pop('error', "Thông báo", err.data);
                        ngProgress.complete();
                    });
            }
            $scope.ShowPopupBienBan = function (cv) {
                $scope.CongViec = cv;
                $("#popupBienban").bPopup({ escClose: false, modalClose: false });
                $("#popupBienban").show();
            };
            $scope.closePopupBienBan = function () {
                $("#popupBienban").bPopup({}).close();
            };
            $scope.DeleteBienBan = function (IdBB, index) {
                svBienBanNghiemThu.DeleteBienBanById({
                    IdBienBan: IdBB
                }).$promise.then(
                    function (d) {
                        toaster.pop('success', "Thông báo", "Xóa thành công.");
                        $scope.CongViec.lstBienBan.splice(index, 1);
                    }, function (err) {
                        toaster.pop('error', "Thông báo", err.data);
                        ngProgress.complete();
                    });
            }
            $scope.PrintBienBan = function (bb) {
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
                        var strPrint = d.NoiDung
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
                        if ($scope.IsKhoa) {
                            strPrint = strPrint.replace("[TenKhoa]", myAppConfig.TenKhoa);
                        } else {
                            strPrint = strPrint.replace("[TenKhoa]", "............................................");
                        }
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
                        $("#printMessageBox").css("opacity", 99944);
                        $("#printMessageBox").delay(1000).animate({ opacity: 0 }, 700, function () {
                            $(this).remove();
                            var newWin = window.open('', 'Print-Window', 'width=1366,height=768');
                            newWin.document.open();
                            newWin.document.write('<html><body onload="window.print()">' + strPrint + '</body></html>');
                            newWin.document.close();
                            setTimeout(function () { newWin.close(); }, 50);
                        });
                    }, function (err) { ngProgress.complete(); });
            }
        }]);