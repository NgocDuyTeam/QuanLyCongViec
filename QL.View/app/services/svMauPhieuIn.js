
'use strict';
var app = angular.module('uiApp');

app.factory('svMauPhieuIn', function (myAppConfig, $resource) {
    var baseUrl = myAppConfig.baseUrl;
    return $resource(baseUrl + 'api/mauphieuin/',
        { id: '@id' },
        {
        
            'GetByMa': {
                method: 'GET',
                params: {
                    sMa: '@sMa',
                },
                url: baseUrl + 'api/mauphieuin/getByMa',
            },
        })
});