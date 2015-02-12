Imports Microsoft.Owin.Security.Facebook
Imports System.Security.Claims

Public Class FacebookAuthProvider
    Inherits FacebookAuthenticationProvider

    Public Overrides Function Authenticated(context As FacebookAuthenticatedContext) As Task

        context.Identity.AddClaim(New Claim("ExternalAccessToken", context.AccessToken))
        Return Task.FromResult(Of Object)(Nothing)

    End Function

End Class
