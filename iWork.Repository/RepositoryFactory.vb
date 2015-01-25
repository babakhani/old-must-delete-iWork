Imports StructureMap
Imports StructureMap.Configuration.DSL
Imports StructureMap.Graph.AssemblyScannerExtensions
Imports StructureMap.Pipeline
Imports System.Data.Entity

Public Class RepositoryFactory

    Private Shared StructureMapContainer As Container
    Private Shared locker As New Object

    Private Shared Sub Init()

        If StructureMapContainer Is Nothing Then
            SyncLock locker
                If StructureMapContainer Is Nothing Then

                    StructureMapContainer = New Container(Sub(init)

                                                              'init.Scan(Sub(scanner)
                                                              '              scanner.TheCallingAssembly()
                                                              '              scanner.AddAllTypesOf(Of Entity.DbContext)()
                                                              '              scanner.AssembliesFromApplicationBaseDirectory()
                                                              '              scanner.WithDefaultConventions()
                                                              '          End Sub)

                                                              'Dim unique = New HttpContextLifeCyle()


                                                              init.For(Of DbContext).LifecycleIs(Of HttpContextLifeCyle).Use(Of iWorkEntities)()
                                                              init.For(Of IUnitOfWork).Use(Of EF.UnitOfWork)()
                                                              init.For(GetType(IGenericRepository(Of ,))).Use(GetType(EF.GenericRepository(Of ,)))
                                                              init.For(GetType(IGenericRepository(Of ))).Use(GetType(EF.GenericRepository(Of )))

                                                          End Sub)

                    'StructureMapContainer.AssertConfigurationIsValid()

                End If
            End SyncLock
        End If

    End Sub


    Public Shared Function GetInstance(Of T)() As T

        Init()
        Return StructureMapContainer.GetInstance(Of T)()

    End Function

End Class

Public Class HttpContextLifeCyle
    Inherits UniquePerRequestLifecycle

    Public Sub New()
        MyBase.New()
    End Sub

    Public Overrides Function FindCache(context As ILifecycleContext) As IObjectCache
        Return MyBase.FindCache(context)
    End Function

    Public Overrides Sub EjectAll(context As ILifecycleContext)
        MyBase.EjectAll(context)
    End Sub

End Class