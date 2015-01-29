Imports System.Reflection
Imports Castle.Windsor

Public Class Application

    Private Shared _container As WindsorContainer
    Friend Shared ReadOnly Property Container As WindsorContainer
        Get
            Return _container
        End Get
    End Property

    Public Sub Install()

        _container = New WindsorContainer("Castle.config")

        _container.Install(New Installer(Of IRepository))
        _container.Install(New Installer(Of IUnitOfWork))
        _container.Install(New Installer(Of IService))

    End Sub

    Public Shared Function GetService(Of T)() As T
        Try
            Return Container.Resolve(Of T)()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetServices(Of T)() As IEnumerable(Of T)
        Return Container.ResolveAll(Of T)()
    End Function

End Class


