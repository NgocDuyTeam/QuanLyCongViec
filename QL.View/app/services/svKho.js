'use strict';
var app = angular.module('uiApp');

app.factory('svKho', function (myAppConfig, $resource) {
    var baseUrl = myAppConfig.baseUrl;
    return $resource(baseUrl + 'api/kho/',
        { id: '@id' },
        {
            'GetTonKho': {
                method: 'GET',
                params: {
                    sSearch: '@sSearch',
                    iPageIndex: '@iPageIndex',
                    iPageSize: '@iPageSize'
                },
                url: baseUrl + 'api/kho/getTonKho',
            },
            'GetDanhSachGiaoDich': {
                method: 'GET',
                params: {
                    TuNgay: '@TuNgay',
                    DenNgay: '@DenNgay',
                    iStatus: '@iStatus',
                    iPageIndex: '@iPageIndex',
                    iPageSize: '@iPageSize'
                },
                url: baseUrl + 'api/kho/getDanhSachGiaoDich',
            },
            'CreateGiaoDichKho': {
                method: 'POST',
                url: baseUrl + 'api/kho/createGiaoDichKho',
            },
            'GetGiaoDichChitiet': {
                method: 'GET',
                params: {
                    IdGiaoDich: '@IdGiaoDich',
                },
                url: baseUrl + 'api/kho/getGiaoDichChitiet',
            },
        })
});