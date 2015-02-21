iWork.controller('iWorkRoot', function ($rootScope, $translate, $scope, $route) {
    $rootScope.title = 'iWork';
    $rootScope.$on("$routeChangeSuccess", function (currentRoute, previousRoute) {
        //Change page title, based on Route information
        $rootScope.title = $route.current.title;
    });
});
