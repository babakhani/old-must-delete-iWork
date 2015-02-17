﻿angular.module('iWork').directive('deleteBtn', function (appConfig) {
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
                   $self.parents('tr').remove();
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