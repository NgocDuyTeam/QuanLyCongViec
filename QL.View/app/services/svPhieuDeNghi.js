
'use strict';
var app = angular.module('uiApp');

app.factory('svPhieuDeNghi', function (myAppConfig, $resource) {
    var baseUrl = myAppConfig.baseUrl;
    return $resource(baseUrl + 'api/phieudenghi/',
        { id: '@id' },
        {
            'savePhieuDeNghi': {
                method: 'POST',
                url: baseUrl + 'api/phieudenghi/savePhieuDeNghi',
            },
            'savePhanCongPhieuDeNghi': {
                method: 'POST',
                url: baseUrl + 'api/phieudenghi/savePhanCongPhieuDeNghi',
            },
            'SaveTrangThaiHT': {
                method: 'POST',
                url: baseUrl + 'api/phieudenghi/SaveTrangThaiHT',
            },
            'getPhieuDeNghiByPage': {
                method: 'GET',
                params: {
                    IdKhoa: '@IdKhoa',
                    TuNgay: '@TuNgay',
                    DenNgay: '@DenNgay',
                    sTrangThai: '@sTrangThai',
                    iPageIndex: '@iPageIndex',
                    iPageSize: '@iPageSize',
                    IdCanBo: '@IdCanBo'
                },
                url: baseUrl + 'api/phieudenghi/getPhieuDeNghiByPage',
            },
            'getPhieuDeNghiByPagenv': {
                method: 'GET',
                params: {
                    IdNv: '@IdNv',
                    TuNgay: '@TuNgay',
                    DenNgay: '@DenNgay',
                    sTrangThai: '@sTrangThai',
                    iPageIndex: '@iPageIndex',
                    iPageSize: '@iPageSize',
                    IdKhoa: '@IdKhoa'
                },
                url: baseUrl + 'api/phieudenghi/getPhieuDeNghiByPageNv',
            },
            'GetPhieuById': {
                method: 'GET',
                params: {
                    IdPhieu: '@IdPhieu',
                },
                url: baseUrl + 'api/phieudenghi/getPhieuDeId',
            },
            'DeletePhieuById': {
                method: 'Post',
                params: {
                    IdPhieu: '@IdPhieu',
                },
                url: baseUrl + 'api/phieudenghi/deletePhieuDeNghiById',
            },
        })
});