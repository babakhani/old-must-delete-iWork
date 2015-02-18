'Imports Microsoft.AspNet.Identity.EntityFramework
'Imports Microsoft.AspNet.Identity

'Namespace Repositories.EF

'    Public Class AccountRepository
'        Inherits GenericRepository(Of User)
'        Implements IAccountRepository

'        Public Sub New(dbContext As AccountContext)
'            MyBase.New(dbContext)
'        End Sub

'    End Class

'    'Public Class AuthenticationRepository
'    '    Implements IRepository
'    '    Implements IAuthenticationRepository

'    '    Private context As AuthenticationContext
'    '    Private _userManager As UserManager(Of IdentityUser)

'    '    Public Sub New(dbContext As AuthenticationContext)
'    '        context = dbContext
'    '        '_userManager = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(context))
'    '    End Sub

'    '    Public Function RegisterUser(userModel As User) As Boolean Implements IAuthenticationRepository.RegisterUser

'    '        Dim user As New IdentityUser
'    '        user.UserName = userModel.UserName
'    '        Return _userManager.CreateAsync(user, userModel.Password).Result.Succeeded

'    '    End Function

'    '    Public Function FindUser(userName As String, password As String) As User Implements IAuthenticationRepository.FindUser
'    '        Return (From p In context.Users Where p.Password = password And p.UserName.ToLower = userName.ToLower).SingleOrDefault
'    '    End Function

'    '    Public Function FindClient(clientId As String) As Client Implements IAuthenticationRepository.FindClient
'    '        Dim client = context.Set(Of Client).Find(clientId)
'    '        Return client
'    '    End Function

'    '    Public Async Function AddRefreshToken(token As RefreshToken) As Task(Of Boolean) Implements IAuthenticationRepository.AddRefreshToken

'    '        Dim existingToken = context.Set(Of RefreshToken).Where(Function(r) r.Subject = token.Subject AndAlso r.ClientId = token.ClientId).SingleOrDefault()

'    '        If existingToken IsNot Nothing Then
'    '            Dim result = Await RemoveRefreshToken(existingToken)
'    '        End If

'    '        context.Set(Of RefreshToken).Add(token)
'    '        Return Await context.SaveChangesAsync() > 0

'    '    End Function

'    '    Public Async Function RemoveRefreshToken(refreshTokenId As String) As Task(Of Boolean) Implements IAuthenticationRepository.RemoveRefreshToken
'    '        Dim refreshToken = Await context.Set(Of RefreshToken).FindAsync(refreshTokenId)

'    '        If refreshToken IsNot Nothing Then
'    '            context.Set(Of RefreshToken).Remove(refreshToken)
'    '            Return Await context.SaveChangesAsync() > 0
'    '        End If

'    '        Return False

'    '    End Function

'    '    Public Async Function RemoveRefreshToken(refreshToken As RefreshToken) As Task(Of Boolean) Implements IAuthenticationRepository.RemoveRefreshToken
'    '        context.Set(Of RefreshToken).Remove(refreshToken)
'    '        Return Await context.SaveChangesAsync() > 0
'    '    End Function

'    '    Public Async Function FindRefreshToken(refreshTokenId As String) As Task(Of RefreshToken) Implements IAuthenticationRepository.FindRefreshToken
'    '        Dim refreshToken = Await context.Set(Of RefreshToken).FindAsync(refreshTokenId)
'    '        Return refreshToken
'    '    End Function

'    '    Public Function GetAllRefreshTokens() As List(Of RefreshToken) Implements IAuthenticationRepository.GetAllRefreshTokens
'    '        Return context.Set(Of RefreshToken).ToList()
'    '    End Function

'    '    Public Async Function FindAsync(loginInfo As UserLoginInfo) As Task(Of IdentityUser) Implements IAuthenticationRepository.FindAsync
'    '        Dim user As IdentityUser = Await _userManager.FindAsync(loginInfo)
'    '        Return user
'    '    End Function

'    '    Public Async Function CreateAsync(user As IdentityUser) As Task(Of IdentityResult) Implements IAuthenticationRepository.CreateAsync
'    '        Dim result = Await _userManager.CreateAsync(user)
'    '        Return result
'    '    End Function

'    '    Public Function AddLogin(user As User) As Boolean Implements IAuthenticationRepository.AddLogin



'    '    End Function


'    'End Class

'End Namespace


