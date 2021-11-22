
'use strict';
var app = angular.module('uiApp');

app.factory('svCongViectheoQD', function (myAppConfig, $resource) {
    var baseUrl = myAppConfig.baseUrl;
    return $resource(baseUrl + 'api/congviectheoqd/',
        { id: '@id' },
        {
            'saveCongViec': {
                method: 'POST',
                url: baseUrl + 'api/congviectheoqd/saveCongViec',
            },
            'getByPage': {
                method: 'GET',
                params: {
                    TuNgay: '@TuNgay',
                    DenNgay: '@DenNgay',
                    iPageIndex: '@iPageIndex',
                    iPageSize: '@iPageSize'
                },
                url: baseUrl + 'api/congviectheoqd/getByPage',
            },
            'GetById': {
                method: 'GET',
                params: {
                    Id: '@Id',
                },
                url: baseUrl + 'api/congviectheoqd/getById',
            },
            'DeleteCongViecById': {
                method: 'Post',
                params: {
                    IdCongViec: '@IdCongViec',
                },
                url: baseUrl + 'api/congviectheoqd/deleteCongViecById',
            },
        })
});