angular.module( 'ngBoilerplate', [
  'templates-app',
  'templates-common',
  'ngBoilerplate.home',
  'ngBoilerplate.navbar',
  'ngBoilerplate.sidebar',
  'ngBoilerplate.builder',
  'ui.bootstrap',
  'ui.router',
  'ui.tree',
  'cfp.hotkeys',
  'ui.ace'
])

.config(function myAppConfig($stateProvider, $urlRouterProvider) {

    $urlRouterProvider.otherwise('/home');
})

.run( function run () {
})

.controller( 'AppCtrl', function AppCtrl ( $scope, $location, hotkeys ) {
  $scope.$on('$stateChangeSuccess', function(event, toState, toParams, fromState, fromParams){
    if ( angular.isDefined( toState.data.pageTitle ) ) {
      $scope.pageTitle = toState.data.pageTitle + ' | ngBoilerplate' ;
    }

      hotkeys.bindTo($scope)
          .add({
              combo : 'alt+q',
              description : 'blah blah',
              callback : function (){
                  alert('test');
              }
          });

  });
});
