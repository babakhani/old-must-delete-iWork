Imports Castle.Windsor
Imports System.Web.Http
Imports Castle.MicroKernel.Registration

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

                    Dim container = New WindsorContainer("config/Castle.config")
                    GlobalConfiguration.Configuration.DependencyResolver = New DependencyResolver(container)
                    container.Install(New UnitOfWorkInstaller)
                    container.Install(New RepositoryInstaller)
                    container.Install(New ServiceInstaller)
                    container.Install(New ControllerInstaller)
                    container.Install(New ApplicationInstaller)

                    ApplicationConfuration.Install(Function() GlobalConfiguration.Configuration.DependencyResolver.BeginScope)

                    Debug.WriteLine("----------------------------------------------------")
                    For Each x In container.Kernel.GetAssignableHandlers(GetType(Object))
                        For Each ser In x.ComponentModel.Services
                            Debug.WriteLine(ser.Name & "," & x.ComponentModel.Implementation.Name)
                        Next
                    Next
                    Debug.WriteLine("----------------------------------------------------")


                End If

                isFirstTime = False
            End SyncLock
        End If

    End Sub

End Class
