Imports System.Web.Http
Imports Microsoft.AspNet.Identity
Imports System.Net.Http
Imports System.Security.Claims
Imports Microsoft.Owin.Security
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Newtonsoft.Json.Linq
Imports Microsoft.Owin.Security.OAuth

Public Class AccountController
    Inherits ApiController

    Private _repo As AuthRepository
    Private ReadOnly Property Authentication As IAuthenticationManager
        Get
            Return Request.GetOwinContext().Authentication
        End Get
    End Property

    Public Sub New()
        _repo = New AuthRepository
    End Sub

    Public Async Function Register(userModel As User) As Task(Of IHttpActionResult)

        If Not ModelState.IsValid Then
            Return BadRequest(ModelState)
        End If

        Dim result As IdentityResult = Await _repo.RegisterUser(userModel)

        Dim errorResult As IHttpActionResult = GetErrorResult(result)

        If errorResult IsNot Nothing Then
            Return errorResult
        End If

        Return Ok()
    End Function

    Public Async Function GetExternalLogin(provider As String, Optional [error] As String = Nothing) As Task(Of IHttpActionResult)

        Dim redirectUri As String = String.Empty
        If [error] IsNot Nothing Then
            Return BadRequest(Uri.EscapeDataString([error]))
        End If

        If Not Me.User.Identity.IsAuthenticated Then
            Return New ChallengeResult(provider, Me)
        End If

        Dim redirectUriValidationResult = ValidateClientAndRedirectUri(Me.Request, redirectUri)

        If Not String.IsNullOrWhiteSpace(redirectUriValidationResult) Then
            Return BadRequest(redirectUriValidationResult)
        End If

        Dim externalLogin As ExternalLoginData = ExternalLoginData.FromIdentity(Me.User.Identity)
        If externalLogin Is Nothing Then
            Return InternalServerError()
        End If

        If externalLogin.LoginProvider IsNot provider Then
            Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie)
            Return New ChallengeResult(provider, Me)
        End If

        Dim user = Await _repo.FindAsync(New UserLoginInfo(externalLogin.LoginProvider, externalLogin.ProviderKey))

        Dim hasRegistered = (user IsNot Nothing)

        redirectUri = String.Format("{0}#external_access_token={1}&provider={2}&haslocalaccount={3}&external_user_name={4}",
                                        redirectUri,
                                        externalLogin.ExternalAccessToken,
                                        externalLogin.LoginProvider,
                                        hasRegistered.ToString(),
                                        externalLogin.UserName)

        Return Redirect(redirectUri)

    End Function


    Public Async Function RegisterExternal(model As RegisterExternalBindingModel) As Task(Of IHttpActionResult)

        If Not ModelState.IsValid Then
            Return BadRequest(ModelState)
        End If

        Dim verifiedAccessToken = Await VerifyExternalAccessToken(model.Provider, model.ExternalAccessToken)
        If verifiedAccessToken Is Nothing Then
            Return BadRequest("Invalid Provider or External Access Token")
        End If


        Dim user = Await _repo.FindAsync(New UserLoginInfo(model.Provider, verifiedAccessToken.user_id))

        Dim hasRegistered = (user IsNot Nothing)

        If hasRegistered Then
            Return BadRequest("External user is already registered")
        End If

        user = New IdentityUser With {.UserName = model.UserName}

        Dim result = Await _repo.CreateAsync(user)
        If Not result.Succeeded Then
            Return GetErrorResult(result)
        End If

        Dim info As New ExternalLoginInfo With
            {
                .DefaultUserName = model.UserName,
                .Login = New UserLoginInfo(model.Provider, verifiedAccessToken.user_id)
            }

        result = Await _repo.AddLoginAsync(user.Id, info.Login)
        If Not result.Succeeded Then
            Return GetErrorResult(result)
        End If

        Dim accessTokenResponse = GenerateLocalAccessTokenResponse(model.UserName)

        Return Ok(accessTokenResponse)

    End Function

    Public Async Function ObtainLocalAccessToken(provider As String, externalAccessToken As String) As Task(Of IHttpActionResult)

        If String.IsNullOrWhiteSpace(provider) OrElse String.IsNullOrWhiteSpace(externalAccessToken) Then
            Return BadRequest("Provider or external access token is not sent")
        End If
        Dim verifiedAccessToken = Await VerifyExternalAccessToken(provider, externalAccessToken)
        If verifiedAccessToken Is Nothing Then
            Return BadRequest("Invalid Provider or External Access Token")
        End If

        Dim user = Await _repo.FindAsync(New UserLoginInfo(provider, verifiedAccessToken.user_id))

        Dim hasRegistered = (user IsNot Nothing)


        If Not hasRegistered Then
            Return BadRequest("External user is not registered")
        End If

        Dim accessTokenResponse = GenerateLocalAccessTokenResponse(user.UserName)

        Return Ok(accessTokenResponse)

    End Function

    Private Function GenerateLocalAccessTokenResponse(userName As String) As JObject

        Dim tokenExpiration = TimeSpan.FromDays(1)

        Dim identity = New ClaimsIdentity(OAuthDefaults.AuthenticationType)

        identity.AddClaim(New Claim(ClaimTypes.Name, userName))
        identity.AddClaim(New Claim("role", "user"))

        Dim props As New AuthenticationProperties With
            {
                .IssuedUtc = DateTime.UtcNow,
                .ExpiresUtc = DateTime.UtcNow.Add(tokenExpiration)
            }

        Dim ticket = New AuthenticationTicket(identity, props)

        Dim accessToken = Startup.OAuthBearerOptions.AccessTokenFormat.Protect(ticket)

        Dim tokenResponse As New JObject(
                                    New JProperty("userName", userName),
                                    New JProperty("access_token", accessToken),
                                    New JProperty("token_type", "bearer"),
                                    New JProperty("expires_in", tokenExpiration.TotalSeconds.ToString()),
                                    New JProperty(".issued", ticket.Properties.IssuedUtc.ToString()),
                                    New JProperty(".expires", ticket.Properties.ExpiresUtc.ToString()))

        Return tokenResponse

    End Function

    Private Function GetErrorResult(result As IdentityResult) As IHttpActionResult

        If result Is Nothing Then
            Return InternalServerError()
        End If

        If Not result.Succeeded Then

            If result.Errors IsNot Nothing Then
                For Each [error] In result.Errors
                    ModelState.AddModelError("", [error])
                Next

            End If

            If ModelState.IsValid Then
                Return BadRequest()
            End If

            Return BadRequest(ModelState)

        End If

        Return Nothing


    End Function

    Private Function ValidateClientAndRedirectUri(request As HttpRequestMessage, ByRef redirectUriOutput As String) As String

        Dim redirectUri As Uri

        Dim redirectUriString = GetQueryString(request, "redirect_uri")

        If String.IsNullOrWhiteSpace(redirectUriString) Then
            Return "redirect_uri is required"
        End If

        Dim validUri = Uri.TryCreate(redirectUriString, UriKind.Absolute, redirectUri)

        If Not validUri Then
            Return "redirect_uri is invalid"
        End If

        Dim clientId = GetQueryString(request, "client_id")

        If String.IsNullOrWhiteSpace(clientId) Then
            Return "client_Id is required"
        End If

        Dim client = _repo.FindClient(clientId)


        If client Is Nothing Then
            Return String.Format("Client_id '{0}' is not registered in the system.", clientId)
        End If

        If Not String.Equals(client.AllowedOrigin, redirectUri.GetLeftPart(UriPartial.Authority), StringComparison.OrdinalIgnoreCase) Then
            Return String.Format("The given URL is not allowed by Client_id '{0}' configuration.", clientId)
        End If

        redirectUriOutput = redirectUri.AbsoluteUri

        Return String.Empty

    End Function


    Private Function GetQueryString(request As HttpRequestMessage, key As String) As String

        Dim queryStrings = request.GetQueryNameValuePairs()

        If queryStrings Is Nothing Then
            Return Nothing
        End If


        Dim match = queryStrings.FirstOrDefault(Function(keyValue) String.Compare(keyValue.Key, key, True) = 0)

        If String.IsNullOrEmpty(match.Value) Then
            Return Nothing
        End If

        Return match.Value

    End Function

    Private Class ExternalLoginData

        Public LoginProvider As String
        Public ProviderKey As String
        Public UserName As String
        Public ExternalAccessToken As String

        Public Shared Function FromIdentity(identity As ClaimsIdentity) As ExternalLoginData

            If identity Is Nothing Then
                Return Nothing
            End If

            Dim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier)
            If providerKeyClaim Is Nothing OrElse String.IsNullOrEmpty(providerKeyClaim.Issuer) OrElse String.IsNullOrEmpty(providerKeyClaim.Value) Then
                Return Nothing
            End If

            If providerKeyClaim.Issuer = ClaimsIdentity.DefaultIssuer Then
                Return Nothing
            End If

            Return New ExternalLoginData With
                {
                    .LoginProvider = providerKeyClaim.Issuer,
                    .ProviderKey = providerKeyClaim.Value,
                    .UserName = identity.FindFirstValue(ClaimTypes.Name),
                    .ExternalAccessToken = identity.FindFirstValue("ExternalAccessToken")
                }

        End Function


    End Class

    Private Async Function VerifyExternalAccessToken(provider As String, accessToken As String) As Task(Of ParsedExternalAccessToken)

        Dim parsedToken As ParsedExternalAccessToken = Nothing
        Dim verifyTokenEndPoint = ""

        Select Case provider

            Case "Facebook"
                Dim appToken = "xxxxxx"
                verifyTokenEndPoint = String.Format("https://graph.facebook.com/debug_token?input_token={0}&access_token={1}", accessToken, appToken)

            Case "Google"
                verifyTokenEndPoint = String.Format("https://www.googleapis.com/oauth2/v1/tokeninfo?access_token={0}", accessToken)

            Case Else
                Return Nothing

        End Select

        Dim client = New HttpClient()
        Dim uri = New Uri(verifyTokenEndPoint)
        Dim response = Await client.GetAsync(uri)

        If response.IsSuccessStatusCode Then
            Dim content = Await response.Content.ReadAsStringAsync()

            Dim jObj = Newtonsoft.Json.JsonConvert.DeserializeObject(content)

            parsedToken = New ParsedExternalAccessToken()

            Select Case provider
                Case "Facebook"
                    parsedToken.user_id = jObj("data")("user_id")
                    parsedToken.app_id = jObj("data")("app_id")
                    If Not String.Equals(Startup.facebookAuthOptions.AppId, parsedToken.app_id, StringComparison.OrdinalIgnoreCase) Then
                        Return Nothing
                    End If

                Case "Google"
                    parsedToken.user_id = jObj("user_id")
                    parsedToken.app_id = jObj("audience")

                    If Not String.Equals(Startup.googleAuthOptions.ClientId, parsedToken.app_id, StringComparison.OrdinalIgnoreCase) Then
                        Return Nothing
                    End If

            End Select

        End If

        Return parsedToken

    End Function


End Class

Public Class ExternalLoginViewModel
    Public Property Name As String
    Public Property Url As String
    Public Property State As String
End Class

Public Class RegisterExternalBindingModel
    Public Property UserName As String
    Public Property Provider As String
    Public Property ExternalAccessToken As String
End Class

Public Class ParsedExternalAccessToken
    Public Property user_id As String
    Public Property app_id As String
End Class
