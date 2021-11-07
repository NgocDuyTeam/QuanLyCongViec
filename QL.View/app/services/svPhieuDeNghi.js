
'use strict';
var app = angular.module('uiApp');

app.factory('svPhieuDeNghi', function (myAppConfig, $resource) {
    var baseUrl = myAppConfig.baseUrl;
    return $resource(baseUrl + 'api/phieudenghi/',
        { id: '@id' },
        {
            'savePhieuDeNghi': {
                method: 'POST',
                url: baseUrl + 'api/phieudenghi/savePhieuDeNghi',
            },
        })
});