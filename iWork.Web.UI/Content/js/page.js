var iWork = angular.module('iWork', ['ngRoute', 'ngMessages', 'ngAnimate', 'mgcrea.ngStrap', 'frapontillo.bootstrap-switch']);
iWork.constant("appConfig", {
    "animateTime": 200,
    rootControllerUrl: '/api/'
});
iWork.config(function ($controllerProvider, $routeProvider, $compileProvider, $filterProvider, $provide) {
    iWork.controller = $controllerProvider.register;
    $routeProvider
    .when('/contact/form', {
        templateUrl: '/iView/Contacts/form.html'
    })
    .when('/contact/form/:entityId', {
        templateUrl: '/iView/Contacts/form.html'
    })
    .when('/contact/grid', {
        templateUrl: '/iView/Contacts/grid.html',
    })
    .when('/kitchensink/form', {
        templateUrl: '/iView/kitchensink/form.html',
    })
    .when('/kitchensink/grid-observer', {
        templateUrl: '/iView/kitchensink/grid-observer.html',
    })
    .when('/kitchensink/grid-ajax', {
        templateUrl: '/iView/kitchensink/grid-ajax.html',
    })
    .when('/kitchensink/form-validation', {
        templateUrl: '/iView/kitchensink/form-validation.html',
    }).when('/kitchensink/mask', {
        templateUrl: '/iView/kitchensink/mask.html',
    }).when('/kitchensink/password', {
        templateUrl: '/iView/kitchensink/password.html',
    });
});

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

$(document).ready(function () {
    $('#navbar li').each(function () {
        var thisUrl = $(this).find('a').attr("href");
        if (window.location.href.indexOf(thisUrl) > 0) {
            $(this).addClass('active');
        }
    });
    $('a.btn-iconic').each(function () {
        var thisUrl = $(this).attr("href");
        if (window.location.hash.indexOf(thisUrl) >= 0) {
            $(this).addClass('selected');
        }
    });

    // Just Used by member-territory
    $('img.svg-inject').svgInject();

    if (window.location.hash == '') {
        $('a.btn-iconic').first().trigger('click');
    }
    $('.main-content').height($(window).height() - 100);
    $(window).resize(function () {
        $('.slick-list').height('auto');
        $('.main-content').height($(window).height() - 100);
    });
});