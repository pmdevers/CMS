'use strict';

/**
 * @ngdoc directive
 * @name adminApp.directive:html
 * @description
 * # html
 */
angular.module('adminApp')
  .directive('ritchtext', function () {
    return {
      templateUrl: 'views/directives/ritchtext.html',
      replace: true,
      restrict: 'E',
      scope: { 
      	content: '='
      }
    };
  }); 
