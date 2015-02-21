﻿
iWork.controller('TranslateController', function ($rootScope, $translate, $scope) {
    $scope.changeLanguage = function (langKey) {
        $translate.use(langKey);
    };
    $rootScope.$on('$translateChangeSuccess', function (a, b) {
        $scope.checkLayoutDirection(b.language);
        $('.translate-buttton-box button').each(function () {
            if ($(this).attr('ng-click').indexOf(b.language) > 0) {
                $(this).addClass('selected');
            } else {
                $(this).removeClass('selected');
            }
        });
    });
    $scope.checkLayoutDirection = function (lang) {
        if (lang == 'fa') {
            loadStyleSheet('/Content/lib/bootstrap-rtl/css/bootstrap-rtl.css');
            loadStyleSheet('/Content/css/style.rtl.css');
        } else {
            $("link[href='/Content/lib/bootstrap-rtl/css/bootstrap-rtl.css']").remove();
            $("link[href='/Content/css/style.rtl.css']").remove();

        }
    }
});
