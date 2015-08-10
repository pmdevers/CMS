angular.module("ngBoilerplate.builder", [
        "ui.router",
        "ui.bootstrap",
        "cfp.hotkeys",
        'ui.ace'
    ])
    .controller("builderCtrl", function BuilderController($scope, hotkeys) {

        $scope.editorVisible = true;

        $scope.ToggleEditor = function (){
            $scope.editorVisible = !$scope.editorVisible;
        };

    });
