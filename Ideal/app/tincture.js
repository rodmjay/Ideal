angular.module('ideal.tincture', [])
    .controller('TinctureCtrl', [
        function () {
            var self = this;
            this.dosages = ['asdf', 'asdf'];

            this.cbdAmount = 0;
            this.indicaAmount = 0;
            this.sativaAmount = 0;

            this.addCBD = function() {
                this.cbdAmount += 1;
            }
            this.removeCBD = function () {
                self.cbdAmount -= 1;
            }
            this.addIndica = function() {
                self.indicaAmount += 1;
            }
            this.removeIndica = function () {
                self.indicaAmount -= 1;
            }
            this.addSativa = function() {
                self.sativaAmount += 1;
            }
            this.removeSativa = function() {
                self.sativaAmount -= 1;
            }
            this.totalAmount = function() {
                return (self.cbdAmount
                    + self.indicaAmount
                    + self.sativaAmount);
            }
            this.formattedPercentage = function(amount) {
                
            }
        }
    ]);