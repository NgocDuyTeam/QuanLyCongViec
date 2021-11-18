
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
        })
});