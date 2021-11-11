'use strict';
var app = angular.module('uiApp');

app.controller('SC200DanhMucKhoaPhongCtrl',
    ['$scope', '$compile', '$resource', 'myAppConfig', 'ngProgress', 'toaster', 'svDanhMucKhoaPhong',
        function ($scope, $compile, $resource, myAppConfig, ngProgress, toaster, svDanhMucKhoaPhong) {
            $scope.NgayTao = moment().format('YYYY-MM-DD');
<<<<<<< HEAD
          
            $scope.loadDMKhoaPhong = function () {
                svDanhMucKhoaPhong.GetDanhSachKhoaPhong().$promise.then(
                    function (d) {
                        $scope.DSKhoaPhong = d.List;
                    }, function (err) { ngProgress.complete(); });
            }
        
            $scope.loadDMKhoaPhong();



            //$scope.openPopupDemo = function () {
            //    $("#modelDemo").bPopup({ escClose: false, modalClose: false });
            //    $("#modelDemo").show();
            //};
            //$scope.closePopupDemo = function () {
            //    $("#modelDemo").bPopup({}).close();
            //};
=======
            $scope.iPageIndex = 1;
            $scope.iPageSize = 20;

            $scope.loadDMKhoaPhong = function (iPageIndex) {
                $scope.iPageIndex = iPageIndex;
                ngProgress.start();
                svDanhMucKhoaPhong.GetDanhSachKhoaPhong(
                    {
                        iPageIndex: $scope.iPageIndex,
                        iPageSize: $scope.iPageSize
                    }
                ).$promise.then(
                    function (d) {
                        $scope.DSKhoaPhong = d.List;
                        $scope.iTotal = d.iTotal != null ? d.iTotal : 0;
                        $scope.iTotalPage = Math.floor(($scope.iTotal - 1) / $scope.iPageSize) + 1;
                        var lstPage = GetlstPage($scope.iTotalPage, $scope.iPageIndex, 'loadDMKhoaPhong');
                        $("#lstPage").html($compile(lstPage)($scope));
                        ngProgress.complete();
                    }, function (err) { ngProgress.complete(); });
            }

            $scope.loadDMKhoaPhong(1);

>>>>>>> abceccb6d08bb73ae86e6e60e54c47f51342d16d
        }]);