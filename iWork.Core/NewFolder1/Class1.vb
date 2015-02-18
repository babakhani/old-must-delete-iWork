Imports System.Web.Http
Imports System.Net.Http
Imports System.Threading
Imports System.Net

Public Class ChallengeResult
    Implements IHttpActionResult
    Public Property LoginProvider() As String

    Public Property Request() As HttpRequestMessage

    Public Sub New(loginProvider__1 As String, controller As ApiController)
        LoginProvider = loginProvider__1
        Request = controller.Request
    End Sub

    Public Function ExecuteAsync(cancellationToken As CancellationToken) As Task(Of HttpResponseMessage) Implements IHttpActionResult.ExecuteAsync
        Request.GetOwinContext().Authentication.Challenge(LoginProvider)

        Dim response As New HttpResponseMessage(HttpStatusCode.Unauthorized)
        response.RequestMessage = Request
        Return Task.FromResult(response)
    End Function
End Class
