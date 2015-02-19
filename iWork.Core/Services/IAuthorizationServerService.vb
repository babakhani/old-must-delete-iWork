Imports Microsoft.Owin.Security.OAuth

Namespace Services


    Public Interface IAuthorizationServerService
        Inherits IService

        Function ValidateClientAuthentication(context As OAuthValidateClientAuthenticationContext) As Task
        Function GrantResourceOwnerCredentials(context As OAuthGrantResourceOwnerCredentialsContext) As Task
        Function GrantRefreshToken(context As OAuthGrantRefreshTokenContext) As Task
        Function TokenEndpoint(context As OAuthTokenEndpointContext) As Task

    End Interface

End Namespace