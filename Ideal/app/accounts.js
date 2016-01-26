angular
    .module('ideal.accounts', ['ideal.common'])
    .directive('iLoginForm',[function() {
        return {
            templateUrl: '/accounts/login',
            restrict: 'E',
            link:function(scope, elem, attrs, ctrl) {
                
            },
            controller:[function() {
                
            }]
        }
    }])