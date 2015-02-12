Partial Public Class Role
    Public Property Id As String
    Public Property Name As String

    Public Overridable Property Users As ICollection(Of User) = New HashSet(Of User)

End Class
