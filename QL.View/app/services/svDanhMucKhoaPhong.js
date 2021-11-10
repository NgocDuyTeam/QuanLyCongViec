
'use strict';
var app = angular.module('uiApp');

app.factory('svDanhMucKhoaPhong', function (myAppConfig, $resource) {
    var baseUrl = myAppConfig.baseUrl;
    return $resource(baseUrl + 'api/dmkhoaphong/',
        { id: '@id' },
        {
            'GetDanhSachKhoaPhong': {
                method: 'GET',
                params: {
                    iPageIndex: '@iPageIndex',
                    iPageSize: '@iPageSize'
                },
                url: baseUrl + 'api/dmkhoaphong/getDanhSach',
            },
        })
});