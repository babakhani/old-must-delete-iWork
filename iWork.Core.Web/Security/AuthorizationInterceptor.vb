Imports Castle.DynamicProxy
Imports System.Security.Principal
Imports System.Threading

Public Class AuthorizationInterceptor
    Implements IInterceptor

    Public Sub Intercept(invocation As IInvocation) Implements IInterceptor.Intercept

        Dim methodKey As String = invocation.TargetType.Name & "." & invocation.Method.Name

        'If Not Attribute.IsDefined(invocation.Method, GetType(AuthorizeAttribute)) Then
        '    Throw New UnauthorizedAccessException("There has not been set any permission to method: " & methodKey)
        'End If

        'If Not Me.HasPermission(Thread.CurrentPrincipal, GetAuthorizationData(invocation)) Then
        '    Throw New UnauthorizedAccessException("Unauthorized request to: " & methodKey)
        'End If

        invocation.Proceed()

    End Sub

    Private Function HasPermission(principal As IPrincipal, authData As AuthorizationData) As Boolean


        If (authData.DenyUsers.Contains("?") OrElse authData.DenyRoles.Contains("?")) AndAlso Not principal.Identity.IsAuthenticated Then
            Return False
        End If

        If authData.AllowUsers.Contains("*") OrElse authData.AllowRoles.Contains("*") Then
            Return True
        End If

        For Each deniedUser In authData.DenyUsers
            If principal.Identity.Name = deniedUser Then
                Return False
            End If
        Next

        For Each deniedRole In authData.DenyRoles
            If principal.IsInRole(deniedRole) Then
                Return False
            End If
        Next

        For Each allowedUser In authData.AllowUsers
            If principal.Identity.Name = allowedUser Then
                Return True
            End If
        Next

        For Each allowedRole In authData.AllowRoles
            If principal.IsInRole(allowedRole) Then
                Return False
            End If
        Next

        Return False

    End Function

    Private Function GetAuthorizationData(invocation As IInvocation) As AuthorizationData

        Dim methodKey As String = invocation.Method.DeclaringType.Name & "." & invocation.Method.Name

        If Not _authorizationData.ContainsKey(methodKey) Then

            SyncLock locker

                If Not _authorizationData.ContainsKey(methodKey) Then

                    Dim attrib As AuthorizeAttribute = Attribute.GetCustomAttribute(invocation.Method, GetType(AuthorizeAttribute))
                    Dim authData As New AuthorizationData

                    authData.AllowRoles = attrib.AllowRoles.Split({","}, StringSplitOptions.RemoveEmptyEntries)
                    authData.AllowUsers = attrib.AllowUsers.Split({","}, StringSplitOptions.RemoveEmptyEntries)
                    authData.DenyRoles = attrib.DenyRoles.Split({","}, StringSplitOptions.RemoveEmptyEntries)
                    authData.DenyUsers = attrib.DenyUsers.Split({","}, StringSplitOptions.RemoveEmptyEntries)

                    _authorizationData.Item(methodKey) = authData

                End If

            End SyncLock

        End If

        Return _authorizationData.Item(methodKey)

    End Function

    Private Shared _authorizationData As Dictionary(Of String, AuthorizationData)
    Private Shared locker As New Object

    Private Class AuthorizationData
        Public Property AllowRoles As String()
        Public Property AllowUsers As String()
        Public Property DenyRoles As String()
        Public Property DenyUsers As String()
    End Class

End Class


