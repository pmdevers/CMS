'use strict';

/**
 * @ngdoc service
 * @name adminApp.contentService
 * @description
 * # contentService
 * Service in the adminApp.
 */
angular.module('adminApp')
  .service('contentService', ['$http', function ($http) {
    // AngularJS will instantiate a singleton by calling "new" on this function
    return {
    	getContent: function(){
    		var content = [];
    		$http.get('/api/page').success(function(data){
    			pages = data;
    		});
    	}
    }
  }]);
