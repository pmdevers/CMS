'use strict';

/**
 * @ngdoc directive
 * @name adminApp.directive:stringPlugin
 * @description
 * # stringPlugin
 */
angular.module('adminApp')
  .directive('string', function () {
    return {
      templateUrl: 'views/directives/string.html',
      replace: true,
      restrict: 'E',
      scope: { 
      	content: '='
      }
    };
  });


 