iWork.controller('submenu', function ($scope, $routeParams, $element, $rootScope, $http) {
    $scope.$on('$routeChangeSuccess', function (scope, next, current) {
        $('a.btn-iconic').each(function () {
            $(this).removeClass('selected');
            var thisUrl = $(this).attr("href");
            if (window.location.hash == thisUrl) {
                $(this).addClass('selected');
            }
        });
    });
});
