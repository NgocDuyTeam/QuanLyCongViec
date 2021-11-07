﻿
'use strict';
var app = angular.module('uiApp');

app.factory('svDMCongViec', function (myAppConfig, $resource) {
    var baseUrl = myAppConfig.baseUrl;
    return $resource(baseUrl + 'api/dmcongviec/',
        { id: '@id' },
        {
            'GetDanhSachCongViec': {
                method: 'GET',
                url: baseUrl + 'api/dmcongviec/getDanhSach',
            },
        })
});