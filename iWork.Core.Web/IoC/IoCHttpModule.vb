Imports Castle.Windsor
Imports System.Web.Http

Public Class IoCHttpModule
    Implements System.Web.IHttpModule

    Public Sub Dispose() Implements System.Web.IHttpModule.Dispose

    End Sub

    Private Shared locker As New Object
    Private Shared isFirstTime As Boolean = True


    Public Sub Init(context As System.Web.HttpApplication) Implements System.Web.IHttpModule.Init

        If isFirstTime Then
            SyncLock locker

                If isFirstTime Then

                    Dim container As New WindsorContainer("Castle.config")
                    GlobalConfiguration.Configuration.DependencyResolver = New CastleDependencyResolver(container)
                    container.Install(New ControllerInstaller)

                    Dim applicationInstaller As New Application
                    applicationInstaller.Install()

                End If

                isFirstTime = False
            End SyncLock
        End If

    End Sub

End Class
