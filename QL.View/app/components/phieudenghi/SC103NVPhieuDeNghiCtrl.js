'use strict';
var app = angular.module('uiApp');

app.controller('SC103NVPhieuDeNghiCtrl',
    ['$scope', '$compile', '$resource', 'myAppConfig', 'ngProgress', 'toaster', 'svPhieuDeNghi'
        , 'svDanhMucKhoaPhong', 'svMauPhieuIn', 'svKho',
        function ($scope, $compile, $resource, myAppConfig, ngProgress, toaster, svPhieuDeNghi
            , svDanhMucKhoaPhong, svMauPhieuIn, svKho) {
            $scope.TuNgay = moment().format('DD/MM/YYYY');
            $scope.DenNgay = moment().format('DD/MM/YYYY');
            $scope.IdKhoa = "";
            $scope.sTrangThai = "DaPhanViec";
            $scope.iPageIndex = 1;
            $scope.iPageSize = "20";
            $scope.IdCanBo = myAppConfig.IdCanBo;
            $scope.SoDienThoai = myAppConfig.SoDienThoai;
            $scope.DSPhieu = [];
            $scope.DSKhoaPhong = [];
            $scope.DSCanBo = [];

            $scope.refreshData = function (iPageIndex) {
                $scope.iPageIndex = iPageIndex;
                ngProgress.start();
                svPhieuDeNghi.getPhieuDeNghiByPagenv({
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

            $scope.SaveTrangThaiHT = function (phieu) {
                if (typeof phieu.IdCanBoThucHien === "undefined" || phieu.IdCanBoThucHien == null) {
                    toaster.pop('warning', "Thông báo", "Vui lòng chọn cán bộ.");
                    return;
                }
                ngProgress.start();
                svPhieuDeNghi.SaveTrangThaiHT(phieu).$promise.then(
                    function (d) {
                        toaster.pop('success', "Thông báo", "Lưu thông tin thành công.");
                        ngProgress.complete();
                        $scope.refreshData(1);

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
                        var strPrint = d.NoiDung.replace("[TenKhoa]", phieu.TenKhoa)
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
            //region phieu vat tu
            $scope.GiaoDich = {};
            $scope.SanPham = {};
            $scope.showPopupVatTu = function (phieu) {
                $scope.openPopup();
                $scope.Phieu = phieu;
                $scope.GiaoDich = {
                    ChiTiet :[],
                    IdKhoa: phieu.IdKhoa,
                    IdNguoiTao: myAppConfig.IdCanBo,
                    IdPhieuDeNghi: phieu.Id,
                    LoaiGiaoDich: 0,
                }
                $('#txtTenCongViec').focus();
            }
            $scope.openPopup = function () {
                $("#popupVatTu").bPopup({ escClose: false, modalClose: false });
                $("#popupVatTu").show();
            };
            $scope.closePopup = function () {
                $("#popupVatTu").bPopup({}).close();
            };
            $scope.refreshTonKho = function (key) {
                svKho.GetTonKho({
                    sSearch: key,
                    iPageIndex: 1,
                    iPageSize: 20,
                }).$promise.then(
                    function (d) {
                        $scope.DSTonKho = d.List;
                        $scope.sanpham = { Ma: "", TenSanPham: 'Chọn sản phẩm' };
                    }, function (err) { ngProgress.complete(); });
            }
            $scope.AddSanPham = function () {
                if (typeof $scope.SanPham.IdSanPham === 'undefined') {
                    toaster.pop('warning', "Thông báo", "Vui lòng chọn sản phẩm.");
                    return;
                }
                if (typeof $scope.SanPham.SoLuong === 'undefined' || $scope.SanPham.SoLuong == '' || $scope.SanPham.SoLuong == '0') {
                    toaster.pop('warning', "Thông báo", "Vui lòng nhập số lượng.");
                    $('#txtSoLuong').focus();
                    return;
                }
                $scope.GiaoDich.ChiTiet.push($scope.SanPham);
                $scope.SanPham = {};
                $scope.sanpham = { TenSanPham: 'Chọn sản phẩm' };
            }
            $scope.DeleteSanPham = function (index) {
                $scope.GiaoDich.ChiTiet.splice(index, 1);
            }
            $scope.$watch('sanpham', function (newVal, oldVal) {
                if (typeof newVal === 'undefined' || newVal == "") return;
                $scope.SanPham.IdSanPham = newVal.IdSanPham;
                $scope.SanPham.MaSanPham = newVal.MaSanPham;
                $scope.SanPham.TenSanPham = newVal.TenSanPham;
                $scope.SanPham.SoLuongTon = newVal.SoLuong;
                $scope.SanPham.TenDonVi = newVal.TenDonVi;
            });
            $scope.TaoPhieuVatTu = function () {
                if ($scope.GiaoDich.ChiTiet.length == 0) {
                    toaster.pop('warning', "Thông báo", "Chưa có vật tư nào được chọn.");
                    return;
                }
                confirmPopup('Cảnh báo', 'Xác nhận tạo phiếu vật tư và trừ kho ?', function () {
                    $scope.isDisabled = true;
                    svKho.TaoPhieuVTByPhieuDeNghi($scope.GiaoDich).$promise.then(
                        function (d) {
                            setTimeout(function () { toaster.pop('success', "Thông báo", "Tạo phiếu thành công."); }, 2000);
                            ngProgress.complete();
                            $scope.isDisabled = false;
                            $scope.closePopup();
                            $scope.refreshData(1);
                        }, function (err) {
                            toaster.pop('error', "Thông báo", err.data);
                            ngProgress.complete();
                            $scope.isDisabled = false;
                        });

                }, function () {
                    return;
                });
            }
            $scope.InPhieuVatTu = function (phieu) {
                var gd = phieu.GiaoDichVatTu;
                svMauPhieuIn.GetByMa({
                    sMa: 'PhieuVatTu'
                }).$promise.then(
                    function (d) {
                        var strPrint = d.NoiDung.replace("[TenKhoa]", gd.TenKhoa)
                            .replace("[Ngay]", moment(gd.NgayTao).format("DD"))
                            .replace("[Thang]", moment(gd.NgayTao).format("MM"))
                            .replace("[Nam]", moment(gd.NgayTao).format("YYYY"));

                        var htmlTable = "";
                        for (var i = 0; i < gd.ChiTiet.length; i++) {
                            var ct = gd.ChiTiet[i];
                            htmlTable += "<tr>";
                            htmlTable += "<td style='border: 1px solid black;text-align:center;padding-left:5px'>" + (i+1) + "</td>";
                            htmlTable += "<td style='border: 1px solid black;padding-left:5px'>" + ct.TenSanPham + "</td>";
                            htmlTable += "<td style='border: 1px solid black;text-align:center'>" + ct.TenDonVi + "</td>";
                            htmlTable += "<td style='border: 1px solid black;padding-right:5px;text-align:right'>" + ct.SoLuong + "</td>";
                            htmlTable += "<td style='border: 1px solid black;padding-left:5px'>" + ct.GhiChu + "</td>";
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
                            setTimeout(function () { newWin.close(); }, 50);
                        });
                    }, function (err) { ngProgress.complete(); });
            }
            //endregion phieu vat tu
        }]);