Imports Castle.Windsor
Imports Castle.Core.Resource
Imports Castle.MicroKernel.Registration
Imports System.Reflection
Imports System.IO

Public Class IoC

    Private Shared repositoryContainer As WindsorContainer
    Private Shared serviceContainer As WindsorContainer
    Private Shared locker As New Object

    Private Shared Sub InitRepositories()

        If repositoryContainer Is Nothing Then
            SyncLock locker
                If repositoryContainer Is Nothing Then

                    repositoryContainer = New WindsorContainer("Castle.config")
                    Dim assemblyFilter As New AssemblyFilter(AssemblyDirectory)
                    Dim repositoryClasses = Classes.FromAssemblyInDirectory(assemblyFilter).BasedOn(Of IRepository).WithService.FromInterface.LifestylePerWebRequest
                    Dim uowClasses = Classes.FromAssemblyInDirectory(assemblyFilter).BasedOn(Of IUnitOfWork).WithService.FromInterface.LifestylePerWebRequest
                    repositoryContainer.Register(uowClasses)
                    repositoryContainer.Register(repositoryClasses)

                End If
            End SyncLock
        End If

    End Sub

    Public Shared Function GetRepository(Of T As IRepository)() As T

        InitRepositories()
        Return repositoryContainer.Resolve(Of T)()

    End Function

    Public Shared Function GetUnitOfWork(Of T As IUnitOfWork)() As T

        InitRepositories()
        Return repositoryContainer.Resolve(Of T)()

    End Function

    Private Shared Sub InitServices()

        If serviceContainer Is Nothing Then
            SyncLock locker
                If serviceContainer Is Nothing Then

                    serviceContainer = New WindsorContainer("Castle.config")
                    Dim assemblyFilter As New AssemblyFilter(AssemblyDirectory)
                    Dim serviceClasses = Classes.FromAssemblyInDirectory(assemblyFilter).BasedOn(Of IService).WithService.FromInterface.LifestylePerWebRequest
                    serviceContainer.Register(serviceClasses)

                End If
            End SyncLock
        End If

    End Sub

    Public Shared Function GetService(Of T As IService)() As T

        InitServices()
        Return serviceContainer.Resolve(Of T)()

    End Function

    Private Shared ReadOnly Property AssemblyDirectory() As String
        Get
            Dim codeBase = Assembly.GetExecutingAssembly().CodeBase
            Dim uriBuilder = (New UriBuilder(codeBase))
            Dim path = Uri.UnescapeDataString(uriBuilder.Path)
            Return System.IO.Path.GetDirectoryName(path)
        End Get
    End Property


End Class
