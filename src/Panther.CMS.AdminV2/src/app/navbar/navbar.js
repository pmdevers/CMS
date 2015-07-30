angular.module("ngBoilerplate.navbar", [
        "ui.router",
        "ui.bootstrap",
        "cfp.hotkeys"
    ])
    .controller("NavbarCtrl", function NavbarController($scope, hotkeys) {

        $scope.items = [
            { 'name': "phone", 'tooltip': "Smartphone", 'shortcut': "alt+q", 'icon': "mobile" },
            { 'name': "phone_portrait", 'tooltip': "Smartphone Portrait", 'shortcut': "alt+w", 'icon': "mobile l90" },
            { 'name': "tablet", 'tooltip': "Tablet Portrait", 'shortcut': "alt+e", 'icon': "tablet" },
            { 'name': "laptop", 'tooltip': "Laptop", 'shortcut': "alt+r", 'icon': "laptop" },
            { 'name': "desktop", 'tooltip': "Desktop", 'shortcut': "alt+t", 'icon': "desktop" },
            { 'name': "full", 'tooltip': "Fit to screen", 'shortcut': "alt+y", 'icon': "arrows" }
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

        
    });