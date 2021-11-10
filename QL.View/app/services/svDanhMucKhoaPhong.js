
'use strict';
var app = angular.module('uiApp');

app.factory('svDanhMucKhoaPhong', function (myAppConfig, $resource) {
    var baseUrl = myAppConfig.baseUrl;
    return $resource(baseUrl + 'api/dmkhoaphong/',
        { id: '@id' },
        {
            'GetDanhSachKhoaPhong': {
                method: 'GET',
                url: baseUrl + 'api/dmkhoaphong/getDanhSach',
            },
        })
});