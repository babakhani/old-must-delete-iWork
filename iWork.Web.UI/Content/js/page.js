var rootControllerUrl = '/api/';

$(document).ready(function () {

    $('#navbar li').each(function () {
        var thisUrl = $(this).find('a').attr("href");
        if (window.location.href.indexOf(thisUrl) > 0) {
            $(this).addClass('active');
        }
    });

    // Input Delete Script's
    $(document).on('click', '.remove-entity', function () {
        var entityID = $(this).attr('data-entity-id');
        var entityModel = $(this).attr('data-model');
        var removeConfirm = $(this).attr('data-confirm');
        if (removeConfirm && removeConfirm !== 'false') {
            var conf = confirm("Remove " + entityModel + " " + entityID + "!");
        } else {
            var conf = true;
        }

        if (conf == true) {
            $.ajax({
                method: 'POST',
                url: rootControllerUrl + entityModel + "/remove",
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


    // Slick View Script's
    $('.slick-container').slick({
        adaptiveHeight: true,
        accessibility: true,
        arrows: false,
        swipe:false
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
        $('[data-toggle]').removeClass('selected')
        $(this).addClass('selected');
    });
    // END

    $('.svg-inject').svgInject();
    


});