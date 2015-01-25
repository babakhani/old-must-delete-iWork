Public Class ServiceFactory

    Private Shared ifFirstTime As Boolean = True
    Private Shared locker As New Object

    Private Shared Sub Init()

        If ifFirstTime Then
            SyncLock locker
                If ifFirstTime Then


                    ifFirstTime = False
                End If
            End SyncLock
        End If

    End Sub

    Public Shared Function GetInstance(Of T)() As T

        Return Activator.CreateInstance(Of T)()

        Init()

    End Function

End Class
