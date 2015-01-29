Imports Castle.Windsor
Imports Castle.MicroKernel.Registration
Imports Castle.MicroKernel.SubSystems.Configuration

Public Class Installer(Of TInterface)
    Inherits BaseInstaller

    Public Overrides Sub Install(container As IWindsorContainer, store As IConfigurationStore)
        container.Register(Classes.FromAssemblyInDirectory(AssemblyFilter).BasedOn(Of TInterface).WithService.FromInterface.LifestylePerWebRequest)
    End Sub

End Class