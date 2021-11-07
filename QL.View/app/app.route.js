'use strict';
var app = angular.module('uiApp');

app.config(['$stateProvider', '$urlRouterProvider', '$httpProvider',
function ($stateProvider, $urlRouterProvider, $httpProvider) {

    ///////////////////////////
    // State Configurations ///
    ///////////////////////////

   
    // FIX for trailing slashes. Gracefully "borrowed" from https://github.com/angular-ui/ui-router/issues/50
    $urlRouterProvider.rule(function ($injector, $location) {
        if ($location.protocol() === 'file')
            return;

        var path = $location.path()
        // Note: misnomer. This returns a query object, not a search string
        , search = $location.search()
        , params
        ;

        // check to see if the path already ends in '/'
        if (path[path.length - 1] === '/') {
            return;
        }

        // If there was no search string / query params, return with a `/`
        if (Object.keys(search).length === 0) {
            return path + '/';
        }

        // Otherwise build the search string and return a `/?` prefix
        params = [];
        angular.forEach(search, function (v, k) {
            params.push(k + '=' + v);
        });
        return path + '/?' + params.join('&');
    });

    $httpProvider.interceptors.push(function ($q, $location) {
        return {
            'responseError': function (response) {
                if (response.status === 401 || response.status === 403) {
                    $location.path('/login');
                }
                return $q.reject(response);
            }
        };
    });
}]);
