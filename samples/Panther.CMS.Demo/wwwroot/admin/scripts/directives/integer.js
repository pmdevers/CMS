'use strict';

/**
 * @ngdoc directive
 * @name adminApp.directive:integer
 * @description
 * # integer
 */
angular.module('adminApp')
  .directive('integer', function () {
    return {
      templateUrl: 'views/directives/integer.html',
      replace: true,
      restrict: 'E',
      scope: { 
      	content: '='
      },
      link: function($scope) {
      	$scope.decrement = function()
      	{
      		$scope.content.Data = Number($scope.content.Data) + 1
      	};
      	$scope.increment = function()
      	{
      		$scope.content.Data = Number($scope.content.Data) - 1;
      	}
      }
    };
  }); 