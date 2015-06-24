'use strict';

/**
 * @ngdoc directive
 * @name adminApp.directive:boolean
 * @description
 * # boolean
 */
angular.module('adminApp')
  .directive('boolean', function () {
    return {
      templateUrl: 'views/directives/boolean.html',
      replace: true,
      restrict: 'E',
      scope: { 
      	content: '='
      }
    };
  });
 