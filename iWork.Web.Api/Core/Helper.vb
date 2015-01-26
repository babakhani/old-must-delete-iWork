Imports iWork.Service

Public Class Helper

    Public Shared Function GetService(Of T As IService)() As T
        Return ServiceFactory.GetInstance(Of T)()
    End Function

End Class
