Imports Owin
Imports Microsoft.Owin
Imports System.Web.Http
Imports Castle.Windsor
Imports Castle.MicroKernel.Registration
Imports System.Reflection
Imports iWork.Core.Repositories
Imports iWork.Core.Services
Imports iWork.Core.Controllers
Imports Microsoft.Owin.Security.OAuth
Imports Microsoft.Owin.Security.Google
Imports Microsoft.Owin.Security.Facebook
Imports System.Runtime.CompilerServices

Public Module StartupConfiguration


    Public OAuthBearerOptions As OAuthBearerAuthenticationOptions
    Public googleAuthOptions As GoogleOAuth2AuthenticationOptions
    Public facebookAuthOptions As FacebookAuthenticationOptions

    <Extension>
    Public Sub CoreConfig(app As IAppBuilder)

        Dim container As IWindsorContainer = Bootstrap()
        Dim config As New HttpConfiguration()

        ConfigureOAuth(app, container)
        config.DependencyResolver = New DependencyResolver(container)

        config.MapHttpAttributeRoutes()
        config.Routes.MapHttpRoute(
            name:="DefaultApi",
            routeTemplate:="api/{controller}/{action}/{id}",
            defaults:=New With {.id = RouteParameter.Optional}
        )

        app.UseWebApi(config)
        app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll)
        'todo: think about database startup

    End Sub

    Public Sub ConfigureOAuth(app As IAppBuilder, container As IWindsorContainer)

        app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie)
        OAuthBearerOptions = New OAuthBearerAuthenticationOptions()

        Dim OAuthServerOptions As New OAuthAuthorizationServerOptions()
        With OAuthServerOptions
            .AllowInsecureHttp = True
            .TokenEndpointPath = New PathString("/token")
            .AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30)
            .Provider = New SimpleAuthorizationServerProvider ' Application.GetService(Of IAuthorizationServerService)()
            '.RefreshTokenProvider = Application.GetService(Of IRefreshTokenService)()
        End With


        app.UseOAuthAuthorizationServer(OAuthServerOptions)
        app.UseOAuthBearerAuthentication(OAuthBearerOptions)

        googleAuthOptions = New GoogleOAuth2AuthenticationOptions()
        With googleAuthOptions
            .ClientId = "-.googleusercontent.com"
            .ClientSecret = ""
            .Provider = New GoogleAuthenticationService()
        End With
        app.UseGoogleAuthentication(googleAuthOptions)


        facebookAuthOptions = New FacebookAuthenticationOptions()
        With facebookAuthOptions
            .AppId = ""
            .AppSecret = ""
            .Provider = New FacebookAuthenticationService()
        End With
        app.UseFacebookAuthentication(facebookAuthOptions)

    End Sub

    Private Function Bootstrap() As IWindsorContainer

        Dim container = New WindsorContainer("config/Castle.config")
        container.Register(Classes.FromAssemblyInDirectory(GetAssemblyFilter(container)).BasedOn(Of IUnitOfWork).WithService.FromInterface.LifestylePerWebRequest)
        container.Register(Classes.FromAssemblyInDirectory(GetAssemblyFilter(container)).BasedOn(Of IRepository).WithService.FromInterface.LifestylePerWebRequest)
        container.Register(Classes.FromAssemblyInDirectory(GetAssemblyFilter(container)).BasedOn(Of IService).WithService.FromInterface.LifestylePerWebRequest)
        container.Register(Classes.FromAssemblyInDirectory(GetAssemblyFilter(container)).BasedOn(Of IController).WithService.FromInterface.LifestylePerWebRequest)
        'container.Register(Classes.FromAssemblyInDirectory(GetAssemblyFilter(container)).BasedOn(Of IRefreshTokenService).WithService.FromInterface.LifestylePerWebRequest)
        container.Register(Component.For(Of IWindsorContainer).Instance(container).LifestyleSingleton)

#If DEBUG Then

        Debug.WriteLine("----------------------------------------------------")
        Debug.WriteLine("Castle Windsor Registrations")
        Debug.WriteLine("----------------------------------------------------")
        For Each handler In container.Kernel.GetAssignableHandlers(GetType(Object))
            For Each ser In handler.ComponentModel.Services
                Debug.WriteLine(ser.Name & "," & handler.ComponentModel.Implementation.Name)
            Next
        Next
        Debug.WriteLine("----------------------------------------------------")

#End If

        Application.InitializeContainer(AddressOf container.Resolve(Of IServiceContainer))

        Return container

    End Function

    Private Function GetAssemblyFilter(container As IWindsorContainer) As AssemblyFilter

        Dim codeBase = Assembly.GetExecutingAssembly().CodeBase
        Dim uriBuilder = (New UriBuilder(codeBase))
        Dim path = Uri.UnescapeDataString(uriBuilder.Path)
        Dim assemblyDirectory = System.IO.Path.GetDirectoryName(path)

        Return New AssemblyFilter(assemblyDirectory)

    End Function

End Module