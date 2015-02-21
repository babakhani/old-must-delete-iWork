// NOTE: This is sample code and all off this remove on release;
angular.module('MyApp', ['ngResource', 'ngMessages', 'ui.router', 'mgcrea.ngStrap', 'satellizer'])
  .config(function ($stateProvider, $urlRouterProvider, $authProvider) {

      $authProvider.httpInterceptor = true; // Add Authorization header to HTTP request
      $authProvider.loginOnSignup = true;
      $authProvider.loginRedirect = '/';
      $authProvider.logoutRedirect = '/';
      $authProvider.signupRedirect = '/login';
      $authProvider.loginUrl = '/auth/login';
      $authProvider.signupUrl = '/auth/signup';
      $authProvider.loginRoute = '/login';
      $authProvider.signupRoute = '/signup';
      $authProvider.tokenRoot = false; // set the token parent element if the token is not the JSON root
      $authProvider.tokenName = 'token';
      $authProvider.tokenPrefix = 'satellizer'; // Local Storage name prefix
      $authProvider.unlinkUrl = '/auth/unlink/';
      $authProvider.unlinkMethod = 'get';
      $authProvider.authHeader = 'Authorization';
      $authProvider.withCredentials = true; // Send POST request with credentials

      $stateProvider
        .state('home', {
            url: '/',
            templateUrl: '/iView/login/partials/home.html'
        })
        .state('login', {
            url: '/login',
            templateUrl: '/iView/login/partials/login.html',
            controller: 'LoginCtrl'
        })
        .state('signup', {
            url: '/signup',
            templateUrl: '/iView/login/partials/signup.html',
            controller: 'SignupCtrl'
        })
        .state('logout', {
            url: '/logout',
            template: null,
            controller: 'LogoutCtrl'
        })
        .state('profile', {
            url: '/profile',
            templateUrl: '/iView/login/partials/profile.html',
            controller: 'ProfileCtrl',
            resolve: {
                authenticated: function ($q, $location, $auth) {
                    var deferred = $q.defer();

                    if (!$auth.isAuthenticated()) {
                        $location.path('/login');
                    } else {
                        deferred.resolve();
                    }

                    return deferred.promise;
                }
            }
        });

      $urlRouterProvider.otherwise('/');
      $authProvider.google({
          clientId: '219645199901-er0d4s2agltnn2aenih4ruikgplov2na.apps.googleusercontent.com'
      });

      $authProvider.facebook({
          clientId: '657854390977827'
      });
      $authProvider.github({
          clientId: '0ba2600b1dbdb756688b'
      });
      $authProvider.linkedin({
          clientId: '77cw786yignpzj'
      });
  });
