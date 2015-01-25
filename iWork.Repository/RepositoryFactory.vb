Imports StructureMap
Imports StructureMap.Configuration.DSL
Imports StructureMap.Graph.AssemblyScannerExtensions
Imports StructureMap.Pipeline
Imports System.Data.Entity
Imports StructureMap.Web.Pipeline

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

                                                              init.For(Of DbContext).Use(Of iWorkEntities)()
                                                              init.For(Of iWorkEntities).LifecycleIs(Of HybridLifecycle)()
                                                              init.For(Of DbContext).LifecycleIs(Of HybridLifecycle)()

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

        'Dim ctx1 = StructureMapContainer.GetInstance(Of iWorkEntities)()
        'Dim ctx2

        'Using x = StructureMapContainer.GetInstance(Of IUnitOfWork)()
        '    ctx2 = StructureMapContainer.GetInstance(Of iWorkEntities)()
        '    Dim k1 = ctx1 Is ctx2
        'End Using

        'Using x = StructureMapContainer.GetInstance(Of IUnitOfWork)()
        '    ctx2 = StructureMapContainer.GetInstance(Of iWorkEntities)()
        '    Dim k1 = ctx1 Is ctx2
        'End Using

        'Dim ctx3 = StructureMapContainer.GetInstance(Of iWorkEntities)()
        'Dim k12 = ctx1 Is ctx3

        Return StructureMapContainer.GetInstance(Of T)()

    End Function

End Class
