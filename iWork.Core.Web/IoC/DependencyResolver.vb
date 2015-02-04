Imports System.Web.Http.Dependencies
Imports Castle.Windsor

Public Class DependencyResolver
    Implements IDependencyResolver

    Private _container As IWindsorContainer
    Public ReadOnly Property Container As IWindsorContainer
        Get
            Return _container
        End Get
    End Property

    Public Sub New(container As IWindsorContainer)

        If container Is Nothing Then
            Throw New ArgumentNullException("container", "The instance of the container cannot be null.")
        End If
        _container = container

    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Public Function GetService(serviceType As Type) As Object Implements IDependencyScope.GetService
        Try
            Return Container.Resolve(serviceType)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetServices(serviceType As Type) As IEnumerable(Of Object) Implements IDependencyScope.GetServices
        Return Container.ResolveAll(serviceType).Cast(Of Object)()
    End Function

    Private disposedValue As Boolean

    Protected Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                If _container IsNot Nothing Then
                    _container.Dispose()
                    _container = Nothing
                End If
            End If
        End If
        disposedValue = True
    End Sub

    Public Function BeginScope() As IDependencyScope Implements IDependencyResolver.BeginScope
        Return New DependencyScope(_container)
    End Function

End Class
