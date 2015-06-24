'use strict';

/**
 * @ngdoc directive
 * @name adminApp.directive:stringPlugin
 * @description
 * # stringPlugin
 */
angular.module('adminApp')
  .directive('tags', function () {
    return {
      templateUrl: 'views/directives/tags.html',
      replace: true,
      restrict: 'E',
      scope: { 
      	content: '='
      }
    };
  });


 