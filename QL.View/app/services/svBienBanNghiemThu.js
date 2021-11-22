
'use strict';
var app = angular.module('uiApp');

app.factory('svBienBanNghiemThu', function (myAppConfig, $resource) {
    var baseUrl = myAppConfig.baseUrl;
    return $resource(baseUrl + 'api/nghiemthu/',
        { id: '@id' },
        {
            'saveBienBan': {
                method: 'POST',
                url: baseUrl + 'api/nghiemthu/saveBienBan',
            },
            'DeleteBienBanById': {
                method: 'Post',
                params: {
                    IdBienBan: '@IdBienBan',
                },
                url: baseUrl + 'api/nghiemthu/deleteBienBanById',
            },
            'GetById': {
                method: 'GET',
                params: {
                    Id: '@Id',
                },
                url: baseUrl + 'api/nghiemthu/getBienBanById',
            },
        })
});