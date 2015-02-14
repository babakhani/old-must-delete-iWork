angular.module('iWork').directive('deleteBtn', function () {
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
                    url: rootControllerUrl + entityController + "/remove",
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