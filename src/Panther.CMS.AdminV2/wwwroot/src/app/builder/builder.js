angular.module("ngBoilerplate.builder", [
        "ui.router",
        "ui.bootstrap",
        "cfp.hotkeys",
        'ui.ace'
])

    .config(function config($stateProvider) {
        $stateProvider.state('builder', {
            url: '/builder',
            views: {
                '': {
                    controller: 'builderCtrl',
                    templateUrl: 'builder/builder.tpl.html'
                }
            },
            data: { pageTitle: 'Builder' }
        });
    })

    .controller("builderCtrl", function BuilderController($scope, hotkeys) {

        $scope.editorVisible = true;

        $scope.ToggleEditor = function (){
            $scope.editorVisible = !$scope.editorVisible;
        };

    });
