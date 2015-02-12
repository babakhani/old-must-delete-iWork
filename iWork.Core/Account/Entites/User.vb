
Partial Public Class User

    Public Property Id As String
    Public Property Email As String
    Public Property EmailConfirmed As Boolean
    Public Property Password As String
    Public Property SecurityStamp As String
    Public Property PhoneNumber As String
    Public Property PhoneNumberConfirmed As Boolean
    Public Property TwoFactorEnabled As Boolean
    Public Property LockoutEndDateUtc As Nullable(Of Date)
    Public Property LockoutEnabled As Boolean
    Public Property AccessFailedCount As Integer
    Public Property UserName As String

    Public Overridable Property UserClaims As ICollection(Of UserClaim) = New HashSet(Of UserClaim)
    Public Overridable Property UserLogins As ICollection(Of UserLogin) = New HashSet(Of UserLogin)
    Public Overridable Property Roles As ICollection(Of Role) = New HashSet(Of Role)

End Class
