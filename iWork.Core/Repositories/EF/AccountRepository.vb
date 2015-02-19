Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity

Namespace Repositories.EF

   Public Class AuthRepository
        Implements IAccountRepository

        Private _ctx As AccountContext
        Private _userManager As UserManager(Of User, Integer)

        Public Sub New(context As AccountContext)
            _ctx = context
            Dim store As New UserStore(Of User, Role, Integer, UserLogin, UserRole, UserClaim)(_ctx)
            _userManager = New UserManager(Of User, Integer)(store)
        End Sub

        Public Async Function RegisterUser(username As String, password As String) As Task(Of IdentityResult) Implements IAccountRepository.RegisterUser
            Dim user As New User With {.UserName = username}
            Dim result = Await _userManager.CreateAsync(user, password)
            Return result
        End Function

        Public Async Function FindUser(userName As String, password As String) As Task(Of User) Implements IAccountRepository.FindUser
            Dim user As User = Await _userManager.FindAsync(userName, password)
            Return user
        End Function

        Public Async Function FindAsync(loginInfo As UserLoginInfo) As Task(Of User) Implements IAccountRepository.FindAsync
            Dim user As User = Await _userManager.FindAsync(loginInfo)

            Return user
        End Function

        Public Async Function CreateAsync(user As User) As Task(Of IdentityResult) Implements IAccountRepository.CreateAsync
            Dim result = Await _userManager.CreateAsync(user)

            Return result
        End Function

        Public Async Function AddLoginAsync(userId As String, login As UserLoginInfo) As Task(Of IdentityResult) Implements IAccountRepository.AddLoginAsync
            Dim result = Await _userManager.AddLoginAsync(userId, login)

            Return result
        End Function

    End Class

End Namespace


