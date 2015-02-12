Imports Microsoft.Owin.Security.Google
Imports System.Security.Claims

Public Class GoogleAuthProvider
    Implements IGoogleOAuth2AuthenticationProvider

    Public Sub ApplyRedirect(context As GoogleOAuth2ApplyRedirectContext) Implements IGoogleOAuth2AuthenticationProvider.ApplyRedirect
        context.Response.Redirect(context.RedirectUri)
    End Sub

    Public Function Authenticated(context As GoogleOAuth2AuthenticatedContext) As Task Implements IGoogleOAuth2AuthenticationProvider.Authenticated
        context.Identity.AddClaim(New Claim("ExternalAccessToken", context.AccessToken))
        Return Task.FromResult(Of Object)(Nothing)
    End Function

    Public Function ReturnEndpoint(context As GoogleOAuth2ReturnEndpointContext) As Task Implements IGoogleOAuth2AuthenticationProvider.ReturnEndpoint
        Return Task.FromResult(Of Object)(Nothing)
    End Function

End Class
