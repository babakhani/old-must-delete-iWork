Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework

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


Public Class AccountContext
    Inherits EntityFramework.IdentityDbContext(Of User, Role, Integer, UserLogin, UserRole, UserClaim)

    Public Sub New(nameOrConnectionString As String)
        MyBase.New(nameOrConnectionString)
        MyBase.Configuration.LazyLoadingEnabled = False
        MyBase.Configuration.ProxyCreationEnabled = False
    End Sub
    Protected Overrides Sub OnModelCreating(modelBuilder As Entity.DbModelBuilder)

        MyBase.OnModelCreating(modelBuilder)

        modelBuilder.Entity(Of User)().ToTable("Users")
        modelBuilder.Entity(Of Role)().ToTable("Roles")
        modelBuilder.Entity(Of UserRole)().ToTable("UserRoles")
        modelBuilder.Entity(Of UserLogin)().ToTable("UserLogins")
        modelBuilder.Entity(Of UserClaim)().ToTable("UserClaims")

    End Sub

End Class

Public Interface IAuthRepository
    Inherits Repositories.IRepository
    Function RegisterUser(username As String, password As String) As Task(Of IdentityResult)
    Function FindUser(userName As String, password As String) As Task(Of User)
    Function FindAsync(loginInfo As UserLoginInfo) As Task(Of User)
    Function CreateAsync(user As User) As Task(Of IdentityResult)
    Function AddLoginAsync(userId As String, login As UserLoginInfo) As Task(Of IdentityResult)
End Interface

Public Class AuthRepository
    Implements IAuthRepository
    Private _ctx As AccountContext

    Private _userManager As UserManager(Of User, Integer)

    Public Sub New(context As AccountContext)
        _ctx = context
        Dim store As New UserStore(Of User, Role, Integer, UserLogin, UserRole, UserClaim)(_ctx)
        _userManager = New UserManager(Of User, Integer)(store)
    End Sub

    Public Async Function RegisterUser(username As String, password As String) As Task(Of IdentityResult) Implements IAuthRepository.RegisterUser
        Dim user As New User With {.UserName = username}
        Dim result = Await _userManager.CreateAsync(user, password)
        Return result
    End Function

    Public Async Function FindUser(userName As String, password As String) As Task(Of User) Implements IAuthRepository.FindUser
        Dim user As User = Await _userManager.FindAsync(userName, password)
        Return user
    End Function

    Public Async Function FindAsync(loginInfo As UserLoginInfo) As Task(Of User) Implements IAuthRepository.FindAsync
        Dim user As User = Await _userManager.FindAsync(loginInfo)

        Return user
    End Function

    Public Async Function CreateAsync(user As User) As Task(Of IdentityResult) Implements IAuthRepository.CreateAsync
        Dim result = Await _userManager.CreateAsync(user)

        Return result
    End Function

    Public Async Function AddLoginAsync(userId As String, login As UserLoginInfo) As Task(Of IdentityResult) Implements IAuthRepository.AddLoginAsync
        Dim result = Await _userManager.AddLoginAsync(userId, login)

        Return result
    End Function


End Class
