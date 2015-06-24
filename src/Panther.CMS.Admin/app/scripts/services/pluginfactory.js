'use strict';

/**
 * @ngdoc service
 * @name adminApp.pluginFactory
 * @description
 * # pluginFactory
 * Factory in the adminApp.
 */
angular.module('adminApp').factory('pluginFactory', function($q, $timeout) {
  return function() {
      var d = $q.defer();
      $timeout(function() {
          d.resolve('one');
      }, 1);
      
      return d.promise;
  };
});