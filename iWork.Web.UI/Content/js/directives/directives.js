angular.module('iWork').directive('deleteBtn', function (appConfig) {
    return {
        link: function ($scope, element, attrs) {
            element.confirmation({
                btnOkClass: 'btn-sm btn-danger',
                btnCancelClass: 'btn-sm btn-default',
                singleton: true,
                popout: true
            });
            element.bind('click', function () {
                var $self = $(this);
                var entityID = $self.attr('data-entity-id');
                var entityController = $self.data('model');
                $.ajax({
                    method: 'POST',
                    url: appConfig.rootControllerUrl + entityController + "/remove",
                    data: {
                        id: entityID
                    },
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                }).
               success(function (responseData, status, headers, config) {
                   $self.parents('tr').fadeOut('slow', function () {
                       $(this).remove();
                   });
               }).
               error(function (responseData, status, headers, config) {
               });
            });
        }
    }
});

angular.module('iWork').directive('svgInject', function (appConfig) {
    return {
        link: function ($scope, element, attrs) {
            element.svgInject();
        }
    }
});

angular.module('iWork').directive('magicSuggest', function (appConfig) {
    return {
        link: function ($scope, element, attrs) {
            element.magicSuggest({
                name: element.data('name')
            });
        }
    }
});


angular.module('iWork').directive('dropzone', function (appConfig) {
    return {
        link: function ($scope, element, attrs) {
            element.dropzone({
                url: "/file/post",
                autoProcessQueue: false,
                uploadMultiple: true,
                parallelUploads: 3,
                maxFiles: 3,
                init: function () {
                    var myDropzone = this;
                    // First change the button to actually tell Dropzone to process the queue.
                    $(this.element).parents('form').find('button').click(function (e) {
                        // Make sure that the form isn't actually being sent.
                        e.preventDefault();
                        e.stopPropagation();
                        myDropzone.processQueue();
                    });
                    // Listen to the sendingmultiple event. In this case, it's the sendingmultiple event instead
                    // of the sending event because uploadMultiple is set to true.
                    this.on("sendingmultiple", function () {
                        // Gets triggered when the form is actually being sent.
                        // Hide the success button or the complete form.
                    });
                    this.on("successmultiple", function (files, response) {
                        // Gets triggered when the files have successfully been sent.
                        // Redirect user or notify of success.
                    });
                    this.on("errormultiple", function (files, response) {
                        // Gets triggered when there was an error sending the files.
                        // Maybe show form again, and notify user of error
                    });
                }
            });
        }
    }
});


/* Sample Custom validator*/
iWork.directive('customValidation',
  ['$http', function ($http) {
      return {
          require: 'ngModel',
          link: function (scope, element, attrs, ngModel) {
              ngModel.$setValidity('customValidation', true);
          }
      }
  }]);

/* Input Mask Directive 
    https://github.com/RobinHerbots/jquery.inputmask */

iWork.directive('inputMask', function () {
    return {
        restrict: 'A',
        link: function (scope, el, attrs) {
            $(el).inputmask(scope.$eval(attrs.inputMask));
        }
    };
});



iWork.directive('match', ['$parse', function ($parse) {
    return {
        require: 'ngModel',
        restrict: 'A',
        link: function (scope, elem, attrs, ctrl) {
            //This part does the matching
            scope.$watch(function () {
                return (ctrl.$pristine && angular.isUndefined(ctrl.$modelValue)) || $parse(attrs.match)(scope) === ctrl.$modelValue;
            }, function (currentValue) {
                ctrl.$setValidity('match', currentValue);
            });

            //This part is supposed to check the strength
            ctrl.$parsers.unshift(function (viewValue) {
                var pwdValidLength, pwdHasLetter, pwdHasNumber;

                pwdValidLength = (viewValue && viewValue.length >= 8 ? true : false);
                pwdHasLetter = (viewValue && /[A-z]/.test(viewValue)) ? true : false;
                pwdHasNumber = (viewValue && /\d/.test(viewValue)) ? true : false;

                if (pwdValidLength && pwdHasLetter && pwdHasNumber) {
                    ctrl.$setValidity('pwd', true);
                } else {
                    ctrl.$setValidity('pwd', false);
                }
                return viewValue;
            });
        },
    };
}]);


iWork.controller('viewLinkController', function ($scope, $attrs) {
    this.init = function (elem) {
        var $link = $(elem);
        var thisViewname = $link.attr('viewname');
        $.each(sitemap, function (index, item) {
            if (thisViewname == item.viewname) {
                $link.attr({
                    href: '#' + item.href
                });
            }
        });

        var thisUrl = $link.attr("href");
        if (window.location.hash.indexOf(thisUrl) >= 0) {
            $link.addClass('selected');
        }

    };
});

iWork.directive('viewLink', ['$parse', function ($parse) {
    return {
        controller: 'viewLinkController',
        link: function (scope, elem, attrs, ctrl) {
            ctrl.init(elem);
        },
    };
}]);
