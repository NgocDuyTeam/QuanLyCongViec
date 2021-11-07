
'use strict';
var app = angular.module('uiApp');

app.factory('svPhieuDeNghi', function (myAppConfig, $resource) {
    var baseUrl = myAppConfig.baseUrl;
    return $resource(baseUrl + '/api/phieudenghi/',
        { id: '@id' },
        {
            'getDemo': {
                method: 'GET',
                isArray: false,
                url: baseUrl + '/api/phieudenghi/getDemo',
                params: {
                    sDemo: '@sDemo'
                },
                transformResponse: function (data) {
                    return { str: angular.fromJson(data) };
                }
            },
           
        })
});