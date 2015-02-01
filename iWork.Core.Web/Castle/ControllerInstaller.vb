Imports Castle.MicroKernel.Registration
Imports System.Web.Http
Imports Castle.Windsor
Imports Castle.MicroKernel.SubSystems.Configuration
Imports System.Reflection
Imports Castle.DynamicProxy
Imports iWork.Core.Web.Controllers

Public Class ControllerInstaller
    Inherits BaseInstaller

    Public Overrides Sub Install(container As IWindsorContainer, store As IConfigurationStore)

        'todo: implement proxy and remove basecontroller
        'Dim proxyBuilder As New DefaultProxyBuilder
        'Dim baseType As Type = GetType(ApiController)
        'Dim intefacesImplemented As Type() = {GetType(IController)}
        'Dim proxy As Type = proxyBuilder.CreateClassProxyType(baseType, intefacesImplemented, ProxyGenerationOptions.Default)
        'container.Register(Component.For(Of IController).Forward(proxy).LifestyleScoped)
        'Dim u = Classes.FromAssemblyInDirectory(AssemblyFilter).BasedOn(Of IController).ConfigureFor(Of ApiController)(Function(x) x.Forward(proxy).LifestyleScoped)
        'container.Register(u)

        container.Register(Classes.FromAssemblyInDirectory(AssemblyFilter).BasedOn(Of ApiController).Configure(Function(x) x.LifestyleScoped())) '.Configure(Function(x) x.Interceptors(Of AuthorizationInterceptor)()))
        '.Interceptors(Of AuthorizationInterceptor)
        'container.Register(Classes.FromAssemblyInDirectory(AssemblyFilter).BasedOn(Of ApiController).LifestyleScoped().Configure(Function(c) c.Interceptors(Of AuthorizationInterceptor)()))


    End Sub

End Class