﻿
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
            'getPhieuDeNghiByPage': {
                method: 'GET',
                params: {
                    IdKhoa: '@IdKhoa',
                    TuNgay: '@TuNgay',
                    DenNgay: '@DenNgay',
                    sTrangThai: '@sTrangThai',
                    iPageIndex: '@iPageIndex',
                    iPageSize: '@iPageSize'
                },
                url: baseUrl + 'api/phieudenghi/getPhieuDeNghiByPage',
            },
        })
});