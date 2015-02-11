Imports Castle.Windsor

Public Class ServiceContainer
    Implements IServiceContainer

    Private _container As IWindsorContainer
    Public Sub New(container As IWindsorContainer)
        _container = container
    End Sub

    Public Function GetService(Of T)() As T Implements IServiceContainer.GetService

        If Not _container.Kernel.HasComponent(GetType(T)) Then
            Debug.WriteLine("----------------------------------------------------")
            Debug.WriteLine(GetType(T).ToString)
            Debug.WriteLine("----------------------------------------------------")
            For Each handler In _container.Kernel.GetAssignableHandlers(GetType(Object))
                For Each ser In handler.ComponentModel.Services
                    Debug.WriteLine(ser.Name & "," & handler.ComponentModel.Implementation.Name)
                Next
            Next
            Debug.WriteLine("----------------------------------------------------")

        End If

        Dim out = _container.Resolve(Of T)()




        Return out
    End Function

End Class