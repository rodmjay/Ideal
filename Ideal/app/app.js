/// <reference path="~/Scripts/angular.js" />

angular.module('ideal', ['ideal.accounts', 'ideal.tincture'])
    .controller('iMainLayout', ['$element', function ($element) {
        //$element.css('background-color', 'red');
    }])
    .filter('percentage', ['$filter', function ($filter) {
        return function (input, decimals) {
            return $filter('number')(input * 100, decimals) + '%';
        };
    }]);