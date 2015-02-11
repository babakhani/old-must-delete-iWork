Imports System.Web.Http.Dependencies
Imports Castle.Windsor
Imports System.Collections.Concurrent

Public Class DependencyResolver
    Implements IDependencyResolver

    Private _toBeReleased As ConcurrentBag(Of Object)

    Private _container As IWindsorContainer

    Public Sub New(container As IWindsorContainer)

        _toBeReleased = New ConcurrentBag(Of Object)
        If container Is Nothing Then
            Throw New ArgumentNullException("container", "The instance of the container cannot be null.")
        End If
        _container = container

    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose

        If _toBeReleased IsNot Nothing Then
            For Each item In _toBeReleased
                _container.Release(item)
            Next
            _toBeReleased = Nothing
        End If

    End Sub

    Public Function GetService(serviceType As Type) As Object Implements IDependencyScope.GetService

        If Not _container.Kernel.HasComponent(serviceType) Then
            Return Nothing
        End If

        Dim resolved = _container.Resolve(serviceType)

        If resolved IsNot Nothing Then
            _toBeReleased.Add(resolved)
        End If

        Return resolved

    End Function

    Public Function GetServices(serviceType As Type) As IEnumerable(Of Object) Implements IDependencyScope.GetServices

        Dim allResolved = _container.ResolveAll(serviceType).Cast(Of Object).ToArray

        If allResolved IsNot Nothing Then
            allResolved.ToList.ForEach(Sub(x) _toBeReleased.Add(x))
        End If

        Return allResolved

    End Function


    Public Function BeginScope() As IDependencyScope Implements IDependencyResolver.BeginScope
        Return New DependencyScope(_container)
    End Function

End Class
