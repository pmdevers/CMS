'use strict';

/**
 * @ngdoc directive
 * @name adminApp.directive:html
 * @description
 * # html
 */
angular.module('adminApp')
  .directive('jumbotron', function () {
      return {
          templateUrl: 'views/directives/jumbotron.html',
          replace: true,
          restrict: 'E',
          scope: {
              content: '='
          }
      };
  });
