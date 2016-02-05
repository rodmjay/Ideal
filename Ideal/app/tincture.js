angular.module('ideal.tincture', ['ngResource'])
    .config(['$resourceProvider', function ($resourceProvider) {
        // Don't strip trailing slashes from calculated URLs
        $resourceProvider.defaults.stripTrailingSlashes = false;
    }])
    .factory('Influencer', function ($rootScope) {

        function Influencer(input) {
            this._name = input.name;
            this._value = input.value || 0;
        }

        Influencer.prototype.value = function(amount) {
            if (angular.isNumber(amount)) {
                this._value = amount;
                $rootScope.$emit('value', amount);
            } else {
                return this._value;
            }
        }

        Influencer.prototype.increment = function() {
            var val = this._value + 1;
            this.value(val);
        }
        Influencer.prototype.decriment = function () {
            if (this._value) {
                var val = this._value - 1;
                this.value(val);
            }
        }
        return Influencer;
    })
    .filter('ratio', function () {
        return function (first, second, third) {
            if(third===undefined)
                return (first || 0) + ':' + (second || 0);
            return (first || 0) + ':' + (second || 0) + ':' + (third || 0);
        }
    })
    .controller('TinctureCtrl', ['Influencer','$rootScope', '$resource',
        function (Influencer, $rootScope, $resource) {

            var self = this;

            var Ratios = $resource('/symptoms.json');

            this.cbd = new Influencer({ name: 'CBD', value: 1 });
            this.thc = new Influencer({ name: 'THC', value: 1 });

            this.tincture = {
                dosage:15
            }

            this.readjust = function () {

                var cbd = self.cbd.value();
                var thc = self.thc.value();

                if (cbd === thc) {
                    cbd = 1;
                    thc = 1;
                }

                self.cbd.value(cbd);
                self.thc.value(thc);

            }
            
            this.mgAmount=function(amount) {
                var total = self.totalAmount();
                return (amount / (total||1)) * self.tincture.dosage;
            }

            this.totalAmount = function() {
                return (self.cbd.value()
                    + self.thc.value());
            }
            this.dosages = [
                { name: '2.5 mg', value: 2.5 },
                { name: '5.0 mg', value: 5 },
                { name: '7.5 mg', value: 7.5 },
                { name: '10.0 mg', value: 10 },
                { name: '12.5 mg', value: 12.5 },
                { name: '15.0 mg', value: 15 }
            ];

            this.ratios = Ratios.get();
        }
    ]);