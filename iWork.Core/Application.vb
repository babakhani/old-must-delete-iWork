Public Class Application

    Private Shared _func As Func(Of IServiceContainer)
    Public Shared Sub InitializeContainer(func As Func(Of IServiceContainer))
        _func = func
    End Sub

    Public Shared Function GetService(Of T)() As T
        Return _func.Invoke.GetService(Of T)()
    End Function

End Class