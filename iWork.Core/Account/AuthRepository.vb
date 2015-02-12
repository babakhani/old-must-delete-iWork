Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity

Public Class AuthRepository
    Implements IDisposable

    Private _ctx As AuthContext
    Private _userManager As UserManager(Of IdentityUser)

    Public Sub New()
        _ctx = New AuthContext()
        _userManager = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(_ctx))
    End Sub

    Public Async Function RegisterUser(userModel As User) As Task(Of IdentityResult)

        Dim user As New IdentityUser
        user.UserName = userModel.UserName
        Dim result = Await _userManager.CreateAsync(user, userModel.Password)
        Return result

    End Function


    Public Async Function FindUser(userName As String, password As String) As Task(Of IdentityUser)
        Dim user As IdentityUser = Await _userManager.FindAsync(userName, password)
        Return user
    End Function

    Public Function FindClient(clientId As String) As Client
        Dim client = _ctx.Set(Of Client).Find(clientId)
        Return client
    End Function


    Public Async Function AddRefreshToken(token As RefreshToken) As Task(Of Boolean)

        Dim existingToken = _ctx.Set(Of RefreshToken).Where(Function(r) r.Subject = token.Subject AndAlso r.ClientId = token.ClientId).SingleOrDefault()

        If existingToken IsNot Nothing Then
            Dim result = Await RemoveRefreshToken(existingToken)
        End If

        _ctx.Set(Of RefreshToken).Add(token)
        Return Await _ctx.SaveChangesAsync() > 0

    End Function


    Public Async Function RemoveRefreshToken(refreshTokenId As String) As Task(Of Boolean)
        Dim refreshToken = Await _ctx.Set(Of RefreshToken).FindAsync(refreshTokenId)

        If refreshToken IsNot Nothing Then
            _ctx.Set(Of RefreshToken).Remove(refreshToken)
            Return Await _ctx.SaveChangesAsync() > 0
        End If

        Return False

    End Function


    Public Async Function RemoveRefreshToken(refreshToken As RefreshToken) As Task(Of Boolean)
        _ctx.Set(Of RefreshToken).Remove(refreshToken)
        Return Await _ctx.SaveChangesAsync() > 0
    End Function

    Public Async Function FindRefreshToken(refreshTokenId As String) As Task(Of RefreshToken)
        Dim refreshToken = Await _ctx.Set(Of RefreshToken).FindAsync(refreshTokenId)
        Return refreshToken
    End Function

    Public Function GetAllRefreshTokens() As List(Of RefreshToken)
        Return _ctx.Set(Of RefreshToken).ToList()
    End Function

    Public Async Function FindAsync(loginInfo As UserLoginInfo) As Task(Of IdentityUser)
        Dim user As IdentityUser = Await _userManager.FindAsync(loginInfo)
        Return user
    End Function


    Public Async Function CreateAsync(user As IdentityUser) As Task(Of IdentityResult)
        Dim result = Await _userManager.CreateAsync(user)
        Return result
    End Function

    Public Async Function AddLoginAsync(userId As String, login As UserLoginInfo) As Task(Of IdentityResult)
        Dim result = Await _userManager.AddLoginAsync(userId, login)
        Return result
    End Function

    Public Sub Dispose() Implements IDisposable.Dispose
        _ctx.Dispose()
        _userManager.Dispose()
    End Sub

End Class
