Public Class AuthorizeAttribute
    Inherits Attribute

    Public Property AllowRoles As String
    Public Property AllowUsers As String
    Public Property DenyRoles As String
    Public Property DenyUsers As String

    Public Sub New(Optional allowRoles As String = "", Optional allowUsers As String = "", Optional denyRoles As String = "", Optional denyUsers As String = "")
        Me.AllowRoles = allowRoles
        Me.AllowUsers = allowUsers
        Me.DenyRoles = denyRoles
        Me.DenyUsers = denyUsers
    End Sub


End Class
