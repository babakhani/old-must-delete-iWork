Imports Microsoft.Owin.Security.Infrastructure

Public Class SimpleRefreshTokenProvider
    Implements IAuthenticationTokenProvider

    Public Async Function CreateAsync(context As AuthenticationTokenCreateContext) As Task Implements IAuthenticationTokenProvider.CreateAsync

        Dim clientid = context.Ticket.Properties.Dictionary("as:client_id")

        If String.IsNullOrEmpty(clientid) Then
            Exit Function
        End If

        Dim refreshTokenId = Guid.NewGuid().ToString("n")

        Using _repo As New AuthRepository()

            Dim refreshTokenLifeTime = context.OwinContext.Get(Of String)("as:clientRefreshTokenLifeTime")
            Dim token As New RefreshToken()
            With token
                .Id = Helper.GetHash(refreshTokenId)
                .ClientId = clientid
                .Subject = context.Ticket.Identity.Name
                .IssuedUtc = DateTime.UtcNow
                .ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
            End With

            context.Ticket.Properties.IssuedUtc = token.IssuedUtc
            context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc

            token.ProtectedTicket = context.SerializeTicket()

            Dim result = Await _repo.AddRefreshToken(token)

            If result Then
                context.SetToken(refreshTokenId)
            End If

        End Using

    End Function

    Public Async Function ReceiveAsync(context As AuthenticationTokenReceiveContext) As Task Implements IAuthenticationTokenProvider.ReceiveAsync

        Dim allowedOrigin = context.OwinContext.Get(Of String)("as:clientAllowedOrigin")

        context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", {allowedOrigin})

        Dim hashedTokenId As String = Helper.GetHash(context.Token)

        Using _repo As New AuthRepository()

            Dim refreshToken = Await _repo.FindRefreshToken(hashedTokenId)

            If refreshToken IsNot Nothing Then
                context.DeserializeTicket(refreshToken.ProtectedTicket)
                Dim result = Await _repo.RemoveRefreshToken(hashedTokenId)
            End If

        End Using

    End Function

    Public Sub Create(context As AuthenticationTokenCreateContext) Implements IAuthenticationTokenProvider.Create
        Throw New NotImplementedException()
    End Sub

    Public Sub Receive(context As AuthenticationTokenReceiveContext) Implements IAuthenticationTokenProvider.Receive
        Throw New NotImplementedException()
    End Sub

End Class


