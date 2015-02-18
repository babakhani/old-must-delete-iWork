Imports System.ComponentModel.DataAnnotations

Public Class Role

    <Key>
    Public Property Id As Integer

    Public Property Name As String

    Public Overridable Property Users As ICollection(Of User) = New HashSet(Of User)

End Class
