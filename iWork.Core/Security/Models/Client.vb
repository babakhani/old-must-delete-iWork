Imports System.ComponentModel.DataAnnotations

Public Class Client
    <Key>
    Public Property Id As String
    Public Property Secret As String
    Public Property Name As String
    Public Property ApplicationType As Integer
    Public Property Active As Boolean
    Public Property RefreshTokenLifeTime As Integer
    Public Property AllowedOrigin As String

End Class
