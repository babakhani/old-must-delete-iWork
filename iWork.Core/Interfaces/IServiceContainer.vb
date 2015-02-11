Public Interface IServiceContainer

    Function GetService(Of T)() As T

End Interface