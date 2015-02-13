Imports System.ComponentModel.DataAnnotations

Public Class Contact

    <Key>
    Public Property Id As Integer
    Public Property Fullname As String
    Public Property Gender As Byte
    Public Property Tel1 As String
    Public Property Tel2 As String
    Public Property Tel3 As String
    Public Property Tel4 As String
    Public Property Company As String
    Public Property Unit As String
    Public Property Description As String

End Class