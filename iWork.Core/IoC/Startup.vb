Imports Owin
Imports Microsoft.Owin
Imports System.Web.Http
Imports Castle.Windsor
Imports Castle.MicroKernel.Registration
Imports System.Reflection
Imports iWork.Core.Repositories
Imports iWork.Core.Services
Imports iWork.Core.Controllers

<Assembly: OwinStartup("StartupConfiguration", GetType(Startup))> 
Public Class Startup


    Public Sub Configuration(app As IAppBuilder)

        Dim container As IWindsorContainer = Bootstrap()
        Dim config As New HttpConfiguration()
        config.DependencyResolver = New DependencyResolver(container)

        config.MapHttpAttributeRoutes()
        config.Routes.MapHttpRoute(
            name:="DefaultApi",
            routeTemplate:="api/{controller}/{action}/{id}",
            defaults:=New With {.id = RouteParameter.Optional}
        )

        app.UseWebApi(config)

    End Sub

    Private Function Bootstrap() As IWindsorContainer

        Dim container = New WindsorContainer("config/Castle.config")
        container.Register(Classes.FromAssemblyInDirectory(GetAssemblyFilter(container)).BasedOn(Of IUnitOfWork).WithService.FromInterface.LifestylePerWebRequest)
        container.Register(Classes.FromAssemblyInDirectory(GetAssemblyFilter(container)).BasedOn(Of IRepository).WithService.FromInterface.LifestylePerWebRequest)
        container.Register(Classes.FromAssemblyInDirectory(GetAssemblyFilter(container)).BasedOn(Of IService).WithService.FromInterface.LifestylePerWebRequest)
        container.Register(Classes.FromAssemblyInDirectory(GetAssemblyFilter(container)).BasedOn(Of IController).WithService.FromInterface.LifestylePerWebRequest)
        container.Register(Component.For(Of IWindsorContainer).Instance(container).LifestyleSingleton)

#If DEBUG Then

        'todo: amir remove debug trace later
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

End Class