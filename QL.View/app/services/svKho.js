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
            'TaoPhieuVTByPhieuDeNghi': {
                method: 'POST',
                url: baseUrl + 'api/kho/taoPhieuVTByPhieuDeNghi',
            },
            
            'GetGiaoDichChitiet': {
                method: 'GET',
                params: {
                    IdGiaoDich: '@IdGiaoDich',
                },
                url: baseUrl + 'api/kho/getGiaoDichChitiet',
            },
            'BCXuatNhapTon': {
                method: 'GET',
                params: {
                    sSearch: '@sSearch',
                    TuNgay: '@TuNgay',
                    DenNgay: '@DenNgay',
                },
                url: baseUrl + 'api/kho/bcXuatNhapTon',
            },
            'BCXuatKhoaPhong': {
                method: 'GET',
                params: {
                    sSearch: '@sSearch',
                    TuNgay: '@TuNgay',
                    DenNgay: '@DenNgay',
                    IdKhoa: '@IdKhoa'
                },
                url: baseUrl + 'api/kho/bcXuatKhoaPhong',
            },
        })
});