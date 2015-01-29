Imports Castle.MicroKernel.Registration
Imports Castle.Windsor
Imports Castle.MicroKernel.SubSystems.Configuration
Imports System.Reflection

Public MustInherit Class BaseInstaller
    Implements IWindsorInstaller

    Public MustOverride Sub Install(container As IWindsorContainer, store As IConfigurationStore) Implements IWindsorInstaller.Install

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

