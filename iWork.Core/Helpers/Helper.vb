Imports System.Security.Cryptography

Public Class Helper

    Public Shared Function GetHash(input As String) As String
        Dim hashAlgorithm As New SHA256CryptoServiceProvider()
        Dim byteValue = System.Text.Encoding.UTF8.GetBytes(input)
        Dim byteHash = hashAlgorithm.ComputeHash(byteValue)
        Return Convert.ToBase64String(byteHash)
    End Function


End Class