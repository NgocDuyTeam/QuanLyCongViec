'use strict';
var app = angular.module('uiApp');

app.factory('svTuDien', function (myAppConfig, $resource) {
    var baseUrl = myAppConfig.baseUrl;
    return $resource(baseUrl + 'api/tudien/',
        { id: '@id' },
        {
            'GetTuDienByLoai': {
                method: 'GET',
                params: {
                    sLoaiTuDien: '@sLoaiTuDien',
                },
                url: baseUrl + 'api/tudien/getTuDienByLoai',
            },
        })
});