'use strict';

/**
 * @ngdoc directive
 * @name adminApp.directive:plugin
 * @description
 * # plugin
 */
angular.module('adminApp').directive('plugin', ['$compile', function ($compile) {
    return {
      restrict: 'E',
      replace: true,
      scope: { 'plugin': '@', 'content': '=' },
      link: function postLink(scope, element, attrs) {
        
        	var template = '<' + scope.plugin + ' content="content" />',
        		compiled = $compile(template)(scope);

        	element.append(compiled);
      }
    };
}]);