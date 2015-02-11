Imports Castle.Windsor

Public Class ServiceContainer
    Implements IServiceContainer

    Private _container As IWindsorContainer
    Public Sub New(container As IWindsorContainer)
        _container = container
    End Sub

    Public Function GetService(Of T)() As T Implements IServiceContainer.GetService
        Return _container.Resolve(Of T)()
    End Function

End Class