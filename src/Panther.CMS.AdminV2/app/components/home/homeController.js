(function () {
    'use strict';

    angular
        .module('app')
        .controller('homeController', homeController);

    homeController.$inject = ['$scope']; 

    function homeController($scope) {
        $scope.title = 'homeController';

        activate();

        function activate() { }
    }
})();
