'use strict';

/**
 * @ngdoc service
 * @name adminApp.pageService
 * @description
 * # pageService
 * Service in the adminApp.
 */
angular.module('adminApp')
  .service('siteService', ['$http', function ($http) {
    // AngularJS will instantiate a singleton by calling "new" on this function

    this.getSite = function(callback)
    {
    	$http({
    	    url: '/api/site'
    	})
    	.success(function(data, status, header, config){
            callback(data);
		});
    }
}]);