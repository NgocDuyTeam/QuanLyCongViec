'use strict';
var app = angular.module('uiApp');

app.controller('SC301NghiemThuCongViecCtrl',
    ['$scope', '$compile', '$resource', '$filter', 'myAppConfig', 'ngProgress', 'toaster', 'svPhieuDeNghi'
        , 'svBienBanNghiemThu', 'svMauPhieuIn', 'svDanhMucKhoaPhong', 'svCongViectheoQD',
        function ($scope, $compile, $resource, $filter, myAppConfig, ngProgress, toaster, svPhieuDeNghi
            , svBienBanNghiemThu, svMauPhieuIn, svDanhMucKhoaPhong, svCongViectheoQD) {
            $scope.Phieu = {};
            $scope.BienBan = {};
            $scope.LstCongViec = [];
            $scope.NgayBatDau = moment().format('HH:mm DD/MM/YYYY');
            $scope.NgayKetThuc = moment().format('HH:mm DD/MM/YYYY');
            $scope.NgayTao = moment().format('YYYY-MM-DD');
            $scope.BienBan.ObjPhongQuanTri = {};
            $scope.BienBan.ObjPhongQuanTri.HoVaTen = myAppConfig.HoVaTen;
            $scope.BienBan.ObjPhongQuanTri.ChucVu = "Chức vụ: Cán bộ kỹ thuật";

            $scope.loadThongTinCongViec = function (IdCongViec) {
                svCongViectheoQD.GetById({
                    Id: IdCongViec
                }).$promise.then(
                    function (d) {
                        $scope.CongViec = d;
                        $scope.BienBan.GoiThau = $scope.CongViec.MoTaCongViec;
                        $scope.BienBan.DoiTuongNghiemThu = $scope.CongViec.MoTaCongViec;
                    }, function (err) { ngProgress.complete(); });
            }
            $scope.loadThongTinBienBan = function (Id) {
                svBienBanNghiemThu.GetById({
                    Id: Id
                }).$promise.then(
                    function (d) {
                        $scope.BienBan = d;
                        $scope.NgayBatDau = moment($scope.BienBan.NgayBatDau).format('HH:mm DD/MM/YYYY');
                        $scope.NgayKetThuc = moment($scope.BienBan.NgayKetThuc).format('HH:mm DD/MM/YYYY');
                        for (var i = 0; i < $scope.DSKhoaPhong.length; i++) {
                            if ($scope.BienBan.DanhSachKhoa.indexOf($scope.DSKhoaPhong[i].Id) > -1) {
                                $scope.DSKhoaPhong[i].IsCheck = true;
                            } else {
                                $scope.DSKhoaPhong[i].IsCheck = false;
                            }
                        }
                        $scope.LstCongViec = $scope.BienBan.LstCongViec;
                    }, function (err) { ngProgress.complete(); });
            }

            Promise.resolve(1).then(Test => {
                var reKhoa = svDanhMucKhoaPhong.GetDanhSachKhoaPhong({
                    iPageIndex: -1,
                    iPageSize: -1
                }).$promise;
                return Promise.all([reKhoa]);
            }).then(([reKhoa]) => {
                $scope.DSKhoaPhong = reKhoa.List;
                var urlObj = $.deparam.querystring();
                if (urlObj.idCongViec) {
                    $scope.loadThongTinCongViec(urlObj.idCongViec);
                    $scope.BienBan.IdCongViec = urlObj.idCongViec;
                } else if (urlObj.idBienBan) {
                    $scope.loadThongTinBienBan(urlObj.idBienBan);
                };
            });

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

            $scope.saveBienBan = function () {
                $scope.BienBan.LstCongViec = $scope.LstCongViec;
                $scope.BienBan.NgayBatDau = moment($scope.NgayBatDau, "HHmmDDMMYYYY").format('HH:mm MM/DD/YYYY');
                $scope.BienBan.NgayKetThuc = moment($scope.NgayKetThuc, "HHmmDDMMYYYY").format('HH:mm MM/DD/YYYY');
                $scope.BienBan.IdCanBo = myAppConfig.IdCanBo;
                $scope.BienBan.DanhSachKhoa = "";
                for (var i = 0; i < $scope.DSKhoaPhong.length; i++) {
                    if ($scope.DSKhoaPhong[i].IsCheck) {
                        $scope.BienBan.DanhSachKhoa += $scope.DSKhoaPhong[i].Id + ";";
                    }
                }
                ngProgress.start();
                $scope.isDisabled = true;
                svBienBanNghiemThu.saveBienBan($scope.BienBan).$promise.then(
                    function (d) {
                        toaster.pop('success', "Thông báo", "Lưu thông tin thành công.");
                        $scope.isDisabled = false;
                        window.location.href = "/CongViecTheoQD/SC400_CongViec";
                        ngProgress.complete();
                    }, function (err) {
                        ngProgress.complete();
                        $scope.isDisabled = false;
                    });
            }
        }]);