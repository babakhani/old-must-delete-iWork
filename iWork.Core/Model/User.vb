Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports iWork.Core.Repositories

Public Class User
    Inherits IdentityUser(Of Integer, UserLogin, UserRole, UserClaim)

End Class

Public Class UserLogin
    Inherits IdentityUserLogin(Of Integer)

End Class

Public Class Role
    Inherits IdentityRole(Of Integer, UserRole)

End Class

Public Class UserRole
    Inherits IdentityUserRole(Of Integer)
End Class

Public Class UserClaim
    Inherits IdentityUserClaim(Of Integer)

End Class
