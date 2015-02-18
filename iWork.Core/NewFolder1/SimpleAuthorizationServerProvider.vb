Imports Microsoft.Owin.Security.OAuth
Imports System.Security.Claims

Public Class SimpleAuthorizationServerProvider
    Inherits OAuthAuthorizationServerProvider
    Public Overrides Async Function ValidateClientAuthentication(context As OAuthValidateClientAuthenticationContext) As Task
        context.Validated()
    End Function

    Public Overrides Async Function GrantResourceOwnerCredentials(context As OAuthGrantResourceOwnerCredentialsContext) As Task

        context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", {"*"})

        Dim _repo = Application.GetService(Of IAuthRepository)()
        Dim user As User = Await _repo.FindUser(context.UserName, context.Password)

        If user Is Nothing Then
            context.SetError("invalid_grant", "The user name or password is incorrect.")
            Return
        End If


        Dim identity = New ClaimsIdentity(context.Options.AuthenticationType)
        identity.AddClaim(New Claim("sub", context.UserName))
        identity.AddClaim(New Claim("role", "user"))

        context.Validated(identity)

    End Function
End Class