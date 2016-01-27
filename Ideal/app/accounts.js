angular
    .module('ideal.accounts', ['ideal.common'])
    .directive('iLoginForm', [
        function() {
            return {
                templateUrl: '/account/login',
                restrict: 'E',
                link: function(scope, elem, attrs) {

                },
                controller: [
                    function() {

                    }
                ]
            }
        }
    ])
    .controller('iRegisterCtrl', ['$scope',function($scope) {
        
    }]);