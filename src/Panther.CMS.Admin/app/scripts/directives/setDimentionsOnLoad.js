'use strict';


angular.module('adminApp')
  .directive('setDimentionsOnload', [function(){
    return {
        restrict: 'A',
        link: function(scope, element, attrs){
            element.on('load', function(){
                /* Set the dimensions here, 
                   I think that you were trying to do something like this: */
                   var iFrameHeight = element[0].contentWindow.document.body.scrollHeight + 'px';
                   //var iFrameWidth = element[0].parent.width; // '100%';
                   //var iFrameWidth = element[0].parentElement.clientWidth * 1.5; // '100%';
                   //element.css('width', iFrameWidth);
                   element.css('height', iFrameHeight);
            });
        }
    }
  }]);