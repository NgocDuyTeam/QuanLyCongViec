'use strict';
var app = angular.module('uiApp');

app.controller('SC300BienBanNghiemThuCtrl',
    ['$scope', '$compile', '$resource', '$filter', 'myAppConfig', 'ngProgress', 'toaster', 'svPhieuDeNghi', 'svBienBanNghiemThu', 'svMauPhieuIn',
        function ($scope, $compile, $resource, $filter, myAppConfig, ngProgress, toaster, svPhieuDeNghi, svBienBanNghiemThu, svMauPhieuIn) {
            $scope.Phieu = {};
            $scope.BienBan = {};
          
            $scope.NgayBatDau = moment().format('HH:mm DD/MM/YYYY');
            $scope.NgayKetThuc = moment().format('HH:mm DD/MM/YYYY');
            $scope.NgayTao = moment().format('YYYY-MM-DD');
            $scope.BienBan.ObjPhongQuanTri = {};
            $scope.BienBan.ObjPhongQuanTri.HoVaTen = myAppConfig.HoVaTen;
            $scope.BienBan.ObjPhongQuanTri.ChucVu = "Chức vụ: Cán bộ kỹ thuật";
          
            $scope.LstCongViec = [];
            $scope.AddCongViec = function () {
                var obj = {};
                obj.STT = $scope.LstCongViec.length + 1;
                obj.NoiDung = "";
                obj.DonVi = "";
                obj.KhoiLuong = "";
                obj.GhiChu = "";
                $scope.LstCongViec.push(obj);
                $scope.OrderByCongViec();
            }
            $scope.XoaCongViec = function (index) {
                $scope.LstCongViec.splice(index, 1);
                $scope.OrderByCongViec();
            }
            $scope.OrderByCongViec = function () {
                $scope.LstCongViec = $filter('orderBy')($scope.LstCongViec, 'STT');
                for (var i = 0; i < $scope.LstCongViec.length; i++) {
                    $scope.LstCongViec[i].STT = i + 1;
                }
            }

            $scope.loadThongTinPhieu = function (IdPhieu) {
                svPhieuDeNghi.GetPhieuById({
                    IdPhieu: IdPhieu
                }).$promise.then(
                    function (d) {
                        $scope.Phieu = d;
                        $scope.BienBan.TenKhoa = $scope.Phieu.TenKhoa;
                    }, function (err) { ngProgress.complete(); });
            }
            var urlObj = $.deparam.querystring();
            if (urlObj.idPhieu) {
                $scope.loadThongTinPhieu(urlObj.idPhieu);
                $scope.BienBan.IdPhieuDeNghi = urlObj.idPhieu;
            };
            $scope.saveBienBan = function () {
                $scope.BienBan.LstCongViec = $scope.LstCongViec;
                $scope.BienBan.NgayBatDau = moment($scope.NgayBatDau, "HHmmDDMMYYYY").format('HH:mm MM/DD/YYYY');
                $scope.BienBan.NgayKetThuc = moment($scope.NgayKetThuc, "HHmmDDMMYYYY").format('HH:mm MM/DD/YYYY');
                $scope.BienBan.IdCanBo = myAppConfig.IdCanBo;
                ngProgress.start();
                $scope.isDisabled = true;
                svBienBanNghiemThu.saveBienBan($scope.BienBan).$promise.then(
                    function (d) {
                        toaster.pop('success', "Thông báo", "Lưu thông tin thành công.");
                        $scope.isDisabled = false;
                        ngProgress.complete();
                        svMauPhieuIn.GetByMa({
                            sMa: 'BienBanNghiemThu1'
                        }).$promise.then(
                            function (d) {
                                var ngaybatdau = "Bắt đầu " + moment($scope.NgayBatDau, "HHmmDDMMYYYY").format("HH")
                                    + " giờ " + moment($scope.NgayBatDau, "HHmmDDMMYYYY").format("mm")
                                    + " phút, ngày " + moment($scope.NgayBatDau, "HHmmDDMMYYYY").format("DD")
                                    + " tháng " + moment($scope.NgayBatDau, "HHmmDDMMYYYY").format("MM")
                                    + " năm " + moment($scope.NgayBatDau, "HHmmDDMMYYYY").format("YYYY");
                                var ngayketthuc = "Kết thúc " + moment($scope.NgayKetThuc, "HHmmDDMMYYYY").format("HH")
                                    + " giờ " + moment($scope.NgayKetThuc, "HHmmDDMMYYYY").format("mm")
                                    + " phút, ngày " + moment($scope.NgayKetThuc, "HHmmDDMMYYYY").format("DD")
                                    + " tháng " + moment($scope.NgayKetThuc, "HHmmDDMMYYYY").format("MM")
                                    + " năm " + moment($scope.NgayKetThuc, "HHmmDDMMYYYY").format("YYYY");
                                var strPrint = d.NoiDung.replace("[TenKhoa]", $scope.BienBan.TenKhoa)
                                    .replace("[PhongQuanTriHoTen]", $scope.BienBan.ObjPhongQuanTri.HoVaTen)
                                    .replace("[PhongQuanTriChucVu]", $scope.BienBan.ObjPhongQuanTri.ChucVu)
                                    .replace("[Ngay]", moment($scope.NgayBatDau, "HHmmDDMMYYYY").format("DD"))
                                    .replace("[Thang]", moment($scope.NgayBatDau, "HHmmDDMMYYYY").format("MM"))
                                    .replace("[Nam]", moment($scope.NgayBatDau, "HHmmDDMMYYYY").format("YYYY"))
                                    .replace("[NgayBatDau]", ngaybatdau)
                                    .replace("[NgayKetThuc]", ngayketthuc)
                                    .replace("[GoiThau]", $scope.BienBan.GoiThau.replace(/\n/g, "<br/>"))
                                    .replace("[DoiTuongNghiemThu]", $scope.BienBan.DoiTuongNghiemThu.replace(/\n/g, "<br/>"))
                                    .replace("[TenNhaThau]", $scope.BienBan.ObjNhaThau.TenNhaThau)
                                    .replace("[NhaThauHoTen]", $scope.BienBan.ObjNhaThau.HoVaTen)
                                    .replace("[NhaThauChucVu]", $scope.BienBan.ObjNhaThau.ChucVu)
                                    .replace("[HopDongSo]", $scope.BienBan.HopDongKinhTe)
                                    ;

                                var htmlTable = "";
                                for (var i = 0; i < $scope.LstCongViec.length; i++) {
                                    htmlTable += "<tr>";
                                    htmlTable += "<td style='border: 1px solid black;padding-left:5px'>" + $scope.LstCongViec[i].STT + "</td>";
                                    htmlTable += "<td style='border: 1px solid black;padding-left:5px'>" + $scope.LstCongViec[i].NoiDung + "</td>";
                                    htmlTable += "<td style='border: 1px solid black;padding-left:5px;text-align:center'>" + $scope.LstCongViec[i].DonVi + "</td>";
                                    htmlTable += "<td style='border: 1px solid black;padding-left:5px;text-align:center'>" + $scope.LstCongViec[i].KhoiLuong + "</td>";
                                    htmlTable += "<td style='border: 1px solid black;padding-left:5px'>" + $scope.LstCongViec[i].GhiChu + "</td>";
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
                                    window.location.href = "/PhieuDeNghi/SC103_NVPhieDeNghi";
                                });
                            }, function (err) { ngProgress.complete(); });
                    }, function (err) {
                        ngProgress.complete();
                        $scope.isDisabled = false;
                    });
            }
        }]);