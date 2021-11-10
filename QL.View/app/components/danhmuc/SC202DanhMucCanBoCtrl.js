'use strict';
var app = angular.module('uiApp');

app.controller('SC202DanhMucCanBoCtrl',
    ['$scope', '$compile', '$resource', 'myAppConfig', 'ngProgress', 'toaster', 'svDanhMucCanBo',
        function ($scope, $compile, $resource, myAppConfig, ngProgress, toaster, svDanhMucCanBo) {
            $scope.NgayTao = moment().format('YYYY-MM-DD');
            $scope.loadDMCanBo = function () {
                svDanhMucCanBo.GetDanhSachCanBo().$promise.then(
                    function (d) {
                        $scope.DSCanBo = d.List;
                    }, function (err) { ngProgress.complete(); });
            }

            $scope.loadDMCanBo();

        }]);