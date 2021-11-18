
'use strict';
var app = angular
    .module('uiApp', [
        'ngCookies',
        'ngResource',
        'ngSanitize',
        'ui.mask',
        'ui.select',
        'toaster',
        'ngProgress',
        'ui.router'])
    .config(function (uiSelectConfig) {
        uiSelectConfig.theme = 'bootstrap';
    })
    .run(['$rootScope', '$state', '$stateParams',
        function ($rootScope, $state, $stateParams) {

            // It's very handy to add references to $state and $stateParams to the $rootScope
            // so that you can access them from any scope within your applications.For example,
            // <li ng-class="{ active: $state.includes('contacts.list') }"> will set the <li>
            // to active whenever 'contacts.list' or one of its decendents is active.
            $rootScope.$state = $state;
            $rootScope.$stateParams = $stateParams;
        }
    ])
    .constant('myAppConfig', {
        baseUrl: _baseUrl, TenKhoa: _tenKhoa
        , IdCanBo: _idCanBo, IdKhoa: _idKhoa
        , HoVaTen: _hovaten
    }
    );
