'Imports Microsoft.Owin.Security.OAuth
'Imports Microsoft.AspNet.Identity.EntityFramework
'Imports System.Security.Claims
'Imports Microsoft.Owin.Security
'Imports iWork.Core.Repositories

'Namespace Services

'    Public Class AuthorizationServerService
'        Inherits OAuthAuthorizationServerProvider
'        Implements IAuthorizationServerService

'        Public Overrides Function ValidateClientAuthentication(context As OAuthValidateClientAuthenticationContext) As Task Implements IAuthorizationServerService.ValidateClientAuthentication

'            Dim clientId As String = String.Empty
'            Dim clientSecret As String = String.Empty
'            Dim client As Client = Nothing

'            If Not context.TryGetBasicCredentials(clientId, clientSecret) Then
'                context.TryGetFormCredentials(clientId, clientSecret)
'            End If

'            If context.ClientId Is Nothing Then
'                context.Validated()
'                Return Task.FromResult(Of Object)(Nothing)
'            End If

'            client = Nothing ' Application.GetService(Of IAuthenticationRepository).FindClient(context.ClientId)

'            If client Is Nothing Then
'                context.SetError("invalid_clientId", String.Format("Client '{0}' is not registered in the system.", context.ClientId))
'                Return Task.FromResult(Of Object)(Nothing)
'            End If


'            If client.ApplicationType = ApplicationTypes.NativeConfidential Then
'                If (String.IsNullOrWhiteSpace(clientSecret)) Then
'                    context.SetError("invalid_clientId", "Client secret should be sent.")
'                    Return Task.FromResult(Of Object)(Nothing)
'                Else

'                    If (client.Secret IsNot Helper.GetHash(clientSecret)) Then
'                        context.SetError("invalid_clientId", "Client secret is invalid.")
'                        Return Task.FromResult(Of Object)(Nothing)
'                    End If

'                End If
'            End If


'            If Not client.Active Then
'                context.SetError("invalid_clientId", "Client is inactive.")
'                Return Task.FromResult(Of Object)(Nothing)
'            End If


'            context.OwinContext.Set(Of String)("as:clientAllowedOrigin", client.AllowedOrigin)
'            context.OwinContext.Set(Of String)("as:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime.ToString())

'            context.Validated()
'            Return Task.FromResult(Of Object)(Nothing)

'        End Function

'        Public Overrides Function GrantResourceOwnerCredentials(context As OAuthGrantResourceOwnerCredentialsContext) As Task Implements IAuthorizationServerService.GrantResourceOwnerCredentials

'            Dim allowedOrigin = context.OwinContext.Get(Of String)("as:clientAllowedOrigin")

'            If allowedOrigin Is Nothing Then
'                allowedOrigin = "*"
'            End If

'            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", {allowedOrigin})

'            Dim user = Application.GetService(Of IAccountService).Find(context.UserName, context.Password)
'            If user Is Nothing Then
'                context.SetError("invalid_grant", "The user name or password is incorrect.")
'                Return Task.FromResult(Of Object)(Nothing)
'            End If

'            Dim identity As New ClaimsIdentity(context.Options.AuthenticationType)
'            identity.AddClaim(New Claim(ClaimTypes.Name, context.UserName))
'            identity.AddClaim(New Claim(ClaimTypes.Role, "user"))
'            identity.AddClaim(New Claim("sub", context.UserName))

'            Dim dic As New Dictionary(Of String, String)
'            dic.Add("userName", context.UserName)
'            If context.ClientId Is Nothing Then
'                dic.Add("as:client_id", String.Empty)
'            Else
'                dic.Add("as:client_id", context.ClientId)
'            End If

'            Dim props As New AuthenticationProperties(dic)
'            Dim ticket As New AuthenticationTicket(identity, props)

'            context.Validated(ticket)
'            Return Task.FromResult(Of Object)(Nothing)

'        End Function

'        Public Overrides Function GrantRefreshToken(context As OAuthGrantRefreshTokenContext) As Task Implements IAuthorizationServerService.GrantRefreshToken

'            Dim originalClient = context.Ticket.Properties.Dictionary("as:client_id")
'            Dim currentClient = context.ClientId

'            If originalClient IsNot currentClient Then
'                context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.")
'                Return Task.FromResult(Of Object)(Nothing)
'            End If

'            Dim newIdentity As New ClaimsIdentity(context.Ticket.Identity)

'            Dim newClaim = newIdentity.Claims.Where(Function(c) c.Type = "newClaim").FirstOrDefault()
'            If newClaim IsNot Nothing Then
'                newIdentity.RemoveClaim(newClaim)
'            End If
'            newIdentity.AddClaim(New Claim("newClaim", "newValue"))

'            Dim newTicket = New AuthenticationTicket(newIdentity, context.Ticket.Properties)
'            context.Validated(newTicket)

'            Return Task.FromResult(Of Object)(Nothing)

'        End Function

'        Public Overrides Function TokenEndpoint(context As OAuthTokenEndpointContext) As Task Implements IAuthorizationServerService.TokenEndpoint

'            For Each prop In context.Properties.Dictionary
'                context.AdditionalResponseParameters.Add(prop.Key, prop.Value)
'            Next
'            Return Task.FromResult(Of Object)(Nothing)

'        End Function

'    End Class


'End Namespace

