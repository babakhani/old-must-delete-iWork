var rootControllerUrl = '/api/';
var iDropzone;
Dropzone.autoDiscover = false;

angular.module('iwork', ['datatables', 'mgcrea.ngStrap', 'frapontillo.bootstrap-switch']);

var log = function (x) {
    console.log(x);
}
$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name] !== undefined) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};

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
                    ContactId: entityID
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
        $('.slick-container').slick('slickGoTo', window.location.hash.split('/')[1]);
    }

    //====================================================================================== END

    $('.svg-inject').svgInject();

    $(window).resize(function () {
        $('.slick-list').height('auto');
    });



});