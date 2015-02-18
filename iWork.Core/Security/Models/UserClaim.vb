Imports System.ComponentModel.DataAnnotations

Public Class UserClaim

    <Key>
    Public Property Id As Integer
    Public Property UserId As Integer
    Public Property ClaimType As String
    Public Property ClaimValue As String

    Public Overridable Property User As User

End Class
