Imports Microsoft.Owin.Security.Facebook
Imports System.Security.Claims


Namespace Services

    Public Class FacebookAuthenticationService
        Inherits FacebookAuthenticationProvider
        Implements IService

        Public Overrides Function Authenticated(context As FacebookAuthenticatedContext) As Task

            context.Identity.AddClaim(New Claim("ExternalAccessToken", context.AccessToken))
            Return Task.FromResult(Of Object)(Nothing)

        End Function

    End Class


End Namespace
