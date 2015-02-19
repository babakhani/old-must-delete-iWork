Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework

Namespace Repositories

    Public Interface IAccountRepository
        Inherits IRepository
        Function RegisterUser(username As String, password As String) As Task(Of IdentityResult)
        Function FindUser(userName As String, password As String) As Task(Of User)
        Function FindAsync(loginInfo As UserLoginInfo) As Task(Of User)
        Function CreateAsync(user As User) As Task(Of IdentityResult)
        Function AddLoginAsync(userId As String, login As UserLoginInfo) As Task(Of IdentityResult)
    End Interface

End Namespace
