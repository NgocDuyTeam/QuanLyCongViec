'use strict';
var app = angular.module('uiApp');

app.controller('P100PhieuDeNghiCtrl',
    ['$scope', '$compile', '$resource', 'ngProgress', 'toaster', 'svPhieuDeNghi',
        function ($scope, $compile, $resource, ngProgress, toaster, svPhieuDeNghi) {
        $scope.NgayTao = moment().format('DD/MM/YYYY');
    
        $scope.refreshData = function (pageindex) {
            $scope.pageIndex = pageindex;
            svPhieuDeNghi.getDemo({
                sDemo: "123"
            }).$promise.then(
                function (d) {
                    alert("sss");
                ngProgress.complete();
            }, function (err) { ngProgress.complete(); });
            };
            $scope.refreshData();
    }]);