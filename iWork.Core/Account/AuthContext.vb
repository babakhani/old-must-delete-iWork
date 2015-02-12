Imports Microsoft.AspNet.Identity.EntityFramework

Public Class AuthContext
    Inherits IdentityDbContext(Of IdentityUser)

    Public Sub New()
        MyBase.New()
    End Sub


End Class
