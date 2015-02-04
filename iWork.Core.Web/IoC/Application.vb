Imports Castle.Windsor
Imports iWork.Core.Repository
Imports iWork.Core.Service

Public Class Application
    Implements IApplication

    Private _container As IWindsorContainer

    Public Sub New(container As IWindsorContainer)
        _container = container
    End Sub

    Public Function GetService(Of T)() As T Implements IApplication.GetService
        Return _container.Resolve(Of T)()
    End Function

End Class


