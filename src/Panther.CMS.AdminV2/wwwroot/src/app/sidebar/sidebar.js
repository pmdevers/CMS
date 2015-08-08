angular.module("ngBoilerplate.sidebar", [
        "ui.router",
        "ui.bootstrap",
        "cfp.hotkeys"
])
    .controller("SidebarCtrl", function SidebarController($scope, hotkeys) {

        $scope.sideBarItems = [
            { 'name': "site", 'tooltip': "change page", 'shortcut': "alt+s", 'icon': "globe", 'option':"star" },
            { 'name': "newPage", 'tooltip': "Create a new Page", 'shortcut': "alt+n", 'icon': "file-o", 'option': "" }
        ];

        angular.forEach($scope.items, function (value) {
            hotkeys.add({
                combo: value.shortcut,
                description: value.tooltip,
                callback: function () {
                    alert("pressed " + value.shortcut);
                }
            });
        });

        $scope.currentMenu = $scope.sideBarItems[0];
        angular.element('#' + $scope.currentMenu.name).css("display", "block");
        

        $scope.hasOption = function (item){
            if (item.option) {
                return true;
            }

            return false;
        };

        $scope.showSubMenu = function (item){
            angular.element('#' + $scope.currentMenu.name).css("display", "none");

            $scope.currentMenu = item;

            angular.element('#' + $scope.currentMenu.name).css("display", "block");
        };
    });