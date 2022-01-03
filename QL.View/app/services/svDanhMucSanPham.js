'use strict';
var app = angular.module('uiApp');

app.factory('svDanhMucSanPham', function (myAppConfig, $resource) {
    var baseUrl = myAppConfig.baseUrl;
    return $resource(baseUrl + 'api/dmsanpham/',
        { id: '@id' },
        {
            'GetDanhSach': {
                method: 'GET',
                params: {
                    sSearch : '@sSearch',
                    iPageIndex: '@iPageIndex',
                    iPageSize: '@iPageSize'
                },
                url: baseUrl + 'api/dmsanpham/getDanhSach',
            },
            'AddOrUpdate': {
                method: 'POST',
                url: baseUrl + 'api/dmsanpham/addOrUpdate',
            },
            'DeleteById': {
                method: 'POST',
                params: {
                    IdSanPham: '@IdSanPham',
                },
                url: baseUrl + 'api/dmsanpham/deleteById',
            },
            'GetById': {
                method: 'GET',
                params: {
                    IdSanPham: '@IdSanPham',
                },
                url: baseUrl + 'api/dmsanpham/getById',
            },
        })
});