var iDropzone;
Dropzone.autoDiscover = false;
var iWork = angular.module('iWork', ['ngRoute', 'mgcrea.ngStrap', 'frapontillo.bootstrap-switch'])

iWork.config(function ($routeProvider, $controllerProvider, $compileProvider, $filterProvider, $provide) {

    iWork.controllerProvider = $controllerProvider;
    iWork.compileProvider = $compileProvider;
    iWork.routeProvider = $routeProvider;
    iWork.filterProvider = $filterProvider;
    iWork.provide = $provide;

    $routeProvider
    .when('/contact/form', {
        templateUrl: '/iView/Contacts/form.html',
        controller: 'Contact_form'
    }).when('/contact/grid', {
        templateUrl: '/iView/Contacts/grid.html',
    });
});


$(document).ready(function () {
    $('#navbar li').each(function () {
        var thisUrl = $(this).find('a').attr("href");
        if (window.location.href.indexOf(thisUrl) > 0) {
            $(this).addClass('active');
        }
    });

    // Input Delete Script's
    $(document).on('click', 'span.remove-entity', function () {
        var entityID = $(this).data('entity-id');
        var entityController = $(this).data('model');
        var removeConfirm = $(this).data('confirm');
        if (removeConfirm && removeConfirm !== 'false') {
            var conf = confirm("Remove " + entityController + " " + entityID + "!");
        } else {
            var conf = true;
        }
        if (conf == true) {
            $.ajax({
                method: 'POST',
                url: rootControllerUrl + entityController + "/remove",
                data: {
                    id: entityID
                },
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).
           success(function (responseData, status, headers, config) {
               console.log("success Ajax delete")
               console.log(responseData)
           }).
           error(function (responseData, status, headers, config) {
               console.log("error Ajax delete")
               console.log(responseData)
           });
        }
    });
    // END

    // Input Update Script's
    $(document).on('click', '.update-entity', function () {
        var entityID = $(this).data('entity-id');
        var formName = $(this).data('target-form');

        var controllerElement = $('form[name=' + formName + ']').parents('[ng-controller]');
        controllerElement.scope().formUpdate(entityID);
    });
    // END

    /* Slick View Script's
    =====================================================================================*/

   
    $('.slick-container').slick({
        adaptiveHeight: true,
        accessibility: false,
        arrows: false,
        edgeFriction: 10,
        infinite: false,
        swipe: false
    }).on('afterChange', function () {
        $('button[data-toggle=slick]').each(function () {
            if ($(this).data("target") == $('.slick-container').slick('slickCurrentSlide')) {
                $('[data-toggle]').removeClass('selected')
                $(this).addClass('selected')
            }
        });
    })
    $('button[data-toggle=slick]').each(function () {
        if ($(this).data("target") == $('.slick-container').slick('slickCurrentSlide')) {
            $('[data-toggle]').removeClass('selected')
            $(this).addClass('selected')
        }
    });

    $('button[data-toggle=slick]').click(function () {
        $('.slick-container').slick('slickGoTo', $(this).data("target"));

        window.location.hash = $(this).data("target");

        $('[data-toggle]').removeClass('selected')
        $(this).addClass('selected');
    });

    if (window.location.hash !== '') {
        $('.slick-container').slick('slickGoTo', parseInt(window.location.hash.match(/\d+/)[0]));
    }
    

    //====================================================================================== END

    $('.svg-inject').svgInject();

    $(window).resize(function () {
        $('.slick-list').height('auto');
    });



});