'use strict';
var app = angular.module('uiApp');

app.factory('svDanhMucCanBo', function (myAppConfig, $resource) {
    var baseUrl = myAppConfig.baseUrl;
    return $resource(baseUrl + 'api/dmcanbo/',
        { id: '@id' },
        {
            'GetDanhSachCanBo': {
                method: 'GET',
                url: baseUrl + 'api/dmcanbo/getDanhSach',
            },
        })
});