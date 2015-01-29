'Imports Castle.Windsor
'Imports Castle.Windsor.Installer
'Imports Castle.MicroKernel.Registration
'Imports Castle.MicroKernel

'Public Class IocContainer

'    Private Shared contianer As IWindsorContainer

'    Public Shared Sub SetUp()

'        contianer = New WindsorContainer().Install(FromAssembly.This())

'        Dim controllerFactory As New WindsorControllerFactory(contianer.Kernel)
'        ControllerBuilder.Current.SetControllerFactory(controllerFactory)


'    End Sub

'End Class

'Public Class ControllersInstaller
'    Implements IWindsorInstaller


'    Public Sub Install(container As IWindsorContainer, store As Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore) Implements IWindsorInstaller.Install

'        container.Register(Classes.FromThisAssembly().Pick().If(Function(t) t.Name.EndsWith("Controller")).Configure(Function(configurer) configurer.Named(configurer.Implementation.Name)).LifestylePerWebRequest())

'    End Sub

'End Class

'Public Class WindsorControllerFactory

'    Private _kernel As IKernel

'    Public Sub New(kernel As IKernel)
'        _kernel = kernel
'    End Sub

'End Class
