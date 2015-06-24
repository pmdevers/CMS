'use strict';

/**
 * @ngdoc directive
 * @name adminApp.directive:integer
 * @description
 * # integer
 */
angular.module('adminApp')
  .directive('image1', function () {
    return {
      templateUrl: 'views/directives/image.html',
      replace: true,
      restrict: 'E',
      scope: { 
      	content: '='
      },
      link: function($scope)
      {
        
      }
    };
  }); 