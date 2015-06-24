'use strict';

/**
 * @ngdoc directive
 * @name adminApp.directive:html
 * @description
 * # html
 */
angular.module('adminApp')
  .directive('container', function () {
      return {
          templateUrl: 'views/directives/container.html',
          replace: true,
          restrict: 'E',
          scope: {
              content: '='
          }
      };
  });

angular.module('adminApp')
  .directive('row', function () {
      return {
          templateUrl: 'views/directives/container.html',
          replace: true,
          restrict: 'E',
          scope: {
              content: '='
          }
      };
  });
angular.module('adminApp')
  .directive('column', function () {
      return {
          templateUrl: 'views/directives/column.html',
          replace: true,
          restrict: 'E',
          scope: {
              content: '='
          }
      };
  });