Public Class ResponseModel

    Public Property Data As Object
    Public Property Message As String
    Public Property HasError As String
    Public Property [Error] As Object

    Public Shared Function SendOK(message As String, data As Object) As ResponseModel

        Dim out As New ResponseModel
        out.Data = data
        out.Error = Nothing
        out.HasError = False
        out.Message = message

        Return out

    End Function

    Public Shared Function SendError(errorMessage As String, _error As Object) As ResponseModel

        Dim out As New ResponseModel
        out.Data = Nothing
        out.Error = _error
        out.HasError = True
        out.Message = errorMessage

        Return out

    End Function

End Class

Public Class ResponseModel(Of T As {ResponseModel, New})
    Inherits ResponseModel

    Public Overloads Shared Function SendOK(message As String, data As Object) As T

        Dim out As New T
        out.Data = data
        out.Error = Nothing
        out.HasError = False
        out.Message = message

        Return out

    End Function

    Public Overloads Shared Function SendError(errorMessage As String, _error As Object) As T

        Dim out As New T
        out.Data = Nothing
        out.Error = _error
        out.HasError = True
        out.Message = errorMessage

        Return out

    End Function

End Class
