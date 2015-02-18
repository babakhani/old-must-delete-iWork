Imports System.ComponentModel.DataAnnotations

Public Class RefreshToken

    <Key>
    Public Property Id As String
    Public Property Subject As String
    Public Property ClientId As String
    Public Property IssuedUtc As Date
    Public Property ExpiresUtc As Date
    Public Property ProtectedTicket As String

End Class
