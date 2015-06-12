'use strict';

/**
 * @ngdoc service
 * @name adminApp.pageService
 * @description
 * # pageService
 * Service in the adminApp.
 */
angular.module('adminApp')
  .service('pageService', ['$http', function ($http) {
    // AngularJS will instantiate a singleton by calling "new" on this function

    this.getPages = function(callback){
		$http({
			url: '/api/page',
			method: 'GET'
		})
		.success(function (data, status, header, config){
        	callback(data);
    	});
	};

	this.getContent = function(page, callback){
		$http({
			url: '/api/content/' + page.Url,
			method: 'GET'
		})
		.success(function(data, status, header, config){
			callback(data);
		})
	};

	this.saveContent = function(page, content, callback)
	{
		$http({
			url: '/api/content/' + page.Url,
			method:'POST',
			data: content
		})
		.success(function(data, status, header, config){
			callback(data);
		});
	};

}]);
