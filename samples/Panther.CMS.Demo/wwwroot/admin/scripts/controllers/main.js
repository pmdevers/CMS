'use strict';

/**
 * @ngdoc function
 * @name adminApp.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the adminApp
 */
angular.module('adminApp')
  .controller('MainCtrl', ['$scope','pageService', 'siteService', function ($scope, pageService, siteService) {

    $scope.site = {};
  	$scope.currentPage = {};
  	$scope.contents = [];
    $scope.previewMode = "desktop";

    $scope.currentUrl = "";
  	  	
  	pageService.getPages(function(data) {
          $scope.pages = data;
 	  });

    siteService.getSite(function(data){
        $scope.site = data;
    })

  	$scope.editPage = function(page) {
  		$scope.currentPage = page;
      $scope.currentUrl = "http://" + $scope.site.Url + "/" + page.Url;

  		pageService.getContent(page, function(data) {
  			$scope.contents = data;
  		})
  	};

  	$scope.saveContent = function() {
  		pageService.saveContent($scope.currentPage, $scope.contents, function(data){
        var d = new Date();
        var n = d.getTime(); 

        $scope.currentUrl = "http://" + $scope.site.Url + "/" + $scope.currentPage.Url + "?" + n;
      });
  	}

    $scope.setPreviewMode = function (mode) {
      $scope.previewMode = mode;
    }

  }]);
