Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure

Namespace EF

    Public Class GenericRepository(Of TEntity As Class, TKey)
        Implements IGenericRepository(Of TEntity, TKey)

        Protected DbContext As DbContext
        Protected DbSet As DbSet(Of TEntity)

        Public Sub New(dbContext As DbContext)

            If dbContext Is Nothing Then
                Throw New ArgumentNullException("Null Entity Framework DbContext")
            End If

            Me.DbContext = dbContext
            Me.DbSet = dbContext.Set(Of TEntity)()

        End Sub

        Public Sub Add(entity As TEntity) Implements IGenericRepository(Of TEntity, TKey).Add

            Dim dbEntityEntry As DbEntityEntry = DbContext.Entry(entity)
            If dbEntityEntry.State <> EntityState.Detached Then
                dbEntityEntry.State = EntityState.Added
            Else
                DbSet.Add(entity)
            End If

        End Sub

        Public Function GetAll() As IQueryable(Of TEntity) Implements IGenericRepository(Of TEntity, TKey).GetAll
            Return DbSet
        End Function

        Public Sub Update(entity As TEntity) Implements IGenericRepository(Of TEntity, TKey).Update

            Dim dbEntityEntry As DbEntityEntry = DbContext.Entry(entity)

            If dbEntityEntry.State = EntityState.Detached Then
                DbSet.Attach(entity)
            End If

            dbEntityEntry.State = EntityState.Modified

        End Sub

        Public Function GetById(id As TKey) As TEntity Implements IGenericRepository(Of TEntity, TKey).GetById
            Return DbSet.Find(id)
        End Function

        Public Sub Remove(id As TKey) Implements IGenericRepository(Of TEntity, TKey).Remove

            Dim entity = GetById(id)

            If entity Is Nothing Then
                Exit Sub
            End If

            Remove(entity)

        End Sub

        Public Sub Remove(entity As TEntity) Implements IGenericRepository(Of TEntity, TKey).Remove

            Dim dbEntityEntry As DbEntityEntry = DbContext.Entry(entity)

            If dbEntityEntry.State <> EntityState.Deleted Then
                dbEntityEntry.State = EntityState.Deleted
            Else
                DbSet.Attach(entity)
                DbSet.Remove(entity)
            End If

        End Sub


        Public Sub Commit() Implements IGenericRepository(Of TEntity, TKey).Commit
            DbContext.SaveChanges()
        End Sub

    End Class

    Public Class GenericRepository(Of TEntity As Class)
        Inherits GenericRepository(Of TEntity, Long)
        Implements IGenericRepository(Of TEntity)

        Public Sub New(dbContext As DbContext)
            MyBase.New(dbContext)
        End Sub


    End Class

End Namespace
