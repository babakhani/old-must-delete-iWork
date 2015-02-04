Imports Castle.MicroKernel.Registration
Imports Castle.Windsor
Imports Castle.MicroKernel.SubSystems.Configuration
Imports System.Reflection
Imports Castle.DynamicProxy
Imports iWork.Core.Web.Controllers

Public Class ControllerInstaller
    Implements IWindsorInstaller

    Public Sub Install(container As IWindsorContainer, store As IConfigurationStore) Implements IWindsorInstaller.Install
        container.Register(Classes.FromAssemblyInDirectory(AssemblyFilter).BasedOn(Of BaseController).LifestyleScoped.Configure(Function(c) c.Interceptors(Of AuthorizationInterceptor)()))
    End Sub

    Public ReadOnly Property AssemblyDirectory() As String
        Get
            Dim codeBase = Assembly.GetExecutingAssembly().CodeBase
            Dim uriBuilder = (New UriBuilder(codeBase))
            Dim path = Uri.UnescapeDataString(uriBuilder.Path)
            Return System.IO.Path.GetDirectoryName(path)
        End Get
    End Property

    Public ReadOnly Property AssemblyFilter As AssemblyFilter
        Get
            Return New AssemblyFilter(AssemblyDirectory)
        End Get
    End Property

End Class