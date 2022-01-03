'use strict';
var app = angular.module('uiApp');

app.controller('SC503TonKhoCtrl',
    ['$scope', '$compile', '$resource', 'myAppConfig', 'ngProgress', 'toaster', 'svKho', 'svTuDien',
        function ($scope, $compile, $resource, myAppConfig, ngProgress, toaster, svKho, svTuDien) {
            $scope.sSearch = "";
            $scope.iPageIndex = 1;
            $scope.iPageSize = "20";
            $scope.refreshData = function (iPageIndex) {
                $scope.iPageIndex = iPageIndex;
                svKho.GetTonKho({
                    sSearch: $scope.sSearch,
                    iPageIndex: $scope.iPageIndex,
                    iPageSize: $scope.iPageSize,
                }).$promise.then(
                    function (d) {
                        $scope.DSTonKho = d.List;
                        $scope.iTotal = d.iTotal != null ? d.iTotal : 0;
                        $scope.iTotalPage = Math.floor(($scope.iTotal - 1) / $scope.iPageSize) + 1;
                        var lstPage = GetlstPage($scope.iTotalPage, $scope.iPageIndex, 'refreshData');
                        $("#lstPage").html($compile(lstPage)($scope));
                    }, function (err) { ngProgress.complete(); });
            }
            $scope.refreshData(1);
//..................................................................................................
        }]);