Imports System.Web.Http.Dependencies
Imports Castle.MicroKernel.Lifestyle.LifestyleExtensions
Imports Castle.Windsor

Public Class DependencyScope
    Implements IDependencyScope
    'Implements IScope

    Private _container As IWindsorContainer
    Private _scope As IDisposable

    Public ReadOnly Property Container As IWindsorContainer
        Get
            Return _container
        End Get
    End Property

    Public Sub New(container As IWindsorContainer)
        If container Is Nothing Then
            Throw New ArgumentNullException("container")
        End If
        _container = container
        _scope = container.BeginScope
    End Sub

    Public Function GetService(serviceType As Type) As Object Implements IDependencyScope.GetService

        If _container.Kernel.HasComponent(serviceType) Then
            Return _container.Kernel.Resolve(serviceType)
        Else
            Return Nothing
        End If

    End Function

    Public Function GetServices(serviceType As Type) As IEnumerable(Of Object) Implements IDependencyScope.GetServices
        Return Container.ResolveAll(serviceType).Cast(Of Object).ToArray
    End Function

    Private disposedValue As Boolean

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                If _scope IsNot Nothing Then
                    _scope.Dispose()
                    _scope = Nothing
                End If
            End If
        End If
        Me.disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    'Public Function GetScopeService(Of T)() As T Implements IScope.GetScopeService
    '    Return Container.Resolve(GetType(T))
    'End Function

End Class
