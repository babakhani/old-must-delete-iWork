var iDropzone;
Dropzone.autoDiscover = false;
var iWork = angular.module('iWork', ['ngRoute', 'ngAnimate', 'mgcrea.ngStrap', 'frapontillo.bootstrap-switch']);
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
    });
});

iWork.controller('submenu', function ($scope, $routeParams, $element, $rootScope, $http) {
    $scope.$on('$routeChangeSuccess', function (scope, next, current) {
        $('a.btn-iconic').each(function () {
            $(this).removeClass('selected');
            var thisUrl = $(this).attr("href");
            if (window.location.hash.indexOf(thisUrl) >= 0) {
                $(this).addClass('selected');
            }
        });
    });
});

$(document).ready(function () {
    $(document).on('confirmed.bs.confirmation', 'a.remove-entity', function () {
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
    if (window.location.hash == '') {
        $('a.btn-iconic').first().trigger('click');
    }
    $('.main-content').height($(window).height() - 100);
    $('.svg-inject').svgInject();
    $(window).resize(function () {
        $('.slick-list').height('auto');
        $('.main-content').height($(window).height() - 100);
    });
});