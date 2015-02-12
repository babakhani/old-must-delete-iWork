Imports System.Web.Http
Imports System.Net.Http
Imports System.Net

Public Class ChallengeResult
    Implements IHttpActionResult
    Public Property LoginProvider As String
    Public Property Request As HttpRequestMessage

    Public Sub New(loginProvider As String, controller As ApiController)
        Me.LoginProvider = loginProvider
        Me.Request = controller.Request
    End Sub

    Public Function ExecuteAsync(cancellationToken As Threading.CancellationToken) As Task(Of Net.Http.HttpResponseMessage) Implements IHttpActionResult.ExecuteAsync

        Request.GetOwinContext().Authentication.Challenge(LoginProvider)

        Dim response As New HttpResponseMessage(HttpStatusCode.Unauthorized)
        response.RequestMessage = Request
        Return Task.FromResult(response)

    End Function

End Class
